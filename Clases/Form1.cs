using Clases.ApiRest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Timers;
using System.Configuration;
using System.Xml;
using System.Reflection;
using System.Data;
using System.Diagnostics;

namespace Clases
{
    public partial class Form1 : Form
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


        public Form1()
        {

            if (getPrevInstance())
            {
                this.Close();
                Application.Exit();
                Application.ExitThread();
            }
            else
            {
                if (ConfigurationManager.AppSettings["status"] == "True")
                {
                    InitializeComponent();
                    con = dirCon.crearConexion();
                    timerIP.Enabled = true;
                    Log oLog = new Log(@"C:\Log Poleo(Tiempos y 25 pts)\");
                    oLog.Add("Se inicio el servicio... ");
                }
                else
                {
                    MessageBox.Show("CONFIGURA SERVIDOR", "CONEXION");
                    InitializeComponent();
                    Log oLog = new Log(@"C:\Log Poleo(Tiempos y 25 pts)\");
                    oLog.Add("Falta configurar Servidor... ");
                }

            }



        }

        private static bool getPrevInstance()
        {
            //get the name of current process, i,e the process 
            //name of this current application

            string currPrsName = Process.GetCurrentProcess().ProcessName;

            //Get the name of all processes having the 
            //same name as this process name 
            Process[] allProcessWithThisName
                         = Process.GetProcessesByName(currPrsName);

            //if more than one process is running return true.
            //which means already previous instance of the application 
            //is running
            if (allProcessWithThisName.Length > 1)
            {
                MessageBox.Show("YA SE ESTA EJECUTANDO");

                return true; // Yes Previous Instance Exist
            }
            else
            {
                return false; //No Prev Instance Running
            }
        }

        static void ExecuteAsAdmin(string fileName, string script)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.Arguments = "/c" + script;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.RedirectStandardOutput = true;


            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //proc.StartInfo.Verb = "runas";

            proc.Start();
            string output = proc.StandardOutput.ReadToEnd();

            //string path = HttpContext.Current.Request.MapPath("~/log/log.txt");
            Log oLog = new Log(@"C:\Log Poleo(Tiempos y 25 pts)\");
            oLog.Add("Se ejecuto correctamente " + output);
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            //' Asignar los submenús del ContextMenu
            //'
            //' Añadimos la opción Restaurar, que será el elemento predeterminado
            //          MenuItem tMenu = new MenuItem("&Restaurar", new EventHandler(this.Restaurar_Click));
            //          tMenu.DefaultItem = true;
            //          ContextMenu1.MenuItems.Add(tMenu);
            //
            //' Esto también se puede hacer así:
            ContextMenu1.MenuItems.Add("&Restaurar", new EventHandler(this.Restaurar_Click));
            ContextMenu1.MenuItems[0].DefaultItem = true;
            //'
            //' Añadimos un separador
            ContextMenu1.MenuItems.Add("-");
            //' Añadimos el elemento Acerca de...
            ContextMenu1.MenuItems.Add("V3.0", new EventHandler(this.AcercaDe_Click));
            //' Añadimos otro separador
            ContextMenu1.MenuItems.Add("-");
            //' Añadimos la opción de salir
            ContextMenu1.MenuItems.Add("&Salir", new EventHandler(this.Salir_Click));
            //'
            //' Asignar los valores para el NotifyIcon
            NotifyIcon1.Icon = this.Icon;
            NotifyIcon1.ContextMenu = this.ContextMenu1;
            NotifyIcon1.Text = Application.ProductName;
            NotifyIcon1.Visible = true;
            NotifyIcon1.BalloonTipTitle = "POLEO";
            NotifyIcon1.BalloonTipText = "Configurar Servidor";
            //
            // Asignamos los otros eventos al formulario
            this.Resize += new EventHandler(this.Form1_Resize);
            this.Activated += new EventHandler(this.Form1_Activated);
            this.Closing += new CancelEventHandler(this.Form1_Closing);
            // Asignamos el evento DoubleClick del NotifyIcon
            this.NotifyIcon1.DoubleClick += new EventHandler(this.Restaurar_Click);


        }



        private void Salir_Click(object sender, System.EventArgs e)
        {
            //' Este procedimiento se usa para cerrar el formulario,
            //' se usará como procedimiento de eventos, en principio usado por el botón Salir
            this.Close();
            Application.Exit();
            Application.ExitThread();
        }

        private void Restaurar_Click(object sender, System.EventArgs e)
        {
            //' Restaurar por si se minimizó
            //' Este evento manejará tanto los menús Restaurar como el NotifyIcon.DoubleClick
            Show();
            WindowState = FormWindowState.Normal;
            Activate();

        }

        private void AcercaDe_Click(object sender, System.EventArgs e)
        {
            //' Mostrar la información del autor, versión, etc.
            MessageBox.Show("POLEO V3.0", "VERSIÓN");
        }
        private void Form1_Resize(object sender, System.EventArgs e)
        {
            //' Cuando se minimice, ocultarla, se quedará disponible en la barra de tareas
            if (this.WindowState == FormWindowState.Minimized)
                this.Visible = false;
        }

        // la declaramos fuera de la función, para que mantenga su valor
        Boolean PrimeraVez = true;
        //
        private void Form1_Activated(object sender, System.EventArgs e)
        {
            // En C# no se puede usar static para hacer que una variable mantenga su valor
            // en C/C++ sí que se puede
            //static Boolean PrimeraVez = true;
            //
            //' La primera vez que se active, ocultar el form,
            //' es una chapuza, pero el formulario no permite que se oculte en el Form_Load
            if (PrimeraVez)
            {
                PrimeraVez = false;
                Visible = false;
            }
        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            // Cuando se va a cerrar el formulario...
            // eliminar el objeto de la barra de tareas
            this.NotifyIcon1.Visible = false;
            // esto es necesario, para que no se quede el icono en la barra de tareas
            // (el cual se quita al pasar el ratón por encima)
            this.NotifyIcon1 = null;
            // de paso eliminamos el menú contextual
            this.ContextMenu1 = null;
        }


        private void ButtonAceptar_Click(object sender, EventArgs e)
        {
            if (textBoxContraseña.Text == "172106")
            {
                Opciones op = new Opciones();
                op.Show(this);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Inicio de Sesion", "Contraseña Icorrecta");
                textBoxContraseña.Clear();
            }
        }

        private void TimerIP_Tick(object sender, EventArgs e)
        {
            //ACCION DEL TIMER
            if (ConfigurationManager.AppSettings["dia"] != DateTime.Now.ToString("yyyyMMdd") && procStar == 0)
            {
                //string path = HttpContext.Current.Request.MapPath("~/log/log.txt");

                procStar = 1;
                ServiceEnvioTiempos();
                procStar = 0;

                //oLog.Add("sale tiempos... ");
            }
            if (ConfigurationManager.AppSettings["dia2"] != DateTime.Now.ToString("yyyyMMdd") && procStar == 0)
            {

                procStar = 1;
                ServiceEnvio25pts();
                procStar = 0;
            }
        }
        public void ResetApp()
        {
            this.Close();
            Application.Exit();
            Application.ExitThread();
            // Cuando se va a cerrar el formulario...
            // eliminar el objeto de la barra de tareas
            this.NotifyIcon1.Visible = false;
            // esto es necesario, para que no se quede el icono en la barra de tareas
            // (el cual se quita al pasar el ratón por encima)
            this.NotifyIcon1 = null;
            // de paso eliminamos el menú contextual
            this.ContextMenu1 = null;
            Application.Restart();

        }

        private void ServiceEnvioTiempos()
        {

            Log oLog = new Log(@"C:\Log Poleo(Tiempos y 25 pts)\");

            if (DateTime.Now.Hour >= 23 && DateTime.Now.Minute >= 45)
            {
                this.timerIP.Stop();
            
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
                    consulta2.SelectCommand = new SqlCommand("SELECT  TOP (100) IDCOMANDA, CODARTICULO, ORDEN, POSICION, TERMINAL, HORA, DESCRIPCION, UNIDADES, UDSRECIBIDAS AS MINUTOS, TEMPORAL AS ENTIEMPO FROM LISTACOCINA WHERE ((UDSRECIBIDAS <= 30 AND UDSRECIBIDAS >= 1) OR (UDSRECIBIDAS = -1)) AND (UDSPREPARADAS = 0) AND (HORA >= CONVERT(DATETIME, '" + ConfigurationManager.AppSettings["dia"] + " 00:00:00', 102))", con);


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
                                query.UpdateCommand = new SqlCommand("UPDATE TOP (100) LISTACOCINA SET  UDSPREPARADAS = 1 WHERE ((UDSRECIBIDAS <= 30 AND UDSRECIBIDAS >= 1) OR (UDSRECIBIDAS = -1)) AND (UDSPREPARADAS = 0) AND (HORA >= CONVERT(DATETIME, '" + ConfigurationManager.AppSettings["dia"] + " 00:00:00', 102))", con);



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

                XmlsaveTiempos();
                this.timerIP.Start();
                oLog.Add("inicia timer... ");

            }
            catch (Exception ex)
            {
                oLog.Add("Ocurrio un Error: " + ex);
                con.Close();
                this.timerIP.Start();
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

        private void ServiceEnvio25pts()
        {
            Log oLog = new Log(@"C:\Log Poleo(Tiempos y 25 pts)\");
            if (DateTime.Now.Hour >= 23 && DateTime.Now.Minute >= 45)
            {
                this.timerIP.Stop();
            
            oLog.Add("inicia carga 25pts... ");
            try
            {

                var envioTodo = false;
                while (envioTodo != true)
                {
                    con.Open();
                    SqlDataAdapter consulta = new SqlDataAdapter();
                    DataSet datos = new DataSet();
                    consulta.SelectCommand = new SqlCommand("SELECT TOP (100) FECHAINI, SALA, MESA, TOTAL_AYC, COBROS, COBROS_MINIMOS, DIFERENCIA, JUSTIFICACION, USUARIO,VENDEDOR FROM TAYC25 WHERE (ENVIADO IS NULL) AND (FECHAINI >= CONVERT(DATETIME, '" + ConfigurationManager.AppSettings["dia2"] + " 00:00:00', 102))", con);


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

                        if (respuesta.success.ToString() == "True")
                        {
                            oLog.Add("Se enviaron los registros de 25pts con exito... " + datos.Tables[0].Rows.Count);

                            con.Open();
                            SqlDataAdapter query = new SqlDataAdapter();
                            query.UpdateCommand = new SqlCommand("UPDATE TOP (100) TAYC25 SET ENVIADO = 'T' WHERE  (ENVIADO IS NULL) AND (FECHAINI >= CONVERT(DATETIME, '" + ConfigurationManager.AppSettings["dia2"] + " 00:00:00', 102))", con);



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
                Xmlsave25pts();

                this.timerIP.Start();
                //oLog.Add("inicia timer... ");


            }
            catch (Exception ex)
            {
                
                oLog.Add("Ocurrio un Error: " + ex);
                con.Close();
                this.timerIP.Start();
            }


            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log oLog = new Log(@"C:\Log Poleo(Tiempos y 25 pts)\");
            oLog.Add("Se detuvo el servicio... ");
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

    }

    public class Tiempos
    {

        public int Id { get; set; }
        public int IdComanda { get; set; }
        public int CodArticulo { get; set; }
        public int Orden { get; set; }
        public int Posicion { get; set; }
        public string Terminal { get; set; }
        public string Hora { get; set; }
        public string Descripcion { get; set; }
        public double Unidades { get; set; }
        public double Minutos { get; set; }
        public string EnTiempo { get; set; }
        public string Sucursal { get; set; }
    }
    public class _25pts
    {
        public int Id { get; set; }
        public string FechaIni { get; set; }
        public int Sala { get; set; }
        public int Mesa { get; set; }
        public int TotalAyc { get; set; }
        public int Cobros { get; set; }
        public int CobrosMinimos { get; set; }
        public int Diferencia { get; set; }
        public string Justificacion { get; set; }
        public string Usuario { get; set; }
        public string Sucursal { get; set; }

        public string Vendedor { get; set; }    
    }
}
