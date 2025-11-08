using DOM;

namespace ABS
{
    /// <summary>
    /// Contrato para operaciones de acceso a datos XML
    /// Abstrae las operaciones de lectura y escritura de archivos XML
    /// </summary>
    public interface IXmlDataAccess
    {
        /// <summary>
        /// Guarda una colección de juegos en un archivo XML
        /// </summary>
        /// <param name="juegos">Colección de juegos a guardar</param>
        /// <param name="rutaArchivo">Ruta del archivo XML</param>
        void GuardarJuegos(IEnumerable<Juego> juegos, string rutaArchivo);

        /// <summary>
        /// Carga juegos desde un archivo XML
        /// </summary>
        /// <param name="rutaArchivo">Ruta del archivo XML</param>
        /// <returns>Colección de juegos cargados</returns>
        IEnumerable<Juego> CargarJuegos(string rutaArchivo);
    }
}
