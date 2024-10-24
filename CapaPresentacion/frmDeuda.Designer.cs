
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDeudaH1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeudaH2 = new System.Windows.Forms.TextBox();
            this.txtDeudaAS = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDeuda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreSucursalOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreSucursalDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.costo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdTraspasoMercaderia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idSucursalOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idSucursalDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancelarDeuda = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnRechazarRecepcion = new System.Windows.Forms.DataGridViewImageColumn();
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
            // txtDeudaH1
            // 
            this.txtDeudaH1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDeudaH1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtDeudaH1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeudaH1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeudaH1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(212)))), ((int)(((byte)(216)))));
            this.txtDeudaH1.Location = new System.Drawing.Point(170, 700);
            this.txtDeudaH1.Name = "txtDeudaH1";
            this.txtDeudaH1.Size = new System.Drawing.Size(148, 18);
            this.txtDeudaH1.TabIndex = 107;
            this.txtDeudaH1.Text = "Ninguno";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(212)))), ((int)(((byte)(216)))));
            this.label13.Location = new System.Drawing.Point(12, 700);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(118, 17);
            this.label13.TabIndex = 106;
            this.label13.Text = "Deuda a Hitech 1:";
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
            this.fecha,
            this.idDeuda,
            this.nombreProducto,
            this.nombreSucursalOrigen,
            this.nombreSucursalDestino,
            this.costo,
            this.IdTraspasoMercaderia,
            this.idSucursalOrigen,
            this.idSucursalDestino,
            this.estado,
            this.btnCancelarDeuda,
            this.btnRechazarRecepcion});
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
            this.dgvData.Location = new System.Drawing.Point(18, 66);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle3;
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
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(212)))), ((int)(((byte)(216)))));
            this.label1.Location = new System.Drawing.Point(341, 701);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 109;
            this.label1.Text = "Deuda a Hitech 2:";
            // 
            // txtDeudaH2
            // 
            this.txtDeudaH2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDeudaH2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtDeudaH2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeudaH2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeudaH2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(212)))), ((int)(((byte)(216)))));
            this.txtDeudaH2.Location = new System.Drawing.Point(465, 701);
            this.txtDeudaH2.Name = "txtDeudaH2";
            this.txtDeudaH2.Size = new System.Drawing.Size(148, 18);
            this.txtDeudaH2.TabIndex = 110;
            this.txtDeudaH2.Text = "Ninguno";
            // 
            // txtDeudaAS
            // 
            this.txtDeudaAS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDeudaAS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtDeudaAS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeudaAS.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeudaAS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(212)))), ((int)(((byte)(216)))));
            this.txtDeudaAS.Location = new System.Drawing.Point(763, 701);
            this.txtDeudaAS.Name = "txtDeudaAS";
            this.txtDeudaAS.Size = new System.Drawing.Size(148, 18);
            this.txtDeudaAS.TabIndex = 112;
            this.txtDeudaAS.Text = "Ninguno";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(212)))), ((int)(((byte)(216)))));
            this.label2.Location = new System.Drawing.Point(639, 701);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 17);
            this.label2.TabIndex = 111;
            this.label2.Text = "Deuda a Apple 49:";
            // 
            // txtAC
            // 
            this.txtAC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtAC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAC.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(212)))), ((int)(((byte)(216)))));
            this.txtAC.Location = new System.Drawing.Point(1075, 699);
            this.txtAC.Name = "txtAC";
            this.txtAC.Size = new System.Drawing.Size(148, 18);
            this.txtAC.TabIndex = 114;
            this.txtAC.Text = "Ninguno";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(212)))), ((int)(((byte)(216)))));
            this.label3.Location = new System.Drawing.Point(935, 700);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 17);
            this.label3.TabIndex = 113;
            this.label3.Text = "Deuda a Apple Cafe:";
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
            // IdTraspasoMercaderia
            // 
            this.IdTraspasoMercaderia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IdTraspasoMercaderia.HeaderText = "ID ORDEN TRASPASO";
            this.IdTraspasoMercaderia.Name = "IdTraspasoMercaderia";
            this.IdTraspasoMercaderia.ReadOnly = true;
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
            // frmDeuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.txtAC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDeudaAS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDeudaH2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtDeudaH1);
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
        private System.Windows.Forms.TextBox txtDeudaH1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDeudaH2;
        private System.Windows.Forms.TextBox txtDeudaAS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDeuda;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreSucursalOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreSucursalDestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn costo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdTraspasoMercaderia;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSucursalOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSucursalDestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DataGridViewImageColumn btnCancelarDeuda;
        private System.Windows.Forms.DataGridViewImageColumn btnRechazarRecepcion;
    }
}