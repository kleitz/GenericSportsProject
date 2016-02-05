<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmDivisionTeams.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmDivisionTeams" %>

<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

    <script type="text/javascript">
        $(document).ready(function () {

            $('.ddlActionSelect').change(function (evt) {
                evt.preventDefault();
                if ($(this).val() == "Delete") {
                    if (confirm('Are you sure you want to delete this Team from Division?')) {
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


  <script type="text/javascript">

      function validateAndConfirmClose(OnlyClose) {
          var validated = Page_ClientValidate('CloseSports');

          if (OnlyClose == "btnTeamCancel") {
              document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Form ?";
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
                          if (OnlyClose == "btnTeamCancel") {
                              <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnTeamCancel))%>;
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

                          if (btn_clientid == "btnTeamSave") {
                              <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnTeamSave))%>;
                          }

                          if (btn_clientid == "btnTeamSaveAndAddTeam") {
                              <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnTeamSaveAndAddTeam))%>;
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
        <img src="<%= Page.ResolveUrl("~/DesktopModules/SportSite/Images/icons/Ok.png")%>" />
         <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" Competition Team save successfully. ">
         </asp:Label>
    </div>

<div id="divcancelmassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
     <img src="<%= Page.ResolveUrl("~/DesktopModules/SportSite/Images/icons/Cancel.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label3" ClientIDMode="Static" runat="server" Text=" Competition Team are delete successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static" style="display:none;">
    <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Division Team ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

<asp:HiddenField runat="server" ID="hfteamID" />
<asp:Panel ID="pnlTeamList" runat="server" >
    
   <asp:Panel ID="addPanel" runat="server">

    <div id="submenu" style="float:left;">
        <ul>
            <li class="active">
                <asp:LinkButton id="btnAddTeam" runat="server" Width="120px" Height="35px"
                                Text=" Add Team " onclick="btnAddTeam_Click" ForeColor="White"/>
            </li>
        </ul>
    </div>

   <div style="position: relative;float: right;padding-top: 15px;margin-right: -7.9px;">
         <asp:Button ID="btnGoToDivision" runat="server"  Text="Back" OnClick="btnGoToDivision_Click" CssClass="btn blue back_btn_Position" />
    </div>
  
    <div style="position:relative;float:right;padding-top:15px;margin-right:10px;">
         <%--<asp:DropDownList ID="ddlCompetitionGroupSearch" runat="server" Width="200px" Height="35px" CssClass="medium m-wrap"
                           onselectedindexchanged="ddlCompetitionGroupSearch_SelectedIndexChanged" 
                           AutoPostBack="true" ></asp:DropDownList> --%>
  
         <asp:DropDownList ID="ddlTeamSearch" runat="server" Width="200px" Height="35px" CssClass="medium m-wrap"
                           onselectedindexchanged="ddlTeamSearch_SelectedIndexChanged" 
                           AutoPostBack="true" ></asp:DropDownList> 
   </div> 

    <asp:Button ID="btnTeamsClearFilter" runat="server" Text="Clear" OnClick="btnTeamsClearFilter_Click" CssClass="btn red clear_btn_psn" Visible="false"/>

    </asp:Panel> 

    <br />
	<div class="portlet box green">
			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480">
                        <asp:Label ID="lbl_Division_Name" runat="server"></asp:Label>
			            - Teams
					</span>
				</div>
                 <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>

        <div class="portlet-body flip-scroll">

        <asp:GridView ID="grid_Teams" runat="server" AutoGenerateColumns="false"  
                      CssClass="table-bordered table-striped table-condensed flip-content" 
                      ShowHeaderWhenEmpty="true" AllowPaging="true" 
                      PageSize="10" EmptyDataText="No Records Found" 
                      EmptyDataRowStyle-ForeColor="Red"  
                      onpageindexchanging="grid_Teams_PageIndexChanging"
                      Width="100%" OnRowDataBound="grid_Teams_OnRowDataBound">
                     
        <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
		<Columns>
            <asp:BoundField DataField="TeamName" HeaderText="Team Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-Width="30%" HeaderStyle-Width="30%" ItemStyle-CssClass="grid-column" />

            <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                               ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                            <asp:ListItem Value="Delete">Delete</asp:ListItem>
                    </asp:DropDownList>
                        <asp:HiddenField ID="hdnDivisionTeamID" runat="server" Value='<%#Eval("DivisionTeamDetailId") %>'></asp:HiddenField>
                        <asp:HiddenField ID="hdnTeamID" runat="server" Value='<%#Eval("TeamId") %>'></asp:HiddenField>
                </ItemTemplate>

                <%--<EditItemTemplate>
                    <asp:Panel ID="pnlEditable" runat="server" Visible="false">
                        <asp:LinkButton ID="lnkupdate" Text="Update" runat="server" OnClick = "OnUpdate" CssClass="grid_row_edit_linkbtn" ToolTip="Save">
                            <img src="<%= Page.ResolveUrl("~/DesktopModules/SportSite/Images/icons/save_icon.png")%>" alt="" width="20" />
                        </asp:LinkButton>
            
                        <asp:LinkButton Text="Cancel" ID="lnkcancel" runat="server" OnClick = "OnCancel" CssClass="grid_row_edit_linkbtn" ToolTip="Cancel">
                            <img src="<%= Page.ResolveUrl("~/DesktopModules/SportSite/Images/icons/cancel_icon.gif")%>" alt="" width="28" />
                        </asp:LinkButton>
                    </asp:Panel>
                </EditItemTemplate>--%>

            </asp:TemplateField>

		</Columns>
            <PagerSettings Mode="NumericFirstLast" PageButtonCount="8" />
            <PagerStyle CssClass="paging" HorizontalAlign="Center"/>
	    </asp:GridView>    

        </div>
    
   </div>

</asp:Panel>

<asp:HiddenField ID="hdn_DivisionTeamDetailID" runat="server" />

<asp:Panel ID="pnlTeamEntry" runat="server" Visible="false">

   <div style="padding:10px 0px;">
            * Note: All Fields marked with an asterisk (*) are required.
   </div>

<div class="portlet box blue tabbable">

			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480">Team List</span>
				</div>
                
			</div>
            
  
<div class="portlet-body form">
    <div class="tabbable portlet-tabs">
        <div class="tab-content" style="margin-top:10px;">
            <div class="tab-pane active" id="portlet_tab1">
                <div class="form-horizontal">
                    <div style="width: 100%;margin-top:20px;"></div>
              
                <div class="control-group">
		            <label class="control-label">
                        <asp:Label ID="lblDivisionName" runat="server" Text="Division :" ></asp:Label>
                    </label>
                    <div class="startsetallfrom">
                        <span class="help-inline"><font Color="red"><b>*</b></font></span>
                    </div>  
                    <div class="controls" style="position:relative;">
                        <asp:TextBox ID="txtDivisionName" runat="server" CssClass="m-wrap large" Enabled="false"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rfvtxtDivisionName" ClientIDMode="Static" runat="server"
                                    ErrorMessage="Division Name," CssClass="errorfordnn" ControlToValidate="txtDivisionName" 
                                    ValidationGroup="Sports"
                                    Text=" Division Name !"></asp:RequiredFieldValidator>
                    </div>
                </div>
        
                <div class="control-group">
		            <label class="control-label">
                        <asp:Label ID="lblGroupName" runat="server" Text="Group Name :" ></asp:Label>
                    </label>
                     <div id="divgnvalidationred" runat="server" class="startsetallfrom">
                        <span class="help-inline"><font Color="red"><b>*</b></font></span>
                    </div>
                    <div class="controls" style="position:relative;">
                        <asp:DropDownList ID="ddlGroupName" runat="server" CssClass="medium m-wrap">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlGroupName" ClientIDMode="Static" runat="server"
                                    ErrorMessage="Group Name," CssClass="errorfordnn" ControlToValidate="ddlGroupName" 
                                    ValidationGroup="Sports" InitialValue="0"
                                    Text=" Group Name !"></asp:RequiredFieldValidator>
                    </div>
                </div>

    
  
<div style="border: 1px solid #35aa47;width:100%;display:block;">

        <asp:Repeater ID="rptrForTeams" runat="server">
                <HeaderTemplate>
                    <table style="width: 100%">
                        <tr style="background-color: #35aa47; color: White; height: 35px;">
                            <th>Select</th>
                            <th>Team Name</th>
                            <th>Select</th>
                             <th>Team Name</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                        <%# (Container.ItemIndex + 2) % 2 == 0 ? "<tr>" : string.Empty%>
                            <td style="padding:10px;text-align:center;">
                                <asp:CheckBox ID="chk_Assign_team" runat="server" />
                            </td>
                            <td style="padding:5px 10px;">
                                <asp:HiddenField ID="hdnTeamID" runat="server" Value='<%#Eval("TeamID") %>'/>
                                 <asp:Label ID="lbl_Team_Name" runat="server" Text='<%#Eval("TeamName") %>'></asp:Label>
                            </td>
                        <%# (Container.ItemIndex + 2) % 2 == 1 ? "</tr>" : string.Empty%>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
       </asp:Repeater>
</div>

<div class="form-actions">

    <div class="right_div_css">
	<asp:Button id="btnTeamSave" runat="server" Text="Save" OnClick="btnTeamSave_Click" ValidationGroup="Sports" 
                CssClass="btn blue"  Width="100px" ClientIDMode="Static" CausesValidation="false"
                OnClientClick="return validateAndConfirm(this.id);"/>

    <asp:Button id="btnTeamSaveAndAddTeam" runat="server" Text="Save & Add Team" OnClick="btnTeamSaveAndAddTeam_Click" 
                ValidationGroup="Sports" CssClass="btn blue" Visible="false" ClientIDMode="Static" CausesValidation="false"
                OnClientClick="return validateAndConfirm(this.id);"/>

    <asp:Button id="btnTeamCancel" runat="server" Text="Cancel" OnClick="btnTeamCancel_Click" CssClass="btn" 
                OnClientClick="return validateAndConfirmClose(this.id);" ClientIDMode="Static"
                ValidationGroup="CloseSports" Width="100px"/>
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