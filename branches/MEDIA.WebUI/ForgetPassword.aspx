<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" Async="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="MEDIA.WebUI.ForgetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="js/moment.min.js"></script>
 <script type="text/javascript">
     jQuery(document).ready(function ($) {
         $('#ForgetPwdForm').validationEngine('attach', {
             scroll: false,
             onValidationComplete: function (form, status) {
                 if (status) {
                     showWaitDialog();
                     return true;
                 }
             }
         });
     });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form runat="server" id="ForgetPwdForm">
    <div class="page secondary">
        <div class="page-header">
            <div class="page-header-content">
                <h1><asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:Resource, Index_Login_Forgot %>" /><small>&nbsp;</small></h1>
                <a class="back-button big page-back" href="/"></a>
            </div>
        </div>
        <div class="page-region">
            <div class="page-region-content">
                    <div class="grid">
                        <div class="row">
                            <div class="span10">
                                <fieldset class="border-color-red">
                                    <legend class="fg-color-darken"><asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:Resource, ForgetPwd %>" /></legend>
                                    <div class="row">
                                        <div class="span5">
                                            <div class="input-control text span5">
                                                <asp:TextBox runat="server" id="txtEmail" placeholder="Enter your email"  TextMode="SingleLine" class="validate[required,custom[email]] text-input" />
                                                <button class="helper"></button>
                                            </div>
                                        </div>
                                    </div>
                                  
                                </fieldset>
                            </div>
                        </div>
                        <div class="row">
                            <div class="span10">
                                <p class="tlc"><asp:Button ID="btnLogin" CssClass="bg-color-green5" runat="server" Text="<%$ Resources:Resource, Submit %>" onclick="btnLogin_Click" /></p>
                                <p class="tlc"><asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label></p>
                            </div>
                        </div>
                    </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
