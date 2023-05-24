using System;
using Npgsql;

namespace MatakDBConnector
{
    public class Permission
    {
        public static void getPermissionID(string description)
        {

            var connString =
                "Host=matakdev.clmkyptnunzo.eu-west-1.rds.amazonaws.com;Username=Gaidax;Password=zIfvuj-dyfti7-dyffet;Database=playground";

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Successfully opened postgreSQL connection.");
                }
                else
                {
                    Console.WriteLine("Failed to open postgreSQL connection.");
                }


                using (var cmd =
                    new NpgsqlCommand("SELECT priority_id, description, created FROM priority WHERE priority_id = 1",
                        conn))
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nExporting priority table entries: \n");
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} | {1} | {2}", reader.GetDataTypeName(0),
                            reader.GetDataTypeName(1), reader.GetDataTypeName(2));
                    }
                }

                using (var cmd =
                    new NpgsqlCommand(
                        "SELECT priority_id, description, created, updated FROM priority ORDER BY priority_id", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} | {1} | {2} ", reader.GetInt32(0), reader.GetString(1),
                            reader.GetDateTime(2));
                    }
                }


                Console.WriteLine("Task is done, closing postgreSQL connection");
                conn.Close();



            }


        }



        /*
        int result = 0;
        
        //DBConnector.Connect();

        
        
        
        using (var cmd = new NpgsqlCommand())
        {
            try
            {
                cmd.Connection = DBConnector.connection;
                cmd.CommandText = "SELECT priority_id, description, created FROM priority WHERE priority_id = 1";
                
                //cmd.CommandText = "SELECT priority_id FROM priority WHERE description = (@p)";
                //cmd.Parameters.AddWithValue("p", description);
                var reader = cmd.ExecuteReader();
                Console.WriteLine("\nExporting priority table entries: \n");
                while (reader.Read())
                {
                    Console.WriteLine("{0} | {1} | {2}", reader.GetDataTypeName(0),
                        reader.GetDataTypeName(1), reader.GetDataTypeName(2));
                }
                /*
                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
        
        //DBConnector.Disconnect();
        //return result;
    }
    */
        
    }
}