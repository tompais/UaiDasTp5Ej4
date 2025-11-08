using ABS;
using DOM;

namespace REPO
{
    /// <summary>
    /// Repositorio para gestionar juegos en XML
    /// Implementa el patrón Repository para abstraer el acceso a datos
    /// Carga los datos bajo demanda para mejor gestión de memoria
    /// </summary>
    public class JuegoRepository(IXmlDataAccess xmlDataAccess, string rutaArchivo) : IJuegoRepository
    {
        /// <summary>
        /// Obtiene todos los juegos del archivo XML
        /// </summary>
        public IEnumerable<Juego> ObtenerTodos() =>
            xmlDataAccess.CargarJuegos(rutaArchivo);

        /// <summary>
        /// Obtiene un juego por su identificador
        /// </summary>
        public Juego? ObtenerPorId(int id) =>
            ObtenerTodos().FirstOrDefault(j => j.Id == id);

        /// <summary>
        /// Agrega un nuevo juego al repositorio
        /// Los IDs se generan de forma autoincremental
        /// </summary>
        public void Agregar(Juego juego)
        {
            var juegos = ObtenerTodos().ToList();

            var nuevoJuego = juego with
            {
                Id = GenerarNuevoId(juegos),
                Preguntas = AsignarIdsPreguntasYOpciones(juego.Preguntas, juegos)
            };

            if (juegos.Any(j => j.Id == nuevoJuego.Id))
            {
                throw new InvalidOperationException($"Ya existe un juego con el ID {nuevoJuego.Id}");
            }

            juegos.Add(nuevoJuego);
            xmlDataAccess.GuardarJuegos(juegos, rutaArchivo);
        }

        /// <summary>
        /// Actualiza un juego existente
        /// </summary>
        public void Actualizar(Juego juego)
        {
            var juegos = ObtenerTodos().ToList();
            var juegoExistente = juegos.FirstOrDefault(j => j.Id == juego.Id) ?? throw new InvalidOperationException($"No existe un juego con el ID {juego.Id}");
            var juegoActualizado = juego with
            {
                Preguntas = AsignarIdsPreguntasYOpciones(juego.Preguntas, juegos)
            };

            var index = juegos.IndexOf(juegoExistente);
            juegos[index] = juegoActualizado;

            xmlDataAccess.GuardarJuegos(juegos, rutaArchivo);
        }

        /// <summary>
        /// Elimina un juego por su identificador
        /// </summary>
        public void Eliminar(int id)
        {
            var juegos = ObtenerTodos().ToList();
            var juego = juegos.FirstOrDefault(j => j.Id == id) ?? throw new InvalidOperationException($"No existe un juego con el ID {id}");
            juegos.Remove(juego);
            xmlDataAccess.GuardarJuegos(juegos, rutaArchivo);
        }

        /// <summary>
        /// Persiste los cambios en el archivo XML
        /// Obsoleto: Ahora cada operación persiste automáticamente
        /// </summary>
        public void Guardar()
        {
            // Método mantenido por compatibilidad con la interfaz
            // Las operaciones ya persisten automáticamente
        }

        /// <summary>
        /// Genera un nuevo ID autoincremental basado en los juegos existentes
        /// </summary>
        private static int GenerarNuevoId(List<Juego> juegos) =>
            juegos.Count != 0 ? juegos.Max(j => j.Id) + 1 : 1;

        /// <summary>
        /// Asigna IDs autoincrementales a preguntas y opciones que no tengan
        /// Usa LINQ para procesamiento funcional
        /// </summary>
        private static List<Pregunta> AsignarIdsPreguntasYOpciones(List<Pregunta> preguntas, List<Juego> juegosExistentes)
        {
            if (preguntas == null || preguntas.Count == 0)
            {
                return preguntas ?? [];
            }

            // Obtener el máximo ID de preguntas existente en todos los juegos
            var maxPreguntaId = juegosExistentes
        .SelectMany(j => j.Preguntas)
           .Select(p => p.Id)
              .DefaultIfEmpty(0)
           .Max();

            return [.. preguntas.Select((pregunta, index) =>
                {
                    var nuevaPregunta = pregunta.Id == 0
              ? pregunta with { Id = maxPreguntaId + index + 1 }
             : pregunta;

                    // Asignar IDs a opciones si es necesario
                    if (nuevaPregunta.Opciones != null && nuevaPregunta.Opciones.Count != 0)
                    {
                        var opcionesConId = nuevaPregunta.Opciones
                         .Select((opcion, opcionIndex) => opcion.Id == 0
                      ? opcion with { Id = opcionIndex + 1 }
                           : opcion)
                       .ToList();

                        return nuevaPregunta with { Opciones = opcionesConId };
                    }

                    return nuevaPregunta;
                })];
        }
    }
}
