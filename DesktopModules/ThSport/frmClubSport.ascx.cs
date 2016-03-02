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
    public partial class frmClubSport : PortalModuleBase
    {
        clsClubSport objClsSport = new clsClubSport();
        clsClubSportController objClsSportController = new clsClubSportController();

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
            ClearData();
            FillClubName();
            btnUpdateClubSport.Visible = false;
            btnSaveClubSport.Visible = false;
            pnlClubSportEntry.Visible = false;

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
                txtClubName.Text = dt.Rows[0]["ClubName"].ToString();
            }
        }

        protected void btnUpdateClubSport_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            //clsClubSport cco = new clsClubSport();
            //clsClubSportController ccoc = new clsClubSportController();

            //cco.ClubSportsId = Convert.ToInt32(currentId);
            //cco.ClubId = ClubID;
            //cco.RegistrationId = Convert.ToInt32(regid);
            //cco.OwnerDescription = txtClubSportDescription.Text.Trim();

            //if (txtClubSportPercentage.Text == "")
            //{
            //    txtClubSportPercentage.Text = "0";
            //}
            //else
            //{
            //    cco.OwnerPercentage = Convert.ToInt32(txtClubSportPercentage.Text.Trim());
            //}

            //cco.PortalID = PortalId;
            //cco.ModifiedById = currentUser.Username;

            //// Call Update Method
            //ccoc.UpdateClubSport(cco);

            //btnAddClubSport.Visible = true;
            //pnlClubSportGrid.Visible = true;
            //btnSaveClubSport.Visible = true;
            //btnUpdateClubSport.Visible = false;
            //FillClubName();
            //LoadDocumentsGrid(ClubID);
            //ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubSport", "ClubID=" + ClubID));
        }

        #endregion Page Events

        #region Methods

        public void LoadDocumentsGrid(int ClubID)
        {
            DataTable dt = new DataTable();
            

            dt = objClsSportController.GetClubSportListByClubId(ClubID);

            //if (dt.Rows.Count > 0)
            //{
                gvClubSport.DataSource = dt;
                gvClubSport.DataBind();
            //}
        }

        private void SaveSport()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            foreach (RepeaterItem i in rptrForSports.Items)
            {
                HiddenField hdnSportID = (HiddenField)i.FindControl("hdnSportID");
                CheckBox chk_Assign_sport = (CheckBox)i.FindControl("chk_Assign_sport");

                if (chk_Assign_sport.Checked)
                {
                    Boolean FileOK = false;
                    Boolean FileSaved = false;
                    int group_id = 0;
                    objClsSport.ClubId = ClubID;
                    int TeamID = 0;
                    int.TryParse(hdnSportID.Value, out TeamID);
                    objClsSport.SportID = TeamID;
                    objClsSport.PortalID = currentUser.PortalID;
                    objClsSport.CreatedById = currentUser.Username;
                    objClsSportController.InsertClubSports(objClsSport);
                }
            }

            LoadDocumentsGrid(ClubID);
        }


        #endregion Methods

        #region Button Click Events

        protected void btnSaveClubSport_Click(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            SaveSport();
            btnAddClubSport.Visible = true;
            pnlClubSportGrid.Visible = true;
            //FillClubName();
            LoadDocumentsGrid(ClubID);
            //ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubSport", "ClubID=" + ClubID));
        }

        protected void btnCancelClubSport_Click(object sender, EventArgs e)
        {
            pnlClubSportGrid.Visible = true;
            pnlClubSportEntry.Visible = false;
            btnSaveClubSport.Visible = false;
            btnUpdateClubSport.Visible = false;
            LoadDocumentsGrid(ClubID);
            //ClearData();
        }

        #endregion Button Click Events

        protected void gvClubSport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClubSport.PageIndex = e.NewPageIndex;
            LoadDocumentsGrid(ClubID);
        }

        protected void btnAddClubSport_Click(object sender, EventArgs e)
        {
            pnlClubSportGrid.Visible = false;
            pnlClubSportEntry.Visible = true;
            btnSaveClubSport.Visible = true;
            btnUpdateClubSport.Visible = false;
            FillClubSport();
            ClearData();
        }

        public void FillClubSport()
        {
            DataTable dt = new DataTable();
            clsClubSportController ccoc = new clsClubSportController();

            dt = ccoc.GetSportNotAssignByClubId(ClubID);

            if (dt.Rows.Count > 0)
            {
                rptrForSports.Visible = true;
                rptrForSports.DataSource = dt;
                rptrForSports.DataBind();
            }
            else
            {
                rptrForSports.Visible = false;
            }

        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionClubSportsId")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Delete")
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);
                int clubSportID = 0;
                int.TryParse(str, out clubSportID);

                objClsSportController.DeleteClubSports(clubSportID);
                LoadDocumentsGrid(ClubID);
            }
        }

        protected void btnGoToBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClub"));
        }

        public void ClearData()
        {
            //txtClubSportDescription.Text = "";
            //txtClubSportPercentage.Text = "";
        }

    }
}