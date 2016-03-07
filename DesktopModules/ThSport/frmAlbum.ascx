<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmAlbum.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmAlbum" %>



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
    function previewFilelogo() {
        var preview = document.querySelector('#<%=PictureLogoImage.ClientID %>');
        var file = document.querySelector('#<%=PictureLogoFile.ClientID %>').files[0];
        var btn = document.querySelector('#<%=btnAddPicture.ClientID%>');
        var reader = new FileReader();
        console.log(btn);
        reader.onloadend = function () {
            preview.src = reader.result;
            
        }
      
        if (file) {
            if (file.size > 10485760) {
                document.getElementById('dvMsg').style.display = "block";
                preview.src = "";
               
            }
            reader.readAsDataURL(file);
            console.log(file);
        }
        else {
            preview.src = "";
        }
    }
</script>

<script type="text/javascript">
    function previewFile() {
        var file = document.querySelector('#<%=AlbumLogoFile.ClientID %>').files[0];
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
function validateAndConfirmClose(OnlyClose) {
        var validated = Page_ClientValidate('CloseSports');

        if (OnlyClose == "btnCloseAlbum") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Album Form ?";
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

                        if (OnlyClose == "btnCloseAlbum") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCloseAlbum))%>;
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

        if (btn_clientid == "btnUpdateAlbum")
        {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Album Details ?";
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

                        if (btn_clientid == "btnSaveAlbum")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveAlbum))%>;
                        }

                        if (btn_clientid == "btnUpdateAlbum")
                        {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateAlbum))%>;
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


<script type="text/javascript">
    function imgError(image) {
        image.onerror = "";
        image.src = "\\DesktopModules\\ThSport\\Images\\OtherImages\\1_pix.png";
        return true;
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
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" Album detail are save successfully. ">
     </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" Album detail are update successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
     <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Album Details ?"></asp:Label>
    </div>
</div>


<div class="row-fluid">
	<div class="span12">
    <asp:Panel ID="PnlGridAlbum" runat="server">
       <asp:Panel ID="addPanel" runat="server">
            <div id="submenu" style="float:left;">
                <ul>
                    <li class="active">
                        <asp:LinkButton ID="btnAddAlbum" 
                                        runat="server" 
                                        Height="35px" 
                                        Text=" Add Album " 
                                        onclick="btnAddAlbum_Click" 
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
					    <span class="hidden-480">Album List</span>
				    </div>
                    <div class="tools">
					    <a href="javascript:;" class="collapse"></a>
                    </div>
			    </div>
      
                <div class="portlet-body flip-scroll">

            <asp:GridView ID="gvAlbum" runat="server" 
                          CssClass="table-bordered table-striped table-condensed flip-content" 
                          AutoGenerateColumns="false" width="100%"
                          ShowHeaderWhenEmpty="true" 
                          AllowPaging="true" PageSize="10"
                          EmptyDataText="No Records Found" 
                          EmptyDataRowStyle-ForeColor="Red" 
                          onpageindexchanging="gvAlbum_PageIndexChanging"
                          DataKeyNames ="AlbumId">
                <RowStyle CssClass="grid-row" />
                <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
                <Columns>

                 <asp:TemplateField HeaderText="AlbumId" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                            Visible="false" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column">
                        <ItemTemplate>
                            <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                                <asp:Label ID="lblAlbumId" runat="server" Text='<%#Eval("AlbumId") %>'></asp:Label>
                            </div> 
                        </ItemTemplate>
                 </asp:TemplateField>

                    <asp:TemplateField HeaderText="Album Title" HeaderStyle-CssClass="grid-header-column" 
                                              ItemStyle-CssClass="grid-column" ItemStyle-Width="55%" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                                <asp:Literal ID="lblAlbumTitle" runat="server" Text='<%#Eval("AlbumName") %>'></asp:Literal>
                        </ItemTemplate>
                 </asp:TemplateField>

                 <asp:TemplateField HeaderText="Album Date" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                            HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <div class="grid-cell-inner">
                                <asp:Label ID="lblAlbumDate" runat="server" Text='<%#Eval("AlbumDate") %>'></asp:Label>
                            </div> 
                        </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderText="Album Level" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" 
                                             HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <div class="grid-cell-inner">
                                <asp:Label ID="lblAlbumLevel" runat="server" Text='<%#Eval("AlbumType") %>'></asp:Label>
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
                                   <asp:Label ID="lblddlActionAlbumID" runat="server" Text='<%#Eval("AlbumId") %>' Visible="false">
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
				    <span class="hidden-480"> Album Detail</span>
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

                            <div style="width: 100%;margin-top:20px;"></div>

                            <div class="control-group">
		                        <label class="control-label">
                                    <asp:Label ID="lblSport" runat="server" Text=" Sport :" ></asp:Label>
                                </label>
                                <div class="controls" style="position:relative;">
                                    <asp:DropDownList ID="ddlSports" runat="server" CssClass="medium m-wrap"/>
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

                            <div class="control-group">
		                         <label class="control-label">
                                       <asp:Label ID="lblClubMember" runat="server" Text="  Club Member :" ></asp:Label>
                                 </label>
                                 <div class="controls" style="position:relative;">
                                      <asp:DropDownList ID="ddlClubMember" runat="server" CssClass="medium m-wrap"/>
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
                        
                            <div class="control-group">
		                         <label class="control-label">
                                       <asp:Label ID="lblTeamMember" runat="server" Text="  Team Member :" ></asp:Label>
                                 </label>
                                 <div class="controls" style="position:relative;">
                                      <asp:DropDownList ID="ddlTeamMember" runat="server" CssClass="medium m-wrap"/>
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
                                       <asp:Label ID="lblAlbumTitle" runat="server" Text=" Album Title :" ></asp:Label>
                                 </label>
                                 <div class="startsetallfrom">
                                     <span class="help-inline"><font Color="red"><b>*</b></font></span>
                                 </div>
                                 <div class="controls" style="position:relative;">
                                      <asp:TextBox ID="txtAlbumTitle" runat="server" CssClass="m-wrap large"/>
                                       <asp:RequiredFieldValidator ID="rfvtxtAlbumTitle" runat="server" ErrorMessage="AlbumTitle"  
                                                                    ControlToValidate="txtAlbumTitle" SetFocusOnError="true" 
                                                                    ValidationGroup="Sports" Text=" Album Title Required !" CssClass="errorfordnn" ClientIDMode="Static"/>
                                       <asp:RegularExpressionValidator ID="rgvtxtAlbumTitle"
                                                                        Display="Static" ControlToValidate="txtAlbumTitle"  
                                                                        ValidationGroup="Sports" CssClass="errorfordnn"
                                                                        ValidationExpression = "^[\s\S]{0,100}$" 
                                                                        runat="server" ErrorMessage="Maximum 100 characters allowed.">
                                       </asp:RegularExpressionValidator>  
                                       <asp:CustomValidator ID="cvtxtAlbumTitle" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                                     ControlToValidate="txtAlbumTitle" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                                     CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                                       </asp:CustomValidator>
                                 </div>
                            </div>

                            <div class="control-group">
		                        <label class="control-label"> 
                                    <asp:Label ID="lblAlbumType" runat="server" Text="Album Type :" ></asp:Label>
                                    </label>
                                <div class="controls" style="position:relative;">
                                        <asp:DropDownList ID="ddlAlbumtype" runat="server" Width="320px" Height="34px" 
                                                            OnSelectedIndexChanged="ddlAlbumtype_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="Video">Video</asp:ListItem>
                                                <asp:ListItem Value="Picture">Picture</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                            </div>

                            <div class="control-group">
		                        <label class="control-label">
                                    <asp:Label ID="lblAlbumDesc" runat="server" Text=" Description :" ></asp:Label>
                                </label>
                                <div class="controls" style="position:relative;">
                                    <asp:TextBox ID="txtAlbumDesc" runat="server" CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px"/>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                                        Display="Static" ControlToValidate="txtAlbumDesc"  
                                                                        ValidationGroup="Sports" CssClass="errorfordnn"
                                                                        ValidationExpression = "^[\s\S]{0,300}$" 
                                                                        runat="server" ErrorMessage="Maximum 300 characters allowed.">
                                        </asp:RegularExpressionValidator>  
                                     <asp:CustomValidator ID="cvtxtAlbumDesc" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                                     ControlToValidate="txtAlbumDesc" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                                     CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                                       </asp:CustomValidator>
                               </div>
                            </div>

                            <div class="control-group">
		                        <label class="control-label"> 
                                    <asp:Label ID="lblAlbumDate" runat="server" Text="Album Date :" ></asp:Label>
                                </label>
                                <div class="startsetallfrom">
                                      <span class="help-inline"><font Color="red"><b>*</b></font></span>
                                </div>
                                <div class="controls" style="position:relative;">  
                                    <asp:TextBox ID="txtAlbumDate" runat="server" CssClass="datetimepicker m-wrap medium onlynumeric"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Album Date"
                                                                     ControlToValidate="txtAlbumDate" SetFocusOnError="true" 
                                                                     ValidationGroup="Sports" Text="Album Date Required !" CssClass="errorfordnn" 
                                                                     ClientIDMode="Static"/>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator8"
                                                                     Display="Static" ControlToValidate="txtAlbumDate"  
                                                                     ValidationGroup="Sports" CssClass="errorfordnn"
                                                                     ValidationExpression = "^[\s\S]{0,25}$" 
                                                                     runat="server" ErrorMessage="Maximum 25 characters allowed.">
                                     </asp:RegularExpressionValidator>  
                                    <asp:CustomValidator ID="CustomValidator6" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" 
                                                                     ControlToValidate="txtAlbumDate" EnableClientScript="true" ClientValidationFunction="validateTextBox" 
                                                                     CssClass="errorfordnn" Text="First Character Should Not Be Special Character">
                                       </asp:CustomValidator>
                                </div>
                            </div>

<%--                            <div class="control-group">
		                         <label class="control-label">
                                       <asp:Label ID="lblAlbumPriority" runat="server" Text=" Priority :" ></asp:Label>
                                 </label>
                                  <div class="startsetallfrom">
                                     <span class="help-inline"><font Color="red"><b>*</b></font></span>
                                 </div>
                                 <div class="controls" style="position:relative;">
                                      <asp:DropDownList ID="ddlAlbumPriority" runat="server" CssClass="medium m-wrap">
                                            <asp:ListItem Text=" -- Select Priority -- " Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="High" Value="High" ></asp:ListItem>
                                            <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                                      </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="RFVAlbumPriority" runat="server" ErrorMessage="Album Priority,"
                                                                    ControlToValidate="ddlAlbumPriority" SetFocusOnError="true"  
                                                                    ValidationGroup="Sports" 
                                                                    InitialValue="0" Text="Select Album Priority Required !" CssClass="errorfordnn" 
                                                                    ClientIDMode="Static"/>
                                 </div>
                            </div>--%>

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

                            <asp:Panel runat="server" ID="pnlPicture" Visible="false">
                                 <div class="control-group">
		                            <label class="control-label"> 
                                        <asp:Label ID="lblPictureLogo" runat="server" Text=" Photo : "></asp:Label>
                                     </label>
                                        <div class="controls" style="position:relative;">  
                                            <input ID="PictureLogoFile" type="file" name="file" runat="server" onchange="previewFilelogo()"/>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" 
                                                                            ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$"
                                                                            ControlToValidate="PictureLogoFile" ValidationGroup="Sports" 
                                                                            runat="server" ForeColor="Red" 
                                                                            ErrorMessage="Please choose only .jpg, .png and .gif images!"
                                                                            CssClass ="errorfordnn" />
                                            <div style="padding-top:10px;border:none; Width:200px;">
                                                <asp:Image ID="PictureLogoImage" runat="server" onError="imgError(this);"/>
                                            </div>
                                            <div style="padding-top:10px;border:none; Width:200px;">
                                                  <asp:LinkButton ID="btnAddPicture" 
                                                        runat="server" 
                                                        Text=" Add Album " 
                                                        onclick="btnAddPicture_Click" 
                                                        CssClass="btn blue">
                                                    </asp:LinkButton>
                                            </div>
                                        </div>
                                </div>
                                 <div class="control-group" style="margin-left:17%;">
                                  <asp:GridView ID="grdAlbumList" runat="server" AutoGenerateColumns="false"  
                                                      CssClass="table-bordered table-striped table-condensed flip-content"
                                                      ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="10" EmptyDataText="No Records Found" 
                                                      EmptyDataRowStyle-ForeColor="Red" onpageindexchanging="grdAlbumList_PageIndexChanging" 
                                                      Width="60%" OnRowDataBound="grdAlbumList_OnRowDataBound" OnRowCommand="grdAlbumList_OnRowCommand">
                                            <RowStyle CssClass="grid-row" />
                                        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
		                                <Columns>
			                                 <asp:TemplateField HeaderText="Picture" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%">
				                                <ItemTemplate>
                                                    <div style="text-align:center;">
					                                     <asp:Image ID="PictureLogoImage1" runat="server" ImageUrl='<%# Eval("PictureFile") %>' Width="100px" Height="100px" />
                               <%-- <video><source  src="~\\DesktopModules\\ThSport\\Videos\\NewsVideo\\AmazingFootballGoals.3gp" type="video/3gpp" /></video>--%>
                                   <%-- <iframe id = "video" width="420" height="315" frameborder="0" src="\\DesktopModules\\ThSport\\Videos\\NewsVideo\\AmazingFootballGoals.3gp"></iframe>--%>

                                                    </div> 
				                                </ItemTemplate>
			                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                                                               ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                     <asp:LinkButton ID="AlbumEditLink" CommandName="DeletePicture"
                                                                        CommandArgument='<%# Eval("Id") %>' runat="server" CssClass="viewButton">Delete</asp:LinkButton>
                                                </ItemTemplate>

                                            </asp:TemplateField>
            
		                                  </Columns>
                                            <PagerStyle CssClass="paging" HorizontalAlign="Center"/>
	                                    </asp:GridView>
                                 </div>
                            </asp:Panel>

                            <asp:Panel runat="server" ID="pnlVideo" Visible="false">
                                <div class="control-group">
		                                <label class="control-label"> 
                                            <asp:Label ID="lblVideoType" runat="server" Text="Album Type :" ></asp:Label>
                                         </label>
                                        <div class="controls" style="position:relative;">
                                                <asp:DropDownList ID="ddlVideotype" runat="server" Width="320px" Height="34px" 
                                                                  OnSelectedIndexChanged="ddlVideotype_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="YouTube">YouTube</asp:ListItem>
                                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                                </asp:DropDownList>
                                         </div>
                                 </div>

                                <div ID="divYouTubeVideopath" runat="server" visible="false">
                                    <div class="control-group">
		                                <label class="control-label">
                                            <asp:Label ID="lblAlbumPath" runat="server" Text="Album Path :" ></asp:Label>
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
                                            <asp:Label ID="lblAlbumPathExample" runat="server" Text=""></asp:Label>
                                        </label>
                                        <div class="controls">
                                            <asp:Label ID="lblAlbumPathExample1" runat="server" Text="http://www.youtube.com/watch?v=_____________________" ></asp:Label>
		                                </div>
                                    </div>  
                                </div>

                                <div id="divOtherVideoPath" runat="server" visible="false">
                                    <div class="control-group">
		                                <div class="controls" style="position:relative;">
                                            <input ID="AlbumLogoFile" type="file" name="file" runat="server" onchange="previewFile()"/>
                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
                                                                            ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.flv|.webm|.mkv|.vob|.ogv|.ogg|.avi|.mov|.wmv|.rm|.mp4|.m4p|.m4v|.mpg|.mp2|.mpeg|.mpe|.mpv|.m2v|.m4v|.svi|.3gp|.3g2|.nsv|.asf|.asx|.srt|.swf)$"
                                                                            ControlToValidate="AlbumLogoFile" ValidationGroup="Sports" 
                                                                            runat="server" ForeColor="Red" 
                                                                            ErrorMessage="This is not Album file!"
                                                                            CssClass ="errorfordnn" />
                                        </div>
                                        <div class="controls">
                                           <asp:Label ID="lblErrorAlbum" runat="server" Text="Album Must be Less Than 10 MB." ForeColor="Red"></asp:Label>
                                        </div> 
                                       <%-- <div class="controls">
                                            <iframe id="ifmOtherAlbumPath" runat="server" width="200" height="150" src='<%#Eval("AlbumOtherFile") %>'></iframe>
                                        </div> --%>
                                     </div>
                                </div>
                                  <div class="control-group">
                                    <div style="padding-top:10px;border:none; Width:200px;margin-left: 17%;">
                                                      <asp:LinkButton ID="LinkButton1" 
                                                            runat="server" 
                                                            Text=" Add Video " 
                                                            onclick="btnAddVideo_Click" 
                                                            CssClass="btn blue">
                                                        </asp:LinkButton>
                                     </div>
                                    </div>
                                 <div class="control-group" style="margin-left:17%;">
                                  <asp:GridView ID="grdVideoList" runat="server" AutoGenerateColumns="false"  
                                                      CssClass="table-bordered table-striped table-condensed flip-content"
                                                      ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="10" EmptyDataText="No Records Found" 
                                                      EmptyDataRowStyle-ForeColor="Red" onpageindexchanging="grdAlbumList_PageIndexChanging" 
                                                      Width="60%" OnRowCommand="grdVideoList_OnRowCommand">
                                            <RowStyle CssClass="grid-row" />
                                        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
		                                <Columns>
			                                 <asp:TemplateField HeaderText="Video" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%">
				                                <ItemTemplate>
                                                    <div style="text-align:center;">
					                                     <%--<asp:Image ID="PictureLogoImage1" runat="server" ImageUrl='<%# Eval("PictureFile") %>' Width="100px" Height="100px" />--%>
                                  <%--  <iframe id = "video" width="420" height="315" frameborder="0" src="//www.youtube.com/embed/fB0spy6xsPk"></iframe>--%>
                                                        <asp:Label runat="server" ID="lblVideoUrl" Text='<%# Eval("VideoFile") %>'>' ></asp:Label>
                                                       
                                                    </div> 
				                                </ItemTemplate>
			                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                                                               ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                     <asp:LinkButton ID="AlbumEditLink" CommandName="DeleteVideo"
                                                                        CommandArgument='<%# Eval("Id") %>' runat="server" CssClass="viewButton">Delete</asp:LinkButton>
                                                </ItemTemplate>

                                            </asp:TemplateField>
            
		                                  </Columns>
                                            <PagerStyle CssClass="paging" HorizontalAlign="Center"/>
	                                    </asp:GridView>
                                 </div>


                            </asp:Panel>
       
                       </div>

                        <div class="form-actions">
                    
                            <div class="right_div_css">

                                   <asp:Button id="btnSaveAlbum" runat="server" Width="100px" Text="Save" ClientIDMode="Static"
                                             onclick="btnSaveAlbum_Click" ValidationGroup="Sports" 
                                             OnClientClick="return validateAndConfirm(this.id);"
                                             CssClass="btn blue"/>

                                 <asp:Button id="btnUpdateAlbum" runat="server" Width="100px" Text="Update"  ClientIDMode="Static"
                                             onclick="btnUpdateAlbum_Click" Visible="false" 
                                             OnClientClick="return validateAndConfirm(this.id);"
                                             CssClass="btn red"  ValidationGroup="Sports"/>        

                                 <asp:Button id="btnCloseAlbum" runat="server" Width="100px"  Text="Cancel" 
                                             onclick="btnCloseAlbum_Click" CssClass="btn" ClientIDMode="Static" ValidationGroup="CloseSports"
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