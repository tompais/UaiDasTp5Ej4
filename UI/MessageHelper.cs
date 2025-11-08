namespace UI
{
    /// <summary>
    /// Helper para mostrar mensajes al usuario de forma consistente
    /// Centraliza la interacción con MessageBox
    /// </summary>
    public static class MessageHelper
    {
        /// <summary>
        /// Muestra un mensaje de éxito al usuario
        /// </summary>
        public static void MostrarExito(string mensaje)
        {
            MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Muestra un mensaje de error al usuario
        /// </summary>
        public static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Muestra un mensaje de advertencia al usuario
        /// </summary>
        public static void MostrarAdvertencia(string mensaje)
        {
            MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Solicita confirmación al usuario
        /// </summary>
        /// <returns>True si el usuario confirma, False en caso contrario</returns>
        public static bool Confirmar(string mensaje)
        {
            var resultado = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return resultado == DialogResult.Yes;
        }
    }
}
