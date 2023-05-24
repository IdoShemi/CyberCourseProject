using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class ClientModel : Client
    {
        public List<Client> GetAllClients(out string errorMessage)
        {
            List<Client> allClients = new List<Client>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM postgres.cyberschema1.clients";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Client client = new Client();
                        allClients.Add(client.ClientMaker(reader));
                    }

                    return allClients;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public List<Client> GetAllClientsByOrgId(int orgId, out string errorMessage)
        {
            List<Client> allClients = new List<Client>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM postgres.cyberschema1.clients WHERE org_id = (@orgId)";
                    command.Parameters.AddWithValue("orgId", orgId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Client client = new Client();
                        allClients.Add(client.ClientMaker(reader));
                    }

                    return allClients;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public List<Client> GetAllClientsByUsername(string username, out string errorMessage)
        {
            List<Client> allClients = new List<Client>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM postgres.cyberschema1.clients WHERE client_username = (@username)";
                    command.Parameters.AddWithValue("username", username);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Client client = new Client();
                        allClients.Add(client.ClientMaker(reader));
                    }

                    return allClients;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public List<Client> GetAllClientsByUsernameVulnerable(string username, out string errorMessage)
        {
            List<Client> allClients = new List<Client>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();


                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM postgres.cyberschema1.clients WHERE client_username = '" + username + "'";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Client client = new Client();
                        allClients.Add(client.ClientMaker(reader));
                    }

                    return allClients;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public int AddNewClient(Client newClient, out string errorMessage)
        {
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;                                     
                
                    command.CommandText =
                        "INSERT INTO postgres.cyberschema1.clients (org_id, name, surname, client_username) VALUES (@org_id, @name, @surname, @client_username) RETURNING client_id";
                    newClientCommandHelper(newClient, command);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
    }
}