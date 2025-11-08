using ABS;
using CONTEXT;
using Microsoft.Extensions.DependencyInjection;
using REPO;
using SERV;

namespace APP
{
    /// <summary>
    /// Configuración de Dependency Injection del sistema
    /// Centraliza el registro y configuración de todos los servicios
    /// Sigue el principio de Inversión de Dependencias (SOLID)
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Configura los servicios del sistema
        /// </summary>
        /// <param name="rutaArchivoXml">Ruta del archivo XML de persistencia</param>
        /// <returns>ServiceProvider configurado</returns>
        public static IServiceProvider ConfigurarServicios(string rutaArchivoXml)
        {
            var services = new ServiceCollection();

            // Registrar acceso a datos XML con LINQ to XML
            services.AddSingleton<IXmlDataAccess, XmlLinqDataAccess>();

            // Registrar repositorio con la ruta del archivo
            services.AddSingleton<IJuegoRepository>(provider =>
            {
                var xmlDataAccess = provider.GetRequiredService<IXmlDataAccess>();
                return new JuegoRepository(xmlDataAccess, rutaArchivoXml);
            });

            // Registrar servicios de negocio
            services.AddSingleton<IJuegoService, JuegoService>();

            return services.BuildServiceProvider();
        }
    }
}
