using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using System.Xml;
using System.IO;

namespace Clases
{
    public partial class ServidorConex : Form
    {
        SqlConnection con;
        DirConexion dirCon = new DirConexion();
        public ServidorConex()
        {
            InitializeComponent();


            //MessageBox.Show("YA SE ESTA EJECUTANDO " + url);
        }

        private void ServidorConex_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Conexion = "Data Source =" + textServidor.Text + ";Initial Catalog=" + textBase.Text + ";User Id=" + textUsuario.Text + ";Password=" + textContraseña.Text + "";
            ConfigurationManager.AppSettings["conexion"] = Conexion;
            try
            {

                dirCon = new DirConexion();
                con = dirCon.crearConexion();
                SqlDataAdapter query = new SqlDataAdapter();
                SqlDataAdapter consulta = new SqlDataAdapter();
                DataSet datos = new DataSet();
                consulta.SelectCommand = new SqlCommand("select	* from TUsuarios ", con);
                consulta.Fill(datos);
                string Usuario = datos.Tables[0].Rows[0][1].ToString();
                MessageBox.Show("Conexion Exitosa ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ConfigurationManager.AppSettings["status"] = "True";
                button1.Enabled = true;
                textBase.Enabled = false;
                textServidor.Enabled = false;
                textUsuario.Enabled = false;
                textContraseña.Enabled = false;
                textSucursal.Enabled = false;
                button2.Enabled = false;


                //

                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                foreach (XmlElement element in XmlDoc.DocumentElement)
                {
                    if (element.Name.Equals("appSettings"))
                    {

                        foreach (XmlNode node in element.ChildNodes)
                        {

                            if (node.Attributes[0].Value == "status")
                            {
                                node.Attributes[1].Value = "True";
                            }
                            if (node.Attributes[0].Value == "conexion")
                            {
                                node.Attributes[1].Value = Conexion;
                            }
                            if (node.Attributes[0].Value == "sucursal")
                            {
                                node.Attributes[1].Value = "" + textSucursal.Text;
                            }
                            if (node.Attributes[0].Value == "dia")
                            {
                                node.Attributes[1].Value = "2023-08-01";
                            }
                            if (node.Attributes[0].Value == "dia2")
                            {
                                node.Attributes[1].Value = "2023-08-01";
                            }
                        }

                    }

                }
                XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                ConfigurationManager.RefreshSection("appSettings");
                ConfigurationManager.RefreshSection("connectionStrings");

                env25pts();

            }
            catch
            {
                MessageBox.Show("Conexion no Exitosa ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            

        }
        private void env25pts()
        {
            try
            {

                string connsql = ConfigurationManager.AppSettings["conexion"];
                SqlConnection conn = new SqlConnection(connsql);
                conn.Open();
                StreamReader readfilequery = new StreamReader(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"src\agrega25.sql");
                string query = "";
                string line = readfilequery.ReadLine();
                while (line != null)
                {
                    if (line.IndexOf("GO") == -1)
                    {
                        query = query + " " + line;
                    }
                    else
                    {
                        SqlCommand commqnd = new SqlCommand(query, conn);
                        commqnd.ExecuteNonQuery();
                        query = null;
                    }
                    line = readfilequery.ReadLine();
                }

                MessageBox.Show("SE GENERO CAMPO A TABLA DE 25PTS");

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.InnerException.ToString());
                //MessageBox.Show(ex.Message);
                MessageBox.Show("YA SE ENCUENTRA EL CAMPO EN LA TABLA DE 25PTS");
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.ResetApp();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.ResetApp();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void conServerRemoto()
        {
            try
            {

                string connsql = ConfigurationManager.AppSettings["conexion"];
                SqlConnection conn = new SqlConnection(connsql);
                conn.Open();
                StreamReader readfilequery = new StreamReader(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"src\servidor.sql");
                string query = "";
                string line = readfilequery.ReadLine();
                while (line != null)
                {
                    if (line.IndexOf("GO") == -1)
                    {
                        query = query + " " + line;
                    }
                    else
                    {
                        SqlCommand commqnd = new SqlCommand(query, conn);
                        commqnd.ExecuteNonQuery();
                        query = null;
                    }
                    line = readfilequery.ReadLine();
                }

                MessageBox.Show("SE CREO LA CONEXION AL SERVIDOR REMOTO ");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void procTiempos()
        {
            try
            {

                string connsql = ConfigurationManager.AppSettings["conexion"];
                SqlConnection conn = new SqlConnection(connsql);
                conn.Open();
                StreamReader readfilequery = new StreamReader(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"src\tiempos.sql");
                string query = "";
                string line = readfilequery.ReadLine();
                while (line != null)
                {
                    if (line.IndexOf("GO") == -1)
                    {
                        query = query + " " + line;
                    }
                    else
                    {
                        SqlCommand commqnd = new SqlCommand(query, conn);
                        commqnd.ExecuteNonQuery();
                        query = null;
                    }
                    line = readfilequery.ReadLine();
                }

                MessageBox.Show("SE GUARDO EL PROCEDIMIENTO DE TIEMPOS");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void proc25pts()
        {
            try
            {

                string connsql = ConfigurationManager.AppSettings["conexion"];
                SqlConnection conn = new SqlConnection(connsql);
                conn.Open();
                StreamReader readfilequery = new StreamReader(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"src\25pts.sql");
                string query = "";
                string line = readfilequery.ReadLine();
                while (line != null)
                {
                    if (line.IndexOf("GO") == -1)
                    {
                        query = query + " " + line;
                    }
                    else
                    {
                        SqlCommand commqnd = new SqlCommand(query, conn);
                        commqnd.ExecuteNonQuery();
                        query = null;
                    }
                    line = readfilequery.ReadLine();
                }

                MessageBox.Show("SE GUARDO EL PROCEDIMIENTO DE 25PTS");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void envTiempos()
        {
            try
            {

                string connsql = ConfigurationManager.AppSettings["conexion"];
                SqlConnection conn = new SqlConnection(connsql);
                conn.Open();
                StreamReader readfilequery = new StreamReader(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"src\tiemposenvio.sql");
                string query = "";
                string line = readfilequery.ReadLine();
                while (line != null)
                {
                    if (line.IndexOf("GO") == -1)
                    {
                        query = query + " " + line;
                    }
                    else
                    {
                        SqlCommand commqnd = new SqlCommand(query, conn);
                        commqnd.ExecuteNonQuery();
                        query = null;
                    }
                    line = readfilequery.ReadLine();
                }

                MessageBox.Show("SE GENERO TABLA DE ENVIO DE TIEMPOS");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }
    }
}
