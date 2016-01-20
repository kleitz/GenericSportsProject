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
    public partial class frmAddDocumentsType : PortalModuleBase
    {
        clsAddDocumentsType ccm = new clsAddDocumentsType();
        clsAddDocumentsTypeController ccmc = new clsAddDocumentsTypeController();

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
            btnUpdateDocumentType.Visible = false;
            btnSaveDocumentType.Visible = false;
            pnlDocumentTypeEntry.Visible = false;

            LoadDocumentsGrid();
        }

        protected void btnUpdateDocumentType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            clsAddDocumentsType ccm = new clsAddDocumentsType();
            clsAddDocumentsTypeController ccmc = new clsAddDocumentsTypeController();

            ccm.RegistrationDocTypeId = Convert.ToInt16(currentId);
            ccm.RegistrationDocName = txtDocumentType.Text.Trim();
            ccm.RegistrationDocDesc = txtDocumentTypeDesc.Text.Trim();
            
            ccm.PortalID = PortalId;
            ccm.ModifiedById = currentUser.Username;

            // Call Update Method
            ccmc.UpdateDocumentType(ccm);

            btnAddDocumentType.Visible = true;
            pnlDocumentTypeGrid.Visible = true;
            btnSaveDocumentType.Visible = true;
            btnUpdateDocumentType.Visible = false;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        #endregion Page Events

        #region Methods

        public void LoadDocumentsGrid()
        {
            DataTable dt = new DataTable();
            clsAddDocumentsTypeController ccmc = new clsAddDocumentsTypeController();

            dt = ccmc.GetDocumentTypeList();

            if (dt.Rows.Count > 0)
            {
                gvDocumentType.DataSource = dt;
                gvDocumentType.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveDocumentType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            clsAddDocumentsType ccm = new clsAddDocumentsType();
            clsAddDocumentsTypeController ccmc = new clsAddDocumentsTypeController();

            ccm.RegistrationDocName = txtDocumentType.Text.Trim();
            ccm.RegistrationDocDesc = txtDocumentTypeDesc.Text.Trim();
            
            ccm.PortalID = PortalId;
            ccm.CreatedById = currentUser.Username;
            ccm.ModifiedById = currentUser.Username;

            // Call Save Method
            ccmc.InsertDocumentType(ccm);

            btnAddDocumentType.Visible = true;
            pnlDocumentTypeGrid.Visible = true;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        protected void btnCancelDocumentType_Click(object sender, EventArgs e)
        {
            pnlDocumentTypeGrid.Visible = true;
            pnlDocumentTypeEntry.Visible = false;
            btnSaveDocumentType.Visible = false;
            btnUpdateDocumentType.Visible = false;
            LoadDocumentsGrid();
            ClearData();
        }

        #endregion Button Click Events

        protected void gvDocumentType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDocumentType.PageIndex = e.NewPageIndex;
            LoadDocumentsGrid();
        }

        protected void btnAddDocumentType_Click(object sender, EventArgs e)
        {
            pnlDocumentTypeGrid.Visible = false;
            pnlDocumentTypeEntry.Visible = true;
            btnSaveDocumentType.Visible = true;
            btnUpdateDocumentType.Visible = false;
            ClearData();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionRegistrationDocTypeId")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                int editid = 0;
                int.TryParse(str, out editid);
                ViewState["currentId"] = Convert.ToInt16(str);

                clsAddDocumentsType ccm = new clsAddDocumentsType();
                clsAddDocumentsTypeController ccmc = new clsAddDocumentsTypeController();

                ClearData();
                DataTable dt1 = new clsAddDocumentsTypeController().GetDocumentTypeByDocumentTypeID(editid);

                if (dt1.Rows.Count > 0)
                {
                    txtDocumentType.Text = dt1.Rows[0]["RegistrationDocName"].ToString();
                    txtDocumentTypeDesc.Text = dt1.Rows[0]["RegistrationDocDesc"].ToString();
                }

                btnUpdateDocumentType.Visible = true;
                btnSaveDocumentType.Visible = false;
                pnlDocumentTypeEntry.Visible = true;
                pnlDocumentTypeGrid.Visible = false;
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
            txtDocumentType.Text = "";
            txtDocumentTypeDesc.Text = "";
            
        }
    }
}