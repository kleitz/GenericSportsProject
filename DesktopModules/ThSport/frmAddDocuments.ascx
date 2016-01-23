<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmAddDocuments.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmAddDocuments" %>

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
                if (confirm('Are you sure to delete this Document?')) {
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
    function textBoxOnBlur(elementRef, id) {
        var checkValue = new String(elementRef.value);
        var save_btn = document.getElementById("<%=btnSaveDocument.ClientID %>");
        var update_btn = document.getElementById("<%=btnUpdateDocument.ClientID %>");
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

        if (OnlyClose == "btnCloseDocument") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Document Form ?";
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

                        if (OnlyClose == "btnCloseDocument")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCloseDocument))%>;
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

        if (btn_clientid == "btnUpdateDocument")
        {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Document Details ?";
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

                        if (btn_clientid == "btnSaveDocument")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveDocument))%>;
                        }

                        if (btn_clientid == "btnUpdateDocument") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateDocument))%>;
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
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/AllImage/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" Document detail are save successfully. ">
     </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/AllImage/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" Document detail are update successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
     <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Document Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

<asp:Panel ID="PnlGridDocument" runat="server">
   <asp:Panel ID="addPanel" runat="server">
        <div id="submenu" style="float:left;">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddDocument" 
                                    runat="server" 
                                    Height="35px" 
                                    Text=" Add Document" 
                                    onclick="btnAddDocument_Click" 
                                    ForeColor="White">
                    </asp:LinkButton>
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
					<span class="hidden-480"> Document List </span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
      
    <div class="portlet-body flip-scroll">

        <asp:GridView ID="gvDocument" runat="server" 
                  CssClass="table-bordered table-striped table-condensed flip-content" 
                  AutoGenerateColumns="false" width="100%"
                  ShowHeaderWhenEmpty="true" 
                  AllowPaging="true" PageSize="10"
                  EmptyDataText="No Records Found" 
                  EmptyDataRowStyle-ForeColor="Red" 
                  onpageindexchanging="gvDocument_PageIndexChanging"
                  OnRowCommand="gvDocument_RowCommand" 
                  DataKeyNames ="RegistrationDocId">
        <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
        <Columns>

         <asp:TemplateField HeaderText="RegistrationDocId" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                    Visible="false" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                        <asp:Label ID="lblRegistrationDocId" runat="server" Text='<%#Eval("RegistrationDocId") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
         </asp:TemplateField>

           <asp:TemplateField HeaderText="Doc. Type" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="22px" 
                               ItemStyle-Width="20px">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">                    
                        <asp:Label ID="lblRegistrationDocName" runat="server" Text='<%#Eval("RegistrationDocName") %>' ToolTip=" Registration Doc. Type">
                        </asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

              <asp:TemplateField HeaderText="Doc. No." ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="22px" 
                               ItemStyle-Width="20px">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">                    
                        <asp:Label ID="lblRegistrationDocNumber" runat="server" Text='<%#Eval("RegistrationDocNumber") %>' ToolTip=" Registration Doc. Number">
                        </asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Date Of Issue" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="22px" 
                               ItemStyle-Width="20px">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">                    
                        <asp:Label ID="lblRegistrationDocDateOfIssue" runat="server" Text='<%#Eval("RegistrationDocDateOfIssue") %>' ToolTip="Date Of Issue">
                        </asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

               <asp:TemplateField HeaderText="Date Of Expiry" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="22px" 
                               ItemStyle-Width="20px">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">                    
                        <asp:Label ID="lblRegistrationDocDateOfExpiry" runat="server" Text='<%#Eval("RegistrationDocDateOfExpiry") %>' ToolTip="Date Of Expiry">
                        </asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Download" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" ItemStyle-Width="40px">
					<ItemTemplate>
						<asp:LinkButton ID="DownloadLink" CommandName="download" CommandArgument='<%# Eval("RegistrationDocFile") %>' runat="server" CssClass="form-button">
						   Download
						</asp:LinkButton>
					</ItemTemplate>
				</asp:TemplateField>

            <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                               ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" AutoPostBack="true" 
                                      OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                            <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                            <asp:ListItem Value="Edit">Edit</asp:ListItem>
                            <asp:ListItem Value="Delete">Delete</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlActionRegistrationDocId" runat="server" Text='<%#Eval("RegistrationDocId") %>' Visible="false">
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

<asp:Panel ID="pnlEntryDocument" runat="server" Visible="false">

   <div style="padding:10px 0px;">
        * Note: All Fields marked with an asterisk (*) are required.
   </div>
    
	<!-- BEGIN SAMPLE FORM PORTLET-->   
	<div class="portlet box blue tabbable">
		<div class="portlet-title">
			<div class="caption">
				<i class="icon-reorder"></i>
				<span class="hidden-480"> Document Details</span>
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
                   <asp:Label ID="lblDocumentType" runat="server" Text=" Document Type :" ></asp:Label>
             </label>
              <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="medium m-wrap">
                  </asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Document Type,"
                                                ControlToValidate="ddlDocumentType" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select Document Type Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
             </div>
        </div>
                        
        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblRegistrationDocNumber" runat="server" Text=" Document No. :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtRegistrationDocNumber" runat="server" CssClass="m-wrap large"/>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                    Display="Static" ControlToValidate="txtRegistrationDocNumber"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                     <asp:CustomValidator ID="cvtxtRegistrationDocNumber" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtRegistrationDocNumber" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

      <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblRegistrationDocCountryOfIssue" runat="server" Text=" Country Of Issue :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtRegistrationDocCountryOfIssue" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="100px"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6"
                                                    Display="Static" ControlToValidate="txtRegistrationDocCountryOfIssue"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,300}$" 
                                                    runat="server" ErrorMessage="Maximum 300 characters allowed.">
                    </asp:RegularExpressionValidator>  
                 <asp:CustomValidator ID="cvtxtRegistrationDocCountryOfIssue" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtRegistrationDocCountryOfIssue" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>

          <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblRegistrationDocDateOfIssue" runat="server" Text=" Date Of Issue :"></asp:Label>
             </label>
             <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">   
                  <asp:TextBox ID="txtRegistrationDocDateOfIssue" runat="server" CssClass="datetimepicker m-wrap medium"/>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                 Display="Static" ControlToValidate="txtRegistrationDocDateOfIssue"  
                                                 ValidationGroup="Sports" CssClass="errorfordnn"
                                                 ValidationExpression = "^[\s\S]{0,25}$" 
                                                 runat="server" ErrorMessage="Maximum 25 characters allowed.">
                 </asp:RegularExpressionValidator>   
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Date Of Issue,"
                                                ControlToValidate="txtRegistrationDocDateOfIssue" SetFocusOnError="true"  
                                                ValidationGroup="Sports" Text="Select Date Of Issue Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
                 <asp:CustomValidator ID="CustomValidator1" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtRegistrationDocDateOfIssue" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div> 
        </div>

                <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblRegistrationDocDateOfExpiry" runat="server" Text=" Date Of Expiry :"></asp:Label>
             </label>
                    <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">   
                  <asp:TextBox ID="txtRegistrationDocDateOfExpiry" runat="server" CssClass="enddatetimepicker m-wrap medium"/>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                 Display="Static" ControlToValidate="txtRegistrationDocDateOfExpiry"  
                                                 ValidationGroup="Sports" CssClass="errorfordnn"
                                                 ValidationExpression = "^[\s\S]{0,25}$" 
                                                 runat="server" ErrorMessage="Maximum 25 characters allowed.">
                 </asp:RegularExpressionValidator>   
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Date Of Expiry,"
                                                ControlToValidate="txtRegistrationDocDateOfExpiry" SetFocusOnError="true"  
                                                ValidationGroup="Sports" Text="Select Date Of Expiry Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
                   <asp:CustomValidator ID="CustomValidator2" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtRegistrationDocDateOfExpiry" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div> 
        </div>

        <div class="control-group">
		    <label class="control-label">
                Upload Document :
              </label>    
                <div class="startsetallfrom">
                        <span class="help-inline"><font Color="red"><b>*</b></font></span>
                  </div>
            <div class="controls" style="position:relative;">
              <asp:FileUpload ID="DocumentFileUpload" runat="server"  />
                  <asp:RequiredFieldValidator ID="DocumentFileRequiredFieldValidator" ControlToValidate="DocumentFileUpload" 
                                                runat="server" ErrorMessage="Document File"  ValidationGroup="Sports"  
                                                Text="Document File Required !" ClientIDMode="Static" CssClass="errorfordnn">
                  </asp:RequiredFieldValidator>
              
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.doc|.docx|.pdf)$"
                                ControlToValidate="DocumentFileUpload" runat="server" ForeColor="Red" 
                                ErrorMessage="File select a valid Word or PDF File."
                                CssClass="errorfordnn" ClientIDMode="Static"/>
            </div>
       </div>

       </div>
       <div class="form-actions">
                    
        <div class="right_div_css">

               <asp:Button id="btnSaveDocument" runat="server" Width="100px" Text="Save" ClientIDMode="Static"
                                onclick="btnSaveDocument_Click" ValidationGroup="Sports" 
                                OnClientClick="return validateAndConfirm(this.id);"
                                CssClass="btn blue"/>

             <asp:Button id="btnUpdateDocument" runat="server" Width="100px" Text="Update"  ClientIDMode="Static"
                                onclick="btnUpdateDocument_Click" Visible="false" 
                                OnClientClick="return validateAndConfirm(this.id);"
                                CssClass="btn red"  ValidationGroup="Sports"/>        

             <asp:Button id="btnCloseDocument" runat="server" Width="100px"  Text="Cancel" 
                                onclick="btnCloseDocument_Click" CssClass="btn" ClientIDMode="Static" ValidationGroup="CloseSports"
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
        image.src = "\\DesktopModules\\ThSport\\Images\\AllImage\\1_pix.png";
        return true;
    }
</script>
