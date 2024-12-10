using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Logging;
using RegistroEmpleados.AppMovil.servicios;
using RegistroEmpleados.Modelos.Modelos;

namespace RegistroEmpleados.AppMovil
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            _ = RegistrarCursosAsync();
            return builder.Build();


        }

        public static async Task RegistrarCursosAsync()
        {
            try
            {
                var client = FirebaseService.Instance.Client;

                var cursosExistentes = await client.Child("Cursos").OnceAsync<Curso>();

                if (cursosExistentes.Count == 0)
                {
                    var listaDeCursos = new List<Curso>
                    {
                        new Curso { Nombre = "1ro Básico" },
                        new Curso { Nombre = "2do Básico" },
                        new Curso { Nombre = "3ro Básico" },
                        new Curso { Nombre = "4to Básico" },
                        new Curso { Nombre = "5to Básico" },
                        new Curso { Nombre = "6to Básico" },
                        new Curso { Nombre = "7mo Básico" },
                        new Curso { Nombre = "8vo Básico" },
                        new Curso { Nombre = "1ro Medio" },
                        new Curso { Nombre = "2do Medio" },
                        new Curso { Nombre = "3ro Medio" },
                        new Curso { Nombre = "4to Medio" }
                    };

                    foreach (var curso in listaDeCursos)
                    {
                        await client.Child("Cursos").PostAsync(curso);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar cursos: {ex.Message}");
            }
        }

    }
}
