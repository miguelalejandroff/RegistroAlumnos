namespace RegistroAlumnos.Services.Helpers
{
    public static class AlertHelper
    {
        public static Func<string, string, string, Task>? ShowAlert { get; set; }
        public static Func<string, string, string, string, Task<bool>>? ShowConfirmation { get; set; }

    }
}
