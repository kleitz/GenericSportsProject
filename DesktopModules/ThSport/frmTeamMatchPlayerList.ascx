<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmTeamMatchPlayerList.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmTeamMatchPlayerList" %>

<div class="breadcrumbs" style="display:none;">
     <ul>
         <li class="home"><asp:HyperLink id="titela" runat="server"></asp:HyperLink> </li>
         <li><asp:Label ID="titel" runat="server" ></asp:Label></li>
     </ul>
</div>

<div style="margin:40px 10px;">
<div class="grid-header-title" style="width:419px;">
	<div style="float:left;">Player List</div>
           <div style="float:left;margin-left: 150px;"> ALL</div>
         <asp:CheckBox ID="checkAllPlayer" runat="server" AutoPostBack="true" 
                              OnCheckedChanged="checkAllPlayer_CheckedChanged" Text="" 
                                style="display:block !important;float:right; margin-right:82px;" />
           
	</div>

<asp:GridView ID="gvTeamMatchPlayerList" runat="server" AutoGenerateColumns="false" 
               CssClass="grid-table" ShowHeaderWhenEmpty="true" AllowPaging="true" 
               EmptyDataText="No Records Found" EmptyDataRowStyle-ForeColor="Red"
               OnRowDataBound="gvTeamMatchPlayerList_OnRowDataBound"
     onpageindexchanging="gvTeamMatchPlayerList_PageIndexChanging" >
                      
        <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
		<Columns>
          <asp:TemplateField HeaderText="Player Name" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" HeaderStyle-Width="197px" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                    <div class="grid-cell-inner">
                        <asp:HiddenField ID="hdnPlayerID" runat="server" Value='<%#Eval("RegistrationId") %>' />
                        <asp:Label ID="lblPlayerName" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
                    </div> 
				</ItemTemplate>
			</asp:TemplateField>

            <asp:TemplateField HeaderText="Assign" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" HeaderStyle-Width="197px" ItemStyle-HorizontalAlign="Center">
				<ItemTemplate>
                        <div style="text-align:center;">
                        <asp:CheckBox ID="chkview" runat="server" AutoPostBack="true" OnCheckedChanged="chkview_CheckedChanged" style="display:block !important;" />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            

    </Columns>

     
             <PagerSettings Mode="NumericFirstLast" PageButtonCount="8" /> 
        
        <PagerStyle  CssClass="paging" HorizontalAlign="Center"/>
    </asp:GridView>

</div>
    <div id="noDatastatus" runat="server" >
        <asp:Label ID="lblNoData" runat="server"  Text="" Visible="false" />
    </div>
