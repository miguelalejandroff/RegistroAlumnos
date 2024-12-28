using Firebase.Database.Query;
using RegistroAlumnos.Models.Models;

namespace RegistroAlumnos.Services.Services
{
    public class EstudianteService
    {
        private const string NodePath = "Estudiantes";
        private readonly FirebaseService _firebaseService;

        public EstudianteService()
        {
            _firebaseService = FirebaseService.Instance;
        }

        /// Obtiene una lista de estudiantes activos
        public async Task<List<Estudiante>> ObtenerEstudiantesActivosAsync()
        {
            var estudiantes = await _firebaseService.Client
                .Child(NodePath)
                .OnceAsync<Estudiante>();

            return estudiantes
                .Where(e => e.Object.Estado == true)
                .Select(e => new Estudiante
                {
                    Id = e.Key,
                    PrimerNombre = e.Object.PrimerNombre,
                    SegundoNombre = e.Object.SegundoNombre,
                    PrimerApellido = e.Object.PrimerApellido,
                    SegundoApellido = e.Object.SegundoApellido,
                    CorreoElectronico = e.Object.CorreoElectronico,
                    FechaInsripcion = e.Object.FechaInsripcion,
                    Edad = e.Object.Edad,
                    Estado = e.Object.Estado,
                    Curso = e.Object.Curso
                })
                .ToList();
        }

        /// Agrega un nuevo estudiante
        public async Task AgregarEstudianteAsync(Estudiante estudiante)
        {
            await _firebaseService.AddAsync(NodePath, estudiante);
        }
        /// Actualiza un estudiante existente
        public async Task ActualizarEstudianteAsync(Estudiante estudiante)
        {
            try
            {
                // Actualizar los datos del estudiante
                await _firebaseService.Client
                    .Child(NodePath)
                    .Child(estudiante.Id)
                    .PutAsync(estudiante);

                Console.WriteLine("Estudiante actualizado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando el estudiante: {ex.Message}");
                throw;
            }
        }
        public async Task ActualizarEstudiantesInicialesAsync()
        {
            try
            {
                var estudiantes = await _firebaseService.Client.Child(NodePath).OnceAsync<Estudiante>();

                foreach (var estudiante in estudiantes)
                {
                    if (estudiante.Object.Estado == null)
                    {
                        var estudianteActualizado = estudiante.Object;
                        estudianteActualizado.Estado = true;

                        await _firebaseService.Client.Child(NodePath).Child(estudiante.Key).PutAsync(estudianteActualizado);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar los estudiantes: {ex.Message}");
            }
        }
    }
}
