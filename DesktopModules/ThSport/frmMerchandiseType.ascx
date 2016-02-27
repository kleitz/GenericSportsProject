<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmMerchandiseType.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmMerchandiseType" %>
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
    function MerchandiseTypeSaveSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { MerchandiseTypesavevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function MerchandiseTypesavevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divMerchandiseTypesavemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divMerchandiseTypesavemassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
</script>

<script type="text/javascript">
    function MerchandiseTypeUpdateSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { MerchandiseTypeupdatevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function MerchandiseTypeupdatevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divMerchandiseTypeupdatemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divMerchandiseTypeupdatemassage").delay(2000).fadeOut(0);
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
    function validateAndConfirmClose(OnlyClose)
    {
        var validated = Page_ClientValidate('CloseSports');

        if (OnlyClose == "btnCloseMerchandiseType")
        {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Merchandise Type Form ?";
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

                        if (OnlyClose == "btnCloseMerchandiseType") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCloseMerchandiseType))%>;
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

    function validateAndConfirm(btn_clientid)
    {
        var validated = Page_ClientValidate('Sports');

        if (btn_clientid == "btnUpdateMerchandiseType")
        {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Merchandise Type Details ?";
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

                        if (btn_clientid == "btnSaveMerchandiseType") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveMerchandiseType))%>;
                        }

                        if (btn_clientid == "btnUpdateMerchandiseType") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateMerchandiseType))%>;
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

<div id="divMerchandiseTypesavemassage" runat="server" clientidmode="static" style="display: none; position: inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
    <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" Merchandise Type detail are save successfully. ">
    </asp:Label>
</div>

<div id="divMerchandiseTypeupdatemassage" runat="server" clientidmode="static" style="display: none; position: inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
    <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" Merchandise Type detail are update successfully. ">
    </asp:Label>
</div>

<div id="divcancelmassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Cancel.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label3" ClientIDMode="Static" runat="server" Text=" Merchandise Type detail are delete successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static" style="display: none;">
    <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Merchandise Type Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
    <div class="span12">

        <asp:Panel ID="pnlList" runat="server">
            <asp:Panel ID="addPanel" runat="server">
                <div id="submenu" style="float: left;">
                    <ul>
                        <li class="active">
                            <asp:LinkButton ID="btnAddMerchandiseType" runat="server" Height="35px" Text=" Add Merchandise Type" OnClick="btnAddMerchandiseType_Click" ForeColor="White"></asp:LinkButton>
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
                        <span class="hidden-480">Merchandise Type List</span>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>

                <div class="portlet-body flip-scroll">

                    <asp:GridView ID="gvMerchandiseType" runat="server" AutoGenerateColumns="false"
                        CssClass="table-bordered table-striped table-condensed flip-content"
                        ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="10" EmptyDataText="No Records Found"
                        EmptyDataRowStyle-ForeColor="Red" OnPageIndexChanging="gvMerchandiseType_PageIndexChanging"
                        Width="100%">
                        <RowStyle CssClass="grid-row" />
                        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
                        <Columns>

                            <asp:BoundField DataField="MerchandiseType" HeaderText="Merchandise Type" HeaderStyle-CssClass="grid-header-column" ItemStyle-Width="20%" HeaderStyle-Width="25%" ItemStyle-CssClass="grid-column" />

                            <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="grid-header-column" HeaderStyle-VerticalAlign="Middle"
                                ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("MerchandiseDesc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%" >
                                <ItemTemplate>

                                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                                        <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                                        <asp:ListItem Value="Edit">Edit</asp:ListItem>
                                       <%-- <asp:ListItem Value="Delete">Delete</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnMerchandiseTypeID" runat="server" Value='<%#Eval("MerchandiseTypeId") %>' />
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
                        <span class="hidden-480">Merchandise Type Detail</span>
                    </div>
                </div>

                <asp:HiddenField ID="hdn_MerchandiseTypeID" runat="server" />

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
		                                <label class="control-label">Sport : </label>
                                            <div class="startsetallfrom">
                                            <span class="help-inline"><font Color="red"><b>*</b></font></span>
                                        </div>
                                        <div class="controls" style="position:relative;">  
                                            <asp:DropDownList ID="ddlSport" runat="server"  CssClass="large m-wrap"/>
               
                                            <asp:RequiredFieldValidator ID="rfvddlSport" ClientIDMode="Static" runat="server" InitialValue="0" 
                                                ErrorMessage="Sport Required !" CssClass="errorfordnn" SetFocusOnError="true" ControlToValidate="ddlSport"
                                                ValidationGroup="Sports" Text="Sport Required !"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="control-group">
                                        <label class="control-label">
                                            <asp:Label ID="lblMerchandiseTypeName" runat="server" Text="Merchandise Type :"></asp:Label>
                                        </label>
                                        <div class="startsetallfrom">
                                            <span class="help-inline"><font color="red"><b>*</b></font></span>
                                        </div>
                                        <div class="controls" style="position: relative;">
                                            <asp:TextBox ID="txtMerchandiseTypeName" runat="server" CssClass="m-wrap large" />
                                            <asp:RequiredFieldValidator ID="rfvtxtMerchandiseTypeName" runat="server" ErrorMessage="Merchandise Title"
                                                ControlToValidate="txtMerchandiseTypeName" SetFocusOnError="true"
                                                ValidationGroup="Sports" Text=" MerchandiseType Required !" CssClass="errorfordnn" ClientIDMode="Static" />
                                            <asp:RegularExpressionValidator ID="rgvtxtMerchandiseTypeName"
                                                Display="Static" ControlToValidate="txtMerchandiseTypeName"
                                                ValidationGroup="Sports" CssClass="errorfordnn"
                                                ValidationExpression="^[\s\S]{0,100}$"
                                                runat="server" ErrorMessage="Maximum 100 characters allowed.">
                                            </asp:RegularExpressionValidator>
                                            <asp:CustomValidator ID="cvtxtMerchandiseTypeName" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true"
                                                ControlToValidate="txtMerchandiseTypeName" EnableClientScript="true" ClientValidationFunction="validateTextBox"
                                                CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                                            </asp:CustomValidator>
                                        </div>
                                    </div>

                                    <div class="control-group">
                                        <label class="control-label">
                                            <asp:Label ID="lblMerchandiseTypeDescription" runat="server" Text=" Description :"></asp:Label>
                                        </label>
                   
                                        <div class="controls" style="position: relative;">
                                            <asp:TextBox ID="txtMerchandiseTypeDescription" runat="server" CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px" />

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                Display="Static" ControlToValidate="txtMerchandiseTypeDescription"
                                                ValidationGroup="Sports" CssClass="errorfordnn"
                                                ValidationExpression="^[\s\S]{0,300}$"
                                                runat="server" ErrorMessage="Maximum 300 characters allowed.">
                                            </asp:RegularExpressionValidator>
                                            <asp:CustomValidator ID="cvtxtSportDescription" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true"
                                                ControlToValidate="txtMerchandiseTypeDescription" EnableClientScript="true" ClientValidationFunction="validateTextBox"
                                                CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                                            </asp:CustomValidator>


                                        </div>

                                    </div>
                                </div>

                            </div>

                            <div class="form-actions">

                                <div class="right_div_css">
                                    <asp:Button ID="btnSaveMerchandiseType" runat="server" Width="100px" Text="Save" ClientIDMode="Static"
                                        OnClick="btnSaveMerchandiseType_Click" ValidationGroup="Sports"
                                        OnClientClick="return validateAndConfirm(this.id);"
                                        CssClass="btn blue" />

                                    <asp:Button ID="btnUpdateMerchandiseType" runat="server" Width="100px" Text="Update" ClientIDMode="Static"
                                        OnClick="btnUpdateMerchandiseType_Click" Visible="false"
                                        OnClientClick="return validateAndConfirm(this.id);"
                                        CssClass="btn red" ValidationGroup="Sports" />

                                    <asp:Button ID="btnCloseMerchandiseType" runat="server" Width="100px" Text="Cancel"
                                        OnClick="btnCloseMerchandiseType_Click" CssClass="btn" ClientIDMode="Static" ValidationGroup="CloseSports"
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




<script type="text/javascript">
    $('.datetimepicker').datetimepicker()
	.datetimepicker({ value: '', step: 15 });


    $('.enddatetimepicker').datetimepicker()
	.datetimepicker({ value: '', step: 15 });

    var logic = function (currentDateTime) {
        if (currentDateTime) {
            if (currentDateTime.getDay() == 6) {
                this.setOptions({
                    minTime: '11:00:00'
                });
            } else
                this.setOptions({
                    minTime: '8:00:00'
                });
        }
    };
</script>

<script type="text/javascript">
    function imgError(image) {
        image.onerror = "";
        image.src = "\\DesktopModules\\ThSport\\Images\\OtherImages\\1_pix.png";
        return true;
    }
</script>
