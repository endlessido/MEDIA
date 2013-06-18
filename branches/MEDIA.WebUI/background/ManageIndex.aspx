<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageIndex.aspx.cs" Inherits="MEDIA.WebUI.background.ManageIndex" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<title>FA Raetia Media</title>
<meta name="description" content="" />
<meta name="keywords" content="" />
<link href="../css/modern.css" rel="stylesheet" />
<link href="../css/modern-responsive.css" rel="stylesheet" />
<link href="../css/manager.css" rel="stylesheet" />
<script type="text/javascript" src="../js/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="../js/modern/dropdown.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <div class="page-header">
        <div class="page-header-content">
         <h2><i class="icon-home"></i> Dashboard</h2>
        </div>
      </div>
      <div class="page-region bg-color-white">
        <div class="page-region-content">
                    <div class="page-dashboard">
                        <h4>Welcome Mr. <%= AdminName%>!</h4>
                        <dl class="page-dash-box">
                            <dt class="bg-color-darken fg-color-white">At A Glance</dt>
                            <dd class="clearfix">
                                <div class="page-glance-box fl">
                                    <h5>Projects</h5>
                                    <ul>
                                        <li><%=NewlyProjectCount %> newly applied projects</li>
                                        <li><%=GoingProjectCount %> ongoing projects</li>
                                        <li><%=EndProjectCount %> ended projects</li>
                                    </ul>
                                </div>
                                <div class="page-glance-box fl">
                                    <h5>Shop goods</h5>
                                    <ul>
                                        <li><%= GoodyDonatedCount %> donated goodies</li>
                                        <li><%= UnPaymentCount%> new orders</li>
                                        <li><%= CompletedCount%> completed orders</li>
                                    </ul>
                                </div>
                                <div class="page-glance-box fl">
                                    <h5>Users</h5>
                                    <ul>
                                        <li><%= TotalUserCount%> users</li>
                                        <li><%= NewlyUserCount%> new registed users</li>
                                        <li><%= NewlyBlogs%> uncheck blogs</li>
                                    </ul>
                                </div>
                            </dd>
                        </dl>
                        <dl class="page-dash-box">
                            <dt class="bg-color-darken fg-color-white tlr"><span class="fl">Newly <%=NewlyProjectCount %> Applied Projects</span><a href="ManageProjectList.aspx" class="fg-color-white">More<i class="icon-enter"></i></a></dt>
                            <dd class="clearfix">
                            <asp:Repeater ID="RepeaterProject" runat="server">
                            <HeaderTemplate>
                            <table class="hovered bordered">
                             <thead>
                                <tr>
                                    <th>Project Name</th>
                                    <th class="right">Donation Target</th>
                                    <th class="right">Region</th>
                                    <th class="right">Duration</th>
                                    <th class="right">Operation</th>
                                </tr>
                            </thead>
                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>  
                                        <tr>
                                            <td><a href="ManageProjectDetail.aspx?pid=<%# Eval("ProjectId") %>"><%# Eval("ProjectName")%></a></td>
                                            <td class="right"><%# Eval("CurrencyStr")%> <%# Eval("TotalFunding")%> </td>
                                            <td class="right"><%# Eval("AreaName")%>, <%# Eval("CountryName")%> </td>
                                            <td class="right"><%# this.GetProjectLeaveDays((System.DateTime?)Eval("EndDate")) %>days </td>
                                            <td class="right"><a href="ManageProjectDetail.aspx?pid=<%# Eval("ProjectId") %>">Verify this project</a></td>
                                        </tr>
                            </ItemTemplate>
                            <FooterTemplate> 
                            </tbody>
                            </table>
                            </FooterTemplate>
                            </asp:Repeater>    
                            </dd>
                        </dl>

                         <dl class="page-dash-box">
                            <dt class="bg-color-darken fg-color-white tlr"><span class="fl">Most Recent <%= UnPaymentCount%> Orders</span><a href="ManageOrderList.aspx?type=newly" class="fg-color-white">More<i class="icon-enter"></i></a></dt>
                            <dd class="clearfix">
                                <table class="hovered bordered">
                                    <thead>
                                        <tr>
                                            <th>Order Number</th>
                                            <th>Date</th>
                                            <th class="right">Customer</th>
                                            <th class="right">Total</th>
                                            <th class="right">Operation</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    <asp:Repeater ID="RepOrder" runat="server">
                                    <ItemTemplate>
                                     <tr>
                                            <td ><%# Eval("OrderId")%></td>
                                            <td class="right"><%# Eval("OrderDate")%></td>
                                            <td class="right"><%# Eval("Customer")%></td>
                                            <td class="right"><%# Eval("TotalPrice")%> <%# Eval("CurrencyStr")%></td>
                                            <td class="right"><a href="#">Details</a></td>
                                     </tr>
                                    </ItemTemplate>
                                    </asp:Repeater>
                                    </tbody>
                                </table>
                            </dd>
                        </dl>

                        <dl class="page-dash-box">
                            <dt class="bg-color-darken fg-color-white tlr"><span class="fl">Most Popular <%= PopularGoodyCount%> Donated Goodies</span><a href="ManageGoodyList.aspx" class="fg-color-white">More<i class="icon-enter"></i></a></dt>
                            <dd class="clearfix">
                              <table class="hovered bordered">
                                    <thead>
                                        <tr>
                                            <th>Goodie Name</th>
                                            <th class="right">Price</th>
                                            <th class="right">Sales Nums</th>
                                            <th class="right">Remain Nums</th>
                                            <th class="right">Belongs Project Name</th>
                                            <th class="right">Opeartion</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                              <asp:Repeater  runat="server" ID="RepGoodies">
                                  <ItemTemplate>
                                    <tr>
                                    <td><a href="ManageDonators.aspx?gid=<%# Eval("Id") %>"><%# Eval("Title") %></a></td>
                                    <td class="right"><%# Eval("CurrencyStr")%> <%# Eval("Price")%></td>
                                    <td class="right"><%# Eval("SaleNum")%></td>
                                    <td class="right"><%# GetRemainNum(Eval("Num"),Eval("SaleNum"),Eval("IsLimit"))%></td>
                                    <td class="right"><%# Eval("DonationProject.ProjectName")%></td>
                                    <td class="right"><a href="ManageDonators.aspx?gid=<%# Eval("Id") %>">View the donators</a></td>
                                    </tr>
                                 </ItemTemplate>
                            </asp:Repeater>
                                </tbody>
                                </table>
                            </dd>
                        </dl>
                        <dl class="page-dash-box">
                            <dt class="bg-color-darken fg-color-white tlr"><span class="fl"><%= NewlyBlogs %> UnCheck Blogs</span><a href="ManageBlogList.aspx" class="fg-color-white">More<i class="icon-enter"></i></a></dt>
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
                        <dl class="page-dash-box">
                            <dt class="bg-color-darken fg-color-white tlr"><span class="fl">Newly Registed <%=NewlyUserCount %> Users</span><a href="ManageUserList.aspx" class="fg-color-white">More<i class="icon-enter"></i></a></dt>
                            <dd class="clearfix">
                            <table class="hovered bordered">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th class="right">Email</th>
                                        <th class="right">Gender</th>
                                        <th class="right">Birthday</th>
                                    </tr>
                                </thead>
                                <tbody>
                                <asp:Repeater runat="server" ID="RepeaterUser">
                                  <ItemTemplate>  
                                    <tr>
                                        <td><a href="ManageUserDetail.aspx?uid=<%# Eval("UserId")%>"><%# Eval("FirstName")%> <%# Eval("LastName")%></a></td>
                                        <td class="right"><%# Eval("EmailAddress")%></td>
                                        <td class="right"><%# this.GetSex((bool)Eval("Gender"))%></td>
                                        <td class="right"><%# ((DateTime)Eval("Birthday")).ToShortDateString()%></td>
                                    </tr>
                                    </ItemTemplate>
                                 </asp:Repeater>  
                                 </tbody>
                                </table>
                            </dd>
                        </dl>
                    </div>
                </div>
      </div>
    </form>
</body>
</html>
