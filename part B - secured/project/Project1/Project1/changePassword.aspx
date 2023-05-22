<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changePassword.aspx.cs" Inherits="Project1.changePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="/css/changePassword.css" type="text/css" />
    <title>Change Password</title>
</head>
<body>
        <div class="changePassword-card">

    <div class="back-icon">
        <a style="all:unset" href="SystemScreen.aspx"><i class="fa-solid fa-arrow-left"></i></a>
      </div>
      <h1>Change Password</h1>
      <form class="changePassword-form" runat="server">
        <div class="changePassword-input-box">
            <asp:TextBox runat="server" PlaceHolder="Current password" ID="currentPasswordBox"></asp:TextBox>
            <asp:TextBox runat="server" PlaceHolder="New password" ID="newPasswordBox"></asp:TextBox>
            <asp:TextBox runat="server" PlaceHolder="Confirm new password" ID="confirmNewPasswordBox"></asp:TextBox>
        </div>
        <div class="changePassword-submit-form">
            <asp:Button runat="server" Text="Change Password" OnClick="btnChangePassword_click" />
        </div>
            <asp:Label runat="server" ID="errorLabel" CssClas="change-password-error" Visible="false"></asp:Label>
      </form>
    </div>
    
    <script
      src="https://kit.fontawesome.com/2c35fa1731.js"
      crossorigin="anonymous"
    ></script>
</body>
</html>
