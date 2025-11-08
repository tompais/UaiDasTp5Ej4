namespace UaiDasTp5Ej4.UI
{
    partial class FrmPreguntaDetalle
    {
  private System.ComponentModel.IContainer components = null;

     private TextBox txtTextoPregunta;
        private ComboBox cboCategoria;
   private ComboBox cboDificultad;
  private TextBox txtOpcion;
        private ListBox lstOpciones;
        private ComboBox cboRespuestaCorrecta;
    private Button btnAgregarOpcion;
    private Button btnQuitarOpcion;
    private Button btnAceptar;
private Button btnCancelar;
 private Label lblTextoPregunta;
        private Label lblCategoria;
  private Label lblDificultad;
        private Label lblOpciones;
     private Label lblOpcion;
        private Label lblRespuestaCorrecta;
        private GroupBox grpOpciones;

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
    this.txtTextoPregunta = new TextBox();
  this.cboCategoria = new ComboBox();
         this.cboDificultad = new ComboBox();
        this.txtOpcion = new TextBox();
    this.lstOpciones = new ListBox();
   this.cboRespuestaCorrecta = new ComboBox();
 this.btnAgregarOpcion = new Button();
     this.btnQuitarOpcion = new Button();
      this.btnAceptar = new Button();
this.btnCancelar = new Button();
    this.lblTextoPregunta = new Label();
          this.lblCategoria = new Label();
  this.lblDificultad = new Label();
this.lblOpciones = new Label();
     this.lblOpcion = new Label();
  this.lblRespuestaCorrecta = new Label();
  this.grpOpciones = new GroupBox();
     this.grpOpciones.SuspendLayout();
this.SuspendLayout();

      // lblTextoPregunta
 this.lblTextoPregunta.AutoSize = true;
  this.lblTextoPregunta.Location = new Point(12, 15);
    this.lblTextoPregunta.Name = "lblTextoPregunta";
    this.lblTextoPregunta.Size = new Size(111, 15);
  this.lblTextoPregunta.TabIndex = 0;
  this.lblTextoPregunta.Text = "Texto de Pregunta:";

     // txtTextoPregunta
    this.txtTextoPregunta.Location = new Point(12, 33);
this.txtTextoPregunta.Multiline = true;
    this.txtTextoPregunta.Name = "txtTextoPregunta";
       this.txtTextoPregunta.Size = new Size(460, 60);
    this.txtTextoPregunta.TabIndex = 1;

    // lblCategoria
   this.lblCategoria.AutoSize = true;
   this.lblCategoria.Location = new Point(12, 106);
  this.lblCategoria.Name = "lblCategoria";
   this.lblCategoria.Size = new Size(61, 15);
            this.lblCategoria.TabIndex = 2;
      this.lblCategoria.Text = "Categoría:";

    // cboCategoria
 this.cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
  this.cboCategoria.FormattingEnabled = true;
          this.cboCategoria.Location = new Point(12, 124);
this.cboCategoria.Name = "cboCategoria";
        this.cboCategoria.Size = new Size(220, 23);
       this.cboCategoria.TabIndex = 3;

     // lblDificultad
       this.lblDificultad.AutoSize = true;
  this.lblDificultad.Location = new Point(252, 106);
  this.lblDificultad.Name = "lblDificultad";
  this.lblDificultad.Size = new Size(65, 15);
 this.lblDificultad.TabIndex = 4;
            this.lblDificultad.Text = "Dificultad:";

   // cboDificultad
   this.cboDificultad.DropDownStyle = ComboBoxStyle.DropDownList;
 this.cboDificultad.FormattingEnabled = true;
   this.cboDificultad.Location = new Point(252, 124);
     this.cboDificultad.Name = "cboDificultad";
  this.cboDificultad.Size = new Size(220, 23);
         this.cboDificultad.TabIndex = 5;

      // grpOpciones
 this.grpOpciones.Controls.Add(this.lblOpcion);
       this.grpOpciones.Controls.Add(this.txtOpcion);
  this.grpOpciones.Controls.Add(this.btnAgregarOpcion);
 this.grpOpciones.Controls.Add(this.lblOpciones);
      this.grpOpciones.Controls.Add(this.lstOpciones);
       this.grpOpciones.Controls.Add(this.btnQuitarOpcion);
            this.grpOpciones.Location = new Point(12, 163);
            this.grpOpciones.Name = "grpOpciones";
   this.grpOpciones.Size = new Size(460, 240);
   this.grpOpciones.TabIndex = 6;
     this.grpOpciones.TabStop = false;
         this.grpOpciones.Text = "Opciones de Respuesta";

       // lblOpcion
     this.lblOpcion.AutoSize = true;
 this.lblOpcion.Location = new Point(6, 25);
 this.lblOpcion.Name = "lblOpcion";
this.lblOpcion.Size = new Size(92, 15);
 this.lblOpcion.TabIndex = 0;
this.lblOpcion.Text = "Nueva Opción:";

// txtOpcion
 this.txtOpcion.Location = new Point(6, 43);
    this.txtOpcion.Name = "txtOpcion";
    this.txtOpcion.Size = new Size(348, 23);
      this.txtOpcion.TabIndex = 1;

   // btnAgregarOpcion
   this.btnAgregarOpcion.Location = new Point(360, 41);
   this.btnAgregarOpcion.Name = "btnAgregarOpcion";
         this.btnAgregarOpcion.Size = new Size(94, 27);
  this.btnAgregarOpcion.TabIndex = 2;
    this.btnAgregarOpcion.Text = "Agregar";
  this.btnAgregarOpcion.UseVisualStyleBackColor = true;
    this.btnAgregarOpcion.Click += BtnAgregarOpcion_Click;

   // lblOpciones
    this.lblOpciones.AutoSize = true;
  this.lblOpciones.Location = new Point(6, 77);
    this.lblOpciones.Name = "lblOpciones";
this.lblOpciones.Size = new Size(61, 15);
     this.lblOpciones.TabIndex = 3;
   this.lblOpciones.Text = "Opciones:";

   // lstOpciones
    this.lstOpciones.FormattingEnabled = true;
   this.lstOpciones.ItemHeight = 15;
            this.lstOpciones.Location = new Point(6, 95);
     this.lstOpciones.Name = "lstOpciones";
 this.lstOpciones.Size = new Size(348, 139);
        this.lstOpciones.TabIndex = 4;

 // btnQuitarOpcion
   this.btnQuitarOpcion.Location = new Point(360, 95);
        this.btnQuitarOpcion.Name = "btnQuitarOpcion";
       this.btnQuitarOpcion.Size = new Size(94, 27);
   this.btnQuitarOpcion.TabIndex = 5;
    this.btnQuitarOpcion.Text = "Quitar";
            this.btnQuitarOpcion.UseVisualStyleBackColor = true;
  this.btnQuitarOpcion.Click += BtnQuitarOpcion_Click;

   // lblRespuestaCorrecta
  this.lblRespuestaCorrecta.AutoSize = true;
         this.lblRespuestaCorrecta.Location = new Point(12, 416);
     this.lblRespuestaCorrecta.Name = "lblRespuestaCorrecta";
            this.lblRespuestaCorrecta.Size = new Size(116, 15);
   this.lblRespuestaCorrecta.TabIndex = 7;
       this.lblRespuestaCorrecta.Text = "Respuesta Correcta:";

  // cboRespuestaCorrecta
this.cboRespuestaCorrecta.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboRespuestaCorrecta.FormattingEnabled = true;
          this.cboRespuestaCorrecta.Location = new Point(12, 434);
 this.cboRespuestaCorrecta.Name = "cboRespuestaCorrecta";
 this.cboRespuestaCorrecta.Size = new Size(460, 23);
        this.cboRespuestaCorrecta.TabIndex = 8;

 // btnAceptar
   this.btnAceptar.Location = new Point(266, 473);
   this.btnAceptar.Name = "btnAceptar";
 this.btnAceptar.Size = new Size(100, 35);
            this.btnAceptar.TabIndex = 9;
this.btnAceptar.Text = "Aceptar";
    this.btnAceptar.UseVisualStyleBackColor = true;
     this.btnAceptar.Click += BtnAceptar_Click;

   // btnCancelar
      this.btnCancelar.Location = new Point(372, 473);
     this.btnCancelar.Name = "btnCancelar";
this.btnCancelar.Size = new Size(100, 35);
      this.btnCancelar.TabIndex = 10;
     this.btnCancelar.Text = "Cancelar";
   this.btnCancelar.UseVisualStyleBackColor = true;
  this.btnCancelar.Click += BtnCancelar_Click;

       // FrmPreguntaDetalle
         this.AutoScaleDimensions = new SizeF(7F, 15F);
       this.AutoScaleMode = AutoScaleMode.Font;
this.ClientSize = new Size(484, 520);
        this.Controls.Add(this.btnCancelar);
     this.Controls.Add(this.btnAceptar);
this.Controls.Add(this.cboRespuestaCorrecta);
    this.Controls.Add(this.lblRespuestaCorrecta);
            this.Controls.Add(this.grpOpciones);
     this.Controls.Add(this.cboDificultad);
  this.Controls.Add(this.lblDificultad);
this.Controls.Add(this.cboCategoria);
   this.Controls.Add(this.lblCategoria);
this.Controls.Add(this.txtTextoPregunta);
this.Controls.Add(this.lblTextoPregunta);
     this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
  this.MinimizeBox = false;
       this.Name = "FrmPreguntaDetalle";
         this.StartPosition = FormStartPosition.CenterParent;
  this.Text = "Agregar/Editar Pregunta";
 this.grpOpciones.ResumeLayout(false);
       this.grpOpciones.PerformLayout();
            this.ResumeLayout(false);
    this.PerformLayout();
     }
    }
}
