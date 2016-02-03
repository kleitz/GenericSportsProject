
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
    public partial class frmPlayerType : PortalModuleBase
    {
        clsPlayerType ccm = new clsPlayerType();
        clsPlayerTypeController ccmc = new clsPlayerTypeController();

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
            btnUpdatePlayerType.Visible = false;
            btnSavePlayerType.Visible = false;
            pnlPlayerTypeEntry.Visible = false;
            LoadDocumentsGrid();
        }

        private void FillSport()
        {
            DataTable dt = new DataTable();
            dt = ccmc.GetSport();
            if (dt.Rows.Count > 0)
            {
                ddlSport.DataSource = dt;
                ddlSport.DataTextField = "SportName";
                ddlSport.DataValueField = "SportID";
                ddlSport.DataBind();
                ddlSport.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        protected void btnUpdatePlayerType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            clsPlayerType ccm = new clsPlayerType();
            clsPlayerTypeController ccmc = new clsPlayerTypeController();

            ccm.PlayerTypeID = Convert.ToInt16(currentId);
            ccm.SportID = Convert.ToInt32(ddlSport.SelectedValue);
            ccm.PlayerTypeName = txtPlayerType.Text.Trim();
            ccm.PlayerTypeDesc = txtPlayerTypeDesc.Text.Trim();

            ccm.PortalID = PortalId;
            ccm.ModifiedById = currentUser.Username;

            // Call Update Method
            ccmc.UpdatePlayerType(ccm);

            btnAddPlayerType.Visible = true;
            pnlPlayerTypeGrid.Visible = true;
            btnSavePlayerType.Visible = true;
            btnUpdatePlayerType.Visible = false;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        #endregion Page Events

        #region Methods

        public void LoadDocumentsGrid()
        {
            DataTable dt = new DataTable();
            clsPlayerTypeController ccmc = new clsPlayerTypeController();

            dt = ccmc.GetPlayerTypeList();

            if (dt.Rows.Count > 0)
            {
                gvPlayerType.DataSource = dt;
                gvPlayerType.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSavePlayerType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            clsPlayerType ccm = new clsPlayerType();
            clsPlayerTypeController ccmc = new clsPlayerTypeController();

            ccm.SportID = Convert.ToInt32(ddlSport.SelectedValue);
            ccm.PlayerTypeName = txtPlayerType.Text.Trim();
            ccm.PlayerTypeDesc = txtPlayerTypeDesc.Text.Trim();

            ccm.PortalID = PortalId;
            ccm.CreatedById = currentUser.Username;
            ccm.ModifiedById = currentUser.Username;

            // Call Save Method
            ccmc.InsertPlayerType(ccm);

            btnAddPlayerType.Visible = true;
            pnlPlayerTypeGrid.Visible = true;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        protected void btnCancelPlayerType_Click(object sender, EventArgs e)
        {
            pnlPlayerTypeGrid.Visible = true;
            pnlPlayerTypeEntry.Visible = false;
            btnSavePlayerType.Visible = false;
            btnUpdatePlayerType.Visible = false;
            LoadDocumentsGrid();
            ClearData();
        }

        #endregion Button Click Events

        protected void gvPlayerType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlayerType.PageIndex = e.NewPageIndex;
            LoadDocumentsGrid();
        }

        protected void btnAddPlayerType_Click(object sender, EventArgs e)
        {
            pnlPlayerTypeGrid.Visible = false;
            pnlPlayerTypeEntry.Visible = true;
            btnSavePlayerType.Visible = true;
            btnUpdatePlayerType.Visible = false;
            ClearData();
            FillSport();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionPlayerTypeID")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                int editid = 0;
                int.TryParse(str, out editid);
                ViewState["currentId"] = Convert.ToInt16(str);

                clsPlayerType ccm = new clsPlayerType();
                clsPlayerTypeController ccmc = new clsPlayerTypeController();

                ClearData();
                FillSport();
                DataTable dt1 = new clsPlayerTypeController().GetPlayerTypeDetailByPlayerTypeID(editid);

                if (dt1.Rows.Count > 0)
                {
                    ddlSport.SelectedValue = dt1.Rows[0]["SportID"].ToString();
                    txtPlayerType.Text = dt1.Rows[0]["PlayerTypeName"].ToString();
                    txtPlayerTypeDesc.Text = dt1.Rows[0]["PlayerTypeDesc"].ToString();
                }

                btnUpdatePlayerType.Visible = true;
                btnSavePlayerType.Visible = false;
                pnlPlayerTypeEntry.Visible = true;
                pnlPlayerTypeGrid.Visible = false;
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
            txtPlayerType.Text = "";
            txtPlayerTypeDesc.Text = "";
        }
    }
}