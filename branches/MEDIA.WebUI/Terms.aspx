<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="Terms.aspx.cs" Inherits="MEDIA.WebUI.Terms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="page secondary">
        <div class="page-header">
            <div class="page-header-content">
                <h1>Terms &amp; Conditions <small>&nbsp;</small></h1>
            </div>
        </div>
        <div class="page-region">
            <div class="page-region-content">
                <div class="grid">
                    <div class="row">
                        <div class="span10">
                         <asp:Label runat="server" ID="lblTerms" ></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
