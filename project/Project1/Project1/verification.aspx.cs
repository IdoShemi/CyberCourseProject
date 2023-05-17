using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MatakDBConnector;
namespace Project1
{
    public partial class verification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetCode_func(object sender, EventArgs e)
        {
            string outString = "";

            string token = "";
            string verificationCode = VerificationCode.Text.ToString();

            TokenModel mtkTokenModel = new TokenModel();  // Client Model contains all methods for client table manipulation
            string mail = Session["mail"].ToString();
            // reading token
            List<Token> tokens = new List<Token>();
            tokens = mtkTokenModel.GetAllTokensByEmail(mail, out outString);

            // if somehow the mail field in the session changed and doesn't have code
            if(tokens.Count == 0)
            {
                Response.Redirect("forgotPassword.aspx");
            }

            // checking token
            if (tokens[0].TokenSetter == verificationCode)
                Response.Redirect("resetPassword.aspx?t="+ token);

            err_label.Visible = true;
            err_label.Text = "verification code is not valid";
        }
    }
}