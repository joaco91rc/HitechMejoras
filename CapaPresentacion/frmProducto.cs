using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
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
    public partial class frmProducto : Form
    {
        private Usuario usuarioActual;
        private Image defaultImage = Properties.Resources.CHECK;
        private DataTable dtProductos;
        private DataTable dtProductosOriginal;
        public decimal cotizacionActiva { get; set; } = new CN_Cotizacion().CotizacionActiva().importe;
        public frmProducto(Usuario usuario)
        {
            usuarioActual = usuario;
            InitializeComponent();
        }


        private void CargarCombos()
        {
            List<Categoria> listaCategoria = new CN_Categoria().Listar();

            foreach (Categoria item in listaCategoria)
            {
                cboCategoria.Items.Add(new OpcionCombo() { Valor = item.idCategoria, Texto = item.descripcion });
            }
            cboCategoria.DisplayMember = "Texto";
            cboCategoria.ValueMember = "Valor";
            cboCategoria.SelectedIndex = 0;

            List<Moneda> listaMonedas = new CN_Moneda().ListarMonedas();

            foreach (Moneda item in listaMonedas)
            {
                cboMonedas.Items.Add(new OpcionCombo() { Valor = item.IdMoneda, Texto = item.Simbolo });
            }
            cboMonedas.DisplayMember = "Texto";
            cboMonedas.ValueMember = "Valor";
            cboMonedas.SelectedIndex = 0;

            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;
        }

        

        private void frmProducto_Load(object sender, EventArgs e)
        {
                               
            
            CargarCombos();

                      

            //CargarGrilla();
            dtProductos = new CN_Producto().ListarProductos(GlobalSettings.SucursalId);
            dtProductosOriginal = dtProductos;
            CargarDataGridView(dtProductos);
            foreach (DataColumn columna in dtProductosOriginal.Columns)
            {
                // Agrega cada columna del DataTable al ComboBox, excepto "btnSeleccionar", "ProductoId" y "idCategoria"
                if (columna.ColumnName != "btnSeleccionar" && columna.ColumnName != "ProductoId" && columna.ColumnName != "idCategoria")
                {
                    cboBusqueda.Items.Add(new OpcionCombo()
                    {
                        Valor = columna.ColumnName,
                        Texto = columna.ColumnName.ToUpper() // Convierte el texto a mayúsculas
                    });
                }
            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 1;





        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("El campo 'Código' es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo 'Nombre' es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboCategoria.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPrecioLista.Value<= 0 && cboMonedas.Text == "ARS")
            {
                MessageBox.Show("Debe establecer el precio de Lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear el objeto Producto
            Producto objProducto = new Producto()
            {
                idProducto = Convert.ToInt32(txtIdProducto.Text),
                codigo = txtCodigo.Text,
                nombre = txtNombre.Text,
                descripcion = txtDescripcion.Text,
                oCategoria = new Categoria { idCategoria = Convert.ToInt32(((OpcionCombo)cboCategoria.SelectedItem).Valor) },
                estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false,
                prodSerializable = checkSerializable.Checked,
                productoDolar = checkProductoEnDolares.Checked,
                precioLista = txtPrecioLista.Value,
                precioVenta = Math.Round((txtPrecioLista.Value / cotizacionActiva), 2),
                precioCompra = Math.Round((txtCosto.Value / cotizacionActiva), 2),
                ventaPesos = Math.Round(txtPrecioLista.Value * 0.85m, 2),
                costoPesos = txtCosto.Value
            };

            if (checkCostoDolares.Checked)
            {
                lblPrecioVenta.Visible = true;
                txtPrecioVenta.Visible  =true;
                lblPrecioVenta.Visible = true;
                txtPrecioVenta.Visible = true;
                objProducto.precioCompra = txtPrecioVenta.Value;
                objProducto.precioVenta = txtPrecioVenta.Value;

            } else
            {
                lblPrecioVenta.Visible = false;
                txtPrecioVenta.Visible = false;
                lblPrecioVenta.Visible = false;
                txtPrecioVenta.Visible = false;
                objProducto.precioCompra = Math.Round((txtCosto.Value / cotizacionActiva), 2);
                objProducto.precioVenta = Math.Round((txtPrecioLista.Value / cotizacionActiva), 2);
            }

            
            string simboloMoneda = ((OpcionCombo)cboMonedas.SelectedItem).Texto;
           // string simboloMonedaObjeto = new CN_Moneda().ObtenerMonedaPorId(objPrecioProducto.IdMoneda).Simbolo;
            // Guardar o actualizar en base de datos
            if (objProducto.idProducto == 0) // Nuevo producto
            {
                int idProductoGenerado = new CN_Producto().Registrar(objProducto, out mensaje);
                if (idProductoGenerado != 0)
                {
                    objProducto.idProducto = idProductoGenerado;
                    
                    if(simboloMoneda == "ARS")
                    {
                        PrecioProducto objPrecioProductoPesos = new PrecioProducto()
                        {
                            IdProducto = idProductoGenerado,
                            IdMoneda = 1,
                            PrecioCompra = Math.Round(txtCosto.Value,2),
                            PrecioVenta = Math.Round(txtPrecioLista.Value,2),
                            PrecioLista = Math.Round(txtPrecioLista.Value,2),
                            PrecioEfectivo = Math.Round(txtPrecioLista.Value * 0.85m, 2),
                            FechaRegistro = DateTime.Now
                        };
                        int idPrecioPesos = new CN_PrecioProducto().RegistrarPrecioProducto(objPrecioProductoPesos, out mensaje);
                        PrecioProducto objPrecioProductoDolar = new PrecioProducto()
                        {
                            IdProducto= idProductoGenerado,
                            IdMoneda = 2,
                            PrecioCompra = Math.Round((txtCosto.Value / cotizacionActiva), 2),
                            PrecioVenta = Math.Round((txtPrecioLista.Value / cotizacionActiva), 2),
                            FechaRegistro = DateTime.Now,
                            PrecioEfectivo = Math.Round(txtPrecioLista.Value * 0.85m, 2),
                            PrecioLista = Math.Round(txtPrecioLista.Value, 2)
                        };
                        int idPrecioDolar = new CN_PrecioProducto().RegistrarPrecioProducto(objPrecioProductoDolar, out mensaje);
                    }else if (simboloMoneda == "USD")
                    {

                        PrecioProducto objPrecioProductoPesos = new PrecioProducto()
                        {
                            IdProducto = idProductoGenerado,
                            IdMoneda = 1,
                            PrecioCompra = Math.Round((txtCosto.Value * cotizacionActiva), 2),
                            PrecioVenta = Math.Round((txtPrecioVenta.Value*cotizacionActiva),2),
                            PrecioLista = Math.Round(txtPrecioVenta.Value * cotizacionActiva * 1.35m, 2),
                            PrecioEfectivo = Math.Round((txtPrecioVenta.Value * cotizacionActiva * 1.35m) * 0.85m, 2),
                            FechaRegistro = DateTime.Now
                        };
                        int idPrecioPesos = new CN_PrecioProducto().RegistrarPrecioProducto(objPrecioProductoPesos, out mensaje);
                        PrecioProducto objPrecioProductoDolar = new PrecioProducto()
                        {
                            IdProducto = idProductoGenerado,
                            IdMoneda = 2,
                            PrecioCompra = Math.Round(txtCosto.Value, 2),
                            PrecioVenta = Math.Round(txtPrecioVenta.Value, 2),
                            FechaRegistro = DateTime.Now,
                            PrecioEfectivo = Math.Round(txtPrecioVenta.Value, 2),
                            PrecioLista = Math.Round(txtPrecioVenta.Value * cotizacionActiva * 1.35m, 2)
                        };
                        int idPrecioDolar = new CN_PrecioProducto().RegistrarPrecioProducto(objPrecioProductoDolar, out mensaje);

                    }
                    AgregarProductoAlDataTable(objProducto);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else // Actualizar producto existente
            {
                bool resultado = new CN_Producto().Editar(objProducto, out mensaje);
                if (resultado)
                {
                    PrecioProducto precioPesos = new CN_PrecioProducto().ObtenerPreciosPorProductoYMoneda(objProducto.idProducto, 1);
                    PrecioProducto precioDolar = new CN_PrecioProducto().ObtenerPreciosPorProductoYMoneda(objProducto.idProducto, 2);
                    if (simboloMoneda == "ARS")
                    {
                        PrecioProducto objPrecioProductoPesos = new PrecioProducto()
                        {
                            IdProducto = objProducto.idProducto,
                            IdPrecioProducto = precioPesos.IdPrecioProducto,
                            IdMoneda = 1,
                            PrecioCompra = Math.Round(txtCosto.Value, 2),
                            PrecioVenta = Math.Round(txtPrecioLista.Value, 2),
                            PrecioLista = Math.Round(txtPrecioLista.Value, 2),
                            PrecioEfectivo = Math.Round(txtPrecioLista.Value * 0.85m, 2),
                            FechaRegistro = DateTime.Now
                        };
                        bool editarPrecioPesos = new CN_PrecioProducto().EditarPrecioProducto(objPrecioProductoPesos, out mensaje);
                        
                        PrecioProducto objPrecioProductoDolar = new PrecioProducto()
                        {
                            IdProducto = objProducto.idProducto,
                            IdPrecioProducto = precioDolar.IdPrecioProducto,
                            IdMoneda = 2,
                            PrecioCompra = Math.Round((txtCosto.Value / cotizacionActiva), 2),
                            PrecioVenta = Math.Round((txtPrecioLista.Value / cotizacionActiva), 2),
                            FechaRegistro = DateTime.Now,
                            PrecioEfectivo = Math.Round(txtPrecioLista.Value * 0.85m, 2),
                            PrecioLista = Math.Round(txtPrecioLista.Value, 2)
                        };
                        bool editarPrecioDolares = new CN_PrecioProducto().EditarPrecioProducto(objPrecioProductoDolar, out mensaje);
                        

                    }
                    else if (simboloMoneda == "USD")
                    {

                        PrecioProducto objPrecioProductoPesos = new PrecioProducto()
                        {
                            IdProducto = objProducto.idProducto,
                            IdPrecioProducto = precioPesos.IdPrecioProducto,
                            IdMoneda = 1,
                            PrecioCompra = Math.Round((txtCosto.Value * cotizacionActiva), 2),
                            PrecioVenta = Math.Round((txtPrecioVenta.Value * cotizacionActiva), 2),
                            PrecioLista = Math.Round(txtPrecioVenta.Value * cotizacionActiva * 1.35m, 2),
                            PrecioEfectivo = Math.Round((txtPrecioVenta.Value * cotizacionActiva * 1.35m) * 0.85m, 2),
                            FechaRegistro = DateTime.Now
                        };
                        bool editarPrecioPesos = new CN_PrecioProducto().EditarPrecioProducto(objPrecioProductoPesos, out mensaje);
                        
                        PrecioProducto objPrecioProductoDolar = new PrecioProducto()
                        {
                            IdProducto = objProducto.idProducto,
                            IdPrecioProducto = precioDolar.IdPrecioProducto,
                            IdMoneda = 2,
                            PrecioCompra = Math.Round(txtCosto.Value, 2),
                            PrecioVenta = Math.Round(txtPrecioVenta.Value, 2),
                            FechaRegistro = DateTime.Now,
                            PrecioEfectivo = Math.Round(txtPrecioVenta.Value, 2),
                            PrecioLista = Math.Round(txtPrecioVenta.Value * cotizacionActiva * 1.35m, 2)
                        };
                        bool editarPrecioDolar = new CN_PrecioProducto().EditarPrecioProducto(objPrecioProductoDolar, out mensaje);
                        
                        
                    }
                    ActualizarProductoEnDataTable(objProducto);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }

        private void AgregarProductoAlDataTable(Producto producto)
        {
            DataRow newRow = dtProductos.NewRow();

            newRow["ProductoId"] = producto.idProducto;
            newRow["codigo"] = producto.codigo;
            newRow["nombre"] = producto.nombre;
            newRow["descripcion"] = producto.descripcion;
            newRow["idCategoria"] = producto.oCategoria.idCategoria;
            newRow["DescripcionCategoria"] = ((OpcionCombo)cboCategoria.SelectedItem).Texto;

            // Obtener el stock basado en la sucursal seleccionada
            newRow["stock"] = new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(producto.idProducto, GlobalSettings.SucursalId);

            // Obtener los precios desde la tabla precio_producto
            PrecioProducto preciosPesos = new CN_PrecioProducto().ObtenerPreciosPorProductoYMoneda(producto.idProducto, 1);
            PrecioProducto preciosDolares = new CN_PrecioProducto().ObtenerPreciosPorProductoYMoneda(producto.idProducto, 2);

            // Precios en Pesos (idMoneda = 1) con símbolo
            newRow["precioCompraPesos"] = string.Format("{0} {1:0.00}", preciosPesos.SimboloMoneda, preciosPesos.PrecioCompra);
            newRow["precioVentaPesos"] = string.Format("{0} {1:0.00}", preciosPesos.SimboloMoneda, preciosPesos.PrecioVenta);
            newRow["precioListaPesos"] = string.Format("{0} {1:0.00}", preciosPesos.SimboloMoneda, preciosPesos.PrecioLista);
            newRow["precioEfectivoPesos"] = string.Format("{0} {1:0.00}", preciosPesos.SimboloMoneda, preciosPesos.PrecioEfectivo);

            // Precios en Dólares (idMoneda = 2) con símbolo
            newRow["precioCompraDolar"] = string.Format("{0} {1:0.00}", preciosDolares.SimboloMoneda, preciosDolares.PrecioCompra);
            newRow["precioVentaDolar"] = string.Format("{0} {1:0.00}", preciosDolares.SimboloMoneda, preciosDolares.PrecioVenta);

            // Estado (Activo/Inactivo)
            newRow["estado"] = ((OpcionCombo)cboEstado.SelectedItem).Texto;

            // prodSerializable ("SI"/"NO")
            newRow["prodSerializable"] = producto.prodSerializable ? "SI" : "NO";
            newRow["productoDolar"] = producto.productoDolar ? "SI" : "NO";

            // Otros campos
            // newRow["fechaUltimaVenta"] = producto.fechaUltimaVenta.HasValue ? producto.fechaUltimaVenta.Value.ToString("yyyy-MM-dd") : DBNull.Value;
            // newRow["diasSinVenta"] = producto.diasSinVenta;

            dtProductos.Rows.Add(newRow);
        }



        private void ActualizarProductoEnDataTable(Producto producto)
        {
            foreach (DataRow row in dtProductos.Rows)
            {
                // Verifica si el ID del producto coincide con el ProductoId en el DataTable
                if (Convert.ToInt32(row["ProductoId"]) == producto.idProducto)
                {
                    row["codigo"] = producto.codigo;
                    row["nombre"] = producto.nombre;
                    row["descripcion"] = producto.descripcion;

                    // Actualización de la categoría
                    row["idCategoria"] = producto.oCategoria.idCategoria;
                    row["DescripcionCategoria"] = ((OpcionCombo)cboCategoria.SelectedItem).Texto;

                    // Stock actualizado
                    row["stock"] = new CN_ProductoNegocio().ObtenerStockProductoEnSucursal(producto.idProducto, GlobalSettings.SucursalId);

                    // Obtener los precios desde la tabla precio_producto
                    PrecioProducto preciosPesos = new CN_PrecioProducto().ObtenerPreciosPorProductoYMoneda(producto.idProducto, 1);
                    PrecioProducto preciosDolares = new CN_PrecioProducto().ObtenerPreciosPorProductoYMoneda(producto.idProducto, 2);

                    // Precios en pesos (idMoneda = 1) con símbolo
                    row["precioCompraPesos"] = string.Format("{0} {1:0.00}", preciosPesos.SimboloMoneda, preciosPesos.PrecioCompra);
                    row["precioVentaPesos"] = string.Format("{0} {1:0.00}", preciosPesos.SimboloMoneda, preciosPesos.PrecioVenta);
                    row["precioListaPesos"] = string.Format("{0} {1:0.00}", preciosPesos.SimboloMoneda, preciosPesos.PrecioLista);
                    row["precioEfectivoPesos"] = string.Format("{0} {1:0.00}", preciosPesos.SimboloMoneda, preciosPesos.PrecioEfectivo);

                    // Precios en dólares (idMoneda = 2) con símbolo
                    row["precioCompraDolar"] = string.Format("{0} {1:0.00}", preciosDolares.SimboloMoneda, preciosDolares.PrecioCompra);
                    row["precioVentaDolar"] = string.Format("{0} {1:0.00}", preciosDolares.SimboloMoneda, preciosDolares.PrecioVenta);

                    // Estado: Activo o Inactivo
                    row["estado"] = producto.estado ? "Activo" : "No Activo";

                    // Serialización del producto: Sí o No
                    row["prodSerializable"] = producto.prodSerializable ? "SI" : "NO";
                    row["productoDolar"] = producto.productoDolar ? "SI" : "NO";
                    break; // Termina el bucle una vez encontrado el producto
                }
            }
        }







        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtIdProducto.Text = "0";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtCosto.Visible = true;
            
            checkCostoDolares.Checked = false;
            txtPrecioVenta.Visible = false;
            txtPrecioVenta.Value = 0;
            
            cboCategoria.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
            txtCodigo.Select();
            txtProductoSeleccionado.Text = "Ninguno";
            txtCosto.Value = 0;
            txtPrecioLista.Value = 0;
            checkSerializable.Checked = false;
            
        }



        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica si la columna clicada es "btnSeleccionar"
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    // Asignar valores a los campos de texto desde la fila seleccionada
                    txtIndice.Text = indice.ToString();
                    txtProductoSeleccionado.Text = dgvData.Rows[indice].Cells["nombre"].Value.ToString();
                    txtIdProducto.Text = dgvData.Rows[indice].Cells["ProductoId"].Value.ToString();
                    txtCodigo.Text = dgvData.Rows[indice].Cells["codigo"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["nombre"].Value.ToString();
                    txtDescripcion.Text = dgvData.Rows[indice].Cells["descripcion"].Value.ToString();

                    // Eliminar símbolo de moneda y convertir a número para el NumericUpDown
                    decimal precioLista = Convert.ToDecimal(RemoverSimboloMoneda(dgvData.Rows[indice].Cells["precioListaPesos"].Value.ToString()));
                    decimal precioVenta = Convert.ToDecimal(RemoverSimboloMoneda(dgvData.Rows[indice].Cells["precioVentaPesos"].Value.ToString()));
                    decimal costo = Convert.ToDecimal(RemoverSimboloMoneda(dgvData.Rows[indice].Cells["precioCompraPesos"].Value.ToString()));
                    decimal costoDolar = Convert.ToDecimal(RemoverSimboloMoneda(dgvData.Rows[indice].Cells["precioCompraDolar"].Value.ToString()));
                    decimal precioVentaDolar = Convert.ToDecimal(RemoverSimboloMoneda(dgvData.Rows[indice].Cells["precioVentaDolar"].Value.ToString()));
                    // Verificar si prodSerializable es "Sí" o "No" y marcar el CheckBox en consecuencia
                    checkSerializable.Checked = dgvData.Rows[indice].Cells["prodSerializable"].Value.ToString() == "SI";
                    checkProductoEnDolares.Checked = dgvData.Rows[indice].Cells["productoDolar"].Value.ToString() == "SI";

                    if (checkProductoEnDolares.Checked)
                    {
                        txtPrecioVenta.Visible = true;
                        lblPrecioVenta.Visible = true;
                        lblPrecioLista.Visible = false;
                        txtPrecioLista.Visible = false;
                        // Asignar a los controles
                        txtPrecioLista.Text = precioLista.ToString("N2");
                        txtPrecioVenta.Text = precioVentaDolar.ToString("N2");
                    
                        txtCosto.Text = costoDolar.ToString("N2");
                        cboMonedas.SelectedIndex = 1;
                          
                    } else
                    {
                        txtPrecioVenta.Visible = false;
                        lblPrecioVenta.Visible = false;
                        lblPrecioLista.Visible = true;
                        txtPrecioLista.Visible = true;
                        // Asignar a los controles
                        txtPrecioLista.Text = precioLista.ToString("N2");
                        txtPrecioVenta.Text = precioVenta.ToString("N2");

                        txtCosto.Text = costo.ToString("N2");
                        cboMonedas.SelectedIndex = 0;

                    }

                    // Seleccionar la categoría en el ComboBox
                    foreach (OpcionCombo oc in cboCategoria.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["idCategoria"].Value))
                        {
                            cboCategoria.SelectedIndex = cboCategoria.Items.IndexOf(oc);
                            break;
                        }
                    }

                    // Verificar el estado: si es "Activo" selecciona 1, si es "Inactivo" selecciona 0
                    string estado = dgvData.Rows[indice].Cells["estado"].Value.ToString();
                    foreach (OpcionCombo oc in cboEstado.Items)
                    {
                        if (estado == "Activo" && Convert.ToInt32(oc.Valor) == 1 ||
                            estado == "No Activo" && Convert.ToInt32(oc.Valor) == 0)
                        {
                            cboEstado.SelectedIndex = cboEstado.Items.IndexOf(oc);
                            break;
                        }
                    }
                }
            }
        }

        private string RemoverSimboloMoneda(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return "0";

            // Remueve los primeros caracteres (ej. "ARS " o "$ ") y devuelve el resto
            return valor.Trim().Substring(4); // Ajusta según el formato de tu dato
        }




        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdProducto.Text) != 0)
            {
                if (MessageBox.Show("Desea eliminar el Producto?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    int idProducto = Convert.ToInt32(txtIdProducto.Text);

                    bool respuesta = new CN_Producto().DarBajaLogica(idProducto, out mensaje);
                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));  // Eliminar la fila de la grilla
                        MessageBox.Show("Producto Eliminado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();
            string filtroTexto = txtBusqueda.Text.Trim().ToUpper();

            // Verifica que el DataSource sea un DataTable
            if (dgvData.DataSource is DataTable dt)
            {
                // Crea un DataView a partir del DataTable
                DataView dv = dt.DefaultView;

                // Aplica el filtro en la columna seleccionada
                dv.RowFilter = string.Format("{0} LIKE '%{1}%'", columnaFiltro, filtroTexto);

                // Asigna el DataView filtrado al DataGridView
                dgvData.DataSource = dv;
            }
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear(); // Limpia el cuadro de búsqueda

            // Solo vuelve a cargar el DataGridView si hay datos en dtProductosOriginal
            if (dtProductosOriginal != null)
            {
                CargarDataGridView(dtProductosOriginal);
            }
            else
            {
                MessageBox.Show("No se ha cargado ningún producto aún.");
            }
        }


        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para Exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {
                DataTable dt = new DataTable();

                foreach (DataGridViewColumn columna in dgvData.Columns)
                {
                    if (columna.HeaderText != "" && columna.Visible)
                    {


                        dt.Columns.Add(columna.HeaderText, typeof(string));

                    }
                }

                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Visible)
                    {
                        dt.Rows.Add(new object[]
                        {
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[6].Value.ToString(),
                            row.Cells[7].Value.ToString(),
                            row.Cells[8].Value.ToString(),
                            row.Cells[9].Value.ToString(),
                            row.Cells[10].Value.ToString(),
                            row.Cells[11].Value.ToString(),
                            row.Cells[12].Value.ToString(),
                            row.Cells[13].Value.ToString(),
                            row.Cells[14].Value.ToString(),
                            row.Cells[15].Value.ToString()
                            

                        });
                    }



                }
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("ReporteProducto_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                saveFile.Filter = "Excel Files | *.xlsx";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe Productos");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(saveFile.FileName);
                        MessageBox.Show("Planilla Exportada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch
                    {
                        MessageBox.Show("Error al generar la Planilla de Excel", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

            }
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();
                string filtroTexto = txtBusqueda.Text.Trim().ToUpper();

                // Verifica que el DataSource sea un DataTable
                if (dgvData.DataSource is DataTable dt)
                {
                    // Crea un DataView a partir del DataTable
                    DataView dv = dt.DefaultView;

                    // Aplica el filtro en la columna seleccionada
                    dv.RowFilter = string.Format("{0} LIKE '%{1}%'", columnaFiltro, filtroTexto);

                    // Asigna el DataView filtrado al DataGridView
                    dgvData.DataSource = dv;
                }
            }
        }


        private void checkCostoPesos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCostoDolares.Checked)
            {
                
                lblPrecioVenta.Visible = true;
                txtPrecioVenta.Visible = true;
                lblPrecioVenta.Visible = true;
                txtPrecioVenta.Visible = true;

            }
            else
            {
                
                lblPrecioVenta.Visible = false;
                txtPrecioVenta.Visible = false;
                lblPrecioVenta.Visible = false;
                txtPrecioVenta.Visible = false;
                txtPrecioLista.Select();

            }
        }

        private void CargarDataGridView(DataTable dtProductos)
        {
            dgvData.Columns.Clear(); // Limpiar columnas existentes

            // Guardar el DataTable en la variable global
            dtProductosOriginal = dtProductos.Copy();

            // Crear y configurar la columna de imagen
            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
            imgColumn.Name = "btnSeleccionar";
            imgColumn.HeaderText = "";
            imgColumn.Image = Properties.Resources.CHECK; // Reemplaza con tu icono de selección
            imgColumn.ImageLayout = DataGridViewImageCellLayout.Stretch; // Ajustar la imagen
            dgvData.Columns.Add(imgColumn); // Agregar la columna de imagen al DataGridView

            // Asignar el DataSource al DataGridView
            dgvData.DataSource = dtProductos;

            // Renombrar encabezados de las columnas
            dgvData.Columns["ProductoId"].HeaderText = "Producto ID";
            dgvData.Columns["codigo"].HeaderText = "Código";
            dgvData.Columns["nombre"].HeaderText = "Producto";
            dgvData.Columns["descripcion"].HeaderText = "Descripción";
            dgvData.Columns["DescripcionCategoria"].HeaderText = "Categoría";
            dgvData.Columns["stock"].HeaderText = "Stock";
            dgvData.Columns["precioCompraPesos"].HeaderText = "Costo ARS";
            dgvData.Columns["precioVentaPesos"].HeaderText = "Precio Venta ARS";
            dgvData.Columns["precioListaPesos"].HeaderText = "Precio Lista ARS";
            dgvData.Columns["precioEfectivoPesos"].HeaderText = "Precio Efectivo ARS";
            dgvData.Columns["precioCompraDolar"].HeaderText = "Costo USD";
            dgvData.Columns["precioVentaDolar"].HeaderText = "Precio Venta USD";
            dgvData.Columns["prodSerializable"].HeaderText = "Es Serializable?";
            dgvData.Columns["productoDolar"].HeaderText = "Es Dolarizado?";

            // Ocultar columnas que no deseas mostrar
            dgvData.Columns["idCategoria"].Visible = false;
            dgvData.Columns["ProductoId"].Visible = false;
            dgvData.Columns["estado"].Visible = false;
            dgvData.Columns["precioVentaPesos"].Visible = false;
            dgvData.Columns["descripcion"].Visible = false;

            // Ajustes de tamaño para las columnas
            dgvData.Columns["nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvData.Columns["nombre"].MinimumWidth = 200;
            dgvData.Columns["btnSeleccionar"].Width = 30;
            dgvData.Columns["codigo"].Width = 80;
            dgvData.Columns["descripcion"].Width = 150;
            dgvData.Columns["DescripcionCategoria"].Width = 140;
            dgvData.Columns["stock"].Width = 80;
            dgvData.Columns["precioCompraPesos"].Width = 100;
            dgvData.Columns["precioCompraDolar"].Width = 100;
            dgvData.Columns["precioVentaPesos"].Width = 130;
            dgvData.Columns["precioListaPesos"].Width = 130;
            dgvData.Columns["precioEfectivoPesos"].Width = 150;
            dgvData.Columns["precioVentaDolar"].Width = 140;
            dgvData.Columns["prodSerializable"].Width = 130;
            dgvData.Columns["productoDolar"].Width = 130;

            // Establecer el orden de las columnas usando DisplayIndex
            dgvData.Columns["btnSeleccionar"].DisplayIndex = 0; // Columna de imagen en primer lugar
            dgvData.Columns["codigo"].DisplayIndex = 1;
            dgvData.Columns["nombre"].DisplayIndex = 2;
            //dgvData.Columns["descripcion"].DisplayIndex = 3;
            dgvData.Columns["DescripcionCategoria"].DisplayIndex = 3;
            dgvData.Columns["stock"].DisplayIndex = 4;
            //dgvData.Columns["precioVentaPesos"].DisplayIndex = 6;
            dgvData.Columns["precioListaPesos"].DisplayIndex = 5;
            dgvData.Columns["precioEfectivoPesos"].DisplayIndex = 6;
            dgvData.Columns["precioVentaDolar"].DisplayIndex = 7;
            dgvData.Columns["precioCompraPesos"].DisplayIndex = 8;
            dgvData.Columns["precioCompraDolar"].DisplayIndex = 9;
            dgvData.Columns["prodSerializable"].DisplayIndex = 10;
            dgvData.Columns["productoDolar"].DisplayIndex = 11;
        }










        private void CargarGrilla()
        {
            dgvData.Rows.Clear();
            List<Producto> listaProducto = new CN_Producto().Listar(GlobalSettings.SucursalId);

            foreach (Producto item in listaProducto)
            {
                dgvData.Rows.Add(new object[] {
            defaultImage, // Asignar la imagen predeterminada
            item.idProducto,
            item.codigo,
            item.nombre,
            item.descripcion,
            item.oCategoria.idCategoria,
            item.oCategoria.descripcion,
            item.stock,
            item.precioCompra,
            item.costoPesos,
            item.ventaPesos,
            item.precioVenta,
            (Math.Round((item.precioVenta * cotizacionActiva) / 1000, 0) * 1000 - 100).ToString("0.00"),
            item.prodSerializable ? "SI" : "NO",
            item.estado ? 1 : 0,
            item.estado ? "Activo" : "No Activo",
            item.fechaUltimaVenta.HasValue ? item.fechaUltimaVenta.Value.ToString("dd/MM/yyyy") : "Sin ventas",
            item.diasSinVenta
        });
            }
        }

        private void CargarGrillaPorNegocio()
        {
            dgvData.Rows.Clear();
            //Mostrar todos los Productos
            List<Producto> listaProducto = new CN_Producto().ListarPorNegocio(GlobalSettings.SucursalId);

            foreach (Producto item in listaProducto)
            {
                dgvData.Rows.Add(new object[] {
                defaultImage, // Asignar la imagen predeterminada
                item.idProducto,
                item.codigo,
                item.nombre,
                item.descripcion,
                item.oCategoria.idCategoria,
                item.oCategoria.descripcion,
                item.stock,
                item.precioCompra,
                item.costoPesos,
                item.ventaPesos,
                item.precioVenta,
                (Math.Round((item.precioVenta * cotizacionActiva) / 1000, 0) * 1000 - 100).ToString("0.00"),
                item.prodSerializable == true? "SI":"NO",
                item.estado == true ? 1 : 0,
                item.estado == true ? "Activo" : "No Activo"
                });
            }
        }

        private void checkProductosLocal_CheckedChanged(object sender, EventArgs e)
        {
            // Verifica que dtProductosOriginal esté inicializado
            if (dtProductosOriginal == null)
            {
                // Asegúrate de que el DataTable original esté cargado antes de usarlo
                dtProductosOriginal = new CN_Producto().ListarProductos(GlobalSettings.SucursalId); // O cualquier otra forma de obtener los productos originales
            }

            if (checkProductosLocal.Checked)
            {
                // Cargar los productos filtrados por negocio
                DataTable dtProductosXNegocio = new CN_Producto().ListarProductosPorNegocio(GlobalSettings.SucursalId);
                CargarDataGridView(dtProductosXNegocio); // Cargar los productos del negocio
            }
            else
            {
                dtProductosOriginal = new CN_Producto().ListarProductos(GlobalSettings.SucursalId);
                // Restaurar los productos originales
                CargarDataGridView(dtProductosOriginal); // Cargar los productos originales
            }
        }


        private void btnSetearPrecios_Click(object sender, EventArgs e)
        {
            txtPrecioVenta.Value = 0;
            
            txtCosto.Value = 0;
           

        }

        private void cboMonedas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string simboloMoneda = ((OpcionCombo)cboMonedas.SelectedItem).Texto;
            if (simboloMoneda == "ARS")
            {
                lblPrecioLista.Visible = true;
                txtPrecioLista.Visible = true;
                txtPrecioVenta.Visible = false;
                lblPrecioVenta.Visible = false;
            }else
            {
                lblPrecioLista.Visible = false;
                txtPrecioLista.Visible = false;
                txtPrecioVenta.Visible = true;
                lblPrecioVenta.Visible = true;
            }
        }
    }
}