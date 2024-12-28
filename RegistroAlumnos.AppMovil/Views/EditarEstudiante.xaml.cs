using RegistroAlumnos.AppMovil.ViewModels;
using RegistroAlumnos.Models.Models;

namespace RegistroAlumnos.AppMovil.Views;

public partial class EditarEstudiante : ContentPage, IQueryAttributable
{
    private EditarEstudianteViewModel _viewModel;

    public EditarEstudiante()
    {
        InitializeComponent();

        var mauiContext = Application.Current?.Handler?.MauiContext;
        if (mauiContext == null)
        {
            throw new InvalidOperationException("El contexto de Maui no está configurado.");
        }

        _viewModel = mauiContext.Services.GetService<EditarEstudianteViewModel>();
        if (_viewModel == null)
        {
            throw new InvalidOperationException("El servicio EditarEstudianteViewModel no está registrado.");
        }

        BindingContext = _viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (_viewModel == null)
        {
            throw new InvalidOperationException("El ViewModel no está inicializado.");
        }

        if (query.ContainsKey("Estudiante") && query["Estudiante"] is Estudiante estudiante)
        {
            // Pasar el estudiante al ViewModel
            _viewModel.InicializarEstudiante(estudiante);
        }
    }
}