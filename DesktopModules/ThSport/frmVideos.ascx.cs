using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThSportServer;
using System.Data;
using System.IO;
using System.Globalization;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using System.Web.UI.HtmlControls;

namespace DotNetNuke.Modules.ThSport
{
    public partial class frmVideos : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();
        public string VideoUploadFolder = "DesktopModules\\ThSport\\Videos\\Video\\";
        public string videohpathDB = "Videos\\Video\\";

        clsVideos cs = new clsVideos();
        clsVideosController csc = new clsVideosController();

        string m_controlToLoad;
        string VName;
        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;

        Boolean FileOKForUpdate = false;
        Boolean FileSavedForUpdate = false;

        #region Page events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        #endregion

        #region Grid Editing Related Events

        protected void BindGrid()
        {
            FillGridView();
        }

        #endregion

        private void FillGridView()
        {
            DataTable dt = new DataTable();

            //if (currentUser.IsSuperUser || currentUser.IsInRole("Club Admin"))
            //{
                dt = csc.GetDataVideo();
            //}

            if (dt.Rows.Count > 0)
            {
                gvVideo.DataSource = dt;
                gvVideo.DataBind();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            funClearData();
        }

        private void funClearData()
        {
            txtVideoTitle.Text = "";
            txtVideoDesc.Text = "";
            txtVideoDate.Text = "";
            txtVideoPath.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
        }

        protected void btnSaveVideo_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;
            Boolean FileOKVidoe = false;
            Boolean FileSavedVideo = false;

            if (ddlSports.SelectedValue == "")
            {
                cs.SportId = 0;
            }
            else
            {
                cs.SportId = Convert.ToInt32(ddlSports.SelectedValue);
            }

            if (ddlCountry.SelectedValue == "")
            {
                cs.CountryId = 0;
            }
            else
            {
                cs.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
            }

            if (ddlEvent.SelectedValue == "")
            {
                cs.EventId = 0;
            }
            else
            {
                cs.EventId = Convert.ToInt32(ddlEvent.SelectedValue);
            }

            if (ddlSeason.SelectedValue == "")
            {
                cs.SeasonId = 0;
            }
            else
            {
                cs.SeasonId = Convert.ToInt32(ddlSeason.SelectedValue);
            }

            if (ddlCompetition.SelectedValue == "")
            {
                cs.CompetitionId = 0;
            }
            else
            {
                cs.CompetitionId = Convert.ToInt32(ddlCompetition.SelectedValue);
            }

            if (ddlClub.SelectedValue == "")
            {
                cs.ClubId = 0;
            }
            else
            {
                cs.ClubId = Convert.ToInt32(ddlClub.SelectedValue);
            }

            if (ddlClubOwner.SelectedValue == "")
            {
                cs.ClubOwnersId = 0;
            }
            else
            {
                cs.ClubOwnersId = Convert.ToInt32(ddlClubOwner.SelectedValue);
            }

            if (ddlClubMember.SelectedValue == "")
            {
                cs.ClubMemberId = 0;
            }
            else
            {
                cs.ClubMemberId = Convert.ToInt32(ddlClubMember.SelectedValue);
            }

            if (ddlTeam.SelectedValue == "")
            {
                cs.TeamId = 0;
            }
            else
            {
                cs.TeamId = Convert.ToInt32(ddlTeam.SelectedValue);
            }

            if (ddlTeamMember.SelectedValue == "")
            {
                cs.TeamMemberId = 0;
            }
            else
            {
                cs.TeamMemberId = Convert.ToInt32(ddlTeamMember.SelectedValue);
            }

            if (ddlPlayer.SelectedValue == "")
            {
                cs.PlayerId = 0;
            }
            else
            {
                cs.PlayerId = Convert.ToInt32(ddlPlayer.SelectedValue);
            }

            if (ddlSponsor.SelectedValue == "")
            {
                cs.SponsorId = 0;
            }
            else
            {
                cs.SponsorId = Convert.ToInt32(ddlSponsor.SelectedValue);
            }

            cs.VideoTitle = txtVideoTitle.Text.Trim();
            cs.VideoDesc = txtVideoDesc.Text.Trim();
            cs.VideoDate = txtVideoDate.Text.Trim();

            // string youtubelink = "http://www.youtube.com/embed/";
            string videopath = txtVideoPath.Text;
            //youtubelink +
            cs.VideoYouTubeFile = videopath;

            if (VideoLogoFile.PostedFile != null)
            {
                cs.VideoOtherFile = videohpathDB + VideoLogoFile.PostedFile.FileName.Replace(" ", "");
                String FileExtension = Path.GetExtension(VideoLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
                String[] allowedExtensions = { ".flv", ".webm", ".mkv", ".vob", ".ogv", ".ogg", ".avi", ".mov", ".wmv", ".rm", ".mp4", ".m4p", ".m4v", ".mpg", ".mp2", ".mpeg", ".mpe", ".mpv", ".m2v", ".m4v", ".svi", ".3gp", ".3g2", ".nsv", ".asf", ".asx", ".srt", ".swf" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (FileExtension == allowedExtensions[i])
                    {
                        FileOKVidoe = true;
                        break;
                    }
                }
            }

            if (FileOKVidoe)
            {
                try
                {
                    VideoLogoFile.PostedFile.SaveAs(physicalpath + VideoUploadFolder + VideoLogoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSavedVideo = true;
                }
                catch (Exception ex)
                {
                    FileSavedVideo = false;
                }
            }

            if (ddlvideotype.SelectedValue == "YouTube")
            {
                cs.VideoType = 1;
            }
            else
            {
                cs.VideoType = 2;
            }

            if (ChkIsActive.Checked == true)
            {
                cs.ActiveFlagId = 1;
            }
            else
            {
                cs.ActiveFlagId = 0;
            }

            if (ChkIsShow.Checked == true)
            {
                cs.ShowFlagId = 1;
            }
            else
            {
                cs.ShowFlagId = 0;
            }

            cs.VideoLevelId = ddlVideoPriority.SelectedValue;

            cs.PortalID = PortalId;
            cs.CreatedById = currentUser.Username;
            cs.ModifiedById = currentUser.Username;

            int spid = csc.InsertVideo(cs);

            DataTable dt = new DataTable();
            dt = csc.GetLatestVideoID();
            if (dt.Rows.Count > 0)
            {
                cs.VideoId = Convert.ToInt32(dt.Rows[0]["VideoId"].ToString());
                csc.InsertVideoLinks(cs);
            }

            pnlEntryVideo.Visible = false;
            PnlGridVideo.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void btnAddVideo_Click(object sender, EventArgs e)
        {
            funClearData();
            pnlEntryVideo.Visible = true;
            PnlGridVideo.Visible = false;
            btnSaveVideo.Visible = true;
            btnUpdateVideo.Visible = false;
            FillSport();
            FillCountry();
            FillEvent();
            FillSeason();
            FillSponsor();
            FillEvent();
            FillTeam();
            FillClub();
            FillCompetition();
            FillClubOwner();
            FillClubMember();
            FillTeamMember();
            FillPlayer();
            ddlvideotype.SelectedValue = "YouTube";
            divvideopath.Visible = true;
            divOtherVideoPath.Visible = false;
        }

        private void FillSponsor()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.GetSponsorIDAndSponsorName();
            if (dt.Rows.Count > 0)
            {
                ddlSponsor.DataSource = dt;
                ddlSponsor.DataTextField = "SponsorName";
                ddlSponsor.DataValueField = "SponsorId";
                ddlSponsor.DataBind();
                ddlSponsor.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        protected void btnCloseVideo_Click(object sender, EventArgs e)
        {
            pnlEntryVideo.Visible = false;
            PnlGridVideo.Visible = true;
            FillGridView();
        }

        protected void btnUpdateVideo_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully()", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            cs.VideoId = Convert.ToInt32(hidRegID.Value);

            if (ddlSports.SelectedValue == "")
            {
                cs.SportId = 0;
            }
            else
            {
                cs.SportId = Convert.ToInt32(ddlSports.SelectedValue);
            }

            if (ddlCountry.SelectedValue == "")
            {
                cs.CountryId = 0;
            }
            else
            {
                cs.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
            }

            if (ddlEvent.SelectedValue == "")
            {
                cs.EventId = 0;
            }
            else
            {
                cs.EventId = Convert.ToInt32(ddlEvent.SelectedValue);
            }

            if (ddlSeason.SelectedValue == "")
            {
                cs.SeasonId = 0;
            }
            else
            {
                cs.SeasonId = Convert.ToInt32(ddlSeason.SelectedValue);
            }

            if (ddlCompetition.SelectedValue == "")
            {
                cs.CompetitionId = 0;
            }
            else
            {
                cs.CompetitionId = Convert.ToInt32(ddlCompetition.SelectedValue);
            }

            if (ddlClub.SelectedValue == "")
            {
                cs.ClubId = 0;
            }
            else
            {
                cs.ClubId = Convert.ToInt32(ddlClub.SelectedValue);
            }

            if (ddlClubOwner.SelectedValue == "")
            {
                cs.ClubOwnersId = 0;
            }
            else
            {
                cs.ClubOwnersId = Convert.ToInt32(ddlClubOwner.SelectedValue);
            }

            if (ddlClubMember.SelectedValue == "")
            {
                cs.ClubMemberId = 0;
            }
            else
            {
                cs.ClubMemberId = Convert.ToInt32(ddlClubMember.SelectedValue);
            }

            if (ddlTeam.SelectedValue == "")
            {
                cs.TeamId = 0;
            }
            else
            {
                cs.TeamId = Convert.ToInt32(ddlTeam.SelectedValue);
            }

            if (ddlTeamMember.SelectedValue == "")
            {
                cs.TeamMemberId = 0;
            }
            else
            {
                cs.TeamMemberId = Convert.ToInt32(ddlTeamMember.SelectedValue);
            }

            if (ddlPlayer.SelectedValue == "")
            {
                cs.PlayerId = 0;
            }
            else
            {
                cs.PlayerId = Convert.ToInt32(ddlPlayer.SelectedValue);
            }

            if (ddlSponsor.SelectedValue == "")
            {
                cs.SponsorId = 0;
            }
            else
            {
                cs.SponsorId = Convert.ToInt32(ddlSponsor.SelectedValue);
            }

            cs.VideoTitle = txtVideoTitle.Text.Trim();
            cs.VideoDesc = txtVideoDesc.Text.Trim();
            cs.VideoDate = txtVideoDate.Text.Trim();

            if (ddlvideotype.SelectedValue == "YouTube")
            {
                cs.VideoType = 1;
                // string youtubelink = "http://www.youtube.com/embed/";
                string videopath = txtVideoPath.Text;
                //youtubelink 
                cs.VideoYouTubeFile = videopath;

            }
            else
            {
                cs.VideoType = 2;

                if (VideoLogoFile.PostedFile.FileName == "")
                {
                    DataTable dt = new DataTable();
                    cs.VideoId = Convert.ToInt32(hidRegID.Value);

                    dt = csc.GetOtherVideoPathByVideoID(cs);
                    string ufname = dt.Rows[0]["VideoOtherFile"].ToString().Replace(" ", "");
                    cs.VideoOtherFile = ufname;
                }
                else
                {

                    if (VideoLogoFile.PostedFile != null)
                    {
                        cs.VideoOtherFile = videohpathDB + VideoLogoFile.PostedFile.FileName.Replace(" ", "");
                        String FileExtension = Path.GetExtension(VideoLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
                        String[] allowedExtensions = { ".flv", ".webm", ".mkv", ".vob", ".ogv", ".ogg", ".avi", ".mov", ".wmv", ".rm", ".mp4", ".m4p", ".m4v", ".mpg", ".mp2", ".mpeg", ".mpe", ".mpv", ".m2v", ".m4v", ".svi", ".3gp", ".3g2", ".nsv", ".asf", ".asx", ".srt", ".swf" };
                        for (int i = 0; i < allowedExtensions.Length; i++)
                        {
                            if (FileExtension == allowedExtensions[i])
                            {
                                FileOK = true;
                                break;
                            }
                        }
                    }

                    if (FileOK)
                    {
                        try
                        {
                            VideoLogoFile.PostedFile.SaveAs(physicalpath + VideoUploadFolder + VideoLogoFile.PostedFile.FileName.Replace(" ", ""));
                            FileSaved = true;
                        }
                        catch (Exception ex)
                        {

                            FileSaved = false;
                        }
                    }
                }

            }

            if (ChkIsActive.Checked == true)
            {
                cs.ActiveFlagId = 1;
            }
            else
            {
                cs.ActiveFlagId = 0;
            }

            if (ChkIsShow.Checked == true)
            {
                cs.ShowFlagId = 1;
            }
            else
            {
                cs.ShowFlagId = 0;
            }

            cs.VideoLevelId = ddlVideoPriority.SelectedValue;

            cs.PortalID = PortalId;
            cs.ModifiedById = currentUser.Username;

            int eventid = csc.UpdateVideo(cs);

            int evid = csc.UpdateVideoLinks(cs);

            pnlEntryVideo.Visible = false;
            PnlGridVideo.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void gvVideo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVideo.PageIndex = e.NewPageIndex;
            FillGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionVideoID")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                funClearData();
                FillSport();
                FillCountry();
                FillEvent();
                FillSeason();
                FillCompetition();
                FillClub();
                FillClubOwner();
                FillClubMember();
                FillTeam();
                FillTeamMember();
                FillPlayer();
                FillSponsor();

                int VideoID = 0;
                int.TryParse(str, out VideoID);

                LinkButton btn = sender as LinkButton;

                clsVideos cs = new clsVideos();
                clsVideosController csc = new clsVideosController();

                DataTable dt = new DataTable();

                dt = csc.GetVideoDataByVideoID(VideoID);

                if (dt.Rows.Count > 0)
                {
                    hidRegID.Value = dt.Rows[0]["VideoId"].ToString();

                    ddlSports.SelectedValue = dt.Rows[0]["SportId"].ToString();
                    ddlSeason.SelectedValue = dt.Rows[0]["SeasonId"].ToString();
                    ddlCompetition.SelectedValue = dt.Rows[0]["CompetitionId"].ToString();
                    ddlClub.SelectedValue = dt.Rows[0]["ClubId"].ToString();
                    ddlClubOwner.SelectedValue = dt.Rows[0]["ClubOwnersId"].ToString();
                    ddlClubMember.SelectedValue = dt.Rows[0]["ClubMemberId"].ToString();
                    ddlTeam.SelectedValue = dt.Rows[0]["TeamId"].ToString();
                    ddlTeamMember.SelectedValue = dt.Rows[0]["TeamMemberId"].ToString();
                    ddlPlayer.SelectedValue = dt.Rows[0]["PlayerId"].ToString();
                    ddlEvent.SelectedValue = dt.Rows[0]["EventId"].ToString();
                    ddlSponsor.SelectedValue = dt.Rows[0]["SponsorId"].ToString();
                    ddlCountry.SelectedValue = dt.Rows[0]["CountryID"].ToString();

                    ddlVideoPriority.SelectedValue = dt.Rows[0]["VideoLevelId"].ToString();

                    txtVideoTitle.Text = dt.Rows[0]["VideoTitle"].ToString();
                    txtVideoDesc.Text = dt.Rows[0]["VideoDesc"].ToString();
                    txtVideoDate.Text = dt.Rows[0]["VideoDate"].ToString();

                    txtVideoPath.Text = dt.Rows[0]["VideoYouTubeFile"].ToString();

                    //HtmlGenericControl contentPanel1 = (HtmlGenericControl)this.FindControl("ifmOtherVideoPath");

                    //contentPanel1.Attributes.Add("src", "/DesktopModules/ThSportSite/" + dt.Rows[0]["VideoOtherFile"].ToString());

                    //ifmOtherVideoPath.Attributes.Add("src", "/DesktopModules/ThSportSite/" + dt.Rows[0]["VideoOtherFile"].ToString());
                    ifmOtherVideoPath.InnerHtml =  "\\DesktopModules\\ThSportSite\\" + dt.Rows[0]["VideoOtherFile"].ToString();

                    if (dt.Rows[0]["VideoType"].ToString() == "1")
                    {
                        ddlvideotype.SelectedValue = "YouTube";
                        divvideopath.Visible = true;
                        divOtherVideoPath.Visible = false;
                        ifmOtherVideoPath.Visible = false;
                    }
                    else
                    {
                        ddlvideotype.SelectedValue = "Other";
                        divvideopath.Visible = false;
                        divOtherVideoPath.Visible = true;
                        ifmOtherVideoPath.Visible = true;
                    }

                    if (dt.Rows[0]["ActiveFlagId"].ToString() == "1")
                    {
                        ChkIsActive.Checked = true;
                    }
                    else
                    {
                        ChkIsActive.Checked = false;
                    }

                    if (dt.Rows[0]["ShowFlagId"].ToString() == "1")
                    {
                        ChkIsShow.Checked = true;
                    }
                    else
                    {
                        ChkIsShow.Checked = false;
                    }

                    pnlEntryVideo.Visible = true;
                    PnlGridVideo.Visible = false;
                    btnUpdateVideo.Visible = true;
                    btnSaveVideo.Visible = false;
                }
            }
            else if (ddlSelectedValue == "Delete")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "javascript:confirm('Are You Sure? Want To Delete.');", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", "javascript:Confirmation();", true);
                //int competition_Id = 0;
                //int.TryParse(str, out competition_Id);
                //CompRegInfo.DeleteCompetitionReg(competition_Id);
                //FillGridView();
            }
        }

        private void FillSport()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();

            DataTable dt = new DataTable();

            dt = ec.GetSportIDAndSportName();
            if (dt.Rows.Count > 0)
            {
                ddlSports.DataSource = dt;
                ddlSports.DataTextField = "SportName";
                ddlSports.DataValueField = "SportID";
                ddlSports.DataBind();
                ddlSports.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillEvent()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();

            DataTable dt = new DataTable();

            dt = ec.GetEventIDAndEventName();
            if (dt.Rows.Count > 0)
            {
                ddlEvent.DataSource = dt;
                ddlEvent.DataTextField = "EventName";
                ddlEvent.DataValueField = "EventID";
                ddlEvent.DataBind();
                ddlEvent.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillSeason()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();

            DataTable dt = new DataTable();

            dt = ec.GetSeasonIDAndSeasonName();
            if (dt.Rows.Count > 0)
            {
                ddlSeason.DataSource = dt;
                ddlSeason.DataTextField = "SeasonName";
                ddlSeason.DataValueField = "SeasonID";
                ddlSeason.DataBind();
                ddlSeason.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillPlayer()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.GetAllPlayer();
            if (dt.Rows.Count > 0)
            {
                ddlPlayer.DataSource = dt;
                ddlPlayer.DataTextField = "PlayerName";
                ddlPlayer.DataValueField = "RegistrationId";
                ddlPlayer.DataBind();
                ddlPlayer.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillPlayer(int TeamID)
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.GetPlayerIDAndPlayerNameByTeamID(TeamID);
            if (dt.Rows.Count > 0)
            {
                ddlPlayer.DataSource = dt;
                ddlPlayer.DataTextField = "PlayerName";
                ddlPlayer.DataValueField = "RegistrationId";
                ddlPlayer.DataBind();
                ddlPlayer.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillCompetition(int SportID)
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.GetAllCompetition();
            if (dt.Rows.Count > 0)
            {
                ddlCompetition.DataSource = dt;
                ddlCompetition.DataTextField = "CompetitionName";
                ddlCompetition.DataValueField = "CompetitionId";
                ddlCompetition.DataBind();
                ddlCompetition.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillClub(int SportID)
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.GetAllClubs();
            if (dt.Rows.Count > 0)
            {
                ddlClub.DataSource = dt;
                ddlClub.DataTextField = "ClubName";
                ddlClub.DataValueField = "ClubId";
                ddlClub.DataBind();
                ddlClub.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillTeam(int SportID, int ClubID)
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.GetAllTeams();
            if (dt.Rows.Count > 0)
            {
                ddlTeam.DataSource = dt;
                ddlTeam.DataTextField = "TeamName";
                ddlTeam.DataValueField = "TeamId";
                ddlTeam.DataBind();
                ddlTeam.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        protected void ddlClub_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ClubID = Convert.ToInt32(ddlClub.SelectedValue);

            if (ClubID > 0)
            {
                divclubowner.Visible = true;
                divclubmember.Visible = true;
                FillClubOwner(ClubID);
                FillClubMember(ClubID);
                int SportID = Convert.ToInt32(ddlSports.SelectedValue);
                FillTeam(SportID, ClubID);
            }
            else
            {
                divclubowner.Visible = false;
                divclubmember.Visible = false;
                FillClubOwner(ClubID);
                FillClubMember(ClubID);
                int SportID = Convert.ToInt32(ddlSports.SelectedValue);
                FillTeam(SportID, ClubID);
            }

        }

        private void FillClubOwner(int ClubID)
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.GetClubOwnerIDAndClubOwnerName(ClubID);
            if (dt.Rows.Count > 0)
            {
                ddlClubOwner.DataSource = dt;
                ddlClubOwner.DataTextField = "ClubOwnerName";
                ddlClubOwner.DataValueField = "ClubOwnersId";
                ddlClubOwner.DataBind();
                ddlClubOwner.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillClubMember(int ClubID)
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.GetClubMemberIDAndClubMemberName(ClubID);
            if (dt.Rows.Count > 0)
            {
                ddlClubMember.DataSource = dt;
                ddlClubMember.DataTextField = "ClubMemberName";
                ddlClubMember.DataValueField = "ClubMemberId";
                ddlClubMember.DataBind();
                ddlClubMember.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillTeamMember(int TeamID)
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.GetTeamMemberIDAndTeamMemberName(TeamID);
            if (dt.Rows.Count > 0)
            {
                ddlTeamMember.DataSource = dt;
                ddlTeamMember.DataTextField = "TeamMemberName";
                ddlTeamMember.DataValueField = "TeamMemberID";
                ddlTeamMember.DataBind();
                ddlTeamMember.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        protected void ddlSports_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int SportID = Convert.ToInt32(ddlSports.SelectedValue);

            //if (SportID > 0)
            //{
            //    FillCompetition(SportID);
            //    FillClub(SportID);
            //}
            //else
            //{
            //    divclubmember.Visible = false;
            //    divclubowner.Visible = false;
            //    divteammember.Visible = false;
            //    FillCompetition(SportID);
            //    FillClub(SportID);
            //}

        }

        protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    int TeamID = Convert.ToInt32(ddlTeam.SelectedValue);

            //if (TeamID > 0)
            //{
            //    divteammember.Visible = true;
            //    FillTeamMember(TeamID);
            //    FillPlayer(TeamID);
            //}
            //else
            //{
            //    divteammember.Visible = false;
            //    FillTeamMember(TeamID);
            //}
        }

        private void FillCompetition()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.FillComptitionIDAndCompetitionName();
            if (dt.Rows.Count > 0)
            {
                ddlCompetition.DataSource = dt;
                ddlCompetition.DataTextField = "CompetitionName";
                ddlCompetition.DataValueField = "CompetitionId";
                ddlCompetition.DataBind();
                ddlCompetition.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillClub()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.FillClubIDAndClubName();
            if (dt.Rows.Count > 0)
            {
                ddlClub.DataSource = dt;
                ddlClub.DataTextField = "ClubName";
                ddlClub.DataValueField = "ClubId";
                ddlClub.DataBind();
                ddlClub.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillClubOwner()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.FillClubOwnerIDAndClubOwnerName();
            if (dt.Rows.Count > 0)
            {
                ddlClubOwner.DataSource = dt;
                ddlClubOwner.DataTextField = "ClubOwnerName";
                ddlClubOwner.DataValueField = "ClubOwnersId";
                ddlClubOwner.DataBind();
                ddlClubOwner.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillClubMember()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.FillClubMemberIDAndClubMemberName();
            if (dt.Rows.Count > 0)
            {
                ddlClubMember.DataSource = dt;
                ddlClubMember.DataTextField = "ClubMemberName";
                ddlClubMember.DataValueField = "ClubMemberId";
                ddlClubMember.DataBind();
                ddlClubMember.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillTeam()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.FillTeamIDAndTeamName();
            if (dt.Rows.Count > 0)
            {
                ddlTeam.DataSource = dt;
                ddlTeam.DataTextField = "TeamName";
                ddlTeam.DataValueField = "TeamId";
                ddlTeam.DataBind();
                ddlTeam.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillTeamMember()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.FillTeamMemberIDAndTeamMemberName();
            if (dt.Rows.Count > 0)
            {
                ddlTeamMember.DataSource = dt;
                ddlTeamMember.DataTextField = "TeamMemberName";
                ddlTeamMember.DataValueField = "TeamMemberID";
                ddlTeamMember.DataBind();
                ddlTeamMember.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillCountry()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.FillCountryIDAndCountryName();
            if (dt.Rows.Count > 0)
            {
                ddlCountry.DataSource = dt;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryID";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        protected void ddlvideotype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlvideotype.SelectedValue == "YouTube")
            {
                divvideopath.Visible = true;
                divOtherVideoPath.Visible = false;
            }
            else
            {
                divvideopath.Visible = false;
                divOtherVideoPath.Visible = true;
            }
        }

    }
}