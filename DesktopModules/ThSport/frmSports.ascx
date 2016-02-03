<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmSports.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmSports" %>

<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<script type="text/javascript">
    $(document).ready(function () {
        $('.ddlActionSelect').change(function (evt) {
            evt.preventDefault();
            if ($(this).val() == "Delete") {
                if (confirm('Are you sure to delete this sport?')) {
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
    function previewFilemain() {
        var preview = document.querySelector('#<%=SportMainImage.ClientID %>');
        var file = document.querySelector('#<%=SportMainFile.ClientID %>').files[0];
        var reader = new FileReader();

        reader.onloadend = function ()
        {
            preview.src = reader.result;
        }

        if (file) {
            if (file.size > 10485760) {
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
    function previewFilelogo()
    {
        var preview = document.querySelector('#<%=SportLogoImage.ClientID %>');
        var file = document.querySelector('#<%=SportLogoFile.ClientID %>').files[0];
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
        else
        {
            preview.src = "";
        }
    }
</script>

<script type="text/javascript">
    function previewFilesmall()
    {
        var preview = document.querySelector('#<%=SportSmallImage.ClientID %>');
        var file = document.querySelector('#<%=SportSmallFile.ClientID %>').files[0];
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
        else
        {
            preview.src = "";
        }
    }
</script>

<script type="text/javascript">
    function validateTextBox(sender, args)
    {
        var txtcheckValue = args.Value;

        var chars = ['<', '>', '*', '$', '@', ',', '_', '%', '.','!','#','^','&','(',')','-','=','+','\\','|','?','/','[',']','{','}'];
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
    function textBoxOnBlur(elementRef, id)
    {
        var checkValue = new String(elementRef.value);
        var save_btn = document.getElementById("<%=btnSaveSport.ClientID %>");
        var update_btn = document.getElementById("<%=btnUpdateSport.ClientID %>");
        var chars = ['<', '>', '*', '$', '@', ',', '_', '%'];
        var realted_span = document.getElementById("nameError");

        if (checkValue.length > 0)
        {
            var currentChar = checkValue.charAt(0);

            if (chars.indexOf(currentChar) >= 0)
            {
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

    function validateAndConfirmClose(OnlyClose) {
        var validated = Page_ClientValidate('CloseSports');
        if (OnlyClose == "btnCloseSport") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Sport Form ?";
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

                        if (OnlyClose == "btnCloseSport") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCloseSport))%>;
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

        if (btn_clientid == "btnUpdateSport") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Sport Details ?";
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

                        if (btn_clientid == "btnSaveSport")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveSport))%>;
                        }

                        if (btn_clientid == "btnUpdateSport")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateSport))%>;
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
    function SportSaveSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { SportsavevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function SportsavevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divSportsavemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divSportsavemassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
</script>

<script type="text/javascript">
    function SportUpdateSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { SportupdatevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function SportupdatevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divSportupdatemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divSportupdatemassage").delay(2000).fadeOut(0);
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

<div id="divSportsavemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" Sport detail are save successfully. ">
     </asp:Label>
</div>

<div id="divSportupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" Sport detail are update successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
     <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Sport Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

<asp:Panel ID="PnlGridSport" runat="server">
   <asp:Panel ID="addPanel" runat="server">
        <div id="submenu" style="float:left;">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddSport" 
                                    runat="server" 
                                    Height="35px" 
                                    Text=" Add Sport " 
                                    onclick="btnAddSport_Click" 
                                    ForeColor="White">
                    </asp:LinkButton>
                </li>
            </ul>
        </div>

        <div class="teams-search-area">
            <asp:TextBox ID="txtSportNameSearch" runat="server"  placeholder=" Enter Sport Name" Width="250px" CssClass="m-wrap medium" 
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
					<span class="hidden-480">Sport List</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
      
    <div class="portlet-body flip-scroll">

        <asp:GridView ID="gvSport" runat="server" 
                  CssClass="table-bordered table-striped table-condensed flip-content" 
                  AutoGenerateColumns="false" width="100%"
                  ShowHeaderWhenEmpty="true" 
                  AllowPaging="true" PageSize="10"
                  EmptyDataText="No Records Found" 
                  EmptyDataRowStyle-ForeColor="Red" 
                  onpageindexchanging="gvSport_PageIndexChanging"
                  DataKeyNames ="SportID">
        <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
        <Columns>

         <asp:TemplateField HeaderText="SportID" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                        <asp:Label ID="lblSportID" runat="server" Text='<%#Eval("SportID") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

         <asp:BoundField DataField="SportName" HeaderText="Sport Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-Width="30%" ItemStyle-CssClass="grid-column" HeaderStyle-Width="30%" />

         <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="grid-header-column" HeaderStyle-VerticalAlign="Middle" 
                          ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="35%" HeaderStyle-Width="100px">
                <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("SportDesc") %>'></asp:Label>
                        <asp:LinkButton ID="lbSportName" CommandName="sportedit" runat="server" CommandArgument='<%#Eval("SportID")%>' Text='<%#Eval("SportName")%>' ToolTip='<%#Eval("SportName")%>' Visible="false"></asp:LinkButton>
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
                    <asp:Label ID="lblddlActionSportID" runat="server" Text='<%#Eval("SportID") %>' Visible="false">
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

<asp:Panel ID="pnlSportEntry" runat="server" Visible="false">

   <div style="padding:10px 0px;">
        * Note: All Fields marked with an asterisk (*) are required.
   </div>
    
	<!-- BEGIN SAMPLE FORM PORTLET-->   
	<div class="portlet box blue tabbable">
		<div class="portlet-title">
			<div class="caption">
				<i class="icon-reorder"></i>
				<span class="hidden-480"> Sport Details</span>
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
                   <asp:Label ID="lblSportName" runat="server" Text=" Sport Name :" ></asp:Label>
             </label>
             <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtSportName" runat="server" CssClass="m-wrap large"/>
                <asp:RequiredFieldValidator ID="rfvtxtSportName" runat="server" ErrorMessage="SportName"  
                                                ControlToValidate="txtSportName" SetFocusOnError="true" 
                                                ValidationGroup="Sports" Text=" Sport Name  Required !" CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="rgvtxtSportName"
                                                    Display="Static" ControlToValidate="txtSportName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="cvtxtSportName" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSportName" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblSportDescription" runat="server" Text="Description :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtSportDescription" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                    Display="Static" ControlToValidate="txtSportDescription"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,300}$" 
                                                    runat="server" ErrorMessage="Maximum 300 characters allowed.">
                    </asp:RegularExpressionValidator>  
                  <asp:CustomValidator ID="cvtxtSportDescription" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSportDescription" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>

        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblMainImage" runat="server" Text="Main Photo : "></asp:Label>
             </label>
            <div class="controls" style="position:relative;">  
                <input ID="SportMainFile" type="file" name="file" runat="server" onchange="previewFilemain()"/>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" 
                                                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$"
                                                ControlToValidate="SportMainFile" ValidationGroup="Sports" 
                                                runat="server" ForeColor="Red" 
                                                ErrorMessage="Please choose only .jpg, .png and .gif images!"
                                                CssClass ="errorfordnn" />
                <div style="padding-top:10px;border:none; Width:200px;">
                    <asp:Image ID="SportMainImage" runat="server" onError="imgError(this);"/>
                </div>
            </div>
        </div>

         <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblSportMainImageDesc" runat="server" Text=" Image Description :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtMainImageDesc" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="100px"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6"
                                                    Display="Static" ControlToValidate="txtMainImageDesc"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,300}$" 
                                                    runat="server" ErrorMessage="Maximum 300 characters allowed.">
                    </asp:RegularExpressionValidator>  
                <asp:CustomValidator ID="cvtxtMainImageDesc" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtMainImageDesc" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>
       
        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblLogoImage" runat="server" Text="Logo Photo : "></asp:Label>
             </label>
            <div class="controls" style="position:relative;">  
                <input ID="SportLogoFile" type="file" name="file" runat="server" onchange="previewFilelogo()"/>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                                                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$"
                                                ControlToValidate="SportLogoFile" ValidationGroup="Sports" 
                                                runat="server" ForeColor="Red" 
                                                ErrorMessage="Please choose only .jpg, .png and .gif images!"
                                                CssClass ="errorfordnn" />
                <div style="padding-top:10px;border:none; Width:200px;">
                    <asp:Image ID="SportLogoImage" runat="server" onError="imgError(this);"/>
                </div>
            </div>
        </div>
      
        
       <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblSportLogoImageDesc" runat="server" Text=" Logo Description :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtLogoImageDesc" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="100px"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                    Display="Static" ControlToValidate="txtLogoImageDesc"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,300}$" 
                                                    runat="server" ErrorMessage="Maximum 300 characters allowed.">
                    </asp:RegularExpressionValidator>  
                <asp:CustomValidator ID="cvtxtLogoImageDesc" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtLogoImageDesc" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>


         <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblSmallImage" runat="server" Text="Small Photo : "></asp:Label>
             </label>
            <div class="controls" style="position:relative;">  
                <input ID="SportSmallFile" type="file" name="file" runat="server" onchange="previewFilesmall()"/>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
                                                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$"
                                                ControlToValidate="SportSmallFile" ValidationGroup="Sports" 
                                                runat="server" ForeColor="Red" 
                                                ErrorMessage="Please choose only .jpg, .png and .gif images!"
                                                CssClass ="errorfordnn" />
                <div style="padding-top:10px;border:none; Width:200px;">
                    <asp:Image ID="SportSmallImage" runat="server" onError="imgError(this);"/>
                </div>
            </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblSportSmallImageDesc" runat="server" Text=" Small Img Description :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtSmallImageDesc" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="100px"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8"
                                                    Display="Static" ControlToValidate="txtSmallImageDesc"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,300}$" 
                                                    runat="server" ErrorMessage="Maximum 300 characters allowed.">
                    </asp:RegularExpressionValidator>  
                <asp:CustomValidator ID="cvtxtSmallImageDesc" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSmallImageDesc" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
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

       <div class="control-group">
		    <label class="control-label">
            <asp:Label ID="lblShow" runat="server" Text=" Is Show :"></asp:Label>
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
       </div>

       <div class="form-actions">
                    
        <div class="right_div_css">

             <asp:Button id="btnSaveSport" runat="server" Width="100px" Text="Save" ClientIDMode="Static"
                         onclick="btnSaveSport_Click" ValidationGroup="Sports" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn blue"/>

             <asp:Button id="btnUpdateSport" runat="server" Width="100px" Text="Update"  ClientIDMode="Static"
                         onclick="btnUpdateSport_Click" Visible="false" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn red"  ValidationGroup="Sports"/>        

             <asp:Button id="btnCloseSport" runat="server" Width="100px"  Text="Cancel" 
                         onclick="btnCloseSport_Click" CssClass="btn" ClientIDMode="Static" ValidationGroup="CloseSports"
                         OnClientClick="return validateAndConfirmClose(this.id);"/>        

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
    function imgError(image) {
        image.onerror = "";
        image.src = "\\DesktopModules\\ThSport\\Images\\OtherImages\\1_pix.png";
        return true;
    }
</script>