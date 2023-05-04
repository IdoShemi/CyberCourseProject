using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Net;
using MatakDBConnector;
namespace Project1
{
    public partial class forgotPassword : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetCode_func(object sender, EventArgs e)
        {
            // Response Redirect
            string mail = email.Text.ToString();
            if (!IsValidEmail(mail))
            { // change to text label
                PrintError("Not Valid");
                return;
            }

            // preparation - create user model objects
            User mtkUser = new User();
            UserModel mtkUserModel = new UserModel();
            string outString = "";
            mtkUser = mtkUserModel.GetUserByEmail(mail, out outString);
            if (mtkUser == null) // check if mail exits in database 
            {
                PrintError("Mail Not Found");
                return;
            }

            // send mail and redirect to verification page
            string confirmationString = EmailSender.SendEmail(mail);
            Session["confirmationString"] = confirmationString; // change to db instead of session
            Session["userMail"] = mail;
            Response.Redirect("verification.aspx");

        }


        bool IsValidEmail(string email)
        { 
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(email);
        }

        protected void PrintError(string err)
        {
            err_label.Visible = true;
            err_label.Text = err;
        }
    }
}