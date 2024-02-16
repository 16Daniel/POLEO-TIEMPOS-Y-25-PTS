namespace Clases
{
    partial class Ejecutar
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.timerIP = new System.Windows.Forms.Timer(this.components);
            this.fechainicial = new System.Windows.Forms.DateTimePicker();
            this.fechafinal = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fecha inicial";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(381, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fecha final";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(213, 171);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(247, 48);
            this.button1.TabIndex = 4;
            this.button1.Text = "EJECUTAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timerIP
            // 
            this.timerIP.Interval = 300000;
            // 
            // fechainicial
            // 
            this.fechainicial.Location = new System.Drawing.Point(58, 96);
            this.fechainicial.Name = "fechainicial";
            this.fechainicial.Size = new System.Drawing.Size(302, 22);
            this.fechainicial.TabIndex = 5;
            this.fechainicial.ValueChanged += new System.EventHandler(this.fechainicial_ValueChanged);
            // 
            // fechafinal
            // 
            this.fechafinal.Location = new System.Drawing.Point(384, 96);
            this.fechafinal.Name = "fechafinal";
            this.fechafinal.Size = new System.Drawing.Size(304, 22);
            this.fechafinal.TabIndex = 6;
            this.fechafinal.ValueChanged += new System.EventHandler(this.fechafinal_ValueChanged);
            // 
            // Ejecutar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 285);
            this.Controls.Add(this.fechafinal);
            this.Controls.Add(this.fechainicial);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Ejecutar";
            this.Text = "Ejecutar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timerIP;
        private System.Windows.Forms.DateTimePicker fechainicial;
        private System.Windows.Forms.DateTimePicker fechafinal;
    }
}