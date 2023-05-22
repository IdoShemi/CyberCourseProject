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
    public partial class SystemScreen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {   // check if connected 


        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("index.aspx");
        }
        protected void btnAddClient_click(object sender, EventArgs e)
        {

            string outString = "";
            // input validation in part 2
            //check first name
            if (!CheckTextField(firstNameBox.Text, "First Name", out outString))
            {
                PrintError(outString);
                return;
            }
            // check last name
            if (!CheckTextField(lastNameBox.Text, "Last Name", out outString))
            {
                PrintError(outString);
                return;
            }



            string clientUserName = usernameBox.Text;
            string firstName = firstNameBox.Text;
            string lastName = lastNameBox.Text;

            var EncodedclientUserName = Server.HtmlEncode(clientUserName);
            var EncodedfirstName = Server.HtmlEncode(firstName);
            var EncodedlastName = Server.HtmlEncode(lastName);

            ClientModel mtkClientModel = new ClientModel();
            Client mtkNewClient = new Client(0, EncodedfirstName, EncodedlastName, 1, EncodedclientUserName);

            if (isUserExists(EncodedclientUserName))
            {
                errorLabel.Text = "client exists";
                errorLabel.Visible = true;
                return;
            }

            mtkClientModel.AddNewClient(mtkNewClient, out outString);
            errorLabel.Visible = false;
            UpdateTable();
        }


        protected void UpdateTable()
        {
            List<Client> clients = new List<Client>();
            ClientModel mtkClientModel = new ClientModel();
            string outString = "";


            string clientUserName = usernameBox.Text;
            var EncodedclientUserName = Server.HtmlEncode(clientUserName);
            clients = mtkClientModel.GetAllClientsByUsername(EncodedclientUserName, out outString);
            myRepeater.DataSource = clients;
            myRepeater.DataBind();


        }

        protected bool isUserExists(string userName)
        {
            List<Client> clients = new List<Client>();
            ClientModel mtkClientModel = new ClientModel();
            string outString = "";
            clients = mtkClientModel.GetAllClientsByUsername(userName, out outString);
            return clients.Count > 0;
        }


        protected bool CheckTextField(string text, string fieldName, out string error)
        {// this function checks for general input validation such as length.
            if (String.IsNullOrEmpty(text))
            {
                error = $"{fieldName} Invalid: The field is required.";
                return false;
            }

            // input validation for part 2 !
            string pattern = "^[a-zA-Z]+$";

            // Check if the input matches the pattern
            if (!Regex.IsMatch(text, pattern))
            {
                error = $"{fieldName} Invalid: Non-letter characters found.";
                return false;
            }
            error = "";
            return true;
        }

        protected void PrintError(string error)
        {
            errorLabel.Visible = true;
            errorLabel.Text = error;
        }
    }
}