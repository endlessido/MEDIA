<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="BlogDetails.aspx.cs" Inherits="MEDIA.WebUI.BlogDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="page secondary with-sidebar">
        <div class="page-header">
            <div class="page-header-content">
                <h1>Name is free<small>&nbsp;</small></h1>
                <a class="back-button big page-back" href="/"></a>
            </div>
        </div>
        <div class="page-sidebar">
            <div class="tile tile-donate bg-color-red as-block">
                 <a href="ProjectDetails.aspx?pid=<%=pid %>">
                    <div class="tile-content">
                        <h3><asp:Label runat="server" ID="lblProjectName" /> <asp:Label runat="server" ID="lblProjectNeed"/></h3>
                        <p><asp:Label runat="server" ID="lblProjectSummary"/> </p>
                        <div class="brand">
                            <p class="tile-desc"><span class="icon-arrow-right-3 page-lnkico"><!--ARROW ICON--></span> Project Detail</p>
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="page-region">
            <div class="page-region-content">
                <div class="blog-text">
                    <h3><asp:Label runat="server" ID="lblTitle"></asp:Label> </h3>
                    <h5><asp:Label runat="server" ID="lblUserName"/> @ <asp:Label runat="server" ID="lblBlogDate"/> </h5>
                    <asp:Image Width="630" Height="310" runat="server" ID="imgblog" />
                   <asp:Label runat="server" ID="lblblogContent"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
