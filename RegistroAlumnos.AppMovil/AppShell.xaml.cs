using RegistroAlumnos.AppMovil.Views;

namespace RegistroAlumnos.AppMovil
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("ListarEstudiantes", typeof(ListarEstudiantes));
            Routing.RegisterRoute("CrearEstudiante", typeof(CrearEstudiante));
            Routing.RegisterRoute("EditarEstudiante", typeof(EditarEstudiante));
        }
    }
}
