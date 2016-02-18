<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmMerchandise.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmMerchandise" %>

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
    function MerchandiseSaveSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { MerchandisesavevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function MerchandisesavevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divMerchandisesavemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divMerchandisesavemassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
</script>

<script type="text/javascript">
    function MerchandiseUpdateSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { MerchandiseupdatevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function MerchandiseupdatevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divMerchandiseupdatemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divMerchandiseupdatemassage").delay(2000).fadeOut(0);
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
        if (OnlyClose == "btnCloseMerchandise") {
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

                        if (OnlyClose == "btnCloseMerchandise") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCloseMerchandise))%>;
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

        if (btn_clientid == "btnUpdateMerchandise") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Match Type Details ?";
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

                        if (btn_clientid == "btnSaveMerchandise") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveMerchandise))%>;
                        }

                        if (btn_clientid == "btnUpdateMerchandise") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateMerchandise))%>;
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
   .ui-dialog , .ui-dialog-buttonpane 
   {
       margin:0 !important;
       padding:0 !important;
   }
   .ui-widget-content 
   {
       overflow: hidden;
       display: table;
       position: relative;
       width: 100%;
       background-color: rgba(255,255,255,.98) !important;
       font-size: 16px;
       height:17% !important;
   }
</style>

<div id="divMerchandisesavemassage" runat="server" clientidmode="static" style="display: none; position: inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
    <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" MatchType detail are save successfully. ">
    </asp:Label>
</div>

<div id="divMerchandiseupdatemassage" runat="server" clientidmode="static" style="display: none; position: inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
    <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" MatchType detail are update successfully. ">
    </asp:Label>
</div>

<div id="divcancelmassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Cancel.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label3" ClientIDMode="Static" runat="server" Text=" MatchType detail are delete successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static" style="display: none;">
    <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Merchandise Details ?"></asp:Label>
    </div>
</div>


<div class="row-fluid">
    <div class="span12">
         <asp:Panel ID="pnlList" runat="server">
            <asp:Panel ID="addPanel" runat="server">
                <div id="submenu" style="float: left;">
                    <ul>
                        <li class="active">
                            <asp:LinkButton ID="btnAddMerchandise" runat="server" Height="35px" Text=" Add Merchandise" OnClick="btnAddMerchandise_Click" ForeColor="White"></asp:LinkButton>
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
                        <span class="hidden-480">Merchandise List</span>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>

                <div class="portlet-body flip-scroll">

                    <asp:GridView ID="gvMerchandise" runat="server" AutoGenerateColumns="false"
                        CssClass="table-bordered table-striped table-condensed flip-content"
                        ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="10" EmptyDataText="No Records Found"
                        EmptyDataRowStyle-ForeColor="Red" OnPageIndexChanging="gvMerchandise_PageIndexChanging"
                        Width="100%">
                        <RowStyle CssClass="grid-row" />
                        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
                        <Columns>

                            <asp:BoundField DataField="MerchandiseTitle" HeaderText="Merchandise Title" HeaderStyle-CssClass="grid-header-column" ItemStyle-Width="20%" ItemStyle-CssClass="grid-column" />

                             <asp:BoundField DataField="MerchandiseType" HeaderText="Merchandise Type" HeaderStyle-CssClass="grid-header-column" ItemStyle-Width="20%" ItemStyle-CssClass="grid-column" />
                             <asp:BoundField DataField="MerchandisePrice" HeaderText="Merchandise Price" HeaderStyle-CssClass="grid-header-column" ItemStyle-Width="20%" ItemStyle-CssClass="grid-column" />

                            <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="grid-header-column" HeaderStyle-VerticalAlign="Middle"
                                ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="150px">
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
                                        <asp:ListItem Value="Delete">Delete</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnMerchandiseID" runat="server" Value='<%#Eval("merchandiseid") %>' />
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

                <asp:HiddenField ID="hdn_MerchandiseID" runat="server" />

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
		                                <label class="control-label">Merchandise Type : </label>
                                         <div class="startsetallfrom">
                                          <span class="help-inline"><font Color="red"><b>*</b></font></span>
                                        </div>
                                        <div class="controls" style="position:relative;">  
                                            <asp:DropDownList ID="ddlMerchandiseType" runat="server"  CssClass="large m-wrap"/>
               
                                            <asp:RequiredFieldValidator ID="rfvddlMerchandiseType" ClientIDMode="Static" runat="server" InitialValue="0" 
                                                ErrorMessage="Season Required !" CssClass="errorfordnn" SetFocusOnError="true" ControlToValidate="ddlMerchandiseType"
                                                ValidationGroup="Sports" Text="Merchandise Type Required !"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                   
                                    <div class="control-group">
                                        <label class="control-label">
                                            <asp:Label ID="lblMerchandisetitle" runat="server" Text="Merchandise Title :"></asp:Label>
                                        </label>
                                        <div class="startsetallfrom">
                                            <span class="help-inline"><font color="red"><b>*</b></font></span>
                                        </div>
                                        <div class="controls" style="position: relative;">
                                            <asp:TextBox ID="txtMerchandiseTitle" runat="server" CssClass="m-wrap large" />
                                            <asp:RequiredFieldValidator ID="rfvtxtMerchandiseTypeName" runat="server" ErrorMessage="Merchandise Title"
                                                ControlToValidate="txtMerchandiseTitle" SetFocusOnError="true"
                                                ValidationGroup="Sports" Text=" MerchandiseType Name Required !" CssClass="errorfordnn" ClientIDMode="Static" />
                                            <asp:RegularExpressionValidator ID="rgvtxtMerchandiseTypeName"
                                                Display="Static" ControlToValidate="txtMerchandiseTitle"
                                                ValidationGroup="Sports" CssClass="errorfordnn"
                                                ValidationExpression="^[\s\S]{0,100}$"
                                                runat="server" ErrorMessage="Maximum 100 characters allowed.">
                                            </asp:RegularExpressionValidator>
                                            <asp:CustomValidator ID="cvtxtMerchandiseTypeName" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true"
                                                ControlToValidate="txtMerchandiseTitle" EnableClientScript="true" ClientValidationFunction="validateTextBox"
                                                CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                                            </asp:CustomValidator>
                                        </div>
                                    </div>

                                    <div class="control-group">
                                        <label class="control-label">
                                            <asp:Label ID="lblMerchandiseTypeDescription" runat="server" Text="Description :"></asp:Label>
                                        </label>
                   
                                        <div class="controls" style="position: relative;">
                                            <asp:TextBox ID="txtMerchandiseDescription" runat="server" CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px" />

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                Display="Static" ControlToValidate="txtMerchandiseDescription"
                                                ValidationGroup="Sports" CssClass="errorfordnn"
                                                ValidationExpression="^[\s\S]{0,300}$"
                                                runat="server" ErrorMessage="Maximum 300 characters allowed.">
                                            </asp:RegularExpressionValidator>
                                            <asp:CustomValidator ID="cvtxtSportDescription" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true"
                                                ControlToValidate="txtMerchandiseDescription" EnableClientScript="true" ClientValidationFunction="validateTextBox"
                                                CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                                            </asp:CustomValidator>


                                        </div>

                                    </div>

                                     <div class="control-group">
		                                 <label class="control-label">          
                                               <asp:Label ID="lblSponsorAmount" runat="server" Text=" Price :" ></asp:Label>
                                         </label>
                                         <div class="controls" style="position:relative;">
                                              <asp:TextBox ID="txtPrice" runat="server" CssClass="m-wrap small" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;" />
                                                <span id="error" style="color: Red; display: none">* Input digits (0 - 9)</span>
                                               <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                                                Display="Static" ControlToValidate="txtPrice"  
                                                                                ValidationGroup="Sports" CssClass="errorfordnn"
                                                                                ValidationExpression = "^[\s\S]{0,10}$" 
                                                                                runat="server" ErrorMessage="Maximum 10 characters allowed.">
                                               </asp:RegularExpressionValidator>  
                                               <asp:CustomValidator ID="CustomValidator5" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                                             ControlToValidate="txtPrice" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                                             CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                                               </asp:CustomValidator>
                                         </div>
                                    </div>

                                     <div class="control-group">
		                                <label class="control-label">
                                        <asp:Label ID="lblActive" runat="server" Text=" Is Active :"></asp:Label>
                                        </label>
                                        <div class="controls">
                                            <label class="checkbox"> 
                                                <asp:CheckBox ID="ChkIsActive" runat="server" />
                                            </label>
                                         </div>
                                    </div>

                                   <div class="control-group">
		                                <label class="control-label">
                                        <asp:Label ID="lblShow" runat="server" Text=" Is Show :"></asp:Label>
                                        </label>
                                        <div class="controls">
                                            <label class="checkbox"> 
                                                <asp:CheckBox ID="ChkIsShow" runat="server" />
                                            </label>
                                         </div>
                                    </div>    
       
                                 </div>
                             </div>

                           

                            <div class="form-actions">

                                <div class="right_div_css">
                                    <asp:Button ID="btnSaveMerchandise" runat="server" Width="100px" Text="Save" ClientIDMode="Static"
                                        OnClick="btnSaveMerchandise_Click" ValidationGroup="Sports"
                                        OnClientClick="return validateAndConfirm(this.id);"
                                        CssClass="btn blue" />

                                    <asp:Button ID="btnUpdateMerchandise" runat="server" Width="100px" Text="Update" ClientIDMode="Static"
                                        OnClick="btnUpdateMerchandise_Click" Visible="false"
                                        OnClientClick="return validateAndConfirm(this.id);"
                                        CssClass="btn red" ValidationGroup="Sports" />

                                    <asp:Button ID="btnCloseMerchandise" runat="server" Width="100px" Text="Cancel"
                                        OnClick="btnCloseMerchandise_Click" CssClass="btn" ClientIDMode="Static" ValidationGroup="CloseSports"
                                        OnClientClick="return validateAndConfirmClose(this.id);" />

                                </div>

                            </div>
                        </div>

                    </div>

                
           
           </asp:Panel>
    </div>
</div>


<script type="text/javascript">
    var specialKeys = new Array();
    specialKeys.push(8); //Backspace
    function IsNumeric(e) {
        var keyCode = e.which ? e.which : e.keyCode
        var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
        document.getElementById("error").style.display = ret ? "none" : "inline";
        return ret;
    }
    </script>