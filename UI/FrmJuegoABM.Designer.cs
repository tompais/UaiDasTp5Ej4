namespace UaiDasTp5Ej4.UI
{
    partial class FrmJuegoABM
 {
        private System.ComponentModel.IContainer components = null;
     
 private ListBox lstJuegos;
    private TextBox txtId;
        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private TextBox txtCantidadPreguntas;
 private ListBox lstPreguntas;
     private Button btnNuevo;
    private Button btnGuardar;
     private Button btnEliminar;
        private Button btnAgregarPregunta;
        private Label lblJuegos;
     private Label lblId;
    private Label lblNombre;
   private Label lblDescripcion;
        private Label lblCantidad;
        private Label lblPreguntas;
        private GroupBox grpDatos;
  private GroupBox grpLista;

        protected override void Dispose(bool disposing)
  {
         if (disposing && (components != null))
          {
     components.Dispose();
            }
base.Dispose(disposing);
      }

      private void InitializeComponent()
        {
       this.lstJuegos = new ListBox();
   this.txtId = new TextBox();
            this.txtNombre = new TextBox();
       this.txtDescripcion = new TextBox();
            this.txtCantidadPreguntas = new TextBox();
     this.lstPreguntas = new ListBox();
    this.btnNuevo = new Button();
            this.btnGuardar = new Button();
this.btnEliminar = new Button();
  this.btnAgregarPregunta = new Button();
   this.lblJuegos = new Label();
     this.lblId = new Label();
          this.lblNombre = new Label();
    this.lblDescripcion = new Label();
   this.lblCantidad = new Label();
        this.lblPreguntas = new Label();
         this.grpDatos = new GroupBox();
            this.grpLista = new GroupBox();
  this.grpDatos.SuspendLayout();
      this.grpLista.SuspendLayout();
    this.SuspendLayout();

         // grpLista
this.grpLista.Controls.Add(this.lblJuegos);
 this.grpLista.Controls.Add(this.lstJuegos);
      this.grpLista.Location = new Point(12, 12);
    this.grpLista.Name = "grpLista";
    this.grpLista.Size = new Size(250, 520);
 this.grpLista.TabIndex = 0;
            this.grpLista.TabStop = false;
            this.grpLista.Text = "Lista de Juegos";

          // lblJuegos
   this.lblJuegos.AutoSize = true;
   this.lblJuegos.Location = new Point(6, 19);
this.lblJuegos.Name = "lblJuegos";
     this.lblJuegos.Size = new Size(50, 15);
        this.lblJuegos.TabIndex = 0;
          this.lblJuegos.Text = "Juegos:";

   // lstJuegos
     this.lstJuegos.FormattingEnabled = true;
            this.lstJuegos.ItemHeight = 15;
   this.lstJuegos.Location = new Point(6, 37);
            this.lstJuegos.Name = "lstJuegos";
    this.lstJuegos.Size = new Size(238, 469);
   this.lstJuegos.TabIndex = 1;
    this.lstJuegos.SelectedIndexChanged += LstJuegos_SelectedIndexChanged;

   // grpDatos
   this.grpDatos.Controls.Add(this.lblId);
          this.grpDatos.Controls.Add(this.txtId);
            this.grpDatos.Controls.Add(this.lblNombre);
            this.grpDatos.Controls.Add(this.txtNombre);
       this.grpDatos.Controls.Add(this.lblDescripcion);
        this.grpDatos.Controls.Add(this.txtDescripcion);
       this.grpDatos.Controls.Add(this.lblCantidad);
    this.grpDatos.Controls.Add(this.txtCantidadPreguntas);
         this.grpDatos.Controls.Add(this.lblPreguntas);
            this.grpDatos.Controls.Add(this.lstPreguntas);
 this.grpDatos.Controls.Add(this.btnAgregarPregunta);
 this.grpDatos.Location = new Point(268, 12);
     this.grpDatos.Name = "grpDatos";
       this.grpDatos.Size = new Size(504, 470);
    this.grpDatos.TabIndex = 1;
            this.grpDatos.TabStop = false;
            this.grpDatos.Text = "Datos del Juego";

     // lblId
        this.lblId.AutoSize = true;
        this.lblId.Location = new Point(6, 25);
   this.lblId.Name = "lblId";
  this.lblId.Size = new Size(21, 15);
    this.lblId.TabIndex = 0;
  this.lblId.Text = "ID:";

     // txtId
        this.txtId.Location = new Point(120, 22);
  this.txtId.Name = "txtId";
    this.txtId.ReadOnly = true;
       this.txtId.Size = new Size(100, 23);
  this.txtId.TabIndex = 1;
     this.txtId.Text = "0";

  // lblNombre
            this.lblNombre.AutoSize = true;
   this.lblNombre.Location = new Point(6, 54);
      this.lblNombre.Name = "lblNombre";
     this.lblNombre.Size = new Size(54, 15);
     this.lblNombre.TabIndex = 2;
    this.lblNombre.Text = "Nombre:";

   // txtNombre
 this.txtNombre.Location = new Point(120, 51);
      this.txtNombre.Name = "txtNombre";
  this.txtNombre.Size = new Size(378, 23);
       this.txtNombre.TabIndex = 3;

      // lblDescripcion
 this.lblDescripcion.AutoSize = true;
    this.lblDescripcion.Location = new Point(6, 83);
     this.lblDescripcion.Name = "lblDescripcion";
   this.lblDescripcion.Size = new Size(72, 15);
  this.lblDescripcion.TabIndex = 4;
        this.lblDescripcion.Text = "Descripción:";

 // txtDescripcion
      this.txtDescripcion.Location = new Point(120, 80);
    this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
      this.txtDescripcion.Size = new Size(378, 60);
 this.txtDescripcion.TabIndex = 5;

    // lblCantidad
  this.lblCantidad.AutoSize = true;
  this.lblCantidad.Location = new Point(6, 149);
        this.lblCantidad.Name = "lblCantidad";
   this.lblCantidad.Size = new Size(108, 15);
        this.lblCantidad.TabIndex = 6;
    this.lblCantidad.Text = "Cant. Preguntas:";

          // txtCantidadPreguntas
            this.txtCantidadPreguntas.Location = new Point(120, 145);
            this.txtCantidadPreguntas.Name = "txtCantidadPreguntas";
       this.txtCantidadPreguntas.ReadOnly = true;
         this.txtCantidadPreguntas.Size = new Size(100, 23);
          this.txtCantidadPreguntas.TabIndex = 7;
            this.txtCantidadPreguntas.Text = "0";

         // lblPreguntas
      this.lblPreguntas.AutoSize = true;
     this.lblPreguntas.Location = new Point(6, 178);
     this.lblPreguntas.Name = "lblPreguntas";
   this.lblPreguntas.Size = new Size(66, 15);
    this.lblPreguntas.TabIndex = 8;
  this.lblPreguntas.Text = "Preguntas:";

     // lstPreguntas
      this.lstPreguntas.FormattingEnabled = true;
 this.lstPreguntas.ItemHeight = 15;
   this.lstPreguntas.Location = new Point(6, 196);
      this.lstPreguntas.Name = "lstPreguntas";
 this.lstPreguntas.Size = new Size(492, 229);
   this.lstPreguntas.TabIndex = 9;

   // btnAgregarPregunta
            this.btnAgregarPregunta.Location = new Point(6, 430);
         this.btnAgregarPregunta.Name = "btnAgregarPregunta";
            this.btnAgregarPregunta.Size = new Size(150, 30);
       this.btnAgregarPregunta.TabIndex = 10;
    this.btnAgregarPregunta.Text = "Agregar Pregunta";
   this.btnAgregarPregunta.UseVisualStyleBackColor = true;
  this.btnAgregarPregunta.Click += BtnAgregarPregunta_Click;

            // btnNuevo
            this.btnNuevo.Location = new Point(268, 488);
       this.btnNuevo.Name = "btnNuevo";
        this.btnNuevo.Size = new Size(120, 44);
    this.btnNuevo.TabIndex = 2;
     this.btnNuevo.Text = "Nuevo";
  this.btnNuevo.UseVisualStyleBackColor = true;
          this.btnNuevo.Click += BtnNuevo_Click;

       // btnGuardar
   this.btnGuardar.Location = new Point(394, 488);
    this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new Size(120, 44);
    this.btnGuardar.TabIndex = 3;
   this.btnGuardar.Text = "Guardar";
       this.btnGuardar.UseVisualStyleBackColor = true;
       this.btnGuardar.Click += BtnGuardar_Click;

      // btnEliminar
          this.btnEliminar.Location = new Point(520, 488);
          this.btnEliminar.Name = "btnEliminar";
    this.btnEliminar.Size = new Size(120, 44);
 this.btnEliminar.TabIndex = 4;
        this.btnEliminar.Text = "Eliminar";
        this.btnEliminar.UseVisualStyleBackColor = true;
   this.btnEliminar.Click += BtnEliminar_Click;

      // FrmJuegoABM
this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
   this.ClientSize = new Size(784, 561);
    this.Controls.Add(this.btnEliminar);
       this.Controls.Add(this.btnGuardar);
    this.Controls.Add(this.btnNuevo);
     this.Controls.Add(this.grpDatos);
     this.Controls.Add(this.grpLista);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
  this.MaximizeBox = false;
     this.Name = "FrmJuegoABM";
            this.StartPosition = FormStartPosition.CenterScreen;
       this.Text = "ABM de Juegos - Preguntados";
      this.grpDatos.ResumeLayout(false);
    this.grpDatos.PerformLayout();
this.grpLista.ResumeLayout(false);
   this.grpLista.PerformLayout();
      this.ResumeLayout(false);

       ConfigurarFormulario();
    }
    }
}
