using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace RegistroEstudiantes
{
    public partial class Form1 : Form
    {
        ErrorProvider error = new ErrorProvider();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // ===================== GUARDAR =====================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarCampos();

                MessageBox.Show("Estudiante registrado correctamente",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error de validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ===================== VALIDACIONES =====================
        private void ValidarCampos()
        {
            error.Clear();

            ValidarNombre();
            ValidarEdad();
            ValidarCorreo();
        }

        // -------- NOMBRE --------
        private void ValidarNombre()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                error.SetError(txtNombre, "El nombre es obligatorio");
                throw new Exception("Debe ingresar el nombre completo");
            }
        }

        // -------- EDAD --------
        private void ValidarEdad()
        {
            int edad;

            if (!int.TryParse(txtEdad.Text, out edad))
            {
                error.SetError(txtEdad, "Edad inválida");
                throw new Exception("La edad debe ser un número entero");
            }

            if (edad <= 0 || edad > 120)
            {
                error.SetError(txtEdad, "Edad fuera de rango");
                throw new Exception("Edad debe estar entre 1 y 120");
            }
        }

        // -------- CORREO --------
        private void ValidarCorreo()
        {
            string correo = txtCorreo.Text.Trim();

            if (string.IsNullOrWhiteSpace(correo))
            {
                error.SetError(txtCorreo, "Correo obligatorio");
                throw new Exception("Debe ingresar un correo electrónico");
            }

            if (!correo.Contains("@"))
            {
                error.SetError(txtCorreo, "Debe contener @");
                throw new Exception("Correo inválido");
            }

            if (!(correo.EndsWith("@gmail.com") ||
                  correo.EndsWith("@hotmail.com") ||
                  correo.EndsWith("@outlook.com")))
            {
                error.SetError(txtCorreo, "Dominio no válido");
                throw new Exception("Solo se permiten gmail, hotmail o outlook");
            }

            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(correo, patron))
            {
                error.SetError(txtCorreo, "Formato incorrecto");
                throw new Exception("Formato de correo inválido");
            }
        }

        // ===================== LIMPIAR =====================
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtEdad.Clear();
            txtCorreo.Clear();

            error.Clear();

            txtNombre.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // ===================== SALIR =====================
        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                "¿Deseas salir del sistema?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}