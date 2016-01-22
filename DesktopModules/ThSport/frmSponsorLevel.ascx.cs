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
    public partial class frmSponsorLevel : PortalModuleBase
    {
        clsSponsorLevel ccm = new clsSponsorLevel();
        clsSponsorLevelController ccmc = new clsSponsorLevelController();

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
            btnUpdateSponsorLevel.Visible = false;
            btnSaveSponsorLevel.Visible = false;
            pnlSponsorLevelEntry.Visible = false;
            LoadDocumentsGrid();
        }

        protected void btnUpdateSponsorLevel_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            clsSponsorLevel ccm = new clsSponsorLevel();
            clsSponsorLevelController ccmc = new clsSponsorLevelController();

            ccm.SponsorLevelId = Convert.ToInt16(currentId);
            ccm.SponsorLevelValue = txtSponsorLevel.Text.Trim();
            ccm.SponsorLevelDesc = txtSponsorLevelDesc.Text.Trim();

            ccm.PortalID = PortalId;
            ccm.ModifiedById = currentUser.Username;

            // Call Update Method
            ccmc.UpdateSponsorLevel(ccm);

            btnAddSponsorLevel.Visible = true;
            pnlSponsorLevelGrid.Visible = true;
            btnSaveSponsorLevel.Visible = true;
            btnUpdateSponsorLevel.Visible = false;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        #endregion Page Events

        #region Methods

        public void LoadDocumentsGrid()
        {
            DataTable dt = new DataTable();
            clsSponsorLevelController ccmc = new clsSponsorLevelController();

            dt = ccmc.GetSponsorLevelList();

            if (dt.Rows.Count > 0)
            {
                gvSponsorLevel.DataSource = dt;
                gvSponsorLevel.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveSponsorLevel_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            clsSponsorLevel ccm = new clsSponsorLevel();
            clsSponsorLevelController ccmc = new clsSponsorLevelController();

            ccm.SponsorLevelValue = txtSponsorLevel.Text.Trim();
            ccm.SponsorLevelDesc = txtSponsorLevelDesc.Text.Trim();

            ccm.PortalID = PortalId;
            ccm.CreatedById = currentUser.Username;
            ccm.ModifiedById = currentUser.Username;

            // Call Save Method
            ccmc.InsertSponsorLevel(ccm);

            btnAddSponsorLevel.Visible = true;
            pnlSponsorLevelGrid.Visible = true;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        protected void btnCancelSponsorLevel_Click(object sender, EventArgs e)
        {
            pnlSponsorLevelGrid.Visible = true;
            pnlSponsorLevelEntry.Visible = false;
            btnSaveSponsorLevel.Visible = false;
            btnUpdateSponsorLevel.Visible = false;
            LoadDocumentsGrid();
            ClearData();
        }

        #endregion Button Click Events

        protected void gvSponsorLevel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSponsorLevel.PageIndex = e.NewPageIndex;
            LoadDocumentsGrid();
        }

        protected void btnAddSponsorLevel_Click(object sender, EventArgs e)
        {
            pnlSponsorLevelGrid.Visible = false;
            pnlSponsorLevelEntry.Visible = true;
            btnSaveSponsorLevel.Visible = true;
            btnUpdateSponsorLevel.Visible = false;
            ClearData();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionSponsorLevelId")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                int editid = 0;
                int.TryParse(str, out editid);
                ViewState["currentId"] = Convert.ToInt16(str);

                clsSponsorType ccm = new clsSponsorType();
                clsSponsorTypeController ccmc = new clsSponsorTypeController();

                ClearData();
                DataTable dt1 = new clsSponsorLevelController().GetSponsorLevelDetailBySponsorLevelID(editid);

                if (dt1.Rows.Count > 0)
                {
                    txtSponsorLevel.Text = dt1.Rows[0]["SponsorLevelValue"].ToString();
                    txtSponsorLevelDesc.Text = dt1.Rows[0]["SponsorLevelDesc"].ToString();
                }

                btnUpdateSponsorLevel.Visible = true;
                btnSaveSponsorLevel.Visible = false;
                pnlSponsorLevelEntry.Visible = true;
                pnlSponsorLevelGrid.Visible = false;
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
            txtSponsorLevel.Text = "";
            txtSponsorLevelDesc.Text = "";
        }
    }
}