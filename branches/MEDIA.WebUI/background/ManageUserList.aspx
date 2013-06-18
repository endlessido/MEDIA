<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUserList.aspx.cs" Inherits="MEDIA.WebUI.background.ManageUserList" %>

<%@ Register Assembly="MEDIA.WebUI" Namespace="MEDIA.WebUI" TagPrefix="cc1" %>
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
<div class="page-header">
    <div class="page-header-content">
        <h2><asp:Label ID="lblTitle" runat="server" ></asp:Label></h2>
    </div>
</div>
<div class="page-region bg-color-white">
    <div class="page-region-content">
        <div class="page-dashboard">
         <asp:Repeater runat="server" ID="RepeaterUser">
                            <HeaderTemplate> 
                            <table class="hovered bordered">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th class="right">Email</th>
                                            <th class="right">Gender</th>
                                            <th class="right">Birthday</th>
                                        </tr>
                                    </thead>
                                    <tbody></HeaderTemplate>
                            <ItemTemplate>  <tr>
                                            <td><a href="ManageUserDetail.aspx?uid=<%# Eval("UserId")%>"><%# Eval("FirstName")%> <%# Eval("LastName")%></a></td>
                                            <td class="right"><%# Eval("EmailAddress")%></td>
                                            <td class="right"><%# this.GetSex((bool)Eval("Gender"))%></td>
                                             <td class="right"><%# ((DateTime)Eval("Birthday")).ToShortDateString()%></td>
                                        </tr></ItemTemplate>
                            <FooterTemplate></tbody>
                                </table></FooterTemplate>
                            </asp:Repeater>  
            <div class="pagination tlc">
                <cc1:CustomPager ID="CustomPager1" PageSize="10" RequiredMethod="get" runat="server" />
            </div>
        </div>
    </div>
</div>
</body>
</html>