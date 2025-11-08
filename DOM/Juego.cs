namespace DOM
{
    /// <summary>
    /// Representa un juego completo de Preguntados
    /// Entidad raíz del dominio que contiene toda la información del juego
    /// Record para permitir inmutabilidad y with expressions
    /// </summary>
    public record Juego
    {
        public int Id { get; init; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; init; } = DateTime.Now;
        public int CantidadPreguntas { get; set; }
        public List<Pregunta> Preguntas { get; set; } = [];
    }
}
