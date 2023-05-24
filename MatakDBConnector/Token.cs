using System;
using Npgsql;

namespace MatakDBConnector
{
    public class Token
    {
        private int _tokenId;
        private string _email;
        private string _token;



        public Token()
        {
            _tokenId = 0;
            _email = "0";
            _token = "0";
        }

        public Token(int tokenId, string email, string token)
        {
            _tokenId = tokenId;
            _email = email;
            _token = token;
        }
        
        public Token TokenMaker(NpgsqlDataReader reader)
        {
            Token token = new Token();
            
            token.ClientId = reader.GetInt32(0);
            token.Email = reader.GetString(1);
            token.TokenSetter = reader.GetString(2);

            return token;
        }
        
        protected void newTokenCommandHelper(Token token, NpgsqlCommand command)
        {
            command.Parameters.AddWithValue("email", token.Email);
            command.Parameters.AddWithValue("token", token.TokenSetter);
        }

        public int ClientId
        {
            get => _tokenId;
            set => _tokenId = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string TokenSetter
        {
            get => _token;
            set => _token = value;
        }
    }
}