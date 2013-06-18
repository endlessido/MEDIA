<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" Async="true" CodeBehind="UserRegist.aspx.cs" Inherits="MEDIA.WebUI.UserRegist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="js/jquery.pwstrength.js"></script>
<script type="text/javascript" src="js/moment.min.js"></script>
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        $('#password').pwstrength();
        $('#formRegist').validationEngine('attach', {
            onValidationComplete: function (form, status) {
                if (status == false)
                    return false;
                var month = parseInt($('#birthmonth').val(), 10),
                    year = parseInt($('#birthyear').val(), 10),
                    date = parseInt($('#birthdate').val(), 10);
                if (month !== "" && date !== "" && year !== "") {
                    switch (month) {
                        case 4:
                        case 6:
                        case 9:
                        case 11: if (date > 30) {
                                $('#birthdate').validationEngine('showPrompt', 'Lesser Months has 30 days!', 'error', "topRight", true);
                                return false;
                            } else {
                                return true;
                            };
                            break;
                        case 2: if (date > 28) {
                                if (date === 29 && new Date(year, 1, 29).getMonth() === 1) {
                                    if (status == true) {
                                        showWaitDialog();
                                    }
                                    return true;
                                } else if (date > 29 && new Date(year, 1, 29).getMonth() === 1) {
                                    $('#birthdate').validationEngine('showPrompt', 'February in Leap Year has 29 days!', 'error', "topRight", true);
                                    return false;
                                } else {
                                    $('#birthdate').validationEngine('showPrompt', 'February has 28 days!', 'error', "topRight", true);
                                    return false;
                                }
                            } else {
                                if (status == true) {
                                    showWaitDialog();
                                }
                                return true;
                            }
                            break;
                        default:
                            if (status == true) {
                                showWaitDialog();
                            }
                            return status;
                            break;
                    }
                }
            }
        });
    });
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="formRegist" runat="server">
    <div class="page secondary">
            <div class="page-header">
                <div class="page-header-content">
                    <h1><asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:Resource, NewAccount %>" /><small>&nbsp;</small></h1>
                    <a class="back-button big page-back" href="/"></a>
                </div>
            </div>
             
            <div class="page-region">
                <div class="page-region-content">
                    <div class="grid">
                        <div class="row">
                            <div class="span10">
                                <fieldset class="border-color-red">
                                    <legend class="fg-color-darken"><span class="fg-color-red">*</span> <asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:Resource, Required %>" /></legend>
                                    <div class="row">
                                        <div class="span4">
                                            <p class="fg-color-greenDark"><asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:Resource, Email %>" /> <span class="fg-color-red">*</span></p>
                                            <div class="input-control text span8">
                                                <asp:TextBox runat="server" id="txtEmail" tabindex="1" type="email" autocomplete="off" placeholder="Enter your Email address." CssClass ="validate[required,custom[email],ajax[ajaxUserCall]]" />
                                                <button class="helper"></button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="span4">
                                            <p class="fg-color-greenDark"><asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:Resource, Password %>" />  <span class="fg-color-red">*</span></p>
                                            <div class="input-control password span4">
                                                <input name="txtPwd" type="password"  tabindex="2" autocomplete="off" placeholder="Enter password." id="password" class="validate[required]" data-indicator="pwindicator"/>
                                                <button class="helper"></button>
                                            </div>
                                             <div class="page-pwstrenth">
                                                <span class="ibk">Password Strenth:</span>
                                                <div id="pwindicator">
                                                    <div class="bar"></div>
                                                    <div class="label"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <p class="fg-color-greenDark"><asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:Resource, RepPassword %>" /> <span class="fg-color-red">*</span></p>
                                            <div class="input-control password span4">
                                                <input type="password" autocomplete="off" tabindex="3" placeholder="Enter password again." class="validate[required,equals[password]]" />
                                                <button class="helper"></button>
                                            </div>
                                        </div>
                                    </div>
                                    <p class="fg-color-greenDark"><asp:Localize ID="Localize7" runat="server" Text="<%$ Resources:Resource, Gender %>" /> <span class="fg-color-red">*</span></p>
                                    <label class="input-control radio">
                                        <input type="radio" name="gender" tabindex="4" checked="checked" value="0" autocomplete="off" class="validate[required] radio">
                                        <span class="helper"><asp:Localize ID="Localize8" runat="server" Text="<%$ Resources:Resource, Male %>" /> </span>
                                    </label>
                                    <label class="input-control radio">
                                        <input type="radio" name="gender" tabindex="5" value="1" autocomplete="off" class="validate[required] radio">
                                        <span class="helper"><asp:Localize ID="Localize9" runat="server" Text="<%$ Resources:Resource, Female %>" /> </span>
                                    </label>
                                    <p class="fg-color-greenDark"><asp:Localize ID="Localize10" runat="server" Text="<%$ Resources:Resource, Last_name %>" /> <span class="fg-color-red">*</span></p>
                                    <div class="input-control text span6 as-block">
                                        <asp:TextBox runat="server" id="txtLastName"  TabIndex="6" TextMode="SingleLine" autocomplete="off" placeholder="Enter your last name." CssClass="validate[required]" />
                                        <button class="helper"></button>
                                    </div>
                                    <p class="fg-color-greenDark"><asp:Localize ID="Localize11" runat="server" Text="<%$ Resources:Resource, First_name %>" /><span class="fg-color-red">*</span></p>
                                    <div class="input-control text span6 as-block">
                                         <asp:TextBox runat="server" id="txtFirstName" TabIndex="7" TextMode="SingleLine" autocomplete="off" placeholder="Enter your first name." CssClass="validate[required]" />
                                        <button class="helper"></button>
                                    </div>
                                    <p class="fg-color-greenDark"><asp:Localize ID="Localize12" runat="server" Text="<%$ Resources:Resource, Address %>" /> <span class="fg-color-red">*</span></p>
                                    <div class="input-control text span6 as-block">
                                        <asp:TextBox runat="server" id="txtAddress" TabIndex="8" TextMode="SingleLine" autocomplete="off" placeholder="Enter your address." CssClass="validate[required]" />
                                        <button class="helper"></button>
                                    </div>
                                   <div class="span8 as-block">
                                        <div class="span3">
                                            <p class="fg-color-greenDark"><asp:Localize ID="Localize13" runat="server" Text="<%$ Resources:Resource, ZIP %>" /><span class="fg-color-red">*</span></p>
                                            <div class="input-control text span3 as-block">
                                               <asp:TextBox runat="server" id="txtZIP"  TabIndex="9" TextMode="SingleLine" autocomplete="off" placeholder="Enter ZIP" CssClass="validate[required]" />
                                                <button class="helper"></button>
                                            </div>
                                        </div>
                                        <div class="span3">
                                            <p class="fg-color-greenDark"><asp:Localize ID="Localize14" runat="server" Text="<%$ Resources:Resource, Town %>" /><span class="fg-color-red">*</span></p>
                                            <div class="input-control text span3 as-block">
                                                <asp:TextBox runat="server" id="txtTown" TabIndex="10" TextMode="SingleLine" autocomplete="off" placeholder="Enter Town" CssClass="validate[required]" />
                                                <button class="helper"></button>
                                            </div>
                                        </div>
                                    </div>
                                    <p class="fg-color-greenDark"><asp:Localize ID="Localize15" runat="server" Text="<%$ Resources:Resource, Country %>" /> <span class="fg-color-red">*</span></p>

                                      <div class="span8 as-block">
                                        <div class="input-control select span6 as-block">
                                            <asp:DropDownList runat="server" ID="ddlCountry" TabIndex="11" CssClass="validate[required]"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <p class="fg-color-greenDark"><asp:Localize ID="Localize16" runat="server" Text="<%$ Resources:Resource, Birthday %>" /> (Format: DD-MM-YYYY) <span class="fg-color-red">*</span></p>
                                                                          <div class="as-block span8 page-reg-birthday">
                                        <div class="input-control text span2">
                                            <input type="text" name="txtDay" maxlength="2" tabindex="12" class="validate[required,custom[integer],min[1],max[31]]" autocomplete="off" id="birthdate" placeholder="Enter date" />
                                            <button class="helper"></button>
                                        </div>
                                        <span class="fl">-</span>
                                        <div class="input-control text span2">
                                            <input type="text" name="txtMonth" maxlength="2" tabindex="13" class="validate[required,custom[integer],min[1],max[12]]" autocomplete="off" id="birthmonth" placeholder="Enter month" />
                                            <button class="helper"></button>
                                        </div>
                                        <span class="fl">-</span>
                                        <div class="input-control text span3">
                                            <input type="text" name="txtYear" maxlength="4" tabindex="14" class="validate[required,custom[integer],custom[age],min[1900],minSize[4]]" autocomplete="off" id="birthyear" placeholder="Enter year" />
                                            <button class="helper"></button>
                                        </div>
                                    </div>

                                     <div class="input-term">
                                        <label onclick="" class="input-control checkbox">
                                            <input type="checkbox" autocomplete="off" value="1" name="tac" class="validate[required]">
                                            <span class="helper">I am over 18 years</span>
                                        </label>
                                    </div>

                                     <div class="input-term">
                                        <label onclick="" class="input-control checkbox">
                                            <input type="checkbox" autocomplete="off"  tabindex="15" value="1" name="tac" class="validate[required]">
                                            <span class="helper">I've read and agree to XYZ's</span>
                                        </label><a href="Terms.aspx"  id="page-term">terms &amp; conditions</a>
                                    </div>
                                    <p><asp:Localize ID="Localize17" runat="server" Text="<%$ Resources:Resource, GetEmail %>" /></p>
                                </fieldset>
                            </div>
                        </div>
                        <div class="row">
                            <div class="span10">
                                <p class="tlc"> <asp:Button ID="btnSubmit" CssClass="bg-color-green5" OnClick="btnSubmit_Click" Text="<%$ Resources:Resource, Submit %>" runat="server" /></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</form>
</asp:Content>
