using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MatakDBConnector;
namespace Project1
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // check mail and password - input validation - check if necessary
            // make sure to import the handle password library and use it
            // sql 

            // preparation - create user model objects
            User mtkUser = new User();
            UserModel mtkUserModel = new UserModel();
            string outString = "";

            // check for the login attempts - if we need to count it in a database because changing mail can reset on local code. - elaborate to lecturer

            // part 1 - check if user exists in db
            string mail = mailBox.Text;
            string pass = passwordBox.Text;
            mtkUser = mtkUserModel.GetUserByEmail(mail, out outString);
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
            if(loginAttempts <= userLoginAttempts)
            {
                if(timedifference.TotalSeconds < SUSPENDED_TIME)
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
            if (!handler.CompareCurrentPassword(pass))
            {
                mtkUserModel.SetLoginAttempts(mail, userLoginAttempts+1, out outString);
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