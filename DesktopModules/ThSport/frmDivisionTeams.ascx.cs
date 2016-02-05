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
    public partial class frmDivisionTeams : PortalModuleBase
    {

        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\Team-Logos\\";
        public string imhpathDB = "Images\\Team-Logos\\";

        clsDivisionController dvController = new clsDivisionController();
        
        clsDivisionTeams dtClass = new clsDivisionTeams();
        clsDivisionTeamController dtController = new clsDivisionTeamController();

        clsTeamController teamControl = new clsTeamController();
        //clsMatchController matchControl = new clsMatchController();

        int IsNationalTeam = 0;
        clsTeamPlayer tpClass = new clsTeamPlayer();
        clsTeamPlayerController tpControl = new clsTeamPlayerController();

        public int master_teamId = 0;

        //int SportStageValue = 5;

        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Variables

        int DivisionID
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["DivisionID"] != null))
                {
                    int.TryParse(Request.QueryString["DivisionID"].ToString(), out retVal);
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

        int Division_team_detail_id
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdn_DivisionTeamDetailID.Value))
                {
                    int.TryParse(hdn_DivisionTeamDetailID.Value, out id);
                }
                return id;
            }
        }


        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            FillSearchFilter();
            FillDivisionTeamGrid();

            if (!Page.IsPostBack)
            {
                FillTeams();
                //FillGroupOrDivision();
                if (page_index != 0)
                {
                    grid_Teams.PageIndex = page_index;
                }
            }
        }

        //protected void ddlSelectDivision_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int compValue = Convert.ToInt32(ddlSelectDivision.SelectedValue);

        //    DataTable dt = new DataTable();
        //    dt = CRC.GetSelectGroupDivisionByDivisionID(compValue);
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
        //            dt = CRC.GetGroupNameIDBySelectDivisionIdD(compValue);
        //            if (dt.Rows.Count > 0)
        //            {
        //                pnlTeamEntry.Visible = true;
        //                pnlTeamList.Visible = false;
        //                ddlGroupName.DataSource = dt;
        //                ddlGroupName.DataTextField = "DivisionGroupName";
        //                ddlGroupName.DataValueField = "DivisionGroupID";
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
        //            dt = CRC.GetGroupNameIDBySelectDivisionIdD(compValue);
        //            if (dt.Rows.Count > 0)
        //            {

        //                pnlTeamEntry.Visible = true;
        //                pnlTeamList.Visible = false;
        //                ddlGroupName.DataSource = dt;
        //                ddlGroupName.DataTextField = "DivisionGroupName";
        //                ddlGroupName.DataValueField = "DivisionGroupID";
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

        

        //protected void ddlTeamSearch_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillDivisionTeamGrid();
        //}

        #endregion

        #region Methods

        //private void FillGroupOrDivision()
        //{
        //    using (DataTable dt = cgController.GetDivisionGroupList(DivisionID))
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlGroupName.DataSource = dt;
        //            ddlGroupName.DataTextField = "DivisionGroupName";
        //            ddlGroupName.DataValueField = "DivisionGroupID";
        //            ddlGroupName.DataBind();
        //            ddlGroupName.Items.Insert(0, new ListItem("-- Select Group --", "0"));
        //            lblGroupName.Text = "Group Name :";
        //        }
        //        else
        //        {
        //            ddlGroupName.Visible = false;
        //            lblGroupName.Visible = false;
        //            divgnvalidationred.Visible = false;
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(Request.QueryString["DivisionGroupID"]))
        //    {
        //        lblGroupName.Visible = true;
        //        ddlGroupName.Visible = true;
        //        divgnvalidationred.Visible = true;
        //        ddlGroupName.SelectedValue = Request.QueryString["DivisionGroupID"];
        //        pnlTeamEntry.Visible = true;
        //        pnlTeamList.Visible = false;
        //    }
        //}

        private void FillTeams()
        {
            DataTable dt = new DataTable();
            dt = teamControl.GetMasterTeamsNotInDivisionTeam(DivisionID);

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

            DataTable DivisionDetails = dvController.GetDivisionByDivisionID(DivisionID);
            if (DivisionDetails.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(DivisionDetails.Rows[0]["DivisionName"].ToString()))
                {
                    txtDivisionName.Text = DivisionDetails.Rows[0]["DivisionName"].ToString();
                    lbl_Division_Name.Text = DivisionDetails.Rows[0]["DivisionName"].ToString();
                }
            }
        }

        private void FillSearchFilter()
        {
            
            using (DataTable team_tbl = dtController.GetTeamsByDivisionID(DivisionID))
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

        private void FillDivisionTeamGrid()
        {
            int Searched_DivisionID = 0;
            int Searched_TeamID = 0;

            int.TryParse(ddlTeamSearch.SelectedValue, out Searched_TeamID);

            using (DataTable dt = dtController.GetDivisionTeamsByUser(currentUser.Username, DivisionID, Searched_TeamID))
            {
                if (dt.Rows.Count > 0)
                {
                    grid_Teams.DataSource = dt;
                    grid_Teams.DataBind();
                }
            }
        }

        //private void fillTeamsGridWithDivisionType(string Division_type)
        //{
        //    TeamsController tm = new TeamsController();
        //    DataTable dt = new DataTable();
        //    dt = tm.GetAllTeamsWithDivisionType(currentUser.Username, Division_type);


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

            #region Add Selected Teams In Division Team Table

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
                    
                    dtClass.DivisionId = DivisionID;

                    int MasterID = 0;
                    int.TryParse(hdnTeamMasterID.Value, out MasterID);

                    dtClass.TeamId = MasterID;
                    dtClass.CreatedById = currentUser.Username;
                    dtClass.ModifiedById = currentUser.Username;
                    dtController.InsertDivisionTeam(dtClass);
                }
            }

            #endregion

            FillDivisionTeamGrid();
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
            
            ddlTeamSearch.Items.Clear();
            ddlTeamSearch.Items.Insert(0, new ListItem("Select Team", "0"));

            FillDivisionTeamGrid();
        }


        protected void btnTeamSaveAndAddTeam_Click(object sender, EventArgs e)
        {
            SaveTeam();
            FillTeams();
            pnlTeamList.Visible = false;
            pnlTeamEntry.Visible = true;
            //ddlSelectDivision.SelectedValue = "0";
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
            //ddlSelectDivision.SelectedValue = "0";
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
            //hdn_DivisionTeamID.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdnDivisionTeamID")).Value;

            //int team_MasterID = 0;

            //int.TryParse(((HiddenField)((DropDownList)sender).Parent.FindControl("hdnTeamID")).Value, out team_MasterID);

            //string ddlSelectedValue = ((DropDownList)sender).SelectedValue;
            //if (ddlSelectedValue == "Delete")
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);

            //    ctController.DeleteDivisionTeam(Division_team_detail_id);
            //    FillDivisionTeamGrid();
            //}
        }

        #endregion

        protected void btnGoToDivision_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmDivision"));
        }
    }
}