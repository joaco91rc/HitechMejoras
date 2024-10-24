using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmDetalleProducto : Form
    {

        public int StockProducto { get; set; }
        public frmDetalleProducto()
        {
            InitializeComponent();
        }

        public void ActualizarStock()
        {
            txtStock.Text = StockProducto.ToString();
        }
        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            using (var modal = new mdSeleccionProductosSerializables(this))
            {
                modal.Owner = this;
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtIdProducto.Text = modal._Producto.idProducto.ToString();
                    txtCodigoProducto.Text = modal._Producto.codigo;
                    txtProducto.Text = modal._Producto.nombre;
                    

                    txtCantidad.Select();
                }
                else
                {
                    txtCodigoProducto.Select();
                }
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            // Verificar que el usuario ingresó un número válido en el TextBox txtCantidad
            if (int.TryParse(txtCantidad.Text, out int cantidad))
            {
                // Obtener el stock disponible del producto seleccionado
                int stockDisponible = StockProducto; // Suponiendo que ya tienes el stock del producto en una propiedad

                int productosSerializados = new CN_Producto().ContarProductosSerializados(Convert.ToInt32(txtIdProducto.Text),GlobalSettings.SucursalId);

                // Verificar si todos los productos ya están serializados
                if (productosSerializados >= stockDisponible)
                {
                    // Mostrar mensaje de advertencia si el stock ya fue serializado por completo
                    MessageBox.Show($"El stock de este producto ya fue completamente serializado ({productosSerializados}/{stockDisponible}).",
                                    "Stock serializado completo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return; // Salir del método si todos los productos ya están serializados
                }
                // Verificar si la cantidad ingresada es mayor que el stock disponible
                if (cantidad > stockDisponible)
                {
                    // Mostrar mensaje de error si la cantidad supera el stock
                    MessageBox.Show($"La cantidad ingresada ({cantidad}) no puede superar el stock disponible ({stockDisponible}) del producto.",
                                    "Stock insuficiente",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;  // Salir del método si la cantidad es mayor al stock
                }

                // Si la cantidad es válida, agregar la fila al DataGridView
                for (int i = 0; i < cantidad; i++)
                {
                    // Obtener los datos del producto desde los TextBox
                    string idProducto = txtIdProducto.Text;
                    string nombreProducto = txtProducto.Text;

                    // Agregar la fila al DataGridView con cantidad = 1
                    dgvData.Rows.Add(idProducto, nombreProducto, 1);
                }
                StockProducto -=   Convert.ToInt32(txtCantidad.Value);
                txtStock.Text = StockProducto.ToString();
            }
            else
            {
                // Mostrar mensaje de error si no se ingresó un número válido
                MessageBox.Show("Por favor, ingresa un número válido en el campo Cantidad.",
                                "Entrada no válida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que la celda clickeada pertenece a la columna del botón de eliminar
            if (dgvData.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {
               
                    // Eliminar la fila seleccionada
                    dgvData.Rows.RemoveAt(e.RowIndex);
                    StockProducto += 1;
                    txtStock.Text = StockProducto.ToString();
                
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Verificar si hay filas en el DataGridView
            if (dgvData.Rows.Count > 0)
            {
                // Variable para el mensaje
                string mensaje;

                // Recorrer cada fila del DataGridView
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    // Asegurarse de que la fila no sea nueva (la última fila es generalmente una fila nueva)
                    if (!row.IsNewRow)
                    {
                        // Obtener los valores de las columnas necesarias
                        int idProducto = Convert.ToInt32(row.Cells["idProducto"].Value);
                        string numeroSerie = row.Cells["serialNumber"].Value.ToString();
                        string color = row.Cells["color"].Value.ToString();
                        string modelo = row.Cells["modelo"].Value.ToString();
                        string marca = row.Cells["marca"].Value.ToString();
                        int idNegocio = GlobalSettings.SucursalId; // Asegúrate de que esta columna exista

                        // Crear un objeto de ProductoDetalle
                        var productoDetalle = new ProductoDetalle
                        {
                            idProducto = idProducto,
                            numeroSerie = numeroSerie,
                            color = color,
                            modelo = modelo,
                            marca = marca,
                            idNegocio = idNegocio
                        };

                        // Llamar al método de la capa de negocio para registrar el número de serie
                        int idProductoDetalle = new CN_Producto().RegistrarSerialNumber(productoDetalle, out mensaje);

                        // Verificar el resultado de la inserción
                        if (idProductoDetalle == 0)
                        {
                            // Mostrar mensaje de error si no se pudo registrar
                            MessageBox.Show($"Error al registrar el número de serie: {mensaje}");
                        }
                    }
                }

                // Si todo salió bien, puedes mostrar un mensaje de éxito
                MessageBox.Show("Números de serie registrados exitosamente.");
                dgvData.Rows.Clear();
                txtIdProducto.Text = "0";
                txtCodigoProducto.Text = string.Empty;
                txtProducto.Text = string.Empty;
                txtCantidad.Value = 1;
                txtStock.Text = string.Empty;
                txtIndice.Text = "-1";
            }
            else
            {
                MessageBox.Show("No hay filas para guardar.");
            }
        }

    }
}
