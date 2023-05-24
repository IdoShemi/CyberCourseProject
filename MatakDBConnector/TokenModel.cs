using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class TokenModel : Token
    {
        public List<Token> GetAllTokens(out string errorMessage)
        {
            List<Token> allTokens = new List<Token>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM postgres.cyberschema1.tokens";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Token token = new Token();
                        allTokens.Add(token.TokenMaker(reader));
                    }

                    return allTokens;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        
        public List<Token> GetAllTokensByEmail(string email, out string errorMessage)
        {
            List<Token> allTokens = new List<Token>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM postgres.cyberschema1.tokens WHERE email = (@email)";
                    command.Parameters.AddWithValue("email", email);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Token token = new Token();
                        allTokens.Add(token.TokenMaker(reader));
                    }

                    return allTokens;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public List<Token> GetAllEmailsByToken(string tokenString, out string errorMessage)
        {
            List<Token> allTokens = new List<Token>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM postgres.cyberschema1.tokens WHERE token = (@token)";
                    command.Parameters.AddWithValue("token", tokenString);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Token token = new Token();
                        allTokens.Add(token.TokenMaker(reader));
                    }

                    return allTokens;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public int AddNewTokenByEmail(Token newToken, out string errorMessage)
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
                        "INSERT INTO postgres.cyberschema1.tokens (email, token) VALUES (@email, @token) RETURNING token_id";
                    newTokenCommandHelper(newToken, command);

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
        
        public void DeleteTokenByEmail(string email, out string errorMessage)
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
                        "DELETE FROM postgres.cyberschema1.tokens WHERE email = (@email)";
                    command.Parameters.AddWithValue("email", email);
                    command.ExecuteNonQuery();
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