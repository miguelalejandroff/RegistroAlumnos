using Firebase.Database;
using Firebase.Database.Query;
using RegistroEmpleados.AppMovil.servicios;
using RegistroEmpleados.Modelos.Modelos;

namespace RegistroEmpleados.AppMovil.Vistas;

public partial class CrearEstudiante : ContentPage
{
    private readonly FirebaseClient _client = FirebaseService.Instance.Client;
	public List<Curso> Cursos { get; set; } = new List<Curso>();
    public CrearEstudiante()
	{
		InitializeComponent();
		ListarCursos();
		BindingContext = this;
	}
	private void ListarCursos()
	{
		var cursos = _client.Child("Cursos").OnceAsync<Curso>();
		Cursos = cursos.Result.Select(x => x.Object).ToList();
	}
	private async void guardarButton_Clicked(object sender, EventArgs e)
	{
		Curso curso = cursoPicker.SelectedItem as Curso;

		var estudiante = new Estudiante
		{
			PrimerNombre = primerNombreEntry.Text,
			SegundoNombre = segundoNombreEntry.Text,
			PrimerApellido = primerApellidoEntry.Text,
			SegundoApellido = segundoApellidoEntry.Text,
			CorreoElectronico = correoEntry.Text,
			FechaInsripcion = fechaInscripcionPicker.Date,
			Edad = int.Parse(edadEntry.Text),
			Curso = curso
		};
		try
		{
            await _client.Child("Estudiantes").PostAsync(estudiante);
            await DisplayAlert("Exito", $"El estudiante {estudiante.PrimerNombre} {estudiante.PrimerApellido} fue guardado correctamente", "OK");
            await Navigation.PopAsync();
        }
		catch(Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}