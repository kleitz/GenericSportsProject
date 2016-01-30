<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmClubOwner.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmClubOwner" %>

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
     function validateAndConfirmClose(OnlyClose) {
         var validated = Page_ClientValidate('CloseSports');

         if (OnlyClose == "btnCancelClubOwner") {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close ClubOwner Form ?";
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

                         if (OnlyClose == "btnCancelClubOwner")
                         {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCancelClubOwner))%>;
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

         if (btn_clientid == "btnUpdateClubOwner") {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update ClubOwner Details ?";
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

                         if (btn_clientid == "btnSaveClubOwner")
                         {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveClubOwner))%>;
                         }

                         if (btn_clientid == "btnUpdateClubOwner")
                         {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateClubOwner))%>;
                         }

                     },
                     Cancel: function ()
                     {
                         $(this).dialog('close');
                         return false;
                     }
                 }

             });

         }
         return false;
     }

     $(document).ready(function ()
     {
         //Reset drop down list
         $(".ddlActionSelect > option:first").attr("selected", "selected");
     });

</script>

<div id="divsavemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" ClubOwner detail are save successfully. ">
     </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" ClubOwner detail are update successfully. ">
     </asp:Label>
</div>

<div id="divcancelmassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Cancel.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label3" ClientIDMode="Static" runat="server" Text=" ClubOwner detail are delete successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
    <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save ClubOwner Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

        <panel id="pnlClubOwnerGrid" runat="server">

    <asp:Panel ID="addPanel" runat="server">    
        <div id="submenu">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddClubOwner" runat="server" 
                                    Height="35px" Text=" Add Owner " 
                                    onclick="btnAddClubOwner_Click" ForeColor="White"/>
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
					<span class="hidden-480"> <asp:Label ID="lbl_Club_Name" runat="server"></asp:Label> - Club Owner List</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
			

<div class="portlet-body flip-scroll">
		
          <asp:GridView ID="gvClubOwner" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" 
                        AllowPaging="true" PageSize="10" EmptyDataText="No Records Found" 
                        CssClass="table-bordered table-striped table-condensed flip-content" 
                        HorizontalAlign="Center" AlternatingRowStyle-Font-Size="X-Large" 
                        CellPadding="5" CellSpacing="5" Width="100%"
                        onpageindexchanging="gvClubOwner_PageIndexChanging">
            <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />

		<Columns>

        <asp:TemplateField HeaderText="ClubOwnersId" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                    Visible="false" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                        <asp:Label ID="lblClubOwnersId" runat="server" Text='<%#Eval("ClubOwnersId") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
         </asp:TemplateField>

        	<%--<asp:TemplateField HeaderText="Club Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="width:364px; display: inline-block;">
					    <asp:Label ID="lblClubName" runat="server" Text='<%#Eval("ClubName") %>' ToolTip="Club Name"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Owner Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblOwnerName" runat="server" Text='<%#Eval("OwnerName") %>' ToolTip="Club Owner Name"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Percentage" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblOwnerPercentage" runat="server" Text='<%#Eval("OwnerPercentage") %>' ToolTip="Club Owner Percentage"></asp:Label>
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
                        <asp:Label ID="lblddlActionClubOwnersId" runat="server" Text='<%#Eval("ClubOwnersId") %>' Visible="false">
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

<asp:Panel ID="pnlClubOwnerEntry" runat="server">

    <div style="padding:10px 0px;">
            * Note: All Fields marked with an asterisk (*) are required.
    </div>

    <div class="portlet box blue tabbable">
			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480">Club Owner Detail</span>
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
                <asp:Label ID="lblClubOwner" runat="server" Text=" Owner Name :" ></asp:Label>
		    </label>
            <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
            <div class="controls" style="position:relative;">  
                <asp:DropDownList ID="ddlClubOwner" runat="server"  CssClass="large m-wrap"/>
                <asp:RequiredFieldValidator ID="rfvddlClubOwner" ClientIDMode="Static" runat="server" InitialValue="0" 
                                                     ErrorMessage="Club Owner Required !" CssClass="errorfordnn" SetFocusOnError="true" ControlToValidate="ddlClubOwner"
                                                     ValidationGroup="Sports" Text="Club Owner Name Required !"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblClubOwnerDescription" runat="server" Text="Description :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtClubOwnerDescription" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px"/>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                    Display="Static" ControlToValidate="txtClubOwnerDescription"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,300}$" 
                                                    runat="server" ErrorMessage="Maximum 300 characters allowed.">
                    </asp:RegularExpressionValidator>  
                  <asp:CustomValidator ID="cvtxtClubOwnerDescription" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtClubOwnerDescription" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>

         <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblClubOwnerPercentage" runat="server" Text="Percentage :"></asp:Label>
            </label>
		    <div class="controls" style="position:relative;">
                   <asp:TextBox ID="txtClubOwnerPercentage" runat="server" CssClass="m-wrap small"></asp:TextBox>%
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                    Display="Static" ControlToValidate="txtClubOwnerPercentage"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,5}$" 
                                                    runat="server" ErrorMessage="Maximum 5 characters allowed.">
                    </asp:RegularExpressionValidator>  
                  <asp:CustomValidator ID="cvtxtClubOwnerPercentage" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtClubOwnerPercentage" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
            </div> 
        </div>

        <div class="form-actions">
            <div class="right_div_css">

                    <asp:Button ID="btnSaveClubOwner" runat="server"  Text=" Save " OnClick="btnSaveClubOwner_Click" 
                                ValidationGroup="Sports" CssClass="btn blue" ClientIDMode="Static" Width="100px"
                                OnClientClick="return validateAndConfirm(this.id);" />

                    <asp:Button ID="btnUpdateClubOwner" runat="server"  Text=" Update " OnClick="btnUpdateClubOwner_Click" 
                                ValidationGroup="Sports" CssClass="btn red" ClientIDMode="Static" Width="100px"
                                OnClientClick="return validateAndConfirm(this.id);" />

                    <asp:Button ID="btnCancelClubOwner" runat="server" Text="Cancel" OnClick="btnCancelClubOwner_Click" CssClass="btn" 
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
