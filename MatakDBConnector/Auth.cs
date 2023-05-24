using Npgsql;

namespace MatakDBConnector
{
    public class Auth
    {
        private string _email;
        private string _password;

        public Auth()
        {
            _email = "0";
            _password = "0";
        }

        public Auth(string email, string password)
        {
            _email = email;
            _password = password;

        }
        
        public Auth AuthMaker(NpgsqlDataReader reader)
        {
            Auth auth = new Auth();
            
            auth.Email = reader.GetString(0);
            auth.Password = reader.GetString(1);

            return auth;
        }
        
        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }
    }
}