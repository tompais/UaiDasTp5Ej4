using ABS;
using DOM;
using UI;

namespace UaiDasTp5Ej4.UI
{
    /// <summary>
    /// Formulario para gestionar el ABM de Juegos
    /// </summary>
    public partial class FrmJuegoABM(IJuegoService juegoService) : Form
    {
        private Juego? _juegoSeleccionado;

        private void ConfigurarFormulario() =>
       Load += FrmJuegoABM_Load;

        private void FrmJuegoABM_Load(object? sender, EventArgs e) =>
  CargarJuegos();

        private void CargarJuegos()
        {
            try
            {
                var juegos = juegoService.ObtenerTodosLosJuegos().ToList();
                lstJuegos.DataSource = juegos;
                lstJuegos.DisplayMember = "Nombre";
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageHelper.MostrarError($"Error al cargar juegos: {ex.Message}");
            }
        }

        private void LstJuegos_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstJuegos.SelectedItem is Juego juego)
            {
                _juegoSeleccionado = juego;
                MostrarDatosJuego(juego);
            }
        }

        private void MostrarDatosJuego(Juego juego)
        {
            txtId.Text = juego.Id.ToString();
            txtNombre.Text = juego.Nombre;
            txtDescripcion.Text = juego.Descripcion;
            txtCantidadPreguntas.Text = juego.CantidadPreguntas.ToString();

            lstPreguntas.DataSource = juego.Preguntas.ToList();
            lstPreguntas.DisplayMember = "Texto";
        }

        private void LimpiarFormulario()
        {
            _juegoSeleccionado = null;
            txtId.Text = "0";
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtCantidadPreguntas.Text = "0";
            lstPreguntas.DataSource = null;
        }

        private void BtnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                var idActual = int.Parse(txtId.Text);

                if (idActual == 0)
                {
                    // Crear nuevo juego - FechaCreacion se establece automáticamente
                    var nuevoJuego = new Juego
                    {
                        Id = 0,
                        Nombre = txtNombre.Text.Trim(),
                        Descripcion = txtDescripcion.Text.Trim(),
                        CantidadPreguntas = int.Parse(txtCantidadPreguntas.Text),
                        Preguntas = []
                    };

                    juegoService.CrearJuego(nuevoJuego);
                    MessageHelper.MostrarExito("Juego creado exitosamente");
                }
                else
                {
                    // Actualizar juego existente - mantener fecha original
                    if (_juegoSeleccionado == null)
                    {
                        MessageHelper.MostrarError("No hay un juego seleccionado");
                        return;
                    }

                    var juegoActualizado = _juegoSeleccionado with
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Descripcion = txtDescripcion.Text.Trim(),
                        CantidadPreguntas = int.Parse(txtCantidadPreguntas.Text)
                        // FechaCreacion NO se incluye - mantiene el valor original (init-only)
                    };

                    juegoService.ActualizarJuego(juegoActualizado);
                    MessageHelper.MostrarExito("Juego actualizado exitosamente");
                }

                CargarJuegos();
            }
            catch (Exception ex)
            {
                MessageHelper.MostrarError($"Error al guardar: {ex.Message}");
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (_juegoSeleccionado == null)
            {
                MessageHelper.MostrarAdvertencia("Debe seleccionar un juego");
                return;
            }

            if (!MessageHelper.Confirmar($"¿Está seguro de eliminar el juego '{_juegoSeleccionado.Nombre}'?"))
            {
                return;
            }

            try
            {
                juegoService.EliminarJuego(_juegoSeleccionado.Id);
                MessageHelper.MostrarExito("Juego eliminado exitosamente");
                CargarJuegos();
            }
            catch (Exception ex)
            {
                MessageHelper.MostrarError($"Error al eliminar: {ex.Message}");
            }
        }

        private void BtnAgregarPregunta_Click(object? sender, EventArgs e)
        {
            if (_juegoSeleccionado == null)
            {
                MessageHelper.MostrarAdvertencia("Debe seleccionar o guardar un juego primero");
                return;
            }

            var frmPregunta = new FrmPreguntaDetalle();
            if (frmPregunta.ShowDialog() == DialogResult.OK && frmPregunta.Pregunta != null)
            {
                // Crear una nueva lista con la pregunta agregada
                var nuevasPreguntas = _juegoSeleccionado.Preguntas.ToList();
                nuevasPreguntas.Add(frmPregunta.Pregunta);

                // Actualizar el juego seleccionado (inmutable)
                _juegoSeleccionado = _juegoSeleccionado with
                {
                    Preguntas = nuevasPreguntas,
                    CantidadPreguntas = nuevasPreguntas.Count
                };

                lstPreguntas.DataSource = null;
                lstPreguntas.DataSource = _juegoSeleccionado.Preguntas.ToList();
                lstPreguntas.DisplayMember = "Texto";

                txtCantidadPreguntas.Text = _juegoSeleccionado.CantidadPreguntas.ToString();
            }
        }
    }
}
