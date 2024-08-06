
namespace CapaPresentacion
{
    partial class frmOrdenesDeTraspaso
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
            this.label10 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.FechaCreacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Confirmada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdOrdenTraspaso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idSucursalOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idSucursalDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaConfirmacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnConfirmarRecepcion = new System.Windows.Forms.DataGridViewButtonColumn();
            this.txtProductoSeleccionado = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(83)))), ((int)(((byte)(150)))));
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1350, 729);
            this.label10.TabIndex = 86;
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
            this.FechaCreacion,
            this.idProducto,
            this.producto,
            this.Cantidad,
            this.Confirmada,
            this.IdOrdenTraspaso,
            this.idSucursalOrigen,
            this.idSucursalDestino,
            this.FechaConfirmacion,
            this.btnConfirmarRecepcion});
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
            this.dgvData.TabIndex = 91;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvData_CellPainting);
            // 
            // FechaCreacion
            // 
            this.FechaCreacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FechaCreacion.HeaderText = "FECHA CREACION";
            this.FechaCreacion.Name = "FechaCreacion";
            this.FechaCreacion.ReadOnly = true;
            this.FechaCreacion.Width = 120;
            // 
            // idProducto
            // 
            this.idProducto.HeaderText = "ID PRODUCTO";
            this.idProducto.Name = "idProducto";
            this.idProducto.ReadOnly = true;
            this.idProducto.Visible = false;
            this.idProducto.Width = 170;
            // 
            // producto
            // 
            this.producto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.producto.HeaderText = "PRODUCTO";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            // 
            // Cantidad
            // 
            this.Cantidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Cantidad.HeaderText = "CANTIDAD";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            // 
            // Confirmada
            // 
            this.Confirmada.HeaderText = "CONFIRMADA";
            this.Confirmada.Name = "Confirmada";
            this.Confirmada.ReadOnly = true;
            this.Confirmada.Visible = false;
            this.Confirmada.Width = 140;
            // 
            // IdOrdenTraspaso
            // 
            this.IdOrdenTraspaso.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IdOrdenTraspaso.HeaderText = "ID ORDEN TRASPASO";
            this.IdOrdenTraspaso.Name = "IdOrdenTraspaso";
            this.IdOrdenTraspaso.ReadOnly = true;
            this.IdOrdenTraspaso.Visible = false;
            this.IdOrdenTraspaso.Width = 140;
            // 
            // idSucursalOrigen
            // 
            this.idSucursalOrigen.HeaderText = "ID SUCURSAL ORIGEN";
            this.idSucursalOrigen.Name = "idSucursalOrigen";
            this.idSucursalOrigen.ReadOnly = true;
            this.idSucursalOrigen.Visible = false;
            this.idSucursalOrigen.Width = 140;
            // 
            // idSucursalDestino
            // 
            this.idSucursalDestino.HeaderText = "ID SUCURSAL DESTINO";
            this.idSucursalDestino.Name = "idSucursalDestino";
            this.idSucursalDestino.ReadOnly = true;
            this.idSucursalDestino.Visible = false;
            this.idSucursalDestino.Width = 150;
            // 
            // FechaConfirmacion
            // 
            this.FechaConfirmacion.HeaderText = "FECHA CONFIRMACION";
            this.FechaConfirmacion.Name = "FechaConfirmacion";
            this.FechaConfirmacion.ReadOnly = true;
            this.FechaConfirmacion.Visible = false;
            this.FechaConfirmacion.Width = 140;
            // 
            // btnConfirmarRecepcion
            // 
            this.btnConfirmarRecepcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.btnConfirmarRecepcion.HeaderText = "CONFIRMAR RECEPCION";
            this.btnConfirmarRecepcion.Name = "btnConfirmarRecepcion";
            this.btnConfirmarRecepcion.ReadOnly = true;
            this.btnConfirmarRecepcion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnConfirmarRecepcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnConfirmarRecepcion.Width = 120;
            // 
            // txtProductoSeleccionado
            // 
            this.txtProductoSeleccionado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtProductoSeleccionado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtProductoSeleccionado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProductoSeleccionado.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductoSeleccionado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(212)))), ((int)(((byte)(216)))));
            this.txtProductoSeleccionado.Location = new System.Drawing.Point(170, 700);
            this.txtProductoSeleccionado.Name = "txtProductoSeleccionado";
            this.txtProductoSeleccionado.Size = new System.Drawing.Size(206, 18);
            this.txtProductoSeleccionado.TabIndex = 101;
            this.txtProductoSeleccionado.Text = "Ninguno";
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
            this.label13.Size = new System.Drawing.Size(152, 17);
            this.label13.TabIndex = 100;
            this.label13.Text = "Producto Seleccionado:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1350, 729);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 88;
            this.pictureBox1.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label11.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label11.Location = new System.Drawing.Point(12, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(872, 32);
            this.label11.TabIndex = 102;
            this.label11.Text = "LISTA DE ORDENES DE TRASPASO PENDIENTES  A CONFIRMAR RECEPCION";
            // 
            // frmOrdenesDeTraspaso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.txtProductoSeleccionado);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label10);
            this.Name = "frmOrdenesDeTraspaso";
            this.Text = "frmOrdenesDeTraspaso";
            this.Load += new System.EventHandler(this.frmOrdenesDeTraspaso_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox txtProductoSeleccionado;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaCreacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Confirmada;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdOrdenTraspaso;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSucursalOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSucursalDestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaConfirmacion;
        private System.Windows.Forms.DataGridViewButtonColumn btnConfirmarRecepcion;
    }
}