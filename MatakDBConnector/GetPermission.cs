using System;
using Npgsql;

namespace MatakDBConnector
{
    public class GetPermission
    {
        public static void GetPermissionId(string description, out string errorMessage)
        {
            errorMessage = null;
            DbConnector testConnector = new DbConnector();

            testConnector.Connect();
            
            var cmd = new NpgsqlCommand();
            try
            {
                cmd.Connection = testConnector.Connection;
                cmd.CommandText = "SELECT priority_id, description, created FROM priority WHERE priority_id = 1";
                //TODO modify following examples
                //cmd.CommandText = "SELECT priority_id FROM priority WHERE description = (@p)";
                //cmd.Parameters.AddWithValue("p", description);
                var reader = cmd.ExecuteReader();
                Console.WriteLine("\nExporting priority table entries: \n");
                while (reader.Read())
                {
                    Console.WriteLine("{0} | {1} | {2}", reader.GetDataTypeName(0),
                        reader.GetDataTypeName(1), reader.GetDataTypeName(2));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            

            testConnector.Disconnect();
        }
    }
}