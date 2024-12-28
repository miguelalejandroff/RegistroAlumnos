using Microsoft.Extensions.Logging;
using RegistroAlumnos.AppMovil.Helpers;
using RegistroAlumnos.AppMovil.ViewModels;
using RegistroAlumnos.Services.Services;


namespace RegistroAlumnos.AppMovil
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

            MauiStartup.ConfigureAlerts();
            builder.Services.AddSingleton<IAlertService, AlertService>();
            builder.Services.AddTransient<ListarEstudiantesViewModel>();
            builder.Services.AddTransient<CrearEstudianteViewModel>();
            builder.Services.AddTransient<EditarEstudianteViewModel>();
            builder.Services.AddSingleton<FirebaseService>();
            builder.Services.AddSingleton<CursoService>();
            builder.Services.AddSingleton<EstudianteService>();

            var app = builder.Build();

            // Invocar métodos de inicialización
            var cursoService = app.Services.GetService<CursoService>();
            var estudianteService = app.Services.GetService<EstudianteService>();

            _ = cursoService?.ActualizarCursosInicialesAsync();
            _ = estudianteService?.ActualizarEstudiantesInicialesAsync();

            return app;
        }
    }
}
