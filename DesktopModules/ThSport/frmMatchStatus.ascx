<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmMatchStatus.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmMatchStatus" %>


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
                if (confirm('Are you sure to delete this MatchStatus?')) {
                    setTimeout("__doPostBack('" + this.id + "','')", 0);
                }
                else {
                    //do nothing, prevent postback
                    $(this).prop('selectedIndex', 0);
                }
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
    function MatchStatusSaveSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { MatchStatussavevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function MatchStatussavevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divMatchStatussavemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divMatchStatussavemassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
</script>

<script type="text/javascript">
    function MatchStatusUpdateSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { MatchStatusupdatevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function MatchStatusupdatevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divMatchStatusupdatemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divMatchStatusupdatemassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
</script>

<script type="text/javascript">
    function validateAndConfirmClose(OnlyClose) {
        var validated = Page_ClientValidate('CloseSports');

        if (OnlyClose == "btnCloseMatchStatus") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Match Status Form ?";
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

                        if (OnlyClose == "btnCloseMatchStatus") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCloseMatchStatus))%>;
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

         if (btn_clientid == "btnUpdateMatchStatus") {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Match Status Details ?";
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

                         if (btn_clientid == "btnSaveMatchStatus") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveMatchStatus))%>;
                         }

                         if (btn_clientid == "btnUpdateMatchStatus") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateMatchStatus))%>;
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

<style type="text/css">
    .disabled
    {
       color: #737373;
    }
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

<div id="divMatchStatussavemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" MatchStatus detail are save successfully. ">
     </asp:Label>
</div>

<div id="divMatchStatusupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" MatchStatus detail are update successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
     <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save MatchStatus Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

<asp:Panel ID="pnlList" runat="server">
   <asp:Panel ID="addPanel" runat="server">
        <div id="submenu" style="float:left;">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddMatchStatus" runat="server" Height="35px" Text=" Add MatchStatus" onclick="btnAddMatchStatus_Click" ForeColor="White"></asp:LinkButton>
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
					<span class="hidden-480">MatchStatus List</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
      
    <div class="portlet-body flip-scroll">

        <asp:GridView ID="gvMatchStatus" runat="server" AutoGenerateColumns="false"  
                      CssClass="table-bordered table-striped table-condensed flip-content"
                      ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="10" EmptyDataText="No Records Found" 
                      EmptyDataRowStyle-ForeColor="Red" onpageindexchanging="gvMatchStatus_PageIndexChanging" 
                      Width="100%">
            <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
		<Columns>

            <asp:BoundField DataField="MatchStatusName" HeaderText="Match Status Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-Width="40%" HeaderStyle-Width="25%" ItemStyle-CssClass="grid-column" />
            
			 <asp:TemplateField HeaderText="Sport Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblSportName" runat="server" Text='<%#Eval("SportName") %>' ToolTip=" Sport Name"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>
              <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                               ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                <ItemTemplate>
                    
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" AutoPostBack="true"
                                      OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                            <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                            <asp:ListItem Value="Edit">Edit</asp:ListItem>
                        <asp:ListItem Value="Delete">Delete</asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField ID="hdnMatchStatusID"  runat="server" Value='<%#Eval("MatchStatusId") %>' />
                </ItemTemplate>

            </asp:TemplateField>
            
		  </Columns>
            <PagerStyle CssClass="paging" HorizontalAlign="Center"/>
	    </asp:GridView>    
    </div>
    
   </div>
</asp:Panel>

<asp:Panel ID="pnlEntry" runat="server" Visible="false">

   <div style="padding:10px 0px;">
        * Note: All Fields marked with an asterisk (*) are required.
   </div>
    
	<!-- BEGIN SAMPLE FORM PORTLET-->   
	<div class="portlet box blue tabbable">
		<div class="portlet-title">
			<div class="caption">
				<i class="icon-reorder"></i>
				<span class="hidden-480"> Match Status Detail</span>
			</div>
		</div>

<asp:HiddenField ID="hdn_MatchStatusID" runat="server" />

<div class="portlet-body form">
	<div class="tabbable portlet-tabs">

         <div id="error_div" runat="server" style="display:none;">
            <asp:Label Id="error_msg" runat="server"  Text="" Visible="false"></asp:Label>
        </div>

    <div class="tab-content">
		<div class="tab-pane active" id="portlet_tab1">

        <div class="form-horizontal">

            <div style="width: 100%;margin-top:20px;"></div>
            
             <div ID="divSport" runat="server">
            <div class="control-group">
		         <label class="control-label">
                       <asp:Label ID="lblSport" runat="server" Text=" Sport :" ></asp:Label>
                 </label>
                <div class="startsetallfrom">
                        <span class="help-inline"><font Color="red"><b>*</b></font></span>
                 </div>
                 <div class="controls" style="position:relative;">
                      <asp:DropDownList ID="ddlSport" runat="server" CssClass="medium m-wrap"/>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage=" Sport,"
                                                ControlToValidate="ddlSport" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select Sport Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
                 </div>
            </div>
        </div> 

        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblMatchStatusName" runat="server" Text=" MatchStatus Name :" ></asp:Label>
             </label>
             <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtMatchStatusName" runat="server" CssClass="m-wrap large"/>
                     <asp:RequiredFieldValidator ID="rfvtxtMatchStatusName" runat="server" ErrorMessage="MatchStatusName"  
                                                ControlToValidate="txtMatchStatusName" SetFocusOnError="true" 
                                                ValidationGroup="Sports" Text=" MatchStatus Name Required !" CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="rgvtxtMatchStatusName"
                                                    Display="Static" ControlToValidate="txtMatchStatusName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="cvtxtMatchStatusName" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtMatchStatusName" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>
       
       </div>

       <div class="form-actions">
                    
        <div class="right_div_css">
               <asp:Button id="btnSaveMatchStatus" runat="server" Width="100px" Text="Save" ClientIDMode="Static"
                         onclick="btnSaveMatchStatus_Click" ValidationGroup="Sports" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn blue"/>

             <asp:Button id="btnUpdateMatchStatus" runat="server" Width="100px" Text="Update"  ClientIDMode="Static"
                         onclick="btnUpdateMatchStatus_Click" Visible="false" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn red"  ValidationGroup="Sports"/>        

             <asp:Button id="btnCloseMatchStatus" runat="server" Width="100px"  Text="Cancel" 
                         onclick="btnCloseMatchStatus_Click" CssClass="btn" ClientIDMode="Static" ValidationGroup="CloseSports"
                         OnClientClick="return validateAndConfirmClose(this.id);"/>        

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
