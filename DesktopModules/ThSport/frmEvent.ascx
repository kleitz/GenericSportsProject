<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmEvent.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmEvent" %>

<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnCssInclude FilePath="~/DesktopModules/ThSport/CSS/jquery.datetimepicker.css" runat="server"/>
<dnn:DnnJsInclude FilePath="~/DesktopModules/ThSport/JS/jquery.datetimepicker.js" runat="server"/>

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
    $(document).ready(function () {
        $('.ddlActionSelect').change(function (evt) {
            evt.preventDefault();
            if ($(this).val() == "Delete") {
                if (confirm('Are you sure to delete this Event?'))
                {
                    setTimeout("__doPostBack('" + this.id + "','')", 0);
                }
                else
                {
                    //do nothing, prevent postback
                    $(this).prop('selectedIndex', 0);
                }
            }
            else
            {
                setTimeout("__doPostBack('" + this.id + "','')", 0);
            }
        });
        //Reset drop down list
        $(".ddlActionSelect > option:first").attr("selected", "selected");
    });
</script>

<script type="text/javascript">
    function textBoxOnBlur(elementRef, id) {
        var checkValue = new String(elementRef.value);
        var save_btn = document.getElementById("<%=btnSaveEvent.ClientID %>");
        var update_btn = document.getElementById("<%=btnUpdateEvent.ClientID %>");
        var chars = ['<', '>', '*', '$', '@', ',', '_', '%'];
        var realted_span = document.getElementById("nameError");
        if (checkValue.length > 0) {
            var currentChar = checkValue.charAt(0);
            if (chars.indexOf(currentChar) >= 0) {
                realted_span.style.display = "inline";
                elementRef.value = "";

                if (save_btn != null) {
                    save_btn.disabled = true;
                }

                if (update_btn != null) {
                    update_btn.disabled = true;
                }
            }
            else {
                realted_span.style.display = "none";
                if (save_btn != null) {
                    save_btn.disabled = false;
                }
                if (update_btn != null) {
                    update_btn.disabled = false;
                }
            }
        }
    }

    function validateAndConfirmClose(OnlyClose) {
        var validated = Page_ClientValidate('CloseSports');
        if (OnlyClose == "btnCloseEvent")
        {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Event Form ?";
        }

        if (validated)
        {
            $("#dialogBox").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",

                buttons: {
                    Ok: function () {

                        if (OnlyClose == "btnCloseEvent")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCloseEvent))%>;
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

        if (btn_clientid == "btnUpdateEvent")
        {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Event Details ?";
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

                        if (btn_clientid == "btnSaveEvent")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveEvent))%>;
                        }

                        if (btn_clientid == "btnUpdateEvent")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateEvent))%>;
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

<div id="divsavemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" Event detail are save successfully. ">
     </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" Event detail are update successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
     <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Event Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

<asp:Panel ID="PnlGridEvent" runat="server">
   <asp:Panel ID="addPanel" runat="server">
        <div id="submenu" style="float:left;">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddEvent" 
                                    runat="server" 
                                    Height="35px" 
                                    Text=" Add Event " 
                                    onclick="btnAddEvent_Click" 
                                    ForeColor="White">
                    </asp:LinkButton>
                </li>
            </ul>
        </div>

        <div class="teams-search-area">
            <asp:TextBox ID="txtEventNameSearch" runat="server"  placeholder=" Enter Event Name" Width="250px" CssClass="m-wrap medium" 
                             Height="35px" Font-Size="14px"/>&nbsp;
             <asp:LinkButton id="lbGo" runat="server" Text=" Go " ForeColor="White" CssClass="btn blue" Height="24px"
                                onclick="lbGo_Click"></asp:LinkButton>
        </div>
   </asp:Panel>

     <!-- Html Table -->
        <!-- End Html Table -->
    
		<!-- BEGIN SAMPLE FORM PORTLET-->   
		<div class="portlet box green">
			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480">Event List</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
      
    <div class="portlet-body flip-scroll">

    <asp:GridView ID="gvEvent" runat="server" 
                  CssClass="table-bordered table-striped table-condensed flip-content" 
                  AutoGenerateColumns="false" width="100%"
                  ShowHeaderWhenEmpty="true" 
                  AllowPaging="true" PageSize="10"
                  EmptyDataText="No Records Found" 
                  EmptyDataRowStyle-ForeColor="Red" 
                  onpageindexchanging="gvEvent_PageIndexChanging"
                  DataKeyNames ="EventID">
        <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
        <Columns>

         <asp:TemplateField HeaderText="EventID" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                    Visible="false" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                        <asp:Label ID="lblEventID" runat="server" Text='<%#Eval("EventID") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
         </asp:TemplateField>

         <asp:BoundField DataField="EventName" HeaderText="Event Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-Width="30%" ItemStyle-CssClass="grid-column" HeaderStyle-Width="30%" />

            <asp:TemplateField HeaderText="Start Date" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="22px" 
                               ItemStyle-Width="20px">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">                    
                        <asp:Label ID="lblEventStartDateTime" runat="server" Text='<%#Eval("EventStartDateTime") %>' ToolTip="Event Start Date">
                        </asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="End Date" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="22px" 
                               ItemStyle-Width="20px">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
                         <asp:Label ID="lblEventEndDateTime" runat="server" Text='<%#Eval("EventEndDateTime") %>' ToolTip="Event End Date">
                         </asp:Label>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Priority" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
                        <asp:Label ID="lblEventPriority" runat="server" Text='<%#Eval("EventPriority") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                               ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" AutoPostBack="true" 
                                      OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                            <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                            <asp:ListItem Value="Edit">Edit</asp:ListItem>
                            <%--<asp:ListItem Value="Delete">Delete</asp:ListItem>--%>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlActionEventID" runat="server" Text='<%#Eval("EventID") %>' Visible="false">
                    </asp:Label>
                </ItemTemplate>

     </asp:TemplateField>

        </Columns>
        <PagerSettings Mode="NumericFirstLast" PageButtonCount="8" /> 
        <PagerStyle  CssClass="paging" HorizontalAlign="Center"/>
    </asp:GridView>
   </div>
    <input type="hidden" runat="server" id="hidRegID" />
   </div>
</asp:Panel>

<asp:Panel ID="pnlEventEntry" runat="server" Visible="false">

   <div style="padding:10px 0px;">
        * Note: All Fields marked with an asterisk (*) are required.
   </div>
    
	<!-- BEGIN SAMPLE FORM PORTLET-->   
	<div class="portlet box blue tabbable">
		<div class="portlet-title">
			<div class="caption">
				<i class="icon-reorder"></i>
				<span class="hidden-480"> Event Detail</span>
			</div>
		</div>

<div class="portlet-body form">
	<div class="tabbable portlet-tabs">

         <div id="error_div" runat="server" style="display:none;">
            <asp:Label Id="error_msg" runat="server"  Text="" Visible="false"></asp:Label>
        </div>

    <div class="tab-content">
		<div class="tab-pane active" id="portlet_tab1">

        <div class="form-horizontal">

            <div style="width: 100%;margin-top:20px;">
            
            </div>
                        
            <div class="control-group">
		         <label class="control-label">
                       <asp:Label ID="lblSport" runat="server" Text=" Sport :" ></asp:Label>
                 </label>
                 <div class="controls" style="position:relative;">
                      <asp:DropDownList ID="ddlSports" runat="server" CssClass="medium m-wrap" AutoPostBack="true"
                          OnSelectedIndexChanged="ddlSports_SelectedIndexChanged"/>
                 </div>
            </div>
      
        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblSeason" runat="server" Text="  Season :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlSeason" runat="server" CssClass="medium m-wrap"/>
             </div>
        </div>

        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblCompetition" runat="server" Text="  Competition :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlCompetition" runat="server" CssClass="medium m-wrap"/>
             </div>
        </div>

        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblClub" runat="server" Text="  Club :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlClub" runat="server" CssClass="medium m-wrap" AutoPostBack="true" 
                      OnSelectedIndexChanged="ddlClub_SelectedIndexChanged"/>
             </div>
        </div>

    <div id="divclubowner" runat="server" visible="false">
        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblClubOwner" runat="server" Text="  Club Owner :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlClubOwner" runat="server" CssClass="medium m-wrap"/>
             </div>
        </div>
     </div>

            <div id="divclubmember" runat="server" visible="false">
        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblClubMember" runat="server" Text="  Club Member :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlClubMember" runat="server" CssClass="medium m-wrap"/>
             </div>
        </div>
                </div>

        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblTeam" runat="server" Text="  Team :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlTeam" runat="server" CssClass="medium m-wrap" AutoPostBack="true"
                      OnSelectedIndexChanged="ddlTeam_SelectedIndexChanged"/>
             </div>
        </div>

            <div id="divteammember" runat="server" visible="false">
        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblTeamMember" runat="server" Text="  Team Member :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlTeamMember" runat="server" CssClass="medium m-wrap"/>
             </div>
        </div>
                </div>

        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblSponsor" runat="server" Text="  Sponsor :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlSponsor" runat="server" CssClass="medium m-wrap"/>
             </div>
        </div>
            
        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblEventName" runat="server" Text=" Event Name :" ></asp:Label>
             </label>
             <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtEventName" runat="server" CssClass="m-wrap large"/>
                   <asp:RequiredFieldValidator ID="rfvtxtEventName" runat="server" ErrorMessage="EventName"  
                                                ControlToValidate="txtEventName" SetFocusOnError="true" 
                                                ValidationGroup="Sports" Text=" Event Name Required !" CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="rgvtxtEventName"
                                                    Display="Static" ControlToValidate="txtEventName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="cvtxtEventName" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtEventName" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblEventDetail" runat="server" Text="Description :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtEventDetail" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                    Display="Static" ControlToValidate="txtEventDetail"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,300}$" 
                                                    runat="server" ErrorMessage="Maximum 300 characters allowed.">
                    </asp:RegularExpressionValidator>  
                 <asp:CustomValidator ID="cvtxtEventDetail" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtEventDetail" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>

              <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblEventStartDateTime" runat="server" Text="Start Date :" ></asp:Label>
            </label>
                  <div class="startsetallfrom">
                      <span class="help-inline"><font Color="red"><b>*</b></font></span>
                  </div>
            <div class="controls" style="position:relative;">  
                <asp:TextBox ID="txtEventStartDateTime" runat="server"  CssClass="datetimepicker m-wrap medium onlynumeric"/>
                <asp:RequiredFieldValidator ID="rfvtxtEventStartDateTime" runat="server" ErrorMessage="Enter Start Date"
                                                 ControlToValidate="txtEventStartDateTime" SetFocusOnError="true" 
                                                 ValidationGroup="Sports" Text="Start Date Required !" CssClass="errorfordnn" 
                                                 ClientIDMode="Static"/>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                                 Display="Static" ControlToValidate="txtEventStartDateTime"  
                                                 ValidationGroup="Sports" CssClass="errorfordnn"
                                                 ValidationExpression = "^[\s\S]{0,25}$" 
                                                 runat="server" ErrorMessage="Maximum 25 characters allowed.">
                 </asp:RegularExpressionValidator>  
                <asp:CustomValidator ID="CustomValidator1" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtEventStartDateTime" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
            </div>
        </div>
    
        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblEventEndDateTime" runat="server" Text="End Date :"></asp:Label>
             </label>
              <div class="startsetallfrom">
                      <span class="help-inline"><font Color="red"><b>*</b></font></span>
                  </div>
             <div class="controls" style="position:relative;">   
                  <asp:TextBox ID="txtEventEndDateTime" runat="server" CssClass="enddatetimepicker m-wrap medium"/>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter End Date"
                                                 ControlToValidate="txtEventEndDateTime" SetFocusOnError="true" 
                                                 ValidationGroup="Sports" Text="End Date Required !" CssClass="errorfordnn" 
                                                 ClientIDMode="Static"/>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                                 Display="Static" ControlToValidate="txtEventEndDateTime"  
                                                 ValidationGroup="Sports" CssClass="errorfordnn"
                                                 ValidationExpression = "^[\s\S]{0,25}$" 
                                                 runat="server" ErrorMessage="Maximum 25 characters allowed.">
                 </asp:RegularExpressionValidator>   
                 <asp:CustomValidator ID="CustomValidator2" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtEventEndDateTime" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div> 
        </div>

     

       <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblEventPriority" runat="server" Text=" Priority :" ></asp:Label>
             </label>
              <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlEventPriority" runat="server" CssClass="medium m-wrap">
                        <asp:ListItem Text=" -- Select Priority -- " Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="High" Value="High" ></asp:ListItem>
                        <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                  </asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RFVEventPriority" runat="server" ErrorMessage="Event Priority,"
                                                ControlToValidate="ddlEventPriority" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select Event Priority Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
             </div>
        </div>

       <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblActive" runat="server" Text=" Is Active :"></asp:Label>
            </label>
            <div class="controls" style="margin-top:8px;">
                <div id="checkdiv" runat="server" class="SingleCheckbox col-left">
                    <asp:CheckBox ID="ChkIsActive" runat="server" />
                        <asp:Label ID="lblChkIsActive" AssociatedControlID="ChkIsActive" runat="server" Text="" CssClass="CheckBoxLabel">
                        </asp:Label>
                </div>
             </div>
        </div>
       
       </div>

       <div class="form-actions">
                    
        <div class="right_div_css">

               <asp:Button id="btnSaveEvent" runat="server" Width="100px" Text="Save" ClientIDMode="Static"
                         onclick="btnSaveEvent_Click" ValidationGroup="Sports" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn blue"/>

             <asp:Button id="btnUpdateEvent" runat="server" Width="100px" Text="Update"  ClientIDMode="Static"
                         onclick="btnUpdateEvent_Click" Visible="false" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn red"  ValidationGroup="Sports"/>        

             <asp:Button id="btnCloseEvent" runat="server" Width="100px"  Text="Cancel" 
                         onclick="btnCloseEvent_Click" CssClass="btn" ClientIDMode="Static" ValidationGroup="CloseSports"
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