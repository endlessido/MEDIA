<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  MasterPageFile="~/Index.Master" CodeBehind="ProjectList.aspx.cs" Inherits="MEDIA.WebUI.ProjectListAjax" %>
<%@ Register Assembly="MEDIA.WebUI" Namespace="MEDIA.WebUI" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">

    var terms = {
        region: [],
        sport: []
    };
    jQuery(document).ready(function ($) {
        var menus = $('.multidropdown'),
            condition = $('.page-condbox').detach(),
            condclone;

        for (var i = 0; i < menus.length; i++) {
            menus.eq(i).on('click', { category: menus.eq(i).data('category') }, ClickHandler);
        }

        function ClickHandler(event) {
            event.preventDefault();
            if (event.data.category) {
                var cdtn = event.target.tagName.toLowerCase() === 'a' ? $(event.target) : $(event.target).parent('a'),
                    pos = $.inArray(cdtn.text(), terms[event.data.category]);
                ihook = $('<i>', {
                    'class': 'icon-checkmark fg-color-greenLight'
                })
                if (pos === -1) {
                    terms[event.data.category].push(cdtn.text());
                    cdtn.append(ihook.attr('rel', cdtn.text()));
                } else {
                    terms[event.data.category].splice(pos, 1);
                    cdtn.children('i').remove();
                }
                __doPostBack("ctl00$ContentPlaceHolder1$LinkButton2", terms["region"] + ";" + terms["sport"]);
            } else {
                return;
            }
        }

        document.onkeypress = function (event) {
            if (event.keyCode == 13) {
                document.getElementById("ContentPlaceHolder1_btnSearch").click();
                return;
            }
        }
    });

    function CancelHandler(e) {
        var relcntn = $(e.target).parents('.page-condbox'),
                                relcond = relcntn.attr('rel'),
                                relcata = relcntn.data('category');
        relpos = $.inArray(relcond, terms[relcata])
        $(this).parent('.page-condbox').remove();
        if (relpos !== -1) {
            terms[relcata].splice(relpos, 1);
        }
        $('.multidropdown').find('i[rel="' + relcond + '"]').remove();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form runat="server" id="formList">
<asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
    <div class="page secondary with-sidebar">
        <div class="page-header">
            <div class="page-header-content">
                <h1><asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:Resource, Search_Top_Title %>" /></h1>
                <a class="back-button big page-back" href="Index.aspx"></a>
            </div>
        </div>
        <div class="page-sidebar">
            <ul>
                <li data-role="dropdown" class="sticker sticker-color-pink dropdown active">
                    <a><i class="icon-list"></i> <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:Resource, Search_Country_Title %>" /> </a>
                    <ul class="sub-menu light sidebar-dropdown-menu multidropdown open">
                        <asp:Repeater runat="server" ID="RepCountry">
                        <ItemTemplate>
                            <%# BindCountryData(Eval("Areas"))%>
                        </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </li>
                <li data-role="dropdown" class="sticker sticker-color-red dropdown active">
                    <a><i class="icon-list"></i><asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:Resource, BySport %>" /></a>
                    <ul class="sub-menu light multidropdown sidebar-dropdown-menu open" data-category="sport">
                    <asp:Repeater runat="server" ID="RepCategory" >
                        <ItemTemplate>
                            <li><a href="#"><%# Eval("CategoryName")%></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                    </ul>
                </li>
            </ul>
        </div>
<asp:UpdatePanel runat="server" ID="UpdatePanel1">
<ContentTemplate>
        <div class="page-region">
            <div class="page-region-content">
                <div class="grid">
                    <div class="row">
                        <div class="page-search">
                            <div class="input-control text span3 fl">
                                <asp:TextBox ID="txtSearch" runat="server" TextMode="SingleLine" placeholder="Search by keywords" />
                                <button class="helper"></button>
                            </div>   
                            <asp:Button CssClass="fl bg-color-green5" runat="server" ID="btnSearch" Text="Search" />
                          
                        </div>
                        <h3 class="page-search-kw"><asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:Resource, Result_Term %>" /> <strong> <%= this.txtSearch.Text %> </strong><asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:Resource, search_with_project %>" /></h3>
                        <cc1:SearchConditionList ID="SearchConditionList1" OnCommand="SearchConditionList1_Command"   runat="server" />

                        <div class="span9 tlr"><asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:Resource, Sortby %>" /> <asp:LinkButton ID="LinkButton1" CommandName="SortBy" CommandArgument="popular" Text="Most Popular" runat="server" OnCommand="SortBy_Command"></asp:LinkButton> | 
                        <asp:LinkButton ID="LinkButton2" CommandName="SortBy" CommandArgument="recent" Text="<%$ Resources:Resource, Most_Recent %>" runat="server" OnCommand="SortBy_Command"></asp:LinkButton> | 
                        <asp:LinkButton ID="LinkButton3" CommandName="SortBy" CommandArgument="ending" Text="<%$ Resources:Resource, Ending_Soonest %>" runat="server" OnCommand="SortBy_Command"></asp:LinkButton>
                        </div>
                        <asp:Repeater ID="DataRepeater" runat="server">
                        <ItemTemplate>
                           <div class="page-searchrst">
                            <div class="tile image bg-color-lighten">
                                <div class="tile-content">
                                    <a href="ProjectDetails.aspx?pid=<%# Eval("ProjectId") %>">
                                    <img src="<%# GetProjectImage(Eval("ImgUrl")).Substring(1)  %>" width="150" height="150" alt="FOOTBALL"></a>
                                </div>
                            </div>
                            <div class="tile triple bg-color-green6">
                                <div class="tile-content">
                                    <h2><a href="ProjectDetails.aspx?pid=<%# Eval("ProjectId") %>" class="fg-color-blueDark"><%# Eval("ProjectName")%> </a></h2>
                                    <h5 class="fg-color-blue"><asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:Resource, Category %>" />: <a href="" class="fg-color-blueDark"><%# Eval("CategoryName")%></a></h5>
                                    <p class="fg-color-grayDark"><%# Eval("ProjectSummary")%></p>
                                </div>
                                <div class="brand bg-color-green7">
                                    <span class="name"><asp:Localize ID="Localize7" runat="server" Text="<%$ Resources:Resource, Progress %>" />: <%# GetPercent(Eval("TotalFunding"), Eval("ReceivedFunding"))%>% of 100% of the goal</span>
                                </div>
                            </div>
                        </div>
                        
                        </ItemTemplate>
                        </asp:Repeater>
                        <div class="pagination tlc">
                            <cc1:CustomPager ID="CustomPager1" RequiredMethod="post" OnCommand="CustomPager1_Command"  runat="server" />
                        </div>
                    </div>
                </div>
                </div>
            </div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</form>
</asp:Content>
