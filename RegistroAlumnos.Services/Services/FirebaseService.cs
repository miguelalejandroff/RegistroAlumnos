using Firebase.Database;
using Firebase.Database.Query;

namespace RegistroAlumnos.Services.Services
{
    public class FirebaseService
    {
        private static FirebaseService _instance = null!;
        private readonly FirebaseClient _client = null!;
        private static readonly string FirebaseUrl = "https://registroempleados-b0faf-default-rtdb.firebaseio.com/";

        private FirebaseService()
        {
            try
            {
                _client = new FirebaseClient(FirebaseUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inicializando FirebaseClient: {ex.Message}");
            }
        }

        public static FirebaseService Instance => _instance ??= new FirebaseService();

        public FirebaseClient Client => _client;

        /// Obtiene todos los elementos de un nodo en Firebase.
        public async Task<List<T>> GetAllAsync<T>(string nodePath) where T : class
        {
            try
            {
                var result = await _client.Child(nodePath).OnceAsync<T>();
                return result.Select(item => item.Object).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo datos de {nodePath}: {ex.Message}");
                throw;
            }
        }

        /// Agrega un nuevo elemento a Firebase.
        public async Task AddAsync<T>(string nodePath, T item) where T : class
        {
            try
            {
                await _client.Child(nodePath).PostAsync(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error agregando datos a {nodePath}: {ex.Message}");
                throw;
            }
        }
    }
}
