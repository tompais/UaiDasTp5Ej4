using ABS;
using DOM;

namespace SERV
{
    /// <summary>
    /// Servicio de negocio para gestionar juegos
    /// Implementa validaciones y lógica de negocio
    /// Actúa como intermediario entre la capa de presentación y el repositorio
    /// </summary>
    public class JuegoService(IJuegoRepository juegoRepository) : IJuegoService
    {
        public IEnumerable<Juego> ObtenerTodosLosJuegos() =>
            juegoRepository.ObtenerTodos();

        public Juego? ObtenerJuegoPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));
            }

            return juegoRepository.ObtenerPorId(id);
        }

        public void CrearJuego(Juego juego)
        {
            if (!ValidarJuego(juego, out string mensajeError))
            {
                throw new InvalidOperationException(mensajeError);
            }

            juegoRepository.Agregar(juego);
        }

        public void ActualizarJuego(Juego juego)
        {
            if (!ValidarJuego(juego, out string mensajeError))
            {
                throw new InvalidOperationException(mensajeError);
            }

            juegoRepository.Actualizar(juego);
        }

        public void EliminarJuego(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));
            }

            juegoRepository.Eliminar(id);
        }

        public bool ValidarJuego(Juego juego, out string mensajeError)
        {
            mensajeError = string.Empty;

            if (!ValidarDatosBasicos(juego, out mensajeError))
            {
                return false;
            }

            if (!ValidarPreguntas(juego, out mensajeError))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida los datos básicos del juego usando LINQ
        /// </summary>
        private static bool ValidarDatosBasicos(Juego juego, out string mensajeError)
        {
            mensajeError = string.Empty;

            if (juego == null)
            {
                mensajeError = "El juego no puede ser nulo";
                return false;
            }

            if (string.IsNullOrWhiteSpace(juego.Nombre))
            {
                mensajeError = "El nombre del juego es requerido";
                return false;
            }

            if (juego.Nombre.Length < 3)
            {
                mensajeError = "El nombre del juego debe tener al menos 3 caracteres";
                return false;
            }

            if (string.IsNullOrWhiteSpace(juego.Descripcion))
            {
                mensajeError = "La descripción del juego es requerida";
                return false;
            }

            if (juego.FechaCreacion == default)
            {
                mensajeError = "La fecha de creación es requerida";
                return false;
            }

            if (juego.CantidadPreguntas < 0)
            {
                mensajeError = "La cantidad de preguntas no puede ser negativa";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida las preguntas del juego usando LINQ
        /// </summary>
        private static bool ValidarPreguntas(Juego juego, out string mensajeError)
        {
            mensajeError = string.Empty;

            if (juego.Preguntas == null || !juego.Preguntas.Any())
            {
                return true;
            }

            // Validar textos de preguntas
            var preguntaSinTexto = juego.Preguntas.FirstOrDefault(p => string.IsNullOrWhiteSpace(p.Texto));
            if (preguntaSinTexto != null)
            {
                mensajeError = "Todas las preguntas deben tener texto";
                return false;
            }

            // Validar dificultades
            var preguntaSinDificultad = juego.Preguntas.FirstOrDefault(p => string.IsNullOrWhiteSpace(p.Dificultad));
            if (preguntaSinDificultad != null)
            {
                mensajeError = "Todas las preguntas deben tener una dificultad";
                return false;
            }

            // Validar cantidad de opciones
            var preguntaSinOpciones = juego.Preguntas.FirstOrDefault(p => p.Opciones == null || p.Opciones.Count < 2);
            if (preguntaSinOpciones != null)
            {
                mensajeError = "Cada pregunta debe tener al menos 2 opciones";
                return false;
            }

            // Validar respuesta correcta
            var preguntaConRespuestaInvalida = juego.Preguntas
              .FirstOrDefault(p => !p.Opciones.Any(o => o.Id == p.RespuestaCorrectaId));
            if (preguntaConRespuestaInvalida != null)
            {
                mensajeError = "La respuesta correcta debe corresponder a una opción válida";
                return false;
            }

            // Validar textos de opciones
            var preguntaConOpcionSinTexto = juego.Preguntas
      .FirstOrDefault(p => p.Opciones.Any(o => string.IsNullOrWhiteSpace(o.Texto)));
            if (preguntaConOpcionSinTexto != null)
            {
                mensajeError = "Todas las opciones deben tener texto";
                return false;
            }

            return true;
        }
    }
}
