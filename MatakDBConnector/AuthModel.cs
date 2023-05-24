using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class AuthModel : Auth
    {
        public List<Auth> GetAllAuths(out string errorMessage)
        {
            List<Auth> allAuths = new List<Auth>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM postgres.cyberschema1.auth_verification";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Auth auth = new Auth();
                        allAuths.Add(auth.AuthMaker(reader));
                    }

                    return allAuths;
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