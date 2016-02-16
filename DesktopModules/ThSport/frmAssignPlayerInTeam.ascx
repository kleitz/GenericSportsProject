<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmAssignPlayerInTeam.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmAssignPlayerInTeam" %>



<div class="span12">

    <div style="padding:15px;">

         <div class="form-header">
            <div style="width: 100%;margin-top:10px;">
                 <asp:ValidationSummary ID="ValidationSummary2" 
                                        CssClass="dnnFormMessage dnnFormValidationSummary"
                                        HeaderText=" " 
                                        DisplayMode="SingleParagraph"
                                        ValidationGroup="Sports" 
                                        EnableClientScript="true" 
                                        runat="server"/>
           </div>
         
            <asp:DropDownList ID="ddlCompetitionList" 
                              runat="server"  
                              AutoPostBack="true" 
                              CssClass="large m-wrap"
                              OnSelectedIndexChanged="ddlCompetition_OnSelectedIndexChanged">
            </asp:DropDownList>
            
            <font Color="red">
                 <b>
                    *
                 </b>
            </font>

            <asp:RequiredFieldValidator ID="RFVddlSelectLocation" runat="server" 
                                        ErrorMessage="Must be Select Competition" Display="None"
                                        ControlToValidate="ddlCompetitionList" SetFocusOnError="true" 
                                        Text="" 
                                        ClientIDMode="Static" ValidationGroup="Sports" 
                                        InitialValue="0">
            </asp:RequiredFieldValidator>
                              
            <asp:DropDownList ID="ddlTeamList" runat="server" CssClass="large m-wrap" AutoPostBack="true"
                              OnSelectedIndexChanged="ddlTeamList_OnSelectedIndexChanged"></asp:DropDownList>

            <asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_OnClick" 
                        OnClientClick="javascript:return confirm('Are you sure to assign players for all matches for all teams for this competition?');" 
                        CssClass="btn blue" style="margin-bottom:10px;" Text="Assign All" ValidationGroup="Sports"/>
                &nbsp;

            <asp:Label ID="lblAssignStatus" runat="server" style="color:#fff;" 
                       Text="&#10004; Assigned Successfully" Visible="false"></asp:Label>
        </div> 
    </div>
    <div>
        * Select competition and team to view match details for a specific team
    </div>
     <div class="portlet box green">
			<div id="matchDetailHeader" runat="server" class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480">Match Detail</span>
				</div>
                <div class="tools">
					<a href="javascript:;" class="collapse"></a>
                </div>
			</div>

     <div class="portlet-body flip-scroll">
             
    <asp:GridView ID="gvMatch" runat="server" CssClass="table-bordered table-striped table-condensed flip-content"
                  AutoGenerateColumns="false" DataKeyNames="MatchID, CompetitionID, Team_ID"
                  ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvMatch_OnPageIndexChanging"
                  AllowPaging="true" PageSize="10" OnRowDataBound="gvMatch_OnRowDataBound" OnRowCommand="gvMatch_OnRowCommand"
                  EmptyDataText="No Records Found" 
                  EmptyDataRowStyle-ForeColor="White" Width="100%"  >
                  
                       <RowStyle CssClass="grid-row" />
        <AlternatingRowStyle CssClass="grid-row grid-row-alternet" />
        <Columns>

            <asp:TemplateField HeaderText="Start-Date" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <div class="grid-cell-inner" style="width:130px; display: inline-block;">

                <asp:HiddenField ID="hdnMatchID" runat="server" Value='<%#Eval("MatchID") %>'></asp:HiddenField>
                    <asp:HiddenField ID="hfComp_ID" runat="server" Value='<%#Eval("CompetitionID") %>'></asp:HiddenField>
                    <asp:HiddenField ID="hdnTeamID" runat="server" Value='<%#Eval("Team_ID") %>'></asp:HiddenField>
                    <asp:Label ID="lblStartDate" runat="server" Text='<%#Eval("StartDateTime") %>'></asp:Label>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="End-Date" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <div class="grid-cell-inner" style="width:130px; display: inline-block;">
                    <asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("EndDateTime") %>'></asp:Label>
                    <asp:HiddenField ID="hdmatchstatus" runat="server" Value='<%#Eval("MatchStatusId") %>'></asp:HiddenField>  
                    <asp:HiddenField ID="hfTeam_ID1" runat="server" Value='<%#Eval("TeamAID") %>'></asp:HiddenField>
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Assign Player" HeaderStyle-CssClass="grid-header-column" ItemStyle-CssClass="grid-column" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <div class="grid-cell-inner">
                    <asp:HyperLink ID="hyperAssignPlayer" runat="server" CssClass="btn blue" ForeColor="White">Assign&nbsp;</asp:HyperLink>
                    <asp:LinkButton ID="checkall" runat="server" ForeColor="White" OnClientClick="javascript:return confirm('Are you sure to assign all players for this match?');" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="checkall" Text="Assign All" CssClass="btn red"/>
                </div> 
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <PagerSettings Mode="NumericFirstLast" PageButtonCount="8" />
        <PagerStyle CssClass="paging" HorizontalAlign="Center"/>
    </asp:GridView>

        </div>

    <div id="statusDiv" runat="server">
        <asp:Label Id="lblMessage" runat="server" Text="No Data Available" Visible="false"></asp:Label>
    </div>

    <input type="hidden" runat="server" id="hidRegID" />
   </div>