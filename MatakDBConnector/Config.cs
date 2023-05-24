namespace MatakDBConnector
{
    internal class Config
    {
        private string host = null;
        private string username = null;
        private string password = null;
        private string database = null;

        public string Host
        {
            get => host;
            set => host = value;
        }

        public string Username
        {
            get => username;
            set => username = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public string Database
        {
            get => database;
            set => database = value;
        }
    }
}