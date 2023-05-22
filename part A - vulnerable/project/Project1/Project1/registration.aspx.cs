using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            // input validation - fields
            string error = "";
            //check first name
            if (!CheckTextField(first_name.Text, "First Name", out error))
            {
                PrintError(error);
                return;
            }
            // check last name
            if (!CheckTextField(last_name.Text, "Last Name", out error))
            {
                PrintError(error);
                return;
            }
            //check mail - for part 2 
            //if (!IsValidEmail(mailBox.Text))
            //{
            //    PrintError("Mail is Invalid");
            //    return;
            //}
            //check if mail exists
            UserModel mtkUserModel = new UserModel();
            User mtkUserChecker;
            string mail = mailBox.Text;
            mtkUserChecker = mtkUserModel.GetUserByEmailVulnerable(mail, out error);
            if (mtkUserChecker != null)
            {
                PrintError("mail exists"); // check that message
                return;
            }


            // password verification - config file
            // registration
            string password = passwordBox.Text;
            if (!PasswordChecker.CheckPassword(password, out error))
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

            // inserting database:
            string outString = "";
            string hashedPassword = PasswordJsonHandler.GetHashedPassword(password);
            PasswordJsonHandler p = new PasswordJsonHandler();
            p.InsertPassword(hashedPassword);
            string pj = p.BuildJson();
            User mtkNewUser = new User(0, pj, 0, last_name.Text, first_name.Text, 1, 1, mail, "nickname",DateTime.Now, 0);
            mtkUserModel.AddNewUser(mtkNewUser, out outString);
            Response.Redirect("index.aspx");
        }

        protected bool CheckTextField(string text, string fieldName, out string error)
        {// this function checks for general input validation such as length.
            if(String.IsNullOrEmpty(text))
            {
                error = $"{fieldName} Invalid: The field is required.";
                return false;
            }

            // input validation for part 2 !
            //string pattern = "^[a-zA-Z]+$";
            
            //// Check if the input matches the pattern
            //if (!Regex.IsMatch(text, pattern))
            //{
            //    error = $"{fieldName} Invalid: Non-letter characters found.";
            //    return false;
            //}
            error = "";
            return true;
        }

        protected bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(email);
        }

        protected void PrintError (string error)
        {
            err_label.Visible = true;
            err_label.Text = error;
        }
    }
}