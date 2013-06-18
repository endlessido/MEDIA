<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="MEDIA.WebUI.About" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="page secondary">
        <div class="page-header">
            <div class="page-header-content">
                <h1>About SupportYourLocalTeam <small>&nbsp;</small></h1>
                <a class="back-button big page-back" href="Index.aspx"></a>
            </div>
        </div>
        <div class="page-region">
            <div class="page-region-content">
                <div class="grid">
                    <div class="row">
                        <div class="span10">
                        <asp:Label runat="server" ID="lblAbout" ></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
