using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database;
using Firebase.Database.Query;
using RegistroEmpleados.AppMovil.servicios;
using RegistroEmpleados.AppMovil.Vistas;
using RegistroEmpleados.Modelos.Modelos;
using System.Collections.ObjectModel;

namespace RegistroEmpleados.AppMovil.ViewModels;

internal partial class EstudiantesViewModel : ObservableObject
{
    private readonly FirebaseClient _client;

    [ObservableProperty]
    private ObservableCollection<Estudiante> lista = new();

    [ObservableProperty]
    private string filtro;

    public EstudiantesViewModel()
    {
        _client = FirebaseService.Instance.Client;
        CargarListaCommand.Execute(null);
    }

    [RelayCommand]
    private void CargarLista()
    {
        _client.Child("Estudiantes").AsObservable<Estudiante>().Subscribe(estudiante =>
        {
            //if (estudiante?.Object == null || !estudiante.Object.Activo) return;

            estudiante.Object.Id = estudiante.Key;
            var existente = Lista.FirstOrDefault(e => e.Id == estudiante.Object.Id);

            if (existente == null)
            {
                Lista.Add(estudiante.Object);
            }
            else
            {
                var index = Lista.IndexOf(existente);
                Lista[index] = estudiante.Object;
            }
        });
    }

    [RelayCommand]
    private void FiltrarLista()
    {
        var textoFiltro = Filtro?.ToLower() ?? string.Empty;

        foreach (var estudiante in Lista)
        {
            //estudiante.Activo = string.IsNullOrWhiteSpace(textoFiltro) || estudiante.NombreCompleto.ToLower().Contains(textoFiltro);
        }
    }

    [RelayCommand]
    async Task CrearEstudiante()
    {
        await Navegar(new CrearEstudiante());
    }

    [RelayCommand]
    private async Task VerDetalles(Estudiante estudiante)
    {
        if (estudiante != null)
        {
            await Navegar(new DetalleEstudiante(estudiante));
        }
    }

    [RelayCommand]
    private async Task EditarEstudiante(Estudiante estudiante)
    {
        if (estudiante != null)
        {
            await Navegar(new EditarEstudiante(estudiante));
        }
    }

    [RelayCommand]
    private async Task DesactivarEstudiante(Estudiante estudiante)
    {
        if (estudiante == null) return;

        bool confirmacion = await MostrarAlerta("Confirmar", $"¿Deseas desactivar a {estudiante.NombreCompleto}?", "Sí", "No");
        if (!confirmacion) return;

        try
        {
            //estudiante.Activo = false;
            await _client.Child("Estudiantes").Child(estudiante.Id).PutAsync(estudiante);
            Lista.Remove(estudiante);
            await MostrarAlerta("Éxito", "El estudiante fue desactivado correctamente.", "OK");
        }
        catch (Exception ex)
        {
            await MostrarAlerta("Error", $"No se pudo desactivar el estudiante: {ex.Message}", "OK");
        }
    }

    private async Task Navegar(Page page)
    {
        if (App.Current.MainPage is NavigationPage navigationPage)
        {
            await navigationPage.PushAsync(page);
        }
        else
        {
            throw new InvalidOperationException("La página principal no es un NavigationPage.");
        }
    }


    private async Task<bool> MostrarAlerta(string titulo, string mensaje, string aceptar, string cancelar = null)
    {
        return cancelar == null
            ? await App.Current.MainPage.DisplayAlert(titulo, mensaje, aceptar, cancelar)
            : await App.Current.MainPage.DisplayAlert(titulo, mensaje, aceptar, cancelar);
    }
}
