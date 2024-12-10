using System.Collections.ObjectModel;
using Firebase.Database;
using RegistroEmpleados.AppMovil.servicios;
using RegistroEmpleados.Modelos.Modelos;

namespace RegistroEmpleados.AppMovil.Vistas;

public partial class ListarEstudiante : ContentPage
{
    private readonly FirebaseClient _client = FirebaseService.Instance.Client;
    public ObservableCollection<Estudiante> Lista { get; set; } = new ObservableCollection<Estudiante>();
    public ListarEstudiante()
	{
		InitializeComponent();
        BindingContext = this;
        CargarLista();
	}

    private void CargarLista()
    {
        _client.Child("Estudiantes").AsObservable<Estudiante>().Subscribe((estudiante) =>
        {
            if(estudiante != null)
            {
                Lista.Add(estudiante.Object);
            }
        });
    }

    private void filtroSearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        string filtro = filtroSearchBar.Text.ToLower();
        if(filtro.Length > 0 )
        {
            ListaCollection.ItemsSource = Lista.Where(x => x.NombreCompleto.ToLower().Contains(filtro));
        }
        else
        {
            ListaCollection.ItemsSource = Lista;
        }
    }

    private async void NuevoEstudianteBoton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CrearEstudiante());
    }
}