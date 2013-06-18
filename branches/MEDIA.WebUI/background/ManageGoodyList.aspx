<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageGoodyList.aspx.cs" Inherits="MEDIA.WebUI.background.ManageGoodyList" %>
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
        <h2>Donated Goodies List</h2>
    </div>
</div>
<div class="page-region bg-color-white">
    <div class="page-region-content">
        <div class="page-dashboard">
           <asp:Repeater  runat="server" ID="RepGoodies">
                             <HeaderTemplate>
                              <table class="hovered bordered">
                                    <thead>
                                        <tr>
                                            <th>Goodie Name</th>
                                            <th class="right">Price</th>
                                            <th class="right">Sales Nums</th>
                                            <th class="right">Remain Nums</th>
                                            <th class="right">Belongs Project Name</th>
                                            <th class="right">Opeartion</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                             </HeaderTemplate>
                             <ItemTemplate>
                                         <tr>
                                            <td><a href="#"><%# Eval("Title") %></a></td>
                                            <td class="right"><%# Eval("CurrencyStr")%> <%# Eval("Price")%></td>
                                            <td class="right"><%# Eval("SaleNum")%></td>
                                            <td class="right"><%# GetRemainNum(Eval("Num"),Eval("SaleNum"),Eval("IsLimit"))%></td>
                                            <td class="right"><%# Eval("DonationProject.ProjectName")%></td>
                                            <td class="right"><a href="ManageDonators.aspx?gid=<%# Eval("Id") %>">View the donators</a></td>
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
    </form>
</body>
</html>
