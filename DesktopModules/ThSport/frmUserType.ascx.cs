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
    public partial class frmUserType : PortalModuleBase
    {
        clsUserType ccm = new clsUserType();
        clsUserTypeController ccmc = new clsUserTypeController();

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

        #region variables

        #endregion variables

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdateUserType.Visible = false;
            btnSaveUserType.Visible = false;
            pnlUserTypeEntry.Visible = false;

            LoadDocumentsGrid();

        }

        protected void btnUpdateUserType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            clsUserType ccm = new clsUserType();
            clsUserTypeController ccmc = new clsUserTypeController();

            ccm.UserTypeId = Convert.ToInt16(currentId);
            ccm.UserTypeName = txtUserType.Text.Trim();
            ccm.UserTypeAbbr = txtUserTypeAddress.Text.Trim();
            ccm.UserTypeDesc = txtUserTypeDesc.Text.Trim();

            ccm.PortalID = PortalId;
            ccm.ModifiedById = currentUser.Username;

            // Call Update Method
            ccmc.UpdateUserType(ccm);

            btnAddUserType.Visible = true;
            pnlUserTypeGrid.Visible = true;
            btnSaveUserType.Visible = true;
            btnUpdateUserType.Visible = false;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        #endregion Page Events

        #region Methods

        public void LoadDocumentsGrid()
        {
            DataTable dt = new DataTable();
            clsUserTypeController ccmc = new clsUserTypeController();

            dt = ccmc.GetUserTypeList();

            if (dt.Rows.Count > 0)
            {
                gvUserType.DataSource = dt;
                gvUserType.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveUserType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            clsUserType ccm = new clsUserType();
            clsUserTypeController ccmc = new clsUserTypeController();

            ccm.UserTypeName = txtUserType.Text.Trim();
            ccm.UserTypeAbbr = txtUserTypeAddress.Text.Trim();
            ccm.UserTypeDesc = txtUserTypeDesc.Text.Trim();

            ccm.PortalID = PortalId;
            ccm.CreatedById = currentUser.Username;
            ccm.ModifiedById = currentUser.Username;

            // Call Save Method
            ccmc.InsertUserType(ccm);

            btnAddUserType.Visible = true;
            pnlUserTypeGrid.Visible = true;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        protected void btnCancelUserType_Click(object sender, EventArgs e)
        {
            pnlUserTypeGrid.Visible = true;
            pnlUserTypeEntry.Visible = false;
            btnSaveUserType.Visible = false;
            btnUpdateUserType.Visible = false;
            LoadDocumentsGrid();
            ClearData();
        }

        #endregion Button Click Events

        protected void gvUserType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserType.PageIndex = e.NewPageIndex;
            LoadDocumentsGrid();
        }

        protected void btnAddUserType_Click(object sender, EventArgs e)
        {
            pnlUserTypeGrid.Visible = false;
            pnlUserTypeEntry.Visible = true;
            btnSaveUserType.Visible = true;
            btnUpdateUserType.Visible = false;
            ClearData();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionUserTypeId")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                int editid = 0;
                int.TryParse(str, out editid);
                ViewState["currentId"] = Convert.ToInt16(str);

                clsUserType ccm = new clsUserType();
                clsUserTypeController ccmc = new clsUserTypeController();

                ClearData();
                DataTable dt1 = new clsUserTypeController().GetUserTypeDetailByUserTypeID(editid);

                if (dt1.Rows.Count > 0)
                {
                    txtUserType.Text = dt1.Rows[0]["UserTypeName"].ToString();
                    txtUserTypeAddress.Text = dt1.Rows[0]["UserTypeAbbr"].ToString();
                    txtUserTypeDesc.Text = dt1.Rows[0]["UserTypeDesc"].ToString();
                }

                btnUpdateUserType.Visible = true;
                btnSaveUserType.Visible = false;
                pnlUserTypeEntry.Visible = true;
                pnlUserTypeGrid.Visible = false;
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
            txtUserType.Text = "";
            txtUserTypeAddress.Text = "";
            txtUserTypeDesc.Text = "";
        }
    }
}