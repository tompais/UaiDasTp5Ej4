namespace DOM
{
    /// <summary>
    /// Representa una pregunta del juego Preguntados
    /// Contiene el texto, categoría, dificultad y opciones de respuesta
    /// Record para permitir inmutabilidad y with expressions
    /// </summary>
    public record Pregunta
    {
        public int Id { get; init; }
        public string Texto { get; set; } = string.Empty;
        public Categoria Categoria { get; set; }
        public string Dificultad { get; set; } = string.Empty;
        public List<Opcion> Opciones { get; set; } = [];
        public int RespuestaCorrectaId { get; set; }
    }
}
