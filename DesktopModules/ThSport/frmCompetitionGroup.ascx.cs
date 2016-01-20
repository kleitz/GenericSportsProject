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
    public partial class frmCompetitionGroup : PortalModuleBase
    {
        clsCompetitionGroup cgClass = new clsCompetitionGroup();
        clsCompetitionGroupController cgController = new clsCompetitionGroupController();
        
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

        int CompetitionGroupID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdnCompetitionGroupID.Value))
                {
                    int.TryParse(hdnCompetitionGroupID.Value, out id);
                }
                return id;
            }
        }

        #endregion variables

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdateCompetitionGroup.Visible = false;
            btnSaveCompetitionGroup.Visible = false;
            pnlCompetitionGroupEntry.Visible = false;

            LoadCompetitionGroupGrid();

            if (IsPostBack)
            {
                
            }

        }

        #endregion Page Events

        #region Methods

        public void LoadCompetitionGroupGrid()
        {
            DataTable dt = new DataTable();
            dt = cgController.GetCompetitionGroupList(CompetitionID);

            gvCompetitionGroup.DataSource = dt;
            gvCompetitionGroup.DataBind();
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveCompetitionGroup_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            cgClass.CompetitionId = CompetitionID;
            cgClass.CompetitionGroupName = txtCompetitionGroup.Text.Trim();
            cgClass.CompetitionGroupAbbr = txtCompetitionGroupAbbr.Text;

            int.TryParse(txtNoOfTeam.Text, out cgClass.NumberofTeams);

            if (ChkIsConfirm.Checked)
            {
                cgClass.ConfirmFlag = 1;
            }
            else
            {
                cgClass.ConfirmFlag = 0;
            }

            cgClass.CreatedById = currentUser.Username;
            cgClass.ModifiedById = currentUser.Username;

            // Call Save Method
            cgController.InsertCompetitionGroup(cgClass);

            btnAddCompetitionGroup.Visible = true;
            pnlCompetitionGroupGrid.Visible = true;
            LoadCompetitionGroupGrid();
            ClearData();

        }

        protected void btnUpdateCompetitionGroup_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            cgClass.CompetitionGroupId = CompetitionGroupID;
            
            cgClass.CompetitionGroupName = txtCompetitionGroup.Text.Trim();
            cgClass.CompetitionGroupAbbr = txtCompetitionGroupAbbr.Text;
            cgClass.CompetitionId = CompetitionID;

            int.TryParse(txtNoOfTeam.Text, out cgClass.NumberofTeams);

            if (ChkIsConfirm.Checked)
            {
                cgClass.ConfirmFlag = 1;
            }
            else
            {
                cgClass.ConfirmFlag = 0;
            }

            cgClass.PortalID = PortalId;
            cgClass.ModifiedById = currentUser.Username;

            // Call Update Method
            cgController.UpdateCompetitionGroup(cgClass);

            btnAddCompetitionGroup.Visible = true;
            pnlCompetitionGroupGrid.Visible = true;
            btnSaveCompetitionGroup.Visible = true;
            btnUpdateCompetitionGroup.Visible = false;
            LoadCompetitionGroupGrid();
            ClearData();
        }

        protected void btnCancelCompetitionGroup_Click(object sender, EventArgs e)
        {
            pnlCompetitionGroupGrid.Visible = true;
            pnlCompetitionGroupEntry.Visible = false;
            btnSaveCompetitionGroup.Visible = false;
            btnUpdateCompetitionGroup.Visible = false;
            LoadCompetitionGroupGrid();
            ClearData();
        }

        public void ClearData()
        {
            txtCompetitionGroup.Text = "";
            txtCompetitionGroupAbbr.Text = "";
            txtNoOfTeam.Text = "";
            ChkIsConfirm.Checked = false;
        }

        protected void btnAddCompetitionGroup_Click(object sender, EventArgs e)
        {
            pnlCompetitionGroupGrid.Visible = false;
            pnlCompetitionGroupEntry.Visible = true;
            btnSaveCompetitionGroup.Visible = true;
            btnUpdateCompetitionGroup.Visible = false;
            ClearData();
        }

        #endregion Button Click Events

        #region Gridview Events

        protected void gvCompetitionGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCompetitionGroup.PageIndex = e.NewPageIndex;
            LoadCompetitionGroupGrid();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnCompetitionGroupID.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdn_CompetitionGroup_Id")).Value;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                ClearData();
                DataTable dt1 = cgController.GetCompetitionGroupDetailByCompetitionGroupID(CompetitionGroupID);

                if (dt1.Rows.Count > 0)
                {
                    txtCompetitionGroup.Text = dt1.Rows[0]["CompetitionGroupName"].ToString();
                    txtCompetitionGroupAbbr.Text = dt1.Rows[0]["CompetitionGroupAbbr"].ToString();

                    txtNoOfTeam.Text = dt1.Rows[0]["NumberofTeams"].ToString();

                    if(dt1.Rows[0]["ConfirmFlag"].ToString() == "1")
                    {
                        ChkIsConfirm.Checked = true;
                    }
                }

                btnUpdateCompetitionGroup.Visible = true;
                btnSaveCompetitionGroup.Visible = false;
                pnlCompetitionGroupEntry.Visible = true;
                pnlCompetitionGroupGrid.Visible = false;
            }
            else if (ddlSelectedValue == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);
                cgController.DeleteCompetitionGroup(CompetitionGroupID);
                LoadCompetitionGroupGrid();
            }
        }

        #endregion
    }
}