<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="conTeamList.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.conTeamList" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<script type="text/javascript">var switchTo5x = true;</script>
<script type="text/javascript" src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/JS/buttons.js")%>"></script>
<script type="text/javascript">stLight.options({ publisher: "204bcd9b-f593-474b-9014-06460f154133", doNotHash: false, doNotCopy: false, hashAddressBar: false });</script>

<panel id="pnlTeamList" runat="server">
	<div id="pnlTeamDirectory" runat="server">
		
		<div class="team-listing-search-area">
            <h2 style="float:left;">Team List</h2>
            
			<div style="float: right;">
				<asp:TextBox ID="txtTeamSearch" runat="server" AutoPostBack="true" placeholder="Search For Team Name" 
                                   Width="195px" style="margin-right:4px;" OnTextChanged="txtTeamSearch_TextChanged" 
                                   CssClass="m-wrap medium team-search-box teamlistingFilterTextBox res_text" 
                                   AutoCompleteType="FirstName" />

				<asp:DropDownList ID="ddlCompetitionSearch" runat="server" CssClass="form-select res_text" Width="301px"
					                      AutoPostBack="true" OnSelectedIndexChanged="ddlCompetitionSearch_SelectedIndexChanged" />
			
				<asp:DropDownList ID="ddlGroupSearch" runat="server" CssClass="form-select res_text" Width="323px"
					                      AutoPostBack="true" OnSelectedIndexChanged="ddlGroupSearch_SelectedIndexChanged" />

				<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btnreadmore btn pix-bgcolrhvr" OnClick="btnSearch_Click" />
				<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btnreadmore btn pix-bgcolrhvr" OnClick="btnClear_Click" />

                <div class="right_div_css">
                    <span class='st_facebook_large' displayText='Facebook'></span>
                    <span class='st_twitter_large' displayText='Tweet'></span>
                    <span class='st_instagram_large' displayText='Instagram'></span>
                </div>

			</div>

		</div>

		<div class="team-listing">

			<asp:Repeater ID="rptrTeamListing" runat="server" 
                                OnItemDataBound="rptrTeamListing_ItemDataBound"
                                OnItemCommand="rptrTeamListing_OnItemCommand">
				<ItemTemplate>
					<article>
						<div class="text">
							<div class="teamImage">
							    <asp:ImageButton ID="TeamLogoImage" runat="server" CommandName="lnkToTeamPage" CommandArgument='<%#Eval("TeamID") %>' Width="40px"  ImageUrl='<%#Eval("TeamLogo")%>' CssClass="TeamPage-Logo-Wrapper-40PX" />
								<asp:Literal ID="ltrlTeamImage" runat="server" />
							</div>
							<div class="teamNamewrapper">
                                <asp:HyperLink ID="hprTeamNameLink" runat="server" CssClass="TeamDirectoryName" Text='<%#Eval("TeamName")%>'></asp:HyperLink>
								<div class="teamcode"><asp:Literal ID="teamCode" runat="server" Text='<%#Eval("TeamCode")%>' /></div>
							</div>
							<asp:HiddenField ID="hdnTeamID" runat="server" Value='<%#Eval("TeamID") %>' />
						</div>
						<div class="links">
							<span>
								<asp:HyperLink ID="team_sch_view"  runat="server" CssClass="btn" Visible="false">Fixture</asp:HyperLink>
							</span>
							<span>
								<asp:HyperLink ID="team_res_view"  runat="server" CssClass="btn" Visible="false">Result</asp:HyperLink>
							</span>
							<span>
								<asp:HyperLink ID="team_roster_view"  runat="server" CssClass="btn" Visible="false">Roster</asp:HyperLink> 
							</span>
							<span>
								<asp:HyperLink ID="team_admin_view"  runat="server" CssClass="btn" Visible="false">Management</asp:HyperLink> 
							</span>
							<span>
								<asp:HyperLink ID="team_gallerylink"  runat="server" CssClass="btn" Visible="false">Gallery</asp:HyperLink>
							</span>
							<span>
								<asp:HyperLink ID="team_newslink"  runat="server" CssClass="btn" Visible="false">News</asp:HyperLink> 
							</span>
							<span>
								<asp:HyperLink ID="team_videolink"  runat="server" CssClass="btn" Visible="false">Video</asp:HyperLink>
							</span>
						</div>
					</article>
				</ItemTemplate>
			</asp:Repeater>

      
            <div ID="divTeamDirectoryPaging" runat="server" class="pagination">
                <ul>
                    <li class="prev">
                            <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click" Text="Previous"></asp:LinkButton>
                    </li>
                         <asp:Repeater ID="RepeaterPaging" runat="server" 
                                       OnItemCommand="RepeaterPaging_ItemCommand" 
                                       OnItemDataBound="RepeaterPaging_ItemDataBound">
                          <ItemTemplate>
                              <li style="float:none;">
                    
                               <asp:Literal ID="activepage" runat="server" Visible="false" Text='<%# "<span class=\"active\">" + Eval("PageIndex") + "</span>" %>' />
                               <asp:LinkButton ID="Pagingbtn" runat="server" 
                                                     CommandArgument='<%# Eval("PageIndex") %>' CommandName="newpage" 
                                                     Text='<%# Eval("PageText") %> ' Width="20px"></asp:LinkButton>
                                </li>
                          </ItemTemplate>
                        </asp:Repeater>
                        
                    <li class="next"> 
                       <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click" Text="Next"></asp:LinkButton>
                    </li>
                </ul>
           </div>
                  

		</div>
	</div>
</panel>
<asp:PlaceHolder ID="loadSelectedControl" runat="server"></asp:PlaceHolder>



