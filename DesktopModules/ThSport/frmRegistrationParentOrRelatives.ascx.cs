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
    public partial class frmRegistrationParentOrRelatives : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        int RegistrationId
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["RegistrationId"] != null))
                {
                    int.TryParse(Request.QueryString["RegistrationId"].ToString(), out retVal);
                    return retVal;
                }
                return 0;
            }
        }

        public string entry_mode_masterplayer
        {
            get
            {
                if ((Request.QueryString["entry_mode_masterplayer"] != null))
                {
                    return Request.QueryString["entry_mode_masterplayer"].ToString();
                }
                return "";
            }
        }

        clsRegistrationParentsOrRelatives cc = new clsRegistrationParentsOrRelatives();
        clsRegistrationParentsOrRelativesController ccc = new clsRegistrationParentsOrRelativesController();

        #region Page events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (entry_mode_masterplayer == "editmasterplayer")
            {
                pnlEntryRegistrationParents.Visible = true;
                PnlGridRegistrationParents.Visible = false;
            }
            FillUserNamePrintGrid();
            if (!IsPostBack)
            {
                FillUserType();
                FillParents();
                FillGridView();
            }
            FillUserNamePrintGrid();

        }

        #endregion

        private void FillUserNamePrintGrid()
        {
            DataTable dt = new DataTable();
            dt = ccc.GetUserNameByRegistrantionID(RegistrationId);

            if (dt.Rows.Count > 0)
            {
                lbl_Club_Member.Text = dt.Rows[0]["UserName"].ToString();
            }
        }

        #region Grid Editing Related Events

        protected void BindGrid()
        {
            FillGridView();
        }

        #endregion

        private void FillGridView()
        {
            DataTable dt = new DataTable();

            //if (currentUser.IsSuperUser || currentUser.IsInRole("Club Admin"))
            //{
                dt = ccc.GetRegistrationParentDetail(RegistrationId);
            //}

            if (dt.Rows.Count > 0)
            {
                gvRegistrationParents.DataSource = dt;
                gvRegistrationParents.DataBind();
            }
        }

        private void FillUserType()
        {
            DataTable dt = new DataTable();
            dt = ccc.GetUserType();
            if (dt.Rows.Count > 0)
            {
                ddlUserType.DataSource = dt;
                ddlUserType.DataTextField = "UserTypeName";
                ddlUserType.DataValueField = "UserTypeId";
                ddlUserType.DataBind();
                ddlUserType.Items.Insert(0, new ListItem("-- Select User Type --", "0"));
            }
        }

        private void FillParents()
        {
            DataTable dt = new DataTable();
            dt = ccc.GetRegistrationParentsDetails();
            if (dt.Rows.Count > 0)
            {
                ddlSelectPerson.DataSource = dt;
                ddlSelectPerson.DataTextField = "ParentsName";
                ddlSelectPerson.DataValueField = "ParentsID";
                ddlSelectPerson.DataBind();
                ddlSelectPerson.Items.Insert(0, new ListItem("-- Select Parent's / Relatives --", "0"));
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            funClearData();
        }

        private void funClearData()
        {
            FillUserType();
            FillParents();
        }

        protected void btnSaveRegistrationParents_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            cc.RegistrationId = RegistrationId;
            cc.UserTypeID = Convert.ToInt32(ddlUserType.SelectedValue);
            cc.UserIdRelated = Convert.ToInt32(ddlSelectPerson.SelectedValue);

            ccc.InsertRegistrationParents(cc);

            ccc.UpdateAdminIDForRegistrationForm(cc);

            pnlEntryRegistrationParents.Visible = false;
            PnlGridRegistrationParents.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void btnAddRegistrationParents_Click(object sender, EventArgs e)
        {
            funClearData();
            pnlEntryRegistrationParents.Visible = true;
            PnlGridRegistrationParents.Visible = false;
            btnSaveRegistrationParents.Visible = true;
            btnUpdateRegistrationParents.Visible = false;
            FillUserType();
            FillParents();
        }

        protected void btnCloseRegistrationParents_Click(object sender, EventArgs e)
        {
            pnlEntryRegistrationParents.Visible = false;
            PnlGridRegistrationParents.Visible = true;
            FillGridView();
        }

        protected void btnUpdateRegistrationParents_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully()", true);

            cc.RegisteredUserRelationId = Convert.ToInt32(hidRegID.Value);
            cc.RegistrationId = RegistrationId;
            cc.UserTypeID = Convert.ToInt32(ddlUserType.SelectedValue);
            cc.UserIdRelated = Convert.ToInt32(ddlSelectPerson.SelectedValue);

            int userid = ccc.UpdateRegistrationParents(cc);

            pnlEntryRegistrationParents.Visible = false;
            PnlGridRegistrationParents.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void gvRegistrationParents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRegistrationParents.PageIndex = e.NewPageIndex;
            FillGridView();
        }

        protected void lbAddNewPerson_Click(object sender, EventArgs e)
        {
            //FillGridView();
            Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmRegistration" + "&entry_mode_masterplayer=editmasterplayer"));
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + ""));
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionRegisteredUserRelationId")).Text;
            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                funClearData();
                int UserRelationID = 0;
                int.TryParse(str, out UserRelationID);

                LinkButton btn = sender as LinkButton;

                clsRegistrationParentsOrRelatives cc = new clsRegistrationParentsOrRelatives();
                clsRegistrationParentsOrRelativesController ccc = new clsRegistrationParentsOrRelativesController();

                DataTable dt = new DataTable();

                dt = ccc.GetRegistationParentsDetailsByParentID(UserRelationID);

                if (dt.Rows.Count > 0)
                {
                    hidRegID.Value = dt.Rows[0]["RegisteredUserRelationId"].ToString();
                    ddlSelectPerson.SelectedValue = dt.Rows[0]["UserIdRelated"].ToString();
                    ddlUserType.SelectedValue = dt.Rows[0]["UserTypeID"].ToString();

                    pnlEntryRegistrationParents.Visible = true;
                    PnlGridRegistrationParents.Visible = false;
                    btnUpdateRegistrationParents.Visible = true;
                    btnSaveRegistrationParents.Visible = false;
                }
            }
            else if (ddlSelectedValue == "Delete")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "javascript:confirm('Are You Sure? Want To Delete.');", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", "javascript:Confirmation();", true);
                //int competition_Id = 0;
                //int.TryParse(str, out competition_Id);
                //CompRegInfo.DeleteCompetitionReg(competition_Id);
                //FillGridView();
            }
        }
    }
}