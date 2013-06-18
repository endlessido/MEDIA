<%@ Page Title=""  Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="CreateProject.aspx.cs" Inherits="MEDIA.WebUI.CreateProject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="css/jslider.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="js/jquery.maxlength.js"></script>
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        $('.max-limit').maxlength({
            max: 140
        });
        var gdTmpl = $('.goodie-form').clone();
        $('.formslideout').on('click jqv.field.result', function (event, field, errorFound, prompText) {
            var sTgt = event.target,
                dTgt = event.delegateTarget,
                pHead = $('.page-header-content>h1'),
                gdCurr, gdForm, gdQuant, gdQuantH;
            if (sTgt.id === 'formnext') {
                $(dTgt).animate({
                    'margin-left': '-830px'
                }, function () {
                    $('html,body').animate({ scrollTop: 0 });
                })
                pHead.empty().append('Goodies and Video<small>&nbsp;</small>');
            }

            if (sTgt.id === 'formprev') {
                $(dTgt).animate({
                    'margin-left': '0'
                }, function () {
                    $('html,body').animate({ scrollTop: 0 });
                })
                pHead.empty().append('Questionnaire<small>&nbsp;</small>');
            }

            if (sTgt.id === 'goodie-add') {
                event.preventDefault();
                gdCurr = $('.goodie-form:last');
                gdCurr.after(gdTmpl.clone());
            }

            if (sTgt.type === 'checkbox' && sTgt.className === 'page-gdinfi') {
                gdQuant = $(sTgt).parents('.page-gditrig').siblings('.page-gdnum').find('input[type="text"]');
                gdQuantH = gdQuant.siblings('input[type="hidden"]');
                if (sTgt.checked) {
                    gdQuant.validationEngine('hide').val('Unlimited').attr('disabled', '').removeClass().blur();
                    gdQuantH.val('Unlimited');
                } else {
                    gdQuant.removeAttr('disabled').val('').addClass('validate[required,custom[onlyNumber]]').blur();
                    gdQuantH.val('');
                }
            }

            if ($(sTgt).parent().hasClass('gdremover') && sTgt.tagName.toLowerCase() === 'i') {
                event.preventDefault();
                gdForm = $('.goodie-form');
                if (gdForm.length > 1) {
                    $(sTgt).parents('.goodie-form').remove();
                } else {
                    $.Dialog({
                        'title': 'Attention',
                        'content': 'Please add at least 1 goodie!',
                        'closeButton': true,
                        'buttons': {
                            'OK': {
                                'action': function () { }
                            }
                        }
                    });
                }
            }

        }).on('blur', '.page-gdnum input[type="text"]', function (event) {
            var sTgt = event.target,
                gdQuantH = $(this).siblings('input[type="hidden"]');
            gdQuantH.val($(this).val());

        });

        $('#page-upfile>input[type="file"]').on('change', function () {
            var path = $(this).val();
            var lastIndex = path.lastIndexOf(".");
            var filefix = path.substring(lastIndex, path.length);
            $(this).siblings('.input-control').children('input[type="text"]').val(path);
        });

        $('#ContentPlaceHolder1_ddlCountry').on('change', function () {
            $.ajax({
                type: "get",
                dataType: "json",
                url: "ajax.aspx",
                data: "fieldId=ddlCountry&fieldValue=" + $(this).val(),
                success: function (msg) {
                    $('#ddlArea').empty();
                    $("<option value=''><%= Resources.Resource.Select_Canton %></option>").appendTo($('#ddlArea'));
                    for (var i = 0; i < msg.length; i++) {
                        $("<option value='" + msg[i].Id + "," + msg[i].Name + "'>" + msg[i].Name + "</option>").appendTo($('#ddlArea'));
                    }
                }
            });
        });

        $('#CreateProForm1').validationEngine('attach', {
            scroll: false,
            onValidationComplete: function (form, status) {
                var errField, errTop, errHide;
                if (!status) {
                    errField = $(this.InvalidFields[0]);
                    errTop = errField.offset().top;
                    errHide = errField.parents('.formslidein').is('#formslidel');
                    if (errHide) {
                        $('.formslideout').animate({
                            'margin-left': '0'
                        }, function () {
                            $('html, body').animate({ scrollTop: errTop - 36 });
                        });
                    } else {
                        $('html, body').animate({ scrollTop: errTop - 36 });
                    }
                } else {
                    showWaitDialog();
                    return true;
                }
            }
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form runat="server" id="CreateProForm1">
    <div class="page secondary">
            <div class="page-header">
                <div class="page-header-content">
                    <h1><asp:Localize ID="Localize7" runat="server" Text="<%$ Resources:Resource, Questionnaire %>" /><small>&nbsp;</small></h1>
                    <a class="back-button big page-back" href="Index.aspx"></a>
                </div>
            </div>
             
            <div class="page-region">
                <div class="page-region-content">
                    <h3><asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:Resource, FoundProject %>" /></h3>
                          <div class="formslide">
                        <div class="formslideout clearfix">
                            <div class="formslidein fl" id="formslidel">
                                <div class="grid">
                                    <div class="row">
                                        <div class="span10">
                                            <fieldset class="border-color-orangeDark">
                                                <legend class="fg-color-darken"><asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:Resource, FieldsWith %>" /><span class="fg-color-red">*</span><asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:Resource, Required %>" /></legend>
                                                <p class="fg-color-greenDark"><asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:Resource, ProjectCategory %>" /><span class="fg-color-red">*</span></p>
                                                <div class="row">
                                                    <div class="span4">
                                                        <div class="input-control select span4">
                                                             <asp:DropDownList runat="server" ID="ddlCategory" TabIndex="1" CssClass="validate[required]"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <p class="fg-color-greenDark"><asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:Resource, Are_you_from %>" /><span class="fg-color-red">*</span></p>
                                                <div class="row">
                                                    <div class="span4">
                                                        <div class="input-control select span4">
                                                           <asp:DropDownList runat="server" ID="ddlCountry" TabIndex="2" CssClass="validate[required]"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="span4">
                                                        <div class="input-control select span4">
                                                            <select class="validate[required]" tabindex="3" id="ddlArea" name="canton">
                                                            <option value=''><asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:Resource, Canton %>" /></option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <p class="fg-color-greenDark"><asp:Localize ID="Localize8" runat="server" Text="<%$ Resources:Resource, Name_Of_Project %>" /><span class="fg-color-red">*</span></p>
                                                <div class="input-control text">
                                                    <input name="txtProductName" id="txtProductName" tabindex="4" type="text" class="validate[required,ajax[ajaxUserCall]]" data-prompt-position="topRight:-260,0" autocomplete="off" />
                                                    <button class="helper"></button>
                                                </div>
                                                
                                                <p class="fg-color-greenDark"><asp:Localize ID="Localize10" runat="server" Text="<%$ Resources:Resource, Have_You_Read %>" /> 
                                                <a href="Terms.aspx" target="_blank"><asp:Localize ID="Localize29" runat="server" Text="<%$ Resources:Resource, Terms_Of_Service %>" /></a> <asp:Localize ID="Localize30" runat="server" Text="<%$ Resources:Resource, And_Agree %>" /><span class="fg-color-red">*</span></p>
                                                <label class="input-control radio" class="validate[required]">
                                                    <input type="radio" checked="checked" tabindex="6" name="agreement" class="validate[required]" autocomplete="off">
                                                    <span class="helper"><asp:Localize ID="Localize11" runat="server" Text="<%$ Resources:Resource, Yes %>" /></span>
                                                </label>
                                                <label class="input-control radio" class="validate[required]">
                                                    <input type="radio" name="agreement" tabindex="6" class="validate[required]" autocomplete="off">
                                                    <span class="helper"><asp:Localize ID="Localize12" runat="server" Text="<%$ Resources:Resource, No %>" /></span>
                                                </label>
                                                <p class="fg-color-greenDark"><asp:Localize ID="Localize13" runat="server" Text="<%$ Resources:Resource, Char_Max %>" /><span class="fg-color-red">*</span></p>
                                                <div class="input-control textarea">
                                                    <textarea runat="server" id="txtProductSummary" tabindex="7" autocomplete="off" data-prompt-position="topRight:-68,0" class="validate[required,maxSize[140]] max-limit"></textarea>
                                                </div>

                                                <p class="fg-color-greenDark"><asp:Localize ID="Localize14" runat="server" Text="<%$ Resources:Resource, Introduce %>" /> <span class="fg-color-red">*</span></p>
                                                <div class="input-control textarea">
                                                    <textarea runat="server" id="txtSelfIntroduce" tabindex="8" autocomplete="off" data-prompt-position="topRight:-68,0" class="validate[required]"></textarea>
                                                </div>

                                                <p class="fg-color-greenDark"><asp:Localize ID="Localize15" runat="server" Text="<%$ Resources:Resource, Need_Reason %>" /><span class="fg-color-red">*</span></p>
                                                <div class="input-control textarea">
                                                    <textarea runat="server" id="txtNeedReason" tabindex="9" autocomplete="off" data-prompt-position="topRight:-68,0" class="validate[required]"></textarea>
                                                </div>

                                              

                                                <p class="fg-color-greenDark"><asp:Localize ID="Localize17" runat="server" Text="<%$ Resources:Resource, How_Long %>" /><span class="fg-color-red">*</span></p>
                                                <label class="input-control checkbox">
                                                    <input type="checkbox" tabindex="11" class="validate[required,maxCheckbox[1]]" autocomplete="off" value="33" name="projtime">
                                                    <span class="helper"><asp:Localize ID="Localize18" runat="server" Text="<%$ Resources:Resource, Days_33 %>" /></span>
                                                </label>
                                                <label class="input-control checkbox">
                                                    <input type="checkbox" tabindex="11" class="validate[required,maxCheckbox[1]]" autocomplete="off" value="77" name="projtime">
                                                    <span class="helper"><asp:Localize ID="Localize19" runat="server" Text="<%$ Resources:Resource, Days_77 %>" /></span>
                                                </label>
                                                <label class="input-control checkbox">
                                                    <input type="checkbox" tabindex="11" class="validate[required,maxCheckbox[1]]" autocomplete="off" value="99" name="projtime">
                                                    <span class="helper"><asp:Localize ID="Localize20" runat="server" Text="<%$ Resources:Resource, Days_99 %>" /></span>
                                                </label>
                                                <label class="input-control checkbox">
                                                    <input type="checkbox" tabindex="11" class="validate[required,maxCheckbox[1]]" autocomplete="off" value="111" name="projtime">
                                                    <span class="helper"><asp:Localize ID="Localize21" runat="server" Text="<%$ Resources:Resource, Days_111 %>" /></span>
                                                </label>
                                                <label class="input-control checkbox">
                                                    <input type="checkbox" tabindex="11" class="validate[required,maxCheckbox[1]]" autocomplete="off" value="333" name="projtime">
                                                    <span class="helper"><asp:Localize ID="Localize22" runat="server" Text="<%$ Resources:Resource, Days_333 %>" /></span>
                                                </label>
                                                
                                              <div class="span8 as-block">
                                                    <p class="fg-color-greenDark"><asp:Localize ID="Localize23" runat="server" Text="<%$ Resources:Resource, How_Much %>" /><span class="fg-color-red">*</span></p>
                                                    <div class="span3">
                                                        <div class="input-control text span3">
                                                            <input name="totalfunding" tabindex="12" type="text" autocomplete="off" class="validate[required,custom[onlyNumber]]" />
                                                            <button class="helper"></button>
                                                        </div>
                                                    </div>
                                                    <div class="span1">
                                                        <div class="input-control  select span1">
                                                            <select name="fundingcurrency">
                                                                <option>CHF</option>
                                                                <option>USD</option>
                                                                <option>EUR</option>
                                                                <option>GBP</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <p class="fg-color-greenDark"><asp:Localize ID="Localize24" runat="server" Text="<%$ Resources:Resource, Upload_Image %>" /></p>
                                                <div class="as-block" id="page-upfile">
                                                    <div class="input-control text span5">
                                                        <input id="projectImg" type="text" tabindex="13" autocomplete="off"  placeholder="Select a image, JPG/GIF/PNG allowed" />
                                                    </div>
                                                    <input type="button" value="Browse" tabindex="13" id="fake-file">
                                                    <asp:FileUpload ID="uploadFile" tabindex="13" runat="server" size="54" CssClass="validate[required,custom[imageType]]" />
                                                </div>
                                                        
                                                <p class="fg-color-greenDark"><asp:Localize ID="Localize25" runat="server" Text="<%$ Resources:Resource, WebPage %>" /></p>
                                                <div class="input-control text">
                                                    <input name="homepage" tabindex="14" type="text" autocomplete="off" class="validate[custom[url]]" placeholder="Enter you webpage link." />
                                                    <button class="helper"></button>
                                                </div>
                                               
                                                <div class="seperator">
                                                    <label class="input-control checkbox">
                                                        <input runat="server" tabindex="16" id="chkIsSend" type="checkbox" checked="checked">
                                                        <span class="helper"><asp:Localize ID="Localize27" runat="server" Text="<%$ Resources:Resource, NewsLetter %>" /></span>
                                                    </label>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="span10">
                                            <p class="tlc"><input id="formnext" tabindex="17" class="bg-color-green5 fg-color-white" type="button" value="Next &gt;&gt;" /></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="formslidein fl" id="formslider">
                                <div class="grid">
                                    <div class="row">
                                        <div class="span10">
                                            <fieldset class="border-color-blueDark">
                                                <legend class="fg-color-darken"><asp:Localize ID="Localize28" runat="server" Text="<%$ Resources:Resource, YoutubeLink %>" /><span class="fg-color-red">*</span></legend>
                                                <div class="input-control input-vdlk text">
                                                    <input runat="server"   id="txtYoutubeURL" type="text" placeholder="Enter the link to your youtube video, start with http://" class="validate[required,custom[youtube]] text-input" data-prompt-position="bottomRight:-40,0" />
                                                    <button class="helper"></button>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="row goodie-form">
                                        <div class="span10">
                                            <fieldset class="border-color-greenLight">
                                                <legend class="fg-color-darken"><asp:Localize ID="Localize31" runat="server" Text="<%$ Resources:Resource, Add_goodie %>" /></legend>
                                                 <p class="fg-color-greenDark tlr"><span class="fl"><asp:Localize ID="Localize32" runat="server" Text="<%$ Resources:Resource, Name_goodie %>" /><i class="fg-color-red">*</i></span><a href="#" class="gdremover fg-color-red" title="Remove this goodie"><i class="icon-cancel-2"></i></a></p>
                                                <div class="input-control text">
                                                    <input name="goodyname"  type="text" placeholder="Enter the name of your goodie" class="validate[required] text-input" data-prompt-position="topRight:-40,0" autocomplete="off" />
                                                    <button class="helper"></button>
                                                </div>
                                                <p class="fg-color-greenDark"><asp:Localize ID="Localize33" runat="server" Text="<%$ Resources:Resource, Desc_goodie %>" /><span class="fg-color-red">*</span></p>
                                                <div class="input-control textarea">
                                                    <textarea name="goodydesc" autocomplete="off"  class="validate[required]" data-prompt-position="topRight:-67,0"></textarea>
                                                </div>
                                               <div class="span8">
                                                    <p class="fg-color-greenDark"><asp:Localize ID="Localize34" runat="server" Text="<%$ Resources:Resource, Number_goodie %>" /><span class="fg-color-red">*</span></p>
                                                    <div class="span3 page-gdnum">
                                                        <div class="input-control text span3">
                                                            <input  type="text" autocomplete="off" class="validate[required,custom[onlyNumber]]" />
                                                             <input name="goodynum" type="hidden" value="">
                                                            <button class="helper"></button>
                                                        </div>
                                                    </div>
                                                     <div class="span3 page-gditrig">
                                                        <label class="input-control checkbox">
                                                            <input  type="checkbox" value="limited"  autocomplete="off" class="page-gdinfi">
                                                            <input name="unlimited" type="hidden" value="limited">
                                                            <span class="helper"><asp:Localize ID="Localize36" runat="server" Text="<%$ Resources:Resource, Unlimited %>" /></span>
                                                        </label>
                                                    </div>
                                                </div>

                                                <div class="span8">
                                                    <p class="fg-color-greenDark"><asp:Localize ID="Localize37" runat="server" Text="<%$ Resources:Resource, How_much_donate %>" /><span class="fg-color-red">*</span></p>
                                                    <div class="span3">
                                                        <div class="input-control text span3">
                                                            <input name="goodyprice" type="text" autocomplete="off" class="validate[required,custom[onlyNumber]]" />
                                                            <button class="helper"></button>
                                                        </div>
                                                    </div>
                                                  <%--  <div class="span1">
                                                        <div class="input-control  select span1">
                                                            <select name="ddlCurrency">
                                                                <option>CHF</option>
                                                                <option>USD</option>
                                                                <option>EUR</option>
                                                                <option>GBP</option>
                                                            </select>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                   <div class="row">
                                        <div class="span10">
                                            <p class="goodie-more"><a href="#" class="button default bg-color-green5 fg-color-white" id="goodie-add"><asp:Localize ID="Localize35" runat="server" Text="<%$ Resources:Resource, Add_new_goodie %>" /></a></p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="span10">
                                            <p class="tlc"><input id="formprev" class="bg-color-blueDark fg-color-white" type="button"  value="&lt;&lt; Previous" /> <asp:Button ID="btnSubmit"  CssClass="bg-color-green5 fg-color-white" runat="server" OnClick="btnSubmit_Click" Text="<%$ Resources:Resource, Submit %>" /> </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</form>
</asp:Content>
