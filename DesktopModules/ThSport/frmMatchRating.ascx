<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmMatchRating.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmMatchRating" %>

<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<script type="text/javascript">
    function validateTextBox(sender, args) {
        var txtcheckValue = args.Value;

        var chars = ['<', '>', '*', '$', '@', ',', '_', '%', '.', '!', '#', '^', '&', '(', ')', '-', '=', '+', '\\', '|', '?', '/', '[', ']', '{', '}'];
        args.IsValid = true;

        if (txtcheckValue.length > 0) {
            var currentChar = txtcheckValue.charAt(0);

            if (chars.indexOf(currentChar) >= 0) {
                args.IsValid = false;
                txtcheckValue.value = "";
            }
            else {
                args.IsValid = true;
            }
        }
    }
</script>

<script type="text/javascript">
    $(document).ready(function () {
        $('.ddlActionSelect').change(function (evt) {
            evt.preventDefault();
            if ($(this).val() == "Delete") {
                //if (confirm('Are you sure to delete this MatchType?')) {
                //    setTimeout("__doPostBack('" + this.id + "','')", 0);
                //}
                //else {
                //    //do nothing, prevent postback
                //    $(this).prop('selectedIndex', 0);
                //}
                //DeleteSuccessfully();
            }
            else {
                setTimeout("__doPostBack('" + this.id + "','')", 0);
            }
        });

        //Reset drop down list
        $(".ddlActionSelect > option:first").attr("selected", "selected");
    });
</script>

<script type="text/javascript">
    function MatchRatingSaveSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { MatchRatingsavevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function MatchTypesavevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divMatchRatingsavemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divMatchRatingsavemassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
</script>

<script type="text/javascript">
    function MatchRatingUpdateSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { MatchRatingupdatevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function MatchRatingupdatevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divMatchRatingupdatemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divMatchRatingupdatemassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
</script>

<script type="text/javascript">
    function DeleteSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { cancelvalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>



<script type="text/javascript">


    function validateAndConfirmClose(OnlyClose) {
        var validated = Page_ClientValidate('CloseSports');
        if (OnlyClose == "btnCloseMatchRating") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Match Type Form ?";
        }

        if (validated) {
            $("#dialogBox").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",

                buttons: {
                    Ok: function () {

                        if (OnlyClose == "btnCloseMatchRating") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCloseMatchRating))%>;
                        }

                    },
                    Cancel: function () {
                        $(this).dialog('close');
                        return false;
                    }
                }
            });
        }
        return false;
    }

    function validateAndConfirm(btn_clientid) {
        var validated = Page_ClientValidate('Sports');

        if (btn_clientid == "btnUpdateMatchRating") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Match Rating Details ?";
        }

        if (validated) {
            $("#dialogBox").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",

                buttons: {
                    Ok: function () {

                        btn_clientid.disabled = true;

                        if (btn_clientid == "btnSaveMatchRating") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveMatchRating))%>;
                        }

                        if (btn_clientid == "btnUpdateMatchRating") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateMatchRating))%>;
                        }

                    },
                    Cancel: function () {
                        $(this).dialog('close');
                        return false;
                    }
                }
            });
        }
        return false;
    }

</script>

<script type="text/javascript">
    function savevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divsavemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divsavemassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
</script>

<script type="text/javascript">
    function cancelvalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divcancelmassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divcancelmassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
</script>



<style type="text/css">
    .disabled {
        color: #737373;
    }

    .ui-dialog, .ui-dialog-buttonpane {
        margin: 0 !important;
        padding: 0 !important;
    }

    .ui-widget-content {
        overflow: hidden;
        display: table;
        position: relative;
        width: 100%;
        background-color: rgba(255,255,255,.98) !important;
        font-size: 16px;
        height: 17% !important;
    }
</style>



<div id="divMatchRatinsavemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" MatchRating detail are save successfully. ">
     </asp:Label>
</div>

<div id="divMatchRatingupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" MatchRating detail are update successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
     <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save MatchRating Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
    <div class="span12">

        <asp:Panel ID="pnlList" runat="server">
            <asp:Panel ID="addPanel" runat="server">
                <div id="submenu" style="float: left;">
                    <ul>
                        <li class="active">
                            <asp:LinkButton ID="btnAddMatchRating" runat="server" Height="35px" Text=" Add MatchRating" OnClick="btnAddMatchRating_Click" ForeColor="White"></asp:LinkButton>
                        </li>
                    </ul>
                </div>

            </asp:Panel>

            <!-- Html Table -->
            <!-- End Html Table -->

            <!-- BEGIN SAMPLE FORM PORTLET-->
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-reorder"></i>
                        <span class="hidden-480">MatchRating List</span>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>

                <div class="portlet-body flip-scroll">

                    <asp:GridView ID="gvMatchRating" runat="server" AutoGenerateColumns="false"
                        CssClass="table-bordered table-striped table-condensed flip-content"
                        ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="10" EmptyDataText="No Records Found"
                        EmptyDataRowStyle-ForeColor="Red" OnPageIndexChanging="gvMatchRating_PageIndexChanging"
                        Width="100%" OnRowDataBound="gvMatchRating_RowDataBound">
                        <RowStyle CssClass="grid-row" />
                        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
                        <Columns>

                            <asp:BoundField DataField="TeamARating"  HeaderStyle-CssClass="grid-header-column" ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="TeamBRating" HeaderStyle-CssClass="grid-header-column" ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center"/>

                            <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="grid-header-column" HeaderStyle-VerticalAlign="Middle"
                                ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="39%" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("MatchRatingDesc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                <ItemTemplate>

                                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                                        <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                                        <asp:ListItem Value="Edit">Edit</asp:ListItem>
                                      <%--  <asp:ListItem Value="Delete">Delete</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnMatchRatingID" runat="server" Value='<%#Eval("MatchRatingId") %>' />
                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>
                        <PagerStyle CssClass="paging" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>

            </div>
        </asp:Panel>

        <asp:Panel ID="pnlEntry" runat="server" Visible="false">

            <div style="padding: 10px 0px;">
                * Note: All Fields marked with an asterisk (*) are required.
            </div>

            <!-- BEGIN SAMPLE FORM PORTLET-->
            <div class="portlet box blue tabbable">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-reorder"></i>
                        <span class="hidden-480">Match Rating Detail</span>
                    </div>
                </div>

                <asp:HiddenField ID="hdn_MatchRatingID" runat="server" />

                <div class="portlet-body form">
                    <div class="tabbable portlet-tabs">

                        <div id="error_div" runat="server" style="display: none;">
                            <asp:Label ID="error_msg" runat="server" Text="" Visible="false"></asp:Label>
                        </div>

                        <div class="tab-content">
                            <div class="tab-pane active" id="portlet_tab1">

                                <div class="form-horizontal">

                                    <div style="width: 100%; margin-top: 20px;"></div>

                                    <div class="control-group">
                                        <label class="control-label">
                                            <asp:Label ID="lblTeamAName" runat="server" Text="TeamA Name :"></asp:Label>
                                        </label><div class="controls" style="position: relative;">
                                    <asp:TextBox ID="txtMatchTeamAName" runat="server" CssClass="m-wrap large" Enabled="false"/>
                                    </div></div>

                                    <div class="control-group">
                                        <label class="control-label">
                                            <asp:Label ID="lblTeamARating" runat="server" Text="Rating :"></asp:Label>
                                        </label><div class="controls" style="position: relative;">
                                      <asp:DropDownList ID="ddlTeamARating" runat="server" CssClass="medium m-wrap">
                                       <asp:ListItem Selected="True" Value="0"> -- Select -- </asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                          </asp:DropDownList></div>
                                    </div>

                                     <div class="control-group">
                                        <label class="control-label">
                                            <asp:Label ID="lblTeamBName" runat="server" Text="TeamB Name :"></asp:Label>
                                        </label><div class="controls" style="position: relative;">
                                    <asp:TextBox ID="txtMatchTeamBName" runat="server" CssClass="m-wrap large" Enabled="false"/>
                                    </div></div>

                                     <div class="control-group">
                                        <label class="control-label">
                                            <asp:Label ID="lblTeamBRating" runat="server" Text="Rating :"></asp:Label>
                                        </label>
                                         <div class="controls" style="position: relative;">
                                      <asp:DropDownList ID="ddlTeamBRating" runat="server" CssClass="medium m-wrap">
                                       <asp:ListItem Selected="True" Value="0"> -- Select -- </asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                          </asp:DropDownList></div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">
                                            <asp:Label ID="lblMatchRatingDescription" runat="server" Text="Description : "></asp:Label>
                                        </label>
                                         <div class="controls" style="position: relative;">
                                                <asp:TextBox ID="txtMatchRatingDescription" runat="server" CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px" />

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                    Display="Static" ControlToValidate="txtMatchRatingDescription"
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression="^[\s\S]{0,300}$"
                                                    runat="server" ErrorMessage="Maximum 300 characters allowed.">
                                                </asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="cvtxtSportDescription" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true"
                                                    ControlToValidate="txtMatchRatingDescription" EnableClientScript="true" ClientValidationFunction="validateTextBox"
                                                    CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                                                </asp:CustomValidator>


                                            </div>
                                      </div>
                                </div>

                            </div>

                            <div class="form-actions">

                                <div class="right_div_css">
                                    <asp:Button ID="btnSaveMatchRating" runat="server" Width="100px" Text="Save" ClientIDMode="Static"
                                        OnClick="btnSaveMatchRating_Click" ValidationGroup="Sports"
                                        OnClientClick="return validateAndConfirm(this.id);"
                                        CssClass="btn blue" />

                                    <asp:Button ID="btnUpdateMatchRating" runat="server" Width="100px" Text="Update" ClientIDMode="Static"
                                        OnClick="btnUpdateMatchRating_Click" Visible="false"
                                        OnClientClick="return validateAndConfirm(this.id);"
                                        CssClass="btn red" ValidationGroup="Sports" />

                                    <asp:Button ID="btnCloseMatchRating" runat="server" Width="100px" Text="Cancel"
                                        OnClick="btnCloseMatchRating_Click" CssClass="btn" ClientIDMode="Static" ValidationGroup="CloseSports"
                                        OnClientClick="return validateAndConfirmClose(this.id);" />

                                </div>

                            </div>
                        </div>

                    </div>

                </div>
            </div>
           </asp:Panel>
    </div>

    

</div>