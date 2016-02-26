<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="s3357335_a2.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Movie Management</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <link href="Content/signin.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <form class="form-signin" id="form1" runat="server">
            <h2 class="form-signin-heading">Sign In (Admin)</h2>
            <input type="text" id="inputUserName" name="inputUserName" class="form-control" placeholder="UserName" required autofocus />
            <br />
            <input type="password" id="inputPassword" name="inputPassword" class="form-control" placeholder="Password" required />

            <asp:Button ID="SignIn" runat="server" Text="Sign in" class="btn btn-lg btn-primary btn-block" OnClick="SignIn_Click" />
        </form>
    </div>
    <!-- /container -->
</body>
</html>
