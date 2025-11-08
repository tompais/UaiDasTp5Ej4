using DOM;
using UI;

namespace UaiDasTp5Ej4.UI
{
    /// <summary>
    /// Formulario para crear/editar una pregunta
    /// </summary>
    public partial class FrmPreguntaDetalle : Form
    {
        public Pregunta? Pregunta { get; private set; }
        private readonly List<Opcion> _opciones = [];

        public FrmPreguntaDetalle()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario() =>
      Load += FrmPreguntaDetalle_Load;

        private void FrmPreguntaDetalle_Load(object? sender, EventArgs e)
        {
            // Cargar enum Categoria en el ComboBox
            cboCategoria.DataSource = Enum.GetValues(typeof(Categoria));
            cboDificultad.Items.AddRange(["Fácil", "Media", "Difícil"]);
        }

        private void BtnAgregarOpcion_Click(object? sender, EventArgs e)
        {
            var textoOpcion = txtOpcion.Text.Trim();

            if (string.IsNullOrWhiteSpace(textoOpcion))
            {
                MessageHelper.MostrarAdvertencia("Debe ingresar el texto de la opción");
                return;
            }

            var opcion = new Opcion
            {
                Id = _opciones.Count != 0 ? _opciones.Max(o => o.Id) + 1 : 1,
                Texto = textoOpcion
            };

            _opciones.Add(opcion);
            ActualizarListaOpciones();
            txtOpcion.Clear();
            txtOpcion.Focus();
        }

        private void BtnQuitarOpcion_Click(object? sender, EventArgs e)
        {
            if (lstOpciones.SelectedItem is Opcion opcion)
            {
                _opciones.Remove(opcion);
                ActualizarListaOpciones();
            }
            else
            {
                MessageHelper.MostrarAdvertencia("Debe seleccionar una opción");
            }
        }

        private void ActualizarListaOpciones()
        {
            lstOpciones.DataSource = null;
            lstOpciones.DataSource = _opciones.ToList();
            lstOpciones.DisplayMember = "Texto";

            cboRespuestaCorrecta.DataSource = null;
            cboRespuestaCorrecta.DataSource = _opciones.ToList();
            cboRespuestaCorrecta.DisplayMember = "Texto";
            cboRespuestaCorrecta.ValueMember = "Id";
        }

        private void BtnAceptar_Click(object? sender, EventArgs e)
        {
            if (!ValidarPregunta())
            {
                return;
            }

            Pregunta = new Pregunta
            {
                Id = 0,
                Texto = txtTextoPregunta.Text.Trim(),
                Categoria = (Categoria)(cboCategoria.SelectedItem ?? Categoria.Historia),
                Dificultad = cboDificultad.SelectedItem?.ToString() ?? string.Empty,
                Opciones = [.. _opciones],
                RespuestaCorrectaId = cboRespuestaCorrecta.SelectedValue != null
    ? (int)cboRespuestaCorrecta.SelectedValue
  : 0
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidarPregunta()
        {
            if (string.IsNullOrWhiteSpace(txtTextoPregunta.Text))
            {
                MessageHelper.MostrarAdvertencia("Debe ingresar el texto de la pregunta");
                return false;
            }

            if (cboCategoria.SelectedItem == null)
            {
                MessageHelper.MostrarAdvertencia("Debe seleccionar una categoría");
                return false;
            }

            if (cboDificultad.SelectedItem == null)
            {
                MessageHelper.MostrarAdvertencia("Debe seleccionar una dificultad");
                return false;
            }

            if (_opciones.Count < 2)
            {
                MessageHelper.MostrarAdvertencia("Debe agregar al menos 2 opciones");
                return false;
            }

            if (cboRespuestaCorrecta.SelectedValue == null)
            {
                MessageHelper.MostrarAdvertencia("Debe seleccionar la respuesta correcta");
                return false;
            }

            return true;
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
