
namespace CapaPresentacion
{
    partial class frmPagoParcial
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
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.cboEstado = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.txtSeñaSeleccionada = new System.Windows.Forms.TextBox();
            this.txtIndice = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.cboBusqueda = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtIdCliente = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnEliminar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiarDatos = new FontAwesome.Sharp.IconButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnGuardar = new FontAwesome.Sharp.IconButton();
            this.label13 = new System.Windows.Forms.Label();
            this.txtIdPagoParcial = new System.Windows.Forms.TextBox();
            this.btnBuscarCliente = new FontAwesome.Sharp.IconButton();
            this.cboFormaPago = new System.Windows.Forms.ComboBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.NumericUpDown();
            this.txtProductoReservado = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboVendedores = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkMostrarTodosPagosParciales = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.checkPesos = new System.Windows.Forms.CheckBox();
            this.checkDolares = new System.Windows.Forms.CheckBox();
            this.btnSeleccionar = new System.Windows.Forms.DataGridViewImageColumn();
            this.idPagoParcial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreCompleto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productoReservado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formaPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numeroVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estadoValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreLocal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idNegocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.label10.Location = new System.Drawing.Point(435, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(915, 729);
            this.label10.TabIndex = 82;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label9.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label9.Location = new System.Drawing.Point(12, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(291, 32);
            this.label9.TabIndex = 80;
            this.label9.Text = "DETALLE PAGO PARCIAL";
            // 
            // txtCliente
            // 
            this.txtCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCliente.Location = new System.Drawing.Point(172, 173);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(178, 20);
            this.txtCliente.TabIndex = 76;
            // 
            // cboEstado
            // 
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Location = new System.Drawing.Point(172, 336);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(178, 21);
            this.cboEstado.TabIndex = 75;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.ForestGreen;
            this.label8.Location = new System.Drawing.Point(15, 336);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 17);
            this.label8.TabIndex = 74;
            this.label8.Text = "Estado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.ForestGreen;
            this.label3.Location = new System.Drawing.Point(15, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 71;
            this.label3.Text = "Monto Seña";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.ForestGreen;
            this.label2.Location = new System.Drawing.Point(12, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 70;
            this.label2.Text = "Cliente";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(435, 729);
            this.label1.TabIndex = 69;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnSeleccionar,
            this.idPagoParcial,
            this.fecha,
            this.idCliente,
            this.nombreCompleto,
            this.productoReservado,
            this.formaPago,
            this.monto,
            this.moneda,
            this.idVenta,
            this.numeroVenta,
            this.vendedor,
            this.estadoValor,
            this.estado,
            this.nombreLocal,
            this.idNegocio});
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
            this.dgvData.Location = new System.Drawing.Point(441, 152);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvData.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.Size = new System.Drawing.Size(897, 497);
            this.dgvData.TabIndex = 86;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            // 
            // txtSeñaSeleccionada
            // 
            this.txtSeñaSeleccionada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSeñaSeleccionada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtSeñaSeleccionada.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSeñaSeleccionada.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSeñaSeleccionada.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(212)))), ((int)(((byte)(216)))));
            this.txtSeñaSeleccionada.Location = new System.Drawing.Point(161, 700);
            this.txtSeñaSeleccionada.Name = "txtSeñaSeleccionada";
            this.txtSeñaSeleccionada.Size = new System.Drawing.Size(206, 18);
            this.txtSeñaSeleccionada.TabIndex = 96;
            this.txtSeñaSeleccionada.Text = "Ninguno";
            // 
            // txtIndice
            // 
            this.txtIndice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtIndice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIndice.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIndice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(212)))), ((int)(((byte)(216)))));
            this.txtIndice.Location = new System.Drawing.Point(313, 37);
            this.txtIndice.Name = "txtIndice";
            this.txtIndice.Size = new System.Drawing.Size(36, 18);
            this.txtIndice.TabIndex = 94;
            this.txtIndice.Text = "-1";
            this.txtIndice.Visible = false;
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
            this.btnLimpiar.Location = new System.Drawing.Point(1052, 109);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(35, 26);
            this.btnLimpiar.TabIndex = 93;
            this.btnLimpiar.UseVisualStyleBackColor = false;
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
            this.btnBuscar.Location = new System.Drawing.Point(1011, 109);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(35, 26);
            this.btnBuscar.TabIndex = 92;
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBusqueda.Location = new System.Drawing.Point(698, 115);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(307, 20);
            this.txtBusqueda.TabIndex = 91;
            // 
            // cboBusqueda
            // 
            this.cboBusqueda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBusqueda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboBusqueda.FormattingEnabled = true;
            this.cboBusqueda.Location = new System.Drawing.Point(535, 114);
            this.cboBusqueda.Name = "cboBusqueda";
            this.cboBusqueda.Size = new System.Drawing.Size(157, 21);
            this.cboBusqueda.TabIndex = 90;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.ForestGreen;
            this.label12.Location = new System.Drawing.Point(451, 113);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 17);
            this.label12.TabIndex = 89;
            this.label12.Text = "Buscar por:";
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtIdCliente.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIdCliente.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(212)))), ((int)(((byte)(216)))));
            this.txtIdCliente.Location = new System.Drawing.Point(355, 36);
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.Size = new System.Drawing.Size(36, 18);
            this.txtIdCliente.TabIndex = 88;
            this.txtIdCliente.Text = "0";
            this.txtIdCliente.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label11.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label11.Location = new System.Drawing.Point(513, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(490, 32);
            this.label11.TabIndex = 87;
            this.label11.Text = "LISTA DE PAGOS PARCIALES POR CLIENTE";
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
            this.btnEliminar.Location = new System.Drawing.Point(278, 433);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(141, 31);
            this.btnEliminar.TabIndex = 85;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnLimpiarDatos
            // 
            this.btnLimpiarDatos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.btnLimpiarDatos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiarDatos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(216)))), ((int)(((byte)(212)))));
            this.btnLimpiarDatos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnLimpiarDatos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(216)))), ((int)(((byte)(212)))));
            this.btnLimpiarDatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarDatos.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarDatos.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarDatos.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnLimpiarDatos.IconColor = System.Drawing.Color.White;
            this.btnLimpiarDatos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiarDatos.IconSize = 28;
            this.btnLimpiarDatos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiarDatos.Location = new System.Drawing.Point(158, 433);
            this.btnLimpiarDatos.Name = "btnLimpiarDatos";
            this.btnLimpiarDatos.Size = new System.Drawing.Size(114, 31);
            this.btnLimpiarDatos.TabIndex = 84;
            this.btnLimpiarDatos.Text = "Limpiar";
            this.btnLimpiarDatos.UseVisualStyleBackColor = false;
            this.btnLimpiarDatos.Click += new System.EventHandler(this.btnLimpiarDatos_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(435, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(915, 729);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 83;
            this.pictureBox1.TabStop = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(216)))), ((int)(((byte)(212)))));
            this.btnGuardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(216)))), ((int)(((byte)(212)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.IconChar = FontAwesome.Sharp.IconChar.FileShield;
            this.btnGuardar.IconColor = System.Drawing.Color.White;
            this.btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGuardar.IconSize = 28;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(12, 433);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(140, 31);
            this.btnGuardar.TabIndex = 81;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(212)))), ((int)(((byte)(216)))));
            this.label13.Location = new System.Drawing.Point(12, 700);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(139, 17);
            this.label13.TabIndex = 95;
            this.label13.Text = "Cliente Seleccionado:";
            // 
            // txtIdPagoParcial
            // 
            this.txtIdPagoParcial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtIdPagoParcial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIdPagoParcial.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdPagoParcial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(212)))), ((int)(((byte)(216)))));
            this.txtIdPagoParcial.Location = new System.Drawing.Point(397, 38);
            this.txtIdPagoParcial.Name = "txtIdPagoParcial";
            this.txtIdPagoParcial.Size = new System.Drawing.Size(36, 18);
            this.txtIdPagoParcial.TabIndex = 97;
            this.txtIdPagoParcial.Text = "0";
            this.txtIdPagoParcial.Visible = false;
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
            this.btnBuscarCliente.Location = new System.Drawing.Point(365, 166);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(35, 29);
            this.btnBuscarCliente.TabIndex = 98;
            this.btnBuscarCliente.UseVisualStyleBackColor = false;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // cboFormaPago
            // 
            this.cboFormaPago.FormattingEnabled = true;
            this.cboFormaPago.Location = new System.Drawing.Point(172, 216);
            this.cboFormaPago.Name = "cboFormaPago";
            this.cboFormaPago.Size = new System.Drawing.Size(174, 21);
            this.cboFormaPago.TabIndex = 99;
            this.cboFormaPago.SelectedIndexChanged += new System.EventHandler(this.cboFormaPago_SelectedIndexChanged);
            // 
            // dtpFecha
            // 
            this.dtpFecha.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFecha.Location = new System.Drawing.Point(172, 123);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(244, 25);
            this.dtpFecha.TabIndex = 100;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.ForestGreen;
            this.label4.Location = new System.Drawing.Point(15, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 17);
            this.label4.TabIndex = 101;
            this.label4.Text = "Fecha";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.ForestGreen;
            this.label5.Location = new System.Drawing.Point(15, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 17);
            this.label5.TabIndex = 102;
            this.label5.Text = "Forma de Pago";
            // 
            // txtMonto
            // 
            this.txtMonto.DecimalPlaces = 2;
            this.txtMonto.Location = new System.Drawing.Point(172, 288);
            this.txtMonto.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.txtMonto.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(178, 20);
            this.txtMonto.TabIndex = 119;
            this.txtMonto.ThousandsSeparator = true;
            // 
            // txtProductoReservado
            // 
            this.txtProductoReservado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProductoReservado.Location = new System.Drawing.Point(172, 383);
            this.txtProductoReservado.Name = "txtProductoReservado";
            this.txtProductoReservado.Size = new System.Drawing.Size(244, 20);
            this.txtProductoReservado.TabIndex = 121;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.ForestGreen;
            this.label6.Location = new System.Drawing.Point(15, 386);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 17);
            this.label6.TabIndex = 120;
            this.label6.Text = "Producto Señado";
            // 
            // cboVendedores
            // 
            this.cboVendedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVendedores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboVendedores.FormattingEnabled = true;
            this.cboVendedores.Location = new System.Drawing.Point(172, 94);
            this.cboVendedores.Name = "cboVendedores";
            this.cboVendedores.Size = new System.Drawing.Size(178, 21);
            this.cboVendedores.TabIndex = 123;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.ForestGreen;
            this.label7.Location = new System.Drawing.Point(15, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 17);
            this.label7.TabIndex = 122;
            this.label7.Text = "Vendedor";
            // 
            // checkMostrarTodosPagosParciales
            // 
            this.checkMostrarTodosPagosParciales.AutoSize = true;
            this.checkMostrarTodosPagosParciales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.checkMostrarTodosPagosParciales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkMostrarTodosPagosParciales.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkMostrarTodosPagosParciales.ForeColor = System.Drawing.Color.ForestGreen;
            this.checkMostrarTodosPagosParciales.Location = new System.Drawing.Point(1107, 115);
            this.checkMostrarTodosPagosParciales.Name = "checkMostrarTodosPagosParciales";
            this.checkMostrarTodosPagosParciales.Size = new System.Drawing.Size(237, 21);
            this.checkMostrarTodosPagosParciales.TabIndex = 125;
            this.checkMostrarTodosPagosParciales.Text = "Mostrar Todos Los Pagos Parciales";
            this.checkMostrarTodosPagosParciales.UseVisualStyleBackColor = false;
            this.checkMostrarTodosPagosParciales.Visible = false;
            this.checkMostrarTodosPagosParciales.CheckedChanged += new System.EventHandler(this.checkMostrarTodosPagosParciales_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.ForestGreen;
            this.label14.Location = new System.Drawing.Point(15, 252);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 17);
            this.label14.TabIndex = 127;
            this.label14.Text = "Moneda";
            // 
            // checkPesos
            // 
            this.checkPesos.AutoSize = true;
            this.checkPesos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.checkPesos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkPesos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkPesos.ForeColor = System.Drawing.Color.ForestGreen;
            this.checkPesos.Location = new System.Drawing.Point(110, 252);
            this.checkPesos.Name = "checkPesos";
            this.checkPesos.Size = new System.Drawing.Size(59, 21);
            this.checkPesos.TabIndex = 128;
            this.checkPesos.Text = "Pesos";
            this.checkPesos.UseVisualStyleBackColor = false;
            this.checkPesos.CheckedChanged += new System.EventHandler(this.checkPesos_CheckedChanged);
            // 
            // checkDolares
            // 
            this.checkDolares.AutoSize = true;
            this.checkDolares.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.checkDolares.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkDolares.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkDolares.ForeColor = System.Drawing.Color.ForestGreen;
            this.checkDolares.Location = new System.Drawing.Point(190, 252);
            this.checkDolares.Name = "checkDolares";
            this.checkDolares.Size = new System.Drawing.Size(71, 21);
            this.checkDolares.TabIndex = 129;
            this.checkDolares.Text = "Dolares";
            this.checkDolares.UseVisualStyleBackColor = false;
            this.checkDolares.CheckedChanged += new System.EventHandler(this.checkDolares_CheckedChanged);
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.HeaderText = "";
            this.btnSeleccionar.Image = global::CapaPresentacion.Properties.Resources.CHECK;
            this.btnSeleccionar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.ReadOnly = true;
            this.btnSeleccionar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnSeleccionar.Width = 30;
            // 
            // idPagoParcial
            // 
            this.idPagoParcial.HeaderText = "ID PAGO PARCIAL";
            this.idPagoParcial.Name = "idPagoParcial";
            this.idPagoParcial.ReadOnly = true;
            this.idPagoParcial.Visible = false;
            this.idPagoParcial.Width = 160;
            // 
            // fecha
            // 
            this.fecha.HeaderText = "FECHA";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            this.fecha.Width = 120;
            // 
            // idCliente
            // 
            this.idCliente.HeaderText = "ID CLIENTE";
            this.idCliente.Name = "idCliente";
            this.idCliente.ReadOnly = true;
            this.idCliente.Visible = false;
            // 
            // nombreCompleto
            // 
            this.nombreCompleto.HeaderText = "CLIENTE";
            this.nombreCompleto.Name = "nombreCompleto";
            this.nombreCompleto.ReadOnly = true;
            this.nombreCompleto.Width = 180;
            // 
            // productoReservado
            // 
            this.productoReservado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.productoReservado.HeaderText = "PRODUCTO RESERVADO";
            this.productoReservado.MinimumWidth = 250;
            this.productoReservado.Name = "productoReservado";
            this.productoReservado.ReadOnly = true;
            // 
            // formaPago
            // 
            this.formaPago.HeaderText = "FORMA PAGO";
            this.formaPago.Name = "formaPago";
            this.formaPago.ReadOnly = true;
            this.formaPago.Width = 180;
            // 
            // monto
            // 
            this.monto.HeaderText = "MONTO SEÑA";
            this.monto.Name = "monto";
            this.monto.ReadOnly = true;
            this.monto.Width = 180;
            // 
            // moneda
            // 
            this.moneda.HeaderText = "MONEDA";
            this.moneda.Name = "moneda";
            this.moneda.ReadOnly = true;
            // 
            // idVenta
            // 
            this.idVenta.HeaderText = "ID VENTA";
            this.idVenta.Name = "idVenta";
            this.idVenta.ReadOnly = true;
            this.idVenta.Visible = false;
            // 
            // numeroVenta
            // 
            this.numeroVenta.HeaderText = "VENTA";
            this.numeroVenta.Name = "numeroVenta";
            this.numeroVenta.ReadOnly = true;
            this.numeroVenta.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numeroVenta.Visible = false;
            // 
            // vendedor
            // 
            this.vendedor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.vendedor.HeaderText = "VENDEDOR";
            this.vendedor.MinimumWidth = 100;
            this.vendedor.Name = "vendedor";
            this.vendedor.ReadOnly = true;
            this.vendedor.Width = 200;
            // 
            // estadoValor
            // 
            this.estadoValor.HeaderText = "ESTADO VALOR";
            this.estadoValor.Name = "estadoValor";
            this.estadoValor.ReadOnly = true;
            this.estadoValor.Visible = false;
            // 
            // estado
            // 
            this.estado.HeaderText = "ESTADO";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // nombreLocal
            // 
            this.nombreLocal.HeaderText = "LOCAL";
            this.nombreLocal.Name = "nombreLocal";
            this.nombreLocal.ReadOnly = true;
            // 
            // idNegocio
            // 
            this.idNegocio.HeaderText = "ID NEGOCIO";
            this.idNegocio.Name = "idNegocio";
            this.idNegocio.ReadOnly = true;
            this.idNegocio.Visible = false;
            // 
            // frmPagoParcial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.checkDolares);
            this.Controls.Add(this.checkPesos);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.checkMostrarTodosPagosParciales);
            this.Controls.Add(this.cboVendedores);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtProductoReservado);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.cboFormaPago);
            this.Controls.Add(this.btnBuscarCliente);
            this.Controls.Add(this.txtIdPagoParcial);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.cboEstado);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.txtSeñaSeleccionada);
            this.Controls.Add(this.txtIndice);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBusqueda);
            this.Controls.Add(this.cboBusqueda);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtIdCliente);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnLimpiarDatos);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Name = "frmPagoParcial";
            this.Text = "frmPagoParcial";
            this.Load += new System.EventHandler(this.frmPagoParcial_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox txtSeñaSeleccionada;
        private System.Windows.Forms.TextBox txtIndice;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.ComboBox cboBusqueda;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtIdCliente;
        private System.Windows.Forms.Label label11;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private FontAwesome.Sharp.IconButton btnLimpiarDatos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtIdPagoParcial;
        private FontAwesome.Sharp.IconButton btnBuscarCliente;
        private System.Windows.Forms.ComboBox cboFormaPago;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown txtMonto;
        private System.Windows.Forms.TextBox txtProductoReservado;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboVendedores;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkMostrarTodosPagosParciales;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkPesos;
        private System.Windows.Forms.CheckBox checkDolares;
        private System.Windows.Forms.DataGridViewImageColumn btnSeleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPagoParcial;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreCompleto;
        private System.Windows.Forms.DataGridViewTextBoxColumn productoReservado;
        private System.Windows.Forms.DataGridViewTextBoxColumn formaPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn monto;
        private System.Windows.Forms.DataGridViewTextBoxColumn moneda;
        private System.Windows.Forms.DataGridViewTextBoxColumn idVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeroVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn estadoValor;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreLocal;
        private System.Windows.Forms.DataGridViewTextBoxColumn idNegocio;
    }
}