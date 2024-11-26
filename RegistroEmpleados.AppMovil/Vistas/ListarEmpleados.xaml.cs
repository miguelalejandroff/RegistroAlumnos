namespace RegistroEmpleados.AppMovil.Vistas;

public partial class ListarEmpleados : ContentPage
{
    public ListarEmpleados()
    {
        InitializeComponent();
    }

    private void filtroSearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private async void NuevoEmpleadoBoton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CrearNuevoEmpleado));
    }
}