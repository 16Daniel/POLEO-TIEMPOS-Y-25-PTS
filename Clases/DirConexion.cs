using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace Clases
{
    public class DirConexion
    {
        public string PrincipalConexion;

        //Conexion SQL
        //Data Source=gilberto-pc\sqlexpress;AttachDbFilename="C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\TRW.mdf";Integrated Security=True


        public SqlConnection crearConexion()
        {

            //SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-JVT65GU\\SQLEXPRESS;Initial Catalog=TRW11;Integrated Security=True;Pooling=False"); 

            //ESTA LINEA DE ABAJO ES LA DE LAS SUCURSALES
            //SqlConnection conexion = new SqlConnection(@"Data Source=RWBUCARELI\SQLEXPRESS;Initial Catalog=DBFREST;User Id=icgadmin;Password=masterkey"); 
            //ESTA LINEA DE ABAJO ES PARA TRABAJAR EN SERVIDOR
            //SqlConnection conexion = new SqlConnection("Data Source =WIN-8UEESSEO0HB;Initial Catalog=RW_PROYECTO;User Id=icgadmin;Password=masterkey");

            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["conexion"]);



            return conexion;

        }
    }
}
