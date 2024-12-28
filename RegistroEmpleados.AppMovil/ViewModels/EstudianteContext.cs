
using RegistroEmpleados.Modelos.Modelos;

namespace RegistroEmpleados.AppMovil.ViewModels
{
    public class EstudianteContext
    {
        public Estudiante Estudiante { get; }
        public ListarEstudianteViewModel ViewModel { get; }

        public EstudianteContext(Estudiante estudiante, ListarEstudianteViewModel viewModel)
        {
            Estudiante = estudiante;
            ViewModel = viewModel;
        }
    }
}
