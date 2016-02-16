using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
    public partial class frmCompetitionMatch : PortalModuleBase
    {

        clsCompetitionMatch cmClass = new clsCompetitionMatch();
        clsCompetitionMatchController cmController = new clsCompetitionMatchController();

        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region variables

        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;
       
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

        int CompetitionMatchID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdnCompetitionMatchID.Value))
                {
                    int.TryParse(hdnCompetitionMatchID.Value, out id);
                }
                return id;
            }
        }
        #endregion variables

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            

           LoadCompetitionMatchGrid();

            if (IsPostBack)
            {
                //btnUpdateCompetitionMatch.Visible = false;
                //btnSaveCompetitionMatch.Visible = false;
               
            }

        }
        #endregion Page Events

        #region Methods

        public void LoadCompetitionMatchGrid()
        {
            DataTable dt = new DataTable();
            dt = cmController.GetCompetitionMatchList(CompetitionID);

            gvCompetitionMatch.DataSource = dt;
            gvCompetitionMatch.DataBind();
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveCompetitionMatch_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            cmClass.CompetitionId = CompetitionID;
           // cmClass.PortalId = currentUser.PortalId;
            if (ddlLocation.SelectedValue == "")
            {
                cmClass.LocationID = 0;
            }
            else
            {
                cmClass.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            }
            if (ddlTeamA.SelectedValue == "")
            {
                cmClass.TeamAId = 0;
            }
            else
            {
                cmClass.TeamAId = Convert.ToInt32(ddlTeamA.SelectedValue);
            }
            if (ddlTeamB.SelectedValue == "")
            {
                cmClass.TeamBId = 0;
            }
            else
            {
                cmClass.TeamBId = Convert.ToInt32(ddlTeamB.SelectedValue);
            }
            if (ddlMatchStatus.SelectedValue == "")
            {
                cmClass.MatchStatusId = 0;
            }
            else
            {
                cmClass.MatchStatusId = Convert.ToInt32(ddlMatchStatus.SelectedValue);
            }
            if (ddlMatchType.SelectedValue == "")
            {
                cmClass.MatchTypeId = 0;
            }
            else
            {
                cmClass.MatchTypeId = Convert.ToInt32(ddlMatchType.SelectedValue);
            }
            if (ChkIsFinalized.Checked)
            {
                cmClass.IsFinalized = 1;
            }
            else
            {
                cmClass.IsFinalized = 0;
            }
            cmClass.PortalId = currentUser.PortalID;
            cmClass.CreatedById = currentUser.UserID;
           cmClass.ModifiedById = currentUser.UserID;

           cmClass.StartDateTime = (txtMatchStartDate.Text != "" ? Convert.ToDateTime(txtMatchStartDate.Text, new System.Globalization.CultureInfo("en-GB")) : DateTime.MinValue);
           cmClass.EndDateTime = (txtMatchEndDate.Text != "" ? Convert.ToDateTime(txtMatchEndDate.Text, new System.Globalization.CultureInfo("en-GB")) : DateTime.MinValue);
          // Call Save Method
           cmController.InsertCompetitionMatch(cmClass);

            btnAddCompetitionMatch.Visible = true;
            pnlCompetitionMatchGrid.Visible = true;
            pnlCompetitionMatchEntry.Visible = false;
           LoadCompetitionMatchGrid();
            ClearData();

        }

        protected void btnUpdateCompetitionMatch_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

          

            cmClass.CompetitionId = CompetitionID;
           // cmClass.PortalId = currentUser.PortalId;
            if (ddlLocation.SelectedValue == "")
            {
                cmClass.LocationID = 0;
            }
            else
            {
                cmClass.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            }
            if (ddlTeamA.SelectedValue == "")
            {
                cmClass.TeamAId = 0;
            }
            else
            {
                cmClass.TeamAId = Convert.ToInt32(ddlTeamA.SelectedValue);
            }
            if (ddlTeamB.SelectedValue == "")
            {
                cmClass.TeamBId = 0;
            }
            else
            {
                cmClass.TeamBId = Convert.ToInt32(ddlTeamB.SelectedValue);
            }
            if (ddlMatchStatus.SelectedValue == "")
            {
                cmClass.MatchStatusId = 0;
            }
            else
            {
                cmClass.MatchStatusId = Convert.ToInt32(ddlMatchStatus.SelectedValue);
            }
            if (ddlMatchType.SelectedValue == "")
            {
                cmClass.MatchTypeId = 0;
            }
            else
            {
                cmClass.MatchTypeId = Convert.ToInt32(ddlMatchType.SelectedValue);
            }
            if (ChkIsFinalized.Checked)
            {
                cmClass.IsFinalized = 1;
            }
            else
            {
                cmClass.IsFinalized = 0;
            }
            cmClass.PortalId = currentUser.PortalID;
            cmClass.CreatedById = currentUser.UserID;
           cmClass.ModifiedById = currentUser.UserID;
           cmClass.CompetitionMatchId = CompetitionMatchID;
           cmClass.StartDateTime = (txtMatchStartDate.Text != "" ? Convert.ToDateTime(txtMatchStartDate.Text, new System.Globalization.CultureInfo("en-GB")) : DateTime.MinValue);
           cmClass.EndDateTime = (txtMatchEndDate.Text != "" ? Convert.ToDateTime(txtMatchEndDate.Text, new System.Globalization.CultureInfo("en-GB")) : DateTime.MinValue);
            //// Call Update Method
           cmController.UpdateCompetitionMatch(cmClass);

            btnAddCompetitionMatch.Visible = true;
            pnlCompetitionMatchGrid.Visible = true;
            pnlCompetitionMatchEntry.Visible = false;
            btnSaveCompetitionMatch.Visible = true;
            btnUpdateCompetitionMatch.Visible = false;
            LoadCompetitionMatchGrid();
            ClearData();
        }

        protected void btnCancelCompetitionMatch_Click(object sender, EventArgs e)
        {
            pnlCompetitionMatchGrid.Visible = true;
            pnlCompetitionMatchEntry.Visible = false;
            btnSaveCompetitionMatch.Visible = false;
            btnUpdateCompetitionMatch.Visible = false;
            LoadCompetitionMatchGrid();
            ClearData();
        }

        public void ClearData()
        {
            ddlLocation.SelectedIndex = 0;
            ddlMatchStatus.SelectedIndex = 0;
            ddlMatchType.SelectedIndex = 0;
            ddlTeamA.SelectedIndex = 0;
            //ddlTeamB.SelectedIndex = 0;
            txtMatchStartDate.Text = "";
            txtMatchEndDate.Text = "";
            ChkIsFinalized.Checked = false;
        }

        protected void btnAddCompetitionMatch_Click(object sender, EventArgs e)
        {
            pnlCompetitionMatchGrid.Visible = false;
            pnlCompetitionMatchEntry.Visible = true;
            btnSaveCompetitionMatch.Visible = true;
            btnUpdateCompetitionMatch.Visible = false;
            FillMatchStatus();
            FillMatchType();
            FillTeamA();
            FillLocation();
            ClearData();
        }

        protected void ddlTeamA_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTeamB();
            
        }
        

        #endregion Button Click Events

        #region Gridview Events

        protected void gvCompetitionMatch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCompetitionMatch.PageIndex = e.NewPageIndex;
            LoadCompetitionMatchGrid();
        }
        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnCompetitionMatchID.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdn_CompetitionMatch_Id")).Value;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
               
                FillMatchStatus();
                FillMatchType();
                FillTeamA();
                FillLocation();
                ClearData();
                DataTable dt1 = cmController.GetCompetitionMatchDetailByCompetitionGroupID(CompetitionMatchID);

                if (dt1.Rows.Count > 0)
                {
                 

                    ddlLocation.SelectedValue = dt1.Rows[0]["LocationID"].ToString();
                    ddlTeamA.SelectedValue = dt1.Rows[0]["TeamAID"].ToString();
                   
                    FillTeamB();
                    ddlTeamB.SelectedValue = dt1.Rows[0]["TeamBID"].ToString();
                    ddlMatchStatus.SelectedValue = dt1.Rows[0]["MatchStatusId"].ToString();
                    ddlMatchType.SelectedValue = dt1.Rows[0]["MatchTypeId"].ToString();
                   

                    DateTime startDate = new DateTime();
                    DateTime endDate = new DateTime();

                    DateTime.TryParse(dt1.Rows[0]["StartDateTime"].ToString(), out startDate);
                    DateTime.TryParse(dt1.Rows[0]["EndDateTime"].ToString(), out endDate);

                    txtMatchStartDate.Text = startDate.ToString("yyyy/MM/dd HH':'mm");
                    txtMatchEndDate.Text = endDate.ToString("yyyy/MM/dd HH':'mm");

                    if (dt1.Rows[0]["IsFinalized"].ToString() == "1")
                    {
                        ChkIsFinalized.Checked = true;
                    }
                }

                btnUpdateCompetitionMatch.Visible = true;
                btnSaveCompetitionMatch.Visible = false;
                pnlCompetitionMatchEntry.Visible = true;
                pnlCompetitionMatchGrid.Visible = false;
            }
            else if (ddlSelectedValue == "Result")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);
                //cgController.DeleteCompetitionGroup(CompetitionGroupID);
                //LoadCompetitionmatchGrid();

                pnlCompetitionMatchGrid.Visible = false;
               pnlCompetitionMatchEntry.Visible= false;
                //hidRegID.Value = str;
                //int id = Convert.ToInt32(hidRegID.Value);
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmMatchResult", "MatchID=" + CompetitionMatchID ));
            }
            else if (ddlSelectedValue == "MatchRating")
            {
                pnlCompetitionMatchGrid.Visible = false;
                pnlCompetitionMatchEntry.Visible = false;
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmMatchRating", "MatchID=" + CompetitionMatchID));
            }
        }
        private void FillTeamB()
        {
            clsTeam e = new clsTeam();
            clsTeamController ec = new clsTeamController();
            DataTable dt = new DataTable();

            dt = ec.GetTeamDetailByNotInTeamID(Convert.ToInt32(ddlTeamA.SelectedValue.ToString()));

            if (dt.Rows.Count > 0)
            {
                ddlTeamB.DataSource = dt;
                ddlTeamB.DataTextField = "TeamName";
                ddlTeamB.DataValueField = "TeamId";
                ddlTeamB.DataBind();
                ddlTeamB.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillTeamA()
        {
            clsTeam e = new clsTeam();
            clsTeamController ec = new clsTeamController();
            DataTable dt = new DataTable();

            dt = ec.GetTeamList();
            if (dt.Rows.Count > 0)
            {
                ddlTeamA.DataSource = dt;
                ddlTeamA.DataTextField = "TeamName";
                ddlTeamA.DataValueField = "TeamId";
                ddlTeamA.DataBind();
                ddlTeamA.Items.Insert(0, new ListItem("-- Select --", "0"));
                ddlTeamB.Items.Clear();
                //LstTeam = (from DataRow row in dt.Rows

                //           select new Team
                //           {
                //               TeamID = Convert.ToInt32(row["TeamId"]),
                //               TeamName = row["TeamName"].ToString()

                //           }).ToList();
                //ddlTeamB.DataSource = dt;
                //ddlTeamB.DataTextField = "TeamName";
                //ddlTeamB.DataValueField = "TeamId";
                //ddlTeamB.DataBind();
                //ddlTeamB.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillMatchStatus()
        {
            clsMatchStatus e = new clsMatchStatus();
            clsMatchStatusController ec = new clsMatchStatusController();
            DataTable dt = new DataTable();

            dt = ec.GetAllMatchStatus();
            if (dt.Rows.Count > 0)
            {
                ddlMatchStatus.DataSource = dt;
                ddlMatchStatus.DataTextField = "MatchStatusName";
                ddlMatchStatus.DataValueField = "MatchStatusId";
                ddlMatchStatus.DataBind();
                ddlMatchStatus.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillMatchType()
        {
            clsMatchType e = new clsMatchType();
            clsMatchTypeController ec = new clsMatchTypeController();
            DataTable dt = new DataTable();

            dt = ec.GetAllMatchType();
            if (dt.Rows.Count > 0)
            {
                ddlMatchType.DataSource = dt;
                ddlMatchType.DataTextField = "MatchTypeName";
                ddlMatchType.DataValueField = "MatchTypeId";
                ddlMatchType.DataBind();
                ddlMatchType.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillLocation()
        {
            clsLocation e = new clsLocation();
            clsLocationController ec = new clsLocationController();
            DataTable dt = new DataTable();

            dt = ec.FetchAllLocation(currentUser.Username,"","");
            if (dt.Rows.Count > 0)
            {
                ddlLocation.DataSource = dt;
                ddlLocation.DataTextField = "Loc_LocationName";
                ddlLocation.DataValueField = "Loc_LocationID";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }


        #endregion Gridview Events


    }
}
