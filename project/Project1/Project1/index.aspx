<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Project1.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="/css/loginStyle.css" type="text/css" />
    <title>Login Page</title>
</head>
<body>
    <div class="login-card">
        <!-- <div class="wrapper">
        <span class="icon-close"
          ><i class="fa-sharp fa-solid fa-xmark"></i
        ></span>
      </div> -->
        <h2>Login</h2>
        <h3>Enter your credentials</h3>
        <form id="form1" runat="server" class="login-form">
            <div class="input-box">
                <asp:TextBox runat="server" PlaceHolder="Email" ID="mailBox"></asp:TextBox>
                <%--          <input type="text" placeholder="Email" />--%>
                <i class="fa-solid fa-envelope"></i>
            </div>
            <div class="input-box">
<%--                <input type="password" placeholder="Password" />--%>
                    <asp:TextBox TextMode="Password" runat="server" PlaceHolder="Password" ID="passwordBox"></asp:TextBox>
<%--                <i class="fa-solid fa-eye" ID="icon" runat="server" onclick="eyeclicked"></i>--%>
                <i class="fa-solid fa-lock"></i>
                <%--<i class="fa-solid fa-eye" id="togglePassword"></i>--%>



                <%--<asp:LinkButton runat="server" ID="btnRun"
                ValidationGroup="edt" OnClick="eyeclicked" OnClientClick="return false;"><i runat="server" class='fa-solid fa-eye' id='togglePassword'></i></asp:LinkButton>--%>
                <i runat="server" class='fa-solid fa-eye' id='togglePassword'></i>


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
                    };
                </script>


            </div>
            <span class="forget-remember">
                <div class="password-options">
                    <a href="/html/forgotPassword.html">Forgot Password?</a>
                    <!-- <a href="#">Reset Password</a> -->
                </div>

                <div class="remember-me">
                    <label>
                        <asp:CheckBox runat="server" ID="checkBox1" Text="Remember me" />
                        <%--<input type="checkbox" />Remember me--%></label>
                </div>
            </span>
            <asp:Button ID="btnLogin" runat="server" Text="LOGIN" OnClick="btnLogin_Click" />
<%--            <button type="s">LOGIN</button>--%>
            <div class="register">
                Don't have an account?
                <a href="/html/registration.html" class="register-link">Register</a>
                </p>
            </div>
        </form>
    </div>


    <script
        src="https://kit.fontawesome.com/2c35fa1731.js"
        crossorigin="anonymous">
    </script>
</body>
</html>
