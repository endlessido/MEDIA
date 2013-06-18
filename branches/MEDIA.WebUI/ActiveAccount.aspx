<%@ Page Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="ActiveAccount.aspx.cs" Inherits="MEDIA.WebUI.ActiveAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        run();             //加载页面时启动定时器  
        var interval;
        function run() {
            interval = setInterval(setSeconds, "1000");
        }
        function setSeconds() {
            var sec = $('#sp_seconds').text();
            if (sec == 0) {
                clearTimeout(interval);  //关闭定时器  
                window.location.href = "UserLogin.aspx";
            }
            else {
                sec = sec - 1;
                $('#sp_seconds').text(sec);
            }
        }
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="page secondary">
        <div class="page-header">
            <div class="page-header-content">
                <h1>Activate Account<small>&nbsp;</small></h1>
                <a class="back-button big page-back" href="/"></a>
            </div>
        </div>
         
        <div class="page-region">
            <div class="page-region-content">
                <div class="grid">
                    <div class="row">
                        <div class="span10 border-color-darken page-activate">
                            <h3 class="fg-color-orangeDark">Thank you for acitvate your accout, <asp:Label runat="server" ID="lblUserName" />.</h3>
                            <p>Now you can use your account to Lorem ipsum dolor sit amet, consectetur adipisicing elit. Distinctio ab ea dolor numquam a praesentium nesciunt totam inventore voluptatem. Facilis sed eaque quam aut consectetur atque est totam maxime eius.</p>
                            <p>You will be redirected to Login page in <span id="sp_seconds" style="color:Red">5</span> seconds, if not please <a href="UserLogin.aspx">click here...</a></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
