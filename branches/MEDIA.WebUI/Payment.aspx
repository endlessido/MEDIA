<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Index.Master" CodeBehind="Payment.aspx.cs" Inherits="MEDIA.WebUI.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content2">
    <form method="post" action="https://secure.payengine.de/ncol/test/orderstandard.asp" id="PaymentForm" name="PaymentForm">
    <div class="page secondary">
        <div class="page-header">
            <div class="page-header-content">
                <h1>Currency Amount <small>&nbsp;</small></h1>
                <a class="back-button big page-back" href="/"></a>
            </div>
        </div>
        <div class="page-region">
            <h3>You have ordered:</h3>
                <div class="grid">
                    <div class="row">
                        <div class="page-cart-item">
                            <table class="bordered">
                                <thead>
                                    <tr>
                                        <th>Goodie</th>
                                        <th>Quantity</th>
                                        <th>Price</th>
                                        <th>Description</th>
                                    </tr>
                                </thead>
                                <tbody>
                                <asp:Repeater runat="server" ID="RepGoodies">
                                <ItemTemplate>
                                 <tr>
                                        <td><%# Eval("Title")%></td>
                                        <td>1</td>
                                        <td><%# base.GetCurrency((decimal)Eval("Price"))%> <%# Eval("CurrencyStr")%></td>
                                        <td>
                                            <div class="page-shop-print as-block span5"><%# Eval("Description")%></div>
                                        </td>
                                  </tr>
                                </ItemTemplate>
                                </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <h3><strong class="fg-color-orangeDark">Overall Total: <%= base.GetCurrency(SumPrice.Value) %> <%=CurStr %></strong></h3>
                        <div class="page-shop-cart tlc">
                            <button class="big bg-color-green5 fg-color-white"><i class="icon-cart"></i>Online Secure Payment</button>
                        </div>
                    </div>
                </div>
        </div>
    </div>
        <!-- general parameters -->
        <input type="hidden" name="PSPID" value="<%= PSPID %>" >
        <input type="hidden" name="ORDERID" value="<%= ORDERID %>">
        <input type="hidden" name="AMOUNT" value="<%= AMOUNT %>">
        <input type="hidden" name="CURRENCY" value="<%= CurStr %>">
        <input type="hidden" name="LANGUAGE" value="<%= LANGUAGE %>">
        <input type="hidden" name="CN" value="<%= CN %>">
        <input type="hidden" name="EMAIL" value="<%= EMAIL %>">
        <input type="hidden" name="OWNERZIP" value="<%= OWNERZIP %>">
        <input type="hidden" name="OWNERADDRESS" value="<%= OWNERADDRESS %>">
        <input type="hidden" name="OWNERCTY" value="">
        <input type="hidden" name="OWNERTOWN" value="<%= OWNERTOWN %>">
        <input type="hidden" name="OWNERTELNO" value="">
        <!-- check before the payment: see Security: Check before the payment -->
        <input type="hidden" name="SHASIGN" value="<%= SHASIGN %>" >
        <!-- layout information: see Look and feel of the payment page -->
        <input type="hidden" name="TITLE" value="">
        <input type="hidden" name="BGCOLOR" value="">
        <input type="hidden" name="TXTCOLOR" value="">
        <input type="hidden" name="TBLBGCOLOR" value="">
        <input type="hidden" name="TBLTXTCOLOR" value="">
        <input type="hidden" name="BUTTONBGCOLOR" value="">
        <input type="hidden" name="BUTTONTXTCOLOR" value="">
        <input type="hidden" name="LOGO" value="">
        <input type="hidden" name="FONTTYPE" value="">
        <!-- post payment redirection: see Transaction feedback to the customer -->
        <input type="hidden" name="ACCEPTURL" value="http://www.supportyourlocalteam.net/PayResponse.aspx">
        <input type="hidden" name="DECLINEURL" value="http://www.supportyourlocalteam.net/PayResponse.aspx">
        <input type="hidden" name="EXCEPTIONURL" value="http://www.supportyourlocalteam.net/PayResponse.aspx">
        <input type="hidden" name="CANCELURL" value="http://www.supportyourlocalteam.net/PayResponse.aspx">
        <input type="hidden" name="BACKURL" value="http://www.supportyourlocalteam.net/index.aspx">
</form>
</asp:Content>
  
