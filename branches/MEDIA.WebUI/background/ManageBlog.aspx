<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageBlog.aspx.cs" Inherits="MEDIA.WebUI.background.ManageBlog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link href="../css/modern.css" rel="stylesheet">
<link href="../css/modern-responsive.css" rel="stylesheet">
<link href="../css/manager.css" rel="stylesheet">
<script type="text/javascript" src="../js/jquery-1.8.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page-dashboard">
            <h3>The title:<span class="fg-color-red"><%= model.Title %></span></h3>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Content</span></dt>
                <dd><%= Server.HtmlDecode(model.Content)%></dd>
            </dl>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Create Date</span></dt>
                <dd><%= model.CreateDate %></dd>
            </dl>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Image</span></dt>
                <dd><asp:Image Width="630" Height="310" runat="server" ID="imgblog" /></dd>
            </dl>
            <h4>IsCheck: <%= model.IsCheck == true?"Yes" : "No" %></h4>
        </div>
    </form>
</body>
</html>
