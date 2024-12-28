using Firebase.Database.Query;
using RegistroAlumnos.Models.Models;

namespace RegistroAlumnos.Services.Services
{
    public class CursoService
    {
        private const string NodePath = "Cursos";
        private readonly FirebaseService _firebaseService;

        public CursoService()
        {
            _firebaseService = FirebaseService.Instance;
        }


        /// Obtiene una lista de cursos
        public async Task<List<Curso>> ObtenerCursosAsync()
        {
            return await _firebaseService.GetAllAsync<Curso>(NodePath);
        }

        /// Agrega un nuevo curso
        public async Task AgregarCursoAsync(Curso curso)
        {
            await _firebaseService.AddAsync(NodePath, curso);
        }

        public async Task ActualizarCursosInicialesAsync()
        {
            try
            {
                var cursos = await _firebaseService.Client.Child(NodePath).OnceAsync<Curso>();

                if (cursos.Count == 0)
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
                        await AgregarCursoAsync(curso);
                    }
                }
                else
                {
                    foreach (var curso in cursos)
                    {
                        if (curso.Object.Estado == null)
                        {
                            var cursoActualizado = curso.Object;
                            cursoActualizado.Estado = true;
                            await _firebaseService.Client.Child(NodePath).Child(curso.Key).PutAsync(cursoActualizado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar los cursos: {ex.Message}");
            }
        }
    }
}
