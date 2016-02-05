using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using ThSportServer;
using System.Data;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Modules;
using System.IO;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Security.Membership;
using DotNetNuke.Common.Utilities;
using System.Collections;

namespace DotNetNuke.Modules.ThSport
{
    public partial class frmCompetitionTeam : PortalModuleBase
    {
        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\Team-Logos\\";
        public string imhpathDB = "Images\\Team-Logos\\";

        clsCompetitionController cController = new clsCompetitionController();
        clsCompetitionGroupController cgController = new clsCompetitionGroupController();

        clsCompetitionTeam ctClass = new clsCompetitionTeam();
        clsCompetitionTeamController ctController = new clsCompetitionTeamController();

        clsTeamController teamControl = new clsTeamController();
        //clsMatchController matchControl = new clsMatchController();

        int IsNationalTeam = 0;
        clsTeamPlayer tpClass = new clsTeamPlayer();
        clsTeamPlayerController tpControl = new clsTeamPlayerController();

        public int master_teamId = 0;

        //int SportStageValue = 5;

        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Variables

        int CompetitionID
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["CompetitionID"] != null))
                {
                    int.TryParse(Request.QueryString["CompetitionID"].ToString(), out retVal);
                    return retVal;
                }
                return 0;
            }
        }

        int page_index
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["page_index"] != null))
                {
                    int.TryParse(Request.QueryString["page_index"].ToString(), out retVal);
                    return retVal;
                }
                return 0;
            }
            set
            {
                ViewState["grid_page_index"] = value;
            }
        }

        int competition_team_id
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdn_CompetitionTeamID.Value))
                {
                    int.TryParse(hdn_CompetitionTeamID.Value, out id);
                }
                return id;
            }
        }


        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            FillSearchFilter();
            FillCompetitionTeamGrid();

            if (!Page.IsPostBack)
            {
                FillTeams();
                FillGroupOrDivision();
                if (page_index != 0)
                {
                    grid_Teams.PageIndex = page_index;
                }
            }
        }

        //protected void ddlSelectCompetition_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int compValue = Convert.ToInt32(ddlSelectCompetition.SelectedValue);

        //    DataTable dt = new DataTable();
        //    dt = CRC.GetSelectGroupDivisionByCompetitionID(compValue);
        //    if (dt.Rows.Count > 0)
        //    {
        //        string GroupAndDivision = dt.Rows[0]["InGroup"].ToString();
        //        if (string.IsNullOrEmpty(GroupAndDivision))
        //        {
        //            ddlGroupName.Visible = false;
        //            lblGroupName.Visible = false;
        //        }

        //        if (GroupAndDivision == "Group")
        //        {
        //            dt = CRC.GetGroupNameIDBySelectCompetitionIdD(compValue);
        //            if (dt.Rows.Count > 0)
        //            {
        //                pnlTeamEntry.Visible = true;
        //                pnlTeamList.Visible = false;
        //                ddlGroupName.DataSource = dt;
        //                ddlGroupName.DataTextField = "CompetitionGroupName";
        //                ddlGroupName.DataValueField = "CompetitionGroupID";
        //                ddlGroupName.DataBind();
        //                ddlGroupName.Items.Insert(0, new ListItem("-- Select Group --", "0"));
        //                lblGroupName.Text = "Group Name :";
        //            }
        //            else
        //            {
        //                lblGroupName.Visible = false;
        //                ddlGroupName.Visible = false;
        //            }
        //        }
        //        else if (GroupAndDivision == "Division")
        //        {
        //            dt = CRC.GetGroupNameIDBySelectCompetitionIdD(compValue);
        //            if (dt.Rows.Count > 0)
        //            {

        //                pnlTeamEntry.Visible = true;
        //                pnlTeamList.Visible = false;
        //                ddlGroupName.DataSource = dt;
        //                ddlGroupName.DataTextField = "CompetitionGroupName";
        //                ddlGroupName.DataValueField = "CompetitionGroupID";
        //                ddlGroupName.DataBind();
        //                ddlGroupName.Items.Insert(0, new ListItem("-- Select Division --", "0"));
        //                lblGroupName.Text = "Division Name :";
        //            }
        //            else
        //            {
        //                lblGroupName.Visible = false;
        //                ddlGroupName.Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            lblGroupName.Visible = false;
        //            ddlGroupName.Visible = false;
        //            pnlTeamEntry.Visible = true;
        //            pnlTeamList.Visible = false;
        //        }
        //    }
        //    else
        //    {
        //        lblGroupName.Visible = false;
        //        ddlGroupName.Visible = false;
        //        pnlTeamEntry.Visible = true;
        //        pnlTeamList.Visible = false;

        //    }
        //}

        protected void ddlCompetitionGroupSearch_SelectedIndexChanged(object sender, EventArgs e)
        {  
            int selected_groupid = 0;
            int.TryParse(ddlCompetitionGroupSearch.SelectedValue, out selected_groupid);

            using (DataTable dt = ctController.GetAllTeamsByUser(currentUser.Username, CompetitionID, selected_groupid, 0))
            {
                if (dt.Rows.Count > 0)
                {
                    ddlTeamSearch.DataSource = dt;
                    ddlTeamSearch.DataTextField = "TeamName";
                    ddlTeamSearch.DataValueField = "TeamID";
                    ddlTeamSearch.DataBind();
                    ddlTeamSearch.Items.Insert(0, new ListItem("Select Team", "0"));
                }
            }

            FillCompetitionTeamGrid();
        }

        protected void ddlTeamSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCompetitionTeamGrid();
        }

        #endregion

        #region Methods

        private void FillGroupOrDivision()
        {
            using (DataTable dt = cgController.GetCompetitionGroupList(CompetitionID))
            {
                if (dt.Rows.Count > 0)
                {
                    ddlGroupName.DataSource = dt;
                    ddlGroupName.DataTextField = "CompetitionGroupName";
                    ddlGroupName.DataValueField = "CompetitionGroupID";
                    ddlGroupName.DataBind();
                    ddlGroupName.Items.Insert(0, new ListItem("-- Select Group --", "0"));
                    lblGroupName.Text = "Group Name :";
                }
                else
                {
                    ddlGroupName.Visible = false;
                    lblGroupName.Visible = false;
                    divgnvalidationred.Visible = false;
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["CompetitionGroupID"]))
            {
                lblGroupName.Visible = true;
                ddlGroupName.Visible = true;
                divgnvalidationred.Visible = true;
                ddlGroupName.SelectedValue = Request.QueryString["CompetitionGroupID"];
                pnlTeamEntry.Visible = true;
                pnlTeamList.Visible = false;
            }
        }

        private void FillTeams()
        {
            DataTable dt = new DataTable();
            dt = teamControl.GetMasterTeamsNotInCompetitionTeam(CompetitionID);

            if (dt.Rows.Count > 0)
            {
                rptrForTeams.Visible = true;
                rptrForTeams.DataSource = dt;
                rptrForTeams.DataBind();
            }
            else
            {
                rptrForTeams.Visible = false;
            }

            DataTable CompetitionDetails = cController.GetCompetitionDetailByCompetitionID(CompetitionID);
            if (CompetitionDetails.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(CompetitionDetails.Rows[0]["CompetitionName"].ToString()))
                {
                    txtCompetitionName.Text = CompetitionDetails.Rows[0]["CompetitionName"].ToString();
                    lbl_Competition_Name.Text = CompetitionDetails.Rows[0]["CompetitionName"].ToString();
                }
            }
        }

        private void FillSearchFilter()
        {
            using (DataTable dt = cgController.GetCompetitionGroupList(CompetitionID))
            {
                if (dt.Rows.Count > 0)
                {
                    ddlCompetitionGroupSearch.DataSource = dt;
                    ddlCompetitionGroupSearch.DataTextField = "CompetitionGroupName";
                    ddlCompetitionGroupSearch.DataValueField = "CompetitionGroupID";
                    ddlCompetitionGroupSearch.DataBind();
                    ddlCompetitionGroupSearch.Items.Insert(0, new ListItem("-- Select Group --", "0"));
                }
            }

            using (DataTable team_tbl = ctController.GetTeamsByCompetitionID(CompetitionID))
            {
                if (team_tbl.Rows.Count > 0)
                {
                    ddlTeamSearch.DataSource = team_tbl;
                    ddlTeamSearch.DataTextField = "TeamName";
                    ddlTeamSearch.DataValueField = "TeamID";
                    ddlTeamSearch.DataBind();
                    ddlTeamSearch.Items.Insert(0, new ListItem("-- Select Team --", "0"));
                }
            }
        }

        private void FillCompetitionTeamGrid()
        {
            int Searched_CompetitionID = 0;
            int Searched_CompetitionGroupID = 0;
            int Searched_TeamID = 0;

            int.TryParse(ddlCompetitionGroupSearch.SelectedValue, out Searched_CompetitionGroupID);
            int.TryParse(ddlTeamSearch.SelectedValue, out Searched_TeamID);

            using (DataTable dt = ctController.GetAllTeamsByUser(currentUser.Username, CompetitionID, Searched_CompetitionGroupID, Searched_TeamID))
            {
                if (dt.Rows.Count > 0)
                {
                    grid_Teams.DataSource = dt;
                    grid_Teams.DataBind();
                }
            }
        }

        //private void fillTeamsGridWithCompetitionType(string competition_type)
        //{
        //    TeamsController tm = new TeamsController();
        //    DataTable dt = new DataTable();
        //    dt = tm.GetAllTeamsWithCompetitionType(currentUser.Username, competition_type);


        //    int teamId = 0;
        //    int.TryParse(ddlTeamSearch.SelectedValue.ToString(), out teamId);
        //    if (teamId != null && teamId > 0)
        //    {
        //        DataView dv = new DataView();
        //        dv = dt.AsDataView();
        //        dv.RowFilter = " TeamID = " + teamId;
        //        dt = dv.ToTable();
        //    }

        //    grid_Teams.DataSource = dt;
        //    grid_Teams.DataBind();

        //}

        private void SaveTeam()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            #region Add Selected Teams In Competition Team Table

            foreach (RepeaterItem i in rptrForTeams.Items)
            {
                HiddenField hdnTeamMasterID = (HiddenField)i.FindControl("hdnTeamID");
                CheckBox chk_Assign_team = (CheckBox)i.FindControl("chk_Assign_team");

                if (chk_Assign_team.Checked)
                {
                    Boolean FileOK = false;
                    Boolean FileSaved = false;
                    int group_id = 0;
                    if (ddlGroupName.SelectedIndex > 0)
                    {
                        int.TryParse(ddlGroupName.SelectedValue, out group_id);
                    }
                    ctClass.CompetitionGroupId = group_id;
                    ctClass.CompetitionId = CompetitionID;

                    int MasterID = 0;
                    int.TryParse(hdnTeamMasterID.Value, out MasterID);

                    ctClass.TeamId = MasterID;
                    ctClass.CreatedById = currentUser.Username;
                    ctClass.ModifiedById = currentUser.Username;
                    ctController.InsertCompetitionTeam(ctClass);
                }
            }

            #endregion

            FillCompetitionTeamGrid();
        }

        #endregion

        #region Button Click Events

        protected void btnTeamSave_Click(object sender, EventArgs e)
        {
            SaveTeam();

            pnlTeamEntry.Visible = false;
            pnlTeamList.Visible = true;

            Response.Redirect(Request.RawUrl);
        }

        protected void btnTeamsClearFilter_Click(object sender, EventArgs e)
        {
            ddlCompetitionGroupSearch.SelectedIndex = 0;
            ddlTeamSearch.Items.Clear();
            ddlTeamSearch.Items.Insert(0, new ListItem("Select Team", "0"));

            FillCompetitionTeamGrid();
        }


        protected void btnTeamSaveAndAddTeam_Click(object sender, EventArgs e)
        {
            SaveTeam();
            FillTeams();
            pnlTeamList.Visible = false;
            pnlTeamEntry.Visible = true;
            //ddlSelectCompetition.SelectedValue = "0";
            ddlGroupName.SelectedValue = "0";
            ddlTeamSearch.SelectedValue = "0";
            Response.Redirect(Request.RawUrl);
        }

        protected void btnAddTeam_Click(object sender, EventArgs e)
        {
            pnlTeamList.Visible = false;
            pnlTeamEntry.Visible = true;
            btnTeamSave.Visible = true;
            btnTeamSaveAndAddTeam.Visible = true;
            //ddlSelectCompetition.SelectedValue = "0";
            ddlGroupName.SelectedValue = "0";
            FillTeams();
        }

        protected void btnTeamCancel_Click(object sender, EventArgs e)
        {
            pnlTeamList.Visible = true;
            pnlTeamEntry.Visible = false;
            FillTeams();
        }

        #endregion

        #region GridView Events

        protected void grid_Teams_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gridview = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(grid_Teams, "Edit$" + e.Row.RowIndex);
                //e.Row.Attributes["style"] = "cursor:pointer";
            }
        }

        protected void grid_Teams_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_Teams.PageIndex = e.NewPageIndex;
            FillTeams();
            pnlTeamList.Visible = true;

            ViewState["grid_page_index"] = grid_Teams.PageIndex;
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdn_CompetitionTeamID.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdnCompetitionTeamID")).Value;

            int team_MasterID = 0;

            int.TryParse(((HiddenField)((DropDownList)sender).Parent.FindControl("hdnTeamID")).Value, out team_MasterID);

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;
            if (ddlSelectedValue == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);

                ctController.DeleteCompetitionTeam(competition_team_id);
                FillCompetitionTeamGrid();

                /* Check if Match exists or not for this CompetitionTeam  */

                //DataTable dtforMatch = matchControl.FetchAllMatch(CompetitionID);

                //if (dtforMatch.Rows.Count == 0)
                //{
                //    //teamControl.DeleteTeam(team_Id);

                //    fillTeamGrid();

                //    if (ddlCompetitionGroupSearch.SelectedIndex > 0)
                //    {
                //        FillTeamDropdown(ddlCompetitionGroupSearch.SelectedValue);
                //    }
                //}
            }
        }

        #endregion

        protected void btnGoToCompetition_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmCompetition"));
        }

    }
}