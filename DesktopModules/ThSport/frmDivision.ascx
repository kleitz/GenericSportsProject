<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmDivision.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmDivision" %>

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

    function previewFilephoto() {
        var preview = document.querySelector('#<%=DivisionLogoImage.ClientID %>');
        var file = document.querySelector('#<%=DivisionLogoFile.ClientID %>').files[0];
        var reader = new FileReader();

        reader.onloadend = function () {
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
    function DeleteSuccessfully() {
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
    function cancelvalidateAndConfirmClose() {
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
     function validateAndConfirmClose(OnlyClose) {
         var validated = Page_ClientValidate('CloseSports');

         if (OnlyClose == "btnCancelDivision") {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Division Form ?";
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

                         if (OnlyClose == "btnCancelDivision") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnCancelDivision))%>;
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

         if (btn_clientid == "btnUpdateDivision") {
             document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Division Details ?";
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

                         if (btn_clientid == "btnSaveDivision") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSaveDivision))%>;
                         }

                         if (btn_clientid == "btnUpdateDivision") {
                             <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnUpdateDivision))%>;
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

     $(document).ready(function () {
         //Reset drop down list
         $(".ddlActionSelect > option:first").attr("selected", "selected");
     });

</script>

<div id="divsavemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label1" ClientIDMode="Static" runat="server" Text=" Division detail are save successfully. ">
     </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" Division detail are update successfully. ">
     </asp:Label>
</div>

<div id="divcancelmassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Cancel.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label3" ClientIDMode="Static" runat="server" Text=" Division detail are delete successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
    <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Division Details ?"></asp:Label>
    </div>
</div>

<div class="row-fluid">
	<div class="span12">

   <asp:Panel id="pnlDivisionGrid" runat="server">

    <asp:Panel ID="addPanel" runat="server">    
        <div id="submenu">
            <ul>
                <li class="active">
                    <asp:LinkButton ID="btnAddDivision" runat="server" 
                                    Height="35px" Text=" Add Division" 
                                    onclick="btnAddDivision_Click" ForeColor="White"/>
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
					<span class="hidden-480"> Division List</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>
			

    <div class="portlet-body flip-scroll">
		
          <asp:GridView ID="gvDivision" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" 
                        AllowPaging="true" PageSize="10" EmptyDataText="No Records Found" 
                        CssClass="table-bordered table-striped table-condensed flip-content" 
                        HorizontalAlign="Center" AlternatingRowStyle-Font-Size="X-Large" 
                        CellPadding="5" CellSpacing="5" Width="100%"
                        onpageindexchanging="gvDivision_PageIndexChanging">
            <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />

		<Columns>

            <asp:TemplateField HeaderText="Season" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblSeason" runat="server" Text='<%#Eval("SeasonName") %>' ToolTip="Competition Season"></asp:Label>
                    </div> 
                    <asp:HiddenField ID="hdn_Season_Id" runat="server" Value='<%#Eval("SeasonId") %>'></asp:HiddenField>
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Division" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblDivisionName" runat="server" Text='<%#Eval("DivisionName") %>' ToolTip="Competition Division"></asp:Label>
                    </div> 
                    <asp:HiddenField ID="hdn_Division_Id" runat="server" Value='<%#Eval("DivisionId") %>'></asp:HiddenField>
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Abbreviation" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblDivisionAbbr" runat="server" Text='<%#Eval("DivisionAbbr") %>' ToolTip="Abbreviation"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblDivisionDesc" runat="server" Text='<%#Eval("DivisionDesc") %>' ToolTip="Description"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="DivisionLevel" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblDivisionLevel" runat="server" Text='<%#Eval("DivisionLevel") %>' ToolTip="Division Level"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Total No of Teams" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblTotalNumofTeams" runat="server" Text='<%#Eval("TotalNumofTeams") %>' ToolTip="No of Teams"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Promoted Num" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblPromotedNum" runat="server" Text='<%#Eval("PromotedNum") %>' ToolTip="Promoted Num"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Demoted Num" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner" style="text-align:center;">
					    <asp:Label ID="lblDemotedNum" runat="server" Text='<%#Eval("DemotedNum") %>' ToolTip="Demoted Num"></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

             <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" 
                               ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="small m-wrap ddlActionSelect" 
                                      OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0"> -- Action -- </asp:ListItem>
                            <asp:ListItem Value="Edit">Edit</asp:ListItem>
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

<asp:Panel ID="pnlDivisionEntry" runat="server">

    <div style="padding:10px 0px;">
            * Note: All Fields marked with an asterisk (*) are required.
    </div>

    <div class="portlet box blue tabbable">
			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480"> Division Details</span>
				</div>
			</div>

    <div class="portlet-body form">

	<div class="tabbable portlet-tabs">

    <div class="tab-content" style="margin-top:10px !important;">
		<div class="tab-pane active" id="portlet_tab1">

        <div class="form-horizontal">

        <div style="width: 100%;margin-top:20px;"></div>

        <asp:HiddenField ID="hdnDivisionID" runat="server" />

            <div class="control-group">
		        <label class="control-label">Season : </label>
                <div class="controls" style="position:relative;">  
                    <asp:DropDownList ID="ddlSeason" runat="server"  CssClass="large m-wrap"/>
                    <span class="help-inline"><font Color="red"><b>*</b></font></span>
                    <asp:RequiredFieldValidator ID="rfvddlSeason" ClientIDMode="Static" runat="server" InitialValue="0" 
                        ErrorMessage="Season Required !" CssClass="errorfordnn" SetFocusOnError="true" ControlToValidate="ddlSeason"
                        ValidationGroup="Sports" Text="Season Required !"></asp:RequiredFieldValidator>
                </div>
            </div>

           <div class="control-group">
		         <label class="control-label">          
                       <asp:Label ID="lblDivision" runat="server" Text="Division :" ></asp:Label>
                 </label>
             
             <div class="controls" style="position:relative;">
                  <asp:TextBox ID="txtDivision" runat="server"  CssClass="m-wrap large" />
                 <span class="help-inline"><font Color="red"><b>*</b></font></span>
                  <asp:RequiredFieldValidator ID="rfvDivision" runat="server" ErrorMessage="Division"
                                              ControlToValidate="txtDivision" SetFocusOnError="true" 
                                              ValidationGroup="Sports" Text="Division Name Required !" 
                                              CssClass="errorfordnn" ClientIDMode="Static"/>
                   <asp:RegularExpressionValidator ID="rgvtxtDivision"
                                                    Display="Static" ControlToValidate="txtDivision"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,100}$" 
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                   </asp:RegularExpressionValidator>  
                   <asp:CustomValidator ID="cvtxtDivision" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" ControlToValidate="txtDivision"
                                    EnableClientScript="true" ClientValidationFunction="validateTextBox" CssClass="errorfordnn" Text="First Character Should Not Be Special Character"></asp:CustomValidator>
             </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblDivisionAbbr" runat="server" Text="Abbreviation :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtDivisionAbbr" runat="server"  CssClass="m-wrap small" />
                <span class="help-inline"><font Color="red"><b>*</b></font></span>
                    <asp:RegularExpressionValidator ID="rgvtxtDivisionAbbr"
                                                    Display="Static" ControlToValidate="txtDivisionAbbr"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,5}$" 
                                                    runat="server" ErrorMessage="Maximum 5 characters allowed.">
                    </asp:RegularExpressionValidator>  
           </div>
        </div>
    
        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblDivisionDesc" runat="server" Text="Description :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtDivisionDesc" runat="server"  
                             CssClass="m-wrap mediumSmallDesc" TextMode="MultiLine" Width="319px" Height="150px"/>
                    <asp:RegularExpressionValidator ID="rgvtxtDivisionDesc"
                                                    Display="Static" ControlToValidate="txtDivisionDesc"  
                                                    ValidationGroup="Sports" CssClass="errorfordnn"
                                                    ValidationExpression = "^[\s\S]{0,200}$" 
                                                    runat="server" ErrorMessage="Maximum 200 characters allowed.">
                    </asp:RegularExpressionValidator>  
                <asp:CustomValidator ID="cvtxtDivisionDesc" ValidationGroup="Sports" runat="server" ErrorMessage="" SetFocusOnError="true" ControlToValidate="txtDivisionDesc"
                                    EnableClientScript="true" ClientValidationFunction="validateTextBox" CssClass="errorfordnn" Text="First Character Should Not Be Special Character"></asp:CustomValidator>
           </div>
        </div>


        <div class="control-group">
		    <label class="control-label">
                <asp:Label ID="lblDivisionLogoName" runat="server" Text="Logo Name :" ></asp:Label>
            </label>
            <div class="controls" style="position:relative;">
                <asp:TextBox ID="txtDivisionLogoName" runat="server"  CssClass="m-wrap large" />
                    <asp:RegularExpressionValidator ID="rgvDivisionLogoName"
                                                    Display="Static" ControlToValidate="txtDivisionLogoName"  
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
            <div class="controls" style="position:relative;">  
                <input ID="DivisionLogoFile" type="file" name="file" runat="server" onchange="previewFilephoto()"/>
                <asp:RegularExpressionValidator ID="rgvDivisionLogoFile" 
                                                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$"
                                                ControlToValidate="DivisionLogoFile" ValidationGroup="Sports" 
                                                runat="server"  
                                                ErrorMessage="Please choose only .jpg, .png and .gif images!"
                                                CssClass ="errorfordnn" />
                <div style="padding-top:10px;border:none; Width:200px;">
                    <asp:Image ID="DivisionLogoImage" runat="server" />
                </div>
            </div>
        </div>

        <div class="control-group">
		    <label class="control-label">Division Level :</label>
            <div class="controls">
                <asp:TextBox ID="txtDivisionLevel" runat="server" CssClass="m-wrap small onlynumeric" />
            </div>
        </div>

        <div class="control-group">
		    <label class="control-label">
            <asp:Label ID="lblTotalNoOfTeam" runat="server" Text="No.Of Teams :"></asp:Label>
            </label>
            <div class="controls">
                <asp:TextBox ID="txtTotalNoOfTeam" runat="server" CssClass="m-wrap small onlynumeric" />
            </div>
        </div>

       <div class="control-group">
            <label class="control-label">Promoted No :</label>
            <div class="controls">
                <asp:TextBox ID="txtPromotedNum" runat="server" CssClass="m-wrap small onlynumeric" />
            </div>
        </div>    

        <div class="control-group">
		    <label class="control-label">Demoted No : </label>
            <div class="controls">
                <asp:TextBox ID="txtDemotedNum" runat="server" CssClass="m-wrap small onlynumeric" />
            </div>
        </div>    

        <div class="form-actions">
            <div class="right_div_css">

                    <asp:Button ID="btnSaveDivision" runat="server"  Text=" Save " OnClick="btnSaveDivision_Click" 
                                ValidationGroup="Sports" CssClass="btn blue" ClientIDMode="Static" Width="100px"
                                OnClientClick="return validateAndConfirm(this.id);" />

                    <asp:Button ID="btnUpdateDivision" runat="server"  Text=" Update " OnClick="btnUpdateDivision_Click" 
                                ValidationGroup="Sports" CssClass="btn red" ClientIDMode="Static" Width="100px"
                                OnClientClick="return validateAndConfirm(this.id);" />

                    <asp:Button ID="btnCancelDivision" runat="server" Text="Cancel" OnClick="btnCancelDivision_Click" CssClass="btn" 
                                ClientIDMode="Static" ValidationGroup="CloseSports" Width="100px"
                                OnClientClick="return validateAndConfirmClose(this.id);"/>

            </div>    
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

