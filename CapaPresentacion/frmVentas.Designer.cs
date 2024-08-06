
namespace CapaPresentacion
{
    partial class frmVentas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVentas));
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnRegistrarCompra = new FontAwesome.Sharp.IconButton();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.idProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.txtIdProducto = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtCambioCliente = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnAgregarProducto = new FontAwesome.Sharp.IconButton();
            this.txtCantidad = new System.Windows.Forms.NumericUpDown();
            this.gbRegistrarCompra = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboTipoDocumento = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.gbInfoProveedor = new System.Windows.Forms.GroupBox();
            this.txtIdCliente = new System.Windows.Forms.TextBox();
            this.btnBuscarCliente = new FontAwesome.Sharp.IconButton();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.txtDocumentoCliente = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.txtProducto = new System.Windows.Forms.TextBox();
            this.btnBuscarProducto = new FontAwesome.Sharp.IconButton();
            this.txtCodigoProducto = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gbInfoProducto = new System.Windows.Forms.GroupBox();
            this.txtPagaCon = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTotalAPagar = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cboFormaPago = new System.Windows.Forms.ComboBox();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.txtMontoDescuento = new System.Windows.Forms.TextBox();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.lblMontoDescuento = new System.Windows.Forms.Label();
            this.lblPorcentaje = new System.Windows.Forms.Label();
            this.checkDescuento = new System.Windows.Forms.CheckBox();
            this.checkRecargo = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtCotizacion = new System.Windows.Forms.NumericUpDown();
            this.cboFormaPago2 = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPagaCon2 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cboFormaPago4 = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtPagaCon4 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cboFormaPago3 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtPagaCon3 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtRestaPagar = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).BeginInit();
            this.gbRegistrarCompra.SuspendLayout();
            this.gbInfoProveedor.SuspendLayout();
            this.gbInfoProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCotizacion)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.ForestGreen;
            this.label10.Location = new System.Drawing.Point(827, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 17);
            this.label10.TabIndex = 50;
            this.label10.Text = "Cantidad:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.ForestGreen;
            this.label9.Location = new System.Drawing.Point(680, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 17);
            this.label9.TabIndex = 67;
            this.label9.Text = "Stock:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.ForestGreen;
            this.label8.Location = new System.Drawing.Point(526, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 17);
            this.label8.TabIndex = 50;
            this.label8.Text = "Precio:";
            // 
            // btnRegistrarCompra
            // 
            this.btnRegistrarCompra.BackColor = System.Drawing.Color.Green;
            this.btnRegistrarCompra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarCompra.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarCompra.ForeColor = System.Drawing.Color.White;
            this.btnRegistrarCompra.IconChar = FontAwesome.Sharp.IconChar.MoneyCheckAlt;
            this.btnRegistrarCompra.IconColor = System.Drawing.Color.White;
            this.btnRegistrarCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRegistrarCompra.IconSize = 32;
            this.btnRegistrarCompra.Location = new System.Drawing.Point(844, 465);
            this.btnRegistrarCompra.Name = "btnRegistrarCompra";
            this.btnRegistrarCompra.Size = new System.Drawing.Size(111, 32);
            this.btnRegistrarCompra.TabIndex = 81;
            this.btnRegistrarCompra.Text = "Registrar";
            this.btnRegistrarCompra.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegistrarCompra.UseVisualStyleBackColor = false;
            this.btnRegistrarCompra.Click += new System.EventHandler(this.btnRegistrarCompra_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dgvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idProducto,
            this.producto,
            this.precio,
            this.cantidad,
            this.subTotal,
            this.btnEliminar});
            this.dgvData.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvData.GridColor = System.Drawing.Color.White;
            this.dgvData.Location = new System.Drawing.Point(26, 279);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvData.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(650, 105);
            this.dgvData.TabIndex = 77;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvData_CellPainting);
            // 
            // idProducto
            // 
            this.idProducto.HeaderText = "ID PRODUCTO";
            this.idProducto.Name = "idProducto";
            this.idProducto.ReadOnly = true;
            this.idProducto.Visible = false;
            // 
            // producto
            // 
            this.producto.HeaderText = "PRODUCTO";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            this.producto.Width = 160;
            // 
            // precio
            // 
            this.precio.HeaderText = "PRECIO";
            this.precio.Name = "precio";
            this.precio.ReadOnly = true;
            this.precio.Width = 180;
            // 
            // cantidad
            // 
            this.cantidad.HeaderText = "CANTIDAD";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            // 
            // subTotal
            // 
            this.subTotal.HeaderText = "SUB TOTAL";
            this.subTotal.Name = "subTotal";
            this.subTotal.ReadOnly = true;
            // 
            // btnEliminar
            // 
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminar.HeaderText = "";
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.ReadOnly = true;
            this.btnEliminar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnEliminar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnEliminar.Width = 25;
            // 
            // txtIdProducto
            // 
            this.txtIdProducto.Location = new System.Drawing.Point(126, 28);
            this.txtIdProducto.Name = "txtIdProducto";
            this.txtIdProducto.Size = new System.Drawing.Size(30, 29);
            this.txtIdProducto.TabIndex = 68;
            this.txtIdProducto.TextChanged += new System.EventHandler(this.txtIdProducto_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(967, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(403, 665);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 82;
            this.pictureBox1.TabStop = false;
            // 
            // txtCambioCliente
            // 
            this.txtCambioCliente.Location = new System.Drawing.Point(744, 446);
            this.txtCambioCliente.Name = "txtCambioCliente";
            this.txtCambioCliente.Size = new System.Drawing.Size(60, 20);
            this.txtCambioCliente.TabIndex = 80;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.ForestGreen;
            this.label13.Location = new System.Drawing.Point(679, 446);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 17);
            this.label13.TabIndex = 79;
            this.label13.Text = "Cambio:";
            // 
            // btnAgregarProducto
            // 
            this.btnAgregarProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnAgregarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarProducto.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarProducto.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnAgregarProducto.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAgregarProducto.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAgregarProducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAgregarProducto.IconSize = 32;
            this.btnAgregarProducto.Location = new System.Drawing.Point(742, 280);
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.Size = new System.Drawing.Size(172, 57);
            this.btnAgregarProducto.TabIndex = 78;
            this.btnAgregarProducto.Text = "Agregar Producto";
            this.btnAgregarProducto.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregarProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAgregarProducto.UseVisualStyleBackColor = false;
            this.btnAgregarProducto.Click += new System.EventHandler(this.btnAgregarProducto_Click);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(830, 71);
            this.txtCantidad.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(101, 29);
            this.txtCantidad.TabIndex = 29;
            this.txtCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // gbRegistrarCompra
            // 
            this.gbRegistrarCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.gbRegistrarCompra.Controls.Add(this.label2);
            this.gbRegistrarCompra.Controls.Add(this.cboTipoDocumento);
            this.gbRegistrarCompra.Controls.Add(this.label5);
            this.gbRegistrarCompra.Controls.Add(this.dtpFecha);
            this.gbRegistrarCompra.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbRegistrarCompra.ForeColor = System.Drawing.Color.ForestGreen;
            this.gbRegistrarCompra.Location = new System.Drawing.Point(20, 34);
            this.gbRegistrarCompra.Name = "gbRegistrarCompra";
            this.gbRegistrarCompra.Size = new System.Drawing.Size(523, 117);
            this.gbRegistrarCompra.TabIndex = 74;
            this.gbRegistrarCompra.TabStop = false;
            this.gbRegistrarCompra.Text = "Informacion Venta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.ForestGreen;
            this.label2.Location = new System.Drawing.Point(260, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 17);
            this.label2.TabIndex = 49;
            this.label2.Text = "Tipo de Documento:";
            // 
            // cboTipoDocumento
            // 
            this.cboTipoDocumento.FormattingEnabled = true;
            this.cboTipoDocumento.Location = new System.Drawing.Point(263, 78);
            this.cboTipoDocumento.Name = "cboTipoDocumento";
            this.cboTipoDocumento.Size = new System.Drawing.Size(246, 29);
            this.cboTipoDocumento.TabIndex = 48;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.ForestGreen;
            this.label5.Location = new System.Drawing.Point(6, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 17);
            this.label5.TabIndex = 47;
            this.label5.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFecha.Location = new System.Drawing.Point(9, 81);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(248, 25);
            this.dtpFecha.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(14, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 32);
            this.label1.TabIndex = 73;
            this.label1.Text = "REGISTRAR VENTA";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label11.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label11.Location = new System.Drawing.Point(-1, -1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(973, 667);
            this.label11.TabIndex = 72;
            // 
            // gbInfoProveedor
            // 
            this.gbInfoProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.gbInfoProveedor.Controls.Add(this.txtIdCliente);
            this.gbInfoProveedor.Controls.Add(this.btnBuscarCliente);
            this.gbInfoProveedor.Controls.Add(this.txtNombreCliente);
            this.gbInfoProveedor.Controls.Add(this.txtDocumentoCliente);
            this.gbInfoProveedor.Controls.Add(this.label3);
            this.gbInfoProveedor.Controls.Add(this.label4);
            this.gbInfoProveedor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbInfoProveedor.ForeColor = System.Drawing.Color.ForestGreen;
            this.gbInfoProveedor.Location = new System.Drawing.Point(549, 34);
            this.gbInfoProveedor.Name = "gbInfoProveedor";
            this.gbInfoProveedor.Size = new System.Drawing.Size(412, 117);
            this.gbInfoProveedor.TabIndex = 75;
            this.gbInfoProveedor.TabStop = false;
            this.gbInfoProveedor.Text = "Informacion Cliente";
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.Location = new System.Drawing.Point(376, 28);
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.Size = new System.Drawing.Size(30, 29);
            this.txtIdCliente.TabIndex = 51;
            this.txtIdCliente.Visible = false;
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.BackColor = System.Drawing.Color.White;
            this.btnBuscarCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarCliente.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBuscarCliente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnBuscarCliente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(216)))), ((int)(((byte)(212)))));
            this.btnBuscarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarCliente.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarCliente.ForeColor = System.Drawing.Color.White;
            this.btnBuscarCliente.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscarCliente.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.btnBuscarCliente.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscarCliente.IconSize = 28;
            this.btnBuscarCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarCliente.Location = new System.Drawing.Point(154, 77);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(35, 29);
            this.btnBuscarCliente.TabIndex = 61;
            this.btnBuscarCliente.UseVisualStyleBackColor = false;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.Location = new System.Drawing.Point(206, 78);
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.Size = new System.Drawing.Size(196, 29);
            this.txtNombreCliente.TabIndex = 51;
            // 
            // txtDocumentoCliente
            // 
            this.txtDocumentoCliente.Location = new System.Drawing.Point(9, 77);
            this.txtDocumentoCliente.Name = "txtDocumentoCliente";
            this.txtDocumentoCliente.Size = new System.Drawing.Size(139, 29);
            this.txtDocumentoCliente.TabIndex = 50;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.ForestGreen;
            this.label3.Location = new System.Drawing.Point(203, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 17);
            this.label3.TabIndex = 49;
            this.label3.Text = "Nombre Cliente:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.ForestGreen;
            this.label4.Location = new System.Drawing.Point(6, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 17);
            this.label4.TabIndex = 47;
            this.label4.Text = "Nro. Documento:";
            // 
            // txtStock
            // 
            this.txtStock.Enabled = false;
            this.txtStock.Location = new System.Drawing.Point(678, 71);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(139, 29);
            this.txtStock.TabIndex = 66;
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(529, 71);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(139, 29);
            this.txtPrecio.TabIndex = 65;
            this.txtPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecio_KeyPress);
            // 
            // txtProducto
            // 
            this.txtProducto.Location = new System.Drawing.Point(216, 71);
            this.txtProducto.Name = "txtProducto";
            this.txtProducto.Size = new System.Drawing.Size(293, 29);
            this.txtProducto.TabIndex = 64;
            // 
            // btnBuscarProducto
            // 
            this.btnBuscarProducto.BackColor = System.Drawing.Color.White;
            this.btnBuscarProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarProducto.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBuscarProducto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnBuscarProducto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(216)))), ((int)(((byte)(212)))));
            this.btnBuscarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarProducto.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarProducto.ForeColor = System.Drawing.Color.White;
            this.btnBuscarProducto.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscarProducto.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.btnBuscarProducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscarProducto.IconSize = 28;
            this.btnBuscarProducto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarProducto.Location = new System.Drawing.Point(162, 70);
            this.btnBuscarProducto.Name = "btnBuscarProducto";
            this.btnBuscarProducto.Size = new System.Drawing.Size(35, 28);
            this.btnBuscarProducto.TabIndex = 63;
            this.btnBuscarProducto.UseVisualStyleBackColor = false;
            this.btnBuscarProducto.Click += new System.EventHandler(this.btnBuscarProducto_Click);
            // 
            // txtCodigoProducto
            // 
            this.txtCodigoProducto.Location = new System.Drawing.Point(6, 70);
            this.txtCodigoProducto.Name = "txtCodigoProducto";
            this.txtCodigoProducto.Size = new System.Drawing.Size(150, 29);
            this.txtCodigoProducto.TabIndex = 62;
            this.txtCodigoProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoProducto_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.ForestGreen;
            this.label6.Location = new System.Drawing.Point(213, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 49;
            this.label6.Text = "Producto:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.ForestGreen;
            this.label7.Location = new System.Drawing.Point(6, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 17);
            this.label7.TabIndex = 47;
            this.label7.Text = "Codigo Producto:";
            // 
            // gbInfoProducto
            // 
            this.gbInfoProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.gbInfoProducto.Controls.Add(this.txtIdProducto);
            this.gbInfoProducto.Controls.Add(this.label10);
            this.gbInfoProducto.Controls.Add(this.label9);
            this.gbInfoProducto.Controls.Add(this.label8);
            this.gbInfoProducto.Controls.Add(this.txtCantidad);
            this.gbInfoProducto.Controls.Add(this.txtStock);
            this.gbInfoProducto.Controls.Add(this.txtPrecio);
            this.gbInfoProducto.Controls.Add(this.txtProducto);
            this.gbInfoProducto.Controls.Add(this.btnBuscarProducto);
            this.gbInfoProducto.Controls.Add(this.txtCodigoProducto);
            this.gbInfoProducto.Controls.Add(this.label6);
            this.gbInfoProducto.Controls.Add(this.label7);
            this.gbInfoProducto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbInfoProducto.ForeColor = System.Drawing.Color.ForestGreen;
            this.gbInfoProducto.Location = new System.Drawing.Point(20, 157);
            this.gbInfoProducto.Name = "gbInfoProducto";
            this.gbInfoProducto.Size = new System.Drawing.Size(941, 117);
            this.gbInfoProducto.TabIndex = 76;
            this.gbInfoProducto.TabStop = false;
            this.gbInfoProducto.Text = "Informacion Producto";
            // 
            // txtPagaCon
            // 
            this.txtPagaCon.Location = new System.Drawing.Point(355, 390);
            this.txtPagaCon.Name = "txtPagaCon";
            this.txtPagaCon.Size = new System.Drawing.Size(77, 20);
            this.txtPagaCon.TabIndex = 84;
            this.txtPagaCon.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPagaCon_KeyDown);
            this.txtPagaCon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPagaCon_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.ForestGreen;
            this.label12.Location = new System.Drawing.Point(270, 390);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 17);
            this.label12.TabIndex = 83;
            this.label12.Text = "Monto FP1:";
            // 
            // txtTotalAPagar
            // 
            this.txtTotalAPagar.Location = new System.Drawing.Point(538, 476);
            this.txtTotalAPagar.Name = "txtTotalAPagar";
            this.txtTotalAPagar.Size = new System.Drawing.Size(96, 20);
            this.txtTotalAPagar.TabIndex = 86;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.ForestGreen;
            this.label14.Location = new System.Drawing.Point(438, 476);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 17);
            this.label14.TabIndex = 85;
            this.label14.Text = "Total a Pagar:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.ForestGreen;
            this.label15.Location = new System.Drawing.Point(12, 390);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(112, 17);
            this.label15.TabIndex = 87;
            this.label15.Text = "Forma de Pago 1";
            // 
            // cboFormaPago
            // 
            this.cboFormaPago.FormattingEnabled = true;
            this.cboFormaPago.Location = new System.Drawing.Point(130, 390);
            this.cboFormaPago.Name = "cboFormaPago";
            this.cboFormaPago.Size = new System.Drawing.Size(134, 21);
            this.cboFormaPago.TabIndex = 88;
            // 
            // txtDescuento
            // 
            this.txtDescuento.Location = new System.Drawing.Point(584, 414);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Size = new System.Drawing.Size(56, 20);
            this.txtDescuento.TabIndex = 89;
            this.txtDescuento.Text = "0";
            this.txtDescuento.Visible = false;
            this.txtDescuento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescuento_KeyDown);
            // 
            // txtMontoDescuento
            // 
            this.txtMontoDescuento.Location = new System.Drawing.Point(734, 414);
            this.txtMontoDescuento.Name = "txtMontoDescuento";
            this.txtMontoDescuento.Size = new System.Drawing.Size(81, 20);
            this.txtMontoDescuento.TabIndex = 90;
            this.txtMontoDescuento.Text = "0";
            this.txtMontoDescuento.Visible = false;
            this.txtMontoDescuento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMontoDescuento_KeyDown);
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblDescuento.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescuento.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblDescuento.Location = new System.Drawing.Point(438, 417);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(140, 17);
            this.lblDescuento.TabIndex = 91;
            this.lblDescuento.Text = "Descuento / Recargo:";
            this.lblDescuento.Visible = false;
            // 
            // lblMontoDescuento
            // 
            this.lblMontoDescuento.AutoSize = true;
            this.lblMontoDescuento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblMontoDescuento.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoDescuento.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblMontoDescuento.Location = new System.Drawing.Point(671, 414);
            this.lblMontoDescuento.Name = "lblMontoDescuento";
            this.lblMontoDescuento.Size = new System.Drawing.Size(57, 17);
            this.lblMontoDescuento.TabIndex = 92;
            this.lblMontoDescuento.Text = "Monto :";
            this.lblMontoDescuento.Visible = false;
            // 
            // lblPorcentaje
            // 
            this.lblPorcentaje.AutoSize = true;
            this.lblPorcentaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblPorcentaje.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPorcentaje.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblPorcentaje.Location = new System.Drawing.Point(646, 414);
            this.lblPorcentaje.Name = "lblPorcentaje";
            this.lblPorcentaje.Size = new System.Drawing.Size(19, 17);
            this.lblPorcentaje.TabIndex = 93;
            this.lblPorcentaje.Text = "%";
            this.lblPorcentaje.Visible = false;
            // 
            // checkDescuento
            // 
            this.checkDescuento.AutoSize = true;
            this.checkDescuento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.checkDescuento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkDescuento.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkDescuento.ForeColor = System.Drawing.Color.ForestGreen;
            this.checkDescuento.Location = new System.Drawing.Point(452, 387);
            this.checkDescuento.Name = "checkDescuento";
            this.checkDescuento.Size = new System.Drawing.Size(136, 21);
            this.checkDescuento.TabIndex = 94;
            this.checkDescuento.Text = "Aplicar Descuento";
            this.checkDescuento.UseVisualStyleBackColor = false;
            this.checkDescuento.CheckedChanged += new System.EventHandler(this.checkDescuento_CheckedChanged);
            // 
            // checkRecargo
            // 
            this.checkRecargo.AutoSize = true;
            this.checkRecargo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.checkRecargo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkRecargo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkRecargo.ForeColor = System.Drawing.Color.ForestGreen;
            this.checkRecargo.Location = new System.Drawing.Point(594, 386);
            this.checkRecargo.Name = "checkRecargo";
            this.checkRecargo.Size = new System.Drawing.Size(120, 21);
            this.checkRecargo.TabIndex = 95;
            this.checkRecargo.Text = "Aplicar Recargo";
            this.checkRecargo.UseVisualStyleBackColor = false;
            this.checkRecargo.CheckedChanged += new System.EventHandler(this.checkRecargo_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.ForestGreen;
            this.label16.Location = new System.Drawing.Point(438, 446);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(103, 17);
            this.label16.TabIndex = 96;
            this.label16.Text = "Cotizacion U$S:";
            // 
            // txtCotizacion
            // 
            this.txtCotizacion.DecimalPlaces = 2;
            this.txtCotizacion.Location = new System.Drawing.Point(558, 443);
            this.txtCotizacion.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtCotizacion.Name = "txtCotizacion";
            this.txtCotizacion.ReadOnly = true;
            this.txtCotizacion.Size = new System.Drawing.Size(101, 20);
            this.txtCotizacion.TabIndex = 97;
            this.txtCotizacion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtCotizacion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCotizacion_KeyDown);
            // 
            // cboFormaPago2
            // 
            this.cboFormaPago2.FormattingEnabled = true;
            this.cboFormaPago2.Location = new System.Drawing.Point(130, 420);
            this.cboFormaPago2.Name = "cboFormaPago2";
            this.cboFormaPago2.Size = new System.Drawing.Size(134, 21);
            this.cboFormaPago2.TabIndex = 101;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.ForestGreen;
            this.label17.Location = new System.Drawing.Point(12, 420);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(112, 17);
            this.label17.TabIndex = 100;
            this.label17.Text = "Forma de Pago 2";
            // 
            // txtPagaCon2
            // 
            this.txtPagaCon2.Location = new System.Drawing.Point(355, 420);
            this.txtPagaCon2.Name = "txtPagaCon2";
            this.txtPagaCon2.Size = new System.Drawing.Size(77, 20);
            this.txtPagaCon2.TabIndex = 99;
            this.txtPagaCon2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPagaCon2_KeyDown);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.ForestGreen;
            this.label18.Location = new System.Drawing.Point(270, 420);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 17);
            this.label18.TabIndex = 98;
            this.label18.Text = "Monto FP2:";
            // 
            // cboFormaPago4
            // 
            this.cboFormaPago4.FormattingEnabled = true;
            this.cboFormaPago4.Location = new System.Drawing.Point(130, 476);
            this.cboFormaPago4.Name = "cboFormaPago4";
            this.cboFormaPago4.Size = new System.Drawing.Size(134, 21);
            this.cboFormaPago4.TabIndex = 109;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.ForestGreen;
            this.label19.Location = new System.Drawing.Point(12, 476);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(112, 17);
            this.label19.TabIndex = 108;
            this.label19.Text = "Forma de Pago 4";
            // 
            // txtPagaCon4
            // 
            this.txtPagaCon4.Location = new System.Drawing.Point(355, 476);
            this.txtPagaCon4.Name = "txtPagaCon4";
            this.txtPagaCon4.Size = new System.Drawing.Size(77, 20);
            this.txtPagaCon4.TabIndex = 107;
            this.txtPagaCon4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPagaCon4_KeyDown);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.ForestGreen;
            this.label20.Location = new System.Drawing.Point(270, 476);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(79, 17);
            this.label20.TabIndex = 106;
            this.label20.Text = "Monto FP4:";
            // 
            // cboFormaPago3
            // 
            this.cboFormaPago3.FormattingEnabled = true;
            this.cboFormaPago3.Location = new System.Drawing.Point(130, 446);
            this.cboFormaPago3.Name = "cboFormaPago3";
            this.cboFormaPago3.Size = new System.Drawing.Size(134, 21);
            this.cboFormaPago3.TabIndex = 105;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label21.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.ForestGreen;
            this.label21.Location = new System.Drawing.Point(12, 446);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(112, 17);
            this.label21.TabIndex = 104;
            this.label21.Text = "Forma de Pago 3";
            // 
            // txtPagaCon3
            // 
            this.txtPagaCon3.Location = new System.Drawing.Point(355, 446);
            this.txtPagaCon3.Name = "txtPagaCon3";
            this.txtPagaCon3.Size = new System.Drawing.Size(77, 20);
            this.txtPagaCon3.TabIndex = 103;
            this.txtPagaCon3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPagaCon3_KeyDown);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.ForestGreen;
            this.label22.Location = new System.Drawing.Point(270, 446);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(79, 17);
            this.label22.TabIndex = 102;
            this.label22.Text = "Monto FP3:";
            // 
            // txtRestaPagar
            // 
            this.txtRestaPagar.Location = new System.Drawing.Point(731, 476);
            this.txtRestaPagar.Name = "txtRestaPagar";
            this.txtRestaPagar.Size = new System.Drawing.Size(96, 20);
            this.txtRestaPagar.TabIndex = 111;
            this.txtRestaPagar.TextChanged += new System.EventHandler(this.txtRestaPagar_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label23.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.ForestGreen;
            this.label23.Location = new System.Drawing.Point(640, 476);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(85, 17);
            this.label23.TabIndex = 110;
            this.label23.Text = "Resta Pagar:";
            // 
            // frmVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 665);
            this.Controls.Add(this.txtRestaPagar);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.cboFormaPago4);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtPagaCon4);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cboFormaPago3);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtPagaCon3);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.cboFormaPago2);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtPagaCon2);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtCotizacion);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.checkRecargo);
            this.Controls.Add(this.checkDescuento);
            this.Controls.Add(this.lblPorcentaje);
            this.Controls.Add(this.lblMontoDescuento);
            this.Controls.Add(this.lblDescuento);
            this.Controls.Add(this.txtMontoDescuento);
            this.Controls.Add(this.txtDescuento);
            this.Controls.Add(this.cboFormaPago);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtTotalAPagar);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtPagaCon);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnRegistrarCompra);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtCambioCliente);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnAgregarProducto);
            this.Controls.Add(this.gbRegistrarCompra);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbInfoProveedor);
            this.Controls.Add(this.gbInfoProducto);
            this.Controls.Add(this.label11);
            this.Name = "frmVentas";
            this.Text = "frmVentas";
            this.Load += new System.EventHandler(this.frmVentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).EndInit();
            this.gbRegistrarCompra.ResumeLayout(false);
            this.gbRegistrarCompra.PerformLayout();
            this.gbInfoProveedor.ResumeLayout(false);
            this.gbInfoProveedor.PerformLayout();
            this.gbInfoProducto.ResumeLayout(false);
            this.gbInfoProducto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCotizacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private FontAwesome.Sharp.IconButton btnRegistrarCompra;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox txtIdProducto;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtCambioCliente;
        private System.Windows.Forms.Label label13;
        private FontAwesome.Sharp.IconButton btnAgregarProducto;
        private System.Windows.Forms.NumericUpDown txtCantidad;
        private System.Windows.Forms.GroupBox gbRegistrarCompra;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboTipoDocumento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox gbInfoProveedor;
        private System.Windows.Forms.TextBox txtIdCliente;
        private FontAwesome.Sharp.IconButton btnBuscarCliente;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.TextBox txtDocumentoCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.TextBox txtProducto;
        private FontAwesome.Sharp.IconButton btnBuscarProducto;
        private System.Windows.Forms.TextBox txtCodigoProducto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox gbInfoProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn subTotal;
        private System.Windows.Forms.DataGridViewButtonColumn btnEliminar;
        private System.Windows.Forms.TextBox txtPagaCon;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTotalAPagar;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cboFormaPago;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.TextBox txtMontoDescuento;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.Label lblMontoDescuento;
        private System.Windows.Forms.Label lblPorcentaje;
        private System.Windows.Forms.CheckBox checkDescuento;
        private System.Windows.Forms.CheckBox checkRecargo;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown txtCotizacion;
        private System.Windows.Forms.ComboBox cboFormaPago2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtPagaCon2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cboFormaPago4;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtPagaCon4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cboFormaPago3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtPagaCon3;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtRestaPagar;
        private System.Windows.Forms.Label label23;
    }
}