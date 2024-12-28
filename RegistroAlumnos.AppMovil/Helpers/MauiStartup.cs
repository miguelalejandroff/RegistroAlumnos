using RegistroAlumnos.Services.Helpers;

namespace RegistroAlumnos.AppMovil.Helpers
{
    public static class MauiStartup
    {
        public static void ConfigureAlerts()
        {
            // Configurar el delegado para mostrar alertas
            AlertHelper.ShowAlert = async (title, message, cancel) =>
            {
                if (Application.Current?.MainPage == null)
                {
                    throw new InvalidOperationException("MainPage no está configurado.");
                }

                await Application.Current.MainPage.DisplayAlert(title, message, cancel);
            };

            // Configurar el delegado para mostrar confirmaciones
            AlertHelper.ShowConfirmation = async (title, message, accept, cancel) =>
            {
                if (Application.Current?.MainPage == null)
                {
                    throw new InvalidOperationException("MainPage no está configurado.");
                }

                return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
            };
        }
    }
}
