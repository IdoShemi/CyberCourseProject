<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="resetPassword.aspx.cs" Inherits="Project1.resetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="/css/resetPassword.css" />
    <title>Reset Page</title>
    <style>
        .error {
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
    <div class="resetPassword-card">
      <h1>Reset Password</h1>
      <form class="resetPassword-form" runat="server">
        <div class="resetPassword-input-box">
        <asp:TextBox runat="server" PlaceHolder="new password" ID="passBox"></asp:TextBox>
        <asp:TextBox runat="server" PlaceHolder="confirm new password" ID="verifyPassBox"></asp:TextBox>
        </div>
        <div class="resetPassword-submit-form">
        <asp:Button ID="btnChangePass" runat="server" Text="Reset Password" OnClick="ChangePass_Click" />
        </div>
          <br />
        <asp:Label CssClass="error" runat="server" ID="err_label" Visible="false"></asp:Label>

      </form>
    </div>

    <script
      src="https://kit.fontawesome.com/2c35fa1731.js"
      crossorigin="anonymous"
    ></script>
</body>
</html>
