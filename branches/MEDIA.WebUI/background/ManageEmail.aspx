<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageEmail.aspx.cs" Inherits="MEDIA.WebUI.background.ManageEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link href="../css/modern.css" rel="stylesheet">
<link href="../css/modern-responsive.css" rel="stylesheet">
<link href="../css/manager.css" rel="stylesheet">
<script type="text/javascript" src="../js/jquery-1.8.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page-header">
	<div class="page-header-content">
		<h2><i class="icon-mail"></i> Admin Email</h2>

	</div>
</div>
<div class="page-region bg-color-white">
	<div class="page-region-content">
		<div class="page-contents">
			<h3>Mail Title</h3>
			<div class="input-control text">
				<input type="text" id="txtTitle" runat="server" />
				<button class="helper"></button>
			</div>
			<h3>Mail Content</h3>
            
			<div class="input-control textarea">
				<textarea runat="server" id="txtContent">
				</textarea>
                <h3 style="color:Red"><asp:Label runat="server" ID="lblMsg" ></asp:Label></h3>
			</div>
			<asp:Button  runat="server" ID="btnSubmit" Text="Submit" OnClick="BtnSubmit_Click"/>
		</div>
	</div>
    </form>
</body>
</html>
