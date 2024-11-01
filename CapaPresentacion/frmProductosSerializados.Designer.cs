
namespace CapaPresentacion
{
    partial class frmProductosSerializados
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.cboBusqueda = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnExportarExcel = new FontAwesome.Sharp.IconButton();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.idProductoDetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.marca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreLocal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idNegocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEditar = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnEliminar = new System.Windows.Forms.DataGridViewImageColumn();
            this.checkProductosTodosLocales = new System.Windows.Forms.CheckBox();
            this.checkHitech1 = new System.Windows.Forms.CheckBox();
            this.checkHitech2 = new System.Windows.Forms.CheckBox();
            this.checkApple49 = new System.Windows.Forms.CheckBox();
            this.checkAppleCafe = new System.Windows.Forms.CheckBox();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.dtpfechaHasta = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.White;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnLimpiar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnLimpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(216)))), ((int)(((byte)(212)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnLimpiar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiar.IconSize = 28;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(620, 83);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(35, 26);
            this.btnLimpiar.TabIndex = 112;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.White;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBuscar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(216)))), ((int)(((byte)(212)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscar.IconSize = 28;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(579, 83);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(35, 26);
            this.btnBuscar.TabIndex = 111;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBusqueda.Location = new System.Drawing.Point(266, 89);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(307, 20);
            this.txtBusqueda.TabIndex = 110;
            this.txtBusqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBusqueda_KeyDown);
            // 
            // cboBusqueda
            // 
            this.cboBusqueda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBusqueda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboBusqueda.FormattingEnabled = true;
            this.cboBusqueda.Location = new System.Drawing.Point(103, 88);
            this.cboBusqueda.Name = "cboBusqueda";
            this.cboBusqueda.Size = new System.Drawing.Size(157, 21);
            this.cboBusqueda.TabIndex = 109;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.ForestGreen;
            this.label12.Location = new System.Drawing.Point(19, 87);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 17);
            this.label12.TabIndex = 108;
            this.label12.Text = "Buscar por:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label11.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label11.Location = new System.Drawing.Point(517, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(333, 32);
            this.label11.TabIndex = 107;
            this.label11.Text = "PRODUCTOS SERIALIZADOS";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1350, 729);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 105;
            this.pictureBox1.TabStop = false;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1350, 729);
            this.label10.TabIndex = 104;
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportarExcel.BackColor = System.Drawing.Color.ForestGreen;
            this.btnExportarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(216)))), ((int)(((byte)(212)))));
            this.btnExportarExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnExportarExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(216)))), ((int)(((byte)(212)))));
            this.btnExportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarExcel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportarExcel.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.btnExportarExcel.IconColor = System.Drawing.Color.White;
            this.btnExportarExcel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExportarExcel.IconSize = 28;
            this.btnExportarExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportarExcel.Location = new System.Drawing.Point(1198, 131);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(140, 31);
            this.btnExportarExcel.TabIndex = 114;
            this.btnExportarExcel.Text = "Exportar";
            this.btnExportarExcel.UseVisualStyleBackColor = false;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.idProductoDetalle,
            this.idProducto,
            this.fecha,
            this.codigo,
            this.nombre,
            this.marca,
            this.modelo,
            this.color,
            this.serialNumber,
            this.nombreLocal,
            this.estado,
            this.idNegocio,
            this.btnEditar,
            this.btnEliminar});
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
            this.dgvData.Location = new System.Drawing.Point(18, 168);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.Size = new System.Drawing.Size(1320, 529);
            this.dgvData.TabIndex = 106;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            // 
            // idProductoDetalle
            // 
            this.idProductoDetalle.HeaderText = "ID PRODUCTO DETALLE";
            this.idProductoDetalle.Name = "idProductoDetalle";
            this.idProductoDetalle.ReadOnly = true;
            this.idProductoDetalle.Visible = false;
            // 
            // idProducto
            // 
            this.idProducto.HeaderText = "ID PRODUCTO";
            this.idProducto.Name = "idProducto";
            this.idProducto.Visible = false;
            // 
            // fecha
            // 
            this.fecha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.fecha.HeaderText = "FECHA";
            this.fecha.Name = "fecha";
            this.fecha.Width = 120;
            // 
            // codigo
            // 
            this.codigo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.codigo.HeaderText = "CODIGO";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Width = 80;
            // 
            // nombre
            // 
            this.nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nombre.HeaderText = "NOMBRE";
            this.nombre.MinimumWidth = 250;
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.Width = 400;
            // 
            // marca
            // 
            this.marca.HeaderText = "MARCA";
            this.marca.Name = "marca";
            this.marca.Width = 200;
            // 
            // modelo
            // 
            this.modelo.HeaderText = "MODELO";
            this.modelo.Name = "modelo";
            this.modelo.Width = 200;
            // 
            // color
            // 
            this.color.HeaderText = "COLOR";
            this.color.Name = "color";
            this.color.Width = 150;
            // 
            // serialNumber
            // 
            this.serialNumber.HeaderText = "SERIAL NUMBER (S/N)";
            this.serialNumber.Name = "serialNumber";
            this.serialNumber.Width = 200;
            // 
            // nombreLocal
            // 
            this.nombreLocal.HeaderText = "LOCAL";
            this.nombreLocal.Name = "nombreLocal";
            // 
            // estado
            // 
            this.estado.HeaderText = "ESTADO";
            this.estado.Name = "estado";
            // 
            // idNegocio
            // 
            this.idNegocio.HeaderText = "ID NEGOCIO";
            this.idNegocio.Name = "idNegocio";
            this.idNegocio.Visible = false;
            // 
            // btnEditar
            // 
            this.btnEditar.HeaderText = "";
            this.btnEditar.Image = global::CapaPresentacion.Properties.Resources.CHECK;
            this.btnEditar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnEditar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnEditar.Width = 30;
            // 
            // btnEliminar
            // 
            this.btnEliminar.HeaderText = "";
            this.btnEliminar.Name = "btnEliminar";
            // 
            // checkProductosTodosLocales
            // 
            this.checkProductosTodosLocales.AutoSize = true;
            this.checkProductosTodosLocales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.checkProductosTodosLocales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkProductosTodosLocales.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkProductosTodosLocales.ForeColor = System.Drawing.Color.ForestGreen;
            this.checkProductosTodosLocales.Location = new System.Drawing.Point(22, 141);
            this.checkProductosTodosLocales.Name = "checkProductosTodosLocales";
            this.checkProductosTodosLocales.Size = new System.Drawing.Size(248, 21);
            this.checkProductosTodosLocales.TabIndex = 115;
            this.checkProductosTodosLocales.Text = "Mostrar Productos todos los Locales";
            this.checkProductosTodosLocales.UseVisualStyleBackColor = false;
            this.checkProductosTodosLocales.CheckedChanged += new System.EventHandler(this.checkProductosEnStock_CheckedChanged);
            // 
            // checkHitech1
            // 
            this.checkHitech1.AutoSize = true;
            this.checkHitech1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.checkHitech1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkHitech1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkHitech1.ForeColor = System.Drawing.Color.ForestGreen;
            this.checkHitech1.Location = new System.Drawing.Point(302, 141);
            this.checkHitech1.Name = "checkHitech1";
            this.checkHitech1.Size = new System.Drawing.Size(171, 21);
            this.checkHitech1.TabIndex = 116;
            this.checkHitech1.Text = "Serializados de Hitech 1";
            this.checkHitech1.UseVisualStyleBackColor = false;
            this.checkHitech1.CheckedChanged += new System.EventHandler(this.checkHitech1_CheckedChanged);
            // 
            // checkHitech2
            // 
            this.checkHitech2.AutoSize = true;
            this.checkHitech2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.checkHitech2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkHitech2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkHitech2.ForeColor = System.Drawing.Color.ForestGreen;
            this.checkHitech2.Location = new System.Drawing.Point(546, 141);
            this.checkHitech2.Name = "checkHitech2";
            this.checkHitech2.Size = new System.Drawing.Size(171, 21);
            this.checkHitech2.TabIndex = 117;
            this.checkHitech2.Text = "Serializados de Hitech 2";
            this.checkHitech2.UseVisualStyleBackColor = false;
            this.checkHitech2.CheckedChanged += new System.EventHandler(this.checkHitech2_CheckedChanged);
            // 
            // checkApple49
            // 
            this.checkApple49.AutoSize = true;
            this.checkApple49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.checkApple49.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkApple49.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkApple49.ForeColor = System.Drawing.Color.ForestGreen;
            this.checkApple49.Location = new System.Drawing.Point(766, 141);
            this.checkApple49.Name = "checkApple49";
            this.checkApple49.Size = new System.Drawing.Size(174, 21);
            this.checkApple49.TabIndex = 118;
            this.checkApple49.Text = "Serializados de Apple 49\r\n";
            this.checkApple49.UseVisualStyleBackColor = false;
            this.checkApple49.CheckedChanged += new System.EventHandler(this.checkApple49_CheckedChanged);
            // 
            // checkAppleCafe
            // 
            this.checkAppleCafe.AutoSize = true;
            this.checkAppleCafe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.checkAppleCafe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkAppleCafe.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkAppleCafe.ForeColor = System.Drawing.Color.ForestGreen;
            this.checkAppleCafe.Location = new System.Drawing.Point(974, 141);
            this.checkAppleCafe.Name = "checkAppleCafe";
            this.checkAppleCafe.Size = new System.Drawing.Size(187, 21);
            this.checkAppleCafe.TabIndex = 119;
            this.checkAppleCafe.Text = "Serializados de Apple Café";
            this.checkAppleCafe.UseVisualStyleBackColor = false;
            this.checkAppleCafe.CheckedChanged += new System.EventHandler(this.checkAppleCafe_CheckedChanged);
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaDesde.Location = new System.Drawing.Point(682, 84);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(248, 25);
            this.dtpFechaDesde.TabIndex = 120;
            // 
            // dtpfechaHasta
            // 
            this.dtpfechaHasta.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfechaHasta.Location = new System.Drawing.Point(936, 84);
            this.dtpfechaHasta.Name = "dtpfechaHasta";
            this.dtpfechaHasta.Size = new System.Drawing.Size(248, 25);
            this.dtpfechaHasta.TabIndex = 121;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.ForestGreen;
            this.label1.Location = new System.Drawing.Point(679, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 122;
            this.label1.Text = "Fecha Desde:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.ForestGreen;
            this.label2.Location = new System.Drawing.Point(933, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 17);
            this.label2.TabIndex = 123;
            this.label2.Text = "Fecha Hasta:";
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.White;
            this.iconButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconButton1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.iconButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.iconButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(216)))), ((int)(((byte)(212)))));
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton1.ForeColor = System.Drawing.Color.White;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.iconButton1.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.IconSize = 28;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.Location = new System.Drawing.Point(1198, 84);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Size = new System.Drawing.Size(35, 26);
            this.iconButton1.TabIndex = 124;
            this.iconButton1.UseVisualStyleBackColor = false;
            // 
            // frmProductosSerializados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.iconButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpfechaHasta);
            this.Controls.Add(this.dtpFechaDesde);
            this.Controls.Add(this.checkAppleCafe);
            this.Controls.Add(this.checkApple49);
            this.Controls.Add(this.checkHitech2);
            this.Controls.Add(this.checkHitech1);
            this.Controls.Add(this.checkProductosTodosLocales);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBusqueda);
            this.Controls.Add(this.cboBusqueda);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnExportarExcel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label10);
            this.Name = "frmProductosSerializados";
            this.Text = "frmProductosSerializados";
            this.Load += new System.EventHandler(this.frmProductosSerializados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnLimpiar;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.ComboBox cboBusqueda;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label10;
        private FontAwesome.Sharp.IconButton btnExportarExcel;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.CheckBox checkProductosTodosLocales;
        private System.Windows.Forms.CheckBox checkHitech1;
        private System.Windows.Forms.CheckBox checkHitech2;
        private System.Windows.Forms.CheckBox checkApple49;
        private System.Windows.Forms.CheckBox checkAppleCafe;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProductoDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn marca;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn color;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreLocal;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn idNegocio;
        private System.Windows.Forms.DataGridViewImageColumn btnEditar;
        private System.Windows.Forms.DataGridViewImageColumn btnEliminar;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.DateTimePicker dtpfechaHasta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton iconButton1;
    }
}