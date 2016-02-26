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
    public partial class frmMatchType : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        clsSportController spController = new clsSportController();
        clsMatchType lClass = new clsMatchType();
        clsMatchTypeController lController = new clsMatchTypeController();

        clsPlayerType ccm = new clsPlayerType();
        clsPlayerTypeController ccmc = new clsPlayerTypeController();


        #region MatchType

        int MatchType_ID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdn_MatchTypeID.Value))
                {
                    int.TryParse(hdn_MatchTypeID.Value, out id);
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
                FillMatchTypeGridView();
            }
        }

        #endregion

        #region Grid Events



        protected void gvMatchType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMatchType.PageIndex = e.NewPageIndex;
            FillMatchTypeGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdn_MatchTypeID.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdnMatchTypeID")).Value;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                pnlEntry.Visible = true;
                pnlList.Visible = false;
                btnSaveMatchType.Visible = false;
                btnUpdateMatchType.Visible = true;

                ClearData();
                FillSport();
                LinkButton btn = sender as LinkButton;

                using (DataTable dt = lController.usp_GetMatchTypeByMatchTypeId(MatchType_ID))
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["MatchTypeName"].ToString()))
                        {
                            txtMatchTypeName.Text = dt.Rows[0]["MatchTypeName"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["MatchDesc"].ToString()))
                        {
                            txtMatchTypeDescription.Text = dt.Rows[0]["MatchDesc"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dt.Rows[0]["SportID"].ToString()))
                        {
                            ddlSport.SelectedValue = dt.Rows[0]["SportID"].ToString();
                        }
                    }
                }
            }
            else if (ddlSelectedValue == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);
                lController.DeleteMatchType(MatchType_ID);
                FillMatchTypeGridView();
                pnlEntry.Visible = false;
                pnlList.Visible = true;
            }
        }

        #endregion

        #region Grid Editing Related Events

        protected void BindGrid()
        {
            FillMatchTypeGridView();
        }


        #endregion

        #region Methods

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

        private void FillMatchTypeGridView()
        {
            using (DataTable dt = lController.GetAllMatchType())
            {
                gvMatchType.DataSource = dt;
                gvMatchType.DataBind();
            }
        }

        private void ClearData()
        {

            txtMatchTypeName.Text = "";
            txtMatchTypeDescription.Text = "";

        }

        #endregion

        #region Button Clicks

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void lbGo_Click(object sender, EventArgs e)
        {
            FillMatchTypeGridView();
        }

        protected void btnSaveMatchType_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "MatchTypeSaveSuccessfully();", true);


                lClass.MatchTypeName = txtMatchTypeName.Text.Trim();
                lClass.MatchTypeDescription = txtMatchTypeDescription.Text;
                lClass.PortalID = currentUser.PortalID;
                lClass.CreatedById = currentUser.Username;
                lClass.ModifiedById = currentUser.Username;
                lClass.SportID = Convert.ToInt32(ddlSport.SelectedValue);
                lController.InsertMatchType(lClass);

                pnlList.Visible = true;
                pnlEntry.Visible = false;
                FillMatchTypeGridView();
                ClearData();
            }
        }

        protected void btnAddMatchType_Click(object sender, EventArgs e)
        {
            ClearData();
            pnlEntry.Visible = true;
            pnlList.Visible = false;
            btnSaveMatchType.Visible = true;
            btnUpdateMatchType.Visible = false;
            FillSport();

        }

        protected void btnCloseMatchType_Click(object sender, EventArgs e)
        {
            pnlList.Visible = true;
            pnlEntry.Visible = false;
            FillMatchTypeGridView();
        }

        protected void btnUpdateMatchType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "MatchTypeUpdateSuccessfully()", true);


            lClass.MatchTypeName = txtMatchTypeName.Text.Trim();

            lClass.MatchTypeDescription = txtMatchTypeDescription.Text;

            lClass.ModifiedById = currentUser.Username;

            lClass.MatchTypeId = MatchType_ID;
            lClass.SportID = Convert.ToInt32(ddlSport.SelectedValue);
            lController.UpdateMatchType(lClass);

            pnlEntry.Visible = false;
            pnlList.Visible = true;
            FillMatchTypeGridView();
            ClearData();
        }


        #endregion

    }
}