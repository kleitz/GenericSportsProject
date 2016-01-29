<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmSponsor.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmSponsor" %>

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
    function previewFilelogo()
    {
        var preview = document.querySelector('#<%=SponsorLogoImage.ClientID %>');
        var file = document.querySelector('#<%=SponsorLogoFile.ClientID %>').files[0];
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
    $(document).ready(function () {
        $('.ddlActionSelect').change(function (evt) {
            evt.preventDefault();
            if ($(this).val() == "Delete") {
                if (confirm('Are you sure to delete this Sponsor ?')) {
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
    function textBoxOnBlur(elementRef, id)
    {
        var checkValue = new String(elementRef.value);
        var save_btn = document.getElementById("<%=btnSaveSponsor.ClientID %>");
        var update_btn = document.getElementById("<%=btnUpdateSponsor.ClientID %>");
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

    function validateAndConfirmClose(OnlyClose)
    {
        var validated = Page_ClientValidate('CloseSports');
        if (OnlyClose == "btnCloseSponsor") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Sponsor Form ?";
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

                        if (OnlyClose == "btnCloseSponsor")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCloseSponsor))%>;
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

        if (btn_clientid == "btnUpdateSponsor")
        {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Sponsor Details ?";
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

                        if (btn_clientid == "btnSaveSponsor")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveSponsor))%>;
                        }

                        if (btn_clientid == "btnUpdateSponsor")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateSponsor))%>;
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
    function SaveSuccessfully()
    {
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
    function savevalidateAndConfirmClose()
    {
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
    function UpdateSuccessfully()
    {
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
    function updatevalidateAndConfirmClose()
    {
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
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" Sponsor detail are save successfully. ">
     </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/AllImage/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" Sponsor detail are update successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
     <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Sponsor Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

<asp:Panel ID="PnlGridSponsor" runat="server">
   <asp:Panel ID="addPanel" runat="server">
        <div id="submenu" style="float:left;">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddSponsor" 
                                    runat="server" 
                                    Height="35px" 
                                    Text=" Add Sponsor " 
                                    onclick="btnAddSponsor_Click" 
                                    ForeColor="White">
                    </asp:LinkButton>
                </li>
            </ul>
        </div>

        <div class="teams-search-area">
            <asp:TextBox ID="txtSponsorNameSearch" runat="server"  placeholder=" Enter Sponsor Name" Width="250px" CssClass="m-wrap medium" 
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
					<span class="hidden-480">Sponsor List</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
      
    <div class="portlet-body flip-scroll">

    <asp:GridView ID="gvSponsor" runat="server" 
                  CssClass="table-bordered table-striped table-condensed flip-content" 
                  AutoGenerateColumns="false" width="100%"
                  ShowHeaderWhenEmpty="true" 
                  AllowPaging="true" PageSize="10"
                  EmptyDataText="No Records Found" 
                  EmptyDataRowStyle-ForeColor="Red" 
                  onpageindexchanging="gvSponsor_PageIndexChanging"
                  DataKeyNames ="SponsorId">
        <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
        <Columns>

         <asp:TemplateField HeaderText="SponsorId" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                    Visible="false" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                        <asp:Label ID="lblSponsorId" runat="server" Text='<%#Eval("SponsorId") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
         </asp:TemplateField>

            <asp:TemplateField HeaderText="Sponsor Name" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                     HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                        <asp:Label ID="lblSponsorName" runat="server" Text='<%#Eval("SponsorName") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
         </asp:TemplateField>

         <asp:TemplateField HeaderText="Sponsor Type" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                    HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                        <asp:Label ID="lblSponsorType" runat="server" Text='<%#Eval("SponsorTypeValue") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
         </asp:TemplateField>

            <asp:TemplateField HeaderText="Sponsor Level" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                     HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                        <asp:Label ID="lblSponsorLevel" runat="server" Text='<%#Eval("SponsorLevelValue") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
         </asp:TemplateField>

            <asp:TemplateField HeaderText="Start Date" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="22px" 
                               ItemStyle-Width="20px">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">                    
                        <asp:Label ID="lblSponsorStartDateTime" runat="server" Text='<%#Eval("SponsorStartDate") %>' ToolTip="Sponsor Start Date">
                        </asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="End Date" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="22px" 
                               ItemStyle-Width="20px">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
                         <asp:Label ID="lblSponsorEndDateTime" runat="server" Text='<%#Eval("SponsorEndDate") %>' ToolTip="Sponsor End Date">
                         </asp:Label>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Amount" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="grid-header-column" 
                               ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
                        <asp:Label ID="lblSponsorAmount" runat="server" Text='<%#Eval("SponsorAmt") %>'></asp:Label>
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
                    <asp:Label ID="lblddlActionSponsorID" runat="server" Text='<%#Eval("SponsorId") %>' Visible="false">
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

<asp:Panel ID="pnlSponsorEntry" runat="server" Visible="false">

   <div style="padding:10px 0px;">
        * Note: All Fields marked with an asterisk (*) are required.
   </div>
    
	<!-- BEGIN SAMPLE FORM PORTLET-->   
	<div class="portlet box blue tabbable">
		<div class="portlet-title">
			<div class="caption">
				<i class="icon-reorder"></i>
				<span class="hidden-480"> Sponsor Detail</span>
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
                   <asp:Label ID="lblPlayer" runat="server" Text="  Player :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlPlayer" runat="server" CssClass="medium m-wrap"/>
             </div>
        </div>

        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblSponsorLevel" runat="server" Text=" Sponsor Level :" ></asp:Label>
             </label>
            <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlSponsorLevel" runat="server" CssClass="medium m-wrap"/>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="SponsorLevel"  
                                                ControlToValidate="ddlSponsorLevel" SetFocusOnError="true" InitialValue="0" 
                                                ValidationGroup="Sports" Text=" Sponsor Level Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
             </div>
        </div>

        <div class="control-group">
		     <label class="control-label">
                   <asp:Label ID="lblSponsorType" runat="server" Text=" Sponsor Type :" ></asp:Label>
             </label>
            <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:DropDownList ID="ddlSponsorType" runat="server" CssClass="medium m-wrap"/>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="SponsorType"  
                                                ControlToValidate="ddlSponsorType" SetFocusOnError="true" InitialValue="0" 
                                                ValidationGroup="Sports" Text=" Sponsor Type Required !" CssClass="errorfordnn" 
                                                ClientIDMode="Static"/>
             </div>
        </div>
            
        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblSponsorName" runat="server" Text=" Sponsor Name :" ></asp:Label>
             </label>
             <div class="startsetallfrom">
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
             </div>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtSponsorName" runat="server" CssClass="m-wrap large"/>
                   <asp:RequiredFieldValidator ID="rfvtxtSponsorName" runat="server" ErrorMessage="SponsorName"  
                                                ControlToValidate="txtSponsorName" SetFocusOnError="true" 
                                                ValidationGroup="Sports" Text=" Sponsor Name Required !" CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="rgvtxtSponsorName"
                                                    Display="Static" ControlToValidate="txtSponsorName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="cvtxtSponsorName" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSponsorName" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblSponsorAbbreviation" runat="server" Text="Abbreviation :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtSponsorAbbreviation" runat="server" CssClass="m-wrap small"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                    Display="Static" ControlToValidate="txtSponsorAbbreviation"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,5}$" 
                                                    runat="server" ErrorMessage="Maximum 5 characters allowed.">
                    </asp:RegularExpressionValidator>  
                 <asp:CustomValidator ID="CustomValidator3" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSponsorAbbreviation" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblSponsorDetail" runat="server" Text=" Description :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtSponsorDetail" runat="server" CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                    Display="Static" ControlToValidate="txtSponsorDetail"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,300}$" 
                                                    runat="server" ErrorMessage="Maximum 300 characters allowed.">
                    </asp:RegularExpressionValidator>  
                 <asp:CustomValidator ID="cvtxtSponsorDetail" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSponsorDetail" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
           </div>
        </div>

        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblSponsorLogoName" runat="server" Text=" Logo Name :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtSponsorLogoName" runat="server" CssClass="m-wrap large"/>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                                                    Display="Static" ControlToValidate="txtSponsorLogoName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="CustomValidator4" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSponsorLogoName" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblSponsorLogo" runat="server" Text=" Photo : "></asp:Label>
             </label>
            <div class="controls" style="position:relative;">  
                <input ID="SponsorLogoFile" type="file" name="file" runat="server" onchange="previewFilelogo()"/>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" 
                                                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$"
                                                ControlToValidate="SponsorLogoFile" ValidationGroup="Sports" 
                                                runat="server" ForeColor="Red" 
                                                ErrorMessage="Please choose only .jpg, .png and .gif images!"
                                                CssClass ="errorfordnn" />
                <div style="padding-top:10px;border:none; Width:200px;">
                    <asp:Image ID="SponsorLogoImage" runat="server" onError="imgError(this);"/>
                </div>
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
                <asp:TextBox ID="txtSponsorStartDate" runat="server" CssClass="datetimepicker m-wrap medium onlynumeric"/>
                <asp:RequiredFieldValidator ID="rfvtxtSponsorStartDate" runat="server" ErrorMessage="Enter Start Date"
                                                 ControlToValidate="txtSponsorStartDate" SetFocusOnError="true" 
                                                 ValidationGroup="Sports" Text="Start Date Required !" CssClass="errorfordnn" 
                                                 ClientIDMode="Static"/>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                                 Display="Static" ControlToValidate="txtSponsorStartDate"  
                                                 ValidationGroup="Sports" CssClass="errorfordnn"
                                                 ValidationExpression = "^[\s\S]{0,25}$" 
                                                 runat="server" ErrorMessage="Maximum 25 characters allowed.">
                 </asp:RegularExpressionValidator>  
                <asp:CustomValidator ID="CustomValidator1" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSponsorStartDate" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
            </div>
        </div>
    
        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblSponsorEndDate" runat="server" Text="End Date :"></asp:Label>
             </label>
              <div class="startsetallfrom">
                      <span class="help-inline"><font Color="red"><b>*</b></font></span>
                  </div>
             <div class="controls" style="position:relative;">   
                  <asp:TextBox ID="txtSponsorEndDate" runat="server" CssClass="enddatetimepicker m-wrap medium"/>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter End Date"
                                                 ControlToValidate="txtSponsorEndDate" SetFocusOnError="true" 
                                                 ValidationGroup="Sports" Text="End Date Required !" CssClass="errorfordnn" 
                                                 ClientIDMode="Static"/>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                                 Display="Static" ControlToValidate="txtSponsorEndDate"  
                                                 ValidationGroup="Sports" CssClass="errorfordnn"
                                                 ValidationExpression = "^[\s\S]{0,25}$" 
                                                 runat="server" ErrorMessage="Maximum 25 characters allowed.">
                 </asp:RegularExpressionValidator>   
                 <asp:CustomValidator ID="CustomValidator2" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSponsorEndDate" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                 CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                   </asp:CustomValidator>
             </div> 
        </div>

        <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblSponsorAmount" runat="server" Text=" Amount :" ></asp:Label>
             </label>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtSponsorAmount" runat="server" CssClass="m-wrap small" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;" />
                    <span id="error" style="color: Red; display: none">* Input digits (0 - 9)</span>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                    Display="Static" ControlToValidate="txtSponsorAmount"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,10}$" 
                                                    runat="server" ErrorMessage="Maximum 10 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="CustomValidator5" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                 ControlToValidate="txtSponsorAmount" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
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

               <asp:Button id="btnSaveSponsor" runat="server" Width="100px" Text="Save" ClientIDMode="Static"
                         onclick="btnSaveSponsor_Click" ValidationGroup="Sports" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn blue"/>

             <asp:Button id="btnUpdateSponsor" runat="server" Width="100px" Text="Update"  ClientIDMode="Static"
                         onclick="btnUpdateSponsor_Click" Visible="false" 
                         OnClientClick="return validateAndConfirm(this.id);"
                         CssClass="btn red"  ValidationGroup="Sports"/>        

             <asp:Button id="btnCloseSponsor" runat="server" Width="100px"  Text="Cancel" 
                         onclick="btnCloseSponsor_Click" CssClass="btn" ClientIDMode="Static" ValidationGroup="CloseSports"
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

 <script type="text/javascript">
     var specialKeys = new Array();
     specialKeys.push(8); //Backspace
     function IsNumeric(e) {
         var keyCode = e.which ? e.which : e.keyCode
         var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
         document.getElementById("error").style.display = ret ? "none" : "inline";
         return ret;
     }
    </script>