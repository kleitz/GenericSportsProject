<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="conCompetitionList.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.conCompetitionList" %>

<%--<script type="text/javascript">
    function imgError(image) {
        image.onerror = "";
        image.src = "/DesktopModules/SportSite/Images/no-photo-available.jpg";
        return true;
    }

</script>--%>


<%--<script type="text/javascript">var switchTo5x = true;</script>
<script type="text/javascript" src="<%= Page.ResolveUrl("~/DesktopModules/SportSite/JS/buttons.js")%>"></script>
<script type="text/javascript">stLight.options({ publisher: "204bcd9b-f593-474b-9014-06460f154133", doNotHash: false, doNotCopy: false, hashAddressBar: false });</script>--%>

<asp:Panel id="pnlForBreadCrumbs" runat="server">
       <div  class="breadcrumbs">
            <ul>
                <li class="home"><asp:HyperLink id="titela" runat="server"></asp:HyperLink> </li>
                <li><asp:Label ID="titel" runat="server" ></asp:Label></li>
            </ul>
        </div>
</asp:Panel>

<asp:Panel id="breadcrumbdiv" runat="server">
   
      <div style="width: 100%;">
          <header class="pix-heading-title">
			    <h2 class="pix-section-title heading-color">
                      <asp:Literal ID="Literal1" runat="server"/>
                </h2>
                <div class="input-type" style="float:right;">
                     <div class="CompetitionTopScore" style="margin-right:5px;">
                            <asp:DropDownList ID="ddlSeason" runat="server" Width="250px" 
                                                      Height="35px" AutoPostBack="true"
                                                      OnSelectedIndexChanged="ddlSeason_OnSelectedIndexChanged">
                            </asp:DropDownList>
                    </div>
                      <div id="divdlcompetitioncup" runat="server" class="CompetitionTopScore" style="margin-right:5px;" Visible="false">
                            <asp:DropDownList ID="ddlCompetitionListCup" runat="server" Width="250px" 
                                                      Height="35px" AutoPostBack="true"
                                                     OnSelectedIndexChanged="ddlCompetitionListCup_OnSelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlCompetitionListLeague" runat="server" Width="250px" 
                                                      Height="35px" AutoPostBack="true"
                                                      OnSelectedIndexChanged="ddlCompetitionListLeague_OnSelectedIndexChanged">
                            </asp:DropDownList>
                       </div>
                       <div class="right_div_css">
                            <span class='st_facebook_large' displayText='Facebook'></span>
                            <span class='st_twitter_large' displayText='Tweet'></span>
                            <span class='st_instagram_large' displayText='Instagram'></span>
                        </div>
                 </div>
          </header>
     </div>  
<center>

<div id="divNoDataAvailable" runat="server" visible="false">
        <div class="fixtureNoDataAvailable">
            <asp:Literal ID="NoDataAvailable" runat="server" Text="No Data Available "></asp:Literal>
        </div>       
</div>
     
    <div id="pnlCompetitionList" runat="server" class="CompetitionList-MainContainerCupLeagues" style="padding-top:20px;">
        <div>
        <asp:Repeater id="rptrCompetitionList" runat="server" OnItemDataBound="rptrCompetitionList_ItemDataBound"  OnItemCommand="rptrCompetitionList_ItemCommand">
            <ItemTemplate>
                <asp:HiddenField ID="hdnCompId" Value='<%# Eval("Comp_RegID") %>' runat="server" />
                <asp:HiddenField ID="hdnSchedularType" runat="server" Value='<%# Eval("ScheduleType") %>' />
                <div class="rptrCompetitionListWrapper" >
                     <div class="CompetitionList-area">
	                 <div class="CompetitionList-header">
			            <td class="grid-column">

				            <div class="rptrCompetitionListlvl1">

					            <div class="rptrCompetitionListSponsorLogo" >
                                    <asp:Image ID="imgSponsor" ImageUrl='<%# (Eval("CompetitionImagePath")) %>' CssClass="sponsorImageForCompetitionList" AlternateText="" runat="server" onError="imgError(this);"/>
                                </div>

					            <div class="rptrCompetitionListComp_Desc" >
            
                                        <div class="Competition-Name">
                                             <header class="CompetitionNameWithOutLine">
	                                            <h2 class="pix-section-title heading-color">
                                                     <asp:HyperLink ID="hlnkCompTitle"  runat="server">
                                                        <asp:Literal ID="ltrlCompTitle" Text='<%# Eval("Comp_Title") %>' runat="server"></asp:Literal>
                                                      </asp:HyperLink>
                                                  </h2></header>
                                        </div>
                                        <div class="rptrCompetitionListTeamAndGroupLeft" ><span>
	                                        <asp:Literal ID="ltrlTotalTeam" Text='<%# Eval("TotalTeam") %>' runat="server" />
	                                        </span><br/>
	                                        <span>
	                                        <asp:Literal ID="ltrlTotalGroup" Text='<%# Eval("TotalGroup") %>' runat="server" />
	                                        </span>
		    
                                        </div>
                                        <div class="rptrCompetitionListTeamAndGroupRight">
                                            <span>Start Date :
                                                <font color="black" >
                                                    <asp:Literal ID="ltrlStartDate" Text='<%# GetFormattedDate(Eval("StartDate")) %>' runat="server" />
                                                </font>
                                            </span>
                                            <br />
                                            <span>End  Date : <font color="black">
                                            <asp:Literal ID="ltrlEndDate" Text='<%# GetFormattedDate(Eval("EndDate")) %>' runat="server" /></font></span>
                                        </div>
						                <div style="display:none;">
                                            <time datetime="<%# Eval("StartDate", "{0:d}") %>" class="icon">
                                                <em><%# Eval("StartDate", "{0:yyyy}") %></em>
                                                <strong><%# Eval("StartDate", "{0:m}") %></strong>
                                                <span><%# Eval("StartDate", "{0:d}") %></span>
                                            </time>
                                        </div>
                                        <div class="rptrCompetitionListComp_DescInner" style="float:left;">
                                            <asp:Literal ID="ltrlCompDesc" Text='<%# Eval("Comp_Desc") %>' runat="server"></asp:Literal>
                                        </div>
						                <div class="rptrCompetitionListTeamAndGroup" style="float:left;">
							                <div class="competition-allteam">
                                                <asp:Literal ID="ltrlTeams" Text="" runat="server"></asp:Literal>
                                            </div>  
						                </div>
					                </div>
				                </div>
			                </td>
			                <div>
		            </div>
		            <div class="CompetitionList-footer">
			            <div class="CompetitionList-footer-bar">
                            <span>
                                <asp:HyperLink ID="comp_group_view"  runat="server" CssClass="Competitionbtn" Visible="false">Group & teams</asp:HyperLink> 
				            </span>
                            <span>
					            <asp:HyperLink ID="comp_sch_view"  runat="server" CssClass="Competitionbtn" Visible="false">Fixture</asp:HyperLink>
				            </span>
				            <span>
					            <asp:HyperLink ID="comp_res_view"  runat="server" CssClass="Competitionbtn" Visible="false">Result</asp:HyperLink>
				            </span>
                            <span>
					            <asp:HyperLink ID="comp_gallerylink"  runat="server" CssClass="Competitionbtn" Visible="false">Gallery</asp:HyperLink>
				            </span>
                            <span>
                                <asp:HyperLink ID="comp_newslink"  runat="server" CssClass="Competitionbtn" Visible="false">
                                    News
                                </asp:HyperLink> 
				            </span>
                            <span>
					            <asp:HyperLink ID="comp_videolink"  runat="server" CssClass="Competitionbtn" Visible="false">Video</asp:HyperLink>
				            </span>
			            </div>
		            </div>
                    </div>
                </div>
                </div>
            </ItemTemplate>
            <SeparatorTemplate>
            </SeparatorTemplate>
        </asp:Repeater>
        </div>
    </div>
</center>
</asp:Panel>
<asp:PlaceHolder ID="loadSelectedControl" runat="server"></asp:PlaceHolder>
