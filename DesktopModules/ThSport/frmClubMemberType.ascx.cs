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
    public partial class frmClubMemberType : PortalModuleBase
    {
        clsClubMemberType ccm = new clsClubMemberType();
        clsClubMemberTypeController ccmc = new clsClubMemberTypeController();

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
            btnUpdateClubMemberType.Visible = false;
            btnSaveClubMemberType.Visible = false;
            pnlClubMemberTypeEntry.Visible = false;

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

        protected void btnUpdateClubMemberType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            clsClubMemberType ccm = new clsClubMemberType();
            clsClubMemberTypeController ccmc = new clsClubMemberTypeController();

            ccm.ClubMemberTypeId = Convert.ToInt16(currentId);
            ccm.SportID = Convert.ToInt32(ddlSport.SelectedValue);
            ccm.ClubMemberTypeValue = txtClubMemberTypeValue.Text.Trim();
            ccm.ClubMemberTypeDesc = txtClubMemberTypeDesc.Text.Trim();

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
            ccmc.UpdateClubMemberType(ccm);

            btnAddClubMemberType.Visible = true;
            pnlClubMemberTypeGrid.Visible = true;
            btnSaveClubMemberType.Visible = true;
            btnUpdateClubMemberType.Visible = false;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        #endregion Page Events

        #region Methods

        public void LoadDocumentsGrid()
        {
            DataTable dt = new DataTable();
            clsClubMemberTypeController ccmc = new clsClubMemberTypeController();

            dt = ccmc.GetClubMemberTypeList();

            if (dt.Rows.Count > 0)
            {
                gvClubMemberType.DataSource = dt;
                gvClubMemberType.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveClubMemberType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            clsClubMemberType ccm = new clsClubMemberType();
            clsClubMemberTypeController ccmc = new clsClubMemberTypeController();

            ccm.SportID = Convert.ToInt32(ddlSport.SelectedValue);
            ccm.ClubMemberTypeValue = txtClubMemberTypeValue.Text.Trim();
            ccm.ClubMemberTypeDesc = txtClubMemberTypeDesc.Text.Trim();

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
            ccmc.InsertClubMemberType(ccm);

            btnAddClubMemberType.Visible = true;
            pnlClubMemberTypeGrid.Visible = true;
            LoadDocumentsGrid();
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        protected void btnCancelClubMemberType_Click(object sender, EventArgs e)
        {
            pnlClubMemberTypeGrid.Visible = true;
            pnlClubMemberTypeEntry.Visible = false;
            btnSaveClubMemberType.Visible = false;
            btnUpdateClubMemberType.Visible = false;
            LoadDocumentsGrid();
            ClearData();
        }

        #endregion Button Click Events

        protected void gvClubMemberType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClubMemberType.PageIndex = e.NewPageIndex;
            LoadDocumentsGrid();
        }

        protected void btnAddClubMemberType_Click(object sender, EventArgs e)
        {
            pnlClubMemberTypeGrid.Visible = false;
            pnlClubMemberTypeEntry.Visible = true;
            btnSaveClubMemberType.Visible = true;
            btnUpdateClubMemberType.Visible = false;
            ClearData();
            FillSport();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionClubMemberTypeId")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                int editid = 0;
                int.TryParse(str, out editid);
                ViewState["currentId"] = Convert.ToInt16(str);

                clsClubMemberType ccm = new clsClubMemberType();
                clsClubMemberTypeController ccmc = new clsClubMemberTypeController();

                ClearData();
                FillSport();
                DataTable dt1 = new clsClubMemberTypeController().GetClubMemberTypeDetailByClubMemberTypeID(editid);

                if (dt1.Rows.Count > 0)
                {
                    txtClubMemberTypeValue.Text = dt1.Rows[0]["ClubMemberTypeValue"].ToString();
                    txtClubMemberTypeDesc.Text = dt1.Rows[0]["ClubMemberTypeDesc"].ToString();

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

                    ddlSport.SelectedValue = dt1.Rows[0]["SportID"].ToString();
                }

                

                btnUpdateClubMemberType.Visible = true;
                btnSaveClubMemberType.Visible = false;
                pnlClubMemberTypeEntry.Visible = true;
                pnlClubMemberTypeGrid.Visible = false;
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
            txtClubMemberTypeValue.Text = "";
            txtClubMemberTypeDesc.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
        }

    }
}