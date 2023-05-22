<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemScreen.aspx.cs" Inherits="Project1.SystemScreen" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="/css/systemScreen.css" />
    <title>System Screen</title>
    <style>
        #errorLabel {
    color: red;
    font-weight: bold;
    background-color: rgba(223, 207, 207, 0.872);
    border: 0.5px solid red;
    padding: 5px;
    padding-top:10px;
    /* display: none; */
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header>
      <h1 class="logo">Users Data</h1>
      <nav class="navigation">
        <a href="changePassword.aspx">Change Password</a>
        <asp:Button runat="server" Text="Logout" CssClass="logout-button" OnClick="logout_Click" />
      </nav>
    </header>
    <div class="system-card">
      <table>
        <thead>
          <tr>
            <th>#</th>
            <th>User Name</th>
            <th>First Name</th>
            <th>Last Name</th>
          </tr>
        </thead>
        <tbody>
        <asp:Repeater runat="server" ID="myRepeater">
        <ItemTemplate>
            <tr>
                <td><%# Container.ItemIndex + 1 %></td>
                <td><%# Eval("Nickname") %></td>
                <td><%# Eval("FirstName") %></td>
                <td><%# Eval("LastName") %></td>
            </tr>
        </ItemTemplate>
        </asp:Repeater>
<%--          <tr>
            <td>1</td>
            <td></td>
            <td></td>
            <td></td>
          </tr>
          <tr>
            <td>2</td>
            <td></td>
            <td></td>
            <td></td>
          </tr>
          <tr>
            <td>3</td>
            <td></td>
            <td></td>
            <td></td>
          </tr>
          <tr>
            <td>4</td>
            <td></td>
            <td></td>
            <td></td>
          </tr>
          <tr>
            <td>5</td>
            <td></td>
            <td></td>
            <td></td>
          </tr>--%>
        </tbody>
      </table>
      <div class="add_user_input">
        <div class="add-user-details">
            <asp:TextBox runat="server" PlaceHolder="User Name" ID="usernameBox"></asp:TextBox>
            <asp:TextBox runat="server" PlaceHolder="First Name" ID="firstNameBox"></asp:TextBox>
            <asp:TextBox runat="server" PlaceHolder="Last Name" ID="lastNameBox"></asp:TextBox>
        </div>
        <div class="addUser-submit-form">
            <asp:Button runat="server" Text="Add Client" OnClick="btnAddClient_click" />

        </div>
      </div>
        <asp:Label runat="server" ID="errorLabel" CssClas="change-password-error" Visible="false"></asp:Label>
    </div>
        

    <script
      src="https://kit.fontawesome.com/2c35fa1731.js"
      crossorigin="anonymous"
    ></script>
    </form>
</body>
</html>
