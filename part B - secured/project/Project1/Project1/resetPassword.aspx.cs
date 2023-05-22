using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MatakDBConnector;
namespace Project1
{
    public partial class resetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ChangePass_Click(object sender, EventArgs e)
        {
            string outString = "";

            // not tested, after db modification test it !!!!

            string token = Request.QueryString["t"];
            TokenModel mtkTokenModel = new TokenModel();  // Client Model contains all methods for client table manipulation
            // reading token
            List<Token> tokens = new List<Token>();
            tokens = mtkTokenModel.GetAllEmailsByToken(token, out outString);
            string mail = tokens[0].Email; 

            // preparation - create user model objects
            User mtkUser = new User();
            UserModel mtkUserModel = new UserModel();

            // part 1 - get the username from db
            mtkUser = mtkUserModel.GetUserByEmail(mail, out outString);

            // part 2 - check entered password using password handler
            string pass = passBox.Text;
            string verifyPass = verifyPassBox.Text;
            PasswordJsonHandler handler = new PasswordJsonHandler(mtkUser.Password);
            if (!PasswordChecker.CheckPassword(pass, out outString, handler.BuildJson()))
            {
                PrintError(outString);
                return;
            }


            // check if password matches
            if (pass != verifyPass)
            {
                PrintError("passwords are not the same");
                return;
            }

            // building the new json password
            string hashedPassword = PasswordJsonHandler.GetHashedPassword(pass);
            PasswordJsonHandler p = new PasswordJsonHandler(mtkUser.Password);
            p.InsertPassword(hashedPassword);
            string pj = p.BuildJson();
            mtkUserModel.SetPasswordJson(mail, pj, out outString);
            mtkTokenModel.DeleteTokenByEmail(mail, out outString);
            Response.Redirect("SystemScreen.aspx");


        }

        protected void PrintError(string err)
        {
            err_label.Visible = true;
            err_label.Text = err;
        }
    }
}