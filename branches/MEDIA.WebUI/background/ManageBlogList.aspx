<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageBlogList.aspx.cs" Inherits="MEDIA.WebUI.background.ManageBlogList" %>

<%@ Register Assembly="MEDIA.WebUI" Namespace="MEDIA.WebUI" TagPrefix="cc1" %>
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
</head>
<body>
<div class="page-header">
    <div class="page-header-content">
        <h2>Blog List</h2>
    </div>
</div>
<div class="page-region bg-color-white">
    <div class="page-region-content">
        <div class="page-dashboard">
        <dl class="page-dash-box">
                            <dd class="clearfix">
                                <table class="hovered bordered">
                                    <thead>
                                        <tr>
                                            <th>Title</th>
                                            <th class="right">CreateDate</th>
                                            <th class="right">Project Name</th>
                                            <th class="right">Author</th>
                                            <th class="right">Operation</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    <asp:Repeater runat="server" ID="RepeaterBlogs">
                                    <ItemTemplate>
                                        <tr>
                                            <td><a href="ManageBlog.aspx?bid=<%# Eval("blogId") %>"><%# Eval("Title")%></a></td>
                                            <td class="right"><%# Eval("CreateDate")%></td>
                                            <td class="right"><%# Eval("DonationProject.ProjectName")%></td>
                                            <td class="right"><%# Eval("DonationProject.User.LastName")%></td>
                                            <td class="right"><a href="ManageBlog.aspx?bid=<%# Eval("blogId") %>">Verify this blog</a></td>
                                        </tr>
                                    </ItemTemplate>
                                    </asp:Repeater>
                                    </tbody>
                                </table>
                            </dd>
            </dl>
            <div class="pagination tlc">
                <cc1:CustomPager ID="CustomPager1" PageSize="10" RequiredMethod="get" runat="server" />
            </div>
        </div>
    </div>
</div>
</body>
</html>