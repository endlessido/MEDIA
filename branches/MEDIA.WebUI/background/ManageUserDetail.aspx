<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUserDetail.aspx.cs" Inherits="MEDIA.WebUI.background.UserDetail" %>
<!doctype html>
<html>
<head>
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
<title>FA Raetia Media</title>
<meta name="description" content="">
<meta name="keywords" content="">
<link href="../css/modern.css" rel="stylesheet">
<link href="../css/modern-responsive.css" rel="stylesheet">
<link href="../css/manager.css" rel="stylesheet">
<script type="text/javascript" src="../js/jquery-1.8.2.min.js"></script>
</head>
<body>
<form id="form1" runat="server">
<div class="page-header">
    <div class="page-header-content">
        <h2>User Detail</h2>
    </div>
</div>
<div class="page-region bg-color-white">
    <div class="page-region-content">
        <div class="page-dashboard">
            <h3>The first name:<span class="fg-color-red"><%= model.FirstName %></span></h3>
            <h4>The last name: <span class="fg-color-blue"><%= model.LastName %></span></h4>
            <h4>Email: <span class="fg-color-blue"><%= model.EmailAddress %></span></h4>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Gender</span></dt>
                <dd><%= model.Gender == true?"Male" :"Female" %></dd>
            </dl>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Address</span></dt>
                <dd><%= model.Address %></dd>
            </dl>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Zip</span></dt>
                <dd><%= model.Zip %></dd>
            </dl>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Town</span></dt>
                <dd><%= model.Town %></dd>
            </dl>
            <h4>Country: <span class="fg-color-blue"><%= model.CountryName %></span></h4>
            <h4>Birthday: <span class="fg-color-blue"><%= model.Birthday %></span></h4>
            <h4>Create Date: <span class="fg-color-blue"><%= model.CreateDate %></span></h4>
            <h4>IsCheck: <%= model.IsCheck == true?"Yes" : "No" %></h4>
        </div>
    </div>
</div>
</form>
</body>
</html>