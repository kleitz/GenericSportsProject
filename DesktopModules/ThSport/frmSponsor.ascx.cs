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

namespace DotNetNuke.Modules.ThSport
{
    public partial class frmSponsor : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\SponsorImages\\";
        public string imhpathDB = "Images\\SponsorImages\\";

        clsSponsor cs = new clsSponsor();
        clsSponsorController csc = new clsSponsorController();

        string m_controlToLoad;
        string VName;
        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;

        Boolean FileOKForUpdate = false;
        Boolean FileSavedForUpdate = false;


        int sponsorID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdnSponsorID.Value))
                {
                    int.TryParse(hdnSponsorID.Value, out id);
                }
                return id;
            }
        }

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
                dt = csc.GetDataSponsor();
            }

            DataView dv = new DataView();
            dv = dt.AsDataView();
            dv.RowFilter = " SponsorName like '%%" + txtSponsorNameSearch.Text.Trim() + "%%'";

            if (dv.ToTable().Rows.Count > 0)
            {
                ViewState["dt"] = dv.ToTable();
                
            }
            gvSponsor.DataSource = dv.ToTable();
            gvSponsor.DataBind();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            funClearData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (hndDeleteConfirm.Value == "true")
            {
                DeleteSponsor();
            }
        }

        public void DeleteSponsor()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);
            csc.DeleteSponsor(sponsorID);
            BindGrid();

        }
        private void funClearData()
        {
            txtSponsorName.Text = "";
            txtSponsorAbbreviation.Text = "";
            txtSponsorDetail.Text = "";
            txtSponsorStartDate.Text = "";
            txtSponsorEndDate.Text = "";
            txtSponsorLogoName.Text = "";
            txtSponsorAmount.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
        }

        protected void btnSaveSponsor_Click(object sender, EventArgs e)
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

            if (ddlSponsorLevel.SelectedValue == "")
            {
                cs.SponsorLevelId = 0;
            }
            else
            {
                cs.SponsorLevelId = Convert.ToInt32(ddlSponsorLevel.SelectedValue);
            }

            if (ddlSponsorType.SelectedValue == "")
            {
                cs.SponsorTypeId = 0;
            }
            else
            {
                cs.SponsorTypeId = Convert.ToInt32(ddlSponsorType.SelectedValue);
            }

            cs.SponsorName = txtSponsorName.Text.Trim();
            cs.SponsorAbbr = txtSponsorAbbreviation.Text.Trim();
            cs.SponsorDesc = txtSponsorDetail.Text.Trim();
            cs.SponsorLogoName = txtSponsorLogoName.Text.Trim();

            cs.SponsorLogoFile = imhpathDB + SponsorLogoFile.PostedFile.FileName.Replace(" ", "");

            if (SponsorLogoFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(SponsorLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

            if (!string.IsNullOrEmpty(SponsorLogoFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                    return;
                }
            }

            if (FileOK)
            {
                if (SponsorLogoFile.PostedFile.ContentLength > 10485760)
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
                    SponsorLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + SponsorLogoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }

            cs.SponsorStartDate = txtSponsorStartDate.Text.Trim();
            cs.SponsorEndDate = txtSponsorEndDate.Text.Trim();
            if(txtSponsorAmount.Text!= "")
            cs.SponsorAmt = Convert.ToInt32(txtSponsorAmount.Text.Trim());

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

            cs.PortalID = PortalId;
            cs.CreatedById = currentUser.Username;
            cs.ModifiedById = currentUser.Username;

            int spid = csc.InsertSponsor(cs);

            DataTable dt = new DataTable();
            dt = csc.GetLatestSponsorID();
            if (dt.Rows.Count > 0)
            {
                cs.SponsorId = Convert.ToInt32(dt.Rows[0]["SponsorId"].ToString());
                csc.InsertSponsorSports(cs);
            }

            pnlSponsorEntry.Visible = false;
            PnlGridSponsor.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void btnAddSponsor_Click(object sender, EventArgs e)
        {
            funClearData();
            pnlSponsorEntry.Visible = true;
            PnlGridSponsor.Visible = false;
            btnSaveSponsor.Visible = true;
            btnUpdateSponsor.Visible = false;
            FillSport();
            FillEvent();
            FillSeason();
            FillSponsorLevel();
            FillSponsorType();
        }

        private void FillSponsorLevel()
        {
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
            DataTable dt = new DataTable();

            dt = ec.GetSponsorLevelIDAndSponsorLevelName();
            if (dt.Rows.Count > 0)
            {
                ddlSponsorLevel.DataSource = dt;
                ddlSponsorLevel.DataTextField = "SponsorLevelValue";
                ddlSponsorLevel.DataValueField = "SponsorLevelId";
                ddlSponsorLevel.DataBind();
                ddlSponsorLevel.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillSponsorType()
        {
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
            DataTable dt = new DataTable();

            dt = ec.GetSponsorTypeIDAndSponsorTypeName();
            if (dt.Rows.Count > 0)
            {
                ddlSponsorType.DataSource = dt;
                ddlSponsorType.DataTextField = "SponsorTypeValue";
                ddlSponsorType.DataValueField = "SponsorTypeId";
                ddlSponsorType.DataBind();
                ddlSponsorType.Items.Insert(0, new ListItem("-- Select --", "0"));
            }    
        }

        protected void btnCloseSponsor_Click(object sender, EventArgs e)
        {
            pnlSponsorEntry.Visible = false;
            PnlGridSponsor.Visible = true;
            FillGridView();
        }

        protected void btnUpdateSponsor_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully()", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            cs.SponsorId = Convert.ToInt32(hidRegID.Value);

            if (ddlSports.SelectedValue == "")
            {
                cs.SportsId = 0;
            }
            else
            {
                cs.SportsId = Convert.ToInt32(ddlSports.SelectedValue);
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

            if (ddlSponsorLevel.SelectedValue == "")
            {
                cs.SponsorLevelId = 0;
            }
            else
            {
                cs.SponsorLevelId = Convert.ToInt32(ddlSponsorLevel.SelectedValue);
            }

            if (ddlSponsorType.SelectedValue == "")
            {
                cs.SponsorTypeId = 0;
            }
            else
            {
                cs.SponsorTypeId = Convert.ToInt32(ddlSponsorType.SelectedValue);
            }

            cs.SponsorName = txtSponsorName.Text.Trim();
            cs.SponsorAbbr = txtSponsorAbbreviation.Text.Trim();
            cs.SponsorDesc = txtSponsorDetail.Text.Trim();
            cs.SponsorLogoName = txtSponsorLogoName.Text.Trim();

            if (SponsorLogoFile.PostedFile.FileName == "")
            {
                DataTable dt1 = new DataTable();
                cs.SponsorId = Convert.ToInt32(hidRegID.Value);
                dt1 = csc.GetSponsorLogoBySponsorID(cs);
                SponsorLogoImage.ImageUrl = dt1.Rows[0]["SponsorLogoFile"].ToString();
                string ufname = dt1.Rows[0]["SponsorLogoFile"].ToString().Replace(" ", "");
                SponsorLogoFile.ResolveUrl("ufname");
                cs.SponsorLogoFile = ufname.Replace(" ", "");
                FileOKForUpdate = true;
            }
            else
            {
                cs.SponsorLogoFile = imhpathDB + SponsorLogoFile.PostedFile.FileName.Replace(" ", "");

                if (SponsorLogoFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(SponsorLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

                if (!string.IsNullOrEmpty(SponsorLogoFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                        return;
                    }
                }

                if (FileOK)
                {
                    if (SponsorLogoFile.PostedFile.ContentLength > 10485760)
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
                        SponsorLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + SponsorLogoFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
            }

            cs.SponsorStartDate = txtSponsorStartDate.Text.Trim();
            cs.SponsorEndDate = txtSponsorEndDate.Text.Trim();
            cs.SponsorAmt = Convert.ToInt32(txtSponsorAmount.Text.Trim());

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

            cs.PortalID = PortalId;
            cs.ModifiedById = currentUser.Username;

            int eventid = csc.UpdateSponsor(cs);

            int evid = csc.UpdateSponsorSport(cs);

            pnlSponsorEntry.Visible = false;
            PnlGridSponsor.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void gvSponsor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSponsor.PageIndex = e.NewPageIndex;
            FillGridView();
        }

        protected void lbGo_Click(object sender, EventArgs e)
        {
            FillGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnSponsorID.Value= ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionSponsorID")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                funClearData();
                FillSport();
                FillEvent();
                FillSeason();
                FillCompetition();
                FillClub();
                FillClubOwner();
                FillClubMember();
                FillTeam();
                FillTeamMember();
                FillPlayer();
                FillSponsorLevel();
                FillSponsorType();

                
                LinkButton btn = sender as LinkButton;

                clsSponsor cs = new clsSponsor();
                clsSponsorController csc = new clsSponsorController();

                DataTable dt = new DataTable();

                dt = csc.GetSponsorDataBySponsorID(sponsorID);

                if (dt.Rows.Count > 0)
                {
                    hidRegID.Value = dt.Rows[0]["SponsorId"].ToString();

                    ddlSports.SelectedValue = dt.Rows[0]["SportsId"].ToString();
                    ddlSeason.SelectedValue = dt.Rows[0]["SeasonId"].ToString();
                    ddlCompetition.SelectedValue = dt.Rows[0]["CompetitionId"].ToString();
                    ddlClub.SelectedValue = dt.Rows[0]["ClubId"].ToString();
                    ddlClubOwner.SelectedValue = dt.Rows[0]["ClubOwnersId"].ToString();
                    ddlClubMember.SelectedValue = dt.Rows[0]["ClubMemberId"].ToString();
                    ddlTeam.SelectedValue = dt.Rows[0]["TeamId"].ToString();
                    ddlTeamMember.SelectedValue = dt.Rows[0]["TeamMemberId"].ToString();
                    ddlPlayer.SelectedValue = dt.Rows[0]["PlayerId"].ToString();
                    ddlEvent.SelectedValue = dt.Rows[0]["EventId"].ToString();

                    ddlSponsorLevel.SelectedValue = dt.Rows[0]["SponsorLevelId"].ToString();
                    ddlSponsorType.SelectedValue = dt.Rows[0]["SponsorTypeId"].ToString();

                    txtSponsorName.Text = dt.Rows[0]["SponsorName"].ToString();
                    txtSponsorAbbreviation.Text = dt.Rows[0]["SponsorAbbr"].ToString();
                    txtSponsorDetail.Text = dt.Rows[0]["SponsorDesc"].ToString();
                    txtSponsorLogoName.Text = dt.Rows[0]["SponsorLogoName"].ToString();

                          
                    SponsorLogoImage.ImageUrl = dt.Rows[0]["SponsorLogoFile"].ToString();
                    string ufname = dt.Rows[0]["SponsorLogoFile"].ToString().Replace(" ", "");
                    SponsorLogoFile.ResolveUrl("ufname");
                    
                    txtSponsorStartDate.Text = dt.Rows[0]["SponsorStartDate"].ToString();
                    txtSponsorEndDate.Text = dt.Rows[0]["SponsorEndDate"].ToString();
                    txtSponsorAmount.Text = dt.Rows[0]["SponsorAmt"].ToString();

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

                    pnlSponsorEntry.Visible = true;
                    PnlGridSponsor.Visible = false;
                    btnUpdateSponsor.Visible = true;
                    btnSaveSponsor.Visible = false;
                }
            }
            else if (ddlSelectedValue == "Delete")
            {

                if (csc.IsSponsorHasOtherData(sponsorID).Rows[0]["RefData"].ToString() != "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteConfirm('" + "Delete" + "');;", true);

                }
                else
                {
                    DeleteSponsor();
                }
              

                //int competition_Id = 0;

                //int.TryParse(str, out competition_Id);

                //CompRegInfo.DeleteCompetitionReg(competition_Id);

                //FillGridView();
            }
        }

        private void FillSport()
        {
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
            clsSponsor e = new clsSponsor();
            clsSponsorController ec = new clsSponsorController();
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
        
    }
}