using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using Microsoft.Office;
using Microsoft.Office.Interop;
using Ex = Microsoft.Office.Interop.Excel;


namespace CANSAT2019
{
    static class Program
    {
        // VARIABLES PARA MANEJAR BOTONES
        public static int Campos = 9;
        // VARIABLES PARA CONECTAR ARDUINO
        public static string Trama = "c";
        public static string PuertoSeleccionado; //COM4, COM6, EJEMPLO
        public static string[] Puertos;          //ARREGLO PUERTOS ENCONTRADOS ACTUALMENTE

        // VARIABLES PARA CONECTAR EXCEL
        public static string DireccionExcel = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        public static string NombreExcel = "Cansat2019.xlsx";

        //----------------------------------------------------------------------------------------------------
        // VARIABLES DE LA LISTA
        public static int Cantidad_Datos = 8;
        public static List<Telemetria> ListaTelemetria = new List<Telemetria>();
        // LISTA PARA ALMACENAR LOS DATOS 
        public class Telemetria
        {
            // DATOS MISION GENERAL 
            public float TempExt;
            public float TempInt;
            public float Presion;
            public float Humedad;
            public float Altitud;
            public float Lngitud;
            public float Latitud;
            public float Voltaje;
            public float Calidad;
            
            // CONSTRUCTOR 
            public Telemetria(float T_E, float T_I, float PRE, float HUM, float ALT, float LON, float LAT, float VOL, float CAL)
            {
                TempExt = T_E;
                TempInt = T_I;
                Presion = PRE;
                Humedad = HUM;
                Altitud = ALT;
                Lngitud = LON;
                Latitud = LAT;
                Voltaje = VOL;
                Calidad = CAL;
            }
        }
        

        public static void _Mandar_Datos_a_Excel()
        {
            //EL EXCEL SIEMPRE SE GUARDARA EN EL ESCRITORIO
            DireccionExcel = DireccionExcel + "\\" + NombreExcel;

            //ENVIAR LA LISTA DE DATOS A EXCEL
            if (File.Exists(DireccionExcel))
            {
                File.Delete(DireccionExcel);
            }

            Ex.Application Excel = new Ex.Application();
            Ex.Workbook Libro;
            Libro = Excel.Workbooks.Add();
            Ex.Worksheet Hoja;
            Hoja = (Ex.Worksheet)Libro.Worksheets.get_Item(1);

            //COLOCAR EL NOMBRE DE LAS COLUMNAS
            Hoja.Cells[1, 1] = "Temp Externa";
            Hoja.Cells[1, 2] = "Temp Interna";
            Hoja.Cells[1, 3] = "Presion  ";
            Hoja.Cells[1, 4] = "Humedad  ";
            Hoja.Cells[1, 5] = "Altitud  ";
            Hoja.Cells[1, 6] = "Longitud ";
            Hoja.Cells[1, 7] = "Latitud  ";
            Hoja.Cells[1, 8] = "Voltaje  ";
            Hoja.Cells[1, 9] = "Cal/ Aire";


            //VACIAR LOS DATOS DE CADA SENSOR
            for (int i = 0; i < ListaTelemetria.Count; i++)
            {
                Hoja.Cells[i + 2, 1] = ListaTelemetria[i].TempExt;
                Hoja.Cells[i + 2, 2] = ListaTelemetria[i].TempInt;
                Hoja.Cells[i + 2, 3] = ListaTelemetria[i].Presion;
                Hoja.Cells[i + 2, 4] = ListaTelemetria[i].Humedad;
                Hoja.Cells[i + 2, 5] = ListaTelemetria[i].Altitud;
                Hoja.Cells[i + 2, 6] = ListaTelemetria[i].Lngitud;
                Hoja.Cells[i + 2, 7] = ListaTelemetria[i].Latitud;
                Hoja.Cells[i + 2, 8] = ListaTelemetria[i].Voltaje;
                Hoja.Cells[i + 2, 9] = ListaTelemetria[i].Calidad;
            }

            //Excel.Visible = true; //QUE SE ABRA EXCEL
            Libro.SaveAs(DireccionExcel);
            Libro.Close();
            Excel.Quit();
        } 


        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CANSAT2019());
        }
    }
}
