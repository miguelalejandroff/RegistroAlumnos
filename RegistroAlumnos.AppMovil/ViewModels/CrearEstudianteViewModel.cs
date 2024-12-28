using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroAlumnos.AppMovil.Validators;
using RegistroAlumnos.Models.Models;
using RegistroAlumnos.Services.Services;

namespace RegistroAlumnos.AppMovil.ViewModels
{
    public partial class CrearEstudianteViewModel : ObservableObject
    {
        private readonly CursoService _cursoService = new();
        private readonly EstudianteService _estudianteService = new();
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private ObservableCollection<Curso> cursos = new();

        [ObservableProperty]
        private Curso? cursoSeleccionado;

        [ObservableProperty]
        private string primerNombre = string.Empty;

        [ObservableProperty]
        private string segundoNombre = string.Empty;

        [ObservableProperty]
        private string primerApellido = string.Empty;

        [ObservableProperty]
        private string segundoApellido = string.Empty;

        [ObservableProperty]
        private string correoElectronico = string.Empty;

        [ObservableProperty]
        private DateTime fechaInscripcion = DateTime.Now;

        [ObservableProperty]
        private string edad = string.Empty;

        [ObservableProperty]
        private bool estado = true;

        public CrearEstudianteViewModel(IAlertService alertService)
        {
            _alertService = alertService;
            CargarCursos();
        }

        private async void CargarCursos()
        {
            try
            {
                var listaCursos = await _cursoService.ObtenerCursosAsync();
                Cursos = new ObservableCollection<Curso>(listaCursos);
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync("Error", $"Error al cargar los cursos: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task GuardarEstudiante()
        {
            var errores = EstudianteValidator.ValidarEstudiante(
                PrimerNombre,
                PrimerApellido,
                CorreoElectronico,
                Edad,
                CursoSeleccionado);

            if (errores.Any())
            {
                var mensajeErrores = string.Join("\n", errores);
                await _alertService.ShowAlertAsync("Advertencia", mensajeErrores, "OK");
                return;
            }

            var estudiante = new Estudiante
            {
                PrimerNombre = PrimerNombre,
                SegundoNombre = SegundoNombre,
                PrimerApellido = PrimerApellido,
                SegundoApellido = SegundoApellido,
                CorreoElectronico = CorreoElectronico,
                FechaInsripcion = FechaInscripcion,
                Edad = int.Parse(Edad),
                Curso = CursoSeleccionado,
                Estado = Estado
            };

            try
            {
                await _estudianteService.AgregarEstudianteAsync(estudiante);
                await _alertService.ShowAlertAsync("Éxito", $"Estudiante {PrimerNombre} {PrimerApellido} guardado correctamente.", "OK");
                await Shell.Current.GoToAsync("//ListarEstudiantes");

            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync("Error", $"Error al guardar el estudiante: {ex.Message}", "OK");
            }
        }
    }
}
