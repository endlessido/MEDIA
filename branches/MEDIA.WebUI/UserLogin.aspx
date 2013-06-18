<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="MEDIA.WebUI.UserLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
     jQuery(document).ready(function ($) {
         $('#LoginForm1').validationEngine();
     });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" id="LoginForm1">
    <div class="page secondary">
        <div class="page-header">
            <div class="page-header-content">
                <h1><asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:Resource, Userlogin %>" /><small>&nbsp;</small></h1>
                <a class="back-button big page-back" href="/"></a>
            </div>
        </div>
        <div class="page-region">
            <div class="page-region-content">
                    <div class="grid">
                        <div class="row">
                            <div class="span10">
                                <fieldset class="border-color-red">
                                    <legend class="fg-color-darken"><asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:Resource, Login_Title %>" /></legend>
                                    <div class="row">
                                        <div class="span5">
                                            <p class="fg-color-greenDark"><asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:Resource, Email %>" /></p>
                                            <div class="input-control text span5">
                                                <asp:TextBox runat="server" TabIndex="1" ID="txtEmail" placeholder="Enter your email" class="validate[required,custom[email]] text-input"></asp:TextBox>
                                                <button class="helper"></button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="span5">
                                            <p class="fg-color-greenDark"><asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:Resource, Password %>" /></p>
                                            <div class="input-control password span5">
                                               <asp:TextBox runat="server" TabIndex="2" TextMode="Password" ID="txtPwd"  placeholder="Enter password" class="validate[required]" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <label onclick="" class="input-control checkbox">
                                        <input type="checkbox" value="1" checked="checked" id="chkRemember" runat="server"/>
                                        <span class="helper"><asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:Resource, Index_Login_Remember %>" /></span>
                                    </label>
                                    <p><a href="ForgetPassword.aspx"><asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:Resource, Index_Login_Forgot %>" /></a></p>
                                </fieldset>
                            </div>
                        </div>
                        <div class="row">
                            <div class="span10">
                                <p class="tlc"><a class="button bg-color-green5 fg-color-white" href="UserRegist.aspx"><asp:Localize ID="Localize7" runat="server" Text="<%$ Resources:Resource, Regist %>" /></a> <asp:Button ID="btnLogin"  CssClass="bg-color-green5" runat="server" Text="<%$ Resources:Resource, Submit %>" onclick="btnLogin_Click" /></p>
                            </div>
                        </div>
                    </div>
            </div>
        </div>
    </div>
</form>
 </asp:Content>
