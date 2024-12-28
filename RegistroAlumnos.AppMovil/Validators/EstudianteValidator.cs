using RegistroAlumnos.Models.Models;

namespace RegistroAlumnos.AppMovil.Validators
{
    public static class EstudianteValidator
    {
        public static List<string> ValidarEstudiante(
            string primerNombre,
            string primerApellido,
            string correoElectronico,
            string edad,
            Curso? cursoSeleccionado)
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(primerNombre))
                errores.Add("Debe completar el primer nombre.");

            if (string.IsNullOrWhiteSpace(primerApellido))
                errores.Add("Debe completar el primer apellido.");

            if (string.IsNullOrWhiteSpace(correoElectronico))
                errores.Add("Debe completar el correo electrónico.");
            else if (!correoElectronico.Contains("@"))
                errores.Add("El correo electrónico debe contener un '@' válido.");

            if (!int.TryParse(edad, out var edadNumerica) || edadNumerica <= 0)
                errores.Add("La edad debe ser un número válido mayor que 0.");

            if (cursoSeleccionado == null)
                errores.Add("Debe seleccionar un curso.");

            return errores;
        }
    }
}
