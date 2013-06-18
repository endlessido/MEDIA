<%@ Page Title="Index" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true"  CodeBehind="Index.aspx.cs" Inherits="MEDIA.WebUI.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    jQuery(document).ready(function ($) {
        $('#Indexform').validationEngine();
        $('#page-search').on('click', 'input[type="text"]', function (event) {
            event.preventDefault();
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page">
            <div class="page-header">
                <div class="page-header-content">
                    <h1 class="tlc page-index-header"><img src="images/logo.png" width="100" height="100" alt="UNICORN"><span class="ibk">SUPPORTYOURLOCALTEAM</span></h1>
                </div>
            </div>
            <div class="page-region">
                <div class="page-region-content clearfix">
                   <div class="tile bg-hvwki double double-vertical bg-color-green1">
                        <a href="About.aspx">
                            <div class="tile-content">
                                <p class="tile-desc tlr"><span class="icon-help page-tileico"><!--HELP ICON--></span></p>
                                <h2><asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:Resource, Index_What_Title %>" /> </h2>
                                <p class="tile-desc tile-text"><asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:Resource, Index_What_Summary %>" /> </p>
                            </div>
                        </a>
                    </div>
                    <div class="tile quadro tile-last double-vertical bg-color-red1">
                        <div class="tile-content">
                            <h3 class="fg-color-white"><asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:Resource, Index_Project_Title %>" />:</h3>
                             <div class="hero-unit">
                                <div data-param-duration="300" data-param-effect="fade" data-role="carousel" class="carousel" id="carousel1">
                                    <div class="slides bg-color-red1" >
                                        <asp:Repeater runat="server" ID="RepeaterProject">
                                            <ItemTemplate>
                                            <div class="slide">
                                                <a href="ProjectDetails.aspx?pid=<%# Eval("ProjectId") %>">
                                                <h2  class="fg-color-white"><%# Eval("ProjectName")%></h2>
                                                <p class="bg-color-white padding10 fg-color-darken"><img src="<%# GetProjectImage(Eval("ImgUrl")).Substring(1)  %>" alt="PROJECT NAME" width="84" height="84"> <%# Eval("ProjectSummary")%></p>
                                                <h3><small class="fg-color-white"><asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:Resource, Index_Project_Goody %>" />:</small></h3>
                                                <p class="fg-color-white"><%# GetGoodiesTitle(Eval("Goodies"))%></p>
                                                </a>
                                            </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <span class="control left"><i class="icon-arrow-left"></i></span>
                                    <span class="control right"><i class="icon-arrow-right"></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tile bg-hvytb double double-vertical bg-color-green2">
                        <a href="CreateProject.aspx">
                            <div class="tile-content">
                                <h2><asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:Resource, Index_Project_Create %>"/></h2>
                                <p class="tile-desc tlc">
                                    <span class="icon-film page-tileico"><!--FILM ICON--></span>
                                    <span class="icon-upload page-tileico"><!--UPLOAD ICON--></span>
                                    <span class="icon-comments-4 page-tileico"><!--COMMENT ICON--></span>
                                </p>
                                <p class="tile-desc tile-text"><asp:Localize ID="Localize7" runat="server" Text="<%$ Resources:Resource, Index_Project_Summary %>"/></p>
                            </div>
                        </a>
                    </div>
                    <form id="searchForm" method="post" action="ProjectList.aspx">
                     <div id="page-search" class="tile double double-vertical bg-color-green3">
                        <a href="ProjectList.aspx">
                            <div class="tile-content">
                                <p class="tile-desc tlr"><span class="icon-search page-tileico"><!--HELP ICON--></span></p>
                                <h2>Search project</h2>
                                <div class="input-control text tile-desc">
                                    <input type="text" name="searchname" tabindex="1" class="fg-color-darken border-color-white">
                                    <input type="submit" class="btn-search" value="&#xE041;"/>
                                </div>
                            </div>
                        </a>
                    </div>
                    </form>
                    <div class="tile double tile-last double-vertical bg-color-green4">
                    <form  runat="server" id="Indexform" >
                    <div class="tile-content">
                        <% if (!isLogin)
                           {%>
                            <h3>My Account</h3>
                            <p class="tile-desc tlr"><span class="icon-user-3 page-tileico"><!--BASKET ICON--></span></p>
                            <div class="input-control text">
                                <input  runat="server" tabindex="2" id="TextEmail" placeholder="Enter your email" class="validate[required,custom[email]] text-input fg-color-darken border-color-white" data-prompt-position="topLeft" />
                                <button class="helper"></button>
                            </div>
                            <div class="input-control password">
                                <input type="password"  tabindex="3" runat="server" id="TextPwd" placeholder="Enter password" class="validate[required] fg-color-darken border-color-white" data-prompt-position="topLeft"/>
                                <button class="helper"></button>
                            </div>
                            <label class="input-control checkbox">
                                <input type="checkbox" checked="checked" runat="server" id="chkIndexRemember">
                                <span class="helper"><asp:Localize ID="Localize11" runat="server" Text="<%$ Resources:Resource, Index_Login_Remember %>" /></span>
                            </label>
                            <asp:Button ID="Button1"  CssClass="bg-color-grayDark mini" runat ="server" Text="<%$ Resources:Resource, Submit %>" OnClick="btnLogin_Click"/>
                            <p><a href="UserRegist.aspx" class="fg-color-white">Create Account</a>&nbsp;&nbsp;/&nbsp;&nbsp;<a href="ForgetPassword.aspx" class="fg-color-white"><asp:Localize ID="Localize12" runat="server" Text="<%$ Resources:Resource, Index_Login_Forgot %>" /></a></p>
                        <%}
                           else
                           { %>
                            <h3><%= UserName%></h3>
                            <p class="tile-desc tlr"><span class="icon-user-3 page-tileico"><!--BASKET ICON--></span></p>
                            <ul class="tile-logged unstyled">
                                <li><a href="MyProfile.aspx?frame=2"><i class="icon-user-3"></i> Profile</a></li>
                                <li><a href="MyProfile.aspx?frame=0"><i class="icon-gift"></i> Founded Projects</a></li>
                                <li><a href="MyProfile.aspx?frame=1"><i class="icon-gift"></i> Donated Projects</a></li>
                            </ul>
                            <p class="tlr"><a href="LoginOut.aspx" class="">Logout</a></p>
                        <%} %>
                        </div>
                    </form>
                    </div>
                </div>
        </div>
 </div>
</asp:Content>
