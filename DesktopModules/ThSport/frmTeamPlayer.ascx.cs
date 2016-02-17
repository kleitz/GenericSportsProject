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
    public partial class frmTeamPlayer : PortalModuleBase
    {
        clsTeamPlayer ccm = new clsTeamPlayer();
        clsTeamPlayerController ccmc = new clsTeamPlayerController();

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

        string regiId
        {
            get
            {
                if (ViewState["regiId"] != null)
                    return ViewState["regiId"].ToString();
                return null;
            }
        }

        #region variables

        int TeamID
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["TeamID"] != null))
                {
                    int.TryParse(Request.QueryString["TeamID"].ToString(), out retVal);
                    return retVal;
                }
                return 0;
            }
        }

        #endregion variables

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (TeamID == null || TeamID == 0) return;
            btnUpdateTeamPlayer.Visible = false;
            btnSaveTeamPlayer.Visible = false;
            pnlEntryTeamPlayer.Visible = false;

            if (TeamID != 0)
            {
                LoadDocumentsGrid(TeamID);
                //FillPlayerType();
                //FillPlayer();
                FillTeamName();
            }
        }

        private void FillTeamName()
        {
            clsTeamPlayer ccm = new clsTeamPlayer();
            clsTeamPlayerController ccmc = new clsTeamPlayerController();

            DataTable dt = new DataTable();
            dt = ccmc.GetTeamNameByTeamID(TeamID);

            if (dt.Rows.Count > 0)
            {
                lbl_Team_Player.Text = dt.Rows[0]["TeamName"].ToString();
            }
        }

        protected void btnUpdateTeamPlayer_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            clsTeamPlayer ccm = new clsTeamPlayer();
            clsTeamPlayerController ccmc = new clsTeamPlayerController();

            ccm.PlayerID = Convert.ToInt32(currentId);
            ccm.TeamId = TeamID;
            ccm.RegistrationId = Convert.ToInt32(regiId);
            ccm.PlayerTypeId = Convert.ToInt32(ddlPlayerType.SelectedValue);
            if (txtPlayerJerseyNo.Text == " ")
            {
                ccm.PlayerJerseyNo = 0;
            }
            else 
            {
                ccm.PlayerJerseyNo = Convert.ToInt32(txtPlayerJerseyNo.Text);
            }
            
            ccm.PlayerJerseyName = txtPlayerJerseyName.Text.Trim();
            ccm.PlayerFamousName = txtPlayerFamousname.Text.Trim();

            ccm.PortalID = PortalId;
            ccm.ModifiedById = currentUser.Username;

            // Call Update Method
            ccmc.UpdateTeamPlayer(ccm);

            btnAddTeamPlayer.Visible = true;
            pnlGridTeamPlayer.Visible = true;
            btnSaveTeamPlayer.Visible = true;
            btnUpdateTeamPlayer.Visible = false;
            FillTeamName();
            LoadDocumentsGrid(TeamID);
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        #endregion Page Events

        #region Methods

        public void LoadDocumentsGrid(int TeamID)
        {
            DataTable dt = new DataTable();
            clsTeamPlayerController ccmc = new clsTeamPlayerController();

            dt = ccmc.GetTeamPlayerListByTeamID(TeamID);

            if (dt.Rows.Count > 0)
            {
                gvTeamPlayer.DataSource = dt;
                gvTeamPlayer.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveTeamPlayer_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            clsTeamPlayer ccm = new clsTeamPlayer();
            clsTeamPlayerController ccmc = new clsTeamPlayerController();

            ccm.TeamId = TeamID;
            ccm.RegistrationId = Convert.ToInt32(ddlSelectPlayer.SelectedValue);

            if (txtPlayerJerseyNo.Text == " ")
            {
                ccm.PlayerJerseyNo = 0;
            }
            else 
            {
                ccm.PlayerJerseyNo = Convert.ToInt32(txtPlayerJerseyNo.Text);
            }

            ccm.PlayerJerseyName = txtPlayerJerseyName.Text.Trim();
            ccm.PlayerFamousName = txtPlayerFamousname.Text.Trim();
            ccm.PlayerTypeId = Convert.ToInt32(ddlPlayerType.SelectedValue);
            ccm.PortalID = PortalId;
            ccm.CreatedById = currentUser.Username;
            ccm.ModifiedById = currentUser.Username;

            // Call Save Method
            ccmc.InsertTeamPlayer(ccm);

            btnAddTeamPlayer.Visible = true;
            pnlGridTeamPlayer.Visible = true;
            FillTeamName();
            LoadDocumentsGrid(TeamID);
            ClearData();
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
        }

        protected void btnCancelTeamPlayer_Click(object sender, EventArgs e)
        {
            pnlGridTeamPlayer.Visible = true;
            pnlEntryTeamPlayer.Visible = false;
            btnSaveTeamPlayer.Visible = false;
            btnUpdateTeamPlayer.Visible = false;
            LoadDocumentsGrid(TeamID);
            ClearData();
        }

        #endregion Button Click Events

        protected void gvTeamPlayer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTeamPlayer.PageIndex = e.NewPageIndex;
            LoadDocumentsGrid(TeamID);
        }

        protected void btnAddTeamPlayer_Click(object sender, EventArgs e)
        {
            pnlGridTeamPlayer.Visible = false;
            pnlEntryTeamPlayer.Visible = true;
            btnSaveTeamPlayer.Visible = true;
            btnUpdateTeamPlayer.Visible = false;
            FillTeamName();
            FillPlayerType();
            FillPlayer();
            ClearData();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionPlayerId")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                FillAllPlayer();
                ddlSelectPlayer.Enabled = false;
                int editid = 0;
                int.TryParse(str, out editid);
                ViewState["currentId"] = Convert.ToInt16(str);

                clsTeamPlayer ccm = new clsTeamPlayer();
                clsTeamPlayerController ccmc = new clsTeamPlayerController();

                DataTable dt = new DataTable();
                dt = ccmc.GetTeamNameByTeamID(TeamID);
                if (dt.Rows.Count > 0)
                {
                    lbl_Team_Player.Text = dt.Rows[0]["TeamName"].ToString();
                }

                FillPlayerType();
               
                ClearData();
                DataTable dt1 = new clsTeamPlayerController().GetPlayerDetailByPlayerID(editid);

                if (dt1.Rows.Count > 0)
                {
                    ViewState["currentId"] = Convert.ToInt32(dt1.Rows[0]["PlayerId"].ToString());
                    ViewState["regiId"] = Convert.ToInt32(dt1.Rows[0]["RegistrationId"].ToString());
                    ddlSelectPlayer.SelectedValue = dt1.Rows[0]["RegistrationId"].ToString();
                    txtPlayerJerseyNo.Text = dt1.Rows[0]["PlayerJerseyNo"].ToString();
                    txtPlayerJerseyName.Text = dt1.Rows[0]["PlayerJerseyName"].ToString();
                    txtPlayerFamousname.Text = dt1.Rows[0]["PlayerFamousName"].ToString();
                    ddlPlayerType.SelectedValue = dt1.Rows[0]["PlayerTypeId"].ToString();
                }

                btnUpdateTeamPlayer.Visible = true;
                btnSaveTeamPlayer.Visible = false;
                pnlEntryTeamPlayer.Visible = true;
                pnlGridTeamPlayer.Visible = false;
            }
            else if (ddlSelectedValue == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);

                hidRegID.Value = str;

                clsTeamPlayer tp = new clsTeamPlayer();
                clsTeamPlayerController tpc = new clsTeamPlayerController();

                DataTable dt = new DataTable();

                // Delete Player in Match Player Performance 
                tpc.DeleteTransferPlayerToMatchPlayerPerformance(Convert.ToInt32(str));

                // Delete Player in Team 
                int PlayerID = 0;
                int.TryParse(hidRegID.Value, out PlayerID);
                new clsTeamPlayerController().DeleteTeamPlayer(PlayerID);

                LoadDocumentsGrid(TeamID);
            }
        }

        protected void btnGoToBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmTeam"));
        }

        public void ClearData()
        {
            txtPlayerJerseyNo.Text = "";
            txtPlayerJerseyName.Text = "";
            txtPlayerFamousname.Text = "";
        }

        public void FillPlayerType()
        {
            clsTeamPlayer ccm = new clsTeamPlayer();
            clsTeamPlayerController ccmc = new clsTeamPlayerController();
            DataTable dt = new DataTable();

            dt = ccmc.GetTeamPlayerType();
            if (dt.Rows.Count > 0)
            {
                ddlPlayerType.DataSource = dt;
                ddlPlayerType.DataTextField = "PlayerTypeName";
                ddlPlayerType.DataValueField = "PlayerTypeID";
                ddlPlayerType.DataBind();
                ddlPlayerType.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillPlayer()
        {
            clsTeamPlayer ccm = new clsTeamPlayer();
            clsTeamPlayerController ccmc = new clsTeamPlayerController();
            DataTable dt = new DataTable();

            dt = ccmc.GetPlayerList();
            if (dt.Rows.Count > 0)
            {
                ddlSelectPlayer.DataSource = dt;
                ddlSelectPlayer.DataTextField = "PlayerName";
                ddlSelectPlayer.DataValueField = "RegistrationId";
                ddlSelectPlayer.DataBind();
                ddlSelectPlayer.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillAllPlayer()
        {
            clsTeamPlayer ccm = new clsTeamPlayer();
            clsTeamPlayerController ccmc = new clsTeamPlayerController();
            DataTable dt = new DataTable();

            dt = ccmc.GetAllPlayerList();
            if (dt.Rows.Count > 0)
            {
                ddlSelectPlayer.DataSource = dt;
                ddlSelectPlayer.DataTextField = "PlayerName";
                ddlSelectPlayer.DataValueField = "RegistrationId";
                ddlSelectPlayer.DataBind();
                ddlSelectPlayer.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
    }
}