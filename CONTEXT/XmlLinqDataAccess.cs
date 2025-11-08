using ABS;
using DOM;
using System.Xml.Linq;

namespace CONTEXT
{
    /// <summary>
    /// Implementación de acceso a datos XML usando LINQ to XML (XDocument)
    /// Proporciona operaciones de lectura y escritura de juegos en formato XML
    /// </summary>
    public class XmlLinqDataAccess : IXmlDataAccess
    {
        public void GuardarJuegos(IEnumerable<Juego> juegos, string rutaArchivo)
        {
            var xDoc = new XDocument(
                  new XDeclaration("1.0", "utf-8", "yes"),
             new XElement("Juegos",
                juegos.Select(j => new XElement("Juego",
              new XAttribute("Id", j.Id),
            new XElement("Nombre", j.Nombre),
             new XElement("Descripcion", j.Descripcion),
           new XElement("FechaCreacion", j.FechaCreacion.ToString("yyyy-MM-dd")),
             new XElement("CantidadPreguntas", j.CantidadPreguntas),
             new XElement("Preguntas",
          j.Preguntas.Select(p => new XElement("Pregunta",
                   new XAttribute("Id", p.Id),
            new XElement("Texto", p.Texto),
             new XElement("Categoria", p.Categoria.ToString()),
                      new XElement("Dificultad", p.Dificultad),
            new XElement("RespuestaCorrectaId", p.RespuestaCorrectaId),
          new XElement("Opciones",
                  p.Opciones.Select(o => new XElement("Opcion",
                   new XAttribute("Id", o.Id),
            new XElement("Texto", o.Texto)
              ))
              )
             ))
                   )
         ))
           )
             );

            xDoc.Save(rutaArchivo);
        }

        public IEnumerable<Juego> CargarJuegos(string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
            {
                return [];
            }

            var xDoc = XDocument.Load(rutaArchivo);

            return xDoc.Root?.Elements("Juego").Select(juegoElement => new Juego
            {
                Id = int.Parse(juegoElement.Attribute("Id")?.Value ?? "0"),
                Nombre = juegoElement.Element("Nombre")?.Value ?? string.Empty,
                Descripcion = juegoElement.Element("Descripcion")?.Value ?? string.Empty,
                FechaCreacion = DateTime.Parse(juegoElement.Element("FechaCreacion")?.Value ?? DateTime.Now.ToString()),
                CantidadPreguntas = int.Parse(juegoElement.Element("CantidadPreguntas")?.Value ?? "0"),
                Preguntas = juegoElement.Element("Preguntas")?.Elements("Pregunta").Select(preguntaElement => new Pregunta
                {
                    Id = int.Parse(preguntaElement.Attribute("Id")?.Value ?? "0"),
                    Texto = preguntaElement.Element("Texto")?.Value ?? string.Empty,
                    Categoria = Enum.TryParse<Categoria>(preguntaElement.Element("Categoria")?.Value, out var cat)
                    ? cat
                     : Categoria.Historia,
                    Dificultad = preguntaElement.Element("Dificultad")?.Value ?? string.Empty,
                    RespuestaCorrectaId = int.Parse(preguntaElement.Element("RespuestaCorrectaId")?.Value ?? "0"),
                    Opciones = preguntaElement.Element("Opciones")?.Elements("Opcion").Select(opcionElement => new Opcion
                    {
                        Id = int.Parse(opcionElement.Attribute("Id")?.Value ?? "0"),
                        Texto = opcionElement.Element("Texto")?.Value ?? string.Empty
                    }).ToList() ?? []
                }).ToList() ?? []
            }).ToList() ?? [];
        }
    }
}
