using DOM;

namespace ABS
{
    /// <summary>
    /// Contrato para operaciones de repositorio de juegos
    /// Define las operaciones CRUD para la persistencia de juegos
    /// </summary>
    public interface IJuegoRepository
    {
        /// <summary>
        /// Obtiene todos los juegos almacenados
        /// </summary>
        IEnumerable<Juego> ObtenerTodos();

        /// <summary>
        /// Obtiene un juego por su identificador
        /// </summary>
        Juego? ObtenerPorId(int id);

        /// <summary>
        /// Agrega un nuevo juego al repositorio
        /// </summary>
        void Agregar(Juego juego);

        /// <summary>
        /// Actualiza un juego existente
        /// </summary>
        void Actualizar(Juego juego);

        /// <summary>
        /// Elimina un juego por su identificador
        /// </summary>
        void Eliminar(int id);

        /// <summary>
        /// Persiste los cambios en el almacenamiento
        /// </summary>
        void Guardar();
    }
}
