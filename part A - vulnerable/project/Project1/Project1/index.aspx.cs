using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MatakDBConnector;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
namespace Project1
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // preparation
            User mtkUser = new User();
            UserModel mtkUserModel = new UserModel();
            string outString = "";


            // part 1 - check if user exists in db
            string mail = mailBox.Text;
            string pass = passwordBox.Text;
            mtkUser = mtkUserModel.GetUserByEmailVulnerable(mail, out outString); // change to Vulnerable ASAP!!!!
            if (mtkUser == null)
            {
                PrintError();
                return;
            }

            // part 1.2 - check if not suspended and print error
            // add acount suspended error, check the attempts and if so check time. if time is bigger then suspension time
            // reset the login attempts. if not print suspended error and return. 
            int loginAttempts = GetLoginAttemptsFromJson();
            int userLoginAttempts = mtkUser.LoginAttempts;
            DateTime lastLogin = mtkUser.Lastlogin;
            DateTime now = DateTime.Now;
            TimeSpan timedifference = now - lastLogin;
            const int SUSPENDED_TIME = 60;
            if (loginAttempts <= userLoginAttempts)
            {
                if (timedifference.TotalSeconds < SUSPENDED_TIME)
                {
                    err_label.Visible = true;
                    err_label.Text = "ACCOUNT SUSPENDED, try again later";
                    return;
                }
                else
                {
                    mtkUserModel.SetLoginAttempts(mail, 0, out outString);
                    userLoginAttempts = 0;
                    err_label.Visible = false;
                }
            }

            mtkUserModel.SetLastLogin(mail, now, out outString);



            // part 2 - check if password matches
            PasswordJsonHandler handler = new PasswordJsonHandler(mtkUser.Password);

            // secured part 
            //if (!handler.CompareCurrentPassword(pass))
            //{
            //    mtkUserModel.SetLoginAttempts(mail, userLoginAttempts + 1, out outString);
            //    PrintError();
            //    return;
            //}

            // vulnerable way to check password
            handler.DeleteFirst();
            string hashedPassword = PasswordJsonHandler.GetHashedPassword(pass);
            handler.InsertPassword(hashedPassword);
            string pj = handler.BuildJson();

            bool result = mtkUserModel.VerifyEmailPasswordMatchVulnerable(mail, pj, out outString);
            //Response.Write("hellO"+result + "\n"+ outString);

            if (!result)
            {
                mtkUserModel.SetLoginAttempts(mail, userLoginAttempts + 1, out outString);
                PrintError();
                return;
            }

            // connected
            mtkUserModel.SetLoginAttempts(mail, 0, out outString);
            Session["userEmail"] = mtkUser.Email;
            Response.Redirect("SystemScreen.aspx");

        }

        protected void PrintError()
        {
            err_label.Visible = true;
            err_label.Text = "One Of The Credentials Is Incorrect";
        }


        protected int GetLoginAttemptsFromJson()
        {
            var filePath = Server.MapPath("~/password_config.json");

            var jsonString = File.ReadAllText(filePath);

            // Parse the JSON string into a JsonDocument
            var jsonDoc = JsonDocument.Parse(jsonString);

            // Get the root element of the JsonDocument
            var rootElement = jsonDoc.RootElement;
            int loginAttempts = rootElement.GetProperty("loginAttempts").GetInt32();
            jsonDoc.Dispose();
            return loginAttempts;
        }
    }
}