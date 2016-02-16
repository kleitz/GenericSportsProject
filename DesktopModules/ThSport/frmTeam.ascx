<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmTeam.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmTeam" %>

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

    function previewFileLogo() {
        var preview = document.querySelector('#<%=TeamLogoImage.ClientID %>');
        var file = document.querySelector('#<%=TeamLogoFile.ClientID %>').files[0];
        var reader = new FileReader();

        reader.onloadend = function () {
            preview.src = reader.result;
        }

        if (file) {
            if (file.size > 10485760) {
                document.getElementById('span_logo_size_error').style.display = "block";
                preview.src = "";
            }
            reader.readAsDataURL(file);
        }
        else {
            preview.src = "";
        }
    }

    function previewFilePhoto() {
        var preview = document.querySelector('#<%=TeamPhotoImage.ClientID %>');
        var file = document.querySelector('#<%=TeamPhotoFile.ClientID %>').files[0];
        var reader = new FileReader();

        reader.onloadend = function () {
            preview.src = reader.result;
        }

        if (file) {
            if (file.size > 10485760) {
                document.getElementById('span_photo_size_error').style.display = "block";
                preview.src = "";
            }
            reader.readAsDataURL(file);
        }
        else {
            preview.src = "";
        }
    }

    function SaveSuccessfully()
    {
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
    function savevalidateAndConfirmClose()
    {
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

        if (OnlyClose == "btnCancelTeam") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Team Form ?";
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

                        if (OnlyClose == "btnCancelTeam") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCancelTeam))%>;
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

         if (btn_clientid == "btnUpdateTeam") {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Team Details ?";
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

                         if (btn_clientid == "btnSaveTeam") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveTeam))%>;
                         }

                         if (btn_clientid == "btnUpdateTeam") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateTeam))%>;
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
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" Team detail are save successfully. ">
     </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" Team detail are update successfully. ">
     </asp:Label>
</div>

<div id="divcancelmassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Cancel.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label3" ClientIDMode="Static" runat="server" Text=" Team detail are delete successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
    <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Team Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

   <asp:Panel id="pnlTeamGrid" runat="server">

    <asp:Panel ID="addPanel" runat="server">    
        <div id="submenu">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddTeam" runat="server" 
                                    Height="35px" Text=" Add Team " 
                                    onclick="btnAddTeam_Click" ForeColor="White"/>
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
					<span class="hidden-480"> Team  List</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
			

    <div class="portlet-body flip-scroll">
		
          <asp:GridView ID="gvTeam" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" 
                          AllowPaging="true" PageSize="10"
                          EmptyDataText="No Records Found" 
                          EmptyDataRowStyle-ForeColor="Red" 
                        CssClass="table-bordered table-striped table-condensed flip-content" 
                        HorizontalAlign="Center" AlternatingRowStyle-Font-Size="X-Large" 
                        CellPadding="5" CellSpacing="5" Width="100%"
                        onpageindexchanging="gvTeam_PageIndexChanging">
            <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />

		<Columns>

            <asp:TemplateField HeaderText="Team" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblTeamName" runat="server" Text='<%#Eval("TeamName") %>' ToolTip="Team "></asp:Label>
                    </div> 
                    <asp:HiddenField ID="hdn_Team_Id" runat="server" Value='<%#Eval("TeamId") %>'></asp:HiddenField>
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Division" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblDivisionName" runat="server" Text='<%#Eval("DivisionName") %>' ToolTip="Division "></asp:Label>
                    </div> 
                    <asp:HiddenField ID="hdn_Division_Id" runat="server" Value='<%#Eval("DivisionId") %>'></asp:HiddenField>
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Abbreviation" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblTeamAbbr" runat="server" Text='<%#Eval("TeamAbbr") %>' ToolTip="Team League"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Club" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" Visible="false" ItemStyle-Width="20%">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblClub" runat="server" Text='<%#Eval("ClubName") %>' ToolTip="Sport"></asp:Label>
                    </div> 
                    <asp:HiddenField ID="hdnClubID" runat="server" Value='<%#Eval("ClubID") %>' />
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Sport" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblSport" runat="server" Text='<%#Eval("SportName") %>' ToolTip="Sport"></asp:Label>
                    </div> 
                    <asp:HiddenField ID="hdnSportID" runat="server" Value='<%#Eval("SportID") %>' />
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Famous Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" Visible="false">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblFamousName" runat="server" Text='<%#Eval("TeamFamousName") %>' ToolTip="Famous Name"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" Visible="false">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblTeamDesc" runat="server" Text='<%#Eval("TeamDesc") %>' ToolTip="Description"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Established Year" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblTeamEstablishedYear" runat="server" Text='<%#Eval("TeamEstablishedYear","{0:dd-MMM-yyyy}") %>' ToolTip="Established Year"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

             <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                               ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" 
                                      OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                            <asp:ListItem Value="Edit">Edit</asp:ListItem>
                            <asp:ListItem Value="Player">Add Player</asp:ListItem>
                            <asp:ListItem Value="Member">Add Member</asp:ListItem>
                            <asp:ListItem Value="Delete">Delete</asp:ListItem>
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

<asp:Panel ID="pnlTeamEntry" runat="server">

    <div style="padding:10px 0px;">
            * Note: All Fields marked with an asterisk (*) are required.
    </div>

    <div class="portlet box blue tabbable">
			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480"> Team  Details</span>
				</div>
			</div>

    <div class="portlet-body form">

	<div class="tabbable portlet-tabs">

          <div id="error_div" runat="server" style="display:none;">
            <asp:Label Id="error_msg" runat="server"  Text="" Visible="false"></asp:Label>
        </div>

    <div class="tab-content">
		<div class="tab-pane active" id="portlet_tab1">
             <asp:HiddenField ID="hdnTeamID" runat="server" />
        <div class="form-horizontal">

        <div style="width: 100%;margin-top:20px;"></div>

       

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblClub" runat="server" Text="Club :" ></asp:Label>
            </label>
             <div class="startsetallfrom">
             <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
            <div class="controls" style="position:relative;">  
                <asp:DropDownList ID="ddlClub" runat="server"  CssClass="large m-wrap"/>
                <asp:RequiredFieldValidator ID="rfvddlClub" ClientIDMode="Static" runat="server" InitialValue="0" 
                                                      ErrorMessage="Club Required !" CssClass="errorfordnn" SetFocusOnError="true" ControlToValidate="ddlClub"
                                                      ValidationGroup="Sports" Text="Club Required !"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="control-group">
		   <label class="control-label">
                <asp:Label ID="lblSport" runat="server" Text="Sport :" ></asp:Label>
            </label>
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
		   <label class="control-label">Division : </label>
            <div class="startsetallfrom">
             <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
                <div class="controls" style="position:relative;">  
                <asp:DropDownList ID="ddlDivision" runat="server"  CssClass="large m-wrap"/>
               
                <asp:RequiredFieldValidator ID="rfvddlDivision" ClientIDMode="Static" runat="server" InitialValue="0" 
                    ErrorMessage="Division Required !" CssClass="errorfordnn" SetFocusOnError="true" ControlToValidate="ddlDivision"
                    ValidationGroup="Sports" Text="Division Required !"></asp:RequiredFieldValidator>
                </div>
        </div>
       
       <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblTeam" runat="server" Text=" Team  :" ></asp:Label>
             </label>
           <div class="startsetallfrom">
             <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
               <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtTeam" runat="server"  CssClass="m-wrap large" />
                  <asp:RequiredFieldValidator ID="rfvTeam" runat="server" ErrorMessage="Team"  ControlToValidate="txtTeam" SetFocusOnError="true" 
                                              ValidationGroup="Sports" Text="Team  Required !" CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="rgvtxtTeam"
                                                    Display="Static" ControlToValidate="txtTeam"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="cvtxtTeam" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtTeam" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblTeamAbbr" runat="server" Text="Abbreviation :" ></asp:Label>
            </label>
            <div class="startsetallfrom">
            <span class="help-inline"><font Color="red"><b>*</b></font></span>
                </div>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtTeamAbbr" runat="server"  CssClass="m-wrap small" />
                
                    <asp:RegularExpressionValidator ID="rgvtxtTeamAbbr"
                                                    Display="Static" ControlToValidate="txtTeamAbbr"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,5}$" 
                                                    runat="server" ErrorMessage="Maximum 5 characters allowed.">
                    </asp:RegularExpressionValidator>  
                <asp:CustomValidator ID="cvtxtTeamAbbr" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" ControlToValidate="txtTeamAbbr"
                                    EnableClientScript="true" ClientValidationFunction="validateTextBox" CssClass="errorfordnn" Text="First Character Should Not Be Special Character"></asp:CustomValidator>
           </div>
        </div>
    
        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblTeamDesc" runat="server" Text="Description :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtTeamDesc" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px"/>
                    <asp:RegularExpressionValidator ID="rgvtxtTeamDesc"
                                                    Display="Static" ControlToValidate="txtTeamDesc"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,200}$" 
                                                    runat="server" ErrorMessage="Maximum 200 characters allowed.">
                    </asp:RegularExpressionValidator>  
                <asp:CustomValidator ID="cvtxtTeamDesc" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" ControlToValidate="txtTeamDesc"
                                    EnableClientScript="true" ClientValidationFunction="validateTextBox" CssClass="errorfordnn" Text="First Character Should Not Be Special Character"></asp:CustomValidator>
           </div>
        </div>

        <div class="control-group">
            <label class="control-label">          
                   <asp:Label ID="lblFamousName" runat="server" Text=" Famous Name :" ></asp:Label>
             </label>
            <div class="startsetallfrom">
             <span class="help-inline"><font Color="red"><b>*</b></font></span>
		     </div>
                <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtFamousName" runat="server"  CssClass="m-wrap large" />
                  <asp:RequiredFieldValidator ID="rfvtxtFamousName" runat="server" ErrorMessage="Team"  ControlToValidate="txtFamousName" SetFocusOnError="true" 
                                              ValidationGroup="Sports" Text="Famous Name  Required !" CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="rgvtxtFamousName"
                                                    Display="Static" ControlToValidate="txtFamousName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="cvtxtFamousName" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" ControlToValidate="txtFamousName"
                                    EnableClientScript="true" ClientValidationFunction="validateTextBox" CssClass="errorfordnn" Text="First Character Should Not Be Special Character"></asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
            <label class="control-label">          
                   <asp:Label ID="lblTeamLogoName" runat="server" Text=" Team Logo Name :" ></asp:Label>
             </label>
            <div class="startsetallfrom">
             <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
            <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtTeamLogoName" runat="server"  CssClass="m-wrap large" />
                  <asp:RequiredFieldValidator ID="rfvtxtTeamLogoName" runat="server" ErrorMessage="Team"  ControlToValidate="txtTeamLogoName" SetFocusOnError="true" 
                                              ValidationGroup="Sports" Text="Team Logo Name Required !" CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="rgvtxtTeamLogoName"
                                                    Display="Static" ControlToValidate="txtTeamLogoName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="cvtxtTeamLogoName" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" ControlToValidate="txtTeamLogoName"
                                    EnableClientScript="true" ClientValidationFunction="validateTextBox" CssClass="errorfordnn" Text="First Character Should Not Be Special Character"></asp:CustomValidator>
             </div>
        </div>
        
        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblUploadLogo" runat="server" Text="Upload Logo : "></asp:Label>
             </label>
            <div class="startsetallfrom">
              <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
                <div class="controls" style="position:relative;">  
                <input ID="TeamLogoFile" type="file" name="file" runat="server" onchange="previewFileLogo()"/>
                <asp:RegularExpressionValidator ID="rgvTeamLogoFile" 
                                                ValidationExpression="([a-zA-Z\\].*(.jpg|.png|.bmp|.jpeg|.gif|.tif)$)"
                                                ControlToValidate="TeamLogoFile" ValidationGroup="Sports" 
                                                runat="server"  
                                                ErrorMessage="Please choose only .jpg, .jpeg, .png and .gif images!"
                                                CssClass ="errorfordnn" />
                <span id="span_logo_size_error" style="display:none;"><font style="color:red;">Can Not Upload Logo Larger Than 10 MB</font></span> 
                <div style="padding-top:10px;border:none; Width:200px;">
                    <asp:Image ID="TeamLogoImage" runat="server" />
                </div>
            </div>
        </div>

        <div class="control-group">
             <label class="control-label"> 
                <asp:Label ID="lblUploadPhoto" runat="server" Text="Upload Photo : "></asp:Label>
             </label>
            <div class="startsetallfrom">
              <span class="help-inline"><font Color="red"><b>*</b></font></span>
		    </div>
                <div class="controls" style="position:relative;">  
                <input ID="TeamPhotoFile" type="file" name="file" runat="server" onchange="previewFilePhoto()"/>
                <asp:RegularExpressionValidator ID="rgvTeamPhotoFile" 
                                                ValidationExpression="([a-zA-Z\\].*(.jpg|.png|.bmp|.jpeg|.gif|.tif)$)"
                                                ControlToValidate="TeamPhotoFile" ValidationGroup="Sports" 
                                                runat="server"  
                                                ErrorMessage="Please choose only .jpg, .jpeg, .png and .gif images!"
                                                CssClass ="errorfordnn" />
                <span id="span_photo_size_error" style="display:none;"><font style="color:red;">Can Not Upload Logo Larger Than 10 MB</font></span> 
                <div style="padding-top:10px;border:none; Width:200px;">
                    <asp:Image ID="TeamPhotoImage" runat="server" />
                </div>
            </div>
        </div>

        <div class="control-group">
            <label class="control-label"> 
                <asp:Label ID="lblEstablishedYear" runat="server" Text="Established Year : "></asp:Label>
             </label>
            <div class="startsetallfrom">
              <span class="help-inline"><font Color="red"><b>*</b></font></span>
		    </div>
                <div class="controls" style="position:relative;">  
                <asp:TextBox ID="txtEstablishedYear" runat="server" ClientIDMode="Static"  CssClass="datetimepicker m-wrap medium"/>
                <asp:RequiredFieldValidator ID="rfvtxtEstablishedYear" runat="server" ErrorMessage="Enter Start Date"
                                                 ControlToValidate="txtEstablishedYear" SetFocusOnError="true" 
                                                 ValidationGroup="Sports" Text="Established Year Required !" CssClass="errorfordnn" 
                                                 ClientIDMode="Static"/>
            </div>
        </div>

        <div class="control-group">
              <label class="control-label"> 
                <asp:Label ID="lblUploadAnthemAudioFile" runat="server" Text="Upload Anthem Audio File : "></asp:Label>
             </label>
		    <div class="controls" style="position:relative;">  
                <input ID="TeamAnthemAudioFile" type="file" name="file" runat="server"/>
                <asp:RegularExpressionValidator ID="rgvTeamAnthemAudioFile" 
                    ValidationExpression="([a-zA-Z\\].*(.mp3|.MP3|.mpeg|.MPEG|.m3u|.M3U)$)"
                    ControlToValidate="TeamAnthemAudioFile" ValidationGroup="Sports" 
                    runat="server"  
                    ErrorMessage="Please choose only .mp3,..peg,.m3u Audio File !"
                    CssClass ="errorfordnn" />
                <asp:Label ID="lblAudioFile" runat="server"></asp:Label>
            </div>
        </div>

         <div class="control-group">
		    <label class="control-label">Is Active :</label>
            <div class="controls">
                <label class="checkbox"> 
                    <asp:CheckBox ID="ChkIsActive" runat="server" />
                </label>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">Is Show:</label>
            <div class="controls">
                <label class="checkbox"> 
                    <asp:CheckBox ID="ChkIsShow" runat="server" />
                </label>
             </div>
        </div>    

   </div>

        <div class="form-actions">
            <div class="right_div_css">

                    <asp:Button ID="btnSaveTeam" runat="server"  Text=" Save " OnClick="btnSaveTeam_Click" 
                                ValidationGroup="Sports" CssClass="btn blue" ClientIDMode="Static" Width="100px"
                                OnClientClick="return validateAndConfirm(this.id);" />

                    <asp:Button ID="btnUpdateTeam" runat="server"  Text=" Update " OnClick="btnUpdateTeam_Click" 
                                ValidationGroup="Sports" CssClass="btn red" ClientIDMode="Static" Width="100px"
                                OnClientClick="return validateAndConfirm(this.id);" />

                    <asp:Button ID="btnCancelTeam" runat="server" Text="Cancel" OnClick="btnCancelTeam_Click" CssClass="btn" 
                                ClientIDMode="Static" ValidationGroup="CloseSports" Width="100px"
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

