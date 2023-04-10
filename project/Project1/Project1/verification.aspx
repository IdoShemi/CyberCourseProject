<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="verification.aspx.cs" Inherits="Project1.verification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Verification Page</title>
    <link href="css/verificationPage.css" rel="stylesheet" />
</head>
<body>
    <div class="verification-card">
        <h1>Verify your account</h1>
        <p>Please enter the code that we sent to your email</p>
        <form id="form1" runat="server" class="verification-form">
            <div class="text-input">
                <asp:TextBox runat="server" PlaceHolder="Verification Code" ID="VerificationCode"></asp:TextBox>
            </div>

            <div class="verification-input-form">
                <asp:Button ID="btnGetCode" runat="server" Text="Get Code" OnClick="btnGetCode_func" />
            </div>



        </form>
</body>
</html>
