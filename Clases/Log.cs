using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Clases
{
    public class Log
    {
        private string Path = "";


        public Log(string Path)
        {
            this.Path = Path;
        }

        public void Add(string sLog)
        {
            CreateDirectory();
            string nombre = GetNameFile();
            string cadena = "";

            cadena += DateTime.Now + " - " + sLog + Environment.NewLine;

            StreamWriter sw = new StreamWriter(Path + "/" + nombre, true);
            sw.Write(cadena);
            sw.Close();

        }

        #region HELPER
        private string GetNameFile()
        {
            string nombre = "";

            nombre = "log.txt";

            return nombre;
        }

        private void CreateDirectory()
        {
            try
            {
                if (!Directory.Exists(Path))
                    Directory.CreateDirectory(Path);


            }
            catch (DirectoryNotFoundException ex)
            {
                throw new Exception(ex.Message);

            }
        }
        #endregion
    }
}
