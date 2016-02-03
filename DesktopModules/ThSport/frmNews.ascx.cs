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
    public partial class frmNews : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\NewsImage\\";
        public string imhpathDB = "Images\\NewsImage\\";

        public string VideoUploadFolder = "DesktopModules\\ThSport\\Videos\\NewsVideo\\";
        public string videohpathDB = "Videos\\NewsVideo\\";

        clsNews cs = new clsNews();
        clsNewsController csc = new clsNewsController();

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

            if (currentUser.IsSuperUser || currentUser.IsInRole("Club Admin"))
            {
                dt = csc.GetDataNews();
            }

            if (dt.Rows.Count > 0)
            {
                gvNews.DataSource = dt;
                gvNews.DataBind();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            funClearData();
        }

        private void funClearData()
        {
            txtNewsTitle.Text = "";
            txtNewsDesc.Text = "";
            txtNewText.Text = "";
            txtNewsDate.Text = "";
            txtVideoPath.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
        }

        protected void btnSaveNews_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;
            Boolean FileOKVidoe = false;
            Boolean FileSavedVideo = false;
            
            if (ddlSports.SelectedValue == "")
            {
                cs.SportsId = 0;
            }
            else
            {
                cs.SportsId = Convert.ToInt32(ddlSports.SelectedValue);
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

            cs.NewsTitle = txtNewsTitle.Text.Trim();
            cs.NewsDesc = txtNewsDesc.Text.Trim();
            cs.NewsText = txtNewText.Text.Trim();
            cs.NewsDate = txtNewsDate.Text.Trim();           

            cs.NewsPicture = imhpathDB + NewsLogoFile.PostedFile.FileName.Replace(" ", "");

            if (NewsLogoFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(NewsLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
                String[] allowedExtensions = { ".png", ".jpg", ".gif", ".jpeg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (FileExtension == allowedExtensions[i])
                    {
                        FileOK = true;
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(NewsLogoFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                    return;
                }
            }

            if (FileOK)
            {
                if (NewsLogoFile.PostedFile.ContentLength > 10485760)
                {
                    //dvMsg.Attributes.Add("style", "display:block;");
                    //return;
                }
                else
                {
                    //dvMsg.Attributes.Add("style", "display:none;");
                }

                try
                {
                    NewsLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + NewsLogoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }

            // string youtubelink = "http://www.youtube.com/embed/";
            string videopath = txtVideoPath.Text;
            //youtubelink +
             cs.NewsVideo = videopath;
            
            if (NewsVideoLogoFile.PostedFile != null)
            {
                cs.NewsOtherVideoPath = videohpathDB + NewsVideoLogoFile.PostedFile.FileName.Replace(" ", "");
                String FileExtension = Path.GetExtension(NewsVideoLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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
                    NewsVideoLogoFile.PostedFile.SaveAs(physicalpath + VideoUploadFolder + NewsVideoLogoFile.PostedFile.FileName.Replace(" ", ""));
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

            // Call Save Method
            
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

            cs.NewsLevelId = ddlNewsPriority.SelectedValue;

            cs.PortalID = PortalId;
            cs.CreatedById = currentUser.Username;
            cs.ModifiedById = currentUser.Username;

            int spid = csc.InsertNews(cs);

            DataTable dt = new DataTable();
            dt = csc.GetLatestNewsID();
            if (dt.Rows.Count > 0)
            {
                cs.NewsId = Convert.ToInt32(dt.Rows[0]["NewsId"].ToString());
                csc.InsertNewsLinks(cs);
            }

            pnlEntryNews.Visible = false;
            PnlGridNews.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void btnAddNews_Click(object sender, EventArgs e)
        {
            funClearData();
            pnlEntryNews.Visible = true;
            PnlGridNews.Visible = false;
            btnSaveNews.Visible = true;
            btnUpdateNews.Visible = false;
            FillSport();
            FillCountry();
            FillEvent();
            FillSeason();
            FillSponsor();
            ddlvideotype.SelectedValue = "YouTube";
            divvideopath.Visible = true;
            divOtherVideoPath.Visible = false;
        }

        private void FillSponsor()
        {
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
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

        protected void btnCloseNews_Click(object sender, EventArgs e)
        {
            pnlEntryNews.Visible = false;
            PnlGridNews.Visible = true;
            FillGridView();
        }

        protected void btnUpdateNews_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully()", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            cs.NewsId = Convert.ToInt32(hidRegID.Value);

            if (ddlSports.SelectedValue == "")
            {
                cs.SportsId = 0;
            }
            else
            {
                cs.SportsId = Convert.ToInt32(ddlSports.SelectedValue);
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

            cs.NewsTitle = txtNewsTitle.Text.Trim();
            cs.NewsDesc = txtNewsDesc.Text.Trim();
            cs.NewsText = txtNewText.Text.Trim();
            cs.NewsDate = txtNewsDate.Text.Trim();

            if (NewsLogoFile.PostedFile.FileName == "")
            {
                DataTable dt1 = new DataTable();
                cs.NewsId = Convert.ToInt32(hidRegID.Value);
                dt1 = csc.GetNewsLogoByNewsID(cs);
                NewsLogoImage.ImageUrl = dt1.Rows[0]["NewsPicture"].ToString();
                string ufname = dt1.Rows[0]["NewsPicture"].ToString().Replace(" ", "");
                NewsLogoFile.ResolveUrl("ufname");
                cs.NewsPicture = ufname.Replace(" ", "");
                FileOKForUpdate = true;
            }
            else
            {
                cs.NewsPicture = imhpathDB + NewsLogoFile.PostedFile.FileName.Replace(" ", "");

                if (NewsLogoFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(NewsLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
                    String[] allowedExtensions = { ".png", ".jpg", ".gif", ".jpeg" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(NewsLogoFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                        return;
                    }
                }

                if (FileOK)
                {
                    if (NewsLogoFile.PostedFile.ContentLength > 10485760)
                    {
                        //dvMsg.Attributes.Add("style", "display:block;");
                        //return;
                    }
                    else
                    {
                        //dvMsg.Attributes.Add("style", "display:none;");
                    }

                    try
                    {
                        NewsLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + NewsLogoFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
            }

            if (ddlvideotype.SelectedValue == "YouTube")
            {
                cs.VideoType = 1;
                // string youtubelink = "http://www.youtube.com/embed/";
                string videopath = txtVideoPath.Text;
                //youtubelink 
                cs.NewsVideo = videopath;

            }
            else
            {
                cs.VideoType = 2;

                if (NewsVideoLogoFile.PostedFile.FileName == "")
                {
                    DataTable dt = new DataTable();
                    cs.NewsId = Convert.ToInt32(hidRegID.Value);

                    dt = csc.GetOtherVideoPathByNewsID(cs);
                    string ufname = dt.Rows[0]["NewsOtherVideoPath"].ToString().Replace(" ", "");
                    cs.NewsOtherVideoPath = ufname;
                }
                else
                {

                    if (NewsVideoLogoFile.PostedFile != null)
                    {
                        cs.NewsOtherVideoPath = videohpathDB + NewsVideoLogoFile.PostedFile.FileName.Replace(" ", "");
                        String FileExtension = Path.GetExtension(NewsVideoLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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
                            NewsVideoLogoFile.PostedFile.SaveAs(physicalpath + VideoUploadFolder + NewsVideoLogoFile.PostedFile.FileName.Replace(" ", ""));
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

            cs.NewsLevelId = ddlNewsPriority.SelectedValue;

            cs.PortalID = PortalId;
            cs.CreatedById = currentUser.Username;
            cs.ModifiedById = currentUser.Username;

            int eventid = csc.UpdateNews(cs);

            int evid = csc.UpdateNewsLinks(cs);

            pnlEntryNews.Visible = false;
            PnlGridNews.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void gvNews_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvNews.PageIndex = e.NewPageIndex;
            FillGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionNewsID")).Text;

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
                
                int NewsID = 0;
                int.TryParse(str, out NewsID);

                LinkButton btn = sender as LinkButton;

                clsNews cs = new clsNews();
                clsNewsController csc = new clsNewsController();

                DataTable dt = new DataTable();

                dt = csc.GetNewsDataByNewsID(NewsID);

                if (dt.Rows.Count > 0)
                {
                    hidRegID.Value = dt.Rows[0]["NewsId"].ToString();

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

                    ddlNewsPriority.SelectedValue = dt.Rows[0]["NewsLevelId"].ToString();

                    txtNewsTitle.Text = dt.Rows[0]["NewsTitle"].ToString();
                    txtNewsDesc.Text = dt.Rows[0]["NewsDesc"].ToString();
                    txtNewText.Text = dt.Rows[0]["NewsText"].ToString();
                    txtNewsDate.Text = dt.Rows[0]["NewsDate"].ToString();

                    NewsLogoImage.ImageUrl = dt.Rows[0]["NewsPicture"].ToString();
                    string ufname = dt.Rows[0]["NewsPicture"].ToString().Replace(" ", "");
                    NewsLogoFile.ResolveUrl("ufname");

                    txtVideoPath.Text = dt.Rows[0]["NewsVideo"].ToString();
                    
                    HtmlGenericControl contentPanel1 = (HtmlGenericControl)this.FindControl("ifmOtherVideoPath");
                    contentPanel1.Attributes["src"] = "DesktopModules\\ThSportSite\\" + dt.Rows[0]["NewsOtherVideoPath"].ToString();

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

                    pnlEntryNews.Visible = true;
                    PnlGridNews.Visible = false;
                    btnUpdateNews.Visible = true;
                    btnSaveNews.Visible = false;
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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();

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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();

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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();

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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
            DataTable dt = new DataTable();

            dt = ec.GetPlayerIDAndPlayerName();
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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
            DataTable dt = new DataTable();

            dt = ec.GetCompetitionIDAndCompetitionName(SportID);
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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
            DataTable dt = new DataTable();

            dt = ec.GetClubIDAndClubName(SportID);
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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
            DataTable dt = new DataTable();

            dt = ec.GetTeamIDAndTeamName(SportID, ClubID);
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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
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
            int SportID = Convert.ToInt32(ddlSports.SelectedValue);

            if (SportID > 0)
            {
                FillCompetition(SportID);
                FillClub(SportID);
            }
            else
            {
                divclubmember.Visible = false;
                divclubowner.Visible = false;
                divteammember.Visible = false;
                FillCompetition(SportID);
                FillClub(SportID);
            }

        }

        protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TeamID = Convert.ToInt32(ddlTeam.SelectedValue);

            if (TeamID > 0)
            {
                divteammember.Visible = true;
                FillTeamMember(TeamID);
                FillPlayer(TeamID);
            }
            else
            {
                divteammember.Visible = false;
                FillTeamMember(TeamID);
            }
        }

        private void FillCompetition()
        {
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
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
            clsNews e = new clsNews();
            clsNewsController ec = new clsNewsController();
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