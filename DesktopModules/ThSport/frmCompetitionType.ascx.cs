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
    public partial class frmCompetitionType : PortalModuleBase
    {
        clsCompetitionType ccm = new clsCompetitionType();
        clsCompetitionTypeController ccmc = new clsCompetitionTypeController();

        private readonly UserInfo currentType = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region variables

        string currentId
        {
            get
            {
                if (ViewState["currentId"] != null)
                    return ViewState["currentId"].ToString();
                return null;
            }
        }

        #endregion variables

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdateCompetitionType.Visible = false;
            btnSaveCompetitionType.Visible = false;
            pnlCompetitionTypeEntry.Visible = false;

            LoadCompetitionTypeGrid();

        }

        #endregion Page Events

        #region Methods

        public void LoadCompetitionTypeGrid()
        {
            DataTable dt = new DataTable();
            dt = ccmc.GetCompetitionTypeList();

            if (dt.Rows.Count > 0)
            {
                gvCompetitionType.DataSource = dt;
                gvCompetitionType.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveCompetitionType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            ccm.CompetitionTypeName = txtCompetitionType.Text.Trim();
            ccm.CompetitionTypeDesc = txtCompetitionTypeDesc.Text.Trim();
            ccm.ActiveFlagId = Convert.ToInt32(ChkIsActive.Checked);
            ccm.ShowFlagId = Convert.ToInt32(ChkIsShow.Checked);
            ccm.PortalID = PortalId;
            ccm.CreatedById = currentType.Username;
            ccm.ModifiedById = currentType.Username;

            // Call Save Method
            ccmc.InsertCompetitionType(ccm);

            btnAddCompetitionType.Visible = true;
            pnlCompetitionTypeGrid.Visible = true;
            LoadCompetitionTypeGrid();
            ClearData();
            
        }

        protected void btnUpdateCompetitionType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);


            ccm.CompetitionTypeId = Convert.ToInt32(currentId);
            ccm.CompetitionTypeName = txtCompetitionType.Text.Trim();
            ccm.CompetitionTypeDesc = txtCompetitionTypeDesc.Text.Trim();
            ccm.ActiveFlagId = Convert.ToInt32(ChkIsActive.Checked);
            ccm.ShowFlagId = Convert.ToInt32(ChkIsShow.Checked);
            ccm.PortalID = PortalId;
            ccm.ModifiedById = currentType.Username;

            // Call Update Method
            ccmc.UpdateCompetitionType(ccm);

            btnAddCompetitionType.Visible = true;
            pnlCompetitionTypeGrid.Visible = true;
            btnSaveCompetitionType.Visible = true;
            btnUpdateCompetitionType.Visible = false;
            LoadCompetitionTypeGrid();
            ClearData();
        }

        protected void btnCancelCompetitionType_Click(object sender, EventArgs e)
        {
            pnlCompetitionTypeGrid.Visible = true;
            pnlCompetitionTypeEntry.Visible = false;
            btnSaveCompetitionType.Visible = false;
            btnUpdateCompetitionType.Visible = false;
            LoadCompetitionTypeGrid();
            ClearData();
        }

        public void ClearData()
        {
            txtCompetitionType.Text = "";
            txtCompetitionTypeDesc.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
        }

        protected void btnAddCompetitionType_Click(object sender, EventArgs e)
        {
            pnlCompetitionTypeGrid.Visible = false;
            pnlCompetitionTypeEntry.Visible = true;
            btnSaveCompetitionType.Visible = true;
            btnUpdateCompetitionType.Visible = false;
            ClearData();
        }

        #endregion Button Click Events

        #region Gridview Events

        protected void gvCompetitionType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCompetitionType.PageIndex = e.NewPageIndex;
            LoadCompetitionTypeGrid();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            int competitionTypeID = 0;
            int.TryParse(((HiddenField)((DropDownList)sender).Parent.FindControl("hdnCompetitionTypeId")).Value,out competitionTypeID);

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                ViewState["currentId"] = Convert.ToInt16(competitionTypeID);

                ClearData();
                DataTable dt1 = ccmc.GetCompetitionTypeDetailByCompetitionTypeID(competitionTypeID);

                if (dt1.Rows.Count > 0)
                {
                    txtCompetitionType.Text = dt1.Rows[0]["CompetitionTypeName"].ToString();
                    txtCompetitionTypeDesc.Text = dt1.Rows[0]["CompetitionTypeDesc"].ToString();

                    if(dt1.Rows[0]["ActiveFlagId"].ToString() == "1")
                    {
                        ChkIsActive.Checked = true;
                    }
                    if(dt1.Rows[0]["ShowFlagId"].ToString() == "1")
                    {
                        ChkIsShow.Checked = true;
                    }
                }

                btnUpdateCompetitionType.Visible = true;
                btnSaveCompetitionType.Visible = false;
                pnlCompetitionTypeEntry.Visible = true;
                pnlCompetitionTypeGrid.Visible = false;
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

        #endregion


    }
}