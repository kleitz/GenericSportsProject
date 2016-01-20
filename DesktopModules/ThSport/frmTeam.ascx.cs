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
using System.Data;
using System.IO;

namespace DotNetNuke.Modules.ThSport
{
    public partial class frmTeam : PortalModuleBase
    {
        clsTeam tmClass = new clsTeam();
        clsTeamController tmController = new clsTeamController();
        clsClubController cbController = new clsClubController();
        clsSportController spController = new clsSportController();
        
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region variables

        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\TeamLogo\\";
        public string imhpathDB = "Images\\TeamLogo\\";
        Boolean FileOK = false;
        Boolean FileSaved = false;
        Boolean FileOKForUpdate = false;
        Boolean FileSavedForUpdate = false;

        int TeamID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdnTeamID.Value))
                {
                    int.TryParse(hdnTeamID.Value, out id);
                }
                return id;
            }
        }

        #endregion variables

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                FillDropDownForMaster();
            }

            btnUpdateTeam.Visible = false;
            btnSaveTeam.Visible = false;
            pnlTeamEntry.Visible = false;

            LoadTeamGrid();

        }

        #endregion Page Events

        #region Methods

        public void FillDropDownForMaster()
        {
            using (DataTable club_dt = cbController.GetDataClub())
            {
                if (club_dt.Rows.Count > 0)
                {
                    ddlClub.DataSource = club_dt;
                    ddlClub.DataTextField = "ClubName";
                    ddlClub.DataValueField = "ClubID";
                    ddlClub.DataBind();
                }
                ddlClub.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            using (DataTable Sport_dt = spController.GetDataSport())
            {
                if (Sport_dt.Rows.Count > 0)
                {
                    ddlSport.DataSource = Sport_dt;
                    ddlSport.DataTextField = "SportName";
                    ddlSport.DataValueField = "SportID";
                    ddlSport.DataBind();
                }
                ddlSport.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        public void LoadTeamGrid()
        {
            DataTable dt = new DataTable();
            dt = tmController.GetTeamList();

            gvTeam.DataSource = dt;
            gvTeam.DataBind();
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveTeam_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            tmClass.TeamName = txtTeam.Text.Trim();
            tmClass.TeamAbbr = txtTeamAbbr.Text;
            tmClass.TeamDesc = txtTeamDesc.Text.Trim();
            tmClass.TeamLogoName = txtTeamLogoName.Text;
            tmClass.TeamFamousName = txtFamousName.Text;
            int.TryParse(ddlSport.SelectedValue, out tmClass.SportId);
            int.TryParse(ddlClub.SelectedValue, out tmClass.ClubId);

            tmClass.TeamEstablishedYear = (txtEstablishedYear.Text != "" ? Convert.ToDateTime(txtEstablishedYear.Text, new System.Globalization.CultureInfo("en-GB")) : DateTime.MinValue);

            #region Team Logo Upload

            tmClass.TeamLogoFile = imhpathDB + TeamLogoFile.PostedFile.FileName.Replace(" ", "");
            if (TeamLogoFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(TeamLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

            if (!string.IsNullOrEmpty(TeamLogoFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Team !')", true);
                    return;
                }
            }

            if (FileOK)
            {
                if (TeamLogoFile.PostedFile.ContentLength > 10485760)
                {
                    //dvMsg.Attributes.Add("style", "display:block;");
                    return;
                }
                else
                {
                    //dvMsg.Attributes.Add("style", "display:none;");
                }

                try
                {
                    TeamLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + TeamLogoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }

            #endregion

            #region Team Photo Upload

            tmClass.TeamPhotoFile = imhpathDB + TeamPhotoFile.PostedFile.FileName.Replace(" ", "");

            if (TeamPhotoFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(TeamPhotoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

            if (!string.IsNullOrEmpty(TeamPhotoFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Team !')", true);
                    return;
                }
            }

            if (FileOK)
            {
                if (TeamPhotoFile.PostedFile.ContentLength > 10485760)
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
                    TeamPhotoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + TeamPhotoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }

            #endregion

            #region Audio Files

            tmClass.TeamAnthemAudioFile = imhpathDB + TeamAnthemAudioFile.PostedFile.FileName.Replace(" ", "");

            if (TeamAnthemAudioFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(TeamAnthemAudioFile.PostedFile.FileName.Replace(" ", "")).ToLower();
                String[] allowedExtensions = { ".mp3", ".mpeg", ".mpu" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (FileExtension == allowedExtensions[i])
                    {
                        FileOK = true;
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(TeamAnthemAudioFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Team !')", true);
                    return;
                }
            }

            if (FileOK)
            {
                if (TeamAnthemAudioFile.PostedFile.ContentLength > 10485760)
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
                    TeamAnthemAudioFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + TeamAnthemAudioFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }

            #endregion


            tmClass.ActiveFlagId = Convert.ToInt32(ChkIsActive.Checked);
            tmClass.ShowFlagId = Convert.ToInt32(ChkIsShow.Checked);
            tmClass.PortalID = PortalId;
            tmClass.CreatedById = currentUser.Username;
            tmClass.ModifiedById = currentUser.Username;

            // Call Save Method
            tmController.InsertTeam(tmClass);

            btnAddTeam.Visible = true;
            pnlTeamGrid.Visible = true;
            LoadTeamGrid();
            ClearData();

        }

        protected void btnUpdateTeam_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            tmClass.TeamId = TeamID;
            tmClass.TeamName = txtTeam.Text.Trim();
            tmClass.TeamAbbr = txtTeamAbbr.Text;
            tmClass.TeamDesc = txtTeamDesc.Text.Trim();
            tmClass.TeamLogoName = txtTeamLogoName.Text;
            tmClass.TeamFamousName = txtFamousName.Text;
            int.TryParse(ddlSport.SelectedValue, out tmClass.SportId);
            int.TryParse(ddlClub.SelectedValue, out tmClass.ClubId);

            tmClass.TeamEstablishedYear = (txtEstablishedYear.Text != "" ? Convert.ToDateTime(txtEstablishedYear.Text, new System.Globalization.CultureInfo("en-GB")) : DateTime.MinValue);

            #region Team Logo Upload

            if (TeamLogoFile.PostedFile.FileName == "")
            {
                tmClass.TeamLogoFile = TeamLogoImage.ImageUrl;
                FileOKForUpdate = true;
            }
            else
            {

                tmClass.TeamLogoFile = imhpathDB + TeamLogoFile.PostedFile.FileName.Replace(" ", "");

                if (TeamLogoFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(TeamLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

                if (!string.IsNullOrEmpty(TeamLogoFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Team !')", true);
                        return;
                    }
                }

                if (FileOK)
                {
                    if (TeamLogoFile.PostedFile.ContentLength > 10485760)
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
                        TeamLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + TeamLogoFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
            }

            #endregion 

            #region Team Photo Upload

            if (TeamPhotoFile.PostedFile.FileName == "")
            {
                tmClass.TeamPhotoFile = TeamPhotoImage.ImageUrl;
                FileOKForUpdate = true;
            }
            else
            {

                tmClass.TeamPhotoFile = imhpathDB + TeamPhotoFile.PostedFile.FileName.Replace(" ", "");

                if (TeamPhotoFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(TeamPhotoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

                if (!string.IsNullOrEmpty(TeamPhotoFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Team !')", true);
                        return;
                    }
                }

                if (FileOK)
                {
                    if (TeamPhotoFile.PostedFile.ContentLength > 10485760)
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
                        TeamPhotoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + TeamPhotoFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
            }

            #endregion

            #region Team Anthem Audio Upload

            if (TeamAnthemAudioFile.PostedFile.FileName == "")
            {
                tmClass.TeamAnthemAudioFile = lblAudioFile.Text;
                FileOKForUpdate = true;
            }
            else
            {

                tmClass.TeamAnthemAudioFile = imhpathDB + TeamAnthemAudioFile.PostedFile.FileName.Replace(" ", "");

                if (TeamPhotoFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(TeamAnthemAudioFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

                if (!string.IsNullOrEmpty(TeamAnthemAudioFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Team !')", true);
                        return;
                    }
                }

                if (FileOK)
                {
                    if (TeamAnthemAudioFile.PostedFile.ContentLength > 10485760)
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
                        TeamAnthemAudioFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + TeamAnthemAudioFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
            }

            #endregion

            tmClass.ActiveFlagId = Convert.ToInt32(ChkIsActive.Checked);
            tmClass.ShowFlagId = Convert.ToInt32(ChkIsShow.Checked);
            tmClass.PortalID = PortalId;
            tmClass.ModifiedById = currentUser.Username;

            // Call Update Method
            tmController.UpdateTeam(tmClass);

            btnAddTeam.Visible = true;
            pnlTeamGrid.Visible = true;
            btnSaveTeam.Visible = true;
            btnUpdateTeam.Visible = false;
            LoadTeamGrid();
            ClearData();
        }

        protected void btnCancelTeam_Click(object sender, EventArgs e)
        {
            pnlTeamGrid.Visible = true;
            pnlTeamEntry.Visible = false;
            btnSaveTeam.Visible = false;
            btnUpdateTeam.Visible = false;
            LoadTeamGrid();
            ClearData();
        }

        public void ClearData()
        {
            ddlClub.ClearSelection();
            ddlSport.ClearSelection();
            txtTeam.Text = "";
            txtTeamDesc.Text = "";
            txtTeamAbbr.Text = "";
            txtTeamLogoName.Text = "";
            txtFamousName.Text = "";
            txtEstablishedYear.Text = "";
            lblAudioFile.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
        }

        protected void btnAddTeam_Click(object sender, EventArgs e)
        {
            pnlTeamGrid.Visible = false;
            pnlTeamEntry.Visible = true;
            btnSaveTeam.Visible = true;
            btnUpdateTeam.Visible = false;
            ClearData();
        }

        #endregion Button Click Events

        #region Gridview Events

        protected void gvTeam_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTeam.PageIndex = e.NewPageIndex;
            LoadTeamGrid();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnTeamID.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdn_Team_Id")).Value;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                ClearData();
                DataTable dt1 = tmController.GetTeamDetailByTeamID(TeamID);

                if (dt1.Rows.Count > 0)
                {
                    txtTeam.Text = dt1.Rows[0]["TeamName"].ToString();
                    txtTeamDesc.Text = dt1.Rows[0]["TeamDesc"].ToString();
                    txtTeamAbbr.Text = dt1.Rows[0]["TeamAbbr"].ToString();
                    txtFamousName.Text = dt1.Rows[0]["TeamFamousName"].ToString();
                    ddlClub.SelectedValue = dt1.Rows[0]["ClubId"].ToString();
                    ddlSport.SelectedValue = dt1.Rows[0]["SportId"].ToString();

                    DateTime establisedDate = new DateTime();
                    DateTime.TryParse(dt1.Rows[0]["TeamEstablishedYear"].ToString(), out establisedDate);
                    txtEstablishedYear.Text = establisedDate.ToString("yyyy/MM/dd HH':'mm");
                    
                    txtTeamLogoName.Text = dt1.Rows[0]["TeamLogoName"].ToString();

                    TeamLogoImage.ImageUrl = dt1.Rows[0]["TeamLogoFile"].ToString();
                    string ufname = dt1.Rows[0]["TeamLogoFile"].ToString().Replace(" ", "");
                    TeamLogoImage.ResolveUrl("ufname");

                    TeamPhotoImage.ImageUrl = dt1.Rows[0]["TeamPhotoFile"].ToString();
                    string photoName = dt1.Rows[0]["TeamPhotoFile"].ToString().Replace(" ", "");
                    TeamPhotoImage.ResolveUrl("photoName");

                    lblAudioFile.Text = dt1.Rows[0]["TeamAnthemAudioFile"].ToString().Replace("Images\\TeamLogo\\", "");

                    if (dt1.Rows[0]["ActiveFlagId"].ToString() == "1")
                    {
                        ChkIsActive.Checked = true;
                    }
                    if (dt1.Rows[0]["ShowFlagId"].ToString() == "1")
                    {
                        ChkIsShow.Checked = true;
                    }
                }

                btnUpdateTeam.Visible = true;
                btnSaveTeam.Visible = false;
                pnlTeamEntry.Visible = true;
                pnlTeamGrid.Visible = false;
            }
            else if (ddlSelectedValue == "Member")
            {
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmTeamMember", "TeamID=" + TeamID));
            }
            else if (ddlSelectedValue == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);
                tmController.DeleteTeam(TeamID);
                LoadTeamGrid();
            }
        }

        #endregion
    }
}