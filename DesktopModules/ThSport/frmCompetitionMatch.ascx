<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmCompetitionMatch.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmCompetitionMatch" %>

<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

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
     function validateAndConfirmClose(OnlyClose) {
         var validated = Page_ClientValidate('CloseSports');

         if (OnlyClose == "btnCancelCompetitionMatch") {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close CompetitionMatch Form ?";
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

                         if (OnlyClose == "btnCancelCompetitionMatch") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCancelCompetitionMatch))%>;
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

         if (btn_clientid == "btnUpdateCompetitionMatch") {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update CompetitionMatch Details ?";
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

                         if (btn_clientid == "btnSaveCompetitionMatch") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveCompetitionMatch))%>;
                         }

                         if (btn_clientid == "btnUpdateCompetitionMatch") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateCompetitionMatch))%>;
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

<div id="divsavemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" CompetitionMatch detail are save successfully. ">
     </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" CompetitionMatch detail are update successfully. ">
     </asp:Label>
</div>

<div id="divcancelmassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Cancel.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label3" ClientIDMode="Static" runat="server" Text=" CompetitionMatch detail are delete successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
    <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save CompetitionMatch Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

   <asp:Panel id="pnlCompetitionMatchGrid" runat="server">

    <asp:Panel ID="addPanel" runat="server">    
        <div id="submenu">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddCompetitionMatch" runat="server" 
                                    Height="35px" Text=" Add CompetitionMatch" 
                                    onclick="btnAddCompetitionMatch_Click" ForeColor="White"/>
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
					<span class="hidden-480">Competition Match List</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
			

    <div class="portlet-body flip-scroll">
		
          <asp:GridView ID="gvCompetitionMatch" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" 
                        AllowPaging="true" PageSize="10" EmptyDataText="No Records Found" EmptyDataRowStyle-ForeColor="Red" 
                        CssClass="table-bordered table-striped table-condensed flip-content" 
                        HorizontalAlign="Center" AlternatingRowStyle-Font-Size="X-Large" 
                        CellPadding="5" CellSpacing="5" Width="100%"
                        onpageindexchanging="gvCompetitionMatch_PageIndexChanging">
            <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />

		<Columns>
       

            
            <asp:TemplateField HeaderText="Competition Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="170px">
                <ItemTemplate>
                 <asp:HiddenField ID="hdn_CompetitionMatch_Id" runat="server" Value='<%#Eval("MatchId") %>'></asp:HiddenField>
                    <asp:HiddenField ID="hfComp_ID" runat="server" Value='<%#Eval("CompetitionId") %>'></asp:HiddenField>
                <div class="grid-cell-inner" style="width:170px; display: inline-block;">
                   <asp:Label ID="lblCompetitionID" runat="server" Text='<%#Eval("CompetitionName") %>' ToolTip="Competition Name"></asp:Label>
                </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Location Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="111px" >
                <ItemTemplate>
                    <div style="text-align:center;">    
                    <asp:HiddenField ID="hfLoc_ID" runat="server" Value='<%#Eval("Loc_LocationID") %>'></asp:HiddenField>
                    <div class="grid-cell-inner" style="width:100px; display: inline-block;">
                        <asp:Label ID="lblLocationID" runat="server" Text='<%#Eval("Loc_LocationName") %>' ToolTip="Match Location "></asp:Label>
                    </div> 
                   </div>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Start-Date" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="111px" >
                <ItemTemplate>
                <div class="grid-cell-inner" style="width:111px; display: inline-block;">
                    <asp:Label ID="lblStartDate" runat="server" Text='<%#Eval("StartDateTime") %>' ToolTip="Match Start-Date"></asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="End-Date" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="111px" >
                <ItemTemplate>
                <div class="grid-cell-inner" style="width:111px; display: inline-block;">
                    <asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("EndDateTime") %>' ToolTip="Match End-Date"></asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Team A" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="111px"  >
                <ItemTemplate>
                 <div style="text-align:center;">
                    <asp:HiddenField ID="hfTeam_ID" runat="server" Value='<%#Eval("TeamID") %>'></asp:HiddenField>
                    <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                        <asp:Label ID="lblTeamAID" runat="server" Text='<%#Eval("TeamName") %>' ToolTip="Team A Name"></asp:Label>
                    </div> 
                </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Team B" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="111px" >
                <ItemTemplate>
                 <div style="text-align:center;">  
                    <%-- <asp:HiddenField ID="hdmatchstatus" runat="server" Value='<%#Eval("MatchStatus") %>'></asp:HiddenField> --%> 
                    <asp:HiddenField ID="hfTeam_ID1" runat="server" Value='<%#Eval("TeamID1") %>'></asp:HiddenField>
                    <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                        <asp:Label ID="lblTeamBID" runat="server" Text='<%#Eval("TeamName1") %>' ToolTip="Team B Name"></asp:Label>
                    </div> 
                     </div>
                </ItemTemplate>
            </asp:TemplateField>


<%--            <asp:TemplateField HeaderText="Abbreviation" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblTeamA" runat="server" Text='<%#Eval("TeamA") %>' ToolTip="Abbreviation"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="No of Teams" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lbl" runat="server" Text='<%#Eval("NumberofTeams") %>' ToolTip="No of Teams"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>--%>

             <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                               ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" 
                                      OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                            <asp:ListItem Value="Edit">Edit</asp:ListItem>
                           <%-- <asp:ListItem Value="Edit">Delete</asp:ListItem>--%>
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

   <asp:Panel ID="pnlCompetitionMatchEntry" runat="server" Visible="false">

        <div style="padding:10px 0px;">
                * Note: All Fields marked with an asterisk (*) are required.
        </div>

        <div class="portlet box blue tabbable">
			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480"> Competition Match Details</span>
				</div>
			</div>

            <div class="portlet-body form">

	            <div class="tabbable portlet-tabs">

                    <div class="tab-content" style="margin-top:10px !important;">
		                <div class="tab-pane active" id="portlet_tab1">

                            <div class="form-horizontal">

                            <div style="width: 100%;margin-top:20px;"></div>

                            <asp:HiddenField ID="hdnCompetitionMatchID" runat="server" />
                                 <asp:HiddenField ID="hdnTeamList" runat="server" />
                            <%--<div class="control-group">
		                         <label class="control-label">
                                       <asp:Label ID="lblCompetition" runat="server" Text="  Competition :" ></asp:Label>
                                 </label>
                                <div class="startsetallfrom">
                                    <span class="help-inline"><font Color="red"><b>*</b></font></span>
                                </div>
                                 <div class="controls" style="position:relative;">
                                      <asp:DropDownList ID="ddlCompetition" runat="server" CssClass="medium m-wrap"/>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Competition"  
                                                ControlToValidate="ddlCompetition" SetFocusOnError="true" InitialValue="0" 
                                                ValidationGroup="Sports" Text=" Sponsor Level Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
                                 </div>
                            </div>--%>

                            <div class="control-group">
		                         <label class="control-label">
                                       <asp:Label ID="lblLocation" runat="server" Text="  Location :" ></asp:Label>
                                 </label>
                                <div class="startsetallfrom">
                                    <span class="help-inline"><font Color="red"><b>*</b></font></span>
                                </div>
                                 <div class="controls" style="position:relative;">
                                      <asp:DropDownList ID="ddlLocation" runat="server" CssClass="medium m-wrap"/>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Location"  
                                                ControlToValidate="ddlLocation" SetFocusOnError="true" InitialValue="0" 
                                                ValidationGroup="Sports" Text=" Location Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
                                 </div>
                            </div>

                            <div class="control-group">
	                            <label class="control-label"> 
                                    <asp:Label ID="lblSponsorStartDate" runat="server" Text="Start Date :" ></asp:Label>
                                </label>
                                <div class="startsetallfrom">
                                    <span class="help-inline"><font Color="red"><b>*</b></font></span>
                                </div>
                                <div class="controls" style="position:relative;">  
                                    <asp:TextBox ID="txtMatchStartDate" runat="server" CssClass="datetimepicker m-wrap medium onlynumeric"/>
                                    <asp:RequiredFieldValidator ID="rfvtxtMatchStartDate" runat="server" ErrorMessage="Enter Start Date"
                                                                        ControlToValidate="txtMatchStartDate" SetFocusOnError="true" 
                                                                        ValidationGroup="Sports" Text="Start Date Required !" CssClass="errorfordnn" 
                                                                        ClientIDMode="Static"/>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                                                    Display="Static" ControlToValidate="txtMatchStartDate"  
                                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                                    ValidationExpression = "^[\s\S]{0,25}$" 
                                                                    runat="server" ErrorMessage="Maximum 25 characters allowed.">
                                    </asp:RegularExpressionValidator>  
                                    <asp:CustomValidator ID="CustomValidator1" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                                        ControlToValidate="txtMatchStartDate" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                                        CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                                        </asp:CustomValidator>
                                </div>
                            </div>

                            <div class="control-group">
		                        <label class="control-label"> 
                                    <asp:Label ID="lblMatchEndDate" runat="server" Text="End Date :"></asp:Label>
                                 </label>
                                 <div class="startsetallfrom">
                                    <span class="help-inline"><font Color="red"><b>*</b></font></span>
                                 </div>
                                 <div class="controls" style="position:relative;">   
                                     <asp:TextBox ID="txtMatchEndDate" runat="server" CssClass="enddatetimepicker m-wrap medium"/>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter End Date"
                                                                     ControlToValidate="txtMatchEndDate" SetFocusOnError="true" 
                                                                     ValidationGroup="Sports" Text="End Date Required !" CssClass="errorfordnn" 
                                                                     ClientIDMode="Static"/>
                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                                                     Display="Static" ControlToValidate="txtMatchEndDate"  
                                                                     ValidationGroup="Sports" CssClass="errorfordnn"
                                                                     ValidationExpression = "^[\s\S]{0,25}$" 
                                                                     runat="server" ErrorMessage="Maximum 25 characters allowed.">
                                     </asp:RegularExpressionValidator>   
                                     <asp:CustomValidator ID="CustomValidator2" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                                     ControlToValidate="txtMatchEndDate" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                                     CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                                       </asp:CustomValidator>
                                    <%-- <asp:CompareValidator ID="CompareValidator" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                                     ControlToValidate="txtMatchEndDate" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                                     CssClass="errorfordnn" Text="EndDate Should be Greater Then StartDate" Operator="GreaterThan" ValueToCompare="txtMatchStartDate">
                                       </asp:CompareValidator>--%>
                                 </div> 
                            </div>

                            <div class="control-group">
		                         <label class="control-label">
                                       <asp:Label ID="lblTeamA" runat="server" Text="  Team A :" ></asp:Label>
                                 </label>
                                <div class="startsetallfrom">
                                    <span class="help-inline"><font Color="red"><b>*</b></font></span>
                                </div>
                                 <div class="controls" style="position:relative;">
                                      <asp:DropDownList ID="ddlTeamA" runat="server" CssClass="medium m-wrap" OnSelectedIndexChanged="ddlTeamA_SelectedIndexChanged" AutoPostBack="true"/>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Team A"  
                                                ControlToValidate="ddlTeamA" SetFocusOnError="true" InitialValue="0" 
                                                ValidationGroup="Sports" Text="  Team A  Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
                                 </div>
                            </div>

                            <div class="control-group">
		                         <label class="control-label">
                                       <asp:Label ID="lblTeamB" runat="server" Text="  Team B :" ></asp:Label>
                                 </label>
                                <div class="startsetallfrom">
                                    <span class="help-inline"><font Color="red"><b>*</b></font></span>
                                </div>
                                 <div class="controls" style="position:relative;">
                                      <asp:DropDownList ID="ddlTeamB" runat="server" CssClass="medium m-wrap"/>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Team B"  
                                                ControlToValidate="ddlTeamB" SetFocusOnError="true" InitialValue="0" 
                                                ValidationGroup="Sports" Text=" Team B Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
                                 </div>
                            </div>   
                                
                                <div class="control-group">
		                         <label class="control-label">
                                       <asp:Label ID="lblMatchStatus" runat="server" Text="  MatchStatus :" ></asp:Label>
                                 </label>
                                    <div class="startsetallfrom">
                                    <span class="help-inline"><font Color="red"><b>*</b></font></span>
                                </div>
                                 <div class="controls" style="position:relative;">
                                      <asp:DropDownList ID="ddlMatchStatus" runat="server" CssClass="medium m-wrap"/>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="MatchStatus"  
                                                ControlToValidate="ddlMatchStatus" SetFocusOnError="true" InitialValue="0" 
                                                ValidationGroup="Sports" Text=" Match Status Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
                                 </div>
                            </div>   
                                
                                <div class="control-group">
		                         <label class="control-label">
                                       <asp:Label ID="lblMatchType" runat="server" Text="  MatchType :" ></asp:Label>
                                 </label>
                                    <div class="startsetallfrom">
                                    <span class="help-inline"><font Color="red"><b>*</b></font></span>
                                </div>
                                 <div class="controls" style="position:relative;">
                                      <asp:DropDownList ID="ddlMatchType" runat="server" CssClass="medium m-wrap"/>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="MatchType"  
                                                ControlToValidate="ddlMatchType" SetFocusOnError="true" InitialValue="0" 
                                                ValidationGroup="Sports" Text=" Match Type Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
                                 </div>
                            </div>      

                            <div class="control-group">
		                        <label class="control-label">
                                    <asp:Label ID="lblIsFinalized" runat="server" Text=" IsFinalized :"></asp:Label>
                                </label>
                                <div class="controls" style="margin-top:8px;">
                                <div id="checdivshow" runat="server" class="SingleCheckbox col-left">
                                    <asp:CheckBox ID="ChkIsFinalized" runat="server" />
                                    <asp:Label ID="lblChkIsFinalized" AssociatedControlID="ChkIsFinalized" runat="server" Text="" CssClass="CheckBoxLabel">
                                    </asp:Label>
                                </div>
                                </div>
                            </div>  
                            <div class="form-actions">
                                <div class="right_div_css">

                                        <asp:Button ID="btnSaveCompetitionMatch" runat="server"  Text=" Save " OnClick="btnSaveCompetitionMatch_Click" 
                                                    ValidationGroup="Sports" CssClass="btn blue" ClientIDMode="Static" Width="100px"
                                                    OnClientClick="return validateAndConfirm(this.id);" Visible="false"/>

                                        <asp:Button ID="btnUpdateCompetitionMatch" runat="server"  Text=" Update " OnClick="btnUpdateCompetitionMatch_Click" 
                                                    ValidationGroup="Sports" CssClass="btn red" ClientIDMode="Static" Width="100px"
                                                    OnClientClick="return validateAndConfirm(this.id);" Visible="false"/>

                                        <asp:Button ID="btnCancelCompetitionMatch" runat="server" Text="Cancel" OnClick="btnCancelCompetitionMatch_Click" CssClass="btn" 
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