namespace Clases
{
    partial class ServidorConex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServidorConex));
            this.label5 = new System.Windows.Forms.Label();
            this.textSucursal = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBase = new System.Windows.Forms.TextBox();
            this.textUsuario = new System.Windows.Forms.TextBox();
            this.textContraseña = new System.Windows.Forms.TextBox();
            this.textServidor = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "SUCURSAL";
            // 
            // textSucursal
            // 
            this.textSucursal.Location = new System.Drawing.Point(151, 169);
            this.textSucursal.Name = "textSucursal";
            this.textSucursal.Size = new System.Drawing.Size(149, 20);
            this.textSucursal.TabIndex = 33;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(131, 304);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 29;
            this.button3.Text = "SALIR";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(151, 239);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(149, 23);
            this.button2.TabIndex = 26;
            this.button2.Text = "TEST";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "CONTRASEÑA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "USUARIO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "BASE DE DATOS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "SERVIDOR";
            // 
            // textBase
            // 
            this.textBase.Location = new System.Drawing.Point(151, 55);
            this.textBase.Name = "textBase";
            this.textBase.Size = new System.Drawing.Size(149, 20);
            this.textBase.TabIndex = 23;
            // 
            // textUsuario
            // 
            this.textUsuario.Location = new System.Drawing.Point(151, 93);
            this.textUsuario.Name = "textUsuario";
            this.textUsuario.Size = new System.Drawing.Size(149, 20);
            this.textUsuario.TabIndex = 24;
            // 
            // textContraseña
            // 
            this.textContraseña.Location = new System.Drawing.Point(151, 132);
            this.textContraseña.Name = "textContraseña";
            this.textContraseña.Size = new System.Drawing.Size(149, 20);
            this.textContraseña.TabIndex = 25;
            // 
            // textServidor
            // 
            this.textServidor.Location = new System.Drawing.Point(151, 16);
            this.textServidor.Name = "textServidor";
            this.textServidor.Size = new System.Drawing.Size(149, 20);
            this.textServidor.TabIndex = 22;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(225, 304);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "GUARDAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ServidorConex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 343);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textSucursal);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBase);
            this.Controls.Add(this.textUsuario);
            this.Controls.Add(this.textContraseña);
            this.Controls.Add(this.textServidor);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServidorConex";
            this.Text = "ServidorConex";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textSucursal;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBase;
        private System.Windows.Forms.TextBox textUsuario;
        private System.Windows.Forms.TextBox textContraseña;
        private System.Windows.Forms.TextBox textServidor;
        private System.Windows.Forms.Button button1;
    }
}