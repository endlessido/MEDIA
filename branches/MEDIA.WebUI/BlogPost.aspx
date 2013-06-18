<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="BlogPost.aspx.cs" Inherits="MEDIA.WebUI.BlogPost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="js/tiny_mce/jquery.tinymce.js"></script>
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        $('#page-upfile>input[type="file"]').on('change', function () {
            var path = $(this).val();
            var lastIndex = path.lastIndexOf(".");
            var filefix = path.substring(lastIndex, path.length);
            $(this).siblings('.input-control').children('input[type="text"]').val(path);
        });

        $('#blogPost').validationEngine();
        $('textarea').tinymce({
            // Location of TinyMCE script
            script_url: 'js/tiny_mce/tiny_mce.js',
            plugins: "table,insertdatetime,preview",
            // General options
            mode: "textareas",
            theme: "advanced",
            theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,cleanup,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
            theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom"
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form runat="server" id="blogPost">
<div class="page secondary with-sidebar">
        <div class="page-header">
            <div class="page-header-content">
                <h1>Blog Writing <small>&nbsp;</small></h1>
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
                <div class="grid">
                    <div class="row">
                        <h3>Blog Title <span class="fg-color-red">*</span></h3>
                        <div class="input-control text">
                            <input type="text" id="txtTitle"  runat="server"/>
                            <button class="helper"></button>
                        </div>
                        <h3>Upload a image for your project</h3>
                         <div class="as-block" id="page-upfile">
                            <div class="input-control text span5">
                                <input id="projectImg" type="text" tabindex="13" autocomplete="off"  placeholder="Select a image, JPG/GIF/PNG allowed" />
                            </div>
                            <input type="button" value="Browse" tabindex="13" id="fake-file">
                            <asp:FileUpload ID="uploadFile" tabindex="13" runat="server" size="54" CssClass="validate[custom[imageType]]" />
                        </div>

                        <h3>Blog Content <span class="fg-color-red">*</span></h3>
                        <div class="input-control textarea">
                            <textarea id="pageblogedit"  runat="server">
                            </textarea>
                        </div>
                        <asp:Button CssClass="bg-color-greenDark fg-color-white" runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</form>
</asp:Content>
