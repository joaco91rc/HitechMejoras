
namespace CapaPresentacion
{
    partial class frmDeuda
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
            this.label11 = new System.Windows.Forms.Label();
            this.txtDeudaH1ARS = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeudaH2ARS = new System.Windows.Forms.TextBox();
            this.txtDeudaASARS = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeudaACARS = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDeudaACUSD = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDeudaASUSD = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDeudaH2USD = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDeudaH1USD = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDeuda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreSucursalOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreSucursalDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.costo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.simboloMoneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdTraspasoMercaderia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idSucursalOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idSucursalDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancelarDeuda = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnRechazarRecepcion = new System.Windows.Forms.DataGridViewImageColumn();
            this.lblInfoDeuda = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label11.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label11.Location = new System.Drawing.Point(12, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(228, 32);
            this.label11.TabIndex = 108;
            this.label11.Text = "DEUDA DEL LOCAL";
            // 
            // txtDeudaH1ARS
            // 
            this.txtDeudaH1ARS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDeudaH1ARS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtDeudaH1ARS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeudaH1ARS.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeudaH1ARS.ForeColor = System.Drawing.Color.Red;
            this.txtDeudaH1ARS.Location = new System.Drawing.Point(173, 700);
            this.txtDeudaH1ARS.Name = "txtDeudaH1ARS";
            this.txtDeudaH1ARS.Size = new System.Drawing.Size(148, 18);
            this.txtDeudaH1ARS.TabIndex = 107;
            this.txtDeudaH1ARS.Text = "Ninguno";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(15, 699);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(146, 17);
            this.label13.TabIndex = 106;
            this.label13.Text = "Deuda a Hitech 1 ARS:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1350, 729);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 104;
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
            this.label10.TabIndex = 103;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
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
            this.fecha,
            this.idDeuda,
            this.nombreProducto,
            this.nombreSucursalOrigen,
            this.nombreSucursalDestino,
            this.costo,
            this.simboloMoneda,
            this.IdTraspasoMercaderia,
            this.idSucursalOrigen,
            this.idSucursalDestino,
            this.estado,
            this.btnCancelarDeuda,
            this.btnRechazarRecepcion});
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
            this.dgvData.Location = new System.Drawing.Point(18, 66);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvData.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.Size = new System.Drawing.Size(1308, 496);
            this.dgvData.TabIndex = 105;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(344, 701);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 17);
            this.label1.TabIndex = 109;
            this.label1.Text = "Deuda a Hitech 2 ARS:";
            // 
            // txtDeudaH2ARS
            // 
            this.txtDeudaH2ARS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDeudaH2ARS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtDeudaH2ARS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeudaH2ARS.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeudaH2ARS.ForeColor = System.Drawing.Color.Red;
            this.txtDeudaH2ARS.Location = new System.Drawing.Point(498, 701);
            this.txtDeudaH2ARS.Name = "txtDeudaH2ARS";
            this.txtDeudaH2ARS.Size = new System.Drawing.Size(148, 18);
            this.txtDeudaH2ARS.TabIndex = 110;
            this.txtDeudaH2ARS.Text = "Ninguno";
            // 
            // txtDeudaASARS
            // 
            this.txtDeudaASARS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDeudaASARS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtDeudaASARS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeudaASARS.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeudaASARS.ForeColor = System.Drawing.Color.Red;
            this.txtDeudaASARS.Location = new System.Drawing.Point(827, 702);
            this.txtDeudaASARS.Name = "txtDeudaASARS";
            this.txtDeudaASARS.Size = new System.Drawing.Size(148, 18);
            this.txtDeudaASARS.TabIndex = 112;
            this.txtDeudaASARS.Text = "Ninguno";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(670, 701);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 17);
            this.label2.TabIndex = 111;
            this.label2.Text = "Deuda a Apple 49 ARS:";
            // 
            // txtDeudaACARS
            // 
            this.txtDeudaACARS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDeudaACARS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtDeudaACARS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeudaACARS.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeudaACARS.ForeColor = System.Drawing.Color.Red;
            this.txtDeudaACARS.Location = new System.Drawing.Point(1154, 699);
            this.txtDeudaACARS.Name = "txtDeudaACARS";
            this.txtDeudaACARS.Size = new System.Drawing.Size(148, 18);
            this.txtDeudaACARS.TabIndex = 114;
            this.txtDeudaACARS.Text = "Ninguno";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(984, 700);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 17);
            this.label3.TabIndex = 113;
            this.label3.Text = "Deuda a Apple Cafe ARS:";
            // 
            // txtDeudaACUSD
            // 
            this.txtDeudaACUSD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDeudaACUSD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtDeudaACUSD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeudaACUSD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeudaACUSD.ForeColor = System.Drawing.Color.Red;
            this.txtDeudaACUSD.Location = new System.Drawing.Point(1154, 668);
            this.txtDeudaACUSD.Name = "txtDeudaACUSD";
            this.txtDeudaACUSD.Size = new System.Drawing.Size(148, 18);
            this.txtDeudaACUSD.TabIndex = 122;
            this.txtDeudaACUSD.Text = "Ninguno";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(984, 669);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 17);
            this.label4.TabIndex = 121;
            this.label4.Text = "Deuda a Apple Cafe USD:";
            // 
            // txtDeudaASUSD
            // 
            this.txtDeudaASUSD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDeudaASUSD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtDeudaASUSD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeudaASUSD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeudaASUSD.ForeColor = System.Drawing.Color.Red;
            this.txtDeudaASUSD.Location = new System.Drawing.Point(827, 668);
            this.txtDeudaASUSD.Name = "txtDeudaASUSD";
            this.txtDeudaASUSD.Size = new System.Drawing.Size(148, 18);
            this.txtDeudaASUSD.TabIndex = 120;
            this.txtDeudaASUSD.Text = "Ninguno";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(670, 670);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 17);
            this.label5.TabIndex = 119;
            this.label5.Text = "Deuda a Apple 49 USD:";
            // 
            // txtDeudaH2USD
            // 
            this.txtDeudaH2USD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDeudaH2USD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtDeudaH2USD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeudaH2USD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeudaH2USD.ForeColor = System.Drawing.Color.Red;
            this.txtDeudaH2USD.Location = new System.Drawing.Point(498, 669);
            this.txtDeudaH2USD.Name = "txtDeudaH2USD";
            this.txtDeudaH2USD.Size = new System.Drawing.Size(148, 18);
            this.txtDeudaH2USD.TabIndex = 118;
            this.txtDeudaH2USD.Text = "Ninguno";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(344, 669);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 17);
            this.label6.TabIndex = 117;
            this.label6.Text = "Deuda a Hitech 2 USD:";
            // 
            // txtDeudaH1USD
            // 
            this.txtDeudaH1USD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDeudaH1USD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtDeudaH1USD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeudaH1USD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeudaH1USD.ForeColor = System.Drawing.Color.Red;
            this.txtDeudaH1USD.Location = new System.Drawing.Point(173, 668);
            this.txtDeudaH1USD.Name = "txtDeudaH1USD";
            this.txtDeudaH1USD.Size = new System.Drawing.Size(148, 18);
            this.txtDeudaH1USD.TabIndex = 116;
            this.txtDeudaH1USD.Text = "Ninguno";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(15, 668);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 17);
            this.label7.TabIndex = 115;
            this.label7.Text = "Deuda a Hitech 1 USD:";
            // 
            // fecha
            // 
            this.fecha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.fecha.HeaderText = "FECHA CREACION";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            this.fecha.Width = 120;
            // 
            // idDeuda
            // 
            this.idDeuda.HeaderText = "ID DEUDA";
            this.idDeuda.Name = "idDeuda";
            this.idDeuda.ReadOnly = true;
            this.idDeuda.Visible = false;
            this.idDeuda.Width = 170;
            // 
            // nombreProducto
            // 
            this.nombreProducto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nombreProducto.HeaderText = "PRODUCTO";
            this.nombreProducto.MinimumWidth = 200;
            this.nombreProducto.Name = "nombreProducto";
            this.nombreProducto.ReadOnly = true;
            // 
            // nombreSucursalOrigen
            // 
            this.nombreSucursalOrigen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nombreSucursalOrigen.HeaderText = "DEUDA A SUCURSAL";
            this.nombreSucursalOrigen.Name = "nombreSucursalOrigen";
            this.nombreSucursalOrigen.ReadOnly = true;
            this.nombreSucursalOrigen.Width = 150;
            // 
            // nombreSucursalDestino
            // 
            this.nombreSucursalDestino.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nombreSucursalDestino.HeaderText = "SUCURSAL DEUDORA";
            this.nombreSucursalDestino.Name = "nombreSucursalDestino";
            this.nombreSucursalDestino.ReadOnly = true;
            // 
            // costo
            // 
            this.costo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.costo.HeaderText = "DEUDA";
            this.costo.Name = "costo";
            this.costo.ReadOnly = true;
            this.costo.Width = 120;
            // 
            // simboloMoneda
            // 
            this.simboloMoneda.HeaderText = "MONEDA";
            this.simboloMoneda.Name = "simboloMoneda";
            this.simboloMoneda.ReadOnly = true;
            // 
            // IdTraspasoMercaderia
            // 
            this.IdTraspasoMercaderia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IdTraspasoMercaderia.HeaderText = "ID ORDEN TRASPASO";
            this.IdTraspasoMercaderia.Name = "IdTraspasoMercaderia";
            this.IdTraspasoMercaderia.ReadOnly = true;
            this.IdTraspasoMercaderia.Visible = false;
            this.IdTraspasoMercaderia.Width = 140;
            // 
            // idSucursalOrigen
            // 
            this.idSucursalOrigen.HeaderText = "ID SUCURSAL ORIGEN";
            this.idSucursalOrigen.Name = "idSucursalOrigen";
            this.idSucursalOrigen.ReadOnly = true;
            this.idSucursalOrigen.Width = 140;
            // 
            // idSucursalDestino
            // 
            this.idSucursalDestino.HeaderText = "ID SUCURSAL DESTINO";
            this.idSucursalDestino.Name = "idSucursalDestino";
            this.idSucursalDestino.ReadOnly = true;
            this.idSucursalDestino.Width = 150;
            // 
            // estado
            // 
            this.estado.HeaderText = "ESTADO";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            this.estado.Width = 140;
            // 
            // btnCancelarDeuda
            // 
            this.btnCancelarDeuda.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.btnCancelarDeuda.HeaderText = "";
            this.btnCancelarDeuda.Image = global::CapaPresentacion.Properties.Resources.CHECK;
            this.btnCancelarDeuda.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.btnCancelarDeuda.Name = "btnCancelarDeuda";
            this.btnCancelarDeuda.ReadOnly = true;
            this.btnCancelarDeuda.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnCancelarDeuda.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnCancelarDeuda.Width = 30;
            // 
            // btnRechazarRecepcion
            // 
            this.btnRechazarRecepcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.btnRechazarRecepcion.HeaderText = "";
            this.btnRechazarRecepcion.MinimumWidth = 30;
            this.btnRechazarRecepcion.Name = "btnRechazarRecepcion";
            this.btnRechazarRecepcion.ReadOnly = true;
            this.btnRechazarRecepcion.Width = 30;
            // 
            // lblInfoDeuda
            // 
            this.lblInfoDeuda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInfoDeuda.AutoSize = true;
            this.lblInfoDeuda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblInfoDeuda.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoDeuda.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblInfoDeuda.Location = new System.Drawing.Point(18, 607);
            this.lblInfoDeuda.Name = "lblInfoDeuda";
            this.lblInfoDeuda.Size = new System.Drawing.Size(222, 25);
            this.lblInfoDeuda.TabIndex = 123;
            this.lblInfoDeuda.Text = "Deuda Local  X a locales";
            // 
            // frmDeuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.lblInfoDeuda);
            this.Controls.Add(this.txtDeudaACUSD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDeudaASUSD);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDeudaH2USD);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDeudaH1USD);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDeudaACARS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDeudaASARS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDeudaH2ARS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtDeudaH1ARS);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label10);
            this.Name = "frmDeuda";
            this.Text = "frmDeuda";
            this.Load += new System.EventHandler(this.frmDeuda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDeudaH1ARS;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDeudaH2ARS;
        private System.Windows.Forms.TextBox txtDeudaASARS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDeudaACARS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDeudaACUSD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDeudaASUSD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDeudaH2USD;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDeudaH1USD;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDeuda;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreSucursalOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreSucursalDestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn costo;
        private System.Windows.Forms.DataGridViewTextBoxColumn simboloMoneda;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdTraspasoMercaderia;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSucursalOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSucursalDestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DataGridViewImageColumn btnCancelarDeuda;
        private System.Windows.Forms.DataGridViewImageColumn btnRechazarRecepcion;
        private System.Windows.Forms.Label lblInfoDeuda;
    }
}