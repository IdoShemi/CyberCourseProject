using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class SltModel : Slt
    {
        public List<Slt> GetAllSlt(out string errorMessage)
        {
            List<Slt> allSlt = new List<Slt>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM postgres.cyberschema1.slt";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Slt slt = new Slt();
                        allSlt.Add(slt.SltMaker(reader));
                    }

                    return allSlt;
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