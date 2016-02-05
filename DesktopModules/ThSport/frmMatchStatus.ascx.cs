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
    public partial class frmMatchStatus : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        clsSportController spController = new clsSportController();
        clsMatchStatus lClass = new clsMatchStatus();
        clsMatchStatusController lController = new clsMatchStatusController();

        #region MatchStatus

        int MatchStatus_ID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdn_MatchStatusID.Value))
                {
                    int.TryParse(hdn_MatchStatusID.Value, out id);
                }
                return id;
            }
        }

        #endregion

        #region Page events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillMatchStatusGridView();
            }
        }

        #endregion

        #region Grid Events

        

        protected void gvMatchStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMatchStatus.PageIndex = e.NewPageIndex;
            FillMatchStatusGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdn_MatchStatusID.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdnMatchStatusID")).Value;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                pnlEntry.Visible = true;
                pnlList.Visible = false;
                btnSaveMatchStatus.Visible = false;
                btnUpdateMatchStatus.Visible = true;

                ClearData();

                LinkButton btn = sender as LinkButton;

                using (DataTable dt = lController.usp_GetMatchStatusByMatchStatusId(MatchStatus_ID))
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["MatchStatusName"].ToString()))
                        {
                            txtMatchStatusName.Text = dt.Rows[0]["MatchStatusName"].ToString();
                        }
                    }
                }
            }
            else if (ddlSelectedValue == "Delete")
            {
                lController.DeleteMatchStatus(MatchStatus_ID);
                FillMatchStatusGridView();
                pnlEntry.Visible = false;
                pnlList.Visible = true;
            }
        }

        #endregion

        #region Grid Editing Related Events

        protected void BindGrid()
        {
            FillMatchStatusGridView();
        }
       

        #endregion

        #region Methods

        private void FillMatchStatusGridView()
        {
            using (DataTable dt = lController.GetAllMatchStatus())
            {
                gvMatchStatus.DataSource = dt;
                gvMatchStatus.DataBind();
            }
        }

        private void ClearData()
        {
            
            txtMatchStatusName.Text = "";
            
        }

        #endregion

        #region Button Clicks

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void lbGo_Click(object sender, EventArgs e)
        {
            FillMatchStatusGridView();
        }

        protected void btnSaveMatchStatus_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "MatchStatusSaveSuccessfully();", true);

            
            lClass.MatchStatusName = txtMatchStatusName.Text.Trim();
            lClass.CreatedById = currentUser.Username;
            lClass.ModifiedById = currentUser.Username;

            lController.InsertMatchStatus(lClass);

            pnlList.Visible = true;
            pnlEntry.Visible = false;
            FillMatchStatusGridView();
            ClearData();
        }

        protected void btnAddMatchStatus_Click(object sender, EventArgs e)
        {
            ClearData();
            pnlEntry.Visible = true;
            pnlList.Visible = false;
            btnSaveMatchStatus.Visible = true;
            btnUpdateMatchStatus.Visible = false;
            
        }

        protected void btnCloseMatchStatus_Click(object sender, EventArgs e)
        {
            pnlList.Visible = false;
            pnlEntry.Visible = true;
            FillMatchStatusGridView();
        }

        protected void btnUpdateMatchStatus_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "MatchStatusUpdateSuccessfully()", true);

            
            lClass.MatchStatusName = txtMatchStatusName.Text.Trim();
            
            lClass.ModifiedById = currentUser.Username;

            lClass.MatchStatusId = MatchStatus_ID;

            lController.UpdateMatchStatus(lClass);

            pnlEntry.Visible = false;
            pnlList.Visible = true;
            FillMatchStatusGridView();
            ClearData();
        }


        #endregion
    }
}