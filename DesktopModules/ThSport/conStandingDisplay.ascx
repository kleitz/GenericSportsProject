<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="conStandingDisplay.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.conStandingDisplay" %>


<div class="textwidget">
	<div class="pointtable-shortcode">
	    <ul runat="server" id="resultUl">	

<asp:Repeater ID="rptrGroups" runat="server" OnItemDataBound="rptrGroups_ItemDataBound">
<ItemTemplate>
    
    <li style="list-style:inherit;">

            
         <asp:HiddenField ID="hfDistinctGroupId" Value='<%# Eval("id") %>' runat="server" />

              <div class="competionAllDetailGroupName" style="border:none !important;" >
                     <asp:Label ID="lblSelectGroup" CssClass="CompetitionGroupName" runat="server" Text=''></asp:Label>
             </div>


              <div class="points-table fullwidth">

                <asp:GridView ID="gvTeamsView" runat="server" AutoGenerateColumns="false" AllowSorting="true" 
                                OnSorting="gvTeamsView_Sorting" OnRowDataBound="gvTeamsView_OnRowDataBound" 
                                OnRowCommand="gvTeamsView_OnRowCommand" CssClass="table rt1">

                  	<HeaderStyle CssClass="box1"/>
               <Columns>

            <asp:TemplateField SortExpression="TeamRank">
                <HeaderTemplate>
					<span class="box1">POS</span>
				</HeaderTemplate>
                <ItemTemplate>
                         <asp:Literal ID="ltrlPos" runat="server" Text='<%#Eval("TeamRank") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  SortExpression="TeamName" HeaderStyle-Width="3%" ItemStyle-CssClass="CenterAlign">
                <HeaderTemplate>
                    <span class="box1">Team</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="imgTeamLogo"  BorderStyle="None" CssClass="TeamPage-Logo-Wrapper-ForStanding-30PX" runat="server" ImageUrl='<%#Eval("TeamLogo") %>' 
                            AlternateText="" 
                            CommandName="teamName" CommandArgument='<%# Eval("TeamID") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="TeamName" HeaderStyle-Width="70%" ItemStyle-CssClass="CenterAlign">
                
                <ItemTemplate>
                    <div class="break-word">
                        <asp:LinkButton ID="lnkTeamName" runat="server" Text='<%#Eval("TeamName") %>' CommandName="teamName"
                              CommandArgument='<%# Eval("TeamID") %>'></asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField SortExpression = "Pts">
                <HeaderTemplate>
					<span class="box1">PTS</span>
				</HeaderTemplate>
                <ItemTemplate>
                    <div class="Standingcupleague">
                        <asp:Literal ID="ltrlPts" runat="server" Text='<%#Eval("Pts") %>' />
                    </div> 
                     <asp:HiddenField ID="hdnPts" runat="server" Value='<%#Eval("Pts") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField SortExpression = "Played">
                <HeaderTemplate>
					<span class="box1">P</span>
				</HeaderTemplate>
                <ItemTemplate>
                    <div class="Standingcupleague">
                        <asp:Literal ID="Literal6" runat="server" Text='<%#Eval("Played") %>' />
                        </div> 

                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField SortExpression = "Win">
                <HeaderTemplate>
					<span class="box1">W</span>
				</HeaderTemplate>
                <ItemTemplate>
                    <div class="Standingcupleague">
                        <asp:Literal ID="ltrlWin" runat="server" Text='<%#Eval("Win") %>' />
                     </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField SortExpression = "Draw">
                <HeaderTemplate>
					<span class="box1">D</span>
				</HeaderTemplate>
                <ItemTemplate>
                    <div class="Standingcupleague">
                        <asp:Literal ID="ltrlDraw" runat="server" Text='<%#Eval("Draw") %>' />
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField SortExpression = "Loss">
                <HeaderTemplate>
					<span class="box1">L</span>
				</HeaderTemplate>
                <ItemTemplate>
                    <div class="Standingcupleague">
                        <asp:Literal ID="Literal4" runat="server" Text='<%#Eval("Loss") %>' />
                    </div> 

                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField SortExpression = "GF">
                <HeaderTemplate>
					<span class="box1">GF</span>
				</HeaderTemplate>
                <ItemTemplate>
                    <div class="Standingcupleague">
                        <asp:Literal ID="Literal7" runat="server" Text='<%#Eval("GF") %>' />
                    </div> 

                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField SortExpression = "GA">
                <HeaderTemplate>
					<span class="box1">GA</span>
				</HeaderTemplate>
                <ItemTemplate>
                    <div class="Standingcupleague">
                        <asp:Literal ID="Literal9" runat="server" Text='<%#Eval("GA") %>' />
                    </div> 
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField SortExpression = "GD"> 
                <HeaderTemplate>
					<span class="box1">GD</span>
				</HeaderTemplate>
                <ItemTemplate>
                    <div class="Standingcupleague">
                        <asp:Literal ID="Literal5" runat="server" Text='<%#Eval("GD") %>' />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
     </div>
    </li>
      
        
</ItemTemplate>
</asp:Repeater>
        </ul>    
        </div>
    </div>
