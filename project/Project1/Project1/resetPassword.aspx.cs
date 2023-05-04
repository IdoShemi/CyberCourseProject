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
            // not tested, after db modification test it !!!!
            // get token from db
            // get user mail by token
            string mail = "";


            // preparation - create user model objects
            User mtkUser = new User();
            UserModel mtkUserModel = new UserModel();
            string outString = "";
            //string mail = Session["userMail"].ToString();

            // part 1 - get the username from db
            mtkUser = mtkUserModel.GetUserByEmail(mail, out outString);

            // part 2 - check entered password using password handler
            string pass = passBox.Text;
            string verifyPass = verifyPassBox.Text;
            PasswordJsonHandler handler = new PasswordJsonHandler(mtkUser.Password);
            if (!PasswordChecker.CheckPassword(pass, out outString))
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
            Response.Redirect("SystemScreen.aspx");


        }

        protected void PrintError(string err)
        {
            err_label.Visible = true;
            err_label.Text = err;
        }
    }
}