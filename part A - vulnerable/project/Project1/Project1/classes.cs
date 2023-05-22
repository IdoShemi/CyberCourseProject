using System;
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
using MatakDBConnector;
using System.IO;
using System.Web.Hosting;

namespace Project1
{
    public class UniqueString // use instead guid
    {
        static Random random = new Random();
        public static string GetUniqueValue()
        {
            Guid guid = Guid.NewGuid();
            byte[] guidBytes = guid.ToByteArray();
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] hash = sha1.ComputeHash(guidBytes);
            string fullCode = BitConverter.ToString(hash).Replace("-", "");
            return fullCode;
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
            for (int i = length - 1; i >= 1; i--)
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
        public bool CompareNewPassword(string pass, int history)
        {
            for (int i = 0; i < history; i++)
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


        internal static string GetHashedPassword(string password)
        {
            //  we need to add the function that reads the salt string from the database because it's not secured to put it in the code
            SltModel mtkSltModel = new SltModel(); // AuthModel contains all methods to work with email verification (auth_verification) table
            string outString = "";
            List<Slt> slts = new List<Slt>(); // explained above why it's a list
            slts = mtkSltModel.GetAllSlt(out outString);


            byte[] salt = Encoding.UTF8.GetBytes(slts[0].Salt);

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
            //Console.WriteLine("Hashed password: " + hashedPasswordString);
            //Console.WriteLine("Salt: " + saltString);
            return hashedPasswordString;
        }

        public void DeleteFirst()
        {
            for (int i = 0; i < length-1; i++)
                this.passwords[i] = this.passwords[i + 1];
            this.passwords[length - 1] = "";
        }
    }



    public class EmailSender
    {
        public static string SendEmail(string to) // make sure that string to is from the database
        {
            string confirmationString = UniqueString.GetUniqueValue();
            string subject = "Password recovery code";
            string body = $"hi \n your password recovery code is: {confirmationString}";
            try
            {

                AuthModel mtkAuthModel = new AuthModel(); // AuthModel contains all methods to work with email verification (auth_verification) table
                string outString = "";
                List<Auth> auths = new List<Auth>(); // explained above why it's a list
                auths = mtkAuthModel.GetAllAuths(out outString);
                // get from db -- change
                string email = auths[0].Email;
                string password = auths[0].Password;

                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                mail.From = new MailAddress(email);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;

                smtpServer.EnableSsl = true;
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential(email, password);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                throw;
            }

            return confirmationString;
        }
    }

    /*
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

            PasswordJsonHandler passwords = new PasswordJsonHandler(passwordsJson);
            passwords.InsertPassword("hello");
            passwords.PrintPassword();
            string j = passwords.BuildJson();
            Console.WriteLine(j);
            // login - json handler, than comparing passwords
            // inserting - json handler, inserting and comparing
            // new account -- json handler without argument, insert password and build json
        }
    }
    */



    public class PasswordChecker
    {

        internal static bool CheckPassword(string password, out string error, string passwordsJson = "")
        {
            error = "";
            // Read the JSON file into a string
            var filePath = HostingEnvironment.MapPath("~/password_config.json");

            var jsonString = File.ReadAllText(filePath);

            // Parse the JSON string into a JsonDocument
            var jsonDoc = JsonDocument.Parse(jsonString);

            // Get the root element of the JsonDocument
            var rootElement = jsonDoc.RootElement;

            // Read the values from the root element and assign them to C# variables
            int passwordLength = rootElement.GetProperty("passwordLength").GetInt32();
            bool complexPassword = rootElement.GetProperty("complexPassword").GetBoolean();
            int passwordHistory = rootElement.GetProperty("passwordHistory").GetInt32();
            bool preventDictionaryAttack = rootElement.GetProperty("preventDictionaryAttack").GetBoolean();
            int loginAttempts = rootElement.GetProperty("loginAttempts").GetInt32();


            // Dispose the JsonDocument to release resources
            jsonDoc.Dispose();


            // בדיקה שהסיסמא תואמת לדרישות המוגדרות בקובץ הקונפיגורציה
            bool isValidPassword = true;

            if (password.Length < passwordLength)
            {
                error = ($"Your password must be at least {passwordLength} characters long.");
                return false;
            }

            if (complexPassword)
            {
                int[] countArr = new int[4] { 0, 0, 0, 0 };
                foreach (char c in password)
                {
                    if (char.IsDigit(c))
                        countArr[0] = 1;
                    else if (char.IsUpper(c))
                        countArr[1] = 1;
                    else if (char.IsLower(c))
                        countArr[2] = 1;
                    else
                        countArr[3] = 1;
                }
                if (countArr.Sum() < 3)
                {
                    error = ($"Your password must be Contain 3 of the 4 types of characters");
                    return false;
                }
            }

            // בדיקת היסטוריית הסיסמאות
            PasswordJsonHandler passwords = new PasswordJsonHandler(passwordsJson);
            // get the json from the data or get empty when it is new user
            string hashedpassword = PasswordJsonHandler.GetHashedPassword(password);
            if (!passwords.CompareNewPassword(hashedpassword, passwordHistory))
            {
                error = ("Your password cannot be the same as your previous passwords.");
                return false;
            }

            // מניעת התקפות מילון
            if (preventDictionaryAttack && IsCommonPassword(password))
            {
                error = ("common password. Please choose a different password.");
                return false;
            }

            return true;
        }






        static bool IsCommonPassword(string password)
        {
            // Read the list of common passwords from the file
            
            using (StreamReader sr = new StreamReader(HostingEnvironment.MapPath("~/commonPasswords.txt")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    if (line == password)
                        return true;
            }

            return false;
        }
    }
}
