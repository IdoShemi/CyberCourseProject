using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project1
{
    public partial class verification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetCode_func(object sender, EventArgs e)
        {
            if (VerificationCode.Text.ToString() == Session["confirmationString"].ToString())
                Response.Redirect("systemScreen.aspx");
        }
    }
}