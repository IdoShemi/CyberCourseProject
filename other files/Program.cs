﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//important includings
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;// install on the project
using System.Text.Json;
using Microsoft.Extensions.Configuration; // make sure to manage nuget packages Microsoft.Extensions.Configuration.Json and Microsoft.Extensions.Configuration
using Newtonsoft.Json.Linq;

namespace ConsoleApp3
{
    public class UniqueString // use instead guid
    {
        static Random random = new Random();
        public static string GetUniqueValue(int length = 6)
        {
            Guid guid = Guid.NewGuid();
            byte[] guidBytes = guid.ToByteArray();
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] hash = sha1.ComputeHash(guidBytes);
            string fullCode = BitConverter.ToString(hash).Replace("-", "");
            string Verify_Code = fullCode.Substring(0, length);
            return Verify_Code;
        }
    }
 


    public class PasswordJsonHandler
    {
        private string[] passwords;
        private static int length = 10;

        /// <summary>
        /// creating passwords array from json file, if not entered json string it is considered as new account
        /// </summary>
        /// <param name="passwordsJson"></param>
        public PasswordJsonHandler(string passwordsJson = "")
        {
            this.passwords = new string[length];
            if (passwordsJson != "")
                ReadFromJson(passwordsJson);
        }

        public void InsertPassword(string new_password)
        {
            // we have the passwords array with all the previous passwords (can be without any password)
            for(int i = length-1; i>= 1; i--)
                this.passwords[i] = this.passwords[i - 1];
            this.passwords[0] = new_password;
        }

        public void ReadFromJson(string passwordsJson) 
        {
            // Parse the JSON string into a JsonDocument
            JsonDocument doc = JsonDocument.Parse(passwordsJson);

            // Get the JsonElement representing the root object
            JsonElement root = doc.RootElement;

            // Get the array of passwords
            JsonElement passwordsArray = root.GetProperty("passwords");

            int i = 0;
            // Iterate over the array elements and add them to the list
            foreach (JsonElement element in passwordsArray.EnumerateArray())
            {
                this.passwords[i++] = element.GetString();
            }
        }

        public string BuildJson()
        {
            // Create a JSON object with a "passwords" property containing the passwords list
            JObject jsonObject = new JObject();
            jsonObject.Add("passwords", new JArray(passwords));

            // Serialize the JSON object to a JSON string
            string jsonString = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

            return jsonString; 
        }

        public void PrintPassword()
        {
            // Print out the passwords array
            foreach (string password in passwords)
            {
                Console.WriteLine(password);
            }
        }


        /// <summary>
        /// checks whether the password was in the last passwords according to the password configuration file.
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool CompareNewPassword(string pass)
        {
            // add code to read the history field from the configuration
            for (int i = 0; i < length; i++)
                if (passwords[i] == pass)
                    return false;
            return true;
        }

        /// <summary>
        /// for login, check if the password matches
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool CompareCurrentPassword(string pass)
        {
            return passwords[0] == GetHashedPassword(pass);
        }


        private static string GetHashedPassword(string password)
        {
            //  we need to add the function that reads the salt string from the database because it's not secured to put it in the code

            // Generate a random salt
            // add the reading from the sql
            byte[] salt = Encoding.UTF8.GetBytes("MySalt123");

            // Hash the password with the salt using the PBKDF2 algorithm
            byte[] hashedPassword;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                hashedPassword = pbkdf2.GetBytes(32); // 256-bit key
            }

            // Convert the hashed password and salt to strings
            string hashedPasswordString = Convert.ToBase64String(hashedPassword);
            string saltString = Convert.ToBase64String(salt);

            // Output the hashed password and salt
            Console.WriteLine("Hashed password: " + hashedPasswordString);
            Console.WriteLine("Salt: " + saltString);
            return hashedPasswordString;
        }
    }



    public class EmailSender
    {
        public static void SendEmail(string to) // make sure that string to is from the database
        {
            string confirmationString = UniqueString.GetUniqueValue();
            string subject = "Password recovery code";
            string body = $"hi \n your password recovery code is: {confirmationString}";
            try
            {
                IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("TopSecretDoNotOpen.json", optional: true, reloadOnChange: true)
                    .Build();

                string email = config["Email"];
                string password = config["EmailPassword"];

                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(email);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;

                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential(email, password);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {// getting from the sql
            string passwordsJson = @"{ 
                ""passwords"": [
                    ""pass1"",
                    ""pass2"",
                    ""pass3""
                ]
            }";
            PasswordJsonHandler passwords = new PasswordJsonHandler();
            passwords.InsertPassword("pass1");
            passwords.PrintPassword();
            string j = passwords.BuildJson();
            Console.WriteLine(j);
            // login - json handler, than comparing passwords
            // inserting - json handler, inserting and comparing
            // new account -- json handler without argument, insert password and build json
        }
    }
}
