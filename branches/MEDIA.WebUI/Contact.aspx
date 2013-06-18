<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" Async="true" CodeBehind="Contact.aspx.cs" Inherits="MEDIA.WebUI.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
 jQuery(document).ready(function ($) {
     $('#ContactForm').validationEngine();
            });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form runat="server" id="ContactForm">
<div class="page secondary">
        <div class="page-header">
            <div class="page-header-content">
                <h1><asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:Resource, ContactUs %>" /><small>&nbsp;</small></h1>
                <a class="back-button big page-back" href="/"></a>
            </div>
        </div>
         
        <div class="page-region">
            <div class="page-region-content">
                <div class="grid">
                    <div class="row">
                        <div class="span10">
                            <fieldset class="border-color-red">
                                <%--<legend class="fg-color-darken"><asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:Resource, Please_SendEmail %>" /></legend>--%>
                                <div class="row">
                                    <div class="span5">
                                        <p class="fg-color-greenDark">Name <span class="fg-color-red">*</span></p>
                                        <div class="input-control text span5">
                                            <input name="username" type="text" tabindex="1" autocomplete="off" placeholder="Enter your Name." class="validate[required]" />
                                            <button class="helper"></button>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="span5">
                                        <p class="fg-color-greenDark"><asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:Resource, Email %>" /> <span class="fg-color-red">*</span></p>
                                        <div class="input-control text span5">
                                            <input name="emailaddr" type="email" tabindex="2" autocomplete="off" placeholder="Enter your Email address." class="validate[required,custom[email]]" />
                                            <button class="helper"></button>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="span5">
                                        <p class="fg-color-greenDark"><asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:Resource, Subject %>" /> <span class="fg-color-red">*</span></p>
                                        <div class="input-control text span5">
                                            <input name="subject" type="text" tabindex="3" autocomplete="off" placeholder="Enter subject." class="validate[required]" />
                                            <button class="helper"></button>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="span8">
                                        <div class="input-control textarea">
                                            <textarea name="content" tabindex="4" class="validate[required]" ></textarea>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="row">
                        <div class="span10">
                            <p class="tlc"><asp:Button CssClass="bg-color-green5" runat="server" id="btnSend" text="send" OnClick="btnSend_Click"/></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
