using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Net;

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
                email.Text = "not valid";
                return;
            }
            //if () // check if exits in database -- wait for matakdb from oleg
            //{
            //    return;
            //}
            // send mail and redirect to verification page
            // get mail credentials from database -- wait for oleg, for now we can insert the crenditials non secure

            string confirmationString = EmailSender.SendEmail(mail);
            Session["confirmationString"] = confirmationString;

            //            Session["confirmationString"] = confirmationString;
            // don't forget to check in the verification page

            Response.Redirect("verification.aspx");

        }


        bool IsValidEmail(string email)
        { // we used regex because third part library did not work
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(email);
        }
    }
}