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
    public partial class frmTeamMemberType : PortalModuleBase
    {
        clsTeamMemberType ccm = new clsTeamMemberType();
        clsTeamMemberTypeController ccmc = new clsTeamMemberTypeController();

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
            btnUpdateTeamMemberType.Visible = false;
            btnSaveTeamMemberType.Visible = false;
            pnlTeamMemberTypeEntry.Visible = false;
            LoadDocumentsGrid();
        }

        protected void btnUpdateTeamMemberType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            clsTeamMemberType ccm = new clsTeamMemberType();
            clsTeamMemberTypeController ccmc = new clsTeamMemberTypeController();

            ccm.TeamMemberTypeId = Convert.ToInt16(currentId);
            ccm.TeamMemberTypeValue = txtTeamMemberType.Text.Trim();
            ccm.TeamMemberTypeDesc = txtTeamMemberTypeDesc.Text.Trim();

            if (ChkIsActive.Checked == true)
            {
                ccm.ActiveFlagId = 1;
            }
            else
            {
                ccm.ActiveFlagId = 0;
            }

            if (ChkIsShow.Checked == true)
            {
                ccm.ShowFlagId = 1;
            }
            else
            {
                ccm.ShowFlagId = 0;
            }

            ccm.PortalID = PortalId;
            ccm.ModifiedById = currentUser.Username;

            // Call Update Method
            ccmc.UpdateTeamMemberType(ccm);

            btnAddTeamMemberType.Visible = true;
            pnlTeamMemberTypeGrid.Visible = true;
            btnSaveTeamMemberType.Visible = true;
            btnUpdateTeamMemberType.Visible = false;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        #endregion Page Events

        #region Methods

        public void LoadDocumentsGrid()
        {
            DataTable dt = new DataTable();
            clsTeamMemberTypeController ccmc = new clsTeamMemberTypeController();

            dt = ccmc.GetTeamMemberTypeList();

            if (dt.Rows.Count > 0)
            {
                gvTeamMemberType.DataSource = dt;
                gvTeamMemberType.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveTeamMemberType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            clsTeamMemberType ccm = new clsTeamMemberType();
            clsTeamMemberTypeController ccmc = new clsTeamMemberTypeController();

            ccm.TeamMemberTypeValue = txtTeamMemberType.Text.Trim();
            ccm.TeamMemberTypeDesc = txtTeamMemberTypeDesc.Text.Trim();

            if (ChkIsActive.Checked == true)
            {
                ccm.ActiveFlagId = 1;
            }
            else
            {
                ccm.ActiveFlagId = 0;
            }

            if (ChkIsShow.Checked == true)
            {
                ccm.ShowFlagId = 1;
            }
            else
            {
                ccm.ShowFlagId = 0;
            }

            ccm.PortalID = PortalId;
            ccm.CreatedById = currentUser.Username;
            ccm.ModifiedById = currentUser.Username;

            // Call Save Method
            ccmc.InsertTeamMemberType(ccm);

            btnAddTeamMemberType.Visible = true;
            pnlTeamMemberTypeGrid.Visible = true;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        protected void btnCancelTeamMemberType_Click(object sender, EventArgs e)
        {
            pnlTeamMemberTypeGrid.Visible = true;
            pnlTeamMemberTypeEntry.Visible = false;
            btnSaveTeamMemberType.Visible = false;
            btnUpdateTeamMemberType.Visible = false;
            LoadDocumentsGrid();
            ClearData();
        }

        #endregion Button Click Events

        protected void gvTeamMemberType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTeamMemberType.PageIndex = e.NewPageIndex;
            LoadDocumentsGrid();
        }

        protected void btnAddTeamMemberType_Click(object sender, EventArgs e)
        {
            pnlTeamMemberTypeGrid.Visible = false;
            pnlTeamMemberTypeEntry.Visible = true;
            btnSaveTeamMemberType.Visible = true;
            btnUpdateTeamMemberType.Visible = false;
            ClearData();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionTeamMemberTypeID")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                int editid = 0;
                int.TryParse(str, out editid);
                ViewState["currentId"] = Convert.ToInt16(str);

                clsTeamMemberType ccm = new clsTeamMemberType();
                clsTeamMemberTypeController ccmc = new clsTeamMemberTypeController();

                ClearData();
                DataTable dt = new clsTeamMemberTypeController().GetTeamMemberTypeDetailByTeamMemberTypeID(editid);

                if (dt.Rows.Count > 0)
                {
                    txtTeamMemberType.Text = dt.Rows[0]["TeamMemberTypeValue"].ToString();
                    txtTeamMemberTypeDesc.Text = dt.Rows[0]["TeamMemberTypeDesc"].ToString();

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
                }

                btnUpdateTeamMemberType.Visible = true;
                btnSaveTeamMemberType.Visible = false;
                pnlTeamMemberTypeEntry.Visible = true;
                pnlTeamMemberTypeGrid.Visible = false;
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
            txtTeamMemberType.Text = "";
            txtTeamMemberTypeDesc.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
        }
    }
}