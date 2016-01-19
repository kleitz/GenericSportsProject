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
    public partial class frmClubOwner : PortalModuleBase
    {
        clsClubOwner cco = new clsClubOwner();
        clsClubOwnerController ccoc = new clsClubOwnerController();

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

        int ClubID
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["ClubID"] != null))
                {
                    int.TryParse(Request.QueryString["ClubID"].ToString(), out retVal);
                    return retVal;
                }
                return 0;
            }
        }

        #endregion variables

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClubID == null || ClubID == 0) return;
            ClearData();
            FillClubName();
            btnUpdateClubOwner.Visible = false;
            btnSaveClubOwner.Visible = false;
            pnlClubOwnerEntry.Visible = false;

            if (ClubID != 0)
            {
                LoadDocumentsGrid(ClubID);
                ClearData();
                FillClubName();
            }
        }

        private void FillClubName()
        {
            clsClubOwner cco = new clsClubOwner();
            clsClubOwnerController ccoc = new clsClubOwnerController();

            DataTable dt = new DataTable();
            dt = ccoc.GetClubNameByClubID(ClubID);

            if (dt.Rows.Count > 0)
            {
                lbl_Club_Name.Text = dt.Rows[0]["ClubName"].ToString();
            }
        }

        protected void btnUpdateClubOwner_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            clsClubOwner cco = new clsClubOwner();
            clsClubOwnerController ccoc = new clsClubOwnerController();

            cco.ClubOwnersId = Convert.ToInt16(currentId);
            cco.ClubId = ClubID;
            cco.OwnerName = txtClubOwnerName.Text.Trim();
            cco.OwnerDescription = txtClubOwnerDescription.Text.Trim();
            if (txtClubOwnerPercentage.Text == "")
            {
                txtClubOwnerPercentage.Text = "0";
            }
            else
            {
                cco.OwnerPercentage = Convert.ToInt32(txtClubOwnerPercentage.Text.Trim());
            }

            cco.PortalID = PortalId;
            cco.ModifiedById = currentUser.Username;

            // Call Update Method
            ccoc.UpdateClubOwner(cco);

            btnAddClubOwner.Visible = true;
            pnlClubOwnerGrid.Visible = true;
            btnSaveClubOwner.Visible = true;
            btnUpdateClubOwner.Visible = false;
            FillClubName();
            LoadDocumentsGrid(ClubID);
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubOwner", "ClubID=" + ClubID));
        }

        #endregion Page Events

        #region Methods

        public void LoadDocumentsGrid(int ClubID)
        {
            DataTable dt = new DataTable();
            clsClubOwnerController ccoc = new clsClubOwnerController();

            dt = ccoc.GetClubOwnerList(ClubID);

            if (dt.Rows.Count > 0)
            {
                gvClubOwner.DataSource = dt;
                gvClubOwner.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveClubOwner_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            clsClubOwner cco = new clsClubOwner();
            clsClubOwnerController ccoc = new clsClubOwnerController();

            cco.ClubId = ClubID;
            cco.OwnerName = txtClubOwnerName.Text.Trim();
            cco.OwnerDescription = txtClubOwnerDescription.Text.Trim();

            if (txtClubOwnerPercentage.Text == "")
            {
                txtClubOwnerPercentage.Text = "0";
            }
            else
            {
                cco.OwnerPercentage = Convert.ToInt32(txtClubOwnerPercentage.Text.Trim());
            }

            cco.PortalID = PortalId;
            cco.CreatedById = currentUser.Username;
            cco.ModifiedById = currentUser.Username;

            // Call Save Method
            ccoc.InsertClubOwner(cco);

            btnAddClubOwner.Visible = true;
            pnlClubOwnerGrid.Visible = true;
            FillClubName();
            LoadDocumentsGrid(ClubID);
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubOwner", "ClubID=" + ClubID));
        }

        protected void btnCancelClubOwner_Click(object sender, EventArgs e)
        {
            pnlClubOwnerGrid.Visible = true;
            pnlClubOwnerEntry.Visible = false;
            btnSaveClubOwner.Visible = false;
            btnUpdateClubOwner.Visible = false;
            LoadDocumentsGrid(ClubID);
            ClearData();
        }

        #endregion Button Click Events

        protected void gvClubOwner_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClubOwner.PageIndex = e.NewPageIndex;
            LoadDocumentsGrid(ClubID);
        }

        protected void btnAddClubOwner_Click(object sender, EventArgs e)
        {
            pnlClubOwnerGrid.Visible = false;
            pnlClubOwnerEntry.Visible = true;
            btnSaveClubOwner.Visible = true;
            btnUpdateClubOwner.Visible = false;
            ClearData();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionClubOwnersId")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                ClearData();
                int editid = 0;
                int.TryParse(str, out editid);
                ViewState["currentId"] = Convert.ToInt16(str);

                clsClubOwner cco = new clsClubOwner();
                clsClubOwnerController ccoc = new clsClubOwnerController();

                DataTable dt = new DataTable();
                dt = ccoc.GetClubNameByClubID(ClubID);
                if (dt.Rows.Count > 0)
                {
                    lbl_Club_Name.Text = dt.Rows[0]["ClubName"].ToString();
                }

                DataTable dt1 = new clsClubOwnerController().GetClubOwnerDetailByClubOwnerID(editid);

                if (dt1.Rows.Count > 0)
                {
                    txtClubOwnerName.Text = dt1.Rows[0]["OwnerName"].ToString();
                    txtClubOwnerDescription.Text = dt1.Rows[0]["OwnerDescription"].ToString();
                    txtClubOwnerPercentage.Text = dt1.Rows[0]["OwnerPercentage"].ToString();
                }

                btnUpdateClubOwner.Visible = true;
                btnSaveClubOwner.Visible = false;
                pnlClubOwnerEntry.Visible = true;
                pnlClubOwnerGrid.Visible = false;

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
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClub"));
        }

        public void ClearData()
        {
            txtClubOwnerName.Text = "";
            txtClubOwnerDescription.Text = "";
            txtClubOwnerPercentage.Text = "";
        }

    }
}