<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="MEDIA.WebUI.MyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    jQuery(document).ready(function($) {
        $('#ProfileForm').validationEngine();
        $('.bg-hv').each(function(){
            var _tile = $(this),
                imgbg = _tile.data().imgbg;

            if ( imgbg ){
                _tile.on('mouseenter', function(){
                    _tile.css({
                        'background-image': 'url(' + imgbg + ')'
                    });
                }).on('mouseleave', function(){
                    _tile.css({
                        'background-image': 'none'
                    });
                })
            }
        });

        var obj = $('option[value="' + <%=model.CountryId %> + '"]');
        $("select")[0].selectedIndex = $('option').index(obj);

        $('.pwremover > i').on('click', function(event){
            event.preventDefault();
            pwtmpl.after(pwbtn).detach();
        });
        var pwtmpl = $('#page-chgpw').detach(),
            pwbtn;
        $('#changepw').on('click', function(event){
            event.preventDefault();
            pwbtn = $(this).parent().before(pwtmpl).detach();
        });

        ActiveFrame();
    });

    function ActiveFrame()
    {
        var frame = <%=frameNum %>;
        $('.page-control ul').children('li').removeClass("active");
        $('.page-control ul').children('li').eq(frame).addClass("active");

        $('.frames').children('div').removeClass("active");
        $('.frames').children('div').eq(frame).addClass("active");
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form id="ProfileForm" runat="server">
 <div class="page secondary with-sidebar">
            <div class="page-header">
                <div class="page-header-content">
                    <h1><asp:Localize ID="Localize7" runat="server" Text="<%$ Resources:Resource, My_Account %>" /><small>&nbsp;</small></h1>
                    <a class="back-button big page-back" href="/"></a>
                </div>
            </div>
            <div class="page-sidebar">
                <div class="page-sidebox">
                    <h2><span class="fg-color-pink">Hi <asp:Label runat="server" ID="lblUserName"></asp:Label>,</span><br><small><asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:Resource, NiceSee %>" /></small></h2>
                </div>
            </div>
            <div class="page-region">
                <div class="page-region-content">
                    <div class="page-control" data-role="page-control">
                    <ul>
                        <li class="active"><a href="#frame1"><asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:Resource, MyProc %>" /></a></li>
                        <li><a href="#frame2"><asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:Resource, MyDonatedProc %>" /></a></li>
                        <li><a href="#frame3"><asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:Resource, Edit_Account %>" /></a></li>
                    </ul>
                    <div class="frames">
                        <div class="frame active" id="frame1">
                            <div class="clearfix">
                            <asp:Repeater ID="RepFoundedPro" runat="server">
                            <ItemTemplate>
                              <%# IsHref(Eval("IsCheck"), Eval("ProjectId"))%>
                                <div class="<%# GetDivCssClass(MEDIA.WebUI.SysConfigConst.MyProfileClass,4) %>" data-imgbg="<%# GetProjectImage(Eval("ImgUrl")).Substring(1)  %>">
                                    <div class="tile-content">
                                        <h4><%# Eval("ProjectName")%></h4>
                                        <br>
                                        <p><%# Eval("ProjectSummary")%></p>
                                    </div>
                                    <div class="brand">
                                        <%# IsShowUnCheck()%>
                                    </div>
                                </div>
                                </a>
                            </ItemTemplate>
                            </asp:Repeater>
                            </div>
                        </div>
                        <div class="frame" id="frame2">
                            <div class="clearfix">
                            <asp:Repeater ID="RepDonatedPro" runat="server">
                            <ItemTemplate>
                                <a href="ProjectDetails.aspx?pid=<%# Eval("ProjectId") %>">
                                <div class="<%# GetDivCssClass(MEDIA.WebUI.SysConfigConst.MyProfileClass,4) %>" data-imgbg="<%# GetProjectImage(Eval("ImgUrl")).Substring(1)  %>">
                                    <div class="tile-content">
                                        <h4><%# Eval("ProjectName")%></h4>
                                        <br>
                                        <p><%# Eval("ProjectSummary")%></p>
                                    </div>
                                    <div class="brand">
                                        <span class="name">Sum(<%# Eval("CurrencyStr")%>)</span>
                                        <span class="badge"><%# base.GetCurrency((decimal)Eval("SumDonateFunding"))%></span>
                                    </div>
                                </div>
                                </a>
                            </ItemTemplate>
                            </asp:Repeater>
                            </div>
                        </div>
                        <div class="frame" id="frame3">
                         <div class="grid">
                                <div class="row">
                            <fieldset>
                                <legend class="fg-color-blueDark"><asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:Resource, Genernal_info %>" /></legend>
                                <p class="fg-color-greenDark"><asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:Resource, First_name %>" /></p>
                                <div class="input-control text span6 as-block">
                                    <input name="txtFirstName" value="<%= model.FirstName %>" type="text" autocomplete="off" placeholder="Enter your first name." class="validate[required]" />
                                    <button class="helper"></button>
                                </div>
                                <p class="fg-color-greenDark"><asp:Localize ID="Localize8" runat="server" Text="<%$ Resources:Resource, Last_name %>" /></p>
                                <div class="input-control text span6 as-block">
                                    <input  name="txtLastName" value="<%= model.LastName %>" type="text" autocomplete="off" placeholder="Enter your last name." class="validate[required]" />
                                    <button class="helper"></button>
                                </div>
                                <p class="fg-color-greenDark"><asp:Localize ID="Localize9" runat="server" Text="<%$ Resources:Resource, Address %>" /></p>
                                <div class="input-control text span6 as-block">
                                    <input name="txtAddress" value="<%= model.Address %>" type="text" autocomplete="off" placeholder="Enter your address." class="validate[required]" />
                                    <button class="helper"></button>
                                </div>

                                <div class="span8 as-block">
                                        <div class="span3">
                                            <p class="fg-color-greenDark"><asp:Localize ID="Localize10" runat="server" Text="<%$ Resources:Resource, ZIP %>" /><span class="fg-color-red">*</span></p>
                                            <div class="input-control text span3 as-block">
                                                <input name="ZIP" value="<%= model.Zip %>" type="text" autocomplete="off" placeholder="Enter ZIP" class="validate[required]" />
                                                <button class="helper"></button>
                                            </div>
                                        </div>
                                        <div class="span3">
                                            <p class="fg-color-greenDark"><asp:Localize ID="Localize11" runat="server" Text="<%$ Resources:Resource, Town %>" /><span class="fg-color-red">*</span></p>
                                            <div class="input-control text span3 as-block">
                                                <input name="Town" value="<%= model.Town %>" type="text" autocomplete="off" placeholder="Enter Town" class="validate[required]" />
                                                <button class="helper"></button>
                                            </div>
                                        </div>
                                    </div>

                                <p class="fg-color-greenDark"><asp:Localize ID="Localize12" runat="server" Text="<%$ Resources:Resource, Country %>" /> <span class="fg-color-red">*</span></p>
                                <div class="input-control select span6 as-block">
                                    <asp:DropDownList runat="server" ID="ddlCountry" CssClass="validate[required]"></asp:DropDownList>
                                </div>
                                  <p class="fg-color-greenDark"><asp:Localize ID="Localize13" runat="server" Text="<%$ Resources:Resource, Birthday %>" /> (Format: DD-MM-YYYY) <span class="fg-color-red">*</span></p>
                                        <div class="as-block span8 page-reg-birthday">
                                            <div class="input-control text span1">
                                                <input name="day" value="<%= model.Birthday.Value.Day %>" type="text" maxlength="2" class="validate[required],custom[onlyNumber],min[1],max[31]]" autocomplete="off" placeholder="Enter date" />
                                                <button class="helper"></button>
                                            </div>
                                            <span class="fl">-</span>
                                            <div class="input-control text span1">
                                                <input name="month" value="<%= model.Birthday.Value.Month %>" type="text" maxlength="2" class="validate[required],custom[onlyNumber],min[1],max[12]" autocomplete="off" placeholder="Enter month" />
                                                <button class="helper"></button>
                                            </div>
                                            <span class="fl">-</span>
                                            <div class="input-control text span2">
                                                <input name="year" value="<%= model.Birthday.Value.Year %>" type="text" maxlength="4" class="validate[required],custom[onlyNumber],min[1900]" autocomplete="off" placeholder="Enter year" />
                                                <button class="helper"></button>
                                            </div>
                                        </div>

                            </fieldset>                          
                                 <fieldset id="page-chgpw">
                                            <legend class="fg-color-blueDark"><asp:Localize ID="Localize14" runat="server" Text="<%$ Resources:Resource, Change_pwd %>" /></legend>
                                            <p class="fg-color-greenDark tlr"><span class="fl"><asp:Localize ID="Localize15" runat="server" Text="<%$ Resources:Resource, Old_pwd %>" /></span><a href="#" class="pwremover fg-color-red" title="Cancel password editing"><i class="icon-cancel-2"></i></a></p>
                                            <div class="input-control password span4 as-block">
                                                <input type="password" name="oldPwd" id="oldPwd" autocomplete="off" placeholder="Enter old password." class="validate[required]" />
                                                <button class="helper"></button>
                                            </div>
                                            <p class="fg-color-greenDark">New Password</p>
                                            <div class="input-control password span4 as-block">
                                                <input type="password" name="newPwd" autocomplete="off" placeholder="Enter password." id="password" class="validate[required]" />
                                                <button class="helper"></button>
                                            </div>
                                            <p class="fg-color-greenDark"><asp:Localize ID="Localize17" runat="server" Text="<%$ Resources:Resource, RepPassword %>" /></p>
                                            <div class="input-control password span4 as-block">
                                                <input type="password" autocomplete="off" placeholder="Enter password again." class="validate[required,equals[password]]" />
                                                <button class="helper"></button>
                                            </div>
                                 </fieldset>
                                 <p class="page-prf-pwc"><a href="#" id="changepw"><asp:Localize ID="Localize16" runat="server" Text="<%$ Resources:Resource, Change_pwd %>" /></a></p><br>
                                 <p class="tlc"><asp:Button runat="server" CssClass="bg-color-green5" Text="Save Changes" ID="btnSaveChange" OnClick="btnSaveChange_Click" /></p>
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
