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
    public partial class frmCompetition : PortalModuleBase
    {
        clsCompetition cl = new clsCompetition();
        clsCompetitionController clc = new clsCompetitionController();

        clsCompetitionLeagueController clController = new clsCompetitionLeagueController();
        clsCompetitionTypeController ctController = new clsCompetitionTypeController();
        clsCompetitionFormatController cfController = new clsCompetitionFormatController();
        clsSeasonController sController = new clsSeasonController();
        clsSportController spController = new clsSportController();
        clsDivisionController dvController = new clsDivisionController();
       

        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region variables

        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\CompetitionLogo\\";
        public string imhpathDB = "Images\\CompetitionLogo\\";
        Boolean FileOK = false;
        Boolean FileSaved = false;
        Boolean FileOKForUpdate = false;
        Boolean FileSavedForUpdate = false;

        int competitionID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdnCompetitionID.Value))
                {
                    int.TryParse(hdnCompetitionID.Value, out id);
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

            btnUpdateCompetition.Visible = false;
            btnSaveCompetition.Visible = false;
            pnlCompetitionEntry.Visible = false;

            LoadCompetitionGrid();

        }

        #endregion Page Events

        #region Methods

        public void FillDropDownForMaster()
        {
            using (DataTable division_dt = dvController.GetDivisionList())
            {
                if (division_dt.Rows.Count > 0)
                {
                    ddlDivision.DataSource = division_dt;
                    ddlDivision.DataTextField = "DivisionName";
                    ddlDivision.DataValueField = "DivisionID";
                    ddlDivision.DataBind();
                }
                ddlDivision.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            using (DataTable Season_dt = sController.GetDataSeason())
            {
                if (Season_dt.Rows.Count > 0)
                {
                    ddlSeason.DataSource = Season_dt;
                    ddlSeason.DataTextField = "SeasonName";
                    ddlSeason.DataValueField = "SeasonID";
                    ddlSeason.DataBind();
                }
                ddlSeason.Items.Insert(0, new ListItem("--Select--", "0"));
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
            using (DataTable Comp_Type_dt = ctController.GetCompetitionTypeList())
            {
                if (Comp_Type_dt.Rows.Count > 0)
                {
                    ddlCompetitionType.DataSource = Comp_Type_dt;
                    ddlCompetitionType.DataTextField = "CompetitionTypeName";
                    ddlCompetitionType.DataValueField = "CompetitionTypeID";
                    ddlCompetitionType.DataBind();
                }
                ddlCompetitionType.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            using (DataTable Comp_League_dt = clController.GetCompetitionLeagueList())
            {
                if (Comp_League_dt.Rows.Count > 0)
                {
                    ddlCompetitionLeague.DataSource = Comp_League_dt;
                    ddlCompetitionLeague.DataTextField = "CompetitionLeagueName";
                    ddlCompetitionLeague.DataValueField = "CompetitionLeagueID";
                    ddlCompetitionLeague.DataBind();
                }
                ddlCompetitionLeague.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            using (DataTable Comp_Format_dt = cfController.GetCompetitionFormatList())
            {
                if (Comp_Format_dt.Rows.Count > 0)
                {
                    ddlCompetitionFormat.DataSource = Comp_Format_dt;
                    ddlCompetitionFormat.DataTextField = "CompetitionFormatName";
                    ddlCompetitionFormat.DataValueField = "CompetitionFormatID";
                    ddlCompetitionFormat.DataBind();
                }
                ddlCompetitionFormat.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            
        }

        public void LoadCompetitionGrid()
        {
            DataTable dt = new DataTable();
            dt = clc.GetCompetitionList();

            gvCompetition.DataSource = dt;
            gvCompetition.DataBind();
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveCompetition_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            cl.CompetitionName = txtCompetition.Text.Trim();
            cl.CompetitionAbbr = txtCompetitionAbbr.Text;
            cl.CompetitionDesc = txtCompetitionDesc.Text.Trim();
            cl.CompetitionLogoName = txtCompetitionLogoName.Text;

            int.TryParse(ddlSeason.SelectedValue, out cl.SeasonId);
            int.TryParse(ddlCompetitionLeague.SelectedValue, out cl.CompeitionLeagueId);
            int.TryParse(ddlCompetitionType.SelectedValue, out cl.CompetitionTypeId);
            int.TryParse(ddlCompetitionFormat.SelectedValue, out cl.CompetitionFormatId);
            int.TryParse(ddlSport.SelectedValue, out cl.SportId);
            int.TryParse(ddlDivision.SelectedValue, out cl.DivisionId);

            if (cl.DivisionId > 0)
            {
                // Insert Selected Division Teams into Competition Teams

            }

            int.TryParse(txtNoOfGroup.Text, out cl.NumberofGroups);
            int.TryParse(txtNoOfTeam.Text, out cl.NumberofTeams);

            cl.StartDate = (txtStartDate.Text != "" ? Convert.ToDateTime(txtStartDate.Text, new System.Globalization.CultureInfo("en-GB")) : DateTime.MinValue);
            cl.EndDate = (txtEndDate.Text != "" ? Convert.ToDateTime(txtEndDate.Text, new System.Globalization.CultureInfo("en-GB")) : DateTime.MinValue);
            

            cl.CompetitionLogoFile = imhpathDB + CompetitionLogoFile.PostedFile.FileName.Replace(" ", "");

            if (CompetitionLogoFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(CompetitionLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

            if (!string.IsNullOrEmpty(CompetitionLogoFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                    return;
                }
            }

            if (FileOK)
            {
                if (CompetitionLogoFile.PostedFile.ContentLength > 10485760)
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
                    CompetitionLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + CompetitionLogoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }


            cl.ActiveFlagId = Convert.ToInt32(ChkIsActive.Checked);
            cl.ShowFlagId = Convert.ToInt32(ChkIsShow.Checked);
            cl.PortalID = PortalId;
            cl.CreatedById = currentUser.Username;
            cl.ModifiedById = currentUser.Username;

            // Call Save Method
            clc.InsertCompetition(cl);

            btnAddCompetition.Visible = true;
            pnlCompetitionGrid.Visible = true;
            LoadCompetitionGrid();
            ClearData();

        }

        protected void btnUpdateCompetition_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            cl.CompetitionId = competitionID;
            cl.CompetitionName = txtCompetition.Text.Trim();
            cl.CompetitionAbbr = txtCompetitionAbbr.Text;
            cl.CompetitionDesc = txtCompetitionDesc.Text.Trim();
            cl.CompetitionLogoName = txtCompetitionLogoName.Text;

            int.TryParse(ddlSeason.SelectedValue, out cl.SeasonId);
            int.TryParse(ddlCompetitionLeague.SelectedValue, out cl.CompeitionLeagueId);
            int.TryParse(ddlCompetitionType.SelectedValue, out cl.CompetitionTypeId);
            int.TryParse(ddlCompetitionFormat.SelectedValue, out cl.CompetitionFormatId);
            int.TryParse(ddlSport.SelectedValue, out cl.SportId);
            int.TryParse(ddlDivision.SelectedValue, out cl.DivisionId);

            if (cl.DivisionId > 0)
            {
                //Check if this Division Teams are in Competition team table or not
                //if no entry then
                // Insert Selected Division Teams into Competition Teams

            }

            int.TryParse(txtNoOfGroup.Text, out cl.NumberofGroups);
            int.TryParse(txtNoOfTeam.Text, out cl.NumberofTeams);

            cl.StartDate = (txtStartDate.Text != "" ? Convert.ToDateTime(txtStartDate.Text, new System.Globalization.CultureInfo("en-GB")) : DateTime.MinValue);
            cl.EndDate = (txtEndDate.Text != "" ? Convert.ToDateTime(txtEndDate.Text, new System.Globalization.CultureInfo("en-GB")) : DateTime.MinValue);

            if (CompetitionLogoFile.PostedFile.FileName == "")
            {
                //DataTable dt1 = new DataTable();
                //cl.CompetitionId = Convert.ToInt32(hdnCompetitionID.Value);
                //dt1 = clc.GetCompetitionDetailByCompetitionID(cl.CompetitionId);
                //CompetitionLogoImage.ImageUrl = dt1.Rows[0]["CompetitionLogoFile"].ToString();
                //string ufname = dt1.Rows[0]["CompetitionLogoFile"].ToString().Replace(" ", "");
                //CompetitionLogoFile.ResolveUrl("ufname");
                //cl.CompetitionLogoFile = ufname.Replace(" ", "");

                cl.CompetitionLogoFile = CompetitionLogoImage.ImageUrl;
                FileOKForUpdate = true;
            }
            else
            {

                cl.CompetitionLogoFile = imhpathDB + CompetitionLogoFile.PostedFile.FileName.Replace(" ", "");

                if (CompetitionLogoFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(CompetitionLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

                if (!string.IsNullOrEmpty(CompetitionLogoFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                        return;
                    }
                }

                if (FileOK)
                {
                    if (CompetitionLogoFile.PostedFile.ContentLength > 10485760)
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
                        CompetitionLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + CompetitionLogoFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
            }

            cl.ActiveFlagId = Convert.ToInt32(ChkIsActive.Checked);
            cl.ShowFlagId = Convert.ToInt32(ChkIsShow.Checked);
            cl.PortalID = PortalId;
            cl.ModifiedById = currentUser.Username;

            // Call Update Method
            clc.UpdateCompetition(cl);

            btnAddCompetition.Visible = true;
            pnlCompetitionGrid.Visible = true;
            btnSaveCompetition.Visible = true;
            btnUpdateCompetition.Visible = false;
            LoadCompetitionGrid();
            ClearData();
        }

        protected void btnCancelCompetition_Click(object sender, EventArgs e)
        {
            pnlCompetitionGrid.Visible = true;
            pnlCompetitionEntry.Visible = false;
            btnSaveCompetition.Visible = false;
            btnUpdateCompetition.Visible = false;
            LoadCompetitionGrid();
            ClearData();
        }

        public void ClearData()
        {
            txtCompetition.Text = "";
            txtCompetitionDesc.Text = "";
            txtCompetitionAbbr.Text = "";
            txtCompetitionLogoName.Text = "";
            ddlSeason.ClearSelection();
            ddlSport.ClearSelection();
            ddlDivision.ClearSelection();
            ddlCompetitionLeague.ClearSelection();
            ddlCompetitionType.ClearSelection();
            ddlCompetitionFormat.ClearSelection();
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtNoOfGroup.Text = "";
            txtNoOfTeam.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
        }

        protected void btnAddCompetition_Click(object sender, EventArgs e)
        {
            pnlCompetitionGrid.Visible = false;
            pnlCompetitionEntry.Visible = true;
            btnSaveCompetition.Visible = true;
            btnUpdateCompetition.Visible = false;
            ClearData();
        }

        #endregion Button Click Events

        #region Gridview Events

        protected void gvCompetition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCompetition.PageIndex = e.NewPageIndex;
            LoadCompetitionGrid();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnCompetitionID.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdn_Competition_Id")).Value;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                ClearData();
                DataTable dt1 = clc.GetCompetitionDetailByCompetitionID(competitionID);

                if (dt1.Rows.Count > 0)
                {
                    txtCompetition.Text = dt1.Rows[0]["CompetitionName"].ToString();
                    txtCompetitionDesc.Text = dt1.Rows[0]["CompetitionDesc"].ToString();
                    txtCompetitionAbbr.Text = dt1.Rows[0]["CompetitionAbbr"].ToString();

                    txtNoOfGroup.Text = dt1.Rows[0]["NumberofGroups"].ToString();
                    txtNoOfTeam.Text = dt1.Rows[0]["NumberofTeams"].ToString();
                    ddlSeason.SelectedValue = dt1.Rows[0]["SeasonId"].ToString();
                    ddlCompetitionLeague.SelectedValue = dt1.Rows[0]["CompetitionLeagueId"].ToString();
                    ddlSport.SelectedValue = dt1.Rows[0]["SportId"].ToString();
                    ddlCompetitionType.SelectedValue = dt1.Rows[0]["CompetitionTypeId"].ToString();
                    ddlDivision.SelectedValue = dt1.Rows[0]["DivisionId"].ToString();
                    ddlCompetitionFormat.SelectedValue = dt1.Rows[0]["CompetitionFormatId"].ToString();

                    DateTime startDate = new DateTime();
                    DateTime endDate = new DateTime();

                    DateTime.TryParse(dt1.Rows[0]["StartDate"].ToString(), out startDate);
                    DateTime.TryParse(dt1.Rows[0]["EndDate"].ToString(), out endDate);

                    txtStartDate.Text = startDate.ToString("yyyy/MM/dd HH':'mm");
                    txtEndDate.Text = endDate.ToString("yyyy/MM/dd HH':'mm");

                    txtCompetitionLogoName.Text = dt1.Rows[0]["CompetitionLogoName"].ToString();
                    CompetitionLogoImage.ImageUrl = dt1.Rows[0]["CompetitionLogoFile"].ToString();

                    string ufname = dt1.Rows[0]["CompetitionLogoFile"].ToString().Replace(" ", "");
                    CompetitionLogoImage.ResolveUrl("ufname");

                    if (dt1.Rows[0]["ActiveFlagId"].ToString() == "1")
                    {
                        ChkIsActive.Checked = true;
                    }
                    if (dt1.Rows[0]["ShowFlagId"].ToString() == "1")
                    {
                        ChkIsShow.Checked = true;
                    }
                }

                btnUpdateCompetition.Visible = true;
                btnSaveCompetition.Visible = false;
                pnlCompetitionEntry.Visible = true;
                pnlCompetitionGrid.Visible = false;
            }
            else if (ddlSelectedValue == "Group")
            {
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmCompetitionGroup", "CompetitionID=" + competitionID));
            }
            else if (ddlSelectedValue == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);
                clc.DeleteCompetition(competitionID);
                LoadCompetitionGrid();
            }
            else if (ddlSelectedValue == "Team")
            {
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmCompetitionTeam", "CompetitionID=" + competitionID));
            }
        }

        #endregion
    }
}