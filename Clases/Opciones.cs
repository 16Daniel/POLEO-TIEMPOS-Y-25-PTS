using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clases
{
    public partial class Opciones : Form
    {
        public Opciones()
        {
            InitializeComponent();
        }

        private void Opciones_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServidorConex frm = new ServidorConex();
            frm.Show(this);
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ejecutar form = new Ejecutar();
            form.Show(this);
            this.Hide();
        }
    }
}
