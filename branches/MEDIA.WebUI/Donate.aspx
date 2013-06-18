<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="Donate.aspx.cs" Inherits="MEDIA.WebUI.Donate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    jQuery(document).ready(function ($) {

        $('.page-availgoodie input').on('change', function (e) {
            if (e.target.checked) {
                $('#page-customamount').val('').attr('disabled', 'disabled');
            }
        })

        $('#page-customdonate').on('change', function (e) {
            if (e.target.checked) {
                $('.page-availgoodie input[type="checkbox"]').removeAttr('checked').attr('disabled', 'disabled');
                $('#page-customamount').removeAttr('disabled');
            } else {
                $('.page-availgoodie input[type="checkbox"]').removeAttr('disabled');
            }
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form runat="server" id="Donateform">
<div class="page secondary">
        <div class="page-header">
            <div class="page-header-content" >
                <h1 >Donate <small>&nbsp;</small></h1>
                <a class="back-button big page-back" href="/"></a>
            </div>
        </div>
        <div class="page-region">
            <div class="grid">
                <div class="row">
                    <div class="page-fundinfo span12 bg-color-red">
                        <h3 class="fg-color-white"><asp:Localize ID="Localize16" runat="server" Text="<%$ Resources:Resource, Fun_info %>" /></h3>
                         <ul class="fg-color-white">
                            <li><asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:Resource, Total_Date %>" />: <%= base.GetCurrency(model.ReceivedFunding.Value) %> <%= model.CurrencyStr %></li>
                        <li><asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:Resource, Remain_Goal %>" />: <%= base.GetCurrency(model.TotalFunding.Value - model.ReceivedFunding.Value)%> <%= model.CurrencyStr %></li>
                        <li><asp:Localize ID="Localize9" runat="server" Text="<%$ Resources:Resource, Total_Goal %>" />: <%= base.GetCurrency(model.TotalFunding.Value)%> <%= model.CurrencyStr %></li>
                        </ul>
                    </div>
                    <div class="page-prgs span8 bg-color-green6">
                        <h3 class="fg-color-white"><%= Percent %>% <small>of 100% of the goal</small></h3>
                        <div class="progress-bar bg-color-white">
                            <div class="bar bg-color-yellow" style="width: <%= Percent %>%"></div>
                        </div>
                    </div>
                    <div class="page-prgs tile-last span4 bg-color-green7">
                        <h3 class="fg-color-white tlc"><%= leaveDays %> Days left</h3>
                        <div class="progress-bar bg-color-white">
                            <div class="bar bg-color-yellow" style="width: <%= leaveDays %>%"></div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="span9">
                            <fieldset class="border-color-orange">
                                <legend class="fg-color-blueDark"><asp:Localize ID="Localize7" runat="server" Text="<%$ Resources:Resource, Donate_Form %>" /></legend>
                                <p class="fg-color-greenDark"><asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:Resource, much_give %>" /></p>
                                <asp:Repeater runat="server" ID="RepeaterGoodys">
                                <ItemTemplate>
                                 <div class="<%# GetDivCssClass(MEDIA.WebUI.SysConfigConst.DonateGoodyClass,3) %>">
                                    <div class="tile-content">
                                        <h4 class="nwbr"><%# Eval("Title")%><br><small>for <%# Eval("CurrencyStr")%> <%# base.GetCurrency((decimal)Eval("Price"))%></small></h4>
                                        <p>Gone:<%# Eval("SaleNum")%><br />Remains:<%# GetRemainNum(Eval("Num"), Eval("SaleNum"), Eval("IsLimit"))%></p>
                                        <div class="brand tile-brand-input">
                                            <label class="input-control checkbox">
                                                <input value="<%# Eval("Id") %>" name="goodySelect" type="checkbox">
                                                <span class="helper">I want this goodie</span>
                                            </label>
                                        &nbsp;</div>
                                    </div>
                                </div>
                                </ItemTemplate>
                                </asp:Repeater>

                                <div class="clearfix">
                                    <label class="input-control checkbox fl page-domd">
                                        <input type="checkbox" id="page-customdonate">
                                        <span class="helper">Or <%=model.CurrencyStr %> :</span>
                                    </label>
                                    <div class="input-control text span1">
                                        <input name="donatemoney" type="text" id="page-customamount" />
                                        <button class="helper"></button>
                                    </div>
                                </div>

                                <p class="fg-color-greenDark"><asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:Resource, abondon %>" /></p>
                                <label class="input-control checkbox">
                                    <input type="checkbox" name="ck_abondon">
                                    <span class="helper"><asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:Resource, Yes %>" /></span>
                                </label>
                                <% if (isLogin) { %>
                                   <p class="page-doreg"><a href="UserRegist.aspx"><asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:Resource, NewAccount %>" /></a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="ForgetPassword.aspx"><asp:Localize ID="Localize8" runat="server" Text="<%$ Resources:Resource, Index_Login_Forgot %>" /></a></p>
                                   <%} else { %>
                                   <p class="page-doreg"><a href="UserLogin.aspx"><asp:Localize ID="Localize11" runat="server" Text="<%$ Resources:Resource, Login %>" /></a></p>
                                  <% } %>
                                

                                <div class="as-block">
                                    <label class="input-control checkbox">
                                        <input type="checkbox" checked="checked">
                                        <span class="helper"><asp:Localize ID="Localize10" runat="server" Text="<%$ Resources:Resource, NewsLetter %>" /></span>
                                    </label>
                                </div>
                            </fieldset>
                            <div class="page-prgs">
                                <p class="tlc"><asp:Button ID="btnDonate" CssClass="bg-color-green5" runat="server" Text="<%$ Resources:Resource, Donate %>" OnClick="btnDonate_Click" /></p>
                            </div>
                    </div>

                    <div class="span3">
                     <asp:Repeater ID="RepeaterShowGoody" runat="server">
                        <ItemTemplate>
                           <div class="<%# GetDivCssClass(MEDIA.WebUI.SysConfigConst.GoodyClass,3) %>">
                            <h3 class="fg-color-white"><%# Eval("Title")%> <br><small>for <%# Eval("CurrencyStr")%> <%# base.GetCurrency((decimal)Eval("Price"))%></small></h3>
                            <p class="fg-color-white">Gone:<%# Eval("SaleNum")%>, Remains:<%# GetRemainNum(Eval("Num"), Eval("SaleNum"), Eval("IsLimit"))%></p>
                                <div class="progress-bar bg-color-white">
                                    <div class="bar bg-color-yellow" style="width: <%# GetPercent( Eval("Num"), Eval("SaleNum") )%>%"></div>
                                </div>
                           </div>
                        </ItemTemplate>
                     </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
