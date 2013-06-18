<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageProjectList.aspx.cs" Inherits="MEDIA.WebUI.background.ManageProjectList" %>

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
        <h2><%= ProjectTitle%></h2>
    </div>
</div>
<div class="page-region bg-color-white">
    <div class="page-region-content">
        <div class="page-dashboard">
        <asp:Repeater runat="server" ID="RepeaterProject">
        <HeaderTemplate>
         <table class="hovered striped bordered">
                <thead>
                    <tr>
                        <th>Project Name</th>
                        <th class="right">Donation Target</th>
                        <th class="right">Region</th>
                        <th class="right">Duration</th>
                        <th class="right">Operation</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
                     <tr>
                        <td><a href="ManageProjectDetail.aspx?pid=<%# Eval("ProjectId") %>"><%# Eval("ProjectName")%></a></td>
                        <td class="right"><%# Eval("CurrencyStr")%> <%# Eval("TotalFunding")%> </td>
                        <td class="right"><%# Eval("AreaName")%>, <%# Eval("CountryName")%> </td>
                        <td class="right"><%# this.GetProjectLeaveDays((System.DateTime?)Eval("EndDate")) %>days </td>
                        <td class="right"><a href="ManageProjectDetail.aspx?pid=<%# Eval("ProjectId") %>">Verify this project</a></td>
                    </tr>
        </ItemTemplate>
        <FooterTemplate>
                </tbody>
            </table>
        </FooterTemplate>
        </asp:Repeater>
            <div class="pagination tlc">
                 <cc1:CustomPager ID="CustomPager1" RequiredMethod="get" runat="server" />
            </div>
        </div>
    </div>
</div>
</body>
</html>