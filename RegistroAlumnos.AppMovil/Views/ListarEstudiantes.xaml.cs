using RegistroAlumnos.AppMovil.ViewModels;
using RegistroAlumnos.Models.Models;

namespace RegistroAlumnos.AppMovil.Views;

public partial class ListarEstudiantes : ContentPage
{
	public ListarEstudiantes()
	{
		InitializeComponent();
        BindingContext = Application.Current.Handler.MauiContext.Services.GetService<ListarEstudiantesViewModel>();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("CrearEstudiante");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ListarEstudiantesViewModel viewModel)
        {
            viewModel.OnPageAppearing();
        }
    }

    private async void editarButton_Clicked(object sender, EventArgs e)
    {
        var boton = sender as Button;
        var estudiante = boton?.CommandParameter as Estudiante;

        if (estudiante != null && !string.IsNullOrEmpty(estudiante.Id))
        {
            // Pasar los datos del estudiante como parámetros
            var parametros = new Dictionary<string, object>
            {
                { "Estudiante", estudiante }
            };
            await Shell.Current.GoToAsync("EditarEstudiante", parametros);
        }
        else
        {
            await DisplayAlert("Error", $"No se pudo obtener la informacion del estudiante {estudiante}", "Ok");
        }

    }


}