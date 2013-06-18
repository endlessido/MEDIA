<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="MyOrder.aspx.cs" Inherits="MEDIA.WebUI.MyOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="page secondary">
        <div class="page-header">
            <div class="page-header-content">
                <h1><asp:Label runat="server" ID="lblTitle"></asp:Label> <small>&nbsp;</small></h1>
                <a class="back-button big page-back" href="/"></a>
            </div>
        </div>
        <div class="page-region">
            <form action="">
                <div class="grid">
                    <div class="row">
                    <asp:Repeater runat="server" ID="RepOrder">
                    <ItemTemplate>
                    <h4>Order No.<%# Eval("OrderId")%>&nbsp;&nbsp;&nbsp;&nbsp;Bargain made at <%# Eval("OrderDate")%></h4>
                        <div class="page-cart-item">
                            <table class="bordered">
                                <tr>
                                <td>Goodie Name</td>
                                <td>Goodie UnitPrice</td>
                                <td>TotalPrice</td>
                                </tr>
                                <tr>
                                    <td  width="250" class="tlc"><%# Eval("Goodies") %></td>
                                    <td width="400"><%# Eval("UnitPrices")%></td>
                                    <td>Overall Price: <%# base.GetCurrency((decimal)Eval("TotalPrice"))%> <%# Eval("CurrencyStr")%></td>
                                </tr>
                                <tr>
                                    
                                    <%# Show()  %>
                                   
                                </tr>
                            </table>
                        </div>
                    </ItemTemplate>
                    </asp:Repeater>
                        
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
