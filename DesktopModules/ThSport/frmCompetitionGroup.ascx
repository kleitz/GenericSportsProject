<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmCompetitionGroup.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmCompetitionGroup" %>


<script type="text/javascript">

    function validateTextBox(sender, args) {

        var txtcheckValue = args.Value;

        var chars = ['<', '>', '*', '$', '@', ',', '_', '%', '.'];
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

    

    function SaveSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { savevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
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
    function UpdateSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { updatevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function updatevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divupdatemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divupdatemassage").delay(2000).fadeOut(0);
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

 <script type="text/javascript">
     function validateAndConfirmClose(OnlyClose) {
         var validated = Page_ClientValidate('CloseSports');

         if (OnlyClose == "btnCancelCompetitionGroup") {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close CompetitionGroup Form ?";
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

                         if (OnlyClose == "btnCancelCompetitionGroup") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCancelCompetitionGroup))%>;
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

         if (btn_clientid == "btnUpdateCompetitionGroup") {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update CompetitionGroup Details ?";
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

                         if (btn_clientid == "btnSaveCompetitionGroup") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveCompetitionGroup))%>;
                         }

                         if (btn_clientid == "btnUpdateCompetitionGroup") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateCompetitionGroup))%>;
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

     $(document).ready(function () {
         //Reset drop down list
         $(".ddlActionSelect > option:first").attr("selected", "selected");
     });

</script>

<div id="divsavemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/AllImage/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" CompetitionGroup detail are save successfully. ">
     </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/AllImage/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" CompetitionGroup detail are update successfully. ">
     </asp:Label>
</div>

<div id="divcancelmassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/AllImage/Cancel.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label3" ClientIDMode="Static" runat="server" Text=" CompetitionGroup detail are delete successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
    <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save CompetitionGroup Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

   <asp:Panel id="pnlCompetitionGroupGrid" runat="server">

    <asp:Panel ID="addPanel" runat="server">    
        <div id="submenu">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddCompetitionGroup" runat="server" 
                                    Height="35px" Text=" Add CompetitionGroup" 
                                    onclick="btnAddCompetitionGroup_Click" ForeColor="White"/>
                </li>
            </ul>
        </div>
        <div style="position: relative;float: right;padding-top: 15px;margin-right: -0.9%;">
        </div>
    </asp:Panel>
 
    <div class="portlet box green">

			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480">Competition Group List</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
			

    <div class="portlet-body flip-scroll">
		
          <asp:GridView ID="gvCompetitionGroup" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" 
                        AllowPaging="true" PageSize="10" EmptyDataText="No Records Found" EmptyDataRowStyle-ForeColor="Red" 
                        CssClass="table-bordered table-striped table-condensed flip-content" 
                        HorizontalAlign="Center" AlternatingRowStyle-Font-Size="X-Large" 
                        CellPadding="5" CellSpacing="5" Width="100%"
                        onpageindexchanging="gvCompetitionGroup_PageIndexChanging">
            <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />

		<Columns>
            <asp:TemplateField HeaderText="CompetitionGroup" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblCompetitionGroupName" runat="server" Text='<%#Eval("CompetitionGroupName") %>' ToolTip="Group Name"></asp:Label>
                    </div> 
                    <asp:HiddenField ID="hdn_CompetitionGroup_Id" runat="server" Value='<%#Eval("CompetitionGroupId") %>'></asp:HiddenField>
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Abbreviation" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblCompetitionGroupAbbr" runat="server" Text='<%#Eval("CompetitionGroupAbbr") %>' ToolTip="Abbreviation"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="No of Teams" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblTotalNumofTeams" runat="server" Text='<%#Eval("NumberofTeams") %>' ToolTip="No of Teams"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

             <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                               ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" 
                                      OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                            <asp:ListItem Value="Edit">Edit</asp:ListItem>
                            <asp:ListItem Value="Edit">Delete</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>

		</Columns>
              <PagerSettings Mode="NumericFirstLast" PageButtonCount="8" />
                <PagerStyle CssClass="paging" HorizontalAlign="Center"/>
	    </asp:GridView>  
    
   </div>  

   </div>

    </asp:Panel>

<asp:Panel ID="pnlCompetitionGroupEntry" runat="server">

    <div style="padding:10px 0px;">
            * Note: All Fields marked with an asterisk (*) are required.
    </div>

    <div class="portlet box blue tabbable">
			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480"> Competition Group Details</span>
				</div>
			</div>

    <div class="portlet-body form">

	<div class="tabbable portlet-tabs">

    <div class="tab-content" style="margin-top:10px !important;">
		<div class="tab-pane active" id="portlet_tab1">

        <div class="form-horizontal">

        <div style="width: 100%;margin-top:20px;"></div>

        <asp:HiddenField ID="hdnCompetitionGroupID" runat="server" />

           <div class="control-group">
		        <label class="control-label">Group Name :" </label>
             
                 <div class="controls" style="position:relative;">
                      <asp:TextBox ID="txtCompetitionGroup" runat="server"  CssClass="m-wrap large" />
                     <span class="help-inline"><font Color="red"><b>*</b></font></span>
                      <asp:RequiredFieldValidator ID="rfvCompetitionGroup" runat="server" ErrorMessage="CompetitionGroup"
                                                  ControlToValidate="txtCompetitionGroup" SetFocusOnError="true" 
                                                  ValidationGroup="Sports" Text="Group Name Required !" 
                                                  CssClass="errorfordnn" ClientIDMode="Static"/>
                       <asp:RegularExpressionValidator ID="rgvtxtCompetitionGroup"
                                                        Display="Static" ControlToValidate="txtCompetitionGroup"  
                                                        ValidationGroup="Sports" CssClass="errorfordnn"
                                                        ValidationExpression = "^[\s\S]{0,100}$" 
                                                        runat="server" ErrorMessage="Maximum 100 characters allowed.">
                       </asp:RegularExpressionValidator>  
                       <asp:CustomValidator ID="cvtxtCompetitionGroup" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" ControlToValidate="txtCompetitionGroup"
                                        EnableClientScript="true" ClientValidationFunction="validateTextBox" CssClass="errorfordnn" Text="First Character Should Not Be Special Character"></asp:CustomValidator>
                 </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblCompetitionGroupAbbr" runat="server" Text="Abbreviation :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtCompetitionGroupAbbr" runat="server"  CssClass="m-wrap small" />
                <span class="help-inline"><font Color="red"><b>*</b></font></span>
                    <asp:RegularExpressionValidator ID="rgvtxtCompetitionGroupAbbr"
                                                    Display="Static" ControlToValidate="txtCompetitionGroupAbbr"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,5}$" 
                                                    runat="server" ErrorMessage="Maximum 5 characters allowed.">
                    </asp:RegularExpressionValidator>  
           </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
            <asp:Label ID="lblNoOfTeam" runat="server" Text="No.Of Teams :"></asp:Label>
            </label>
            <div class="controls">
                <asp:TextBox ID="txtNoOfTeam" runat="server" CssClass="m-wrap small onlynumeric" />
            </div>
        </div>

        <div class="control-group">
		    <label class="control-label">Is Confirm : </label>
            <div class="controls">
                    <asp:CheckBox ID="ChkIsConfirm" runat="server" />
             </div>
        </div>

        <div class="form-actions">
            <div class="right_div_css">

                    <asp:Button ID="btnSaveCompetitionGroup" runat="server"  Text=" Save " OnClick="btnSaveCompetitionGroup_Click" 
                                ValidationGroup="Sports" CssClass="btn blue" ClientIDMode="Static" Width="100px"
                                OnClientClick="return validateAndConfirm(this.id);" />

                    <asp:Button ID="btnUpdateCompetitionGroup" runat="server"  Text=" Update " OnClick="btnUpdateCompetitionGroup_Click" 
                                ValidationGroup="Sports" CssClass="btn red" ClientIDMode="Static" Width="100px"
                                OnClientClick="return validateAndConfirm(this.id);" />

                    <asp:Button ID="btnCancelCompetitionGroup" runat="server" Text="Cancel" OnClick="btnCancelCompetitionGroup_Click" CssClass="btn" 
                                ClientIDMode="Static" ValidationGroup="CloseSports" Width="100px"
                                OnClientClick="return validateAndConfirmClose(this.id);"/>

            </div>    
        </div>

        </div> 

        </div>
    </div>

    </div>

    </div>

    </div>

</asp:Panel>


</div>
</div>

