using System;
using System.Collections.Generic;
using System.Linq;
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
            string clientUserName = usernameBox.Text;
            string firstName = firstNameBox.Text;
            string lastName = lastNameBox.Text;
            ClientModel mtkClientModel = new ClientModel();
            Client mtkNewClient = new Client(0, firstName, lastName, 1, clientUserName);

            if (isUserExists(clientUserName))
            {
                errorLabel.Text = "client exists";
                errorLabel.Visible = true;
                return;
            }

            mtkClientModel.AddNewClient(mtkNewClient, out outString);

            UpdateTable();
        }


        protected void UpdateTable()
        {
            List<Client> clients = new List<Client>();
            ClientModel mtkClientModel = new ClientModel();
            string outString = "";


            string clientUserName = usernameBox.Text;
            clients = mtkClientModel.GetAllClientsByUsernameVulnerable(clientUserName, out outString);
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
    }
}