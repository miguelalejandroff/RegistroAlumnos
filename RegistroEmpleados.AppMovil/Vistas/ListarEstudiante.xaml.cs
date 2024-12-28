using System.Collections.ObjectModel;
using Firebase.Database;
using Firebase.Database.Query;
using RegistroEmpleados.AppMovil.servicios;
using RegistroEmpleados.AppMovil.ViewModels;
using RegistroEmpleados.Modelos.Modelos;

namespace RegistroEmpleados.AppMovil.Vistas;

public partial class ListarEstudiante : ContentPage
{
    public ListarEstudiante()
	{
		InitializeComponent();
        BindingContext = new ListarEstudianteViewModel();
    }
}