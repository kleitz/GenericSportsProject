<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmCompetition.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmCompetition" %>

<script type="text/javascript">

    function CompareDates(source, args)
    {
        var str1 = document.getElementById("txtStartDate").value;
        var str2 = document.getElementById("txtEndDate").value;

        var dt1 = parseInt(str1.substring(0, 2), 10);
        var mon1 = parseInt(str1.substring(3, 5), 10);
        var yr1 = parseInt(str1.substring(6, 10), 10);
        var dt2 = parseInt(str2.substring(0, 2), 10);
        var mon2 = parseInt(str2.substring(3, 5), 10);
        var yr2 = parseInt(str2.substring(6, 10), 10);
        var date1 = new Date(yr1, mon1, dt1);
        var date2 = new Date(yr2, mon2, dt2);

        if (date2 < date1)
        {
            //alert("To date cannot be greater than from date");
            args.IsValid = false;
        }
        else
        {
            //alert("Submitting ...");
            args.IsValid = true;
        }
    }

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

    function previewFileLogo()
    {
        var preview = document.querySelector('#<%=CompetitionLogoImage.ClientID %>');
        var file = document.querySelector('#<%=CompetitionLogoFile.ClientID %>').files[0];
        var reader = new FileReader();

        reader.onloadend = function ()
        {
            preview.src = reader.result;
        }

        if (file)
        {
            if (file.size > 10485760)
            {
                document.getElementById('span_logo_size_error').style.display = "block";
                preview.src = "";
            }
            reader.readAsDataURL(file);
        }
        else
        {
            preview.src = "";
        }
    }

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

    function imgError(image)
    {
        image.onerror = "";
        
        image.src = "\\DesktopModules\\\ThSport\\Images\\OtherImages\\1_pix.png";
        console.log(image);
        return true;
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

<script type="text/javascript">
    function DeleteSuccessfully()
    {
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
    function cancelvalidateAndConfirmClose()
    {
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
    function validateAndConfirmClose(OnlyClose)
    {
         var validated = Page_ClientValidate('CloseSports');

         if (OnlyClose == "btnCancelCompetition")
         {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Competition Form ?";
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

                         if (OnlyClose == "btnCancelCompetition")
                         {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCancelCompetition))%>;
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

         if (btn_clientid == "btnUpdateCompetition")
         {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Competition Details ?";
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

                         if (btn_clientid == "btnSaveCompetition")
                         {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveCompetition))%>;
                         }

                         if (btn_clientid == "btnUpdateCompetition")
                         {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateCompetition))%>;
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

    function validateAndConfirm1(btn_clientid)
    {
        if (btn_clientid == "Delete")
        {
            document.getElementById("msgConfirm").innerHTML = "This Competition Haveing Other Data like News, Video, Picture Etc. </br> Are You Sure, You Want to Delete This Competition?";
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
                         if (btn_clientid == "Delete") {
                            $('.hndDeleteConfirm').val("true") ;
                            console.log("hello" + $('.hndDeleteConfirm').val());
                            $('.lnkDeleteCompetition').trigger("click");
                           
                         }
                     },
                     Cancel: function () {
                         $(this).dialog('close');
                         $('.hndDeleteConfirm').val("false");
                         $('.lnkDeleteCompetition').trigger("click");
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

<%--<asp:HiddenField ID="hndDeleteConfirm" runat="server"></asp:HiddenField>--%>
<input type="hidden" class="hndDeleteConfirm" runat="server" id="hndDeleteConfirm" />
        <asp:Button ID="lnkDeleteCompetition" runat="server" Text="Cancel" 
                    OnClick="btnDeleteCompetition_Click" CssClass="lnkDeleteCompetition" 
                    ClientIDMode="Static" style="display: none;"/>

<div id="divsavemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
    <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" Competition detail are save successfully. ">
    </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
    <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" Competition detail are update successfully. ">
    </asp:Label>
</div>

<div id="divcancelmassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Cancel.png")%>" />
    <asp:Label CssClass="lobibox-body-text" ID="Label3" ClientIDMode="Static" runat="server" Text=" Competition detail are delete successfully. ">
    </asp:Label>
</div>


<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
    <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Competition Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

   <asp:Panel id="pnlCompetitionGrid" runat="server">

    <asp:Panel ID="addPanel" runat="server">    
        <div id="submenu">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddCompetition" runat="server" 
                                    Height="35px" Text=" Add Competition " 
                                    onclick="btnAddCompetition_Click" ForeColor="White"/>
                </li>
            </ul>
        </div>
        <div style="position: relative;float: right;padding-top: 15px;margin-right: -0.9%;">
        </div>
    </asp:Panel>
 
    <div class="portlet box green">

			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480"> Competition  List</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
			

    <div class="portlet-body flip-scroll">
		
          <asp:GridView ID="gvCompetition" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" 
                          AllowPaging="true" PageSize="10"
                          EmptyDataText="No Records Found" 
                          EmptyDataRowStyle-ForeColor="Red" 
                        CssClass="table-bordered table-striped table-condensed flip-content" 
                        HorizontalAlign="Center" AlternatingRowStyle-Font-Size="X-Large" 
                        CellPadding="5" CellSpacing="5" Width="100%"
                        onpageindexchanging="gvCompetition_PageIndexChanging">
            <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />

		<Columns>

            <asp:TemplateField HeaderText="Competition" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblCompetitionName" runat="server" Text='<%#Eval("CompetitionName") %>' ToolTip="Competition "></asp:Label>
                    </div> 
                    <asp:HiddenField ID="hdn_Competition_Id" runat="server" Value='<%#Eval("CompetitionId") %>'></asp:HiddenField>
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Season" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" Visible="false" ItemStyle-Width="20%">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblCompetitionSeason" runat="server" Text='<%#Eval("SeasonName") %>' ToolTip="Season"></asp:Label>
                    </div> 
                    <asp:HiddenField ID="hdnSeasonID" runat="server" Value='<%#Eval("SeasonID") %>' />
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Competition League" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblCompetitionLeague" runat="server" Text='<%#Eval("CompetitionLeagueName") %>' ToolTip="Competition League"></asp:Label>
                    </div> 
                    <asp:HiddenField ID="hdnCompetitionLeagueID" runat="server" Value='<%#Eval("CompetitionLeagueID") %>' />
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Sport" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblSport" runat="server" Text='<%#Eval("SportName") %>' ToolTip="Sport"></asp:Label>
                    </div> 
                    <asp:HiddenField ID="hdnSportID" runat="server" Value='<%#Eval("SportID") %>' />
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Competition Type" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblCompetitionType" runat="server" Text='<%#Eval("CompetitionTypeName") %>' ToolTip="Competition Type"></asp:Label>
                    </div> 
                    <asp:HiddenField ID="hdnCompetitionTypeID" runat="server" Value='<%#Eval("SeasonID") %>' />
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Division" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblCompetitionDivision" runat="server" Text='<%#Eval("DivisionName") %>' ToolTip="Division"></asp:Label>
                    </div> 
                    <asp:HiddenField ID="hdnDivisionID" runat="server" Value='<%#Eval("SeasonID") %>' />
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Abbreviation" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" Visible="false">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblCompetitionAbbr" runat="server" Text='<%#Eval("CompetitionAbbr") %>' ToolTip="Abbreviation"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center" Visible="false">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblCompetitionDesc" runat="server" Text='<%#Eval("CompetitionDesc") %>' ToolTip="Description"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

             <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                               ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" 
                                      OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                            <asp:ListItem Value="Edit">Edit</asp:ListItem>
                            <asp:ListItem Value="Team">Team</asp:ListItem>
                            <asp:ListItem Value="Group">Group</asp:ListItem>
                            <asp:ListItem Value="Match">Match</asp:ListItem>
                            <asp:ListItem Value="Delete">Delete</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>

		</Columns>
              <PagerSettings Mode="NumericFirstLast" PageButtonCount="8" />
                <PagerStyle CssClass="paging" HorizontalAlign="Center"/>
	    </asp:GridView>  
    
   </div>  

   </div>

    </asp:Panel>

<asp:Panel ID="pnlCompetitionEntry" runat="server">

    <div style="padding:10px 0px;">
            * Note: All Fields marked with an asterisk (*) are required.
    </div>

    <div class="portlet box blue tabbable">
			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480"> Competition  Details</span>
				</div>
			</div>

    <div class="portlet-body form">

	<div class="tabbable portlet-tabs">

    <div class="tab-content" style="margin-top:10px !important;">
		<div class="tab-pane active" id="portlet_tab1">

        <div class="form-horizontal">

        <div style="width: 100%;margin-top:20px;"></div>

        <asp:HiddenField ID="hdnCompetitionID" runat="server" />

        <div class="control-group">
		    <label class="control-label">Sport : </label>
             <div class="startsetallfrom">
              <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
            <div class="controls" style="position:relative;">  
                <asp:DropDownList ID="ddlSport" runat="server"  CssClass="large m-wrap"/>
               
                <asp:RequiredFieldValidator ID="rfvddlSport" ClientIDMode="Static" runat="server" InitialValue="0" 
                    ErrorMessage="Sport Required !" CssClass="errorfordnn" SetFocusOnError="true" ControlToValidate="ddlSport"
                    ValidationGroup="Sports" Text="Sport Required !"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="control-group">
		    <label class="control-label">Season : </label>
             <div class="startsetallfrom">
              <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
            <div class="controls" style="position:relative;">  
                <asp:DropDownList ID="ddlSeason" runat="server"  CssClass="large m-wrap"/>
               
                <asp:RequiredFieldValidator ID="rfvddlSeason" ClientIDMode="Static" runat="server" InitialValue="0" 
                    ErrorMessage="Season Required !" CssClass="errorfordnn" SetFocusOnError="true" ControlToValidate="ddlSeason"
                    ValidationGroup="Sports" Text="Season Required !"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="control-group">
		    <label class="control-label">Competition League : </label>
             <div class="startsetallfrom">
              <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
            <div class="controls" style="position:relative;">  
                <asp:DropDownList ID="ddlCompetitionLeague" runat="server"  CssClass="large m-wrap"/>
               
                <asp:RequiredFieldValidator ID="rfvddlCompetitionLeague" ClientIDMode="Static" runat="server" InitialValue="0" 
                    ErrorMessage="Competition League Required !" CssClass="errorfordnn" SetFocusOnError="true" ControlToValidate="ddlCompetitionLeague"
                    ValidationGroup="Sports" Text="Competition League Required !"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="control-group">
		    <label class="control-label">Division : </label>
            <div class="controls" style="position:relative;">  
                <asp:DropDownList ID="ddlDivision" runat="server"  CssClass="large m-wrap"/>
                
            </div>
        </div>

        <div class="control-group">
		    <label class="control-label">Competition Type : </label>
             <div class="startsetallfrom">
              <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
            <div class="controls" style="position:relative;">  
                <asp:DropDownList ID="ddlCompetitionType" runat="server"  CssClass="large m-wrap"/>
               
                <asp:RequiredFieldValidator ID="rfvddlCompetitionType" ClientIDMode="Static" runat="server" InitialValue="0" 
                    ErrorMessage="Competition Type Required !" CssClass="errorfordnn" SetFocusOnError="true" ControlToValidate="ddlCompetitionType"
                    ValidationGroup="Sports" Text="Competition Type Required !"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="control-group">
		    <label class="control-label">Competition Format : </label>
             <div class="startsetallfrom">
              <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
            <div class="controls" style="position:relative;">  
                <asp:DropDownList ID="ddlCompetitionFormat" runat="server"  CssClass="large m-wrap"/>
               
                <asp:RequiredFieldValidator ID="rfvddlCompetitionFormat" ClientIDMode="Static" runat="server" InitialValue="0" 
                    ErrorMessage="Competition Format Required !" CssClass="errorfordnn" SetFocusOnError="true" ControlToValidate="ddlCompetitionFormat"
                    ValidationGroup="Sports" Text="Competition Format Required !"></asp:RequiredFieldValidator>
            </div>
        </div>

       <div class="control-group">
		     <label class="control-label">          
                   <asp:Label ID="lblCompetition" runat="server" Text=" Competition Name :" ></asp:Label>
             </label>
            <div class="startsetallfrom">
              <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtCompetition" runat="server"  CssClass="m-wrap large" />
               
                  <asp:RequiredFieldValidator ID="rfvCompetition" runat="server" ErrorMessage="Competition"  ControlToValidate="txtCompetition" SetFocusOnError="true" 
                                              ValidationGroup="Sports" Text="Competition  Required !" CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="rgvtxtCompetition"
                                                    Display="Static" ControlToValidate="txtCompetition"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="cvtxtCompetition" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" ControlToValidate="txtCompetition"
                                    EnableClientScript="true" ClientValidationFunction="validateTextBox" CssClass="errorfordnn" Text="First Character Should Not Be Special Character"></asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblCompetitionAbbr" runat="server" Text="Abbreviation :" ></asp:Label>
            </label>
             <div class="startsetallfrom">
              <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtCompetitionAbbr" runat="server"  CssClass="m-wrap small" />
              
                    <asp:RegularExpressionValidator ID="rgvtxtCompetitionAbbr"
                                                    Display="Static" ControlToValidate="txtCompetitionAbbr"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,5}$" 
                                                    runat="server" ErrorMessage="Maximum 5 characters allowed.">
                    </asp:RegularExpressionValidator>  
            <asp:CustomValidator ID="cvtxtCompetitionAbbr" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" ControlToValidate="txtCompetitionAbbr"
                                    EnableClientScript="true" ClientValidationFunction="validateTextBox" CssClass="errorfordnn" Text="First Character Should Not Be Special Character"></asp:CustomValidator>
           </div>
        </div>
    
        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblCompetitionDesc" runat="server" Text="Description :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtCompetitionDesc" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px"/>
                    <asp:RegularExpressionValidator ID="rgvtxtCompetitionDesc"
                                                    Display="Static" ControlToValidate="txtCompetitionDesc"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,200}$" 
                                                    runat="server" ErrorMessage="Maximum 200 characters allowed.">
                    </asp:RegularExpressionValidator>  
                <asp:CustomValidator ID="cvtxtCompetitionDesc" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" ControlToValidate="txtCompetitionDesc"
                                    EnableClientScript="true" ClientValidationFunction="validateTextBox" CssClass="errorfordnn" Text="First Character Should Not Be Special Character"></asp:CustomValidator>
           </div>
        </div>


        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblCompetitionLogoName" runat="server" Text=" Logo Name :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtCompetitionLogoName" runat="server"  CssClass="m-wrap large" />
                    <asp:RegularExpressionValidator ID="rgvCompetitionLogoName"
                                                    Display="Static" ControlToValidate="txtCompetitionLogoName"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,200}$" 
                                                    runat="server" ErrorMessage="Maximum 200 characters allowed.">
                    </asp:RegularExpressionValidator>  
           </div>
        </div>

        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblUploadLogo" runat="server" Text="Upload Logo : "></asp:Label>
             </label>
             <div class="startsetallfrom">
              <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
            <div class="controls" style="position:relative;">  
                <input ID="CompetitionLogoFile" type="file" name="file" runat="server" onchange="previewFileLogo()"/>
            
                <asp:RegularExpressionValidator ID="rgvCompetitionLogoFile" 
                                                ValidationExpression="([a-zA-Z\\].*(.jpg|.png|.bmp|.jpeg|.gif|.tif)$)"
                                                ControlToValidate="CompetitionLogoFile" ValidationGroup="Sports" 
                                                runat="server"  
                                                ErrorMessage="Please choose only .jpg, .jpeg, .png and .gif images!"
                                                CssClass ="errorfordnn" />
                <span id="span_logo_size_error" style="display:none;"><font style="color:red;">Can Not Upload Logo Larger Than 10 MB</font></span> 
                <div style="padding-top:10px;border:none; Width:200px;">
                    <asp:Image ID="CompetitionLogoImage" runat="server" onError="imgError(this);"/>
                </div>
            </div>
        </div>

         <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblStartDate" runat="server" Text="Start Date :" ></asp:Label>
            </label>
              <div class="startsetallfrom">
              <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
            <div class="controls" style="position:relative;">  
                <asp:TextBox ID="txtStartDate" runat="server" ClientIDMode="Static"  CssClass="datetimepicker m-wrap medium"/>
               
                <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ErrorMessage="Enter Start Date"
                                                 ControlToValidate="txtStartDate" SetFocusOnError="true" 
                                                 ValidationGroup="Sports" Text="Start Date Required !" CssClass="errorfordnn" 
                                                 ClientIDMode="Static"/>
            </div>
        </div>

        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblEndDate" runat="server" Text="End Date :" ></asp:Label>
             </label>
             <div class="startsetallfrom">
              <span class="help-inline"><font Color="red"><b>*</b></font></span>
            </div>
            <div class="controls" style="position:relative;">   
                 <asp:TextBox ID="txtEndDate" runat="server" ClientIDMode="Static"  CssClass="enddatetimepicker m-wrap medium onlynumeric"/>
               
                 <asp:RequiredFieldValidator ID="rfvtxtEndDate" runat="server" ErrorMessage="Enter End Date"
                                                 ControlToValidate="txtEndDate" SetFocusOnError="true" 
                                                 ValidationGroup="Sports" Text="End Date Required !" CssClass="errorfordnn" 
                                                 ClientIDMode="Static"/>
                 <asp:CustomValidator ClientValidationFunction="CompareDates" ID="cvDatesValidator" runat="server" CssClass="errorfordnn" ValidationGroup="Sports"
                                    ErrorMessage="End Date cannot be less than Start Date" ControlToValidate="txtStartDate"></asp:CustomValidator>
                
             </div> 
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblNoOfGroup" runat="server" Text="No Of Group(s) :"></asp:Label>
            </label>
            <div class="controls">
                <asp:TextBox ID="txtNoOfGroup" runat="server" CssClass="m-wrap small onlynumeric" />
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblNoOfTeam" runat="server" Text="No Of Team(s) :"></asp:Label>
            </label>
            <div class="controls">
                <asp:TextBox ID="txtNoOfTeam" runat="server" CssClass="m-wrap small onlynumeric" />
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
            <asp:Label ID="lblActive" runat="server" Text=" Is Active :"></asp:Label>
        </label>
            <div class="controls">
                <label class="checkbox"> 
                    <asp:CheckBox ID="ChkIsActive" runat="server" />
                </label>
             </div>
        </div>

       <div class="control-group">
		    <label class="control-label">
            <asp:Label ID="lblShow" runat="server" Text=" Is Show :"></asp:Label>
        </label>
            <div class="controls">
                <label class="checkbox"> 
                    <asp:CheckBox ID="ChkIsShow" runat="server" />
                </label>
                </div>
        </div>    
       
       </div>

            <div class="portlet box blue tabbable">
			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480">Competition Points<span style="font-size: 15px;">&nbsp;&nbsp; (Maximum Length : 2)</span></span>
				</div>
			</div>
        </div>
      <div class="form-horizontal">
        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblWin" runat="server" Text="Win :" ></asp:Label>
              </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtWin" runat="server" CssClass="m-wrap medium onlynumeric"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator10"
                                                Display="Static" ControlToValidate="txtWin"  
                                                ValidationGroup="Sports" CssClass="errorfordnn"
                                                ValidationExpression = "^[\s\S]{0,2}$" 
                                                runat="server" ErrorMessage="Maximum 2 characters allowed.">
                </asp:RegularExpressionValidator>
             </div>
         </div> 

        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblLose" runat="server" Text="Lose :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtLose" runat="server" CssClass="m-wrap medium onlynumeric"></asp:TextBox>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator11"
                                                Display="Static" ControlToValidate="txtLose"  
                                                ValidationGroup="Sports" CssClass="errorfordnn"
                                                ValidationExpression = "^[\s\S]{0,2}$" 
                                                runat="server" ErrorMessage="Maximum 2 characters allowed.">
                </asp:RegularExpressionValidator>
            </div>
        </div>

        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblDraw" runat="server" Text="Draw :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtDraw" runat="server" CssClass="m-wrap medium onlynumeric"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator12"
                                                Display="Static" ControlToValidate="txtDraw"  
                                                ValidationGroup="Sports" CssClass="errorfordnn"
                                                ValidationExpression = "^[\s\S]{0,2}$" 
                                                runat="server" ErrorMessage="Maximum 2 characters allowed.">
                </asp:RegularExpressionValidator>
            </div>
        </div>
                
        <div class="control-group">
		    <label class="control-label"> 
                <asp:Label ID="lblNoShowGoal" runat="server" Text="No. Show Goal :" ></asp:Label>
             </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtNoShowGoal" runat="server" CssClass="m-wrap medium onlynumeric"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator13"
                                                Display="Static" ControlToValidate="txtNoShowGoal"  
                                                ValidationGroup="Sports" CssClass="errorfordnn"
                                                ValidationExpression = "^[\s\S]{0,2}$" 
                                                runat="server" ErrorMessage="Maximum 2 characters allowed.">
                </asp:RegularExpressionValidator>
            </div>
        </div>
          </div>

        <div class="form-actions">
            <div class="right_div_css">

                    <asp:Button ID="btnSaveCompetition" runat="server"  Text=" Save " OnClick="btnSaveCompetition_Click" 
                                ValidationGroup="Sports" CssClass="btn blue" ClientIDMode="Static" Width="100px"
                                OnClientClick="return validateAndConfirm(this.id);" />

                    <asp:Button ID="btnUpdateCompetition" runat="server"  Text=" Update " OnClick="btnUpdateCompetition_Click" 
                                ValidationGroup="Sports" CssClass="btn red" ClientIDMode="Static" Width="100px"
                                OnClientClick="return validateAndConfirm(this.id);" />

                    <asp:Button ID="btnCancelCompetition" runat="server" Text="Cancel" OnClick="btnCancelCompetition_Click" CssClass="btn" 
                                ClientIDMode="Static" ValidationGroup="CloseSports" Width="100px"
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

