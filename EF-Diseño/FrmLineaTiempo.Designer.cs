namespace EF_Diseño
{
    partial class FrmLineaTiempo
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
            this.txtLineaTiempo = new System.Windows.Forms.RichTextBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtLineaTiempo
            // 
            this.txtLineaTiempo.Location = new System.Drawing.Point(72, 94);
            this.txtLineaTiempo.Name = "txtLineaTiempo";
            this.txtLineaTiempo.ReadOnly = true;
            this.txtLineaTiempo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtLineaTiempo.Size = new System.Drawing.Size(403, 189);
            this.txtLineaTiempo.TabIndex = 0;
            this.txtLineaTiempo.Text = "";
            // 
            // btnVolver
            // 
            this.btnVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(200, 317);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(121, 46);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Text = "Volver al Dashboard";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(449, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Auditoría Cronológica Circular (Lista Circular Enlazada)";
            // 
            // FrmLineaTiempo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 399);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.txtLineaTiempo);
            this.Name = "FrmLineaTiempo";
            this.Text = "FrmLineaTiempo";
            this.Load += new System.EventHandler(this.FrmLineaTiempo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtLineaTiempo;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label label1;
    }
}