using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThSportServer;
using System.Data;
using DotNetNuke.Common;
using System.Globalization;
using DotNetNuke.Entities.Modules;
using System.Web.UI.HtmlControls;

namespace DotNetNuke.Modules.ThSport
{
    public partial class conCompetitionAllDetail : PortalModuleBase
    {
        clsCompetitionController cmpController = new clsCompetitionController();
        clsSponsorController sponsorController = new clsSponsorController();
        clsMatchResultController resultControl = new clsMatchResultController();

        string m_controlToLoad;
        string Vname;
        string VDescr;
        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\Team-Logos\\";
        public string imhpathDB = "Images\\Team-Logos\\";

        DotNetNuke.Entities.Tabs.TabController tabs = new Entities.Tabs.TabController();

        DotNetNuke.Entities.Tabs.TabInfo tInfo = new Entities.Tabs.TabInfo();

        #region Variables

        int CompetitionID
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["CompetitionID"] != null))
                {
                    int.TryParse(Request.QueryString["CompetitionID"].ToString(), out retVal);
                }
                return retVal;
            }
        }

        int CompTabID
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["CompTabID"] != null))
                {
                    int.TryParse(Request.QueryString["CompTabID"].ToString(), out retVal);
                }
                return retVal;
            }
        }

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {

            string currenttab = PortalSettings.ActiveTab.TabName;
            if (currenttab == "Competition")
            {
                titela.Text = "Competition List";
                titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "CompetitionList");
            }
            else
            {
                titela.Text = "Division List";
                titela.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "CompetitionList");
            }

            titel.Text = "&raquo; Competition All Detail";

            if (!string.IsNullOrEmpty(Request.QueryString["CompetitionID"]))
            {
                bindCompetitionDetails();
                DateViewCompetitionSponsor();
                DateViewVideo();
                DataViewPhoto();
                DataGroupTemas();
                DataSchedules();
                DataResults();
                LoadNews();


                if (CompTabID == 1)
                {
                    competitionTabs7.Visible = true;
                    currentTabIndex.Value = CompTabID.ToString();
                }
                else if (CompTabID == 2)
                {
                    competitionTabs2.Visible = true;
                    currentTabIndex.Value = CompTabID.ToString();
                }

                else if (CompTabID == 3)
                {
                    competitionTabs8.Visible = true;
                    currentTabIndex.Value = CompTabID.ToString();
                }

                else if (CompTabID == 4)
                {
                    competitionTabs4.Visible = true;
                    currentTabIndex.Value = CompTabID.ToString();
                }

                else if (CompTabID == 5)
                {
                    competitionTabs5.Visible = true;
                    currentTabIndex.Value = CompTabID.ToString();
                }

                else if (CompTabID == 6)
                {
                    competitionTabs6.Visible = true;
                    currentTabIndex.Value = CompTabID.ToString();
                }

                bindCompetitionDetails();
                DateViewCompetitionSponsor();
                DateViewVideo();
                DataViewPhoto();
                DataSchedules();
                DataResults();
            }
        }

        #endregion

        #region Methods

        private void bindCompetitionDetails()
        {
            DataTable dt = new DataTable();
            dt = cmpController.GetCompetitionDetailByCompetitionID(CompetitionID);

            litCompetitionHistory.Text = dt.Rows[0]["CompetitionDesc"].ToString();
            lblCompetitionTitle.Text = dt.Rows[0]["CompetitionName"].ToString();

            ImgCompetitionPhoto.ImageUrl = dt.Rows[0]["CompetitionLogoFile"].ToString();
            ImgCompetitionPhoto.AlternateText = dt.Rows[0]["CompetitionName"].ToString();
        }

        private void DateViewCompetitionSponsor()
        {
            DataTable dt = new DataTable();
            dt = sponsorController.GetCompetitionSponsorByCompetitionID(CompetitionID);
            if (dt.Rows.Count > 0)
            {
                //rptImages.DataSource = dt;
                //rptImages.DataBind();
                //divForSponsorGallery.Visible = true;
            }
            else
            {
                //divForSponsorGallery.Visible = false;
            }
        }

        private void DateViewVideo()
        {
            DataTable dt = new DataTable();
            dt = cmpController.GetCompetitionVideoByCompetitionIDAndVideoType(CompetitionID,1);

            if (dt.Rows.Count > 0)
            {
                rptleftVideo.DataSource = dt;
                rptleftVideo.DataBind();
                lblNoVideo.Visible = false;
            }

            DataTable dt1 = new DataTable();
            dt1 = cmpController.GetCompetitionVideoByCompetitionIDAndVideoType(CompetitionID, 2);
            if (dt1.Rows.Count > 0)
            {
                rptleftothervideo.DataSource = dt1;
                rptleftothervideo.DataBind();
                lblNoVideo.Visible = false;
            }

            if (dt.Rows.Count < 0 && dt1.Rows.Count < 0)
            {
                pnlForVideoTable.Visible = false;
                lblNoVideo.Visible = true;
            }
        }

        private void DataViewPhoto()
        {
            DataTable dt = new DataTable();
            dt = cmpController.GetCompetitionPhotoByCompetitionID(CompetitionID);
            if (dt.Rows.Count > 0)
            {
                competitionphoto.DataSource = dt;
                competitionphoto.DataBind();
                lblNoPhotos.Visible = false;
            }
            else
            {
                //ulForGallery.Visible = false;
                lblNoPhotos.Visible = true;
            }
        }

        private void DataGroupTemas()
        {
            if (plcPoints != null)
            {

                conStandingDisplay objPortalModuleBase = (conStandingDisplay)LoadControl("conStandingDisplay.ascx");
                objPortalModuleBase.ModuleConfiguration = ModuleConfiguration;
                objPortalModuleBase.ID = System.IO.Path.GetFileNameWithoutExtension("conStandingDisplay.ascx");
                objPortalModuleBase.CompetitionId = CompetitionID;
                plcPoints.Controls.Add(objPortalModuleBase);
            }


        }

        private void DataSchedules()
        {
            DataTable dt = new DataTable();
            dt = cmpController.GetCompetitionFixturesByCompetitionID(CompetitionID);
            DataView dataview = new DataView();
            dataview = dt.AsDataView();
            if (dt.Rows.Count > 0)
            {
                dataview = dt.AsDataView();
                dataview.RowFilter = "MatchTypeId = 2";
                dataview.Sort = "StartDate ASC";

                if (dataview.Count > 0)
                {
                    rptFinal.Visible = true;
                }
                rptCompetitionSchedulesResultsFinal.DataSource = dataview;
                rptCompetitionSchedulesResultsFinal.DataBind();
                lblNoResultsFinal.Visible = false;
            }
            else
            {

                lblNoResultsFinal.Visible = true;
            }
        
            if (dt.Rows.Count > 0)
            {
                dataview = dt.AsDataView();

                dataview.RowFilter = "MatchTypeId = 4";
                dataview.Sort = "StartDate ASC";
                if (dataview.Count > 0)
                {
                    rptSemiFinal.Visible = true;
                }
                rptCompetitionSchedulesResultsSemiFinal.DataSource = dataview;
                rptCompetitionSchedulesResultsSemiFinal.DataBind();
                lblNoResultsSemiFinal.Visible = false;
            }
            else
            {
                lblNoResultsSemiFinal.Visible = true;
            }

            if (dt.Rows.Count > 0)
            {
                dataview = dt.AsDataView();
                dataview.RowFilter = "MatchTypeId = 8";
                dataview.Sort = "StartDate ASC";

                if (dataview.Count > 0)
                {
                    rptQuater.Visible = true;
                }
                rptCompetitionSchedulesResultsQuater.DataSource = dataview;
                rptCompetitionSchedulesResultsQuater.DataBind();
                lblNoResultsQuater.Visible = false;
            }
            else
            {
                lblNoResultsQuater.Visible = true;
            }

            #region For Quater Secondary Round

            if (dt.Rows.Count > 0)
            {
                dataview = dt.AsDataView();
                dataview.RowFilter = "MatchTypeId = 81";
                dataview.Sort = "StartDate ASC";

                if (dataview.Count > 0)
                {
                    rptrQuaterSecondary.Visible = true;
                }
                rptrCompetitionQuaterSecondary.DataSource = dataview;
                rptrCompetitionQuaterSecondary.DataBind();
                lblNoResultsForSecondaryQuater.Visible = false;
            }
            else
            {
                lblNoResultsForSecondaryQuater.Visible = true;
            }

            #endregion For Quater Secondary Round

            if (dt.Rows.Count > 0)
            {
                dataview = dt.AsDataView();
                dataview.RowFilter = "MatchTypeId = 16";
                dataview.Sort = "StartDate ASC";
                if (dataview.Count > 0)
                {
                    rptRoundOf16.Visible = true;
                }
                rptCompetitionRoundOf16.DataSource = dataview;
                rptCompetitionRoundOf16.DataBind();
                lblNoResultsRoundOf16.Visible = false;
            }
            else
            {
                lblNoResultsRoundOf16.Visible = true;
            }

            if (dt.Rows.Count > 0)
            {
                dataview = dt.AsDataView();
                dataview.RowFilter = "MatchTypeId = 0";
                dataview.Sort = "StartDate ASC";
                if (dataview.Count > 0)
                {
                    rptScheduleAndResults.Visible = true;
                }
                rptCompetitionSchedulesResults.DataSource = dataview;
                rptCompetitionSchedulesResults.DataBind();
                lblNoResults.Visible = false;
            }

            else
            {
                lblNoResults.Visible = true;
            }
        }

        private void DataResults()
        {
            DataTable dt = new DataTable();
            dt = cmpController.GetCompetitionResultsByCompetitionID(CompetitionID);
            DataView dataview = new DataView();
            dataview = dt.AsDataView();
            if (dt.Rows.Count > 0)
            {
                dataview = dt.AsDataView();
                dataview.RowFilter = "MatchTypeId = 2";
                dataview.Sort = "StartDate DESC";

                if (dataview.Count > 0)
                {
                    final_Results.Visible = true;
                }
                rptCompetitionResultsFinal.DataSource = dataview;
                rptCompetitionResultsFinal.DataBind();
                final_ResultsMsg.Visible = false;
            }
            else
            {

                final_ResultsMsg.Visible = true;
            }
            
            if (dt.Rows.Count > 0)
            {
                dataview = dt.AsDataView();

                dataview.RowFilter = "MatchTypeId = 4";
                dataview.Sort = "StartDate DESC";
                if (dataview.Count > 0)
                {
                    semifinal_Results.Visible = true;
                }
                rptCompetitionResultsSemiFinal.DataSource = dataview;
                rptCompetitionResultsSemiFinal.DataBind();
                semifinal_ResultsMsg.Visible = false;
            }
            else
            {
                semifinal_ResultsMsg.Visible = true;
            }

            
            if (dt.Rows.Count > 0)
            {
                dataview = dt.AsDataView();
                dataview.RowFilter = "MatchTypeId = 8";
                dataview.Sort = "StartDate DESC";

                if (dataview.Count > 0)
                {
                    roundof8_Results.Visible = true;
                }
                rptCompetitionResultsRoundOf8.DataSource = dataview;
                rptCompetitionResultsRoundOf8.DataBind();
                roundof8_ResultsMsg.Visible = false;
            }
            else
            {
                roundof8_ResultsMsg.Visible = true;
            }

            #region For Quater Secondary Round

            if (dt.Rows.Count > 0)
            {
                dataview = dt.AsDataView();
                dataview.RowFilter = "MatchTypeId = 81";
                dataview.Sort = "StartDate DESC";

                if (dataview.Count > 0)
                {
                    RoundOf8Secondary_Results.Visible = true;
                }
                rptCompetitionResultsRoundOf8Secondary.DataSource = dataview;
                rptCompetitionResultsRoundOf8Secondary.DataBind();
                RoundOf8Secondary_Results_Msg.Visible = false;
            }
            else
            {
                RoundOf8Secondary_Results_Msg.Visible = true;
            }

            #endregion For Quater Secondary Round


            if (dt.Rows.Count > 0)
            {
                dataview = dt.AsDataView();
                dataview.RowFilter = "MatchTypeId = 16";
                dataview.Sort = "StartDate DESC";
                if (dataview.Count > 0)
                {
                    RoundOf16Secondary_Results.Visible = true;
                }
                rptCompetitionResultsRoundOf16.DataSource = dataview;
                rptCompetitionResultsRoundOf16.DataBind();
                RoundOf16Secondary_ResultsMsg.Visible = false;
            }
            else
            {
                RoundOf16Secondary_ResultsMsg.Visible = true;
            }

            if (dt.Rows.Count > 0)
            {
                dataview = dt.AsDataView();
                dataview.RowFilter = "MatchTypeId = 0";
                dataview.Sort = "StartDate DESC";
                if (dataview.Count > 0)
                {
                    GroupStage_results.Visible = true;
                }
                rptCompetitionResultsGroupStage.DataSource = dataview;
                rptCompetitionResultsGroupStage.DataBind();
                GroupStage_resultsMsg.Visible = false;
            }

            else
            {
                GroupStage_resultsMsg.Visible = true;
            }
        }

        public string GetFormatedDate(object newsDate)
        {
            DateTime retVal = DateTime.Now;
            DateTime.TryParse(newsDate.ToString(), out retVal);
            string dateFormat = "<time datetime=\"" + retVal + "\" class=\"icon\">" +
                                "   <em>" + retVal.Year + "</em>" +
                                "    <strong>" + retVal.ToString("MMM") + "</strong>" +
                                "    <span>" + retVal.Day + "</span>" +
                                "</time>";
            return dateFormat;
        }

        private void LoadNews()
        {
            lblNoNews.Visible = false;
            rptrNews.Visible = false;
            
            DataTable dt = cmpController.GetNewsByCompetitionId(CompetitionID, PortalId);
            if (dt != null && dt.Rows.Count > 0)
            {
                rptrNews.DataSource = dt;
                rptrNews.DataBind();
                rptrNews.Visible = true;
            }
            else
            {
                lblNoNews.Visible = true;
            }
            
        }

        #endregion

        #region Schedules & Results Repeater Events

        protected void rptCompetitionSchedulesResults_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ItemDataBoundDisplay(sender, e);
        }

        protected void rptCompetitionSchedulesResultsSemiFinal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ItemDataBoundDisplay(sender, e);
        }

        protected void rptCompetitionSchedulesResultsQuater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ItemDataBoundDisplay(sender, e);
        }

        protected void rptrCompetitionQuaterSecondary_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ItemDataBoundDisplay(sender, e);
        }

        protected void rptCompetitionSchedulesResultsFinal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ItemDataBoundDisplay(sender, e);
        }

        protected void rptCompetitionRoundOf16_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ItemDataBoundDisplay(sender, e);
        }

        private void ItemDataBoundDisplay(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Panel SchedulesResultsDatePanel = e.Item.FindControl("SchedulesResultsDatePanel") as Panel;
                HiddenField hdnStartDate = e.Item.FindControl("hdnStartDate") as HiddenField;
                HiddenField hdnMatchID = e.Item.FindControl("hdnMatchID") as HiddenField;
                Literal litSchedulesResultsDate = e.Item.FindControl("litSchedulesResultsDate") as Literal;
                Literal litStartDate = e.Item.FindControl("litStartDate") as Literal;
                HyperLink hlnkTeamAFixtures = e.Item.FindControl("hlnkTeamAFixtures") as HyperLink;
                HiddenField hdnTeamAID = e.Item.FindControl("hdnTeamAID") as HiddenField;
                HyperLink hlnkTeamBFixtures = e.Item.FindControl("hlnkTeamBFixtures") as HyperLink;
                HiddenField hdnTeamBID = e.Item.FindControl("hdnTeamBID") as HiddenField;
                HiddenField hdnMatchResultId = e.Item.FindControl("hdnMatchResultId") as HiddenField;
                HiddenField hidTeamAScoreFixturesCup = e.Item.FindControl("hidTeamAScoreFixturesCup") as HiddenField;
                HiddenField hidTeamBScoreFixturesCup = e.Item.FindControl("hidTeamBScoreFixturesCup") as HiddenField;
                HiddenField HiddenField1 = e.Item.FindControl("HiddenField1") as HiddenField;
                HiddenField HiddenField2 = e.Item.FindControl("HiddenField2") as HiddenField;
                HiddenField hdnNoshow = e.Item.FindControl("hdnNoshow") as HiddenField;
                HiddenField WinningTeam = e.Item.FindControl("hdnwinningteam") as HiddenField;
                HiddenField LosingTeam = e.Item.FindControl("hdnloosingteam") as HiddenField;
                Literal ltrlNoShowTeam = e.Item.FindControl("ltrlNoShowTeam") as Literal;
                Literal ltrnoshow = e.Item.FindControl("ltrnoshow") as Literal;
                Literal ltrnoshowB = e.Item.FindControl("ltrlnoshowB") as Literal;
                Panel noShowRegion = e.Item.FindControl("noShowRegion") as Panel;
                Panel noShowRegionB = e.Item.FindControl("noShowRegionB") as Panel;
                Panel penaltyRegion = e.Item.FindControl("penaltyRegion") as Panel;
                Panel pnlResult = e.Item.FindControl("pnlResult") as Panel;
                Panel pnlFixture = e.Item.FindControl("pnlFixture") as Panel;
                Literal daysToKick = e.Item.FindControl("daysToKick") as Literal;

                Label ltrpenalty = e.Item.FindControl("ltrpenalty") as Label;
                Label ltrpenaltyText = e.Item.FindControl("ltrpenaltyText") as Label;


                Label ltrpenaltyB = e.Item.FindControl("ltrpenaltyB") as Label;
                Label ltrpenaltyTextB = e.Item.FindControl("ltrpenaltyTextB") as Label;

                //new start
                Label litA = e.Item.FindControl("litA") as Label;
                Label litB = e.Item.FindControl("litB") as Label;
                Label literalTeamAName = e.Item.FindControl("literalTeamAName") as Label;
                Label literalTeamBName = e.Item.FindControl("literalTeamBName") as Label;
                //new end

                Image teamALogo = e.Item.FindControl("TeamALogo") as Image;
                Image teamBLogo = e.Item.FindControl("TeamBLogo") as Image;


                if (teamALogo != null)
                {
                    if (teamALogo.ImageUrl == String.Empty || (!(teamALogo.ImageUrl.Contains('.'))))
                    {
                        teamALogo.ImageUrl = "~/DesktopModules/ThSport/Images/Team-Logos/no-available-image.png";
                    }
                }

                if (teamBLogo != null)
                {
                    if (teamBLogo.ImageUrl == String.Empty || (!(teamBLogo.ImageUrl.Contains('.'))))
                    {
                        teamBLogo.ImageUrl = "~/DesktopModules/ThSport/Images/Team-Logos/no-available-image.png";
                    }
                }


                if (hlnkTeamAFixtures != null && hdnTeamAID != null)
                    hlnkTeamAFixtures.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamAllDetail", "TeamID=" + hdnTeamAID.Value.ToString());

                if (hlnkTeamBFixtures != null && hdnTeamBID != null)
                    hlnkTeamBFixtures.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamAllDetail", "TeamID=" + hdnTeamBID.Value.ToString());

                if (hdnStartDate != null)
                {
                    DateTime date;
                    DateTime.TryParse(hdnStartDate.Value, out date);
                    if (date != null)
                    {
                        litSchedulesResultsDate.Text = date.ToString("dddd dd MMMM,yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        litStartDate.Text = "<time datetime=\"" + date.ToString("dddd dd MMMM,yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-US")) + "\">" + string.Format("{0:HH:mm}", date) + "</time>";
                    }
                }

                int matchId = 0;
                if (hdnMatchID != null && hdnMatchID.Value != null)
                {
                    int.TryParse(hdnMatchID.Value, out matchId);
                }

                if (matchId > 0)
                {

                }

                int matchResultId = 0;
                if (hdnMatchResultId != null && hdnMatchResultId.Value != null)
                {
                    int.TryParse(hdnMatchResultId.Value, out matchResultId);
                }

                if (matchResultId > 0)
                {
                    //HyperLink hprLinkForGame = e.Item.FindControl("hprLinkForGame") as HyperLink;
                    //tInfo = tabs.GetTabByName("GameDetail", PortalId);
                    //hprLinkForGame.NavigateUrl = Globals.NavigateURL(tInfo.TabID, "", "mctl=" + "Game", "GameID=" + matchResultId);

                    // For matches without no show , mark winner team with "W"
                    int isNoshow = 0;
                    if (hdnNoshow.Value == "1")
                    {
                        isNoshow = 1;
                        noShowRegion.Visible = true;
                        noShowRegionB.Visible = true;
                    }

                    if (isNoshow == 0)
                    {
                        if (Convert.ToInt32(hidTeamAScoreFixturesCup.Value) > Convert.ToInt32(hidTeamBScoreFixturesCup.Value))
                        {
                            literalTeamAName.Text += (" (W) ");
                        }
                        else if (Convert.ToInt32(hidTeamAScoreFixturesCup.Value) < Convert.ToInt32(hidTeamBScoreFixturesCup.Value))
                        {
                            literalTeamBName.Text += (" (W) ");
                        }
                    }

                    //if (Convert.ToInt32(hidTeamAScoreFixturesCup.Value) == Convert.ToInt32(hidTeamBScoreFixturesCup.Value))
                    //{

                    penaltyRegion.Visible = false;

                    ltrpenalty.Visible = false;
                    ltrpenaltyText.Visible = false;

                    ltrpenaltyB.Visible = false;
                    ltrpenaltyTextB.Visible = false;

                    int teamApanlty = 0, teamBpanlty = 0;
                    int.TryParse(HiddenField1.Value, out teamApanlty);
                    int.TryParse(HiddenField2.Value, out teamBpanlty);

                    if (teamApanlty != 0 || teamBpanlty != 0)
                    {
                        if (teamApanlty > teamBpanlty)
                        {
                            //left win
                            literalTeamAName.Text += (" (W) ");

                        }

                        else if (teamBpanlty > teamApanlty)
                        {
                            //right win
                            literalTeamBName.Text += (" (W) ");
                        }
                        //PENALTYDIV.Visible = true;

                        penaltyRegion.Visible = true;
                        ltrpenalty.Visible = true;
                        ltrpenaltyText.Visible = true;

                        ltrpenaltyB.Visible = true;
                        ltrpenaltyTextB.Visible = true;
                    }
                    //}
                    pnlResult.Visible = true;
                    pnlFixture.Visible = false;
                }
                else
                {
                    int matchdays = 0;
                    if (hdnStartDate != null)
                    {
                        DateTime mdate = DateTime.MinValue;
                        DateTime.TryParse(hdnStartDate.Value, out mdate);
                        matchdays = (int)(mdate.Date - DateTime.Now.Date).TotalDays;
                    }
                    if (daysToKick != null)
                    {
                        if (matchdays == 0)
                            daysToKick.Text = "Kicks off Today";
                        else if (matchdays < 0)
                            daysToKick.Text = "Result Not Available";
                        else
                            daysToKick.Text = matchdays + "  Days till Kick off";
                    }

                    pnlResult.Visible = false;
                    pnlFixture.Visible = true;



                }

                if (noShowRegion != null)
                    noShowRegion.Visible = false;

                if (noShowRegionB != null)
                    noShowRegionB.Visible = false;
                if (!string.IsNullOrEmpty(WinningTeam.Value))
                {
                    int isNoshow = 0, TeamAScoreGame = 0, TeamBScoreGame = 0;//WinningTeamID = 0, LosingTeamID = 0;
                    if (hdnNoshow.Value == "1")
                    {
                        isNoshow = 1;
                        noShowRegion.Visible = true;
                        noShowRegionB.Visible = true;
                    }
                    int.TryParse(hidTeamAScoreFixturesCup.Value, out TeamAScoreGame);
                    int.TryParse(hidTeamBScoreFixturesCup.Value, out TeamBScoreGame);

                    if (isNoshow == 1)
                    {

                        //if no show, hide penalty details
                        noShowRegion.Visible = true;
                        noShowRegionB.Visible = true;
                        //if no show, hide penalty details
                        penaltyRegion.Visible = false;

                        ltrpenalty.Visible = false;
                        ltrpenaltyText.Visible = false;

                        ltrpenaltyB.Visible = false;
                        ltrpenaltyTextB.Visible = false;

                        if (Convert.ToInt32(hidTeamAScoreFixturesCup.Value) > Convert.ToInt32(hidTeamBScoreFixturesCup.Value))
                        {
                            literalTeamAName.Text += (" (W) ");
                        }
                        else if (Convert.ToInt32(hidTeamAScoreFixturesCup.Value) < Convert.ToInt32(hidTeamBScoreFixturesCup.Value))
                        {
                            literalTeamBName.Text += (" (W) ");
                        }

                        if (TeamAScoreGame > TeamBScoreGame)
                        {
                            //left win
                            if (ltrnoshowB != null)
                                ltrnoshowB.Text = "NO SHOW";


                        }
                        else if (TeamAScoreGame < TeamBScoreGame)
                        {
                            //right win
                            if (ltrnoshow != null)
                                ltrnoshow.Text = "NO SHOW";

                        }

                        if (noShowRegion != null)
                            noShowRegion.Visible = true;

                        if (noShowRegionB != null)
                            noShowRegionB.Visible = true;
                    }
                }
                if (SchedulesResultsDatePanel != null)
                {
                    SchedulesResultsDatePanel.Visible = false;
                    if (e.Item.ItemIndex == 0)
                    {
                        SchedulesResultsDatePanel.Visible = true;
                    }
                    else
                    {
                        DateTime date, prevDate;
                        DateTime.TryParse(hdnStartDate.Value, out date);
                        //initial value
                        DateTime.TryParse(hdnStartDate.Value, out prevDate);
                        Repeater rptrToConsider = sender as Repeater;
                        RepeaterItem prevItem = null;
                        if (rptrToConsider != null)
                        {
                            prevItem = rptrToConsider.Items[e.Item.ItemIndex - 1];
                            if (prevItem != null)
                            {
                                HiddenField hdnPrevStartDate = prevItem.FindControl("hdnStartDate") as HiddenField;
                                DateTime.TryParse(hdnPrevStartDate.Value, out prevDate);
                            }
                        }

                        if (date != null && prevDate != null)
                        {
                            if (!(date.Day == prevDate.Day && date.Month == prevDate.Month && date.Year == prevDate.Year))
                            {
                                SchedulesResultsDatePanel.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Results Repeater Events

        protected void rptCompetitionResultsFinal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ItemDataBoundDisplay(sender, e);

        }

        protected void rptCompetitionResultsSemiFinal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ItemDataBoundDisplay(sender, e);

        }

        protected void rptCompetitionResultsRoundOf8_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ItemDataBoundDisplay(sender, e);


        }

        protected void rptCompetitionResultsRoundOf8Secondary_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ItemDataBoundDisplay(sender, e);


        }

        protected void rptCompetitionResultsRoundOf16_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ItemDataBoundDisplay(sender, e);

        }

        protected void rptCompetitionResultsGroupStage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ItemDataBoundDisplay(sender, e);


        }

        #endregion

        #region Other Repeater Events

        protected void rptTeamsView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Panel pnlGroup = e.Item.FindControl("GroupPanel") as Panel;
                if (pnlGroup != null)
                {
                    if (e.Item.ItemIndex == 0)
                    {
                        pnlGroup.Visible = true;
                    }
                    else if (e.Item.ItemIndex >= 1)
                    {
                        Repeater srcRepeater = sender as Repeater;
                        RepeaterItem previousItem = (RepeaterItem)srcRepeater.Items[e.Item.ItemIndex - 1];

                        Literal previousGroup = null;
                        if (previousItem != null)
                        {
                            previousGroup = (Literal)previousItem.FindControl("lblSelectGroup");
                        }
                        Literal currentGroup = null;
                        if (e.Item != null)
                        {
                            currentGroup = (Literal)e.Item.FindControl("lblSelectGroup");
                        }


                        if (previousGroup != null && currentGroup != null && previousGroup.Text != currentGroup.Text)
                        {
                            pnlGroup.Visible = true;
                        }
                        else
                        {
                            pnlGroup.Visible = false;
                        }
                    }
                }
            }
        }

        protected void rptTeamsView_OnIteamCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("teamName"))
            {
                Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamAllDetail", "TeamID=" + e.CommandArgument));
            }
        }

        protected void rptrNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Image img = e.Item.FindControl("ltrlImg") as Image;
                if (img != null)
                {
                    img.Visible = true;

                    if (img.ImageUrl == null)
                    {
                        img.Visible = false;
                    }
                }
            }
        }

        protected void rptImages_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image imgSponsorLogo = e.Item.FindControl("imgSponsorLogo") as Image;

                if (!imgSponsorLogo.ImageUrl.Contains('.'))
                {
                    imgSponsorLogo.Visible = false;
                }
                else
                {
                    imgSponsorLogo.Visible = true;
                }
            }
        }

        protected void rptrNews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("btnNewsDisplayReadMore"))
            {
                int editid = 0;
                int.TryParse(e.CommandArgument.ToString(), out editid);

                Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "conNewsDetail", "NewsID=" + editid));

            }
        }

        #endregion

    }
}