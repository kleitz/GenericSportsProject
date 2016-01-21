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
    public partial class frmSponsorType : PortalModuleBase
    {
        clsSponsorType ccm = new clsSponsorType();
        clsSponsorTypeController ccmc = new clsSponsorTypeController();

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
            btnUpdateSponsorType.Visible = false;
            btnSaveSponsorType.Visible = false;
            pnlSponsorTypeEntry.Visible = false;
            LoadDocumentsGrid();
        }

        protected void btnUpdateSponsorType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            clsSponsorType ccm = new clsSponsorType();
            clsSponsorTypeController ccmc = new clsSponsorTypeController();

            ccm.SponsorTypeId = Convert.ToInt16(currentId);
            ccm.SponsorTypeValue = txtSponsorType.Text.Trim();
            ccm.SponsorTypeDesc = txtSponsorTypeDesc.Text.Trim();
            
            ccm.PortalID = PortalId;
            ccm.ModifiedById = currentUser.Username;

            // Call Update Method
            ccmc.UpdateSponsorType(ccm);

            btnAddSponsorType.Visible = true;
            pnlSponsorTypeGrid.Visible = true;
            btnSaveSponsorType.Visible = true;
            btnUpdateSponsorType.Visible = false;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        #endregion Page Events

        #region Methods

        public void LoadDocumentsGrid()
        {
            DataTable dt = new DataTable();
            clsSponsorTypeController ccmc = new clsSponsorTypeController();

            dt = ccmc.GetSponsorTypeList();

            if (dt.Rows.Count > 0)
            {
                gvSponsorType.DataSource = dt;
                gvSponsorType.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveSponsorType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            clsSponsorType ccm = new clsSponsorType();
            clsSponsorTypeController ccmc = new clsSponsorTypeController();

            ccm.SponsorTypeValue = txtSponsorType.Text.Trim();
            ccm.SponsorTypeDesc = txtSponsorTypeDesc.Text.Trim();
            
            ccm.PortalID = PortalId;
            ccm.CreatedById = currentUser.Username;
            ccm.ModifiedById = currentUser.Username;

            // Call Save Method
            ccmc.InsertSponsorType(ccm);

            btnAddSponsorType.Visible = true;
            pnlSponsorTypeGrid.Visible = true;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        protected void btnCancelSponsorType_Click(object sender, EventArgs e)
        {
            pnlSponsorTypeGrid.Visible = true;
            pnlSponsorTypeEntry.Visible = false;
            btnSaveSponsorType.Visible = false;
            btnUpdateSponsorType.Visible = false;
            LoadDocumentsGrid();
            ClearData();
        }

        #endregion Button Click Events

        protected void gvSponsorType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSponsorType.PageIndex = e.NewPageIndex;
            LoadDocumentsGrid();
        }

        protected void btnAddSponsorType_Click(object sender, EventArgs e)
        {
            pnlSponsorTypeGrid.Visible = false;
            pnlSponsorTypeEntry.Visible = true;
            btnSaveSponsorType.Visible = true;
            btnUpdateSponsorType.Visible = false;
            ClearData();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionSponsorTypeId")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                int editid = 0;
                int.TryParse(str, out editid);
                ViewState["currentId"] = Convert.ToInt16(str);

                clsSponsorType ccm = new clsSponsorType();
                clsSponsorTypeController ccmc = new clsSponsorTypeController();

                ClearData();
                DataTable dt1 = new clsSponsorTypeController().GetSponsorTypeDetailBySponsorTypeID(editid);

                if (dt1.Rows.Count > 0)
                {
                    txtSponsorType.Text = dt1.Rows[0]["SponsorTypeValue"].ToString();
                     txtSponsorTypeDesc.Text = dt1.Rows[0]["SponsorTypeDesc"].ToString();
                }

                btnUpdateSponsorType.Visible = true;
                btnSaveSponsorType.Visible = false;
                pnlSponsorTypeEntry.Visible = true;
                pnlSponsorTypeGrid.Visible = false;
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
            txtSponsorType.Text = "";
            txtSponsorTypeDesc.Text = "";
        }
    }
}