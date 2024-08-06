
namespace CapaPresentacion
{
    partial class frmDetalleVenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDetalleVenta));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDescargarPDF = new FontAwesome.Sharp.IconButton();
            this.txtTotalAPagar = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtnroDocumento = new System.Windows.Forms.TextBox();
            this.btnBuscarProveedor = new FontAwesome.Sharp.IconButton();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gbInfoProveedor = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboTipoDocumento = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.gbRegistrarCompra = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPagaCon = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCambio = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMontoDescuento = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtMontoFP4 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtMontoFP3 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtMontoFP2 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMontoFP1 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtFormaPago4 = new System.Windows.Forms.TextBox();
            this.txtFormaPago3 = new System.Windows.Forms.TextBox();
            this.txtFormaPago2 = new System.Windows.Forms.TextBox();
            this.txtFormaPago1 = new System.Windows.Forms.TextBox();
            this.btnEliminar = new FontAwesome.Sharp.IconButton();
            this.lblIdVenta = new System.Windows.Forms.Label();
            this.lblNumeroVenta = new System.Windows.Forms.Label();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.gbInfoProveedor.SuspendLayout();
            this.gbRegistrarCompra.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(996, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(354, 729);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 87;
            this.pictureBox1.TabStop = false;
            // 
            // btnDescargarPDF
            // 
            this.btnDescargarPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnDescargarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescargarPDF.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescargarPDF.ForeColor = System.Drawing.Color.White;
            this.btnDescargarPDF.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            this.btnDescargarPDF.IconColor = System.Drawing.Color.White;
            this.btnDescargarPDF.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDescargarPDF.IconSize = 32;
            this.btnDescargarPDF.Location = new System.Drawing.Point(754, 441);
            this.btnDescargarPDF.Name = "btnDescargarPDF";
            this.btnDescargarPDF.Size = new System.Drawing.Size(112, 40);
            this.btnDescargarPDF.TabIndex = 86;
            this.btnDescargarPDF.Text = "Descargar";
            this.btnDescargarPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnDescargarPDF.UseVisualStyleBackColor = false;
            this.btnDescargarPDF.Click += new System.EventHandler(this.btnDescargarPDF_Click);
            // 
            // txtTotalAPagar
            // 
            this.txtTotalAPagar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTotalAPagar.Enabled = false;
            this.txtTotalAPagar.Location = new System.Drawing.Point(613, 361);
            this.txtTotalAPagar.Name = "txtTotalAPagar";
            this.txtTotalAPagar.Size = new System.Drawing.Size(139, 20);
            this.txtTotalAPagar.TabIndex = 85;
            this.txtTotalAPagar.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.ForestGreen;
            this.label13.Location = new System.Drawing.Point(497, 361);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 17);
            this.label13.TabIndex = 84;
            this.label13.Text = "Monto Total:";
            // 
            // txtnroDocumento
            // 
            this.txtnroDocumento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtnroDocumento.Enabled = false;
            this.txtnroDocumento.Location = new System.Drawing.Point(411, 45);
            this.txtnroDocumento.Name = "txtnroDocumento";
            this.txtnroDocumento.Size = new System.Drawing.Size(60, 29);
            this.txtnroDocumento.TabIndex = 51;
            this.txtnroDocumento.Visible = false;
            // 
            // btnBuscarProveedor
            // 
            this.btnBuscarProveedor.BackColor = System.Drawing.Color.White;
            this.btnBuscarProveedor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarProveedor.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBuscarProveedor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnBuscarProveedor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(216)))), ((int)(((byte)(212)))));
            this.btnBuscarProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarProveedor.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarProveedor.ForeColor = System.Drawing.Color.White;
            this.btnBuscarProveedor.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscarProveedor.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.btnBuscarProveedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscarProveedor.IconSize = 28;
            this.btnBuscarProveedor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarProveedor.Location = new System.Drawing.Point(154, 45);
            this.btnBuscarProveedor.Name = "btnBuscarProveedor";
            this.btnBuscarProveedor.Size = new System.Drawing.Size(35, 29);
            this.btnBuscarProveedor.TabIndex = 61;
            this.btnBuscarProveedor.UseVisualStyleBackColor = false;
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreCliente.Enabled = false;
            this.txtNombreCliente.Location = new System.Drawing.Point(206, 46);
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.Size = new System.Drawing.Size(196, 29);
            this.txtNombreCliente.TabIndex = 51;
            // 
            // txtDNI
            // 
            this.txtDNI.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDNI.Enabled = false;
            this.txtDNI.Location = new System.Drawing.Point(9, 45);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(139, 29);
            this.txtDNI.TabIndex = 50;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.ForestGreen;
            this.label3.Location = new System.Drawing.Point(203, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 17);
            this.label3.TabIndex = 49;
            this.label3.Text = "Nombre Completo";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dgvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.producto,
            this.precioVenta,
            this.cantidad,
            this.subTotal});
            this.dgvData.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.GridColor = System.Drawing.Color.White;
            this.dgvData.Location = new System.Drawing.Point(18, 250);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(923, 108);
            this.dgvData.TabIndex = 83;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.ForestGreen;
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 17);
            this.label4.TabIndex = 47;
            this.label4.Text = "DNI:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label6.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label6.Location = new System.Drawing.Point(12, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(234, 32);
            this.label6.TabIndex = 78;
            this.label6.Text = "DETALLE DE VENTA";
            // 
            // gbInfoProveedor
            // 
            this.gbInfoProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.gbInfoProveedor.Controls.Add(this.txtnroDocumento);
            this.gbInfoProveedor.Controls.Add(this.btnBuscarProveedor);
            this.gbInfoProveedor.Controls.Add(this.txtNombreCliente);
            this.gbInfoProveedor.Controls.Add(this.txtDNI);
            this.gbInfoProveedor.Controls.Add(this.label3);
            this.gbInfoProveedor.Controls.Add(this.label4);
            this.gbInfoProveedor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbInfoProveedor.ForeColor = System.Drawing.Color.ForestGreen;
            this.gbInfoProveedor.Location = new System.Drawing.Point(18, 157);
            this.gbInfoProveedor.Name = "gbInfoProveedor";
            this.gbInfoProveedor.Size = new System.Drawing.Size(923, 80);
            this.gbInfoProveedor.TabIndex = 77;
            this.gbInfoProveedor.TabStop = false;
            this.gbInfoProveedor.Text = "Informacion Cliente";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.ForestGreen;
            this.label8.Location = new System.Drawing.Point(733, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 17);
            this.label8.TabIndex = 53;
            this.label8.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsuario.Enabled = false;
            this.txtUsuario.Location = new System.Drawing.Point(724, 47);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(196, 29);
            this.txtUsuario.TabIndex = 52;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.ForestGreen;
            this.label2.Location = new System.Drawing.Point(260, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 17);
            this.label2.TabIndex = 49;
            this.label2.Text = "Tipo de Documento:";
            // 
            // cboTipoDocumento
            // 
            this.cboTipoDocumento.Enabled = false;
            this.cboTipoDocumento.FormattingEnabled = true;
            this.cboTipoDocumento.Location = new System.Drawing.Point(263, 47);
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
            this.label5.Location = new System.Drawing.Point(6, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 17);
            this.label5.TabIndex = 47;
            this.label5.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Enabled = false;
            this.dtpFecha.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFecha.Location = new System.Drawing.Point(9, 47);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(248, 25);
            this.dtpFecha.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.ForestGreen;
            this.label7.Location = new System.Drawing.Point(366, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 17);
            this.label7.TabIndex = 79;
            this.label7.Text = "Numero Venta:";
            // 
            // gbRegistrarCompra
            // 
            this.gbRegistrarCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.gbRegistrarCompra.Controls.Add(this.label8);
            this.gbRegistrarCompra.Controls.Add(this.txtUsuario);
            this.gbRegistrarCompra.Controls.Add(this.label2);
            this.gbRegistrarCompra.Controls.Add(this.cboTipoDocumento);
            this.gbRegistrarCompra.Controls.Add(this.label5);
            this.gbRegistrarCompra.Controls.Add(this.dtpFecha);
            this.gbRegistrarCompra.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbRegistrarCompra.ForeColor = System.Drawing.Color.ForestGreen;
            this.gbRegistrarCompra.Location = new System.Drawing.Point(18, 66);
            this.gbRegistrarCompra.Name = "gbRegistrarCompra";
            this.gbRegistrarCompra.Size = new System.Drawing.Size(923, 83);
            this.gbRegistrarCompra.TabIndex = 76;
            this.gbRegistrarCompra.TabStop = false;
            this.gbRegistrarCompra.Text = "Informacion Venta";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label1.Location = new System.Drawing.Point(1, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(989, 730);
            this.label1.TabIndex = 75;
            // 
            // txtPagaCon
            // 
            this.txtPagaCon.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPagaCon.Enabled = false;
            this.txtPagaCon.Location = new System.Drawing.Point(133, 438);
            this.txtPagaCon.Name = "txtPagaCon";
            this.txtPagaCon.Size = new System.Drawing.Size(142, 20);
            this.txtPagaCon.TabIndex = 89;
            this.txtPagaCon.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.ForestGreen;
            this.label9.Location = new System.Drawing.Point(15, 441);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 17);
            this.label9.TabIndex = 88;
            this.label9.Text = "Total Pagado:";
            // 
            // txtCambio
            // 
            this.txtCambio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCambio.Enabled = false;
            this.txtCambio.Location = new System.Drawing.Point(369, 438);
            this.txtCambio.Name = "txtCambio";
            this.txtCambio.Size = new System.Drawing.Size(127, 20);
            this.txtCambio.TabIndex = 91;
            this.txtCambio.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.ForestGreen;
            this.label10.Location = new System.Drawing.Point(286, 441);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 17);
            this.label10.TabIndex = 90;
            this.label10.Text = "Cambio:";
            // 
            // txtDescuento
            // 
            this.txtDescuento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescuento.Enabled = false;
            this.txtDescuento.Location = new System.Drawing.Point(133, 361);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Size = new System.Drawing.Size(77, 20);
            this.txtDescuento.TabIndex = 93;
            this.txtDescuento.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.ForestGreen;
            this.label11.Location = new System.Drawing.Point(15, 361);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 17);
            this.label11.TabIndex = 92;
            this.label11.Text = "% Descuento";
            // 
            // txtMontoDescuento
            // 
            this.txtMontoDescuento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMontoDescuento.Enabled = false;
            this.txtMontoDescuento.Location = new System.Drawing.Point(369, 361);
            this.txtMontoDescuento.Name = "txtMontoDescuento";
            this.txtMontoDescuento.Size = new System.Drawing.Size(126, 20);
            this.txtMontoDescuento.TabIndex = 95;
            this.txtMontoDescuento.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.ForestGreen;
            this.label12.Location = new System.Drawing.Point(286, 364);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 17);
            this.label12.TabIndex = 94;
            this.label12.Text = "Descuento $";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.ForestGreen;
            this.label19.Location = new System.Drawing.Point(497, 414);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(112, 17);
            this.label19.TabIndex = 124;
            this.label19.Text = "Forma de Pago 4";
            // 
            // txtMontoFP4
            // 
            this.txtMontoFP4.Enabled = false;
            this.txtMontoFP4.Location = new System.Drawing.Point(849, 413);
            this.txtMontoFP4.Name = "txtMontoFP4";
            this.txtMontoFP4.Size = new System.Drawing.Size(92, 20);
            this.txtMontoFP4.TabIndex = 123;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.ForestGreen;
            this.label20.Location = new System.Drawing.Point(771, 413);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(79, 17);
            this.label20.TabIndex = 122;
            this.label20.Text = "Monto FP4:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label21.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.ForestGreen;
            this.label21.Location = new System.Drawing.Point(497, 387);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(112, 17);
            this.label21.TabIndex = 120;
            this.label21.Text = "Forma de Pago 3";
            // 
            // txtMontoFP3
            // 
            this.txtMontoFP3.Enabled = false;
            this.txtMontoFP3.Location = new System.Drawing.Point(849, 384);
            this.txtMontoFP3.Name = "txtMontoFP3";
            this.txtMontoFP3.Size = new System.Drawing.Size(92, 20);
            this.txtMontoFP3.TabIndex = 119;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.ForestGreen;
            this.label22.Location = new System.Drawing.Point(771, 384);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(79, 17);
            this.label22.TabIndex = 118;
            this.label22.Text = "Monto FP3:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.ForestGreen;
            this.label17.Location = new System.Drawing.Point(15, 410);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(112, 17);
            this.label17.TabIndex = 116;
            this.label17.Text = "Forma de Pago 2";
            // 
            // txtMontoFP2
            // 
            this.txtMontoFP2.Enabled = false;
            this.txtMontoFP2.Location = new System.Drawing.Point(369, 410);
            this.txtMontoFP2.Name = "txtMontoFP2";
            this.txtMontoFP2.Size = new System.Drawing.Size(127, 20);
            this.txtMontoFP2.TabIndex = 115;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.ForestGreen;
            this.label18.Location = new System.Drawing.Point(286, 413);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 17);
            this.label18.TabIndex = 114;
            this.label18.Text = "Monto FP2:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.ForestGreen;
            this.label15.Location = new System.Drawing.Point(15, 384);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(112, 17);
            this.label15.TabIndex = 112;
            this.label15.Text = "Forma de Pago 1";
            // 
            // txtMontoFP1
            // 
            this.txtMontoFP1.Enabled = false;
            this.txtMontoFP1.Location = new System.Drawing.Point(369, 384);
            this.txtMontoFP1.Name = "txtMontoFP1";
            this.txtMontoFP1.Size = new System.Drawing.Size(127, 20);
            this.txtMontoFP1.TabIndex = 111;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.ForestGreen;
            this.label16.Location = new System.Drawing.Point(286, 384);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 17);
            this.label16.TabIndex = 110;
            this.label16.Text = "Monto FP1:";
            // 
            // txtFormaPago4
            // 
            this.txtFormaPago4.Enabled = false;
            this.txtFormaPago4.Location = new System.Drawing.Point(613, 414);
            this.txtFormaPago4.Name = "txtFormaPago4";
            this.txtFormaPago4.Size = new System.Drawing.Size(142, 20);
            this.txtFormaPago4.TabIndex = 129;
            // 
            // txtFormaPago3
            // 
            this.txtFormaPago3.Enabled = false;
            this.txtFormaPago3.Location = new System.Drawing.Point(613, 384);
            this.txtFormaPago3.Name = "txtFormaPago3";
            this.txtFormaPago3.Size = new System.Drawing.Size(142, 20);
            this.txtFormaPago3.TabIndex = 128;
            // 
            // txtFormaPago2
            // 
            this.txtFormaPago2.Enabled = false;
            this.txtFormaPago2.Location = new System.Drawing.Point(133, 410);
            this.txtFormaPago2.Name = "txtFormaPago2";
            this.txtFormaPago2.Size = new System.Drawing.Size(142, 20);
            this.txtFormaPago2.TabIndex = 127;
            // 
            // txtFormaPago1
            // 
            this.txtFormaPago1.Enabled = false;
            this.txtFormaPago1.Location = new System.Drawing.Point(133, 384);
            this.txtFormaPago1.Name = "txtFormaPago1";
            this.txtFormaPago1.Size = new System.Drawing.Size(142, 20);
            this.txtFormaPago1.TabIndex = 126;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Firebrick;
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(216)))), ((int)(((byte)(212)))));
            this.btnEliminar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(216)))), ((int)(((byte)(212)))));
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            this.btnEliminar.IconColor = System.Drawing.Color.White;
            this.btnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEliminar.IconSize = 28;
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(613, 441);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(135, 40);
            this.btnEliminar.TabIndex = 130;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lblIdVenta
            // 
            this.lblIdVenta.AutoSize = true;
            this.lblIdVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblIdVenta.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdVenta.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblIdVenta.Location = new System.Drawing.Point(227, 50);
            this.lblIdVenta.Name = "lblIdVenta";
            this.lblIdVenta.Size = new System.Drawing.Size(54, 13);
            this.lblIdVenta.TabIndex = 131;
            this.lblIdVenta.Text = "nroVenta";
            this.lblIdVenta.Visible = false;
            // 
            // lblNumeroVenta
            // 
            this.lblNumeroVenta.AutoSize = true;
            this.lblNumeroVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblNumeroVenta.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroVenta.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblNumeroVenta.Location = new System.Drawing.Point(473, 21);
            this.lblNumeroVenta.Name = "lblNumeroVenta";
            this.lblNumeroVenta.Size = new System.Drawing.Size(101, 17);
            this.lblNumeroVenta.TabIndex = 132;
            this.lblNumeroVenta.Text = "Numero Venta:";
            // 
            // producto
            // 
            this.producto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.producto.HeaderText = "PRODUCTO";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            // 
            // precioVenta
            // 
            this.precioVenta.HeaderText = "PRECIO";
            this.precioVenta.Name = "precioVenta";
            this.precioVenta.ReadOnly = true;
            this.precioVenta.Width = 180;
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
            // frmDetalleVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.lblNumeroVenta);
            this.Controls.Add(this.lblIdVenta);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.txtFormaPago4);
            this.Controls.Add(this.txtFormaPago3);
            this.Controls.Add(this.txtFormaPago2);
            this.Controls.Add(this.txtFormaPago1);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtMontoFP4);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtMontoFP3);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtMontoFP2);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtMontoFP1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtMontoDescuento);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtDescuento);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtCambio);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtPagaCon);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnDescargarPDF);
            this.Controls.Add(this.txtTotalAPagar);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gbInfoProveedor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.gbRegistrarCompra);
            this.Controls.Add(this.label1);
            this.Name = "frmDetalleVenta";
            this.Text = "frmDetalleVenta";
            this.Load += new System.EventHandler(this.frmDetalleVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.gbInfoProveedor.ResumeLayout(false);
            this.gbInfoProveedor.PerformLayout();
            this.gbRegistrarCompra.ResumeLayout(false);
            this.gbRegistrarCompra.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private FontAwesome.Sharp.IconButton btnDescargarPDF;
        private System.Windows.Forms.TextBox txtTotalAPagar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtnroDocumento;
        private FontAwesome.Sharp.IconButton btnBuscarProveedor;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gbInfoProveedor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboTipoDocumento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox gbRegistrarCompra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPagaCon;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCambio;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMontoDescuento;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtMontoFP4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtMontoFP3;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtMontoFP2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtMontoFP1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtFormaPago4;
        private System.Windows.Forms.TextBox txtFormaPago3;
        private System.Windows.Forms.TextBox txtFormaPago2;
        private System.Windows.Forms.TextBox txtFormaPago1;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private System.Windows.Forms.Label lblIdVenta;
        private System.Windows.Forms.Label lblNumeroVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn subTotal;
    }
}