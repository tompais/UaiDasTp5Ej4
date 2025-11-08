using DOM;

namespace ABS
{
    /// <summary>
    /// Contrato para servicios de negocio de juegos
    /// Define las operaciones de negocio y validación
    /// </summary>
    public interface IJuegoService
    {
        /// <summary>
        /// Obtiene todos los juegos disponibles
        /// </summary>
        IEnumerable<Juego> ObtenerTodosLosJuegos();

        /// <summary>
        /// Obtiene un juego específico por su identificador
        /// </summary>
        Juego? ObtenerJuegoPorId(int id);

        /// <summary>
        /// Crea un nuevo juego con validaciones de negocio
        /// </summary>
        void CrearJuego(Juego juego);

        /// <summary>
        /// Actualiza un juego existente con validaciones
        /// </summary>
        void ActualizarJuego(Juego juego);

        /// <summary>
        /// Elimina un juego por su identificador
        /// </summary>
        void EliminarJuego(int id);

        /// <summary>
        /// Valida las reglas de negocio de un juego
        /// </summary>
        /// <param name="juego">Juego a validar</param>
        /// <param name="mensajeError">Mensaje de error si la validación falla</param>
        /// <returns>True si es válido, False en caso contrario</returns>
        bool ValidarJuego(Juego juego, out string mensajeError);
    }
}
