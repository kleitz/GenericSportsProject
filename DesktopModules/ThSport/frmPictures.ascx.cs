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
    public partial class frmPictures : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\Pictures\\";
        public string imhpathDB = "Images\\Pictures\\";

        clsPictures cs = new clsPictures();
        clsPicturesController csc = new clsPicturesController();

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
                dt = csc.GetDataPicture();
            //}

            if (dt.Rows.Count > 0)
            {
                gvPicture.DataSource = dt;
                gvPicture.DataBind();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            funClearData();
        }

        private void funClearData()
        {
            txtPictureTitle.Text = "";
            txtPictureDesc.Text = "";
            txtPictureDate.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
        }

        protected void btnSavePicture_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;
        
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

            cs.PictureTitle = txtPictureTitle.Text.Trim();
            cs.PictureDesc = txtPictureDesc.Text.Trim();
            cs.PictureDate = txtPictureDate.Text.Trim();

            cs.PictureFile = imhpathDB + PictureLogoFile.PostedFile.FileName.Replace(" ", "");

            if (PictureLogoFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(PictureLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

            if (!string.IsNullOrEmpty(PictureLogoFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                    return;
                }
            }

            if (FileOK)
            {
                if (PictureLogoFile.PostedFile.ContentLength > 10485760)
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
                    PictureLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + PictureLogoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
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

            cs.PictureLevelId = ddlPicturePriority.SelectedValue;

            cs.PortalID = PortalId;
            cs.CreatedById = currentUser.Username;
            cs.ModifiedById = currentUser.Username;

            int spid = csc.InsertPicture(cs);

            DataTable dt = new DataTable();
            dt = csc.GetLatestPictureID();
            if (dt.Rows.Count > 0)
            {
                cs.PictureId = Convert.ToInt32(dt.Rows[0]["PictureId"].ToString());
                csc.InsertPictureLinks(cs);
            }

            pnlEntryPicture.Visible = false;
            PnlGridPicture.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void btnAddPicture_Click(object sender, EventArgs e)
        {
            funClearData();
            pnlEntryPicture.Visible = true;
            PnlGridPicture.Visible = false;
            btnSavePicture.Visible = true;
            btnUpdatePicture.Visible = false;
            FillSport();
            FillCountry();
            FillEvent();
            FillSeason();
            FillSponsor();
        }

        private void FillSponsor()
        {
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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

        protected void btnClosePicture_Click(object sender, EventArgs e)
        {
            pnlEntryPicture.Visible = false;
            PnlGridPicture.Visible = true;
            FillGridView();
        }

        protected void btnUpdatePicture_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully()", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            cs.PictureId = Convert.ToInt32(hidRegID.Value);

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

            cs.PictureTitle = txtPictureTitle.Text.Trim();
            cs.PictureDesc = txtPictureDesc.Text.Trim();
            cs.PictureDate = txtPictureDate.Text.Trim();

            if (PictureLogoFile.PostedFile.FileName == "")
            {
                DataTable dt1 = new DataTable();
                cs.PictureId = Convert.ToInt32(hidRegID.Value);
                dt1 = csc.GetPictureLogoByPictureID(cs);
                PictureLogoImage.ImageUrl = dt1.Rows[0]["PictureFile"].ToString();
                string ufname = dt1.Rows[0]["PictureFile"].ToString().Replace(" ", "");
                PictureLogoFile.ResolveUrl("ufname");
                cs.PictureFile = ufname.Replace(" ", "");
                FileOKForUpdate = true;
            }
            else
            {
                cs.PictureFile = imhpathDB + PictureLogoFile.PostedFile.FileName.Replace(" ", "");

                if (PictureLogoFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(PictureLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

                if (!string.IsNullOrEmpty(PictureLogoFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                        return;
                    }
                }

                if (FileOK)
                {
                    if (PictureLogoFile.PostedFile.ContentLength > 10485760)
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
                        PictureLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + PictureLogoFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
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

            cs.PictureLevelId = ddlPicturePriority.SelectedValue;

            cs.PortalID = PortalId;
            cs.ModifiedById = currentUser.Username;

            int eventid = csc.UpdatePicture(cs);

            int evid = csc.UpdatePictureLinks(cs);

            pnlEntryPicture.Visible = false;
            PnlGridPicture.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void gvPicture_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPicture.PageIndex = e.NewPageIndex;
            FillGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionPictureID")).Text;

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

                int PictureID = 0;
                int.TryParse(str, out PictureID);

                LinkButton btn = sender as LinkButton;

                clsPictures cs = new clsPictures();
                clsPicturesController csc = new clsPicturesController();

                DataTable dt = new DataTable();

                dt = csc.GetPictureDataByPictureID(PictureID);

                if (dt.Rows.Count > 0)
                {
                    hidRegID.Value = dt.Rows[0]["PictureId"].ToString();

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

                    ddlPicturePriority.SelectedValue = dt.Rows[0]["PictureLevelId"].ToString();

                    txtPictureTitle.Text = dt.Rows[0]["PictureTitle"].ToString();
                    txtPictureDesc.Text = dt.Rows[0]["PictureDesc"].ToString();
                    txtPictureDate.Text = dt.Rows[0]["PictureDate"].ToString();

                    PictureLogoImage.ImageUrl = dt.Rows[0]["PictureFile"].ToString();
                    string ufname = dt.Rows[0]["PictureFile"].ToString().Replace(" ", "");
                    PictureLogoFile.ResolveUrl("ufname");

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

                    pnlEntryPicture.Visible = true;
                    PnlGridPicture.Visible = false;
                    btnUpdatePicture.Visible = true;
                    btnSavePicture.Visible = false;
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();

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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();

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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();

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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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
            clsPictures e = new clsPictures();
            clsPicturesController ec = new clsPicturesController();
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

    }
}