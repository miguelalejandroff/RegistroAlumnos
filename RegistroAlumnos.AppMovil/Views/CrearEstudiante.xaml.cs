using RegistroAlumnos.AppMovil.ViewModels;

namespace RegistroAlumnos.AppMovil.Views;

public partial class CrearEstudiante : ContentPage
{
	public CrearEstudiante()
	{
		InitializeComponent();
        BindingContext = Application.Current.Handler.MauiContext.Services.GetService<CrearEstudianteViewModel>();

    }
}