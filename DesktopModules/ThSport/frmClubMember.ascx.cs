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

        string clubsportid
        {
            get
            {
                if (ViewState["clubsportid"] != null)
                    return ViewState["clubsportid"].ToString();
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
            FillMemberType();
            FillClubName();
            btnUpdateClubMember.Visible = false;
            btnSaveClubMember.Visible = false;
            pnlClubMemberEntry.Visible = false;

            if (ClubID != 0)
            {
                LoadDocumentsGrid(ClubID);
                FillMemberType();
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

            ccm.ClubMemberId = Convert.ToInt16(currentId);
            ccm.ClubSportsId = Convert.ToInt16(clubsportid);
            ccm.ClubMemberTypeId = Convert.ToInt32(ddlMemberType.SelectedValue);
            ccm.ClubMemberTitle = txtClubMemberTitle.Text.Trim();
            ccm.ClubMemberDesc = txtClubMemberDesc.Text.Trim();

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

            DataTable dt = new DataTable();
            dt = ccmc.GetClubSportIDByClubID(ClubID);
            
            if (dt.Rows.Count > 0)
            {
                ccm.ClubSportsId = Convert.ToInt32(dt.Rows[0]["ClubSportsId"].ToString());
            }

            ccm.ClubMemberTypeId = Convert.ToInt32(ddlMemberType.SelectedValue);
            ccm.ClubMemberTitle = txtClubMemberTitle.Text.Trim();
            ccm.ClubMemberDesc = txtClubMemberDesc.Text.Trim();

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
                ClearData();
                DataTable dt1 = new clsClubMemberController().GetClubMemberDetailByClubMemberID(editid);

                if (dt1.Rows.Count > 0)
                {
                    ViewState["clubsportid"] = Convert.ToInt32(dt1.Rows[0]["ClubSportsId"].ToString());
                    ddlMemberType.SelectedValue = dt1.Rows[0]["ClubMemberTypeId"].ToString();
                    txtClubMemberTitle.Text = dt1.Rows[0]["ClubMemberTitle"].ToString();
                    txtClubMemberDesc.Text = dt1.Rows[0]["ClubMemberDesc"].ToString();

                    if (dt1.Rows[0]["ActiveFlagId"].ToString() == "1")
                    {
                        ChkIsActive.Checked = true;
                    }
                    else
                    {
                        ChkIsActive.Checked = false;
                    }

                    if (dt1.Rows[0]["ShowFlagId"].ToString() == "1")
                    {
                        ChkIsShow.Checked = true;
                    }
                    else
                    {
                        ChkIsShow.Checked = false;
                    }
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
            ddlMemberType.SelectedValue = "0";
            txtClubMemberTitle.Text = "";
            txtClubMemberDesc.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
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
                ddlMemberType.Items.Insert(0, new ListItem("-- Select Member Type --", "0"));
            }
        }

    }
}