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
    public partial class frmClubMember : PortalModuleBase
    {
        clsClubMember ccm = new clsClubMember();
        clsClubMemberController ccmc = new clsClubMemberController();

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

        string regid
        {
            get
            {
                if (ViewState["regid"] != null)
                    return ViewState["regid"].ToString();
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
            FillClubName();
            btnUpdateClubMember.Visible = false;
            btnSaveClubMember.Visible = false;
            pnlClubMemberEntry.Visible = false;

            if (ClubID != 0)
            {
                LoadDocumentsGrid(ClubID);
                FillClubName();
            }
        }

        private void FillClubName()
        {
            clsClubMember ccm = new clsClubMember();
            clsClubMemberController ccmc = new clsClubMemberController();

            DataTable dt = new DataTable();
            dt = ccmc.GetClubNameByClubID(ClubID);

            if (dt.Rows.Count > 0)
            {
                lbl_Club_Member.Text = dt.Rows[0]["ClubName"].ToString();
            }
        }

        protected void btnUpdateClubMember_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            clsClubMember ccm = new clsClubMember();
            clsClubMemberController ccmc = new clsClubMemberController();

            ccm.ClubMemberId = Convert.ToInt32(currentId);
            ccm.RegistrationId = Convert.ToInt32(regid);
            ccm.ClubId = Convert.ToInt32(ClubID);
            ccm.ClubMemberTypeId = Convert.ToInt32(ddlMemberType.SelectedValue);
            ccm.ClubId = Convert.ToInt32(ClubID);
            ccm.ClubMemberDesc = txtClubMemberDesc.Text.Trim();

            ccm.PortalID = PortalId;
            ccm.ModifiedById = currentUser.Username;

            // Call Update Method
            ccmc.UpdateClubMember(ccm);

            btnAddClubMember.Visible = true;
            pnlClubMemberGrid.Visible = true;
            btnSaveClubMember.Visible = true;
            btnUpdateClubMember.Visible = false;
            FillClubName();
            LoadDocumentsGrid(ClubID);
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        #endregion Page Events

        #region Methods

        public void LoadDocumentsGrid(int ClubID)
        {
            DataTable dt = new DataTable();
            clsClubMemberController ccmc = new clsClubMemberController();

            dt = ccmc.GetClubMemberList(ClubID);

            if (dt.Rows.Count > 0)
            {
                gvClubMember.DataSource = dt;
                gvClubMember.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveClubMember_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            clsClubMember ccm = new clsClubMember();
            clsClubMemberController ccmc = new clsClubMemberController();

            ccm.RegistrationId = Convert.ToInt32(ddlSelectMember.SelectedValue);
            ccm.ClubId = Convert.ToInt32(ClubID);
            ccm.ClubMemberTypeId = Convert.ToInt32(ddlMemberType.SelectedValue);
            ccm.ClubMemberDesc = txtClubMemberDesc.Text.Trim();

            ccm.PortalID = PortalId;
            ccm.CreatedById = currentUser.Username;
            ccm.ModifiedById = currentUser.Username;

            // Call Save Method
            ccmc.InsertClubMember(ccm);

            btnAddClubMember.Visible = true;
            pnlClubMemberGrid.Visible = true;
            FillClubName();
            LoadDocumentsGrid(ClubID);
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        protected void btnCancelClubMember_Click(object sender, EventArgs e)
        {
            pnlClubMemberGrid.Visible = true;
            pnlClubMemberEntry.Visible = false;
            btnSaveClubMember.Visible = false;
            btnUpdateClubMember.Visible = false;
            LoadDocumentsGrid(ClubID);
            ClearData();
        }

        #endregion Button Click Events

        protected void gvClubMember_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClubMember.PageIndex = e.NewPageIndex;
            LoadDocumentsGrid(ClubID);
        }

        protected void btnAddClubMember_Click(object sender, EventArgs e)
        {
            pnlClubMemberGrid.Visible = false;
            pnlClubMemberEntry.Visible = true;
            btnSaveClubMember.Visible = true;
            btnUpdateClubMember.Visible = false;
            ClearData();
            FillMemberType();
            FillClubMember();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionClubMemberId")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                int editid = 0;
                int.TryParse(str, out editid);
                ViewState["currentId"] = Convert.ToInt16(str);

                clsClubMember ccm = new clsClubMember();
                clsClubMemberController ccmc = new clsClubMemberController();

                DataTable dt = new DataTable();
                dt = ccmc.GetClubNameByClubID(ClubID);
                if (dt.Rows.Count > 0)
                {
                    lbl_Club_Member.Text = dt.Rows[0]["ClubName"].ToString();
                }

                FillMemberType();
                FillClubMember();
                ClearData();
                DataTable dt1 = new clsClubMemberController().GetClubMemberDetailByClubMemberID(editid);

                if (dt1.Rows.Count > 0)
                {
                    ViewState["regid"] = dt1.Rows[0]["RegistrationId"].ToString();
                    ddlSelectMember.SelectedValue = dt1.Rows[0]["RegistrationId"].ToString(); 
                    ddlMemberType.SelectedValue = dt1.Rows[0]["ClubMemberTypeId"].ToString();
                    txtClubMemberDesc.Text = dt1.Rows[0]["ClubMemberDesc"].ToString();
                }

                btnUpdateClubMember.Visible = true;
                btnSaveClubMember.Visible = false;
                pnlClubMemberEntry.Visible = true;
                pnlClubMemberGrid.Visible = false;
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
            txtClubMemberDesc.Text = "";
        }

        public void FillMemberType()
        {
            clsClubMember ccm = new clsClubMember();
            clsClubMemberController ccmc = new clsClubMemberController();
            DataTable dt = new DataTable();

            dt = ccmc.GetClubMemberType();
            if (dt.Rows.Count > 0)
            {
                ddlMemberType.DataSource = dt;
                ddlMemberType.DataTextField = "ClubMemberTypeValue";
                ddlMemberType.DataValueField = "ClubMemberTypeId";
                ddlMemberType.DataBind();
                ddlMemberType.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillClubMember()
        {
            clsClubMember ccm = new clsClubMember();
            clsClubMemberController ccmc = new clsClubMemberController();
            DataTable dt = new DataTable();

            dt = ccmc.GetClubMember();
            if (dt.Rows.Count > 0)
            {
                ddlSelectMember.DataSource = dt;
                ddlSelectMember.DataTextField = "ClubMemberName";
                ddlSelectMember.DataValueField = "RegistrationId";
                ddlSelectMember.DataBind();
                ddlSelectMember.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

    }
}