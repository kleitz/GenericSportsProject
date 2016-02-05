<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmLocation.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmLocation" %>

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
                if (confirm('Are you sure to delete this Location?')) {
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
    function LocationSaveSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { LocationsavevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function LocationsavevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divLocationsavemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divLocationsavemassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
</script>

<script type="text/javascript">
    function LocationUpdateSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { LocationupdatevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function LocationupdatevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divLocationupdatemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divLocationupdatemassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
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

<div id="divLocationsavemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" Location detail are save successfully. ">
     </asp:Label>
</div>

<div id="divLocationupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" Location detail are update successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
     <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Location Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

<asp:Panel ID="pnlList" runat="server">
   <asp:Panel ID="addPanel" runat="server">
        <div id="submenu" style="float:left;">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddLocation" runat="server" Height="35px" Text=" Add Location" onclick="btnAddLocation_Click" ForeColor="White"></asp:LinkButton>
                </li>
            </ul>
        </div>

        <div class="teams-search-area">
            <asp:DropDownList ID="ddlSearchByCountry" runat="server" CssClass="medium m-wrap" Width="200px" Height="35px"></asp:DropDownList>
            <asp:TextBox ID="txtLocationNameSearch" runat="server"  placeholder=" Enter Location Name" Width="250px" CssClass="m-wrap medium" Height="35px" Font-Size="14px"/>
            <asp:LinkButton ID="lbGo" runat="server" Text=" Go " ForeColor="White" CssClass="btn blue" Height="24px" OnClick="lbGo_Click"></asp:LinkButton>
        </div>
   </asp:Panel>

     <!-- Html Table -->
        <!-- End Html Table -->
    
		<!-- BEGIN SAMPLE FORM PORTLET-->   
		<div class="portlet box green">
			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480">Location List</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
      
    <div class="portlet-body flip-scroll">

        <asp:GridView ID="gvLocation" runat="server" AutoGenerateColumns="false"  
                      OnRowDataBound="gvLocation_OnRowDataBound" OnRowEditing="gvLocation_OnRowEditing"
                      CssClass="table-bordered table-striped table-condensed flip-content"
                      ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="10" EmptyDataText="No Records Found" 
                      EmptyDataRowStyle-ForeColor="Red" onpageindexchanging="gvLocation_PageIndexChanging" 
                      Width="100%">
            <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
		<Columns>

            <asp:BoundField DataField="Loc_LocationName" HeaderText="Location Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-Width="25%" HeaderStyle-Width="25%" ItemStyle-CssClass="grid-column" />
            
			<asp:TemplateField HeaderText="Address" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" HeaderStyle-Width="200px" >
				<ItemTemplate>
                <div class="grid-cell-inner" style="width:200px;">
					<asp:Label ID="lblLocationAddress" runat="server" Text='<%#Eval("Loc_LocationAddress") %>' ToolTip='<%#Eval("Loc_LocationAddress") %>'></asp:Label>
                </div> 
                    <asp:HiddenField ID="hdnLocationID" runat="server" Value='<%#Eval("Loc_LocationID") %>'></asp:HiddenField>
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="City" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">  
                <ItemTemplate>
                <div class="grid-cell-inner" style="width:80px; display: inline-block;">
                    <asp:Label ID="lblCity" runat="server" Text='<%#Eval("Loc_City") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="State" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="80px">
                <ItemTemplate>
                <div class="grid-cell-inner" style="width:80px; display: inline-block;">
                    <asp:Label ID="lblState" runat="server" Text='<%#Eval("Loc_State") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="ZipCode" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="70px">
                <ItemTemplate>
                <div class="grid-cell-inner" style="width:70px; display: inline-block;">
                    <asp:Label ID="lblZipCode" runat="server" Text='<%#Eval("Loc_ZipCode") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Country" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="90px">
                <ItemTemplate>
                    <asp:HiddenField ID="hdnloc_CountryID" runat="server" Value='<%#Eval("Loc_Country") %>' />
                <div class="grid-cell-inner" style="width:90px; display: inline-block;">
                    <asp:Label ID="lblCountryName" runat="server" ></asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

              <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                               ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="96px">
                <ItemTemplate>
                    
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" AutoPostBack="true"
                                      OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                            <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                            <asp:ListItem Value="Edit">Edit</asp:ListItem>
                    </asp:DropDownList>
                              
                </ItemTemplate>

                  <EditItemTemplate>
                    <asp:Panel ID="pnlEditable" runat="server" Visible="false">
                        <asp:LinkButton ID="lnkupdate" Text="Update" runat="server" OnClick = "OnUpdate" CssClass="grid_row_edit_linkbtn" ToolTip="Save">
                            <img src="<%= Page.ResolveUrl("~/DesktopModules/SportSite/Images/icons/save_icon.png")%>" alt="" width="20" />
                        </asp:LinkButton>
            
                        <asp:LinkButton ID="lnkcancel" Text="Cancel" runat="server" OnClick = "OnCancel" CssClass="grid_row_edit_linkbtn" ToolTip="Cancel">
                            <img src="<%= Page.ResolveUrl("~/DesktopModules/SportSite/Images/icons/cancel_icon.gif")%>" alt="" width="28" />
                        </asp:LinkButton>
                     </asp:Panel>
                </EditItemTemplate>

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
				<span class="hidden-480"> Location Detail</span>
			</div>
		</div>

<asp:HiddenField ID="hdn_LocationID" runat="server" />

<div class="portlet-body form">
	<div class="tabbable portlet-tabs">

         <div id="error_div" runat="server" style="display:none;">
            <asp:Label Id="error_msg" runat="server"  Text="" Visible="false"></asp:Label>
        </div>

    <div class="tab-content">
		<div class="tab-pane active" id="portlet_tab1">

        <div class="form-horizontal">

            <div style="width: 100%;margin-top:20px;"></div>
            
            <div class="control-group">
		     <label class="control-label">Sport :</label>
              <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlSport" runat="server" CssClass="medium m-wrap"/>
                  <asp:RequiredFieldValidator ID="rfvddlSport" runat="server" ErrorMessage="Sport Name,"
                                                ControlToValidate="ddlSport" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Sport Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
             </div>
        </div>

        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblLocationName" runat="server" Text=" Location Name :" ></asp:Label>
             </label>
             <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtLocationName" runat="server" CssClass="m-wrap large"/>
                     <asp:RequiredFieldValidator ID="rfvtxtLocationName" runat="server" ErrorMessage="LocationName"  
                                                ControlToValidate="txtLocationName" SetFocusOnError="true" 
                                                ValidationGroup="Sports" Text=" Location Name Required !" CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="rgvtxtLocationName"
                                                    Display="Static" ControlToValidate="txtLocationName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="cvtxtLocationName" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtLocationName" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblLocationAddress" runat="server" Text=" Abbreviation :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtLocationAddress" runat="server" CssClass="m-wrap small"/>
                    <asp:RegularExpressionValidator ID="rgvtxtLocationAddress"
                                                    Display="Static" ControlToValidate="txtLocationAddress"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,5}$" 
                                                    runat="server" ErrorMessage="Maximum 5 characters allowed.">
                    </asp:RegularExpressionValidator>  
                 <asp:CustomValidator ID="cvtxtLocationAddress" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtLocationAddress" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>

       <div class="control-group">
		        <label class="control-label">
                    <asp:Label ID="lblCountry" runat="server" Text="Country :" ></asp:Label>
                </label>  
                <div style="position:relative;" class="controls">                
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="medium m-wrap"> 
                    </asp:DropDownList>
                </div>
            </div>

                <div class="control-group">
		        <label class="control-label">
                    <asp:Label ID="lblState" runat="server" Text="State :" ></asp:Label>
                </label>
                <div style="position:relative;" class="controls">                
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="medium m-wrap">
                        <asp:ListItem Value="0"> -- Select State --</asp:ListItem>
                        <asp:ListItem Value="Zambia"> Zambia </asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
    <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblCity" runat="server" Text="City :" ></asp:Label>
            </label>
            <div style="position:relative;" class="controls">                
                <asp:TextBox ID="txtCity" runat="server" CssClass="m-wrap medium"></asp:TextBox>  
                <asp:CompareValidator ID="CVCity" runat="server" Operator="DataTypeCheck" Type="String" ValidationGroup="Sports" ControlToValidate="txtCity" 
                                      ErrorMessage="City," CssClass="errorfordnn" Text="Please Enter Character"/>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2"  Display="Static" ControlToValidate="txtCity"  ValidationGroup="Sports" CssClass="errorfordnn"
                                           ValidationExpression = "^[\s\S]{0,25}$" runat="server" ErrorMessage="Maximum 25 characters allowed.">
                 </asp:RegularExpressionValidator>
            </div>
    </div>
    <div class="control-group">
        <label class="control-label">
            <asp:Label ID="lblZipCode" runat="server" Text="Zip Code :" ></asp:Label>
        </label>
        <div class="controls" style="position:relative;">
        <asp:TextBox ID="txtZipCode" runat="server" CssClass="m-wrap medium"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Static" ControlToValidate="txtZipCode"  ValidationGroup="Sports" CssClass="errorfordnn"
                                ValidationExpression = "^[\s\S]{0,10}$" 
                                runat="server" ErrorMessage="Maximum 10 characters allowed.">
        </asp:RegularExpressionValidator>
    </div>
                </div>  
       
       </div>

       <div class="form-actions">
                    
        <div class="right_div_css">
               <asp:Button id="btnSaveLocation" runat="server" Width="100px" Text="Save" ClientIDMode="Static"
                         onclick="btnSaveLocation_Click" ValidationGroup="Sports" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn blue"/>

             <asp:Button id="btnUpdateLocation" runat="server" Width="100px" Text="Update"  ClientIDMode="Static"
                         onclick="btnUpdateLocation_Click" Visible="false" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn red"  ValidationGroup="Sports"/>        

             <asp:Button id="btnCloseLocation" runat="server" Width="100px"  Text="Cancel" 
                         onclick="btnCloseLocation_Click" CssClass="btn" ClientIDMode="Static" ValidationGroup="CloseSports"
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
