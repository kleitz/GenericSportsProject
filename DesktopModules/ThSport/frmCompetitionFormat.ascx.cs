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
    public partial class frmCompetitionFormat : PortalModuleBase
    {
        clsCompetitionFormat cf = new clsCompetitionFormat();
        clsCompetitionFormatController cfc = new clsCompetitionFormatController();

        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region variables

        int competitionformatID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdnCompetitionFormatId.Value))
                {
                    int.TryParse(hdnCompetitionFormatId.Value, out id);
                }
                return id;
            }
        }

        #endregion variables

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdateCompetitionFormat.Visible = false;
            btnSaveCompetitionFormat.Visible = false;
            pnlCompetitionFormatEntry.Visible = false;

            LoadCompetitionFormatGrid();

        }

        #endregion Page Events

        #region Methods

        public void LoadCompetitionFormatGrid()
        {
            DataTable dt = new DataTable();
            dt = cfc.GetCompetitionFormatList();

            gvCompetitionFormat.DataSource = dt;
            gvCompetitionFormat.DataBind();
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveCompetitionFormat_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            cf.CompetitionFormatName = txtCompetitionFormat.Text.Trim();
            cf.CompetitionFormatDesc = txtCompetitionFormatDesc.Text.Trim();

            cf.PortalID = PortalId;

            // Call Save Method
            cfc.InsertCompetitionFormat(cf);

            btnAddCompetitionFormat.Visible = true;
            pnlCompetitionFormatGrid.Visible = true;
            LoadCompetitionFormatGrid();
            ClearData();

        }

        protected void btnUpdateCompetitionFormat_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            cf.CompetitionFormatId = competitionformatID;
            cf.CompetitionFormatName = txtCompetitionFormat.Text.Trim();
            cf.CompetitionFormatDesc = txtCompetitionFormatDesc.Text.Trim();

            cf.PortalID = PortalId;

            // Call Update Method
            cfc.UpdateCompetitionFormat(cf);

            btnAddCompetitionFormat.Visible = true;
            pnlCompetitionFormatGrid.Visible = true;
            btnSaveCompetitionFormat.Visible = true;
            btnUpdateCompetitionFormat.Visible = false;
            LoadCompetitionFormatGrid();
            ClearData();
        }

        protected void btnCancelCompetitionFormat_Click(object sender, EventArgs e)
        {
            pnlCompetitionFormatGrid.Visible = true;
            pnlCompetitionFormatEntry.Visible = false;
            btnSaveCompetitionFormat.Visible = false;
            btnUpdateCompetitionFormat.Visible = false;
            LoadCompetitionFormatGrid();
            ClearData();
        }

        public void ClearData()
        {
            txtCompetitionFormat.Text = "";
            txtCompetitionFormatDesc.Text = "";
        }

        protected void btnAddCompetitionFormat_Click(object sender, EventArgs e)
        {
            pnlCompetitionFormatGrid.Visible = false;
            pnlCompetitionFormatEntry.Visible = true;
            btnSaveCompetitionFormat.Visible = true;
            btnUpdateCompetitionFormat.Visible = false;
            ClearData();
        }

        #endregion Button Click Events

        #region Gridview Events

        protected void gvCompetitionFormat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCompetitionFormat.PageIndex = e.NewPageIndex;
            LoadCompetitionFormatGrid();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnCompetitionFormatId.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdn_CompetitionFormat_Id")).Value;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                ClearData();
                DataTable dt1 = cfc.GetCompetitionFormatDetailByCompetitionFormatID(competitionformatID);

                if (dt1.Rows.Count > 0)
                {
                    txtCompetitionFormat.Text = dt1.Rows[0]["CompetitionFormatName"].ToString();
                    txtCompetitionFormatDesc.Text = dt1.Rows[0]["CompetitionFormatDesc"].ToString();

                    btnUpdateCompetitionFormat.Visible = true;
                    btnSaveCompetitionFormat.Visible = false;
                    pnlCompetitionFormatEntry.Visible = true;
                    pnlCompetitionFormatGrid.Visible = false;
                }
            }
            else if (ddlSelectedValue == "Delete")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetFormat(), "alert", "DeleteSuccessfully();", true);
                //int documentid = 0;
                //int.TryParse(str, out documentid);
                //new CompetitionSponsorController().DeleteCompeSpon(documentid);
                //LoadDocumentsGrid(CompetitionID);
            }
        }

        #endregion
    }
}