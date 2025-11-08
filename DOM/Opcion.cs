namespace DOM
{
    /// <summary>
    /// Representa una opción de respuesta para una pregunta
    /// Record para permitir inmutabilidad y with expressions
    /// </summary>
    public record Opcion
    {
        public int Id { get; init; }
        public string Texto { get; set; } = string.Empty;
    }
}
