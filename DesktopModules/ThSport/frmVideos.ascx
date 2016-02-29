<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmVideos.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmVideos" %>

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
        function previewFile() {
            var file = document.querySelector('#<%=VideoLogoFile.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            }
            else {
                preview.src = "";
            }
        }
    </script>

<script type="text/javascript">
    $(document).ready(function () {
        $('.ddlActionSelect').change(function (evt) {
            evt.preventDefault();
            if ($(this).val() == "Delete") {
                if (confirm('Are you sure to delete this Video ?')) {
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
        var save_btn = document.getElementById("<%=btnSaveVideo.ClientID %>");
        var update_btn = document.getElementById("<%=btnUpdateVideo.ClientID %>");
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

        if (OnlyClose == "btnCloseVideo") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Video Form ?";
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

                        if (OnlyClose == "btnCloseVideo") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCloseVideo))%>;
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

        if (btn_clientid == "btnUpdateVideo")
        {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Video Details ?";
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

                        if (btn_clientid == "btnSaveVideo")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveVideo))%>;
                        }

                        if (btn_clientid == "btnUpdateVideo")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateVideo))%>;
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
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" Video detail are save successfully. ">
     </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" Video detail are update successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
     <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Video Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

<asp:Panel ID="PnlGridVideo" runat="server">
   <asp:Panel ID="addPanel" runat="server">
        <div id="submenu" style="float:left;">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddVideo" 
                                    runat="server" 
                                    Height="35px" 
                                    Text=" Add Video " 
                                    onclick="btnAddVideo_Click" 
                                    ForeColor="White">
                    </asp:LinkButton>
                </li>
            </ul>
        </div>

        <div class="teams-search-area">
            
        </div>
   </asp:Panel>

     <!-- Html Table -->
        <!-- End Html Table -->
    
		<!-- BEGIN SAMPLE FORM PORTLET-->   
		<div class="portlet box green">
			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480">Video List</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
      
    <div class="portlet-body flip-scroll">

    <asp:GridView ID="gvVideo" runat="server" 
                  CssClass="table-bordered table-striped table-condensed flip-content" 
                  AutoGenerateColumns="false" width="100%"
                  ShowHeaderWhenEmpty="true" 
                  AllowPaging="true" PageSize="10"
                  EmptyDataText="No Records Found" 
                  EmptyDataRowStyle-ForeColor="Red" 
                  onpageindexchanging="gvVideo_PageIndexChanging"
                  DataKeyNames ="VideoId">
        <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
        <Columns>

         <asp:TemplateField HeaderText="VideoId" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                    Visible="false" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                        <asp:Label ID="lblVideoId" runat="server" Text='<%#Eval("VideoId") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
         </asp:TemplateField>

            <asp:TemplateField HeaderText="Video Title" HeaderStyle-CssClass="grid-header-column" 
                                      ItemStyle-CssClass="grid-column" ItemStyle-Width="55%" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                        <asp:Literal ID="lblVideoTitle" runat="server" Text='<%#Eval("VideoTitle") %>'></asp:Literal>
                </ItemTemplate>
         </asp:TemplateField>

         <asp:TemplateField HeaderText="Video Date" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                    HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-Width="10%">
                <ItemTemplate>
                    <div class="grid-cell-inner">
                        <asp:Label ID="lblVideoDate" runat="server" Text='<%#Eval("VideoDate") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
         </asp:TemplateField>

        <asp:TemplateField HeaderText="Video Level" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                     HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-Width="15%">
                <ItemTemplate>
                    <div class="grid-cell-inner">
                        <asp:Label ID="lblVideoLevel" runat="server" Text='<%#Eval("VideoLevelId") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
         </asp:TemplateField>

            <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                               ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" AutoPostBack="true" 
                                      OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                            <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                            <asp:ListItem Value="Edit">Edit</asp:ListItem>
                            <%--<asp:ListItem Value="Delete">Delete</asp:ListItem>--%>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlActionVideoID" runat="server" Text='<%#Eval("VideoId") %>' Visible="false">
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

<asp:Panel ID="pnlEntryVideo" runat="server" Visible="false">

   <div style="padding:10px 0px;">
        * Note: All Fields marked with an asterisk (*) are required.
   </div>
    
	<!-- BEGIN SAMPLE FORM PORTLET-->   
	<div class="portlet box blue tabbable">
		<div class="portlet-title">
			<div class="caption">
				<i class="icon-reorder"></i>
				<span class="hidden-480"> Video Detail</span>
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
                <div class="startsetallfrom">
                  <span class="help-inline"><font Color="red"><b>*</b></font></span>
                    </div>
                 <div class="controls" style="position:relative;">
                      <asp:DropDownList ID="ddlSports" runat="server" CssClass="medium m-wrap"
                          />
                     <asp:RequiredFieldValidator ID="rfvddlSport" ClientIDMode="Static" runat="server" InitialValue="0" 
                    ErrorMessage="Sport Required !" CssClass="errorfordnn" SetFocusOnError="true" ControlToValidate="ddlSports"
                    ValidationGroup="Sports" Text="Sport Required !"></asp:RequiredFieldValidator>
                 </div>
            </div>

            <div class="control-group">
		         <label class="control-label">
                       <asp:Label ID="lblCountryId" runat="server" Text=" Country :" ></asp:Label>
                 </label>
                 <div class="controls" style="position:relative;">
                      <asp:DropDownList ID="ddlCountry" runat="server" CssClass="medium m-wrap"/>
                 </div>
            </div>

            <div class="control-group">
		         <label class="control-label">
                       <asp:Label ID="lblEvent" runat="server" Text=" Event :" ></asp:Label>
                 </label>
                 <div class="controls" style="position:relative;">
                      <asp:DropDownList ID="ddlEvent" runat="server" CssClass="medium m-wrap"/>
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
                  <asp:DropDownList ID="ddlClub" runat="server" CssClass="medium m-wrap" />
             </div>
        </div>

    <div id="divclubowner" runat="server" >
        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblClubOwner" runat="server" Text="  Club Owner :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlClubOwner" runat="server" CssClass="medium m-wrap"/>
             </div>
        </div>
     </div>

     <div id="divclubmember" runat="server" >
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
                  <asp:DropDownList ID="ddlTeam" runat="server" CssClass="medium m-wrap" />
             </div>
        </div>

            <div id="divteammember" runat="server" >
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
                   <asp:Label ID="lblPlayer" runat="server" Text="  Player :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlPlayer" runat="server" CssClass="medium m-wrap"/>
             </div>
        </div>

        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblSponsor" runat="server" Text=" Sponsor :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlSponsor" runat="server" CssClass="medium m-wrap"/>
             </div>
        </div>

        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblVideoTitle" runat="server" Text=" Video Title :" ></asp:Label>
             </label>
             <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtVideoTitle" runat="server" CssClass="m-wrap large"/>
                   <asp:RequiredFieldValidator ID="rfvtxtVideoTitle" runat="server" ErrorMessage="VideoTitle"  
                                                ControlToValidate="txtVideoTitle" SetFocusOnError="true" 
                                                ValidationGroup="Sports" Text=" Video Title Required !" CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="rgvtxtVideoTitle"
                                                    Display="Static" ControlToValidate="txtVideoTitle"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="cvtxtVideoTitle" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtVideoTitle" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblVideoDesc" runat="server" Text=" Description :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtVideoDesc" runat="server" CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                    Display="Static" ControlToValidate="txtVideoDesc"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,300}$" 
                                                    runat="server" ErrorMessage="Maximum 300 characters allowed.">
                    </asp:RegularExpressionValidator>  
                 <asp:CustomValidator ID="cvtxtVideoDesc" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtVideoDesc" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>

        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblVideoDate" runat="server" Text="Video Date :" ></asp:Label>
            </label>
            <div class="startsetallfrom">
                  <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
            <div class="controls" style="position:relative;">  
                <asp:TextBox ID="txtVideoDate" runat="server" CssClass="datetimepicker m-wrap medium onlynumeric"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Video Date"
                                                 ControlToValidate="txtVideoDate" SetFocusOnError="true" 
                                                 ValidationGroup="Sports" Text="Video Date Required !" CssClass="errorfordnn" 
                                                 ClientIDMode="Static"/>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator8"
                                                 Display="Static" ControlToValidate="txtVideoDate"  
                                                 ValidationGroup="Sports" CssClass="errorfordnn"
                                                 ValidationExpression = "^[\s\S]{0,25}$" 
                                                 runat="server" ErrorMessage="Maximum 25 characters allowed.">
                 </asp:RegularExpressionValidator>  
                <asp:CustomValidator ID="CustomValidator6" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtVideoDate" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
            </div>
        </div>

        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblVideoPriority" runat="server" Text=" Priority :" ></asp:Label>
             </label>
              <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlVideoPriority" runat="server" CssClass="medium m-wrap">
                        <asp:ListItem Text=" -- Select Priority -- " Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="High" Value="High" ></asp:ListItem>
                        <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                  </asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RFVVideoPriority" runat="server" ErrorMessage="Video Priority,"
                                                ControlToValidate="ddlVideoPriority" SetFocusOnError="true"  
                                                ValidationGroup="Sports" 
                                                InitialValue="0" Text="Select Video Priority Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
             </div>
        </div>

     <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblVideoType" runat="server" Text="Video Type :" ></asp:Label>
             </label>
            <div class="controls" style="position:relative;">
                    <asp:DropDownList ID="ddlvideotype" runat="server" Width="320px" Height="34px" 
                                      OnSelectedIndexChanged="ddlvideotype_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="YouTube">YouTube</asp:ListItem>
                            <asp:ListItem Value="Other">Other</asp:ListItem>
                    </asp:DropDownList>
             </div>
     </div>

    <div ID="divvideopath" runat="server">
        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblVideoPath" runat="server" Text="Video Path :" ></asp:Label>
		    </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtVideoPath" runat="server" CssClass="m-wrap large"></asp:TextBox>  
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                              Display="Static" ControlToValidate="txtVideoPath"  
                                              ValidationGroup="Sports" CssClass="errorfordnn"
                                              ValidationExpression = "^[\s\S]{0,30}$" 
                                              runat="server" ErrorMessage="Maximum 30 characters allowed.">
                </asp:RegularExpressionValidator>
           </div>
        </div>
         
        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblVideoPathExample" runat="server" Text=""></asp:Label>
            </label>
            <div class="controls">
                <asp:Label ID="lblVideoPathExample1" runat="server" Text="http://www.youtube.com/watch?v=_____________________" ></asp:Label>
		    </div>
        </div>  
    </div>

    <div id="divOtherVideoPath" runat="server">
        <div class="control-group">
		    <div class="controls" style="position:relative;">
                <input ID="VideoLogoFile" type="file" name="file" runat="server" onchange="previewFile()"/>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
                                                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.flv|.webm|.mkv|.vob|.ogv|.ogg|.avi|.mov|.wmv|.rm|.mp4|.m4p|.m4v|.mpg|.mp2|.mpeg|.mpe|.mpv|.m2v|.m4v|.svi|.3gp|.3g2|.nsv|.asf|.asx|.srt|.swf)$"
                                                ControlToValidate="VideoLogoFile" ValidationGroup="Sports" 
                                                runat="server" ForeColor="Red" 
                                                ErrorMessage="This is not video file!"
                                                CssClass ="errorfordnn" />
            </div>
            <div class="controls">
               <asp:Label ID="lblErrorVideo" runat="server" Text="Video Must be Less Than 10 MB." ForeColor="Red"></asp:Label>
            </div> 
            <div class="controls">
                <iframe id="ifmOtherVideoPath" runat="server" width="200" height="150" src='<%#Eval("VideoOtherFile") %>'></iframe>
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

               <asp:Button id="btnSaveVideo" runat="server" Width="100px" Text="Save" ClientIDMode="Static"
                         onclick="btnSaveVideo_Click" ValidationGroup="Sports" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn blue"/>

             <asp:Button id="btnUpdateVideo" runat="server" Width="100px" Text="Update"  ClientIDMode="Static"
                         onclick="btnUpdateVideo_Click" Visible="false" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn red"  ValidationGroup="Sports"/>        

             <asp:Button id="btnCloseVideo" runat="server" Width="100px"  Text="Cancel" 
                         onclick="btnCloseVideo_Click" CssClass="btn" ClientIDMode="Static" ValidationGroup="CloseSports"
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
