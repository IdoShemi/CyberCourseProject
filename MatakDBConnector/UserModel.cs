using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class UserModel : User
    {
        public List<User> GetAllUsers(out string errorMessage)
        {
            List<User> allUsers = new List<User>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM postgres.cyberschema1.user";
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        allUsers.Add(user.UserMaker(reader));
                    }

                    return allUsers;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public User GetUserByUserId(int userId, out string errorMessage)
        {
            errorMessage = null;
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM postgres.cyberschema1.user WHERE user_id = (@userId)";
                    command.Parameters.AddWithValue("userId", userId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        return UserMaker(reader);
                    }

                    errorMessage = "User not found";
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public User GetUserByEmail(string email, out string errorMessage)
        {
            errorMessage = null;
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM postgres.cyberschema1.user WHERE email = (@email)";
                    command.Parameters.AddWithValue("email", email);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        return UserMaker(reader);
                    }

                    errorMessage = "User not found";
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public List<User> GetAllUsersByOrgId(int orgId, out string errorMessage)
        {
            List<User> allUsers = new List<User>();
            errorMessage = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT * FROM postgres.cyberschema1.user WHERE org_id = (@p)";
                    command.Parameters.AddWithValue("p", orgId);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        allUsers.Add(user.UserMaker(reader));
                    }

                    return allUsers;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public string GetPasswordJson(string email, out string errorMessage)
        {
            errorMessage = null;
            string result = null;
            string commandEntry = "SELECT password FROM postgres.cyberschema1.user WHERE email = '" + email + "'";

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    //command.CommandText = "SELECT password FROM postgres.cyberschema1.user WHERE nickname = '(@p)'";
                    //command.Parameters.AddWithValue("p", username);
                    command.CommandText = commandEntry;
                    NpgsqlDataReader reader = command.ExecuteReader();
                
                    while (reader.Read())
                    {
                        result = reader.GetString(0);
                    }
                    
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public string SetPasswordJson(string email, string passwordJson, out string errorMessage)
        {
            errorMessage = null;
            string result = null;
            string commandEntry = "UPDATE postgres.cyberschema1.user SET password = '" + passwordJson + "' WHERE email ='" + email + "'";

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    //command.CommandText = "UPDATE postgres.cyberschema1.user SET password = (@passwordJson) WHERE nickname = (@username)";
                    //command.Parameters.AddWithValue("passwordJson", passwordJson);
                    //command.Parameters.AddWithValue("nickname", username);
                    command.CommandText = commandEntry;
                    NpgsqlDataReader reader = command.ExecuteReader();
                
                    while (reader.Read())
                    {
                        result = reader.GetString(0);
                    }
                    
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public int AddNewUser(User newUser, out string errorMessage)
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
                        "INSERT INTO postgres.cyberschema1.user (password, phone_id, last_name, first_name, permission_id, org_id, email, nickname, last_login, login_attempts) VALUES (@password, @phone_id, @last_name, @first_name, @permission_id, @org_id, @email, @nickname, @last_login, @login_attempts) RETURNING user_id";
                    newUserCommandHelper(newUser, command);

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
        
        public DateTime GetLastLoginDateTime(string email, out string errorMessage)
        {
            errorMessage = null;
            DateTime result = DateTime.Now;
            string commandEntry = "SELECT last_login FROM postgres.cyberschema1.user WHERE email = '" + email + "'";

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    //command.CommandText = "SELECT password FROM postgres.cyberschema1.user WHERE nickname = '(@p)'";
                    //command.Parameters.AddWithValue("p", username);
                    command.CommandText = commandEntry;
                    NpgsqlDataReader reader = command.ExecuteReader();
                
                    while (reader.Read())
                    {
                        result = reader.GetDateTime(0);
                    }
                    
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public string SetLastLogin(string email, DateTime lastLogin, out string errorMessage)
        {
            errorMessage = null;
            string result = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "UPDATE postgres.cyberschema1.user SET last_login = (@lastLogin) WHERE email = (@email)";
                    command.Parameters.AddWithValue("lastLogin", lastLogin);
                    command.Parameters.AddWithValue("email", email);

                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result = reader.GetString(0);
                    }
                    
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public int GetLoginAttempts(string email, out string errorMessage)
        {
            errorMessage = null;
            int result = -1;
            string commandEntry = "SELECT login_attempts FROM postgres.cyberschema1.user WHERE email = '" + email + "'";

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = commandEntry;
                    NpgsqlDataReader reader = command.ExecuteReader();
                
                    while (reader.Read())
                    {
                        result = reader.GetInt32(0);
                    }
                    
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public string SetLoginAttempts(string email, int loginAttempts, out string errorMessage)
        {
            errorMessage = null;
            string result = null;

            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "UPDATE postgres.cyberschema1.user SET login_attempts = (@loginAttempts) WHERE email = (@email)";
                    command.Parameters.AddWithValue("loginAttempts", loginAttempts);
                    command.Parameters.AddWithValue("email", email);

                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result = reader.GetString(0);
                    }
                    
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }

        public bool VerifyEmailPasswordMatch(string email, string password, out string errorMessage)
        {
            errorMessage = null;
            object result;
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = "SELECT COUNT(*) FROM postgres.cyberschema1.user WHERE email = (@email) AND password = (@password)";
                    command.Parameters.AddWithValue("email", email);
                    command.Parameters.AddWithValue("password", password);
                    result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int count = Convert.ToInt32(result);
                        return count > 0;
                    }

                    errorMessage = "User/Password combination not found";
                    return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    errorMessage = e.ToString();
                    throw;
                }
            }
        }
        
        public bool VerifyEmailPasswordMatchVulnerable(string email, string password, out string errorMessage)
        {
            errorMessage = null;
            object result;
            string commandEntry = "SELECT COUNT(*) FROM postgres.cyberschema1.user WHERE email = '" + email + "' AND password = '" + password + "'";
            
            using (var connection = new NpgsqlConnection(ConfigParser.ConnString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    command.CommandText = commandEntry;
                    result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int count = Convert.ToInt32(result);
                        return count > 0;
                    }

                    errorMessage = "User/Password combination not found";
                    return false;
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