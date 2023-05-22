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
            if (!IsValidEmail(mail)) // if shomehow there is invalid mail in db.
            { 
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
            TokenModel mtkTokenModel = new TokenModel();  // Client Model contains all methods for client table manipulation

            // part 1 - delete previous tokens
            mtkTokenModel.DeleteTokenByEmail(mail, out outString);
            // part 2 - add the new token
            Token mtkNewToken = new Token(0, mail, confirmationString); // create new token object, first is ID that can be whatever not used
            mtkTokenModel.AddNewTokenByEmail(mtkNewToken, out outString);

            Session["mail"] = mail;
            Response.Redirect("verification.aspx"); // redirecting to change password page

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