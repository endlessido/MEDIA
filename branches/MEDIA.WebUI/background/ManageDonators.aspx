<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageDonators.aspx.cs" Inherits="MEDIA.WebUI.background.ManageDonators" %>

<%@ Register Assembly="MEDIA.WebUI" Namespace="MEDIA.WebUI" TagPrefix="cc1" %>

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
<div class="page-header">
    <div class="page-header-content">
        <h2>View the Donators</h2>
    </div>
</div>
<div class="page-region bg-color-white">
    <div class="page-region-content">
        <div class="page-dashboard">
            <h4>Goodie Name: <span class="fg-color-blue"><%= model.Title %></span></h4>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Goodie Description</span></dt>
                <dd><%= model.Description %></dd>
            </dl>
            <h4>Goodie Price: <span class="fg-color-blue"><%= model.CurrencyStr %>  <%= model.Price %></span></h4>
            <h4>Goodie Sales Num: <span class="fg-color-blue"><%= model.SaleNum %></span></h4>
            <h4>Goodie Remains Num: <span class="fg-color-blue"><%= GetRemainNum(model.Num,model.SaleNum,model.IsLimit)%></span></h4>
            <h4>Belongs Project Name: <span class="fg-color-blue"><%= model.DonationProject.ProjectName %></span></h4>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Belongs Project Description</span></dt>
                <dd><%= model.DonationProject.ProjectSummary %></dd>
            </dl>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Goodies for donors record</span></dt>
                 <asp:Repeater runat="server" ID="RepeaterUser">
                    <HeaderTemplate> 
                    <table class="hovered bordered">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th class="right">Email</th>
                                    <th class="right">Donate Funding</th>
                                    <th class="right">Donate Date</th>
                                    <th class="right">IsPayment</th>
                                </tr>
                            </thead>
                            <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>  
                                <tr>
                                    <td><a href="#"><%# Eval("LastName")%> </a></td>
                                    <td class="right"><%# Eval("EmailAddress")%></td>
                                    <td class="right"><%# Eval("CurrencyStr")%>  <%# Eval("DonateFunding")%></td>
                                    <td class="right"><%# Eval("DonateDate")%></td>
                                    <td class="right"><%# Eval("IsPayment")%></td>
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
            </dl>
        </div>
    </div>
</div>
    </form>
</body>
</html>
