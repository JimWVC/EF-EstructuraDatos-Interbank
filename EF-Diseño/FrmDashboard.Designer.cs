namespace EF_Diseño
{
    partial class FrmDashboard
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPagina = new System.Windows.Forms.Label();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.cmbEscenario = new System.Windows.Forms.ComboBox();
            this.dgvTransferencias = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLineaTiempo = new System.Windows.Forms.Button();
            this.btnExcedentes = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransferencias)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblPagina);
            this.groupBox1.Controls.Add(this.btnEjecutar);
            this.groupBox1.Controls.Add(this.btnSiguiente);
            this.groupBox1.Controls.Add(this.btnAnterior);
            this.groupBox1.Controls.Add(this.cmbEscenario);
            this.groupBox1.Controls.Add(this.dgvTransferencias);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 358);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reporte Paginado";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(183, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = " Transferencias Válidas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Escenario de Prueba";
            // 
            // lblPagina
            // 
            this.lblPagina.AutoSize = true;
            this.lblPagina.Location = new System.Drawing.Point(311, 319);
            this.lblPagina.Name = "lblPagina";
            this.lblPagina.Size = new System.Drawing.Size(80, 13);
            this.lblPagina.TabIndex = 11;
            this.lblPagina.Text = "Numero Pagina";
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Location = new System.Drawing.Point(443, 93);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(120, 42);
            this.btnEjecutar.TabIndex = 10;
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSiguiente.Location = new System.Drawing.Point(142, 306);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(83, 37);
            this.btnSiguiente.TabIndex = 9;
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnterior.Location = new System.Drawing.Point(39, 306);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(83, 37);
            this.btnAnterior.TabIndex = 8;
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // cmbEscenario
            // 
            this.cmbEscenario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEscenario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEscenario.FormattingEnabled = true;
            this.cmbEscenario.Location = new System.Drawing.Point(186, 49);
            this.cmbEscenario.Name = "cmbEscenario";
            this.cmbEscenario.Size = new System.Drawing.Size(377, 23);
            this.cmbEscenario.TabIndex = 7;
            // 
            // dgvTransferencias
            // 
            this.dgvTransferencias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransferencias.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dgvTransferencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransferencias.Location = new System.Drawing.Point(18, 141);
            this.dgvTransferencias.Name = "dgvTransferencias";
            this.dgvTransferencias.Size = new System.Drawing.Size(544, 159);
            this.dgvTransferencias.TabIndex = 6;
            this.dgvTransferencias.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTransferencias_CellContentClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnLineaTiempo);
            this.groupBox2.Controls.Add(this.btnExcedentes);
            this.groupBox2.Location = new System.Drawing.Point(12, 387);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(597, 99);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Módulos de Inspección";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(95, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(355, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "MÓDULOS DE INSPECCIÓN Y AUDITORÍA DETALLADA";
            // 
            // btnLineaTiempo
            // 
            this.btnLineaTiempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLineaTiempo.Location = new System.Drawing.Point(347, 61);
            this.btnLineaTiempo.Name = "btnLineaTiempo";
            this.btnLineaTiempo.Size = new System.Drawing.Size(142, 32);
            this.btnLineaTiempo.TabIndex = 14;
            this.btnLineaTiempo.Text = "Linea de tiempo";
            this.btnLineaTiempo.UseVisualStyleBackColor = true;
            this.btnLineaTiempo.Click += new System.EventHandler(this.btnLineaTiempo_Click);
            // 
            // btnExcedentes
            // 
            this.btnExcedentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcedentes.Location = new System.Drawing.Point(40, 61);
            this.btnExcedentes.Name = "btnExcedentes";
            this.btnExcedentes.Size = new System.Drawing.Size(128, 32);
            this.btnExcedentes.TabIndex = 13;
            this.btnExcedentes.Text = "Ir Excedentes";
            this.btnExcedentes.UseVisualStyleBackColor = true;
            this.btnExcedentes.Click += new System.EventHandler(this.btnExcedentes_Click);
            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 514);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmDashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.FrmDashboard_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransferencias)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPagina;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.ComboBox cmbEscenario;
        private System.Windows.Forms.DataGridView dgvTransferencias;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExcedentes;
        private System.Windows.Forms.Button btnLineaTiempo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}

