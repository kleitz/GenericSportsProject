<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmAdminMenu.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.frmAdminMenu" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnCssInclude FilePath="~/DesktopModules/ThSport/CSS/jquery.datetimepicker.css" runat="server"/>
<dnn:DnnJsInclude FilePath="~/DesktopModules/ThSport/JS/jquery.datetimepicker.js" runat="server"/>

<div class="page-sidebar nav-collapse collapse">
    <ul class="page-sidebar-menu">
        <li id="li_HomeLink" runat="server">
            <asp:HyperLink id="HomeLink" runat="server" ToolTip="Home" > Home </asp:HyperLink>
	    </li>
       
        <li id="li_SportsRegistration" runat="server" >
		    <asp:HyperLink id="hlSports" runat="server" ToolTip=" Sports Form "> Sports  <span class="selected"></span></asp:HyperLink>
	    </li>

        <li id="li_ClubeRegistrationLink" runat="server">
		    <asp:HyperLink id="hlClub" runat="server" ToolTip=" Club Form" > Club </asp:HyperLink>
	    </li>

        <li id="li_SeasonLink" runat="server">
	        <asp:HyperLink id="hlSeason" runat="server" ToolTip=" Season Form"> Season </asp:HyperLink>
		</li>

        <li id="li_Competition" runat="server">
	        <asp:HyperLink id="hlCompetition" runat="server" ToolTip="Competition Form">Competition </asp:HyperLink>
		</li>
        
        <li id="li_Registration" runat="server">
	        <asp:HyperLink id="hlRegistration" runat="server" ToolTip=" Registration Form "> Registration </asp:HyperLink>
		</li>

        <li id="li_Division" runat="server">
            <asp:HyperLink id="hlDivision" runat="server" ToolTip="Division Form"> Division </asp:HyperLink>
        </li>

        <li id="li_Team" runat="server">
	        <asp:HyperLink id="hlTeam" runat="server" ToolTip="Team Form">Team </asp:HyperLink>
		</li>

        <li id="li_Event" runat="server">
	        <asp:HyperLink id="hlEvent" runat="server" ToolTip=" Event Form"> Event </asp:HyperLink>
		</li>

        <li id="li_Sponsor" runat="server">
	        <asp:HyperLink id="hlSponsor" runat="server" ToolTip=" Sponsor Form"> Sponsor </asp:HyperLink>
		</li>
        <li id="li1" runat="server">
	        <asp:HyperLink id="hlAssignPlayerInTeam" runat="server" ToolTip=" Sponsor Form"> Assign Player In Team </asp:HyperLink>
		</li>
        
        <li id="li_Media" runat="server">
            <asp:HyperLink id="hlMedia" runat="server" ToolTip=" Media Form " NavigateUrl="javascript:;"> Media Form <span class="arrow selected"></span></asp:HyperLink>
            <ul class="sub-menu">
                <li id="li_News" runat="server">
	                <asp:HyperLink id="hlNews" runat="server" ToolTip=" News Form"> News </asp:HyperLink>
		        </li>
                <li id="li_Pictures" runat="server">
	                <asp:HyperLink id="hlPictures" runat="server" ToolTip=" Pictures Form"> Pictures </asp:HyperLink>
		        </li>
                <li id="li_Videos" runat="server">
	                <asp:HyperLink id="hlVideos" runat="server" ToolTip=" Videos Form"> Videos </asp:HyperLink>
		        </li>
                <%--<li id="li_Albam" runat="server">
	                <asp:HyperLink id="hlAlbam" runat="server" ToolTip=" Albam Form"> Albam </asp:HyperLink>
		        </li>--%>
           </ul>
	    </li>

        <li id="li_Masters" runat="server">
            <asp:HyperLink id="hlMaster" runat="server" ToolTip=" Master Form " NavigateUrl="javascript:;"> Masters Form <span class="arrow selected"></span></asp:HyperLink>
            <ul class="sub-menu">
              <li id="li_ClubMemberType" runat="server">
                  <asp:HyperLink id="hlClubMemberType" runat="server" ToolTip=" Club Member Type Form"> Club Member Position </asp:HyperLink>
              </li>
              <li id="li_UserType" runat="server">
                  <asp:HyperLink id="hlUserType" runat="server" ToolTip=" User Type Form"> User Type </asp:HyperLink>
              </li>
              <li id="li_AddDocumentsType" runat="server">
                  <asp:HyperLink id="hlAddDocumentsType" runat="server" ToolTip=" Documents Type  Form"> Documents Type  </asp:HyperLink>
              </li>
              <li id="li_CompetitionType" runat="server">
                  <asp:HyperLink id="hlCompetitionType" runat="server" ToolTip=" Competition Type Form"> Competition Type </asp:HyperLink>
              </li>
              <li id="li_CompetitionLeague" runat="server">
                  <asp:HyperLink id="hlCompetitionLeague" runat="server" ToolTip=" Competition League Form"> Competition League </asp:HyperLink>
              </li>
              <li id="li_CompetitionFormat" runat="server">
                  <asp:HyperLink id="hlCompetitionFormat" runat="server" ToolTip="Competition Format Form"> Competition Format </asp:HyperLink>
              </li>
              <li id="li_SponsorType" runat="server">
                  <asp:HyperLink id="hlSponsorType" runat="server" ToolTip=" Sponsor Type Form "> Sponsor Type </asp:HyperLink>
              </li>
              <li id="li_SponsorLevel" runat="server">
                  <asp:HyperLink id="hlSponsorLevel" runat="server" ToolTip=" Sponsor Level Form "> Sponsor Level </asp:HyperLink>
              </li>
              <li id="li_PlayerType" runat="server">
                  <asp:HyperLink id="hlPlayerType" runat="server" ToolTip=" Player Position Form "> Player Position </asp:HyperLink>
              </li>
              <li id="li_TeamMemberType" runat="server">
                  <asp:HyperLink id="hlTeamMemberType" runat="server" ToolTip=" Team Member Position Form "> Team Member Position </asp:HyperLink>
              </li>  
              <li id="li_Location" runat="server">
                  <asp:HyperLink id="hlLocation" runat="server" ToolTip=" Match Location Form "> Location </asp:HyperLink>
              </li>
              <li id="li_MatchStatus" runat="server">
                  <asp:HyperLink id="hlMatchStatus" runat="server" ToolTip=" Match Status Form "> Match Status </asp:HyperLink>
              </li>  
               <li id="li_MatchType" runat="server">
                  <asp:HyperLink id="hlMatchType" runat="server" ToolTip=" Match Type Form "> Match Type </asp:HyperLink>
              </li>  
              <%--<li id="li_UserRole" runat="server">
                  <asp:HyperLink id="hlUserRole" runat="server" ToolTip=" User Role Form "> User Role </asp:HyperLink>
              </li>  --%>
           </ul>
	    </li>
        
<%--        
        <li id="li_TournamentLink" runat="server">
	        <asp:HyperLink id="TournamentLink" runat="server" ToolTip="Tournament"> Tournament <span class="selected"></span> </asp:HyperLink>
		</li>
        <li id="li_CompetitionLink" runat="server">
		    <asp:HyperLink id="CompetitionLink" runat="server" ToolTip="Competition"> Competition <span class="selected"></span> </asp:HyperLink>
		</li>
        <li id="li_SponsorRegLink" runat="server">
		    <asp:HyperLink id="SponsorRegLink" runat="server"  ToolTip="Sponsor"> Sponsor <span class="selected"></span></asp:HyperLink>
		</li>
        <li id="li_TeamProfile" runat="server">
		    <asp:HyperLink id="TeamProfile" runat="server"  ToolTip="Team"> Team <span class="selected"></span></asp:HyperLink>
		</li>
        <li id="li_UserRegLink" runat="server">
		    <asp:HyperLink id="UserRegLink" runat="server" ToolTip=" Player "> Player <span class="selected"></span></asp:HyperLink>
        </li>--%>

      <%--  <li id="li_LocationLink" runat="server">
		    <asp:HyperLink id="LocationLink" runat="server"  ToolTip="Location"> Location <span class="selected"></span></asp:HyperLink>
		</li>
        
        <li id="li_NewsLink" runat="server">
		    <asp:HyperLink id="Newlink" runat="server" ToolTip="News"> News <span class="selected"></span></asp:HyperLink>
		</li>--%>
        
     </ul>
</div>
         
<div class="breadcrumbs" style="display:none;">
    <ul>
       <li class="home"><asp:HyperLink id="titela" runat="server"></asp:HyperLink> </li>
       <li><asp:Label ID="titel" runat="server" ></asp:Label></li>
       <li><asp:Label ID="Subtitle" runat="server" ></asp:Label></li>
             <asp:HiddenField ID="hdcompetitionid" runat="server"/>
    </ul>
</div>
        
<div class="page-content">
    <div class="container-fluid">
		<!-- BEGIN PAGE HEADER-->   
		<div class="row-fluid">
            <asp:PlaceHolder ID="loadSelectedControl" runat="server"></asp:PlaceHolder>
        </div>
    </div> 
</div>


<script type="text/javascript">

    $('.datetimepicker').datetimepicker()
	.datetimepicker({ value: '', step: 10 });

    $('.enddatetimepicker').datetimepicker()
	.datetimepicker({ value: '', step: 10 });

</script>
