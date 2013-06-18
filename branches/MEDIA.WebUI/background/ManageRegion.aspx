<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageRegion.aspx.cs" Inherits="MEDIA.WebUI.background.ManageRegion" %>
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
<script type="text/javascript" src="../js/modern/multidropdown.js"></script>
<script type="text/javascript" src="../js/modern/buttonset.js"></script>
<script type="text/javascript" src="../js/modern/dialog.js"></script>
<script type="text/javascript" src="../js/modern/input-control.js"></script>
<script type="text/javascript" src="../js/jquery.iframe-auto-height.plugin.1.9.0.min.js"></script>
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        var newForm = $('.page-nwfm').eq(0).detach().removeClass('page-nwfm'),
            newList = $('.page-nwlst').eq(0).detach().removeClass('page-nwlst');
        var isNew = false, editOldValue, editOptionId;
        $('.page-mngregion,.page-mngarea').on('click', function (event) {
            event.preventDefault();
            var sTgt = event.target,
                uTgt = $(this).children('ul'),
                lTgt = $(event.target).closest('li'),
                nTgt, edValue, rpTmpl, edTmpl;

            if (sTgt.className === 'page-nwr' || $(sTgt).parent()[0].className === 'page-nwr') {
                uTgt.append(newForm.clone());
                isNew = true;
            }

            //Del Button Logic
            if ($(sTgt).hasClass('page-del') || $(sTgt).parent().hasClass('page-del')) {
                lTgt.remove();
                var oldId = lTgt.attr("id");
                if (oldId == undefined) {
                    var delValue = lTgt.children('a').eq(0).text();
                    $("#newHidden").val($("#newHidden").val().replace(delValue, ""));
                }
                else {
                    var oldValue = $("#delHidden").val();
                    oldValue += oldId + ";";
                    $("#delHidden").val(oldValue);
                }
            }

            if ($(sTgt).hasClass('page-sub') || $(sTgt).parent().hasClass('page-sub')) {
                window.location.href = lTgt.children('a').eq(3).attr("href");
                return;
            }

            //Edit Button Logic
            if ($(sTgt).hasClass('page-edr') || $(sTgt).parent().hasClass('page-edr')) {
                edValue = lTgt.children('a').eq(0).text();
                if (!isNew) {
                    editOldValue = edValue;
                    editOptionId = lTgt.attr("id");
                }
                rpTmpl = newForm.clone();
                rpTmpl.data('origin', edValue).find('input[type="text"]').val(edValue);
                lTgt.replaceWith(rpTmpl);
            }

            //Cancel Button Logic
            if ($(sTgt).data('role') === 'undo') {
                if (lTgt.data('origin')) {
                    edTmpl = newList.clone();
                    edTmpl.children('a').eq(0).text(lTgt.data('origin'));
                    if (editOptionId != undefined) {
                        edTmpl.attr("id", editOptionId);
                        edTmpl.children('a').eq(3).attr("href", "ManageCaton.aspx?country=" + editOptionId);
                    }
                    lTgt.replaceWith(edTmpl);
                } else {
                    lTgt.remove();
                }
            }

            if ($(sTgt).data('role') === 'save') {
                var changeTxt = lTgt.find('input[type="text"]').val();
                Exists = false;
                $("a").each(function () {
                    var isExists = $(this).text() == changeTxt;
                    if (isExists) {
                        Exists = true;
                    }
                });

                if (Exists) {
                    $.Dialog({
                        'title': 'Attention',
                        'content': 'The Name is exist',
                        'buttons': {
                            'OK': {
                                'action': function () {
                                }
                            }
                        }
                    });
                    return;
                }

                if (changeTxt !== '') {
                    edTmpl = newList.clone();
                    edTmpl.children('a').eq(0).text(changeTxt);
                    if (editOptionId != undefined) {
                        edTmpl.attr("id", editOptionId);
                        edTmpl.children('a').eq(3).attr("href", "ManageCaton.aspx?country=" + editOptionId);
                    }
                    lTgt.replaceWith(edTmpl);
                    var oldValue = $("#newHidden").val();
                    if (isNew) {
                        oldValue += changeTxt + ";";
                        $("#newHidden").val(oldValue);
                        isNew = false;
                    }
                    else if (oldValue.indexOf(editOldValue) != -1) {
                        $("#newHidden").val(oldValue.replace(editOldValue, changeTxt));
                    }
                    else if (editOptionId != undefined) {
                        var editValue = $("#editHidden").val();
                        editValue += editOptionId + "," + changeTxt + ";";
                        $("#editHidden").val(editValue);
                    }
                } else {
                    $.Dialog({
                        'title': 'Attention',
                        'content': 'Please fill the text input!',
                        'buttons': {
                            'OK': {
                                'action': function () {
                                }
                            }
                        }
                    });
                }
            }
        })
    });
</script>
</head>
<body>
<form id="form1" runat="server">
<div class="page-header">
    <div class="page-header-content">
        <h2>Region Management</h2>
    </div>
</div>
<div class="page-region bg-color-white">
    <div class="page-region-content">
        <div class="page-dashboard clearfix">
            <div class="page-mngregion span5 border-color-blue">
                <p class="tlr"><a href="#" class="page-nwr"><i class="icon-plus-2"></i> Add new Region</a></p>
                <ul><li class="page-nwfm">
                        <div class="input-control text span3 ibk">
                            <input type="text" class="with-helper" autocomplete="off" /><button class="helper"></button>
                        </div>
                        <div class="toolbar ibk">
                            <button class="bg-color-red" data-role="undo"><i class="icon-undo fg-color-white"></i></button>
                            <button class="bg-color-greenDark" data-role="save"><i class="icon-checkmark fg-color-white"></i></button>
                        </div>
                    </li>
                    <li class="page-nwlst"><a href="#" class="fg-color-darken"></a>
                     <a href="#" class="fr page-del fg-color-red"><i class="icon-cancel-2"></i></a>
                      <a href="#" class="fr page-edr"><i class="icon-pencil"></i></a> 
                      <a href="#" class="fr page-sub"><i class="icon-list"></i></a></li>
                    <asp:Repeater ID="RepCountry" runat="server">
                    <ItemTemplate>
                    <li id="<%# Eval("CountryId") %>"><a href="#" class="fg-color-darken"><%# Eval("CountryName")%></a> 
                    <a href="#" class="fr page-del fg-color-red"><i class="icon-cancel-2"></i></a> 
                    <a href="#" class="fr page-edr"><i class="icon-pencil"></i></a> 
                    <a href="ManageCaton.aspx?country=<%# Eval("CountryId") %>" class="fr page-sub"><i class="icon-list"></i></a></li>
                    </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <input  type="hidden" name="NewOption" id="newHidden"/>
            <input type="hidden" name="EditOption"  id="editHidden"/>
            <input type="hidden" name="DelOption"  id="delHidden"/>
            <p class="tlr span5"><asp:Button  runat="server" Text="Submit" OnClick="BtnSubmit_Click"/></p>
        </div>
    </div>
</div>
</form>
</body>

</html>