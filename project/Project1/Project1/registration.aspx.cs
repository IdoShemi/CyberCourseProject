using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MatakDBConnector;
namespace Project1
{
    public partial class registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // input validation - part 2
            // password verification - config file
            // registration
            string password = passwordBox.Text;
            string error = "";
            if (PasswordChecker.CheckPassword(password, out error))
            {
                err_label.Visible = false;
                err_label.Text = "good pass";
            }
            else
            {
                PrintError(error);
                return;
            }
            // verification checking
            if (password != passwordVerifyBox.Text)
            {
                PrintError("passwords are not the same");
                return;
            }

            // add to class the login attempts code.


            // inserting database:
            string outString = "";
            string hashedPassword = PasswordJsonHandler.GetHashedPassword(password);
            PasswordJsonHandler p = new PasswordJsonHandler();
            p.InsertPassword(hashedPassword);
            string pj = p.BuildJson();
            UserModel mtkUserModel = new UserModel();
            User mtkNewUser = new User(0, pj, 0, last_name.Text, first_name.Text, 1, 1, mailBox.Text, "nickname",DateTime.Now, 0);
            mtkUserModel.AddNewUser(mtkNewUser, out outString);
        }

        protected void PrintError (string error)
        {
            err_label.Visible = true;
            err_label.Text = error;
        }
    }
}