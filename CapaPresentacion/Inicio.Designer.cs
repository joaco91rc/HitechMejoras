
namespace CapaPresentacion
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuUsuarios = new FontAwesome.Sharp.IconMenuItem();
            this.menuMantenedor = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuCategoria = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuProducto = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuNegocio = new System.Windows.Forms.ToolStripMenuItem();
            this.cotizacionDolarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuFormaPago = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuVendedores = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStock = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuStock = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuTraspasoStock = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSerializacion = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuSerializarProducto = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuProductosSerializados = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuSerialVendidos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVentas = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuRegistrarVenta = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuDetalleVenta = new FontAwesome.Sharp.IconMenuItem();
            this.menuCompras = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuRegistrarCompra = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuDetalleCompra = new FontAwesome.Sharp.IconMenuItem();
            this.menuClientes = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuPagosParciales = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProveedores = new FontAwesome.Sharp.IconMenuItem();
            this.menuReportes = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuReporteCompras = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuReporteVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuReporteCantidadVentasPorLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCajaRegistradora = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuAperturaCaja = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuCajaDiaria = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuConsultaCaja = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuConceptos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRMA = new FontAwesome.Sharp.IconMenuItem();
            this.menuConsultas = new FontAwesome.Sharp.IconMenuItem();
            this.menuConsultaStock = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuDeudas = new FontAwesome.Sharp.IconMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuServicioTecnico = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuIngresoService = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuCobroService = new FontAwesome.Sharp.IconMenuItem();
            this.menuTitulo = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.contenedor = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCambioSucursal = new System.Windows.Forms.Label();
            this.cboSucursal = new System.Windows.Forms.ComboBox();
            this.iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRol = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCerrarSesion = new FontAwesome.Sharp.IconButton();
            this.subMenuMoneda = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.ForestGreen;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUsuarios,
            this.menuMantenedor,
            this.menuStock,
            this.menuSerializacion,
            this.menuVentas,
            this.menuCompras,
            this.menuClientes,
            this.menuProveedores,
            this.menuReportes,
            this.menuCajaRegistradora,
            this.menuRMA,
            this.menuConsultas,
            this.menuServicioTecnico});
            this.menu.Location = new System.Drawing.Point(0, 163);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1366, 79);
            this.menu.TabIndex = 0;
            // 
            // menuUsuarios
            // 
            this.menuUsuarios.AutoSize = false;
            this.menuUsuarios.BackColor = System.Drawing.Color.ForestGreen;
            this.menuUsuarios.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuUsuarios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuUsuarios.IconChar = FontAwesome.Sharp.IconChar.UserCog;
            this.menuUsuarios.IconColor = System.Drawing.Color.Black;
            this.menuUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuUsuarios.IconSize = 50;
            this.menuUsuarios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuUsuarios.Name = "menuUsuarios";
            this.menuUsuarios.Padding = new System.Windows.Forms.Padding(0);
            this.menuUsuarios.Size = new System.Drawing.Size(108, 75);
            this.menuUsuarios.Text = "Usuarios";
            this.menuUsuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuUsuarios.Click += new System.EventHandler(this.menuUsuarios_Click);
            // 
            // menuMantenedor
            // 
            this.menuMantenedor.AutoSize = false;
            this.menuMantenedor.BackColor = System.Drawing.Color.ForestGreen;
            this.menuMantenedor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuCategoria,
            this.subMenuProducto,
            this.subMenuNegocio,
            this.cotizacionDolarToolStripMenuItem,
            this.subMenuFormaPago,
            this.subMenuVendedores,
            this.subMenuMoneda});
            this.menuMantenedor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuMantenedor.ForeColor = System.Drawing.Color.White;
            this.menuMantenedor.IconChar = FontAwesome.Sharp.IconChar.Toolbox;
            this.menuMantenedor.IconColor = System.Drawing.Color.Black;
            this.menuMantenedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuMantenedor.IconSize = 50;
            this.menuMantenedor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuMantenedor.Name = "menuMantenedor";
            this.menuMantenedor.Padding = new System.Windows.Forms.Padding(0);
            this.menuMantenedor.Size = new System.Drawing.Size(122, 75);
            this.menuMantenedor.Text = "Mantenedor";
            this.menuMantenedor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuCategoria
            // 
            this.subMenuCategoria.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuCategoria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.subMenuCategoria.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuCategoria.IconColor = System.Drawing.Color.Black;
            this.subMenuCategoria.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuCategoria.Name = "subMenuCategoria";
            this.subMenuCategoria.Size = new System.Drawing.Size(207, 26);
            this.subMenuCategoria.Text = "Categoria";
            this.subMenuCategoria.Click += new System.EventHandler(this.subMenuCategoria_Click);
            // 
            // subMenuProducto
            // 
            this.subMenuProducto.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuProducto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.subMenuProducto.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuProducto.IconColor = System.Drawing.Color.Black;
            this.subMenuProducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuProducto.Name = "subMenuProducto";
            this.subMenuProducto.Size = new System.Drawing.Size(207, 26);
            this.subMenuProducto.Text = "Producto";
            this.subMenuProducto.Click += new System.EventHandler(this.subMenuProducto_Click);
            // 
            // subMenuNegocio
            // 
            this.subMenuNegocio.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuNegocio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.subMenuNegocio.Name = "subMenuNegocio";
            this.subMenuNegocio.Size = new System.Drawing.Size(207, 26);
            this.subMenuNegocio.Text = "Negocio";
            this.subMenuNegocio.Click += new System.EventHandler(this.subMenuNegocio_Click);
            // 
            // cotizacionDolarToolStripMenuItem
            // 
            this.cotizacionDolarToolStripMenuItem.BackColor = System.Drawing.Color.ForestGreen;
            this.cotizacionDolarToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.cotizacionDolarToolStripMenuItem.Name = "cotizacionDolarToolStripMenuItem";
            this.cotizacionDolarToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.cotizacionDolarToolStripMenuItem.Text = "Cotizacion Dolar";
            this.cotizacionDolarToolStripMenuItem.Click += new System.EventHandler(this.cotizacionDolarToolStripMenuItem_Click);
            // 
            // subMenuFormaPago
            // 
            this.subMenuFormaPago.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuFormaPago.ForeColor = System.Drawing.Color.White;
            this.subMenuFormaPago.Name = "subMenuFormaPago";
            this.subMenuFormaPago.Size = new System.Drawing.Size(207, 26);
            this.subMenuFormaPago.Text = "Formas de Pago";
            this.subMenuFormaPago.Click += new System.EventHandler(this.subMenuFormaPago_Click);
            // 
            // subMenuVendedores
            // 
            this.subMenuVendedores.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuVendedores.ForeColor = System.Drawing.Color.White;
            this.subMenuVendedores.Name = "subMenuVendedores";
            this.subMenuVendedores.Size = new System.Drawing.Size(207, 26);
            this.subMenuVendedores.Text = "Vendedores";
            this.subMenuVendedores.Click += new System.EventHandler(this.subMenuVendedores_Click);
            // 
            // menuStock
            // 
            this.menuStock.AutoSize = false;
            this.menuStock.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuStock,
            this.subMenuTraspasoStock});
            this.menuStock.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuStock.IconChar = FontAwesome.Sharp.IconChar.Clock;
            this.menuStock.IconColor = System.Drawing.Color.Black;
            this.menuStock.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuStock.IconSize = 50;
            this.menuStock.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuStock.Name = "menuStock";
            this.menuStock.Padding = new System.Windows.Forms.Padding(0);
            this.menuStock.Size = new System.Drawing.Size(122, 75);
            this.menuStock.Text = "Stock";
            this.menuStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuStock
            // 
            this.subMenuStock.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuStock.ForeColor = System.Drawing.Color.White;
            this.subMenuStock.Name = "subMenuStock";
            this.subMenuStock.Size = new System.Drawing.Size(259, 26);
            this.subMenuStock.Text = "Stock";
            this.subMenuStock.Click += new System.EventHandler(this.subMenuStock2_Click);
            // 
            // subMenuTraspasoStock
            // 
            this.subMenuTraspasoStock.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuTraspasoStock.ForeColor = System.Drawing.Color.White;
            this.subMenuTraspasoStock.Name = "subMenuTraspasoStock";
            this.subMenuTraspasoStock.Size = new System.Drawing.Size(259, 26);
            this.subMenuTraspasoStock.Text = "Traspaso de Mercaderia";
            this.subMenuTraspasoStock.Click += new System.EventHandler(this.subMenuTraspasoStock_Click);
            // 
            // menuSerializacion
            // 
            this.menuSerializacion.AutoSize = false;
            this.menuSerializacion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuSerializarProducto,
            this.subMenuProductosSerializados,
            this.subMenuSerialVendidos});
            this.menuSerializacion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuSerializacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuSerializacion.IconChar = FontAwesome.Sharp.IconChar.Servicestack;
            this.menuSerializacion.IconColor = System.Drawing.Color.Black;
            this.menuSerializacion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuSerializacion.IconSize = 50;
            this.menuSerializacion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuSerializacion.Name = "menuSerializacion";
            this.menuSerializacion.Padding = new System.Windows.Forms.Padding(0);
            this.menuSerializacion.Size = new System.Drawing.Size(122, 75);
            this.menuSerializacion.Text = "Serializacion";
            this.menuSerializacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuSerializarProducto
            // 
            this.subMenuSerializarProducto.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuSerializarProducto.ForeColor = System.Drawing.Color.White;
            this.subMenuSerializarProducto.Name = "subMenuSerializarProducto";
            this.subMenuSerializarProducto.Size = new System.Drawing.Size(253, 26);
            this.subMenuSerializarProducto.Text = "Serializar Producto";
            this.subMenuSerializarProducto.Click += new System.EventHandler(this.subMenuSerializarProducto_Click_1);
            // 
            // subMenuProductosSerializados
            // 
            this.subMenuProductosSerializados.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuProductosSerializados.ForeColor = System.Drawing.Color.White;
            this.subMenuProductosSerializados.Name = "subMenuProductosSerializados";
            this.subMenuProductosSerializados.Size = new System.Drawing.Size(253, 26);
            this.subMenuProductosSerializados.Text = "Productos Serializados";
            this.subMenuProductosSerializados.Click += new System.EventHandler(this.subMenuProductosSerializados_Click_1);
            // 
            // subMenuSerialVendidos
            // 
            this.subMenuSerialVendidos.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuSerialVendidos.ForeColor = System.Drawing.Color.White;
            this.subMenuSerialVendidos.Name = "subMenuSerialVendidos";
            this.subMenuSerialVendidos.Size = new System.Drawing.Size(253, 26);
            this.subMenuSerialVendidos.Text = "S/N Vendidos";
            this.subMenuSerialVendidos.Click += new System.EventHandler(this.subMenuSerialVendidos_Click);
            // 
            // menuVentas
            // 
            this.menuVentas.AutoSize = false;
            this.menuVentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuRegistrarVenta,
            this.subMenuDetalleVenta});
            this.menuVentas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuVentas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuVentas.IconChar = FontAwesome.Sharp.IconChar.MoneyCheckAlt;
            this.menuVentas.IconColor = System.Drawing.Color.Black;
            this.menuVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuVentas.IconSize = 50;
            this.menuVentas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuVentas.Name = "menuVentas";
            this.menuVentas.Padding = new System.Windows.Forms.Padding(0);
            this.menuVentas.Size = new System.Drawing.Size(122, 75);
            this.menuVentas.Text = "Ventas";
            this.menuVentas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuRegistrarVenta
            // 
            this.subMenuRegistrarVenta.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuRegistrarVenta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.subMenuRegistrarVenta.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuRegistrarVenta.IconColor = System.Drawing.Color.Black;
            this.subMenuRegistrarVenta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuRegistrarVenta.Name = "subMenuRegistrarVenta";
            this.subMenuRegistrarVenta.Size = new System.Drawing.Size(213, 26);
            this.subMenuRegistrarVenta.Text = "Registrar Venta";
            this.subMenuRegistrarVenta.Click += new System.EventHandler(this.subMenuRegistrarVenta_Click);
            // 
            // subMenuDetalleVenta
            // 
            this.subMenuDetalleVenta.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuDetalleVenta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.subMenuDetalleVenta.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuDetalleVenta.IconColor = System.Drawing.Color.Black;
            this.subMenuDetalleVenta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuDetalleVenta.Name = "subMenuDetalleVenta";
            this.subMenuDetalleVenta.Size = new System.Drawing.Size(213, 26);
            this.subMenuDetalleVenta.Text = "Detalle de Ventas";
            this.subMenuDetalleVenta.Click += new System.EventHandler(this.subMenuDetalleVenta_Click);
            // 
            // menuCompras
            // 
            this.menuCompras.AutoSize = false;
            this.menuCompras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuRegistrarCompra,
            this.subMenuDetalleCompra});
            this.menuCompras.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuCompras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuCompras.IconChar = FontAwesome.Sharp.IconChar.Dolly;
            this.menuCompras.IconColor = System.Drawing.Color.Black;
            this.menuCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuCompras.IconSize = 50;
            this.menuCompras.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuCompras.Name = "menuCompras";
            this.menuCompras.Padding = new System.Windows.Forms.Padding(0);
            this.menuCompras.Size = new System.Drawing.Size(122, 75);
            this.menuCompras.Text = "Compras";
            this.menuCompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuRegistrarCompra
            // 
            this.subMenuRegistrarCompra.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuRegistrarCompra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.subMenuRegistrarCompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuRegistrarCompra.IconColor = System.Drawing.Color.Black;
            this.subMenuRegistrarCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuRegistrarCompra.Name = "subMenuRegistrarCompra";
            this.subMenuRegistrarCompra.Size = new System.Drawing.Size(229, 26);
            this.subMenuRegistrarCompra.Text = "Registrar Compra";
            this.subMenuRegistrarCompra.Click += new System.EventHandler(this.subMenuRegistrarCompra_Click);
            // 
            // subMenuDetalleCompra
            // 
            this.subMenuDetalleCompra.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuDetalleCompra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.subMenuDetalleCompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuDetalleCompra.IconColor = System.Drawing.Color.Black;
            this.subMenuDetalleCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuDetalleCompra.Name = "subMenuDetalleCompra";
            this.subMenuDetalleCompra.Size = new System.Drawing.Size(229, 26);
            this.subMenuDetalleCompra.Text = "Detalle de Compras";
            this.subMenuDetalleCompra.Click += new System.EventHandler(this.subMenuDetalleCompra_Click);
            // 
            // menuClientes
            // 
            this.menuClientes.AutoSize = false;
            this.menuClientes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuClientes,
            this.subMenuPagosParciales});
            this.menuClientes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuClientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuClientes.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.menuClientes.IconColor = System.Drawing.Color.Black;
            this.menuClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuClientes.IconSize = 50;
            this.menuClientes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuClientes.Name = "menuClientes";
            this.menuClientes.Padding = new System.Windows.Forms.Padding(0);
            this.menuClientes.Size = new System.Drawing.Size(108, 75);
            this.menuClientes.Text = "Clientes";
            this.menuClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuClientes.Click += new System.EventHandler(this.menuClientes_Click);
            // 
            // subMenuClientes
            // 
            this.subMenuClientes.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuClientes.ForeColor = System.Drawing.Color.White;
            this.subMenuClientes.Name = "subMenuClientes";
            this.subMenuClientes.Size = new System.Drawing.Size(198, 26);
            this.subMenuClientes.Text = "Clientes";
            this.subMenuClientes.Click += new System.EventHandler(this.subMenuClientes_Click);
            // 
            // subMenuPagosParciales
            // 
            this.subMenuPagosParciales.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuPagosParciales.ForeColor = System.Drawing.Color.White;
            this.subMenuPagosParciales.Name = "subMenuPagosParciales";
            this.subMenuPagosParciales.Size = new System.Drawing.Size(198, 26);
            this.subMenuPagosParciales.Text = "Pagos Parciales";
            this.subMenuPagosParciales.Click += new System.EventHandler(this.subMenuPagosParciales_Click);
            // 
            // menuProveedores
            // 
            this.menuProveedores.AutoSize = false;
            this.menuProveedores.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuProveedores.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuProveedores.IconChar = FontAwesome.Sharp.IconChar.AddressCard;
            this.menuProveedores.IconColor = System.Drawing.Color.Black;
            this.menuProveedores.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuProveedores.IconSize = 50;
            this.menuProveedores.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuProveedores.Name = "menuProveedores";
            this.menuProveedores.Padding = new System.Windows.Forms.Padding(0);
            this.menuProveedores.Size = new System.Drawing.Size(108, 75);
            this.menuProveedores.Text = "Proveedores";
            this.menuProveedores.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuProveedores.Click += new System.EventHandler(this.menuProveedores_Click);
            // 
            // menuReportes
            // 
            this.menuReportes.AutoSize = false;
            this.menuReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuReporteCompras,
            this.subMenuReporteVentas,
            this.subMenuReporteCantidadVentasPorLocal});
            this.menuReportes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuReportes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuReportes.IconChar = FontAwesome.Sharp.IconChar.ChartColumn;
            this.menuReportes.IconColor = System.Drawing.Color.Black;
            this.menuReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuReportes.IconSize = 50;
            this.menuReportes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuReportes.Name = "menuReportes";
            this.menuReportes.Padding = new System.Windows.Forms.Padding(0);
            this.menuReportes.Size = new System.Drawing.Size(122, 75);
            this.menuReportes.Text = "Reportes";
            this.menuReportes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuReporteCompras
            // 
            this.subMenuReporteCompras.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuReporteCompras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.subMenuReporteCompras.Name = "subMenuReporteCompras";
            this.subMenuReporteCompras.Size = new System.Drawing.Size(383, 26);
            this.subMenuReporteCompras.Text = "Compras";
            this.subMenuReporteCompras.Click += new System.EventHandler(this.subMenuReporteCompras_Click);
            // 
            // subMenuReporteVentas
            // 
            this.subMenuReporteVentas.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuReporteVentas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.subMenuReporteVentas.Name = "subMenuReporteVentas";
            this.subMenuReporteVentas.Size = new System.Drawing.Size(383, 26);
            this.subMenuReporteVentas.Text = "Ventas - Margenes";
            this.subMenuReporteVentas.Click += new System.EventHandler(this.subMenuReporteVentas_Click);
            // 
            // subMenuReporteCantidadVentasPorLocal
            // 
            this.subMenuReporteCantidadVentasPorLocal.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuReporteCantidadVentasPorLocal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.subMenuReporteCantidadVentasPorLocal.Name = "subMenuReporteCantidadVentasPorLocal";
            this.subMenuReporteCantidadVentasPorLocal.Size = new System.Drawing.Size(383, 26);
            this.subMenuReporteCantidadVentasPorLocal.Text = "Cantidad Productos  Vendidos por Local";
            this.subMenuReporteCantidadVentasPorLocal.Click += new System.EventHandler(this.subMenuReporteCantidadVentasPorLocal_Click);
            // 
            // menuCajaRegistradora
            // 
            this.menuCajaRegistradora.AutoSize = false;
            this.menuCajaRegistradora.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuAperturaCaja,
            this.subMenuCajaDiaria,
            this.subMenuConsultaCaja,
            this.subMenuConceptos});
            this.menuCajaRegistradora.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuCajaRegistradora.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuCajaRegistradora.IconChar = FontAwesome.Sharp.IconChar.CashRegister;
            this.menuCajaRegistradora.IconColor = System.Drawing.Color.Black;
            this.menuCajaRegistradora.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuCajaRegistradora.IconSize = 50;
            this.menuCajaRegistradora.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuCajaRegistradora.Name = "menuCajaRegistradora";
            this.menuCajaRegistradora.Padding = new System.Windows.Forms.Padding(0);
            this.menuCajaRegistradora.Size = new System.Drawing.Size(108, 75);
            this.menuCajaRegistradora.Text = "Caja";
            this.menuCajaRegistradora.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuAperturaCaja
            // 
            this.subMenuAperturaCaja.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuAperturaCaja.ForeColor = System.Drawing.Color.White;
            this.subMenuAperturaCaja.Name = "subMenuAperturaCaja";
            this.subMenuAperturaCaja.Size = new System.Drawing.Size(207, 26);
            this.subMenuAperturaCaja.Text = "Apertura caja";
            this.subMenuAperturaCaja.Click += new System.EventHandler(this.subMenuAperturaCaja_Click);
            // 
            // subMenuCajaDiaria
            // 
            this.subMenuCajaDiaria.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuCajaDiaria.ForeColor = System.Drawing.Color.White;
            this.subMenuCajaDiaria.Name = "subMenuCajaDiaria";
            this.subMenuCajaDiaria.Size = new System.Drawing.Size(207, 26);
            this.subMenuCajaDiaria.Text = "Caja Diaria";
            this.subMenuCajaDiaria.Click += new System.EventHandler(this.subMenuCajaDiaria_Click);
            // 
            // subMenuConsultaCaja
            // 
            this.subMenuConsultaCaja.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuConsultaCaja.ForeColor = System.Drawing.Color.White;
            this.subMenuConsultaCaja.Name = "subMenuConsultaCaja";
            this.subMenuConsultaCaja.Size = new System.Drawing.Size(207, 26);
            this.subMenuConsultaCaja.Text = "Consulta de Caja";
            this.subMenuConsultaCaja.Click += new System.EventHandler(this.subMenuConsultaCaja_Click);
            // 
            // subMenuConceptos
            // 
            this.subMenuConceptos.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuConceptos.ForeColor = System.Drawing.Color.White;
            this.subMenuConceptos.Name = "subMenuConceptos";
            this.subMenuConceptos.Size = new System.Drawing.Size(207, 26);
            this.subMenuConceptos.Text = "Conceptos";
            this.subMenuConceptos.Click += new System.EventHandler(this.subMenuConceptos_Click);
            // 
            // menuRMA
            // 
            this.menuRMA.AutoSize = false;
            this.menuRMA.BackColor = System.Drawing.Color.ForestGreen;
            this.menuRMA.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuRMA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuRMA.IconChar = FontAwesome.Sharp.IconChar.UserCog;
            this.menuRMA.IconColor = System.Drawing.Color.Black;
            this.menuRMA.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuRMA.IconSize = 50;
            this.menuRMA.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuRMA.Name = "menuRMA";
            this.menuRMA.Padding = new System.Windows.Forms.Padding(0);
            this.menuRMA.Size = new System.Drawing.Size(108, 75);
            this.menuRMA.Text = "RMA";
            this.menuRMA.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuRMA.Click += new System.EventHandler(this.menuRMA_Click);
            // 
            // menuConsultas
            // 
            this.menuConsultas.AutoSize = false;
            this.menuConsultas.BackColor = System.Drawing.Color.ForestGreen;
            this.menuConsultas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConsultaStock,
            this.subMenuDeudas,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.menuConsultas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuConsultas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuConsultas.IconChar = FontAwesome.Sharp.IconChar.ListUl;
            this.menuConsultas.IconColor = System.Drawing.Color.Black;
            this.menuConsultas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuConsultas.IconSize = 50;
            this.menuConsultas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuConsultas.Name = "menuConsultas";
            this.menuConsultas.Padding = new System.Windows.Forms.Padding(0);
            this.menuConsultas.Size = new System.Drawing.Size(122, 75);
            this.menuConsultas.Text = "Consultas";
            this.menuConsultas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // menuConsultaStock
            // 
            this.menuConsultaStock.BackColor = System.Drawing.Color.ForestGreen;
            this.menuConsultaStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuConsultaStock.IconChar = FontAwesome.Sharp.IconChar.None;
            this.menuConsultaStock.IconColor = System.Drawing.Color.Black;
            this.menuConsultaStock.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuConsultaStock.Name = "menuConsultaStock";
            this.menuConsultaStock.Size = new System.Drawing.Size(253, 26);
            this.menuConsultaStock.Text = "Productos";
            this.menuConsultaStock.Click += new System.EventHandler(this.menuConsultaStock_Click);
            // 
            // subMenuDeudas
            // 
            this.subMenuDeudas.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuDeudas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.subMenuDeudas.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuDeudas.IconColor = System.Drawing.Color.Black;
            this.subMenuDeudas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuDeudas.Name = "subMenuDeudas";
            this.subMenuDeudas.Size = new System.Drawing.Size(253, 26);
            this.subMenuDeudas.Text = "Deudas de Locales";
            this.subMenuDeudas.Click += new System.EventHandler(this.subMenuDeudas_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.ForestGreen;
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(253, 26);
            this.toolStripMenuItem1.Text = "Productos Serializados";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.Color.ForestGreen;
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(253, 26);
            this.toolStripMenuItem2.Text = "S/N Vendidos";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // menuServicioTecnico
            // 
            this.menuServicioTecnico.AutoSize = false;
            this.menuServicioTecnico.BackColor = System.Drawing.Color.ForestGreen;
            this.menuServicioTecnico.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuIngresoService,
            this.subMenuCobroService});
            this.menuServicioTecnico.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuServicioTecnico.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuServicioTecnico.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.menuServicioTecnico.IconColor = System.Drawing.Color.Black;
            this.menuServicioTecnico.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuServicioTecnico.IconSize = 50;
            this.menuServicioTecnico.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuServicioTecnico.Name = "menuServicioTecnico";
            this.menuServicioTecnico.Padding = new System.Windows.Forms.Padding(0);
            this.menuServicioTecnico.Size = new System.Drawing.Size(130, 75);
            this.menuServicioTecnico.Text = "Servicio Tecnico";
            this.menuServicioTecnico.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuIngresoService
            // 
            this.subMenuIngresoService.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuIngresoService.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.subMenuIngresoService.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuIngresoService.IconColor = System.Drawing.Color.Black;
            this.subMenuIngresoService.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuIngresoService.Name = "subMenuIngresoService";
            this.subMenuIngresoService.Size = new System.Drawing.Size(197, 26);
            this.subMenuIngresoService.Text = "Servicio";
            this.subMenuIngresoService.Click += new System.EventHandler(this.subMenuIngresoService_Click);
            // 
            // subMenuCobroService
            // 
            this.subMenuCobroService.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuCobroService.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.subMenuCobroService.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuCobroService.IconColor = System.Drawing.Color.Black;
            this.subMenuCobroService.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuCobroService.Name = "subMenuCobroService";
            this.subMenuCobroService.Size = new System.Drawing.Size(197, 26);
            this.subMenuCobroService.Text = "Cobrar Servicio";
            this.subMenuCobroService.Click += new System.EventHandler(this.subMenuCobroService_Click);
            // 
            // menuTitulo
            // 
            this.menuTitulo.AutoSize = false;
            this.menuTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.menuTitulo.Location = new System.Drawing.Point(0, 0);
            this.menuTitulo.Name = "menuTitulo";
            this.menuTitulo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuTitulo.Size = new System.Drawing.Size(1366, 163);
            this.menuTitulo.TabIndex = 1;
            this.menuTitulo.Text = "menuStrip2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.ForestGreen;
            this.label1.Location = new System.Drawing.Point(145, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gestion de Negocio";
            // 
            // contenedor
            // 
            this.contenedor.BackColor = System.Drawing.Color.White;
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Location = new System.Drawing.Point(0, 242);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(1366, 503);
            this.contenedor.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.ForestGreen;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Bienvenido";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.BackColor = System.Drawing.Color.ForestGreen;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(95, 9);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(143, 17);
            this.lblUsuario.TabIndex = 13;
            this.lblUsuario.Text = "JULIETA CASTAGNANI";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.ForestGreen;
            this.panel1.Controls.Add(this.lblCambioSucursal);
            this.panel1.Controls.Add(this.cboSucursal);
            this.panel1.Controls.Add(this.iconPictureBox2);
            this.panel1.Controls.Add(this.iconPictureBox1);
            this.panel1.Controls.Add(this.lblSucursal);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblRol);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblDocumento);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblUsuario);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1366, 38);
            this.panel1.TabIndex = 15;
            // 
            // lblCambioSucursal
            // 
            this.lblCambioSucursal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCambioSucursal.AutoSize = true;
            this.lblCambioSucursal.BackColor = System.Drawing.Color.ForestGreen;
            this.lblCambioSucursal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCambioSucursal.ForeColor = System.Drawing.Color.White;
            this.lblCambioSucursal.Location = new System.Drawing.Point(829, 9);
            this.lblCambioSucursal.Name = "lblCambioSucursal";
            this.lblCambioSucursal.Size = new System.Drawing.Size(118, 17);
            this.lblCambioSucursal.TabIndex = 28;
            this.lblCambioSucursal.Text = "Cambiar Sucursal:";
            this.lblCambioSucursal.Visible = false;
            // 
            // cboSucursal
            // 
            this.cboSucursal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSucursal.BackColor = System.Drawing.Color.Black;
            this.cboSucursal.ForeColor = System.Drawing.Color.White;
            this.cboSucursal.FormattingEnabled = true;
            this.cboSucursal.Location = new System.Drawing.Point(953, 9);
            this.cboSucursal.Name = "cboSucursal";
            this.cboSucursal.Size = new System.Drawing.Size(178, 21);
            this.cboSucursal.TabIndex = 4;
            this.cboSucursal.Visible = false;
            this.cboSucursal.SelectedIndexChanged += new System.EventHandler(this.cboSucursal_SelectedIndexChanged);
            // 
            // iconPictureBox2
            // 
            this.iconPictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconPictureBox2.BackColor = System.Drawing.Color.ForestGreen;
            this.iconPictureBox2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.AnglesDown;
            this.iconPictureBox2.IconColor = System.Drawing.Color.WhiteSmoke;
            this.iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox2.IconSize = 19;
            this.iconPictureBox2.Location = new System.Drawing.Point(1319, 4);
            this.iconPictureBox2.Name = "iconPictureBox2";
            this.iconPictureBox2.Size = new System.Drawing.Size(19, 22);
            this.iconPictureBox2.TabIndex = 19;
            this.iconPictureBox2.TabStop = false;
            this.iconPictureBox2.Click += new System.EventHandler(this.iconPictureBox2_Click_1);
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconPictureBox1.BackColor = System.Drawing.Color.ForestGreen;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.X;
            this.iconPictureBox1.IconColor = System.Drawing.Color.White;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 19;
            this.iconPictureBox1.Location = new System.Drawing.Point(1344, 4);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(19, 22);
            this.iconPictureBox1.TabIndex = 27;
            this.iconPictureBox1.TabStop = false;
            this.iconPictureBox1.Click += new System.EventHandler(this.iconPictureBox1_Click_1);
            // 
            // lblSucursal
            // 
            this.lblSucursal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.BackColor = System.Drawing.Color.ForestGreen;
            this.lblSucursal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucursal.ForeColor = System.Drawing.Color.White;
            this.lblSucursal.Location = new System.Drawing.Point(1224, 9);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(45, 17);
            this.lblSucursal.TabIndex = 26;
            this.lblSucursal.Text = "label8";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.ForestGreen;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(1155, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 17);
            this.label9.TabIndex = 25;
            this.label9.Text = "Sucursal:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.ForestGreen;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(1137, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 17);
            this.label7.TabIndex = 24;
            this.label7.Text = "|";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.ForestGreen;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(492, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 17);
            this.label6.TabIndex = 23;
            this.label6.Text = "|";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.ForestGreen;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(319, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "|";
            // 
            // lblRol
            // 
            this.lblRol.AutoSize = true;
            this.lblRol.BackColor = System.Drawing.Color.ForestGreen;
            this.lblRol.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRol.ForeColor = System.Drawing.Color.White;
            this.lblRol.Location = new System.Drawing.Point(548, 9);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(44, 17);
            this.lblRol.TabIndex = 22;
            this.lblRol.Text = "lblRol";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.ForestGreen;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(510, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "Rol:";
            // 
            // lblDocumento
            // 
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.BackColor = System.Drawing.Color.ForestGreen;
            this.lblDocumento.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumento.ForeColor = System.Drawing.Color.White;
            this.lblDocumento.Location = new System.Drawing.Point(373, 9);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(96, 17);
            this.lblDocumento.TabIndex = 21;
            this.lblDocumento.Text = "lblDocumento";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.ForestGreen;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(331, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "DNI:";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(214)))), ((int)(((byte)(243)))));
            this.panel2.Location = new System.Drawing.Point(0, 240);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1366, 2);
            this.panel2.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(214)))), ((int)(((byte)(243)))));
            this.panel3.Location = new System.Drawing.Point(0, 163);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1366, 2);
            this.panel3.TabIndex = 17;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(139, 116);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnCerrarSesion.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.btnCerrarSesion.IconColor = System.Drawing.Color.White;
            this.btnCerrarSesion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCerrarSesion.IconSize = 28;
            this.btnCerrarSesion.Location = new System.Drawing.Point(1203, 63);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(135, 30);
            this.btnCerrarSesion.TabIndex = 18;
            this.btnCerrarSesion.Text = "Cerrar Sesion";
            this.btnCerrarSesion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // subMenuMoneda
            // 
            this.subMenuMoneda.BackColor = System.Drawing.Color.ForestGreen;
            this.subMenuMoneda.ForeColor = System.Drawing.Color.White;
            this.subMenuMoneda.Name = "subMenuMoneda";
            this.subMenuMoneda.Size = new System.Drawing.Size(207, 26);
            this.subMenuMoneda.Text = "Monedas";
            this.subMenuMoneda.Click += new System.EventHandler(this.subMenuMoneda_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 745);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.menuTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private FontAwesome.Sharp.IconMenuItem menuUsuarios;
        private FontAwesome.Sharp.IconMenuItem menuMantenedor;
        private FontAwesome.Sharp.IconMenuItem menuVentas;
        private FontAwesome.Sharp.IconMenuItem menuCompras;
        private FontAwesome.Sharp.IconMenuItem menuClientes;
        private FontAwesome.Sharp.IconMenuItem menuProveedores;
        private FontAwesome.Sharp.IconMenuItem menuReportes;
        private FontAwesome.Sharp.IconMenuItem menuCajaRegistradora;
        private System.Windows.Forms.MenuStrip menuTitulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel contenedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUsuario;
        private FontAwesome.Sharp.IconMenuItem subMenuCategoria;
        private FontAwesome.Sharp.IconMenuItem subMenuProducto;
        private FontAwesome.Sharp.IconMenuItem subMenuRegistrarVenta;
        private FontAwesome.Sharp.IconMenuItem subMenuDetalleVenta;
        private FontAwesome.Sharp.IconMenuItem subMenuRegistrarCompra;
        private FontAwesome.Sharp.IconMenuItem subMenuDetalleCompra;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripMenuItem subMenuNegocio;
        private System.Windows.Forms.ToolStripMenuItem subMenuReporteCompras;
        private System.Windows.Forms.ToolStripMenuItem subMenuReporteVentas;
        private FontAwesome.Sharp.IconButton btnCerrarSesion;
        private System.Windows.Forms.ToolStripMenuItem subMenuAperturaCaja;
        private System.Windows.Forms.ToolStripMenuItem subMenuCajaDiaria;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem subMenuConsultaCaja;
        private System.Windows.Forms.ToolStripMenuItem cotizacionDolarToolStripMenuItem;
        private FontAwesome.Sharp.IconMenuItem menuRMA;
        private FontAwesome.Sharp.IconMenuItem menuConsultas;
        private FontAwesome.Sharp.IconMenuItem menuConsultaStock;
        private System.Windows.Forms.ToolStripMenuItem subMenuFormaPago;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.Label label9;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private System.Windows.Forms.ToolStripMenuItem subMenuConceptos;
        private FontAwesome.Sharp.IconMenuItem menuServicioTecnico;
        private FontAwesome.Sharp.IconMenuItem subMenuIngresoService;
        private FontAwesome.Sharp.IconMenuItem subMenuCobroService;
        private System.Windows.Forms.ToolStripMenuItem subMenuVendedores;
        private System.Windows.Forms.ComboBox cboSucursal;
        private System.Windows.Forms.Label lblCambioSucursal;
        private FontAwesome.Sharp.IconMenuItem menuStock;
        private System.Windows.Forms.ToolStripMenuItem subMenuStock;
        private System.Windows.Forms.ToolStripMenuItem subMenuTraspasoStock;
        private FontAwesome.Sharp.IconMenuItem subMenuDeudas;
        private FontAwesome.Sharp.IconMenuItem menuSerializacion;
        private System.Windows.Forms.ToolStripMenuItem subMenuSerializarProducto;
        private System.Windows.Forms.ToolStripMenuItem subMenuProductosSerializados;
        private System.Windows.Forms.ToolStripMenuItem subMenuSerialVendidos;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem subMenuClientes;
        private System.Windows.Forms.ToolStripMenuItem subMenuPagosParciales;
        private System.Windows.Forms.ToolStripMenuItem subMenuReporteCantidadVentasPorLocal;
        private System.Windows.Forms.ToolStripMenuItem subMenuMoneda;
    }
}

