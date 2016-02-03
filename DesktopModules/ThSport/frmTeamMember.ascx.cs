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
    public partial class frmTeamMember : PortalModuleBase
    {
        clsTeamMember ccm = new clsTeamMember();
        clsTeamMemberController ccmc = new clsTeamMemberController();

        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        string currentId
        {
            get
            {
                if (ViewState["currentId"] != null)
                    return ViewState["currentId"].ToString();
                return null;
            }
        }

        string regiId
        {
            get
            {
                if (ViewState["regiId"] != null)
                    return ViewState["regiId"].ToString();
                return null;
            }
        }

        #region variables

        int TeamID
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["TeamID"] != null))
                {
                    int.TryParse(Request.QueryString["TeamID"].ToString(), out retVal);
                    return retVal;
                }
                return 0;
            }
        }

        #endregion variables

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (TeamID == null || TeamID == 0) return;
            btnUpdateTeamMember.Visible = false;
            btnSaveTeamMember.Visible = false;
            pnlEntryTeamMember.Visible = false;

            if (TeamID != 0)
            {
                LoadDocumentsGrid(TeamID);
                //FillPlayerType();
                //FillPlayer();
                FillTeamName();
            }
        }

        private void FillTeamName()
        {
            clsTeamMember ccm = new clsTeamMember();
            clsTeamMemberController ccmc = new clsTeamMemberController();

            DataTable dt = new DataTable();
            dt = ccmc.GetTeamNameByTeamID(TeamID);

            if (dt.Rows.Count > 0)
            {
                lbl_Team_Member.Text = dt.Rows[0]["TeamName"].ToString();
            }
        }

        protected void btnUpdateTeamMember_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            clsTeamMember ccm = new clsTeamMember();
            clsTeamMemberController ccmc = new clsTeamMemberController();

            ccm.TeamMemberID = Convert.ToInt32(currentId);
            ccm.TeamID = TeamID;
            ccm.RegistrationId = Convert.ToInt32(regiId);
            ccm.TeamMemberTypeId = Convert.ToInt32(ddlTeamMemberType.SelectedValue);

            if (txtTeamMemberJerseyNo.Text == " ")
            {
                ccm.TeamMemberJerseyNo = 0;
            }
            else
            {
                ccm.TeamMemberJerseyNo = Convert.ToInt32(txtTeamMemberJerseyNo.Text);
            }

            ccm.TeamMemberJerseyName = txtTeamMemberJerseyName.Text.Trim();
            ccm.TeamMemberFamousName = txtTeamMemberFamousname.Text.Trim();

            ccm.PortalId = PortalId;
            ccm.ModifiedById = currentUser.Username;

            // Call Update Method
            ccmc.UpdateTeamMember(ccm);

            btnAddTeamMember.Visible = true;
            pnlGridTeamMember.Visible = true;
            btnSaveTeamMember.Visible = true;
            btnUpdateTeamMember.Visible = false;
            FillTeamName();
            LoadDocumentsGrid(TeamID);
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        #endregion Page Events

        #region Methods

        public void LoadDocumentsGrid(int TeamID)
        {
            DataTable dt = new DataTable();
            clsTeamMemberController ccmc = new clsTeamMemberController();

            dt = ccmc.GetTeamMemberListByTeamID(TeamID);

            if (dt.Rows.Count > 0)
            {
                gvTeamMember.DataSource = dt;
                gvTeamMember.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveTeamMember_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            clsTeamMember ccm = new clsTeamMember();
            clsTeamMemberController ccmc = new clsTeamMemberController();

            ccm.TeamID = TeamID;
            ccm.RegistrationId = Convert.ToInt32(ddlSelectMember.SelectedValue);

            if (txtTeamMemberJerseyNo.Text == " ")
            {
                ccm.TeamMemberJerseyNo = 0;
            }
            else
            {
                ccm.TeamMemberJerseyNo = Convert.ToInt32(txtTeamMemberJerseyNo.Text);
            }

            ccm.TeamMemberJerseyName = txtTeamMemberJerseyName.Text.Trim();
            ccm.TeamMemberFamousName = txtTeamMemberFamousname.Text.Trim();
            ccm.TeamMemberTypeId = Convert.ToInt32(ddlTeamMemberType.SelectedValue);
            ccm.PortalId = PortalId;
            ccm.CreatedById = currentUser.Username;
            ccm.ModifiedById = currentUser.Username;

            // Call Save Method
            ccmc.InsertTeamMember(ccm);

            btnAddTeamMember.Visible = true;
            pnlGridTeamMember.Visible = true;
            FillTeamName();
            LoadDocumentsGrid(TeamID);
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        protected void btnCancelTeamMember_Click(object sender, EventArgs e)
        {
            pnlGridTeamMember.Visible = true;
            pnlEntryTeamMember.Visible = false;
            btnSaveTeamMember.Visible = false;
            btnUpdateTeamMember.Visible = false;
            LoadDocumentsGrid(TeamID);
            ClearData();
        }

        #endregion Button Click Events

        protected void gvTeamMember_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTeamMember.PageIndex = e.NewPageIndex;
            LoadDocumentsGrid(TeamID);
        }

        protected void btnAddTeamMember_Click(object sender, EventArgs e)
        {
            pnlGridTeamMember.Visible = false;
            pnlEntryTeamMember.Visible = true;
            btnSaveTeamMember.Visible = true;
            btnUpdateTeamMember.Visible = false;
            FillTeamName();
            FillTeamMemberType();
            FillTeamMember();
            ClearData();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionTeamMemberId")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                FillTeamMember();
                ddlTeamMemberType.Enabled = false;
                int editid = 0;
                int.TryParse(str, out editid);
                ViewState["currentId"] = Convert.ToInt16(str);

                clsTeamMember ccm = new clsTeamMember();
                clsTeamMemberController ccmc = new clsTeamMemberController();

                DataTable dt = new DataTable();
                dt = ccmc.GetTeamNameByTeamID(TeamID);
                if (dt.Rows.Count > 0)
                {
                    lbl_Team_Member.Text = dt.Rows[0]["TeamName"].ToString();
                }

                FillTeamMemberType();

                ClearData();
                DataTable dt1 = new clsTeamMemberController().GetTeamMemberDetailByTeamMemberID(editid);

                if (dt1.Rows.Count > 0)
                {
                    ViewState["currentId"] = Convert.ToInt32(dt1.Rows[0]["TeamMemberID"].ToString());
                    ViewState["regiId"] = Convert.ToInt32(dt1.Rows[0]["RegistrationId"].ToString());
                    ddlSelectMember.SelectedValue = dt1.Rows[0]["RegistrationId"].ToString();
                    txtTeamMemberJerseyNo.Text = dt1.Rows[0]["TeamMemberJerseyNo"].ToString();
                    txtTeamMemberJerseyName.Text = dt1.Rows[0]["TeamMemberJerseyName"].ToString();
                    txtTeamMemberFamousname.Text = dt1.Rows[0]["TeamMemberFamousName"].ToString();
                    ddlTeamMemberType.SelectedValue = dt1.Rows[0]["TeamMemberTypeId"].ToString();
                }

                btnUpdateTeamMember.Visible = true;
                btnSaveTeamMember.Visible = false;
                pnlEntryTeamMember.Visible = true;
                pnlGridTeamMember.Visible = false;
            }
            else if (ddlSelectedValue == "Delete")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);
                //int documentid = 0;
                //int.TryParse(str, out documentid);
                //new CompetitionSponsorController().DeleteCompeSpon(documentid);
                //LoadDocumentsGrid(CompetitionID);
            }
        }

        protected void btnGoToBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmTeam"));
        }

        public void ClearData()
        {
            txtTeamMemberJerseyNo.Text = "";
            txtTeamMemberJerseyName.Text = "";
            txtTeamMemberFamousname.Text = "";
        }

        public void FillTeamMemberType()
        {
            clsTeamMember ccm = new clsTeamMember();
            clsTeamMemberController ccmc = new clsTeamMemberController();
            DataTable dt = new DataTable();

            dt = ccmc.GetTeamMemberType();
            if (dt.Rows.Count > 0)
            {
                ddlTeamMemberType.DataSource = dt;
                ddlTeamMemberType.DataTextField = "TeamMemberTypeValue";
                ddlTeamMemberType.DataValueField = "TeamMemberTypeId";
                ddlTeamMemberType.DataBind();
                ddlTeamMemberType.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillTeamMember()
        {
            clsTeamMember ccm = new clsTeamMember();
            clsTeamMemberController ccmc = new clsTeamMemberController();
            DataTable dt = new DataTable();

            dt = ccmc.GetTeamMemberDetail();
            if (dt.Rows.Count > 0)
            {
                ddlSelectMember.DataSource = dt;
                ddlSelectMember.DataTextField = "TeamMemberName";
                ddlSelectMember.DataValueField = "RegistrationId";
                ddlSelectMember.DataBind();
                ddlSelectMember.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

    }
}