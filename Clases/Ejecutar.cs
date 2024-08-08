using Clases.ApiRest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Clases
{

    public partial class Ejecutar : Form
    {
        public string command = @"net time \\" + ConfigurationManager.AppSettings.Get("IP") + " /set /yes";
        public string Status = ConfigurationManager.AppSettings.Get("Status");
        //' Se declaran como variables los objetos NotifyIcon y el ContextMenu
        NotifyIcon NotifyIcon1 = new NotifyIcon();
        ContextMenu ContextMenu1 = new ContextMenu();
        int procStar = 0;
        DBApi dBApi = new DBApi();
        SqlConnection con;
        DirConexion dirCon = new DirConexion();
        SqlDataAdapter adap = new SqlDataAdapter();
        DataSet ds = new DataSet();
        SqlDataAdapter adap1 = new SqlDataAdapter();
        DataSet ds1 = new DataSet();

        DateTime fechai = DateTime.Now;
        DateTime fechaf = DateTime.Now;
        private NotifyIcon notifyIcon;
        public Ejecutar()
        {
            InitializeComponent();

            // Inicializar el NotifyIcon
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information;
            notifyIcon.Visible = true;

            if (ConfigurationManager.AppSettings["status"] == "True")
            {
                InitializeComponent();
                con = dirCon.crearConexion();
                Log oLog = new Log(@"C:\Log Poleo(Tiempos, 25 pts y Mermas)\");
                oLog.Add("Se inicio el servicio... ");

            }
            else
            {
                MessageBox.Show("CONFIGURA SERVIDOR", "CONEXION");
                InitializeComponent();
                Log oLog = new Log(@"C:\Log Poleo(Tiempos, 25 pts y Mermas)\");
                oLog.Add("Falta configurar Servidor... ");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MostrarNotificacion("El proceso se ha iniciado.");
            enviartiemposEncocina();
            enviar25pts();
            MostrarNotificacion("El proceso se ha finalizado.");
        }

        public void enviarmermas()
        {
            Log oLog = new Log(@"C:\Log Poleo(Tiempos, 25 pts y Mermas)\");

            if (true)
            {

                oLog.Add("inicia carga mermas... ");
                var FECH1 = ConfigurationManager.AppSettings["dia"].ToString();
                var FECH2 = ConfigurationManager.AppSettings["dia"];
                try
                {


                    var envioTodo = false;
                    while (envioTodo != true)
                    {
                        con.Open();
                        SqlDataAdapter consulta2 = new SqlDataAdapter();
                        DataSet datos2 = new DataSet();

                        string stringquery = " SELECT    TOP (100) ID, FECHA, SERIE, NUMERO, CODARTICULO, REFERENCIA, DESCRIPCION, UNIDADES, PRECIO, JUSTIFICACION, COMENTARIOS, USUARIO, ENVIADO   FROM  TMERMAS WHERE  (ENVIADO IS NULL) AND (CONVERT(DATE,HORA, 102) BETWEEN CONVERT(DATE, '" + fechai.ToString("yyyy-MM-dd HH:mm:ss") + "', 102) AND CONVERT(DATE,'" + fechaf.ToString("yyyy-MM-dd HH:mm:ss") + "', 102))";

                        // oLog.Add("consulta... " + stringquery);
                        consulta2.SelectCommand = new SqlCommand(stringquery, con);


                        consulta2.Fill(datos2);

                        con.Close();
                        //oLog.Add("tiempos... " + datos2.Tables[0].Rows.Count);

                        if (datos2.Tables[0].Rows.Count > 0)
                        {

                            var empList = datos2.Tables[0].AsEnumerable().DefaultIfEmpty()
                             .Select(dataRow => new Tiempos
                             {

                                 Id = 0,
                                 IdComanda = dataRow.Field<int>("IDCOMANDA"),
                                 CodArticulo = dataRow.Field<int>("CODARTICULO"),
                                 Orden = dataRow.Field<int>("ORDEN"),
                                 Posicion = dataRow.Field<int>("POSICION"),
                                 Terminal = dataRow.Field<string>("TERMINAL"),
                                 Hora = Convert.ToString(dataRow.Field<DateTime>("HORA").ToString("O")),
                                 Descripcion = dataRow.Field<string>("DESCRIPCION"),
                                 Unidades = dataRow.Field<double>("UNIDADES"),
                                 Minutos = dataRow.Field<double>("MINUTOS"),
                                 EnTiempo = dataRow.Field<string>("ENTIEMPO"),
                                 Sucursal = ConfigurationManager.AppSettings["sucursal"],
                             }).ToList();



                            var json = JsonConvert.SerializeObject(empList);
                            try
                            {

                                dynamic respuesta = dBApi.Post("https://opera.no-ip.net/back/api_rebel_wings/api/Dashboard/envio_tiempos", json);
                                if (respuesta.success.ToString() == "True")
                                {
                                    oLog.Add("Se enviaron los registros de Mermas con exito... " + datos2.Tables[0].Rows.Count);

                                    con.Open();
                                    SqlDataAdapter query = new SqlDataAdapter();
                                    query.UpdateCommand = new SqlCommand("UPDATE TOP (100) LISTACOCINA SET  UDSPREPARADAS = 1 WHERE ((UDSRECIBIDAS <= 30 AND UDSRECIBIDAS >= 1) OR (UDSRECIBIDAS = -1)) AND (UDSPREPARADAS = 0) AND (CONVERT(DATE,HORA, 102) BETWEEN CONVERT(DATE, '" + fechai.ToString("yyyy-MM-dd HH:mm:ss") + "', 102) AND CONVERT(DATE,'" + fechaf.ToString("yyyy-MM-dd HH:mm:ss") + "', 102))", con);



                                    try
                                    {

                                        query.UpdateCommand.ExecuteNonQuery();

                                        con.Close();



                                    }
                                    catch
                                    {
                                        con.Close();
                                        oLog.Add("Error al actualizar status de registros enviados Mermas... ");
                                    }
                                }
                                else
                                {
                                    oLog.Add("Problemas con la conexion al servidor API... ");
                                }
                            }
                            catch
                            {

                                oLog.Add("Error Mermas posst API... ");
                            }

                        }
                        else
                        {
                            oLog.Add("finaliza carga mermas... ");
                            envioTodo = true;
                        }

                    }

                    // XmlsaveTiempos();
                    // oLog.Add("inicia timer... ");

                }
                catch (Exception ex)
                {
                    oLog.Add("Ocurrio un Error: " + ex);
                    con.Close();
                    MessageBox.Show("Ocurrio un Error: " + ex);
                }
            }
        }

        public void enviartiemposEncocina()
        {
            Log oLog = new Log(@"C:\Log Poleo(Tiempos, 25 pts y Mermas)\");

            if (true)
            {

                oLog.Add("inicia carga tiempos... ");
                var FECH1 = ConfigurationManager.AppSettings["dia"].ToString();
                var FECH2 = ConfigurationManager.AppSettings["dia"];
                try
                {


                    var envioTodo = false;
                    while (envioTodo != true)
                    {
                        con.Open();
                        SqlDataAdapter consulta2 = new SqlDataAdapter();
                        DataSet datos2 = new DataSet();

                        string stringquery = "SELECT  TOP (100) IDCOMANDA, CODARTICULO, ORDEN, POSICION, TERMINAL, HORA, DESCRIPCION, UNIDADES, UDSRECIBIDAS AS MINUTOS, TEMPORAL AS ENTIEMPO FROM LISTACOCINA WHERE ((UDSRECIBIDAS <= 30 AND UDSRECIBIDAS >= 1) OR (UDSRECIBIDAS = -1)) AND (UDSPREPARADAS = 0) AND (CONVERT(DATE,HORA, 102) BETWEEN CONVERT(DATE, '" + fechai.ToString("yyyy-MM-dd HH:mm:ss") + "', 102) AND CONVERT(DATE,'" + fechaf.ToString("yyyy-MM-dd HH:mm:ss") + "', 102))";

                       // oLog.Add("consulta... " + stringquery);
                        consulta2.SelectCommand = new SqlCommand(stringquery, con);


                        consulta2.Fill(datos2);

                        con.Close();
                        //oLog.Add("tiempos... " + datos2.Tables[0].Rows.Count);

                        if (datos2.Tables[0].Rows.Count > 0)
                        {

                            var empList = datos2.Tables[0].AsEnumerable().DefaultIfEmpty()
                             .Select(dataRow => new Tiempos
                             {

                                 Id = 0,
                                 IdComanda = dataRow.Field<int>("IDCOMANDA"),
                                 CodArticulo = dataRow.Field<int>("CODARTICULO"),
                                 Orden = dataRow.Field<int>("ORDEN"),
                                 Posicion = dataRow.Field<int>("POSICION"),
                                 Terminal = dataRow.Field<string>("TERMINAL"),
                                 Hora = Convert.ToString(dataRow.Field<DateTime>("HORA").ToString("O")),
                                 Descripcion = dataRow.Field<string>("DESCRIPCION"),
                                 Unidades = dataRow.Field<double>("UNIDADES"),
                                 Minutos = dataRow.Field<double>("MINUTOS"),
                                 EnTiempo = dataRow.Field<string>("ENTIEMPO"),
                                 Sucursal = ConfigurationManager.AppSettings["sucursal"],
                             }).ToList();



                            var json = JsonConvert.SerializeObject(empList);
                            try
                            {

                                dynamic respuesta = dBApi.Post("https://opera.no-ip.net/back/api_rebel_wings/api/Dashboard/envio_tiempos", json);
                                if (respuesta.success.ToString() == "True")
                                {
                                    oLog.Add("Se enviaron los registros de Tiempos con exito... " + datos2.Tables[0].Rows.Count);

                                    con.Open();
                                    SqlDataAdapter query = new SqlDataAdapter();
                                    query.UpdateCommand = new SqlCommand("UPDATE TOP (100) LISTACOCINA SET  UDSPREPARADAS = 1 WHERE ((UDSRECIBIDAS <= 30 AND UDSRECIBIDAS >= 1) OR (UDSRECIBIDAS = -1)) AND (UDSPREPARADAS = 0) AND (CONVERT(DATE,HORA, 102) BETWEEN CONVERT(DATE, '" + fechai.ToString("yyyy-MM-dd HH:mm:ss") + "', 102) AND CONVERT(DATE,'" + fechaf.ToString("yyyy-MM-dd HH:mm:ss") + "', 102))", con);



                                    try
                                    {

                                        query.UpdateCommand.ExecuteNonQuery();

                                        con.Close();



                                    }
                                    catch
                                    {
                                        con.Close();
                                        oLog.Add("Error al actualizar status de registros enviados Tiempos... ");
                                    }
                                }
                                else
                                {
                                    oLog.Add("Problemas con la conexion al servidor API... ");
                                }
                            }
                            catch
                            {

                                oLog.Add("Error Tiempos posst API... ");
                            }

                        }
                        else
                        {
                            oLog.Add("finaliza carga tiempos... ");
                            envioTodo = true;
                        }

                    }

                    // XmlsaveTiempos();
                    // oLog.Add("inicia timer... ");
                  
                }
                catch (Exception ex)
                {
                    oLog.Add("Ocurrio un Error: " + ex);
                    con.Close();
                    MessageBox.Show("Ocurrio un Error: " + ex);
                }
            }
        }

        private void XmlsaveTiempos()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string path = assembly.Location;

            Configuration config = ConfigurationManager.OpenExeConfiguration(path);

            config.Save(ConfigurationSaveMode.Modified);


            //


            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            foreach (XmlElement element in XmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {

                    foreach (XmlNode node in element.ChildNodes)
                    {

                        if (node.Attributes[0].Value == "dia")
                        {
                            node.Attributes[1].Value = DateTime.Now.ToString("yyyy-MM-dd");
                        }

                    }

                }

            }
            XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            ConfigurationManager.RefreshSection("appSettings");
            ConfigurationManager.RefreshSection("connectionStrings");

        }

        public void enviar25pts() 
        {
            Log oLog = new Log(@"C:\Log Poleo(Tiempos, 25 pts y Mermas)\");
            if (true)
            {

                oLog.Add("inicia carga 25pts... ");
                try
                {

                    var envioTodo = false;
                    while (envioTodo != true)
                    {
                        con.Open();
                        SqlDataAdapter consulta = new SqlDataAdapter();
                        DataSet datos = new DataSet();
                        consulta.SelectCommand = new SqlCommand("SELECT TOP (100) FECHAINI, SALA, MESA, TOTAL_AYC, COBROS, COBROS_MINIMOS, DIFERENCIA, JUSTIFICACION, USUARIO,VENDEDOR FROM TAYC25 WHERE (ENVIADO IS NULL) AND (CONVERT(DATE,FECHAINI, 102) BETWEEN CONVERT(DATE, '" + fechai.ToString("yyyy-MM-dd HH:mm:ss") + "', 102) AND CONVERT(DATE,'" + fechaf.ToString("yyyy-MM-dd HH:mm:ss") + "', 102))", con);


                        consulta.Fill(datos);
                        con.Close();

                        if (datos.Tables[0].Rows.Count > 0)
                        {

                            List<_25pts> empList = datos.Tables[0].AsEnumerable()
                             .Select(dataRow => new _25pts
                             {

                                 Id = 0,
                                 FechaIni = Convert.ToString(dataRow.Field<DateTime>("FECHAINI").ToString("O")),
                                 Sala = dataRow.Field<Int16>("SALA"),
                                 Mesa = dataRow.Field<Int16>("MESA"),
                                 TotalAyc = dataRow.Field<int>("TOTAL_AYC"),
                                 Cobros = dataRow.Field<int>("COBROS"),
                                 CobrosMinimos = dataRow.Field<int>("COBROS_MINIMOS"),
                                 Diferencia = dataRow.Field<int>("DIFERENCIA"),
                                 Justificacion = dataRow.Field<string>("JUSTIFICACION"),
                                 Usuario = dataRow.Field<string>("USUARIO"),
                                 Sucursal = ConfigurationManager.AppSettings["sucursal"],
                                 Vendedor = dataRow.Field<string>("VENDEDOR")

                             }).ToList();



                            var json = JsonConvert.SerializeObject(empList);


                            dynamic respuesta = dBApi.Post("https://opera.no-ip.net/back/api_rebel_wings/api/Dashboard/envio_25pts", json);
                            //dynamic respuesta = dBApi.Post("https://localhost:44308/api/Dashboard/envio_25pts", json);

                            if (respuesta.success.ToString() == "True")
                            {
                                oLog.Add("Se enviaron los registros de 25pts con exito... " + datos.Tables[0].Rows.Count);

                                con.Open();
                                SqlDataAdapter query = new SqlDataAdapter();
                                query.UpdateCommand = new SqlCommand("UPDATE TOP (100) TAYC25 SET ENVIADO = 'T' WHERE  (ENVIADO IS NULL) AND (CONVERT(DATE,FECHAINI, 102) BETWEEN CONVERT(DATE, '" + fechai.ToString("yyyy-MM-dd HH:mm:ss") + "', 102) AND CONVERT(DATE,'" + fechaf.ToString("yyyy-MM-dd HH:mm:ss") + "', 102))", con);



                                try
                                {

                                    query.UpdateCommand.ExecuteNonQuery();

                                    con.Close();



                                }
                                catch
                                {
                                    con.Close();
                                    oLog.Add("Error al actualizar status de registros enviados 25pts... ");
                                }
                            }
                            else
                            {
                                oLog.Add("Problemas con la conexion al servidor API... ");
                            }
                        }
                        else
                        {
                            oLog.Add("finaliza carga 25pts... ");
                            envioTodo = true;
                        }


                    }
                    //Xmlsave25pts();
                    //oLog.Add("inicia timer... ");

                }
                catch (Exception ex)
                {

                    oLog.Add("Ocurrio un Error: " + ex);
                    con.Close();
                    MessageBox.Show("Ocurrio un Error: " + ex);
                }


            }

        }

        private void Xmlsave25pts()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string path = assembly.Location;

            Configuration config = ConfigurationManager.OpenExeConfiguration(path);

            config.Save(ConfigurationSaveMode.Modified);


            //


            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            foreach (XmlElement element in XmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {

                    foreach (XmlNode node in element.ChildNodes)
                    {

                        if (node.Attributes[0].Value == "dia2")
                        {
                            node.Attributes[1].Value = DateTime.Now.ToString("yyyy-MM-dd");
                        }

                    }

                }

            }
            XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            ConfigurationManager.RefreshSection("appSettings");
            ConfigurationManager.RefreshSection("connectionStrings");

        }

        private void fechainicial_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker temp = (DateTimePicker)sender;
            fechai = temp.Value;
        }

        private void fechafinal_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker temp = (DateTimePicker)sender;
            fechaf = temp.Value;
        }

        private void MostrarNotificacion(string mensaje)
        {
            // Mostrar un mensaje de notificación como tooltip
            notifyIcon.ShowBalloonTip(1500, "Notificación", mensaje, ToolTipIcon.Info);
        }

    }
}
