using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;

namespace RegistroEmpleados.AppMovil.servicios
{
    public class FirebaseService
    {
        private static FirebaseService _instance;
        private readonly FirebaseClient _client;
        private static readonly string FirebaseUrl = "https://registroempleados-b0faf-default-rtdb.firebaseio.com/";

        private FirebaseService()
        {
            _client = new FirebaseClient(FirebaseUrl);
        }

        public static FirebaseService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FirebaseService();
                }
                return _instance;
            }
        }

        public FirebaseClient Client => _client;
    }
}
