<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="ProjectDetails.aspx.cs" Inherits="MEDIA.WebUI.ProjectDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            var params = { allowScriptAccess: "always",allowFullScreen: "true" };
            var atts = { id: "myplayer" };
            var url = "http://www.youtube.com/v/<%=YoutubeUrl %>?enablejsapi=1&playerapiid=ytplayer";
            swfobject.embedSWF(url,"ytapiplayer", "710", "435", "8", null, null, params, atts);
            NewBlog(false);
        });

        jQuery(document).ready(function ($) {
            $('.page-shares').share({
                networks: ['facebook', 'googleplus', 'twitter', 'tumblr'],
                theme: 'square'
            });
        });

        function NewBlog(isShow) {
            if (isShow) {
                jQuery('#blog_form').show();
            }
            else {
                jQuery('#blog_form').hide();
            }
        }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="page secondary with-sidebar">
        <div class="page-header">
            <div class="page-header-content">
                <h1><%= model.ProjectName %><small>&nbsp;</small></h1>
                <a class="back-button big page-back" href="ProjectList.aspx"></a>
            </div>
        </div>
        <div class="page-sidebar">
            <div class="tile tile-donate bg-color-red as-block">
                <a href="Donate.aspx?pid=<%= model.ProjectId %>">
                    <div class="tile-content">
                        <p class="tile-desc tlc">
                            <span class="icon-flattr page-sideico"><!--FLATTR ICON--></span>
                        </p>
                        <h2><asp:Localize ID="Localize7" runat="server" Text="<%$ Resources:Resource, Donate %>" /></h2>
                        <p><asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:Resource, VIA %>" /></p>
                        <div class="brand">
                            <p class="tile-desc"><span class="icon-arrow-right-3 page-lnkico"><!--ARROW ICON--></span><asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:Resource, Index_Project_Go %>" /> </p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="page-sidebox">
                <h2><%=model.DonateNum %> &nbsp;<asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:Resource, supporters %>" /><small> <asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:Resource, had_donated %>" /></small></h2>
            </div>
            <p class="page-blognewlink tlr"><a href="BlogPost.aspx?pid=<%=ProjectId %>" class="page-blognew fg-color-blue"><%=WriteBlog %></a></p>
             <dl class="page-blogrecent">
             <asp:Repeater runat="server" ID="RepBlog" >
                <ItemTemplate>
                <dd><a href="BlogDetails.aspx?bid=<%# Eval("blogId") %>" class="tlr"><span class="fl"><%# Eval("Title")%></span><small><%# ((DateTime)Eval("CreateDate")).ToShortDateString()%></small></a></dd>
                </ItemTemplate>
                <FooterTemplate>
                <span class="page-blognewlink tlr"><a href="BlogList.aspx?pid=<%=ProjectId %>" class="page-blognew fg-color-blue">more >></a></span>
                </FooterTemplate>
                </asp:Repeater>
            </dl>
             <div class="page-shares"></div>
        </div>
        <div class="page-region">
            <div class="page-region-content">
                <div id="ytapiplayer" class="player"></div>
                <div class="grid">
                    <div class="row">
                         <div class="page-fundinfo span9 bg-color-red">
                        <h3 class="fg-color-white"><asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:Resource, Fun_info %>" /></h3>
                        <ul class="fg-color-white">
                            <li><asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:Resource, Total_Date %>" />: <%= base.GetCurrency(model.ReceivedFunding.Value) %> <%= model.CurrencyStr %></li>
                        <li><asp:Localize ID="Localize8" runat="server" Text="<%$ Resources:Resource, Remain_Goal %>" />: <%= base.GetCurrency(model.TotalFunding.Value - model.ReceivedFunding.Value)%> <%= model.CurrencyStr %></li>
                        <li><asp:Localize ID="Localize10" runat="server" Text="<%$ Resources:Resource, Total_Goal %>" />: <%= base.GetCurrency(model.TotalFunding.Value)%> <%= model.CurrencyStr %></li>
                        </ul>
                    </div>

                        <div class="page-prgs span6 bg-color-green6">
                            <h3 class="fg-color-white"><%=Percent%>%<small> of 100% of the goal</small></h3>
                            <div class="progress-bar bg-color-white">
                                <div class="bar bg-color-yellow" style="width: <%=Percent%>%"></div>
                            </div>
                        </div>
                        <div class="page-prgs tile-last span3 bg-color-green7">
                            <h3 class="fg-color-white tlc"><%=leaveDays%> Days left</h3>
                            <div class="progress-bar bg-color-white">
                                <div class="bar bg-color-yellow" style="width: <%=leaveDays%>%"></div>
                            </div>
                        </div>
                        <asp:Repeater ID="RepeaterGoodys" runat="server">
                        <ItemTemplate>
                           <div class="<%# GetDivCssClass(MEDIA.WebUI.SysConfigConst.GoodyClass,3) %>">
                            <h3 class="fg-color-white"><%# Eval("Title")%> <br><small>for <%# Eval("CurrencyStr")%> <%# base.GetCurrency((decimal)Eval("Price"))%></small></h3>
                            <p class="fg-color-white">Gone:<%# Eval("SaleNum")%>, Remains:<%# GetRemainNum(Eval("Num"),Eval("SaleNum"),Eval("IsLimit"))%></p>
                                <div class="progress-bar bg-color-white">
                                    <div class="bar bg-color-yellow" style="width: <%# GetPercent(Eval("Num") ,Eval("SaleNum"))%>%"></div>
                                </div>
                           </div>
                        </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="body-text">
                  <h3>Project Summary</h3>
                    <p class="long-text"><%= model.ProjectSummary%></p>
                    <h3><asp:Localize ID="Localize9" runat="server" Text="<%$ Resources:Resource, Who_are_we %>" /></h3>
                    <p class="long-text"><%= model.SelfIntroduce%></p>
                    <h3>This is our project</h3>
                    <p class="long-text"><%= model.NeedReason %></p>
                    <h3><asp:Localize ID="Localize12" runat="server" Text="Location" /></h3>
                    <ul>
                        <li> <%=model.AreaName %>, <%=model.CountryName %></li>
                    </ul>
                    <h3><asp:Localize ID="Localize20" runat="server" Text="Link" /></h3>
                    <ul>
                        <li><asp:HyperLink runat="server" Target="_blank" ID="hyperLink" ></asp:HyperLink></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
 </asp:Content>
