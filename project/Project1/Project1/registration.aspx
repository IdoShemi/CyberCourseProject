<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registration.aspx.cs" Inherits="Project1.registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="/css/registrationStyle.css" />
    <title>Registration</title>
</head>
<body>
    <div class="login-card-register">
        <h2>Registration</h2>
        <div class="icon-back-wrapper">
            <span class="icon-back">
                <a href="">
                    <i class="fa-solid fa-arrow-left"></i>
                </a>
            </span>
        </div>
        <form class="login-form" id="form1" runat="server">
            <div class="input-box-names">
                <asp:TextBox runat="server" PlaceHolder="First Name" ID="first_name"></asp:TextBox>
                <asp:TextBox runat="server" PlaceHolder="Last Name" ID="last_name"></asp:TextBox>
            </div>
            <div class="input-box">
                <asp:TextBox runat="server" PlaceHolder="Email" ID="mailBox"></asp:TextBox>

                <i class="fa-solid fa-envelope"></i>
            </div>
            <div class="input-box">
                <asp:TextBox TextMode="Password" runat="server" PlaceHolder="Password" ID="passwordBox"></asp:TextBox>
                <i class="fa-solid fa-eye" id="togglePassword"></i>
            </div>
            <div class="input-box">
                <asp:TextBox TextMode="Password" runat="server" PlaceHolder="Password" ID="passwordVerifyBox"></asp:TextBox>
                <i class="fa-solid fa-eye" id="togglePassword1" style="cursor: pointer;"></i>
            </div>

            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />

            <script
                src="https://kit.fontawesome.com/2c35fa1731.js"
                crossorigin="anonymous"></script>

            <script>
                window.onload = function () {
                    var togglePassword = document.getElementById("togglePassword");
                    var passwordBox = document.getElementById("passwordBox");
                    togglePassword.onclick = function () {
                        if (togglePassword.getAttribute("class") == "fa-solid fa-eye") {
                            passwordBox.type = "text";
                            togglePassword.setAttribute("class", "fa-solid fa-eye-slash");
                        } else {
                            passwordBox.type = "password";
                            togglePassword.setAttribute("class", "fa-solid fa-eye");
                        }
                        return false;
                    };

                    var togglePassword1 = document.getElementById("togglePassword1");
                    var passwordVerifyBox = document.getElementById("passwordVerifyBox");
                    togglePassword1.onclick = function () {
                        if (togglePassword1.getAttribute("class") == "fa-solid fa-eye") {
                            passwordVerifyBox.type = "text";
                            togglePassword1.setAttribute("class", "fa-solid fa-eye-slash");
                        } else {
                            passwordVerifyBox.type = "password";
                            togglePassword1.setAttribute("class", "fa-solid fa-eye");
                        }
                        return false;
                    };
                };
            </script>
        </form>
    </div>
</body>
</html>
