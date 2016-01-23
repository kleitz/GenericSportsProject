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
    public partial class frmUserRole : PortalModuleBase
    {
        clsUserRole ccm = new clsUserRole();
        clsUserRoleController ccmc = new clsUserRoleController();

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

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdateUserRole.Visible = false;
            btnSaveUserRole.Visible = false;
            pnlUserRoleEntry.Visible = false;
            LoadDocumentsGrid();
        }

        protected void btnUpdateUserRole_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            clsUserRole ccm = new clsUserRole();
            clsUserRoleController ccmc = new clsUserRoleController();

            ccm.UserRoleId = Convert.ToInt16(currentId);
            ccm.UserRoleName = txtUserRole.Text.Trim();
            ccm.UserRoleAbbr = txtUserRoleAbbr.Text.Trim();
            ccm.UserRoleDesc = txtUserRoleDesc.Text.Trim();

            ccm.PortalId = PortalId;
            ccm.ModifiedById = currentUser.Username;

            // Call Update Method
            ccmc.UpdateUserRole(ccm);

            btnAddUserRole.Visible = true;
            pnlUserRoleGrid.Visible = true;
            btnSaveUserRole.Visible = true;
            btnUpdateUserRole.Visible = false;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        #endregion Page Events

        #region Methods

        public void LoadDocumentsGrid()
        {
            DataTable dt = new DataTable();
            clsUserRoleController ccmc = new clsUserRoleController();

            dt = ccmc.GetUserRoleList();

            if (dt.Rows.Count > 0)
            {
                gvUserRole.DataSource = dt;
                gvUserRole.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveUserRole_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            clsUserRole ccm = new clsUserRole();
            clsUserRoleController ccmc = new clsUserRoleController();

            ccm.UserRoleName = txtUserRole.Text.Trim();
            ccm.UserRoleAbbr = txtUserRoleAbbr.Text.Trim();
            ccm.UserRoleDesc = txtUserRoleDesc.Text.Trim();

            ccm.PortalId = PortalId;
            ccm.CreatedById = currentUser.Username;
            ccm.ModifiedById = currentUser.Username;

            // Call Save Method
            ccmc.InsertUserRole(ccm);

            btnAddUserRole.Visible = true;
            pnlUserRoleGrid.Visible = true;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        protected void btnCancelUserRole_Click(object sender, EventArgs e)
        {
            pnlUserRoleGrid.Visible = true;
            pnlUserRoleEntry.Visible = false;
            btnSaveUserRole.Visible = false;
            btnUpdateUserRole.Visible = false;
            LoadDocumentsGrid();
            ClearData();
        }

        #endregion Button Click Events

        protected void gvUserRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserRole.PageIndex = e.NewPageIndex;
            LoadDocumentsGrid();
        }

        protected void btnAddUserRole_Click(object sender, EventArgs e)
        {
            pnlUserRoleGrid.Visible = false;
            pnlUserRoleEntry.Visible = true;
            btnSaveUserRole.Visible = true;
            btnUpdateUserRole.Visible = false;
            ClearData();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionUserRoleId")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                int editid = 0;
                int.TryParse(str, out editid);
                ViewState["currentId"] = Convert.ToInt16(str);

                clsUserRole ccm = new clsUserRole();
                clsUserRoleController ccmc = new clsUserRoleController();

                ClearData();
                DataTable dt1 = new clsUserRoleController().GetUserRoleDetailByUserRoleID(editid);

                if (dt1.Rows.Count > 0)
                {
                    txtUserRole.Text = dt1.Rows[0]["UserRoleName"].ToString();
                    txtUserRoleAbbr.Text = dt1.Rows[0]["UserRoleAbbr"].ToString();
                    txtUserRoleDesc.Text = dt1.Rows[0]["UserRoleDesc"].ToString();
                }

                btnUpdateUserRole.Visible = true;
                btnSaveUserRole.Visible = false;
                pnlUserRoleEntry.Visible = true;
                pnlUserRoleGrid.Visible = false;
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

        public void ClearData()
        {
            txtUserRole.Text = "";
            txtUserRoleAbbr.Text = "";
            txtUserRoleDesc.Text = "";
        }
    }
}