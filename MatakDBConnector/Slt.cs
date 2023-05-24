using Npgsql;

namespace MatakDBConnector
{
    public class Slt
    {
        private string _salt;

        public Slt()
        {
            _salt = "0";
        }

        public Slt(string salt, string password)
        {
            _salt = salt;


        }
        
        public Slt SltMaker(NpgsqlDataReader reader)
        {
            Slt slt = new Slt();
            
            slt.Salt = reader.GetString(0);

            return slt;
        }
        
        public string Salt
        {
            get => _salt;
            set => _salt = value;
        }
        
    }
}