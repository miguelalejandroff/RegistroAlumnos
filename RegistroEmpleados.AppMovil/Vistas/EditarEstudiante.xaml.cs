using Firebase.Database;
using Firebase.Database.Query;
using RegistroEmpleados.AppMovil.servicios;
using RegistroEmpleados.Modelos.Modelos;

namespace RegistroEmpleados.AppMovil.Vistas;

public partial class EditarEstudiante : ContentPage
{
    private readonly FirebaseClient _client = FirebaseService.Instance.Client;
    private readonly Estudiante _estudiante; 
    public List<Curso> Cursos { get; set; } = new List<Curso>();
    public EditarEstudiante(Estudiante estudiante)
	{
		InitializeComponent();
        _estudiante = estudiante;
        ListarCursos();
        BindingContext = this;
        PrecargarDatos();
    }
    private void ListarCursos()
    {
        var cursos = _client.Child("Cursos").OnceAsync<Curso>();
        Cursos = cursos.Result.Select(x => x.Object).ToList();
    }

    private void PrecargarDatos()
    {
        primerNombreEntry.Text = _estudiante.PrimerNombre;
        segundoNombreEntry.Text = _estudiante.SegundoNombre;
        primerApellidoEntry.Text = _estudiante.PrimerApellido;
        segundoApellidoEntry.Text = _estudiante.SegundoApellido;
        correoEntry.Text = _estudiante.CorreoElectronico;
        edadEntry.Text = _estudiante.Edad.ToString();
        //fechaInscripcionPicker.Date = _estudiante.FechaInsripcion;

        cursoPicker.SelectedItem = Cursos.FirstOrDefault(c => c.Nombre == _estudiante.Curso.Nombre);
    }

    private async void guardarCambiosButton_Clicked(object sender, EventArgs e)
    {
        _estudiante.PrimerNombre = primerNombreEntry.Text;
        _estudiante.SegundoNombre = segundoNombreEntry.Text;
        _estudiante.PrimerApellido = primerApellidoEntry.Text;
        _estudiante.SegundoApellido = segundoApellidoEntry.Text;
        _estudiante.CorreoElectronico = correoEntry.Text;
        _estudiante.Edad = int.Parse(edadEntry.Text);
        _estudiante.FechaInsripcion = fechaInscripcionPicker.Date;
        _estudiante.Curso = cursoPicker.SelectedItem as Curso;

        try
        {
            await _client.Child("Estudiantes").Child(_estudiante.Id).PutAsync(_estudiante);

            await DisplayAlert("Éxito", $"Los datos del estudiante {_estudiante.NombreCompleto} han sido actualizados.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo actualizar el estudiante: {ex.Message}", "OK");
        }
    }
}