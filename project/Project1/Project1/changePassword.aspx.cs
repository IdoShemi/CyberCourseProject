using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MatakDBConnector;
namespace Project1
{
    public partial class changePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnChangePassword_click(object sender, EventArgs e)
        {
            string userMail = Session["userEmail"].ToString();
            string password = currentPasswordBox.Text;
            string newPassword = newPasswordBox.Text;
            string confirmNewPassword = confirmNewPasswordBox.Text;

            User mtkUser = new User();
            UserModel mtkUserModel = new UserModel();
            string outString = "";

            mtkUser = mtkUserModel.GetUserByEmail(userMail, out outString);

            if(mtkUser == null)
            {
                PrintError("Error Occured");
                return;
            }

            PasswordJsonHandler handler = new PasswordJsonHandler(mtkUser.Password);

            if (!handler.CompareCurrentPassword(password))
            {
                PrintError("One Of The Fields Is Incorrect");
                return;
            }

            if (!PasswordChecker.CheckPassword(newPassword, out outString,handler.BuildJson()))
            {
                PrintError(outString);
                return;
            }

            // check if password matches
            if (newPassword != confirmNewPassword)
            {
                PrintError("passwords are not the same");
                return;
            }

            // building the new json password
            string hashedPassword = PasswordJsonHandler.GetHashedPassword(newPassword);
            PasswordJsonHandler p = new PasswordJsonHandler(mtkUser.Password);
            p.InsertPassword(hashedPassword);
            string pj = p.BuildJson();
            mtkUserModel.SetPasswordJson(userMail, pj, out outString);
            Response.Redirect("SystemScreen.aspx");


        }

        protected void PrintError(string err)
        {
            errorLabel.Visible = true;
            errorLabel.Text = err;
        }
    }
}