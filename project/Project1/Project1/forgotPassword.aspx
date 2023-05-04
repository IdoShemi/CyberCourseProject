<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forgotPassword.aspx.cs" Inherits="Project1.forgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link
        rel="stylesheet"
        href="/css/forgotPasswordStyle.css"
        type="text/css" />
    <title>Forgot Password</title>
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
    <div class="login-card">
        <h1>Forgot your password?</h1>
        <form class="login-form" id="form1" runat="server">
            <div class="input-box">
                <p>Please enter your email address to receive a verification code</p>
            </div>
            <div class="input-box-email">
                <asp:TextBox runat="server" PlaceHolder="Email" ID="email"></asp:TextBox>
            </div>
            <asp:Button ID="btnGetCode" runat="server" Text="Get Code" OnClick="btnGetCode_func" />
            <asp:Label CssClass="error" runat="server" ID="err_label" Visible="false"></asp:Label>
        </form>
    </div>

    <script
        src="https://kit.fontawesome.com/2c35fa1731.js"
        crossorigin="anonymous"></script>
</body>
</html>
