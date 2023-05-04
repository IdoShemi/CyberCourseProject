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
            string token = "";
            string verificationCode = VerificationCode.Text.ToString();
            // add reading token
            // check if not null because token is randomly generated
            //if ( ) // read token from db and insert
                Response.Redirect("systemScreen.aspx?t="+ token);
        }
    }
}