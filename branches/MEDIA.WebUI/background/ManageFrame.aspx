<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageFrame.aspx.cs" Inherits="MEDIA.WebUI.background.WebForm1" %>

<!DOCTYPE html>
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
<script type="text/javascript" src="../js/modern/multidropdown.js"></script>
<script type="text/javascript" src="../js/jquery.iframe-auto-height.plugin.1.9.0.min.js"></script>
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        $('iframe').iframeAutoHeight({
            triggerFunctions: [function (resizeFunction, iframe) {
                $(iframe).on('load', function () {
                    $(this).contents().find('.page-mngregion,.page-mngarea').on('click', function () {
                        resizeFunction(iframe);
                    })
                })
            } ]
        });

//        var menus = $('.multidropdown');
//        for (var i = 0; i < menus.length; i++) {
////            menus.eq(i).on('click', function (event) {
////                event.preventDefault();
////            });
//        }

        $('#ul-navigate li a').on('click', function () {
            $('#ul-navigate li a').removeClass("page-sidebar-curr");
            $(this).addClass("page-sidebar-curr");
        });
    });
</script>
</head>
<body>
    <div class="topbar bg-color-darken">
        <h1 class="topbar-title fl fg-color-lighten"><i class="icon-cog"></i><a href="#" class="fg-color-lighten">FA Raetia Media Control Panel</a></h1>
        <ul class="topbar-link fr">
            <li class="fl fg-color-lighten"><i class="icon-user-3"></i><a href="#" class="fg-color-lighten"><%= AdminName%></a></li>
            <li class="fl fg-color-lighten"><a href="Logout.aspx" class="fg-color-lighten">Logout</a></li>
        </ul>
    </div>
    <div class="wrapper">
        <div class="side bg-color-grayDark">
            <div class="page-sidebar">
                <ul id="ul-navigate">
                    <li><a href="ManageIndex.aspx" target="MainFrame" class="page-sidebar-curr"><i class="icon-home"></i> Dashboard</a></li>
                    <li data-role="dropdown">
                        <a href="#"><i class="icon-stats-up"></i> Manage Projects</a>
                        <ul class="sub-menu sidebar-dropdown-menu multidropdown">
                            <li><a href="ManageProjectList.aspx?type=newly" target="MainFrame"><i class="icon-list"></i> Newly Applied</a></li>
                            <li><a href="ManageProjectList.aspx?type=ongoing" target="MainFrame"><i class="icon-list"></i> Ongoing Projects</a></li>
                            <li><a href="ManageProjectList.aspx?type=ended" target="MainFrame"><i class="icon-list"></i> Ended Projects</a></li>
                        </ul>
                    </li>
                    <li data-role="dropdown">
                        <a href="#"><i class="icon-basket"></i> Manage Orders</a>
                        <ul class="sub-menu sidebar-dropdown-menu multidropdown">
                            <li><a href="ManageOrderList.aspx?type=newly" target="MainFrame"><i class="icon-grid"></i> New Orders</a></li>
                            <li><a href="ManageOrderList.aspx?type=completed" target="MainFrame"><i class="icon-stats"></i> Completed Orders</a></li>
                        </ul>
                    </li>
                    <li >
                        <a href="ManageRegion.aspx" target="MainFrame"><i class="icon-rocket"></i> Manage Region</a>
                    </li>
                    <li >
                        <a href="ManageCategory.aspx" target="MainFrame"><i class="icon-support"></i> Manage Sport Category</a>
                    </li>
                    <li data-role="dropdown">
                        <a href="#"><i class="icon-user-3"></i> Manage Users</a>
                        <ul class="sub-menu sidebar-dropdown-menu multidropdown">
                            <li><a href="ManageUserList.aspx?type=newly" target="MainFrame"><i class="icon-list"></i> Newly User </a></li>
                            <li><a href="ManageUserList.aspx?type=all" target="MainFrame"><i class="icon-list"></i> All User</a></li>
                        </ul>
                    </li>
                     <li data-role="dropdown">
                        <a href="#"><i class="icon-mail"></i> Manage Emails</a>
                        <ul class="sub-menu sidebar-dropdown-menu multidropdown">
                            <li><a href="ManageEmail.aspx?type=active" target="MainFrame"><i class="icon-list"></i> Active Account Email</a></li>
                            <li><a href="ManageEmail.aspx?type=forget" target="MainFrame"><i class="icon-list"></i> Forget Password Email</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
        <div class="container">
            <iframe name="MainFrame" src="ManageIndex.aspx" frameborder="0" width="100%" scrolling="no" sandbox="allow-same-origin allow-scripts allow-forms"></iframe>
        </div>
    </div>
</body>
</html>