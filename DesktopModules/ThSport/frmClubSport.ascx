<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmClubSport.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmClubSport" %>

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
         //var validated = Page_ClientValidate('CloseSports');

         if (OnlyClose == "btnCancelClubSport") {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close ClubSport Form ?";
         }

         //if (validated) {
             $("#dialogBox").dialog({

                 modal: true,
                 resizable: true,
                 draggable: true,
                 closeOnEscape: true,
                 position: ['center', 80],
                 dialogClass: "dnnFormPopup",

                 buttons: {
                     Ok: function () {

                         if (OnlyClose == "btnCancelClubSport") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCancelClubSport))%>;
                         }

                     },
                     Cancel: function () {
                         $(this).dialog('close');
                         return false;
                     }
                 }

             });

         //}
         return false;
     }

     function validateAndConfirm(btn_clientid) {
         //alert(btn_clientid);
         //var validated = Page_ClientValidate('Sports');
      
         //console.log("dgfshdghdf");
         if (btn_clientid == "btnUpdateClubSport") {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update ClubSport Details ?";
         }

         //if (validated) {
             $("#dialogBox").dialog({

                 modal: true,
                 resizable: true,
                 draggable: true,
                 closeOnEscape: true,
                 position: ['center', 80],
                 dialogClass: "dnnFormPopup",

                 buttons: {
                     Ok: function () {

                         if (btn_clientid == "btnSaveClubSport") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveClubSport))%>;
                         }

                         if (btn_clientid == "btnUpdateClubSport") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateClubSport))%>;
                         }

                     },
                     Cancel: function () {
                         $(this).dialog('close');
                         return false;
                     }
                 }

             });

        //}
         return false;
     }

     $(document).ready(function () {
         //Reset drop down list
         $(".ddlActionSelect > option:first").attr("selected", "selected");
     });

</script>

<script type="text/javascript">
    $(document).ready(function () {

        $('.ddlActionSelect').change(function (evt) {
            evt.preventDefault();
            if ($(this).val() == "Delete") {
                if (confirm('Are you sure you want to delete this Team from Competition?')) {
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

<div id="divsavemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" ClubSport detail are save successfully. ">
     </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" ClubSport detail are update successfully. ">
     </asp:Label>
</div>

<div id="divcancelmassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Cancel.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label3" ClientIDMode="Static" runat="server" Text=" ClubSport detail are delete successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
    <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save ClubSport Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">
       <asp:Panel id="pnlClubSportGrid" runat="server">

            <asp:Panel ID="addPanel" runat="server">    
                <div id="submenu">
                    <ul>
                        <li class="active">
                            <asp:LinkButton ID="btnAddClubSport" runat="server" 
                                            Height="35px" Text=" Add Club Sport " 
                                            onclick="btnAddClubSport_Click" ForeColor="White"/>
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
					        <span class="hidden-480"> <asp:Label ID="lbl_Club_Name" runat="server"></asp:Label> Club Sport List</span>
				        </div>
                        <div class="tools">
					        <a href="javascript:;" class="collapse"></a>
                        </div>
			        </div>
			

                <div class="portlet-body flip-scroll">
		
                          <asp:GridView ID="gvClubSport" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" 
                                        AllowPaging="true" PageSize="10" EmptyDataText="No Records Found" 
                                        CssClass="table-bordered table-striped table-condensed flip-content" 
                                        HorizontalAlign="Center" AlternatingRowStyle-Font-Size="X-Large" 
                                        CellPadding="5" CellSpacing="5" Width="100%"
                                        onpageindexchanging="gvClubSport_PageIndexChanging">
                            <RowStyle CssClass="grid-row" />
                        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />

		                <Columns>

        	                <%--<asp:TemplateField HeaderText="Club Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				                <ItemTemplate>
                                    <div class="grid-cell-inner" style="width:364px; display: inline-block;">
					                    <asp:Label ID="lblClubName" runat="server" Text='<%#Eval("ClubName") %>' ToolTip="Club Name"></asp:Label>
                                    </div> 
				                </ItemTemplate>
			                </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Club Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%">
				                <ItemTemplate>
                                    <div class="grid-cell-inner" style="text-align:center;">
					                    <asp:Label ID="lblOwnerName" runat="server" Text='<%#Eval("ClubName") %>' ToolTip="Club Owner Name"></asp:Label>
                                    </div> 
				                </ItemTemplate>
			                </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sport" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%">
				                <ItemTemplate>
                                    <div class="grid-cell-inner" style="text-align:center;">
					                    <asp:Label ID="lblOwnerPercentage" runat="server" Text='<%#Eval("SportName") %>' ToolTip="Club Owner Percentage"></asp:Label>
                                    </div> 
				                </ItemTemplate>
			                </asp:TemplateField>

                             <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                                               ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" 
                                                      OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                                            <asp:ListItem Value="Delete">Delete</asp:ListItem>
                                            <%--<asp:ListItem Value="Delete">Delete</asp:ListItem>--%>
                            
                                    </asp:DropDownList>
                                        <asp:Label ID="lblddlActionClubSportsId" runat="server" Text='<%#Eval("ClubSportsId") %>' Visible="false">
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

        </asp:Panel>

<asp:Panel ID="pnlClubSportEntry" runat="server">

    <div style="padding:10px 0px;">
            * Note: All Fields marked with an asterisk (*) are required.
    </div>

    <div class="portlet box blue tabbable">
	    <div class="portlet-title">
				    <div class="caption">
					    <i class="icon-reorder"></i>
					    <span class="hidden-480">Club Sport Detail</span>
				    </div>
			    </div>

        <div class="portlet-body form">

	       <div class="tabbable portlet-tabs">

                <div class="tab-content" style="margin-top:10px !important;">
		            <div class="tab-pane active" id="portlet_tab1">

                        <div class="form-horizontal">

                            <div style="width: 100%;margin-top:20px;"></div>

                            <div class="control-group">
		                        <label class="control-label">
                                    <asp:Label ID="lblClubSport" runat="server" Text=" Club Name :" ></asp:Label>
		                        </label>
                                <asp:TextBox ID="txtClubName" runat="server"   CssClass="m-wrap large" Enabled="false"/>
           
                            </div>

                            <div style="border: 1px solid #35aa47;width:100%;display:block;">

                                <asp:Repeater ID="rptrForSports" runat="server">
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr style="background-color: #35aa47; color: White; height: 35px;">
                                                    <th>Select</th>
                                                    <th>Sport</th>
                                                    <th>Select</th>
                                                        <th>Sport</th>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                                <%# (Container.ItemIndex + 2) % 2 == 0 ? "<tr>" : string.Empty%>
                                                    <td style="padding:10px;text-align:center;">
                                                        <asp:CheckBox ID="chk_Assign_sport" runat="server" />
                                                    </td>
                                                    <td style="padding:5px 10px;">
                                                        <asp:HiddenField ID="hdnSportID" runat="server" Value='<%#Eval("SportID") %>'/>
                                                            <asp:Label ID="lbSport" runat="server" Text='<%#Eval("SportName") %>'></asp:Label>
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

                                        <asp:Button ID="btnSaveClubSport" runat="server"  Text=" Save " OnClick="btnSaveClubSport_Click" 
                                                    ValidationGroup="Sports" CssClass="btn blue" ClientIDMode="Static" Width="100px"
                                                    OnClientClick="return validateAndConfirm(this.id);" />

                                        <asp:Button ID="btnUpdateClubSport" runat="server"  Text=" Update " OnClick="btnUpdateClubSport_Click" 
                                                    ValidationGroup="Sports" CssClass="btn red" ClientIDMode="Static" Width="100px"
                                                    OnClientClick="return validateAndConfirm(this.id);" />

                                        <asp:Button ID="btnCancelClubSport" runat="server" Text="Cancel" OnClick="btnCancelClubSport_Click" CssClass="btn" 
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
