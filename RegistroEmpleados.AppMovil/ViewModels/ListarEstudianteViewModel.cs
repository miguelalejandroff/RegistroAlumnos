

using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroEmpleados.AppMovil.servicios;
using RegistroEmpleados.Modelos.Modelos;


namespace RegistroEmpleados.AppMovil.ViewModels
{
    public partial class ListarEstudianteViewModel : ObservableObject
    {
        private readonly FirebaseService _firebaseService = FirebaseService.Instance;

        [ObservableProperty]
        private ObservableCollection<EstudianteContext> estudiantes;

        [ObservableProperty]
        private Estudiante estudiante;
        
        public ListarEstudianteViewModel()
        {
            CargarEstudiantes();
        }

        private async void CargarEstudiantes()
        {
            var firebaseEstudiantes = await _firebaseService.Client
                .Child("Estudiantes")
                .OnceAsync<Estudiante>();



            //Estudiantes = new ObservableCollection<EstudianteContext>(
            //    firebaseEstudiantes
            //        .Where(e => e.Object.Estado == true)
            //        .Select(e => new EstudianteContext(EstudianteMapper(e), this)));

        }

        [RelayCommand]
        private void BuscarEstudiante()
        {

        }

        [RelayCommand]
        private void AgregarEstudiante()
        {

        }

        [RelayCommand]
        private void EditarEstudiante()
        {

        }

        [RelayCommand]
        private void DesactivarEstudiante()
        {

        }

    }

}
