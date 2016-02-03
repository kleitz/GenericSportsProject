<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmRegistration.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmRegistration" %>

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
                if (confirm('Are you sure to delete this Registration?')) {
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
    function previewFilelogo()
    {
        var preview = document.querySelector('#<%=UserLogoImage.ClientID %>');
        var file = document.querySelector('#<%=UserLogoFile.ClientID %>').files[0];
        var reader = new FileReader();

        reader.onloadend = function ()
        {
            preview.src = reader.result;
        }

        if (file)
        {
            if (file.size > 10485760)
            {
                document.getElementById('dvMsg').style.display = "block";
                preview.src = "";
            }
            reader.readAsDataURL(file);
        }
        else {
            preview.src = "";
        }
    }
</script>

<script type="text/javascript">
    function textBoxOnBlur(elementRef, id) {
        var checkValue = new String(elementRef.value);
        var save_btn = document.getElementById("<%=btnSaveRegistration.ClientID %>");
        var update_btn = document.getElementById("<%=btnUpdateRegistration.ClientID %>");
        var chars = ['<', '>', '*', '$', '@', ',', '_', '%'];
        var realted_span = document.getElementById("nameError");
        if (checkValue.length > 0) {
            var currentChar = checkValue.charAt(0);
            if (chars.indexOf(currentChar) >= 0) {
                realted_span.style.display = "inline";
                elementRef.value = "";

                if (save_btn != null)
                {
                    save_btn.disabled = true;
                }

                if (update_btn != null)
                {
                    update_btn.disabled = true;
                }
            }
            else
            {
                realted_span.style.display = "none";
                if (save_btn != null)
                {
                    save_btn.disabled = false;
                }
                if (update_btn != null)
                {
                    update_btn.disabled = false;
                }
            }
        }
    }

    function validateAndConfirmClose(OnlyClose)
    {
        var validated = Page_ClientValidate('CloseSports');

        if (OnlyClose == "btnCloseRegistration")
        {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Registration Form ?";
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

                        if (OnlyClose == "btnCloseRegistration")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCloseRegistration))%>;
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

        if (btn_clientid == "btnUpdateRegistration")
        {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Registration Details ?";
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

                        if (btn_clientid == "btnSaveRegistration")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveRegistration))%>;
                        }

                        if (btn_clientid == "btnUpdateRegistration")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateRegistration))%>;
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
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" Registration detail are save successfully. ">
     </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" Registration detail are update successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
     <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Registration Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

<asp:Panel ID="PnlGridRegistration" runat="server">
   <asp:Panel ID="addPanel" runat="server">
        <div id="submenu" style="float:left;">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddRegistration" 
                                    runat="server" 
                                    Height="35px" 
                                    Text=" Add Registration" 
                                    onclick="btnAddRegistration_Click" 
                                    ForeColor="White">
                    </asp:LinkButton>
                </li>
            </ul>
        </div>

        <div class="teams-search-area">
            <asp:TextBox ID="txtRegistrationSearch" runat="server"  placeholder=" Enter Registration Name" Width="250px" CssClass="m-wrap medium" 
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
					<span class="hidden-480"> Registration List </span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
      
    <div class="portlet-body flip-scroll">

    <asp:GridView ID="gvRegistration" runat="server" 
                  CssClass="table-bordered table-striped table-condensed flip-content" 
                  AutoGenerateColumns="false" width="100%"
                  ShowHeaderWhenEmpty="true" 
                  AllowPaging="true" PageSize="10"
                  EmptyDataText="No Records Found" 
                  EmptyDataRowStyle-ForeColor="Red" 
                  onpageindexchanging="gvRegistration_PageIndexChanging"
                  DataKeyNames ="UserId">
        <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
        <Columns>

         <asp:TemplateField HeaderText="UserId" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                    Visible="false" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                        <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
         </asp:TemplateField>

            <asp:BoundField DataField="UserName" HeaderText=" Name " HeaderStyle-CssClass="grid-header-column" ItemStyle-Width="30%" ItemStyle-CssClass="grid-column" HeaderStyle-Width="30%" />

              <asp:TemplateField HeaderText="Email ID" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="22px" 
                               ItemStyle-Width="20px">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">                    
                        <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId") %>' ToolTip="Email Address">
                        </asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

              <asp:TemplateField HeaderText="Telephone No" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="22px" 
                               ItemStyle-Width="20px">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">                    
                        <asp:Label ID="lblTelephoneNumber" runat="server" Text='<%#Eval("TelephoneNumber") %>' ToolTip=" Telephone No">
                        </asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Date Of Birth" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="22px" 
                               ItemStyle-Width="20px">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">                    
                        <asp:Label ID="lblDateOfBirth" runat="server" Text='<%#Eval("DateOfBirth") %>' ToolTip="Date Of Birth">
                        </asp:Label>
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
                            <asp:ListItem Value="AddDocuments">Add Document's</asp:ListItem>
                            <asp:ListItem Value="AddParentORRelatives"> Add Parent / Relatives  </asp:ListItem>
                            <%--<asp:ListItem Value="Delete">Delete</asp:ListItem>--%>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlActionUserID" runat="server" Text='<%#Eval("UserId") %>' Visible="false">
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

<asp:Panel ID="pnlEntryRegistration" runat="server" Visible="false">

   <div style="padding:10px 0px;">
        * Note: All Fields marked with an asterisk (*) are required.
   </div>
    
	<!-- BEGIN SAMPLE FORM PORTLET-->   
	<div class="portlet box blue tabbable">
		<div class="portlet-title">
			<div class="caption">
				<i class="icon-reorder"></i>
				<span class="hidden-480"> Registration Details</span>
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
                   <asp:Label ID="lblSuffixId" runat="server" Text=" Suffix :" ></asp:Label>
             </label>
              <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlSuffix" runat="server" CssClass="medium m-wrap">
                            <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Mr" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Mrs" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Ms" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Miss" Value="4"></asp:ListItem>
                  </asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Suffix ID,"
                                                ControlToValidate="ddlSuffix" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select Suffix Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
             </div>
        </div>
                      
        

        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblFitstName" runat="server" Text=" First Name :" ></asp:Label>
             </label>
             <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtFirstName" runat="server" CssClass="m-wrap large"/>
                  <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="First Name,"
                                              ControlToValidate="txtFirstName" SetFocusOnError="true" 
                                              ValidationGroup="Sports" Text="First Name Required !" 
                                              CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                    Display="Static" ControlToValidate="txtFirstName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                      <asp:CustomValidator ID="cvtxtFirstName" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtFirstName" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblMiddleName" runat="server" Text=" Middle Name :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtMiddleName" runat="server" CssClass="m-wrap large"/>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                    Display="Static" ControlToValidate="txtMiddleName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                     <asp:CustomValidator ID="CustomValidator1" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtMiddleName" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblLastName" runat="server" Text=" Last Name :" ></asp:Label>
             </label>
             <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtLastName" runat="server" CssClass="m-wrap large"/>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Last Name,"
                                              ControlToValidate="txtLastName" SetFocusOnError="true" 
                                              ValidationGroup="Sports" Text="Last Name Required !" 
                                              CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator8"
                                                    Display="Static" ControlToValidate="txtLastName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                 <asp:CustomValidator ID="CustomValidator2" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtLastName" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblGender" runat="server" Text=" Gender :" ></asp:Label>
             </label>
              <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlGender" runat="server" CssClass="medium m-wrap">
                            <asp:ListItem Text="Select Gender" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                  </asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Gender,"
                                                ControlToValidate="ddlGender" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select Gender Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblAddress1" runat="server" Text=" Address 1:" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtAddress1" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="100px"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6"
                                                    Display="Static" ControlToValidate="txtAddress1"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,200}$" 
                                                    runat="server" ErrorMessage="Maximum 200 characters allowed.">
                    </asp:RegularExpressionValidator>  
                <asp:CustomValidator ID="CustomValidator3" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtAddress1" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblAddress2" runat="server" Text=" Address 2:" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtAddress2" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="100px"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9"
                                                    Display="Static" ControlToValidate="txtAddress2"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,200}$" 
                                                    runat="server" ErrorMessage="Maximum 200 characters allowed.">
                    </asp:RegularExpressionValidator>  
                  <asp:CustomValidator ID="CustomValidator4" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtAddress2" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>

        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblCity" runat="server" Text=" City :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtCity" runat="server" CssClass="m-wrap large"/>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                                    Display="Static" ControlToValidate="txtCity"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,15}$" 
                                                    runat="server" ErrorMessage="Maximum 15 characters allowed.">
                    </asp:RegularExpressionValidator> 
                 <asp:CustomValidator ID="CustomValidator5" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtCity" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblState" runat="server" Text=" State :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtState" runat="server" CssClass="m-wrap large"/>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                                    Display="Static" ControlToValidate="txtState"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,15}$" 
                                                    runat="server" ErrorMessage="Maximum 15 characters allowed.">
                    </asp:RegularExpressionValidator> 
                 <asp:CustomValidator ID="CustomValidator6" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtState" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblZipPostalCode" runat="server" Text=" Postal Code :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtZipPostalCode" runat="server" CssClass="m-wrap large" />
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator10"
                                                    Display="Static" ControlToValidate="txtZipPostalCode"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,15}$" 
                                                    runat="server" ErrorMessage="Maximum 15 characters allowed.">
                    </asp:RegularExpressionValidator> 
                  <asp:CustomValidator ID="CustomValidator7" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtZipPostalCode" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

       <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblCountry" runat="server" Text=" Country :" ></asp:Label>
             </label>
              <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlCountry" runat="server" CssClass="medium m-wrap">
                  </asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage=" Country,"
                                                ControlToValidate="ddlCountry" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select Country Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
             </div>
        </div>

          <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblDateOfBirth" runat="server" Text=" Date Of Birth :"></asp:Label>
             </label>
               <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">   
                  <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="enddatetimepicker m-wrap medium"/>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Date Of Birth,"
                                                ControlToValidate="txtDateOfBirth" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select Date Of Birth Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                 Display="Static" ControlToValidate="txtDateOfBirth"  
                                                 ValidationGroup="Sports" CssClass="errorfordnn"
                                                 ValidationExpression = "^[\s\S]{0,25}$" 
                                                 runat="server" ErrorMessage="Maximum 25 characters allowed.">
                 </asp:RegularExpressionValidator> 
                  <asp:CustomValidator ID="CustomValidator8" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtDateOfBirth" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>  
             </div> 
        </div>

        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblPlaceOfBirth" runat="server" Text=" Place Of Birth :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtPlaceOfBirth" runat="server" CssClass="m-wrap large"/>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator11"
                                                    Display="Static" ControlToValidate="txtPlaceOfBirth"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,15}$" 
                                                    runat="server" ErrorMessage="Maximum 15 characters allowed.">
                    </asp:RegularExpressionValidator> 
                  <asp:CustomValidator ID="CustomValidator9" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtPlaceOfBirth" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>  
             </div>
        </div>

            <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblHeight" runat="server" Text=" Height :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtHeight" runat="server" CssClass="m-wrap small"/>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator13"
                                                    Display="Static" ControlToValidate="txtHeight"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,5}$" 
                                                    runat="server" ErrorMessage="Maximum 5 characters allowed.">
                    </asp:RegularExpressionValidator> 
                 <asp:CustomValidator ID="CustomValidator10" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtHeight" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

         <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblWeight" runat="server" Text=" Weight :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtWeight" runat="server" CssClass="m-wrap small"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14"
                                                    Display="Static" ControlToValidate="txtWeight"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,5}$" 
                                                    runat="server" ErrorMessage="Maximum 5 characters allowed.">
                    </asp:RegularExpressionValidator> 
                 <asp:CustomValidator ID="CustomValidator11" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtWeight" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblEmailAddress" runat="server" Text="Email ID :"></asp:Label>
            </label>    

            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="m-wrap medium" ValidationGroup="Sports"></asp:TextBox>
                 <asp:RegularExpressionValidator ID="revEmailAddress" runat="server" ControlToValidate="txtEmailAddress" 
                                                 ValidationGroup="Sports" ErrorMessage="Email," SetFocusOnError="true" 
                                                 Text="Email ID Required !" CssClass="errorfordnn"
                                                 ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"/>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator12"
                                             Display="Static" ControlToValidate="txtEmailAddress"  
                                             ValidationGroup="Sports" CssClass="errorfordnn"
                                             ValidationExpression = "^[\s\S]{0,50}$" 
                                             runat="server" ErrorMessage="Maximum 50 characters allowed.">
                 </asp:RegularExpressionValidator>
                <asp:CustomValidator ID="CustomValidator12" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtEmailAddress" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
            </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblTelephone" runat="server" Text="Telephone :" ></asp:Label>
            </label>
            <div style="position:relative;" class="controls"> 
                <asp:TextBox ID="txtTelephone" runat="server" CssClass="m-wrap medium onlynumeric"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator18"
                                             Display="Static" ControlToValidate="txtTelephone"  
                                             ValidationGroup="Sports" CssClass="errorfordnn"
                                             ValidationExpression = "^[\s\S]{0,20}$" 
                                             runat="server" ErrorMessage="Maximum 20 characters allowed.">
                </asp:RegularExpressionValidator>
                <asp:CustomValidator ID="CustomValidator13" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtTelephone" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
     </div>    

          <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblUserLogoName" runat="server" Text=" Photo Name :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtUserLogoName" runat="server" CssClass="m-wrap large" />
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator15"
                                                    Display="Static" ControlToValidate="txtUserLogoName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                    </asp:RegularExpressionValidator> 
                  <asp:CustomValidator ID="CustomValidator14" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtUserLogoName" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblUserLogo" runat="server" Text=" User Photo : "></asp:Label>
             </label>
            <div class="controls" style="position:relative;">  
                <input ID="UserLogoFile" type="file" name="file" runat="server" onchange="previewFilelogo()"/>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" 
                                                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$"
                                                ControlToValidate="UserLogoFile" ValidationGroup="Sports" 
                                                runat="server" ForeColor="Red" 
                                                ErrorMessage="Please choose only .jpg, .png and .gif images!"
                                                CssClass ="errorfordnn" />
                <div style="padding-top:10px;border:none; Width:200px;">
                    <asp:Image ID="UserLogoImage" runat="server" onError="imgError(this);"/>
                </div>
            </div>
        </div>

       <div ID="divUserRole" runat="server">
        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblUserRole" runat="server" Text=" User Role :" ></asp:Label>
             </label>
             <div class="startsetallfrom">
                   <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlUserRole" runat="server" CssClass="medium m-wrap" 
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlUserRole_SelectedIndexChanged"/>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage=" User Role,"
                                                ControlToValidate="ddlUserRole" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select User Role Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
             </div>
        </div>
        </div>

        <div ID="divUserType" runat="server" visible="false">
            <div class="control-group">
		         <label class="control-label">
                       <asp:Label ID="lblUserType" runat="server" Text=" User Type :" ></asp:Label>
                 </label>
                  <div class="startsetallfrom">
                        <span class="help-inline"><font Color="red"><b>*</b></font></span>
                 </div>
                 <div class="controls" style="position:relative;">
                      <asp:DropDownList ID="ddlUserType" runat="server" CssClass="medium m-wrap"/>
                      <asp:RequiredFieldValidator ID="RFVUserType" runat="server" ErrorMessage=" User Type,"
                                                    ControlToValidate="ddlUserType" SetFocusOnError="true"  
                                                    ValidationGroup="Sports" 
                                                    InitialValue="0" Text="Select UserType Required !" CssClass="errorfordnn" 
                                                    ClientIDMode="Static"/>
                 </div>
            </div>
        </div>       

     <div ID="divAssignToTeam" runat="server" visible="false">
       <div class="control-group">
         <label class="control-label"> 
               <asp:Label ID="lblSelect" runat="server" Text="Assign To :" ></asp:Label>
         </label>
         <div class="controls" style="position:relative;">
                   <asp:DropDownList ID="drpSelectionEntry" runat="server"  AutoPostBack="true" CssClass="medium m-wrap"  
                                             OnSelectedIndexChanged="drpSelectionEntry_SelectedIndexChanged">
                        <asp:ListItem Text="Enter As" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Direct in Team" Value="1"></asp:ListItem>
                    </asp:DropDownList>
          </div>
        </div>
       </div>

    <div ID="divAssignToClub" runat="server" visible="false">
       <div class="control-group">
         <label class="control-label"> 
               <asp:Label ID="lblSelectAssignToClub" runat="server" Text="Assign To :" ></asp:Label>
         </label>
         <div class="controls" style="position:relative;">
                <asp:DropDownList ID="ddlAssignToClub" runat="server"  AutoPostBack="true" CssClass="medium m-wrap"  
                                          OnSelectedIndexChanged="ddlAssignToClub_SelectedIndexChanged">
                     <asp:ListItem Text="Enter As" Value="0"></asp:ListItem>
                     <asp:ListItem Text="Direct in Club" Value="1"></asp:ListItem>
                </asp:DropDownList>
          </div>
        </div>
       </div>

     
            
      <div ID="divSport" runat="server" visible="false">
            <div class="control-group">
		         <label class="control-label">
                       <asp:Label ID="lblSport" runat="server" Text=" Sport :" ></asp:Label>
                 </label>
                <div class="startsetallfrom">
                        <span class="help-inline"><font Color="red"><b>*</b></font></span>
                 </div>
                 <div class="controls" style="position:relative;">
                      <asp:DropDownList ID="ddlSport" runat="server" CssClass="medium m-wrap" AutoPostBack="true"
                          OnSelectedIndexChanged="ddlSport_SelectedIndexChanged"/>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage=" Sport,"
                                                ControlToValidate="ddlSport" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select Sport Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
                 </div>
            </div>
        </div>          

      

      <div ID="divCompetition" runat="server" visible="false">
            <div class="control-group">
		         <label class="control-label">
                       <asp:Label ID="lblCompetition" runat="server" Text=" Competition :" ></asp:Label>
                 </label>
                 <div class="controls" style="position:relative;">
                      <asp:DropDownList ID="ddlCompetition" runat="server" CssClass="medium m-wrap"/>
                 </div>
            </div>
        </div>          

      <div ID="divTeam" runat="server" visible="false">

            <div class="control-group">
		         <label class="control-label">
                       <asp:Label ID="lblTeam" runat="server" Text=" Team :" ></asp:Label>
                 </label>
                <div class="startsetallfrom">
                        <span class="help-inline"><font Color="red"><b>*</b></font></span>
                 </div>
                 <div class="controls" style="position:relative;">
                      <asp:DropDownList ID="ddlTeam" runat="server" CssClass="medium m-wrap"/>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage=" Team,"
                                                ControlToValidate="ddlTeam" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select Team Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
                 </div>
            </div>

        </div>          

        <div ID="divPlayerType" runat="server" visible="false">
            <div class="control-group">
		         <label class="control-label">
                       <asp:Label ID="lblPlayerType" runat="server" Text=" Player Position :" ></asp:Label>
                 </label>
                  <div class="startsetallfrom">
                        <span class="help-inline"><font Color="red"><b>*</b></font></span>
                 </div>
                 <div class="controls" style="position:relative;">
                      <asp:DropDownList ID="ddlPlayerType" runat="server" CssClass="medium m-wrap"/>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage=" Player Type,"
                                                    ControlToValidate="ddlPlayerType" SetFocusOnError="true"  
                                                    ValidationGroup="Sports" 
                                                    InitialValue="0" Text="Select Player Type Required !" CssClass="errorfordnn" 
                                                    ClientIDMode="Static"/>
                 </div>
            </div>

        </div>       

      <div ID="divPlayerJerseyNo" runat="server" visible="false">
                   <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblPlayerJerseyNo" runat="server" Text=" Player Jersey No :" ></asp:Label>
             </label>
             <div class="startsetallfrom">
                    <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtPlayerJerseyNo" runat="server" CssClass="m-wrap small" />
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator16"
                                                    Display="Static" ControlToValidate="txtPlayerJerseyNo"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,5}$" 
                                                    runat="server" ErrorMessage="Maximum 5 characters allowed.">
                    </asp:RegularExpressionValidator> 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage=" Player Jersey No,"
                                                ControlToValidate="txtPlayerJerseyNo" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text=" Player Jersey No Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
                  <asp:CustomValidator ID="CustomValidator15" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtPlayerJerseyNo" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>
              </div>
            
      <div ID="divPlayerJerseyName" runat="server" visible="false">
                   <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblPlayerJerseyName" runat="server" Text=" Player Jersey Name :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtPlayerJerseyName" runat="server" CssClass="m-wrap large" />
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator17"
                                                    Display="Static" ControlToValidate="txtPlayerJerseyName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                    </asp:RegularExpressionValidator> 
                  <asp:CustomValidator ID="CustomValidator16" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtPlayerJerseyName" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>
              </div>

      <div ID="divPlayerFamousName" runat="server" visible="false">
                   <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblPlayerFamousName" runat="server" Text=" Player Famous Name :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtPlayerFamousName" runat="server" CssClass="m-wrap large" />
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator19"
                                                    Display="Static" ControlToValidate="txtPlayerFamousName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                    </asp:RegularExpressionValidator> 
                  <asp:CustomValidator ID="CustomValidator17" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtPlayerFamousName" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>
              </div>

      <div ID="divTeamMemberType" runat="server" visible="false">
            <div class="control-group">
		         <label class="control-label">
                       <asp:Label ID="lblTeamMemberType" runat="server" Text=" Member Position :" ></asp:Label>
                 </label>
                  <div class="startsetallfrom">
                        <span class="help-inline"><font Color="red"><b>*</b></font></span>
                 </div>
                 <div class="controls" style="position:relative;">
                      <asp:DropDownList ID="ddlTeamMemberType" runat="server" CssClass="medium m-wrap"/>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage=" Team Member Type,"
                                                    ControlToValidate="ddlTeamMemberType" SetFocusOnError="true"  
                                                    ValidationGroup="Sports" 
                                                    InitialValue="0" Text="Select Team Member Type Required !" CssClass="errorfordnn" 
                                                    ClientIDMode="Static"/>
                 </div>
            </div>

        </div>       
            
      <div ID="divteammemberjerseyno" runat="server" visible="false">
                   <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblTeamMemberJerseyNo" runat="server" Text=" Member Jersey No :" ></asp:Label>
             </label>
             <div class="startsetallfrom">
                    <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtTeamMemberJerseyNo" runat="server" CssClass="m-wrap small" />
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator20"
                                                    Display="Static" ControlToValidate="txtTeamMemberJerseyNo"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,5}$" 
                                                    runat="server" ErrorMessage="Maximum 5 characters allowed.">
                    </asp:RegularExpressionValidator> 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage=" Team Member Jersey No,"
                                                ControlToValidate="txtTeamMemberJerseyNo" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text=" Team Member Jersey No Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
                  <asp:CustomValidator ID="CustomValidator18" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtTeamMemberJerseyNo" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>
              </div>
            
      <div ID="divTeamMemberJerseyName" runat="server" visible="false">
          <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblTeamMemberJerseName" runat="server" Text=" Member Jersey Name :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtTeamMemberJerseyName" runat="server" CssClass="m-wrap large" />
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator21"
                                                    Display="Static" ControlToValidate="txtTeamMemberJerseyName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                    </asp:RegularExpressionValidator> 
                  <asp:CustomValidator ID="CustomValidator19" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtTeamMemberJerseyName" EnableClientScript="true" 
                                                 ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>
     </div>

      <div ID="divTeamMemberFamousName" runat="server" visible="false">
                   <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblTeamMemberFamousName" runat="server" Text=" Member Famous Name :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtTeamMemberFamousName" runat="server" CssClass="m-wrap large" />
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator22"
                                                    Display="Static" ControlToValidate="txtTeamMemberFamousName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                    </asp:RegularExpressionValidator> 
                  <asp:CustomValidator ID="CustomValidator20" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtTeamMemberFamousName" EnableClientScript="true" 
                                                 ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>
              </div>

      <div ID="divClub" runat="server" visible="false">
            <div class="control-group">
		         <label class="control-label">
                       <asp:Label ID="lblClub" runat="server" Text=" Club :" ></asp:Label>
                 </label>
                <div class="startsetallfrom">
                        <span class="help-inline"><font Color="red"><b>*</b></font></span>
                 </div>
                 <div class="controls" style="position:relative;">
                      <asp:DropDownList ID="ddlClub" runat="server" CssClass="medium m-wrap"/>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage=" Club,"
                                                            ControlToValidate="ddlClub" SetFocusOnError="true"  
                                                            ValidationGroup="Sports" 
                                                            InitialValue="0" Text="Select Club Required !" CssClass="errorfordnn" 
                                                            ClientIDMode="Static"/>
                 </div>
            </div>
        </div>          
       
      <div ID="divClubOwnerDescription" runat="server" visible="false">       
       <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblClubOwnerDescription" runat="server" Text="Description :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtClubOwnerDescription" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px"/>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator23"
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
       </div>

      <div ID="divClubOwnerPercentage" runat="server" visible="false">
         <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblClubOwnerPercentage" runat="server" Text="Percentage :"></asp:Label>
            </label>
		    <div class="controls" style="position:relative;">
                   <asp:TextBox ID="txtClubOwnerPercentage" runat="server" CssClass="m-wrap small"></asp:TextBox>%
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator24"
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
     </div>

            <div ID="divClubMemberType" runat="server" visible="false">       
    <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblClubMemberType" runat="server" Text=" Member Position :" ></asp:Label>
             </label>
              <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlMemberType" runat="server" CssClass="medium m-wrap"/>
                  <asp:RequiredFieldValidator ID="RFVMemberType" runat="server" ErrorMessage="Member Type,"
                                                ControlToValidate="ddlMemberType" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select Member Type Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
             </div>
        </div>
                </div>

        <div ID="divClubMemberDesc" runat="server" visible="false">       
              <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblClubMemberDesc" runat="server" Text="Description :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtClubMemberDesc" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px"/>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator25"
                                                    Display="Static" ControlToValidate="txtClubMemberDesc"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,300}$" 
                                                    runat="server" ErrorMessage="Maximum 300 characters allowed.">
                    </asp:RegularExpressionValidator>  
                  <asp:CustomValidator ID="cvtxtClubMemberDesc" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtClubMemberDesc" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>
            </div>

      <div class="control-group">
		    <label class="control-label">
            <asp:Label ID="lblActive" runat="server" Text=" IsActive :"></asp:Label>
        </label>
            <div class="controls" style="margin-top:8px;">
                <div id="checkdiv" runat="server" class="SingleCheckbox col-left">
                    <asp:CheckBox ID="ChkIsActive" runat="server" />
                        <asp:Label ID="lblChkIsActive" AssociatedControlID="ChkIsActive" runat="server" Text="" CssClass="CheckBoxLabel">
                        </asp:Label>
                </div>
             </div>
        </div>

      <div class="control-group">
		    <label class="control-label">
            <asp:Label ID="lblShow" runat="server" Text=" IsShow :"></asp:Label>
        </label>
            <div class="controls" style="margin-top:8px;">
                <div id="checdivshow" runat="server" class="SingleCheckbox col-left">
                    <asp:CheckBox ID="ChkIsShow" runat="server" />
                        <asp:Label ID="lblChkIsShow" AssociatedControlID="ChkIsShow" runat="server" Text="" CssClass="CheckBoxLabel">
                        </asp:Label>
                </div>
             </div>
        </div>    
       
       </div>

       <div class="form-actions">
                    
        <div class="right_div_css">
               <asp:Button id="btnSaveRegistration" runat="server" Width="100px" Text="Save" ClientIDMode="Static"
                         onclick="btnSaveRegistration_Click" ValidationGroup="Sports" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn blue"/>

             <asp:Button id="btnUpdateRegistration" runat="server" Width="100px" Text="Update"  ClientIDMode="Static"
                         onclick="btnUpdateRegistration_Click" Visible="false" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn red"  ValidationGroup="Sports"/>        

             <asp:Button id="btnCloseRegistration" runat="server" Width="100px"  Text="Cancel" 
                         onclick="btnCloseRegistration_Click" CssClass="btn" ClientIDMode="Static" ValidationGroup="CloseSports"
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
