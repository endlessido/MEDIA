<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageOrderList.aspx.cs" Inherits="MEDIA.WebUI.background.ManageOrderList" %>

<%@ Register Assembly="MEDIA.WebUI" Namespace="MEDIA.WebUI" TagPrefix="cc1" %>
<!doctype html>
<html>
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
        <h2><asp:Label ID="lblTitle" runat="server" ></asp:Label></h2>
    </div>
</div>
<div class="page-region bg-color-white">
    <div class="page-region-content">
        <div class="page-dashboard">
        <dl class="page-dash-box">
            <div class="span9 tlr"><asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:Resource, Sortby %>" /> 
            <asp:LinkButton ID="LinkButton1" CommandName="SortBy" CommandArgument="OrderDate" Text="OrderDate" runat="server" OnCommand="SortBy_Command"></asp:LinkButton> | 
            <asp:LinkButton ID="LinkButton2" CommandName="SortBy" CommandArgument="Address" Text="Address" runat="server" OnCommand="SortBy_Command"></asp:LinkButton> | 
            <asp:LinkButton ID="LinkButton3" CommandName="SortBy" CommandArgument="Quantity" Text="Quantity" runat="server" OnCommand="SortBy_Command"></asp:LinkButton> | 
            <asp:LinkButton ID="LinkButton4" CommandName="SortBy" CommandArgument="ProjecName" Text="ProjecName" runat="server" OnCommand="SortBy_Command"></asp:LinkButton>
            </div>
            <dd class="clearfix">
                <table class="hovered bordered">
                    <thead>
                        <tr>
                            <th>Order Number</th>
                            <th>Date</th>
                            <th class="right">Customer Address</th>
                            <th class="right">Quantity</th>
                            <th class="right">Goodies</th>
                            <th class="right">Project</th>
                            <th class="right">Total</th>
                            <th class="right">IsPayment</th>

                        </tr>
                    </thead>
                    <tbody>
                    <asp:Repeater ID="RepOrder" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("OrderId")%></td>
                            <td><%# Eval("OrderDate")%></td>
                            <td class="right"><%# Eval("Address")%></td>
                            <td class="right"><%# Eval("Quantity")%></td>
                            <td class="right"><%# Eval("Goodies")%></td>
                            <td class="right"><%# Eval("ProjecName")%></td>
                            <td class="right"><%# Eval("TotalPrice")%> <%# Eval("CurrencyStr")%></td>
                            <td class="right"><%# Eval("IsPayment")%></td>
                        </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                </table>
            </dd>
        </dl>
            <div class="pagination tlc">
                <cc1:CustomPager ID="CustomPager1" PageSize="10" RequiredMethod="get" runat="server" />
            </div>
        </div>
    </div>
</div>



    </form>
</body>
</html>
