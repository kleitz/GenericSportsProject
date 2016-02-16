<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmSeason.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmSeason" %>

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
                if (confirm('Are you sure to delete this Season?')) {
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
    function previewFile() {
        var preview = document.querySelector('#<%=SeasonLogoImage.ClientID %>');
        var file = document.querySelector('#<%=SeasonLogoFile.ClientID %>').files[0];
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
    function textBoxOnBlur(elementRef, id) {
        var checkValue = new String(elementRef.value);
        var save_btn = document.getElementById("<%=btnSaveSeason.ClientID %>");
        var update_btn = document.getElementById("<%=btnUpdateSeason.ClientID %>");
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
        if (OnlyClose == "btnCloseSeason") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Season Form ?";
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

                        if (OnlyClose == "btnCloseSeason") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCloseSeason))%>;
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

        if (btn_clientid == "btnUpdateSeason") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Season Details ?";
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

                        if (btn_clientid == "btnSaveSeason")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveSeason))%>;
                        }

                        if (btn_clientid == "btnUpdateSeason")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateSeason))%>;
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
    function SeasonSaveSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { SeasonsavevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function SeasonsavevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divSeasonsavemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divSeasonsavemassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
</script>

<script type="text/javascript">
    function SeasonUpdateSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { SeasonupdatevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function SeasonupdatevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divSeasonupdatemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divSeasonupdatemassage").delay(2000).fadeOut(0);
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

<div id="divSeasonsavemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" Season detail are save successfully. ">
     </asp:Label>
</div>

<div id="divSeasonupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" Season detail are update successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
     <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Season Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

<asp:Panel ID="PnlGridSeason" runat="server">
   <asp:Panel ID="addPanel" runat="server">
        <div id="submenu" style="float:left;">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddSeason" 
                                    runat="server" 
                                    Height="35px" 
                                    Text=" Add Season " 
                                    onclick="btnAddSeason_Click" 
                                    ForeColor="White">
                    </asp:LinkButton>
                </li>
            </ul>
        </div>

        <div class="teams-search-area">
            <asp:TextBox ID="txtSeasonNameSearch" runat="server"  placeholder=" Enter Season Name" Width="250px" CssClass="m-wrap medium" 
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
					<span class="hidden-480">Season List</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
      
    <div class="portlet-body flip-scroll">

    <asp:GridView ID="gvSeason" runat="server" 
                  CssClass="table-bordered table-striped table-condensed flip-content" 
                  AutoGenerateColumns="false" width="100%"
                  ShowHeaderWhenEmpty="true" 
                  AllowPaging="true" PageSize="10"
                  EmptyDataText="No Records Found" 
                  EmptyDataRowStyle-ForeColor="Red" 
                  onpageindexchanging="gvSeason_PageIndexChanging"
                  DataKeyNames ="SeasonID">
        <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
        <Columns>

         <asp:TemplateField HeaderText="SeasonID" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                    Visible="false" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                        <asp:Label ID="lblSeasonID" runat="server" Text='<%#Eval("SeasonID") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
         </asp:TemplateField>

         <asp:BoundField DataField="SeasonName" HeaderText="Season Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-Width="50%" ItemStyle-CssClass="grid-column" HeaderStyle-Width="30%" />

            <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="grid-header-column" HeaderStyle-VerticalAlign="Middle" 
                          ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="35%" HeaderStyle-Width="100px" Visible="false">
                <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("SeasonDesc") %>'></asp:Label>
                        <asp:LinkButton ID="lbSeasonName" CommandName="Seasonedit" runat="server" CommandArgument='<%#Eval("SeasonID")%>' Text='<%#Eval("SeasonName")%>' ToolTip='<%#Eval("SeasonName")%>' Visible="false"></asp:LinkButton>
		        </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Start Date" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="22px" 
                               ItemStyle-Width="15%">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">                    
                        <asp:Label ID="lblSeasonStartDate" runat="server" Text='<%#Eval("StartDate") %>' ToolTip="Season Start Date">
                        </asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="End Date" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="22px" 
                               ItemStyle-Width="15%">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
                         <asp:Label ID="lblSeasonEndDate" runat="server" Text='<%#Eval("EndDate") %>' ToolTip="Season End Date">
                         </asp:Label>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Description" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" Visible="false">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
                        <asp:Label ID="lblSeasonDesc" runat="server" Text='<%#Eval("SeasonDesc") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

<<<<<<< HEAD
            <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                               ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
=======
            <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                               ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
>>>>>>> e30811f3c2ffc00862a36ba4ea022b3a618a0e42
                <ItemTemplate>
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" AutoPostBack="true" 
                                      OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                            <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                            <asp:ListItem Value="Edit">Edit</asp:ListItem>
                            <%--<asp:ListItem Value="Delete">Delete</asp:ListItem>--%>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlActionSeasonID" runat="server" Text='<%#Eval("SeasonID") %>' Visible="false">
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

<asp:Panel ID="mainContentSeason" runat="server" Visible="false">

   <div style="padding:10px 0px;">
        * Note: All Fields marked with an asterisk (*) are required.
   </div>
    
	<!-- BEGIN SAMPLE FORM PORTLET-->   
	<div class="portlet box blue tabbable">
		<div class="portlet-title">
			<div class="caption">
				<i class="icon-reorder"></i>
				<span class="hidden-480"> Season Detail</span>
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
                   <asp:Label ID="lblCountry" runat="server" Text=" Country :" ></asp:Label>
             </label>
              <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlCountry" runat="server" CssClass="medium m-wrap"/>
                  <asp:RequiredFieldValidator ID="RFVSeasonSport" runat="server" ErrorMessage="Country Name,"
                                                ControlToValidate="ddlCountry" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select Country Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
             </div>
        </div>
            
        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblSeasonTitle" runat="server" Text=" Season Name :" ></asp:Label>
             </label>
             <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtSeasonTitle" runat="server" CssClass="m-wrap large"/>
                   <asp:RequiredFieldValidator ID="rfvtxtSeasonTitle" runat="server" ErrorMessage="Season Title"  
                                                ControlToValidate="txtSeasonTitle" SetFocusOnError="true" 
                                                ValidationGroup="Sports" Text=" Season Title Required !" CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="rgvtxtSeasonTitle"
                                                    Display="Static" ControlToValidate="txtSeasonTitle"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="cvtxtSeasonTitle" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSeasonTitle" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblSeasonDescription" runat="server" Text="Description :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtSeasonDescription" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px"/>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator6"
                                                    Display="Static" ControlToValidate="txtSeasonDescription"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,300}$" 
                                                    runat="server" ErrorMessage="Maximum 300 characters allowed.">
                    </asp:RegularExpressionValidator>  
                 <asp:CustomValidator ID="cvtxtSeasonDescription" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSeasonDescription" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>

              <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblSeasonStartDate" runat="server" Text="Start Date :" ></asp:Label>
            </label>
                  <div class="startsetallfrom">
                      <span class="help-inline"><font Color="red"><b>*</b></font></span>
                  </div>
            <div class="controls" style="position:relative;">  
                <asp:TextBox ID="txtSeasonStartDate" runat="server"  CssClass="datetimepicker m-wrap medium onlynumeric"/>
                <asp:RequiredFieldValidator ID="rfvtxtSeasonStartDate" runat="server" ErrorMessage="Enter Start Date"
                                                 ControlToValidate="txtSeasonStartDate" SetFocusOnError="true" 
                                                 ValidationGroup="Sports" Text="Start Date Required !" CssClass="errorfordnn" 
                                                 ClientIDMode="Static"/>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                                 Display="Static" ControlToValidate="txtSeasonStartDate"  
                                                 ValidationGroup="Sports" CssClass="errorfordnn"
                                                 ValidationExpression = "^[\s\S]{0,25}$" 
                                                 runat="server" ErrorMessage="Maximum 25 characters allowed.">
                 </asp:RegularExpressionValidator>  
                 <asp:CustomValidator ID="cvtxtSeasonStartDate" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSeasonStartDate" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
            </div>
        </div>
    
        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblSeasonEndDate" runat="server" Text="End Date :"></asp:Label>
             </label>
             <div class="controls" style="position:relative;">   
                  <asp:TextBox ID="txtSeasonEndDate" runat="server" CssClass="enddatetimepicker m-wrap medium"/>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                                 Display="Static" ControlToValidate="txtSeasonEndDate"  
                                                 ValidationGroup="Sports" CssClass="errorfordnn"
                                                 ValidationExpression = "^[\s\S]{0,25}$" 
                                                 runat="server" ErrorMessage="Maximum 25 characters allowed.">
                 </asp:RegularExpressionValidator>   
                   <asp:CustomValidator ID="CustomValidator1" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSeasonEndDate" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div> 
        </div>

        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblSeasonLogoName" runat="server" Text=" Photo Name :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtSeasonLogoName" runat="server" CssClass="m-wrap large" ClientIDMode="Static"/>
                   <asp:CustomValidator ID="CustomValidator2" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSeasonLogoName" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>   

        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblUploadPhoto" runat="server" Text="Upload Photo : "></asp:Label>
             </label>
            <div class="controls" style="position:relative;">  
                <input ID="SeasonLogoFile" type="file" name="file" runat="server" onchange="previewFile()"/>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" 
                                                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$"
                                                ControlToValidate="SeasonLogoFile" ValidationGroup="Sports" 
                                                runat="server" ForeColor="Red" 
                                                ErrorMessage="Please choose only .jpg, .png and .gif images!"
                                                CssClass ="errorfordnn" />
                <div style="padding-top:10px;border:none; Width:200px;">
                    <asp:Image ID="SeasonLogoImage" runat="server" onError="imgError(this);"/>
                </div>
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

       <div class="form-actions">
                    
        <div class="right_div_css">
               <asp:Button id="btnSaveSeason" runat="server" Width="100px" Text="Save" ClientIDMode="Static"
                         onclick="btnSaveSeason_Click" ValidationGroup="Sports" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn blue"/>

             <asp:Button id="btnUpdateSeason" runat="server" Width="100px" Text="Update"  ClientIDMode="Static"
                         onclick="btnUpdateSeason_Click" Visible="false" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn red"  ValidationGroup="Sports"/>        

             <asp:Button id="btnCloseSeason" runat="server" Width="100px"  Text="Cancel" 
                         onclick="btnCloseSeason_Click" CssClass="btn" ClientIDMode="Static" ValidationGroup="CloseSports"
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