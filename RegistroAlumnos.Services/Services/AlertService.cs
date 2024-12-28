using RegistroAlumnos.Services.Helpers;

namespace RegistroAlumnos.Services.Services
{
    public class AlertService : IAlertService
    {
        public Task ShowAlertAsync(string title, string message, string cancel)
        {
            if (AlertHelper.ShowAlert == null)
                throw new InvalidOperationException("El delegado ShowAlert no está configurado.");

            return AlertHelper.ShowAlert(title, message, cancel);
        }
        public Task<bool> ShowConfirmationAsync(string title, string message, string accept, string cancel)
        {
            if (AlertHelper.ShowConfirmation == null)
                throw new InvalidOperationException("El delegado ShowConfirmation no está configurado.");

            return AlertHelper.ShowConfirmation(title, message, accept, cancel);
        }
    }
}
