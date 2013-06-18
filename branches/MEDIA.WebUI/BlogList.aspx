<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="BlogList.aspx.cs" Inherits="MEDIA.WebUI.BlogList" %>
<%@ Register Assembly="MEDIA.WebUI" Namespace="MEDIA.WebUI" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="page secondary with-sidebar">
        <div class="page-header">
            <div class="page-header-content">
                <h1><asp:Label ID="lblUserName" runat="server"/><small>&nbsp;</small></h1>
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
            <asp:Repeater ID="RepBlog" runat="server">
            <ItemTemplate>
                <div class="blog-text">
                    <h3><a href="BlogDetails.aspx?bid=<%# Eval("blogId") %>"><%# Eval("Title")%></a></h3>
                    <h5><%# Eval("CreateDate")%></h5>
                </div>
            </ItemTemplate>
            </asp:Repeater>
                <div class="pagination tlc">
                    <cc1:CustomPager ID="CustomPager1" RequiredMethod="get" PageSize="10"  runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
