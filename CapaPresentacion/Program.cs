using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

//namespace CapaPresentacion
//{
//    static class Program
//    {
//        /// <summary>
//        /// Punto de entrada principal para la aplicación.
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-PE");
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);
//            Application.Run(new Login());

//        }
//    }
//}

namespace CapaPresentacion
{
    static class Program
    {
        // Define un mutex global con un identificador único
        //static Mutex mutex = new Mutex(true, "{UNIQUE-APP-ID-CapaPresentacion}");
        static Mutex mutex = new Mutex(true, "LPNEWSTORE49-v1.0.0");


        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configuración de la cultura para la aplicación
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-PE");

            // Verifica si ya existe otra instancia de la aplicación
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                // Inicia la aplicación si no hay otra instancia
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Login());

                // Libera el mutex cuando la aplicación se cierra
                mutex.ReleaseMutex();
            }
            else
            {
                // Muestra un mensaje de advertencia si ya hay una instancia en ejecución
                MessageBox.Show("La aplicación ya está en ejecución.", "Instancia duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
