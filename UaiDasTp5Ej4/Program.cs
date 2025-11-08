using ABS;
using APP;
using Microsoft.Extensions.DependencyInjection;
using UaiDasTp5Ej4.UI;

namespace UaiDasTp5Ej4
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configurar DPI y fuentes
            ApplicationConfiguration.Initialize();

            // Configurar ruta del archivo XML
            var rutaArchivo = Path.Combine(
                 Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
              "PreguntadosJuegos.xml"
                );

            // Configurar Dependency Injection
            var serviceProvider = DependencyInjection.ConfigurarServicios(rutaArchivo);

            // Obtener el servicio de juegos
            var juegoService = serviceProvider.GetRequiredService<IJuegoService>();

            // Iniciar la aplicación con el formulario principal
            Application.Run(new FrmJuegoABM(juegoService));
        }
    }
}