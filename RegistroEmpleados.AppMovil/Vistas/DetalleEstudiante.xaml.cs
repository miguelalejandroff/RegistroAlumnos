using RegistroEmpleados.Modelos.Modelos;

namespace RegistroEmpleados.AppMovil.Vistas;

public partial class DetalleEstudiante : ContentPage
{
	public DetalleEstudiante(Estudiante estudiante)
	{
		InitializeComponent();
        BindingContext = estudiante;
    }
    private void RegresarCommand_Execute(object sender, EventArgs e)
    {
        Navigation.PopAsync(); 
    }
}