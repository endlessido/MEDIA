<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageLogin.aspx.cs" Inherits="MEDIA.WebUI.background.ManageLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
<title>Background Management System Login</title>
<meta name="description" content="">
<meta name="keywords" content="">
<link href="../css/modern.css" rel="stylesheet">
<link href="../css/modern-responsive.css" rel="stylesheet">
<link href="../css/manager.css" rel="stylesheet">
<script type="text/javascript" src="../js/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="../js/modern/dropdown.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <div class="topbar bg-color-darken">
        <h1 class="topbar-title fl fg-color-lighten"><i class="icon-cog"></i><a href="#" class="fg-color-lighten">FA Raetia Media Control Panel</a></h1>
    </div>
    <div class="login bg-color-lighten">
        <h3 class="fg-color-blueDark">FA Raetia Media Manager Login</h3>
        <div class="as-block">
            <p>Username</p>
            <div class="input-control span4 text">
                <input runat="server" id="txtAdminName" type="text" />
                <button class="helper"></button>
            </div>
        </div>
        <div class="as-block">
            <p>Password</p>
            <div class="input-control span4 password">
                <input runat="server" id="txtAdminPwd" type="password" />
                <button class="helper"></button>
            </div>
        </div>
        <div class="as-block">
            <label class="input-control checkbox">
                <input type="checkbox" runat="server" id="CKRemeber" class="bg-color-white">
                <span class="helper">Remember Me</span>
            </label>
        </div>
        <div class="as-block tlr">
            <asp:Button ID="btnLogin" runat="server" Text="Login" 
                onclick="btnLogin_Click" />
        </div>
    </div>
    </form>
</body>
</html>
