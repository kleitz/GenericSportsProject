<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmTeamMember.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmTeamMember" %>

<script type="text/javascript">
    function validateTextBox(sender, args)
    {
        var txtcheckValue = args.Value;

        var chars = ['<', '>', '*', '$', '@', ',', '_', '%', '.', '!', '#', '^', '&', '(', ')', '-', '=', '+', '\\', '|', '?', '/', '[', ']', '{', '}'];
        args.IsValid = true;

        if (txtcheckValue.length > 0)
        {
            var currentChar = txtcheckValue.charAt(0);

            if (chars.indexOf(currentChar) >= 0)
            {
                args.IsValid = false;
                txtcheckValue.value = "";
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
</script>

<script type="text/javascript">
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
     function validateAndConfirmClose(OnlyClose)
     {
         var validated = Page_ClientValidate('CloseSports');

         if (OnlyClose == "btnCancelTeamMember")
         {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close TeamMember Form ?";
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

                         if (OnlyClose == "btnCancelTeamMember")
                         {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCancelTeamMember))%>;
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

         if (btn_clientid == "btnUpdateTeamMember")
         {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update TeamMember Details ?";
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

                         if (btn_clientid == "btnSaveTeamMember")
                         {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveTeamMember))%>;
                         }

                         if (btn_clientid == "btnUpdateTeamMember")
                         {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateTeamMember))%>;
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
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" TeamMember detail are save successfully. ">
     </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/AllImage/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" TeamMember detail are update successfully. ">
     </asp:Label>
</div>

<div id="divcancelmassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/AllImage/Cancel.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label3" ClientIDMode="Static" runat="server" Text=" TeamMember detail are delete successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
    <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save TeamMember Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

   <panel id="pnlGridTeamMember" runat="server">

    <asp:Panel ID="addPanel" runat="server">    
        <div id="submenu">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddTeamMember" runat="server" 
                                    Height="35px" Text=" Add Member " 
                                    onclick="btnAddTeamMember_Click" ForeColor="White"/>
                </li>
            </ul>
        </div>
        <div style="position: relative;float: right;padding-top: 15px;margin-right: -0.9%;">
             <asp:Button ID="btnGoToBack" runat="server" Text="Back" 
                         OnClick="btnGoToBack_Click" 
                         CssClass="btn blue back_btn_Position" />
        </div>
    </asp:Panel>
 
    <div class="portlet box green">

			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480"> <asp:Label ID="lbl_Team_Member" runat="server"></asp:Label> - Team Member List</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
			

    <div class="portlet-body flip-scroll">
		
          <asp:GridView ID="gvTeamMember" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" 
                        AllowPaging="true" PageSize="10" EmptyDataText="No Records Found" 
                        CssClass="table-bordered table-striped table-condensed flip-content" 
                        HorizontalAlign="Center" AlternatingRowStyle-Font-Size="X-Large" 
                        CellPadding="5" CellSpacing="5" Width="100%"
                        onpageindexchanging="gvTeamMember_PageIndexChanging">
            <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />

		<Columns>

        <asp:TemplateField HeaderText="TeamMemberID" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                    Visible="false" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                        <asp:Label ID="lblTeamMemberId" runat="server" Text='<%#Eval("TeamMemberID") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
         </asp:TemplateField>

            <asp:TemplateField HeaderText="Member Type" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblMemberType" runat="server" Text='<%#Eval("TeamMemberTypeValue") %>' ToolTip=" Member Type "></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Member Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblMemberName" runat="server" Text='<%#Eval("TeamMemberName") %>' ToolTip=" Member Name "></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

              <asp:TemplateField HeaderText="Jersey No" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblTeamMemberJerseyNo" runat="server" Text='<%#Eval("TeamMemberJerseyNo") %>' ToolTip=" Team Member Jersey No "></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

              <asp:TemplateField HeaderText=" Jersey Name " HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblTeamMemberJerseyName" runat="server" Text='<%#Eval("TeamMemberJerseyName") %>' ToolTip=" Team Member Jersey Name "></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Famous Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblTeamMemberFamousName" runat="server" Text='<%#Eval("TeamMemberFamousName") %>' ToolTip=" Team Member Famous Name "></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

             <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                               ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="135px">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" 
                                      OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                            <asp:ListItem Value="Edit">Edit</asp:ListItem>
                            <%--<asp:ListItem Value="Delete">Delete</asp:ListItem>--%>
                    </asp:DropDownList>
                        <asp:Label ID="lblddlActionTeamMemberId" runat="server" Text='<%#Eval("TeamMemberID") %>' Visible="false">
                        </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

		</Columns>
              <PagerSettings Mode="NumericFirstLast" PageButtonCount="8" />
            <PagerStyle CssClass="paging" HorizontalAlign="Center"/>
	    </asp:GridView>  
    
   </div>  
            
    <input type="hidden" runat="server" id="hidRegID" />
   </div>

</panel>

<asp:Panel ID="pnlEntryTeamMember" runat="server">

    <div style="padding:10px 0px;">
            * Note: All Fields marked with an asterisk (*) are required.
    </div>

    <div class="portlet box blue tabbable">
			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480"> Team Member Detail </span>
				</div>
			</div>

    <div class="portlet-body form">

	<div class="tabbable portlet-tabs">

    <div class="tab-content" style="margin-top:10px !important;">
		<div class="tab-pane active" id="portlet_tab1">

        <div class="form-horizontal">

        <div style="width: 100%;margin-top:20px;">
                
        </div>

        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblTeamMemberType" runat="server" Text=" Member Type :" ></asp:Label>
             </label>
              <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlTeamMemberType" runat="server" CssClass="medium m-wrap"/>
                  <asp:RequiredFieldValidator ID="RFVTeamMemberType" runat="server" ErrorMessage=" Member Type,"
                                                ControlToValidate="ddlTeamMemberType" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select Member Type Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
             </div>
        </div>

        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblSelectMember" runat="server" Text=" Member :" ></asp:Label>
             </label>
              <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlSelectMember" runat="server" CssClass="medium m-wrap"/>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Member ,"
                                                ControlToValidate="ddlSelectMember" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select Member Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
             </div>
        </div>

       <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblTeamMemberJerseyNo" runat="server" Text=" Jersey No :" ></asp:Label>
             </label>
             <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtTeamMemberJerseyNo" runat="server" CssClass="m-wrap small"/>
                  <asp:RequiredFieldValidator ID="rfvtxtTeamMemberJerseyNo" runat="server" ErrorMessage=" Member Jersey No "  
                                                ControlToValidate="txtTeamMemberJerseyNo" SetFocusOnError="true" 
                                                ValidationGroup="Sports" Text=" Member Jersey No Required !" CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="rgvtxtTeamMemberJerseyNo"
                                                    Display="Static" ControlToValidate="txtTeamMemberJerseyNo"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,5}$" 
                                                    runat="server" ErrorMessage="Maximum 5 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="cvtxtTeamMemberJerseyNo" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtTeamMemberJerseyNo" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblTeamMemberJerseyName" runat="server" Text=" Jersey Name :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtTeamMemberJerseyName" runat="server"  CssClass="m-wrap large"/>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                    Display="Static" ControlToValidate="txtTeamMemberJerseyName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                    </asp:RegularExpressionValidator>  
                  <asp:CustomValidator ID="cvtxtTeamMemberJerseyName" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtTeamMemberJerseyName" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblTeamMemberFamousname" runat="server" Text=" Famous Name :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtTeamMemberFamousname" runat="server"  CssClass="m-wrap large"/>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                    Display="Static" ControlToValidate="txtTeamMemberFamousname"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                    </asp:RegularExpressionValidator>  
                  <asp:CustomValidator ID="CustomValidator1" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtTeamMemberFamousname" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>

        <div class="form-actions">
            <div class="right_div_css">

                    <asp:Button ID="btnSaveTeamMember" runat="server"  Text=" Save " OnClick="btnSaveTeamMember_Click" 
                                ValidationGroup="Sports" CssClass="btn blue" ClientIDMode="Static" Width="100px"
                                OnClientClick="return validateAndConfirm(this.id);" />

                    <asp:Button ID="btnUpdateTeamMember" runat="server"  Text=" Update " OnClick="btnUpdateTeamMember_Click" 
                                ValidationGroup="Sports" CssClass="btn red" ClientIDMode="Static" Width="100px"
                                OnClientClick="return validateAndConfirm(this.id);" />

                    <asp:Button ID="btnCancelTeamMember" runat="server" Text="Cancel" OnClick="btnCancelTeamMember_Click" CssClass="btn" 
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
