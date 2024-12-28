using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroAlumnos.AppMovil.Utils;
using RegistroAlumnos.Models.Models;
using RegistroAlumnos.Services.Services;

namespace RegistroAlumnos.AppMovil.ViewModels
{
    public partial class ListarEstudiantesViewModel : ObservableObject
    {
        private readonly EstudianteService _estudianteService = new();
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private ObservableCollection<Estudiante> estudiantes = new();

        [ObservableProperty]
        private ObservableCollection<Estudiante> estudiantesFiltrados = new();

        [ObservableProperty]
        private Estudiante? estudiante;

        [ObservableProperty]
        private string textoBusqueda = string.Empty;

        [ObservableProperty]
        private bool isLoading = false;
        public bool IsContentVisible => !IsLoading;

        private CancellationTokenSource _debounceTokenSource = new();

        public ListarEstudiantesViewModel(IAlertService alertService)
        {
            _alertService = alertService;
            CargarEstudiantes();
        }

        partial void OnTextoBusquedaChanged(string value)
        {
            DebounceHelper.ApplyDebounce(BuscarEstudiante, 300, ref _debounceTokenSource);
        }

        public void OnPageAppearing()
        {
            CargarEstudiantes();
        }
        partial void OnIsLoadingChanged(bool value)
        {
            OnPropertyChanged(nameof(IsContentVisible));
        }


        private async void CargarEstudiantes()
        {
            IsLoading = true;
            try
            {
                var listaEstudiantes = await _estudianteService.ObtenerEstudiantesActivosAsync();
                Estudiantes = new ObservableCollection<Estudiante>(listaEstudiantes);
                EstudiantesFiltrados = new ObservableCollection<Estudiante>(Estudiantes);
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync("Error", $"Error al cargar estudiantes: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void BuscarEstudiante()
        {
            if (Estudiantes == null || Estudiantes.Count == 0)
            {
                _ = _alertService.ShowAlertAsync("Advertencia", "La lista de estudiantes está vacía o no ha sido inicializada.", "OK");
                EstudiantesFiltrados = new ObservableCollection<Estudiante>();
                return;
            }
            if (string.IsNullOrWhiteSpace(TextoBusqueda))
            {
                EstudiantesFiltrados = new ObservableCollection<Estudiante>(Estudiantes);
            }
            else
            {
                var filtro = TextoBusqueda.ToLower();
                var resultados = Estudiantes
                    .Where(e => e.NombreCompleto?.ToLower().Contains(filtro) == true)
                    .ToList();

                EstudiantesFiltrados = new ObservableCollection<Estudiante>(resultados);
            }
        }

        [RelayCommand]
        private async Task DesactivarEstudianteAsync(Estudiante estudiante)
        {
            if (estudiante == null)
            {
                await _alertService.ShowAlertAsync("Error", "No se pudo obtener la información del estudiante.", "OK");
                return;
            }

            var confirmacion = await _alertService.ShowConfirmationAsync("Confirmación",
                $"¿Está seguro de deshabilitar al estudiante {estudiante.NombreCompleto}?", "Sí", "No");

            if (!confirmacion) return;

            try
            {
                estudiante.Estado = false;
                await _estudianteService.ActualizarEstudianteAsync(estudiante);

                Estudiantes.Remove(estudiante);
                EstudiantesFiltrados.Remove(estudiante);

                await _alertService.ShowAlertAsync("Éxito", $"El estudiante {estudiante.NombreCompleto} ha sido deshabilitado.", "OK");
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync("Error", $"Error al deshabilitar el estudiante: {ex.Message}", "OK");
            }
        }
    }
}
