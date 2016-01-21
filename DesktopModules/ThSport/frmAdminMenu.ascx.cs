using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Framework;
using DotNetNuke.Entities.Modules;
using ThSportServer;
using DotNetNuke.Entities.Users;


namespace DotNetNuke.Modules.ThSport
{
    public partial class frmAdminMenu : PortalModuleBase
    {
        private string m_ModuelControl = "";
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();
        DotNetNuke.Entities.Tabs.TabController tabs1 = new Entities.Tabs.TabController();
        DotNetNuke.Entities.Tabs.TabInfo tInfo1 = new Entities.Tabs.TabInfo();

        protected void Page_Load(object sender, EventArgs e)
        {

            //Register Jquery Usage
            jQuery.RequestRegistration();
            jQuery.RequestUIRegistration();
            jQuery.RequestDnnPluginsRegistration();

            if (currentUser.IsSuperUser || currentUser.IsInRole("clubadmin"))
            {
                m_ModuelControl = "frmSports.ascx";
            }
            else
            {
                m_ModuelControl = "frmSeason.ascx";

                li_HomeLink.Visible = false;
                li_SportsRegistration.Visible = false;
                li_SeasonLink.Visible = false;
                li_ClubeRegistrationLink.Visible = false;
                li_Masters.Visible = false;
                li_ClubMemberType.Visible = false;
                li_UserType.Visible = false;
                li_Registration.Visible = false;
                li_AddDocumentsType.Visible = false;
                li_Event.Visible = false;
                li_SponsorType.Visible = false;
                
                //li_TournamentLink.Visible = false;
                
                //li_SponsorRegLink.Visible = false;
                //li_TeamProfile.Visible = false;
                //li_UserRegLink.Visible = false;
                //li_LocationLink.Visible = false;
                //li_NewsLink.Visible = false;
                //li_NationalTeamLink.Visible = false;

                //li_NationalTeamListLink.Visible = false;
                //li_NationalTeamPlayerAssignInGameLink.Visible = false;

                //li_PollLink.Visible = false;
                //li_SuspensionLink.Visible = false;
                //li_AssignAwards.Visible = false;
                //li_AssignAwardsPosition.Visible = false;
                //li_AwardsList.Visible = false;
                //li_AssignWinner.Visible = false;
                //li_AssignWinnerPosition.Visible = false;
                //li_WinnerList.Visible = false;

                //li_Reports.Visible = false;
                //li_ReportTopScorer.Visible = false;
            }

            try
            {
                LoadModuleControl();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }

            if (!Page.IsPostBack)
            {
                //Get Home Page TabId
                tInfo1 = tabs1.GetTabByName("Home", PortalId);

                HomeLink.NavigateUrl = Globals.NavigateURL(tInfo1.TabID, "");
                hlSports.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmSports");
                hlSeason.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmSeason");
                hlClub.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClub");
                hlClubMemberType.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMemberType");
                hlUserType.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmUserType");
                hlRegistration.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmRegistration");
                hlAddDocumentsType.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmAddDocumentsType");
                hlEvent.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmEvent");
                hlSponsorType.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmSponsorType");

                hlCompetitionType.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmCompetitionType");
                hlCompetitionLeague.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmCompetitionLeague");
                hlCompetitionFormat.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmCompetitionFormat");
                hlCompetition.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmCompetition");
                hlDivision.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmDivision");

                hlTeam.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmTeam");
            }

        }

        private void LoadModuleControl()
        {
            if (Request.QueryString["mctl"] != null)//3. Read control name from querystring
            {
                m_ModuelControl = Request.QueryString["mctl"].ToString() + ".ascx";
            }

            if (Request.QueryString["mctl"] == "Password")
            {
                m_ModuelControl = "Admin/Security/" + Request.QueryString["mctl"].ToString() + ".ascx";
            }

            switch (m_ModuelControl)
            {
                
                case "frmSports.ascx":
                    HtmlGenericControl li_SportsRegistration = this.li_SportsRegistration as HtmlGenericControl;
                    if (li_SportsRegistration != null)
                        this.li_SportsRegistration.Attributes.Add("class", "active");
                    titela.Text = "Sports &raquo;";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmSports");
                    break;

                case "frmSeason.ascx":
                    HtmlGenericControl li_SeasonLink = this.li_SeasonLink as HtmlGenericControl;
                    if (li_SeasonLink != null)
                        this.li_SeasonLink.Attributes.Add("class", "active");
                    titela.Text = "Season &raquo;";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Season");
                    break;

                case "frmClub.ascx":
                    HtmlGenericControl li_ClubeRegistrationLink = this.li_ClubeRegistrationLink as HtmlGenericControl;
                    if (li_ClubeRegistrationLink != null)
                        this.li_ClubeRegistrationLink.Attributes.Add("class", "active");
                    titela.Text = "Club Profile &raquo;";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClub");
                    break;

                case "frmClubMemberType.ascx":
                    HtmlGenericControl li_ClubMemberType = this.li_ClubMemberType as HtmlGenericControl;
                    if (li_ClubMemberType != null)
                        this.li_ClubMemberType.Attributes.Add("class", "active");
                    titela.Text = "Club Member Type &raquo;";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMemberType");
                    break;

                case "frmUserType.ascx":
                    HtmlGenericControl li_UserType = this.li_UserType as HtmlGenericControl;
                    if (li_UserType != null)
                        this.li_UserType.Attributes.Add("class", "active");
                    titela.Text = "User Type &raquo;";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmUserType");
                    break;

                case "frmCompetitionType.ascx":
                    HtmlGenericControl li_CompetitionType = this.li_CompetitionType as HtmlGenericControl;
                    if (li_CompetitionType != null)
                        this.li_CompetitionType.Attributes.Add("class", "active");
                    titela.Text = "Competition Type &raquo;";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmCompetitionType");
                    break;

                case "frmCompetitionLeague.ascx":
                    HtmlGenericControl li_CompetitionLeague = this.li_CompetitionLeague as HtmlGenericControl;
                    if (li_CompetitionLeague != null)
                        this.li_CompetitionLeague.Attributes.Add("class", "active");
                    titela.Text = "Competition League &raquo;";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmCompetitionLeague");
                    break;

                case "frmRegistration.ascx":
                    HtmlGenericControl li_Registration = this.li_Registration as HtmlGenericControl;
                    if (li_Registration != null)
                        this.li_UserType.Attributes.Add("class", "active");
                    titela.Text = "Registration &raquo;";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmRegistration");
                    break;


                case "frmCompetition.ascx":
                    HtmlGenericControl li_Competition = this.li_Competition as HtmlGenericControl;
                    if (li_Competition != null)
                        this.li_Competition.Attributes.Add("class", "active");
                    titela.Text = "Competition &raquo;";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmCompetition");
                    break;

                case "frmCompetitionGroup.ascx":
                    HtmlGenericControl li_CompetitionGroup = this.li_Competition as HtmlGenericControl;
                    if (li_CompetitionGroup != null)
                        this.li_Competition.Attributes.Add("class", "active");
                    titela.Text = "Competition";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmCompetition");
                    titel.Text = "&raquo; Competition Group";
                    break;

                case "frmDivision.ascx":
                    HtmlGenericControl li_Division = this.li_Division as HtmlGenericControl;
                    if (li_Division != null)
                        this.li_Division.Attributes.Add("class", "active");
                    titela.Text = "Division";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmDivision");
                    titel.Text = "&raquo; Division";
                    break;

                case "frmAddDocumentsType.ascx":
                    HtmlGenericControl li_AddDocumentsType = this.li_AddDocumentsType as HtmlGenericControl;
                    if (li_AddDocumentsType != null)
                        this.li_AddDocumentsType.Attributes.Add("class", "active");
                    titela.Text = "Documents Type &raquo;";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmAddDocumentsType");
                    break;

                case "frmEvent.ascx":
                    HtmlGenericControl li_Event = this.li_Event as HtmlGenericControl;
                    if (li_Event != null)
                        this.li_Event.Attributes.Add("class", "active");
                    titela.Text = "Event &raquo;";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmEvent");
                    break;

                case "frmSponsorType.ascx":
                    HtmlGenericControl li_SponsorType = this.li_SponsorType as HtmlGenericControl;
                    if (li_SponsorType != null)
                        this.li_SponsorType.Attributes.Add("class", "active");
                    titela.Text = "Sponsor Type &raquo;";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmSponsorType");
                    break;

                //case "frmDivision.ascx":
                //    HtmlGenericControl li_Division = this.li_Division as HtmlGenericControl;
                //    if (li_Division != null)
                //        this.li_Division.Attributes.Add("class", "active");
                //    titela.Text = "Division";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmDivision");
                    
                //    break;

                    break;

                case "frmTeam.ascx":
                    HtmlGenericControl li_Team = this.li_Team as HtmlGenericControl;
                    if (li_Team != null)
                        this.li_Team.Attributes.Add("class", "active");
                    titela.Text = "Team &raquo;";
                    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmTeam");
                    break;

                //case "CompetitionVideo.ascx":
                //    HtmlGenericControl li_CompetitionVideoLink = this.li_CompetitionLink as HtmlGenericControl;
                //    if (li_CompetitionVideoLink != null)
                //        this.li_CompetitionLink.Attributes.Add("class", "active");
                //    titela.Text = "Competition";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Competition");
                //    titel.Text = "&raquo; Competition Video";
                //    break;

                //case "CompetitionMatchPlayerSuspended.ascx":
                //    HtmlGenericControl li_CompetitionMatchPlayerSuspended = this.li_CompetitionLink as HtmlGenericControl;
                //    if (li_CompetitionMatchPlayerSuspended != null)
                //        this.li_CompetitionLink.Attributes.Add("class", "active");
                //    titela.Text = "Competition";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Competition");
                //    titel.Text = "&raquo; Match Player Suspended";
                //    break;

                //case "CompMultiSponsor.ascx":
                //    HtmlGenericControl li_CompetitionMSLink = this.li_CompetitionLink as HtmlGenericControl;
                //    if (li_CompetitionMSLink != null)
                //        this.li_CompetitionLink.Attributes.Add("class", "active");
                //    titela.Text = "Competition";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Competition");
                //    titel.Text = "&raquo; Competition Sponser";
                //    break;

                //case "CompetitionGroup.ascx":
                //    HtmlGenericControl li_CompetitiongroupLink = this.li_CompetitionLink as HtmlGenericControl;
                //    if (li_CompetitiongroupLink != null)
                //        this.li_CompetitionLink.Attributes.Add("class", "active");
                //    titela.Text = "Competition";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Competition");
                //    titel.Text = "&raquo; Competition Group";
                //    break;

                //case "ScoreKeeper.ascx":
                //    HtmlGenericControl li_CompetitionSkLink = this.li_CompetitionLink as HtmlGenericControl;
                //    if (li_CompetitionSkLink != null)
                //        this.li_CompetitionLink.Attributes.Add("class", "active");
                //    titela.Text = "Competition";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Competition");
                //    titel.Text = "&raquo; ScoreKeeper";
                //    break;

                //case "CompetitionPointSystem.ascx":
                //    HtmlGenericControl li_CompetitionPointink = this.li_CompetitionLink as HtmlGenericControl;
                //    if (li_CompetitionPointink != null)
                //        this.li_CompetitionLink.Attributes.Add("class", "active");
                //    titela.Text = "Competition";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Competition");
                //    titel.Text = "&raquo; Competition Point";
                //    break;

                //case "MatchSchedule.ascx":
                //    HtmlGenericControl li_Competitionmstink = this.li_CompetitionLink as HtmlGenericControl;
                //    if (li_Competitionmstink != null)
                //        this.li_CompetitionLink.Attributes.Add("class", "active");
                //    titela.Text = "Competition";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Competition");
                //    titel.Text = "&raquo; Match Schedule";
                //    break;

                //case "Match.ascx":
                //    HtmlGenericControl li_CompetitionMatchLink = this.li_CompetitionLink as HtmlGenericControl;
                //    if (li_CompetitionMatchLink != null)
                //        this.li_CompetitionLink.Attributes.Add("class", "active");
                //    titela.Text = "Competition";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Competition");
                //    titel.Text = "&raquo; Match";
                //    break;

                //case "MatchResult.ascx":
                //    HtmlGenericControl li_CompetitionMatchResultLink = this.li_CompetitionLink as HtmlGenericControl;
                //    if (li_CompetitionMatchResultLink != null)
                //        this.li_CompetitionLink.Attributes.Add("class", "active");
                //    titela.Text = "Competition";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Competition");
                //    string newurl = "Match";
                //    int competition_id = 0;
                //    int.TryParse(Session["competionid"].ToString(), out competition_id);
                //    DotNetNuke.Entities.Tabs.TabController tabControl = new Entities.Tabs.TabController();
                //    DotNetNuke.Entities.Tabs.TabInfo tabInfo = tabControl.GetTabByName("clubadmin", PortalId);
                //    titel.Text = "&raquo; <a href= " + DotNetNuke.Common.Globals.NavigateURL(tabInfo.TabID, "", "mctl=" + "Match" + "&CompetitionID=" + competition_id) + ">Match</a>";
                //    Subtitle.Text = "&raquo; Match Result";
                //    break;

                //case "SponsorReg.ascx":
                //    HtmlGenericControl li_SponsorRegLink = this.li_SponsorRegLink as HtmlGenericControl;
                //    if (li_SponsorRegLink != null)
                //        this.li_SponsorRegLink.Attributes.Add("class", "active");
                //    titela.Text = "Sponsor &raquo;";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "SponsorReg");
                //    break;

                

                //case "TeamMasterPlayerCoach.ascx":
                //    HtmlGenericControl li_TeamMasterPlayerCoachProfile = this.li_TeamProfile as HtmlGenericControl;
                //    if (li_TeamMasterPlayerCoachProfile != null)
                //        this.li_TeamProfile.Attributes.Add("class", "active");
                //    titela.Text = "TeamMasterPlayerCoach &raquo;";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamMaster");
                //    break;

                //case "NationalTeam.ascx":
                //    HtmlGenericControl li_NationalTeamLink = this.li_NationalTeamLink as HtmlGenericControl;
                //    if (li_NationalTeamLink != null)
                //        this.li_NationalTeamLink.Attributes.Add("class", "active");
                //    titela.Text = "National Team &raquo;";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "NationalTeam");
                //    break;

                //case "frmNationalTeamList.ascx":
                //    HtmlGenericControl li_NationalTeamListLink = this.li_NationalTeamListLink as HtmlGenericControl;
                //    if (li_NationalTeamListLink != null)
                //        this.li_NationalTeamListLink.Attributes.Add("class", "active");
                //    titela.Text = "National Team List &raquo;";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmNationalTeamList");
                //    break;

                //case "frmNationalTeamPlayerAssignInGame.ascx":
                //    HtmlGenericControl li_NationalTeamPlayerAssignInGameLink = this.li_NationalTeamPlayerAssignInGameLink as HtmlGenericControl;
                //    if (li_NationalTeamPlayerAssignInGameLink != null)
                //        this.li_NationalTeamPlayerAssignInGameLink.Attributes.Add("class", "active");
                //    titela.Text = " National Team Player Assign In Game &raquo;";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmNationalTeamPlayerAssignInGame");
                //    break;

                //case "TeamSponControl.ascx":
                //    HtmlGenericControl li_TeamSponsor = this.li_TeamProfile as HtmlGenericControl;
                //    if (li_TeamSponsor != null)
                //        this.li_TeamProfile.Attributes.Add("class", "active");
                //    titela.Text = "Team";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Teams");
                //    titel.Text = "&raquo; Team Sponser";
                //    break;

                //case "TeamGallery.ascx":
                //    HtmlGenericControl li_TeamGallery = this.li_TeamProfile as HtmlGenericControl;
                //    if (li_TeamGallery != null)
                //        this.li_TeamProfile.Attributes.Add("class", "active");
                //    titela.Text = "Team";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Teams");
                //    titel.Text = "&raquo; Team Gallery";
                //    break;

                //case "TeamPlayerCoach.ascx":
                //    HtmlGenericControl li_TeamPlayerCoach = this.li_TeamProfile as HtmlGenericControl;
                //    if (li_TeamPlayerCoach != null)
                //        this.li_TeamProfile.Attributes.Add("class", "active");
                //    titela.Text = "Team";
                //    int comp_id = 0;
                //    if (Session["competition_id"] != null)
                //    {
                //        int.TryParse(Session["competition_id"].ToString(), out comp_id);
                //    }
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Teams" + "&CompetitionID=" + comp_id);
                //    titel.Text = "&raquo; Team PlayerCoach";
                //    break;

                //case "TeamVideo.ascx":
                //    HtmlGenericControl li_TeamVideo = this.li_TeamProfile as HtmlGenericControl;
                //    if (li_TeamVideo != null)
                //        this.li_TeamProfile.Attributes.Add("class", "active");
                //    titela.Text = "Team";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Teams");
                //    titel.Text = "&raquo; Team Video";
                //    break;

                //case "TeamAllDetail.ascx":
                //    HtmlGenericControl li_Teamprofile = this.li_TeamProfile as HtmlGenericControl;
                //    if (li_Teamprofile != null)
                //        this.li_TeamProfile.Attributes.Add("class", "active");
                //    titela.Text = "Team";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Teams");
                //    titel.Text = "&raquo; Team Profile";
                //    break;

                //case "Admin/Security/Password.ascx":
                //    HtmlGenericControl li_Teampassword = this.li_TeamProfile as HtmlGenericControl;
                //    if (li_Teampassword != null)
                //        this.li_TeamProfile.Attributes.Add("class", "active");
                //    titela.Text = "Team";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Teams");
                //    titel.Text = "&raquo; Change Password";
                //    break;

                //case "User.ascx":
                //    HtmlGenericControl li_UserRegLink = this.li_UserRegLink as HtmlGenericControl;
                //    if (li_UserRegLink != null)
                //        this.li_UserRegLink.Attributes.Add("class", "active");
                //    titela.Text = "Players / Coach &raquo;";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "User");
                //    break;

                //case "UserDocumentControl.ascx":
                //    HtmlGenericControl li_UserdocumrntLink = this.li_UserRegLink as HtmlGenericControl;
                //    if (li_UserdocumrntLink != null)
                //        this.li_UserRegLink.Attributes.Add("class", "active");
                //    titela.Text = "Players / Coach";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "User");
                //    titel.Text = "&raquo; Document";
                //    break;

                //case "frmPlayerAssignMultiplePhoto.ascx":
                //    HtmlGenericControl li_frmPlayerAssignMultiplePhoto = this.li_UserRegLink as HtmlGenericControl;
                //    if (li_frmPlayerAssignMultiplePhoto != null)
                //        this.li_UserRegLink.Attributes.Add("class", "active");
                //    titela.Text = "Player Assign Multiple Photo";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "User");
                //    titel.Text = "&raquo; Player Assign Multiple Photo";
                //    break;

                //case "Location.ascx":
                //    HtmlGenericControl li_LocationLink = this.li_LocationLink as HtmlGenericControl;
                //    if (li_LocationLink != null)
                //        this.li_LocationLink.Attributes.Add("class", "active");
                //    titela.Text = "Location &raquo;";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Location");
                //    break;

                //case "News.ascx":
                //    HtmlGenericControl li_NewsLink = this.li_LocationLink as HtmlGenericControl;
                //    if (li_NewsLink != null)
                //    {
                //        this.li_NewsLink.Attributes.Add("class", "active");
                //        titela.Text = "News &raquo;";
                //        titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "News");
                //    }
                //    break;

                //case "PollList.ascx":
                //    HtmlGenericControl li_PollLink = this.li_PollLink as HtmlGenericControl;
                //    if (li_PollLink != null)
                //        this.li_PollLink.Attributes.Add("class", "active");
                //    titela.Text = "Poll";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "PollList");
                //    break;

                //case "Suspension.ascx":
                //    HtmlGenericControl li_SuspensionLink = this.li_SuspensionLink as HtmlGenericControl;
                //    if (li_SuspensionLink != null)
                //        this.li_SuspensionLink.Attributes.Add("class", "active");
                //    titela.Text = "Suspended Players";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Suspension");
                //    break;

                //case "frmAwards.ascx":
                //    HtmlGenericControl li_AssignAwards = this.li_AssignAwards as HtmlGenericControl;
                //    if (li_AssignAwards != null)
                //        this.li_AssignAwards.Attributes.Add("class", "active");
                //    titela.Text = "Assign Awards";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmAwards");
                //    break;

                //case "frmAwardsPositionMaster.ascx":
                //    HtmlGenericControl li_AssignAwardsPosition = this.li_AssignAwardsPosition as HtmlGenericControl;
                //    if (li_AssignAwardsPosition != null)
                //        this.li_AssignAwardsPosition.Attributes.Add("class", "active");
                //    titela.Text = "Assign Awards Position Master";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmAwardsPositionMaster");
                //    break;

                //case "WinnerList.ascx":
                //    HtmlGenericControl li_AssignWinner = this.li_AssignWinner as HtmlGenericControl;
                //    if (li_AssignWinner != null)
                //        this.li_AssignWinner.Attributes.Add("class", "active");
                //    titela.Text = "Assign Awards";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "WinnerList");
                //    break;

                //case "WinnerPositionMasterForm.ascx":
                //    HtmlGenericControl li_AssignWinnerPosition = this.li_AssignWinnerPosition as HtmlGenericControl;
                //    if (li_AssignWinnerPosition != null)
                //        this.li_AssignWinnerPosition.Attributes.Add("class", "active");
                //    titela.Text = "Assign Awards Position Master";
                //    titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "WinnerPositionMasterForm");
                //    break;

                //case "frmReportTopScorer.ascx":
                //     HtmlGenericControl li_ReportTopScorer = this.li_ReportTopScorer as HtmlGenericControl;
                //     if (li_ReportTopScorer != null)
                //         this.li_ReportTopScorer.Attributes.Add("class", "active");
                //     titela.Text = " Report Top Scorer";
                //     titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmReportTopScorer");
                //     break;

            }

            PortalModuleBase objPortalModuleBase = (PortalModuleBase)LoadControl(m_ModuelControl);
            objPortalModuleBase.ModuleConfiguration = ModuleConfiguration;
            objPortalModuleBase.ID = System.IO.Path.GetFileNameWithoutExtension(m_ModuelControl);
            loadSelectedControl.Controls.Add(objPortalModuleBase);

        }

    }
}