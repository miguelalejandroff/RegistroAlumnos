

using Firebase.Database;
using RegistroAlumnos.Models.Models;

namespace RegistroAlumnos.Mappers.Mappers
{
    public static class EstudianteMapper
    {
        public static Estudiante MapFirebaseToEstudiante(FirebaseObject<Estudiante> firebaseObject)
        {
            return new Estudiante
            {
                Id = firebaseObject.Key,
                PrimerNombre = firebaseObject.Object.PrimerNombre,
                SegundoNombre = firebaseObject.Object.SegundoNombre,
                PrimerApellido = firebaseObject.Object.PrimerApellido,
                SegundoApellido = firebaseObject.Object.SegundoApellido,
                CorreoElectronico = firebaseObject.Object.CorreoElectronico,
                FechaInsripcion = firebaseObject.Object.FechaInsripcion,
                Edad = firebaseObject.Object.Edad,
                Estado = firebaseObject.Object.Estado,
                Curso = firebaseObject.Object.Curso
            };
        }
    }
}
