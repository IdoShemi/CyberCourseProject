using System;
using Npgsql;

namespace MatakDBConnector
{
    public class Client
    {
        private int _clientId;
        private string _lastName;
        private string _firstName;
        private int _orgId;
        private string _nickname;

        

        public Client()
        {
            _clientId = 0;
            _lastName = "0";
            _firstName = "0";
            _orgId = 0;
            _nickname = "0";
        }

        public Client(int clientId, string lastName, string firstName, int orgId, string nickname)
        {
            _clientId = clientId;
            _lastName = lastName;
            _firstName = firstName;
            _orgId = orgId;
            _nickname = nickname;
        }
        
        public Client ClientMaker(NpgsqlDataReader reader)
        {
            Client client = new Client();
            
            client.ClientId = reader.GetInt32(0);
            client.OrgId = reader.GetInt32(1);
            client.FirstName = reader.GetString(2);
            client.LastName = reader.GetString(3);
            client.Nickname = reader.GetString(4);

            return client;
        }
        
        protected void newClientCommandHelper(Client client, NpgsqlCommand command)
        {
            command.Parameters.AddWithValue("name", client.LastName);
            command.Parameters.AddWithValue("surname", client.FirstName);
            command.Parameters.AddWithValue("org_id", client.OrgId);
            command.Parameters.AddWithValue("client_username", client.Nickname);
        }

        public int ClientId
        {
            get => _clientId;
            set => _clientId = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public int OrgId
        {
            get => _orgId;
            set => _orgId = value;
        }

        public string Nickname
        {
            get => _nickname;
            set => _nickname = value;
        }

    }
}