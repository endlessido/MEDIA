<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageProjectDetail.aspx.cs" Inherits="MEDIA.WebUI.background.ManageProjectDetail" %>
<!doctype html>
<html>
<head>
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
<title>FA Raetia Media</title>
<meta name="description" content="">
<meta name="keywords" content="">
<link href="../css/modern.css" rel="stylesheet">
<link href="../css/modern-responsive.css" rel="stylesheet">
<link href="../css/manager.css" rel="stylesheet">
<script type="text/javascript" src="../js/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="../swfobject/swfobject.js"></script>
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        var params = { allowScriptAccess: "always", allowFullScreen: "true" };
        var atts = { id: "myplayer" };
        var url = "http://www.youtube.com/v/<%=YoutubeUrl %>?enablejsapi=1&playerapiid=ytplayer";
        swfobject.embedSWF(url,
                        "ytapiplayer", "710", "435", "8", null, null, params, atts);
        
    });
</script>
</head>
<body>
<form id="form1" runat="server">
<div class="page-header">
    <div class="page-header-content">
        <h2>New Project Detail</h2>
    </div>
</div>
<div class="page-region bg-color-white">
    <div class="page-region-content">
        <div class="page-dashboard">
            <h3><span class="fg-color-red"><%= model.ProjectName %></span> needs <span class="fg-color-red"><%= model.Need %></span></h3>
            <h4>Project Category: <span class="fg-color-blue"><%= model.CategoryName %></span></h4>
            <h4>Project Region: <span class="fg-color-blue"><%= model.AreaName %></span>, <span class="fg-color-blue"><%= model.CountryName %></span></h4>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Project Summary</span></dt>
                <dd><%= model.ProjectSummary %></dd>
            </dl>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">About Project Founder</span></dt>
                <dd><%= model.SelfIntroduce %></dd>
            </dl>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Project Reason</span></dt>
                <dd><%= model.NeedReason %></dd>
            </dl>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Founder's Vision</span></dt>
                <dd><%= model.YourVision %></dd>
            </dl>
            <h4>Project Time span: <span class="fg-color-blue"><%# GetProjectLeaveDays(model.EndDate) %> days</span></h4>
            <h4>How much the donation will collect: <span class="fg-color-blue"><%= model.CurrencyStr %> <%= model.TotalFunding %></span></h4>
            <h4>How much the donation has collected: <span class="fg-color-red"><%= model.CurrencyStr %> <%= model.ReceivedFunding %></span></h4>
            <h4>The donation percent: <span class="fg-color-blue"><%= GetPercent(model.TotalFunding,model.ReceivedFunding)%>%</span></h4>
            <h4>Founder's homepage: <a class="fg-color-blue" href="#"><%= model.Homepage %></a></h4>
            <h4>Founder's facebook: <a class="fg-color-blue" href="#"><%= model.Feedbookpage %></a></h4>
            <p>Project Image: <br><asp:Image runat="server" ID="ProjectImg" alt="project name" width="150" height="150"/></p>
            <p>Project Video:</p>
            <div id="ytapiplayer" class="player"></div>
            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Goodies for donors</span></dt>
                <asp:Repeater ID="RepGoody" runat="server" >
                <ItemTemplate>
                    <dd>
                        <table class="bordered striped hovered">
                            <tr>
                                <td style="width:15%">Name:</td>
                                <td style="width:85%"><%# Eval("Title")%></td>
                            </tr>
                            <tr>
                                <td style="width:15%">Description:</td>
                                <td style="width:85%"><%# Eval("Description")%></td>
                            </tr>
                            <tr>
                                <td style="width:15%">Quantity:</td><td style="width:85%"><%# GetRemainNum(Eval("Num"),Eval("SaleNum"),Eval("IsLimit"))%></td>
                            </tr>
                             <tr>
                                <td style="width:15%">SaleNum:</td><td style="width:85%"><%# Eval("SaleNum")%></td>
                            </tr>
                            <tr>
                                <td style="width:15%">How much:</td><td style="width:85%"><%# Eval("CurrencyStr")%> <%# Eval("Price")%></td>
                            </tr>
                               <tr>
                                <td style="width:15%"><%# GetDonatorsCount(Eval("Id"))%> Donors:</td>
                                <td style="width:85%">
                                    <ul class="page-donors clearfix">
                                        <asp:Repeater ID="RepUser" runat="server">
                                        <ItemTemplate>
                                        <li><%# Eval("LastName") %></li>
                                        </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                    <p><a href="#">Show full list</a></p>
                                </td>
                                </tr>
                        </table>
                    </dd>
                </ItemTemplate>
                </asp:Repeater>
            </dl>

            <dl class="page-dash-box">
                <dt class="bg-color-darken fg-color-white clearfix tlr"><span class="fl">Orders</span></dt>
                 <asp:Repeater runat="server" ID="RepOrder">
                    <ItemTemplate>
                    <h4>Order No.<%# Eval("OrderId")%>&nbsp;&nbsp;&nbsp;&nbsp;Bargain made at <%# Eval("OrderDate")%></h4>
                        <div class="page-cart-item">
                            <table class="bordered">
                                <tr>
                                <td>Goodie Name</td>
                                <td>Goodie UnitPrice</td>
                                <td>Is Payment</td>
                                <td>TotalPrice</td>
                                </tr>
                                <tr>
                                    <td width="250" class="tlc"><%# Eval("Goodies") %></td>
                                    <td width="400"><%# Eval("UnitPrices")%></td>
                                    <td ><span class="fg-color-red"><%# Eval("IsPayment")%></span></td>
                                    <td>Overall Price: <%# Eval("TotalPrice")%> <%# Eval("CurrencyStr")%></td>
                                </tr>
                            </table>
                        </div>
                    </ItemTemplate>
                    </asp:Repeater>
            </dl>

            <p> 
            <asp:Button ID="btnApprove"  CssClass="standart default" runat="server" CommandName="BtnCheck" OnCommand="BtnCheck_Command" />
            </p>
        </div>
    </div>
</div>
</form>
</body>
</html>