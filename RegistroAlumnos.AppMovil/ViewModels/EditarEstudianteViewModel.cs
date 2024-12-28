using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroAlumnos.AppMovil.Validators;
using RegistroAlumnos.Models.Models;
using RegistroAlumnos.Services.Services;

namespace RegistroAlumnos.AppMovil.ViewModels
{
    public partial class EditarEstudianteViewModel : ObservableObject
    {
        private readonly EstudianteService _estudianteService = new();
        private readonly CursoService _cursoService = new();
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private ObservableCollection<Curso> listaCursos = new();

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
        private string edad = string.Empty;

        [ObservableProperty]
        private bool estado;

        private Estudiante? _estudianteOriginal;

        public EditarEstudianteViewModel(IAlertService alertService)
        {
            _alertService = alertService;
        }

        public void InicializarEstudiante(Estudiante estudiante)
        {
            if (estudiante == null) throw new ArgumentNullException(nameof(estudiante));

            _estudianteOriginal = estudiante;

            PrimerNombre = estudiante.PrimerNombre ?? string.Empty;
            SegundoNombre = estudiante.SegundoNombre ?? string.Empty;
            PrimerApellido = estudiante.PrimerApellido ?? string.Empty;
            SegundoApellido = estudiante.SegundoApellido ?? string.Empty;
            CorreoElectronico = estudiante.CorreoElectronico ?? string.Empty;
            Edad = estudiante.Edad.ToString() ?? string.Empty;
            CursoSeleccionado = estudiante.Curso;
            Estado = estudiante.Estado ?? true;

            _ = CargarCursosAsync();
        }

        private async Task CargarCursosAsync()
        {
            try
            {
                ListaCursos = new ObservableCollection<Curso>(await _cursoService.ObtenerCursosAsync());


                if (_estudianteOriginal?.Curso?.Nombre == null)
                {
                    await _alertService.ShowAlertAsync("Advertencia", "El curso del estudiante es nulo.", "OK");
                    return;
                }

                CursoSeleccionado = ListaCursos
                    .FirstOrDefault(c => c.Nombre?.Equals(_estudianteOriginal.Curso.Nombre, StringComparison.OrdinalIgnoreCase) == true);

                if (CursoSeleccionado == null)
                {
                    await _alertService.ShowAlertAsync("Advertencia", $"No se encontró el curso con el nombre: {_estudianteOriginal.Curso.Nombre}", "OK");
                }
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync("Error", $"Error al cargar los cursos: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task ActualizarEstudianteAsync()
        {
            var errores = EstudianteValidator.ValidarEstudiante(
                PrimerNombre,
                PrimerApellido,
                CorreoElectronico,
                Edad,
                CursoSeleccionado);

            if (errores.Any())
            {
                await _alertService.ShowAlertAsync("Advertencia", string.Join("\n", errores), "OK");
                return;
            }

            if (_estudianteOriginal == null)
            {
                await _alertService.ShowAlertAsync("Error", "El estudiante original no está inicializado.", "OK");
                return;
            }

            try
            {
                ActualizarDatosEstudiante();

                await _estudianteService.ActualizarEstudianteAsync(_estudianteOriginal);
                await _alertService.ShowAlertAsync("Éxito", $"Estudiante {PrimerNombre} actualizado correctamente.", "OK");

                await Shell.Current.GoToAsync("//ListarEstudiantes");
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync("Error", $"Error al actualizar el estudiante: {ex.Message}", "OK");
            }
        }
        private void ActualizarDatosEstudiante()
        {
            if (_estudianteOriginal == null)
            {
                _alertService.ShowAlertAsync("Error", "No se puede actualizar los datos porque el estudiante original no está inicializado.", "OK");
                return;
            }
            _estudianteOriginal.PrimerNombre = PrimerNombre;
            _estudianteOriginal.SegundoNombre = SegundoNombre;
            _estudianteOriginal.PrimerApellido = PrimerApellido;
            _estudianteOriginal.SegundoApellido = SegundoApellido;
            _estudianteOriginal.CorreoElectronico = CorreoElectronico;
            _estudianteOriginal.Edad = int.Parse(Edad);
            _estudianteOriginal.Curso = CursoSeleccionado;
            _estudianteOriginal.Estado = Estado;
        }
    }
}
