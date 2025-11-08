using DOM;

namespace UI
{
    /// <summary>
    /// Extensiones para convertir entidades del dominio a strings para mostrar en controles
    /// Proporciona representaciones legibles de las entidades
    /// </summary>
    public static class DisplayExtensions
    {
        /// <summary>
        /// Convierte un juego a string para mostrar en ListBox
        /// </summary>
        public static string ToDisplayString(this Juego juego)
        {
            return $"[{juego.Id}] {juego.Nombre} - {juego.CantidadPreguntas} preguntas";
        }

        /// <summary>
        /// Convierte una pregunta a string para mostrar en ListBox
        /// </summary>
        public static string ToDisplayString(this Pregunta pregunta)
        {
            return $"[{pregunta.Id}] {pregunta.Texto} - {pregunta.Categoria}";
        }

        /// <summary>
        /// Convierte una opción a string para mostrar en ListBox
        /// </summary>
        public static string ToDisplayString(this Opcion opcion)
        {
            return $"[{opcion.Id}] {opcion.Texto}";
        }
    }
}
