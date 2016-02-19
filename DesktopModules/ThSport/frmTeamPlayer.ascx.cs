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
            else if (ddlSelectedValue == "Transfer")
            {
                int TeamPlayerID = 0;
                int.TryParse(str, out TeamPlayerID);
                ViewState["Store_MasterPlayerID"] = TeamPlayerID;

                clsTeamPlayer ctmpc = new clsTeamPlayer();
                clsTeamPlayerController ctmpcc = new clsTeamPlayerController();
                DataTable dt = new DataTable();

                dt = ctmpcc.GetTransferPlayerToOtherTeam(TeamPlayerID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    pnlSelectTeam.Visible = true;
                    ddlSelectTeam.DataSource = dt;
                    ddlSelectTeam.DataTextField = "TeamName";
                    ddlSelectTeam.DataValueField = "TeamID";
                    ddlSelectTeam.DataBind();
                    ddlSelectTeam.Items.Insert(0, new ListItem("-- Select Team --", "0"));
                }
                else
                {
                    pnlSelectTeam.Visible = false;
                }

                DataTable dtpname = ctmpcc.GetPlayerNameByPlayerID(TeamPlayerID);

                if (dtpname.Rows.Count > 0)
                {
                    txtSelectPlayerName.Text = dtpname.Rows[0]["PlayerName"].ToString();
                }

                DataTable dt2 = ctmpcc.GetTransferPlayerToOtherTeam(TeamPlayerID);
                if (dt2.Rows.Count > 0)
                {
                    int spoid = 0;
                    int.TryParse(dt.Rows[0]["SportId"].ToString(), out spoid);
                    FillPlayerPosition(spoid);
                }

                pnlEntryTeamPlayer.Visible = false;
                pnlGridTeamPlayer.Visible = false;
                pnlSelectTeam.Visible = true;
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

        private void FillPlayerPosition(int spoid)
        {
            clsTeamPlayerController ccmcc = new clsTeamPlayerController();
            DataTable dt = new DataTable();
            dt = ccmcc.GetPlayerAllPositionList(spoid);
            if (dt.Rows.Count > 0)
            {
                ddlSelectPosition.DataSource = dt;
                ddlSelectPosition.DataTextField = "PlayerTypeName";
                ddlSelectPosition.DataValueField = "PlayerTypeID";
                ddlSelectPosition.DataBind();
                ddlSelectPosition.Items.Insert(0, new ListItem("-- Select Position --", "0"));
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

        protected void btnConformTeam_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "transferSuccessfully();", true);

            // Player ID Store
            int masterplayerid;
            int.TryParse(ViewState["Store_MasterPlayerID"].ToString(), out masterplayerid);

            // Player Transfer Team Master ID
            int selectedteamid = 0;
            int.TryParse(ddlSelectTeam.SelectedValue, out selectedteamid);

            clsTeamPlayer ctmpc = new clsTeamPlayer();
            clsTeamPlayerController ctmpcc = new clsTeamPlayerController();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            //Player Transfer Data Store In Table
            // Get Player Details            

            dt2 = ctmpcc.GetPlayerDetailsBySelectedPlayerID(masterplayerid);

            if (dt2.Rows.Count > 0)
            {
                ctmpc.PlayerID = Convert.ToInt32(dt2.Rows[0]["PlayerID"].ToString());
                ctmpc.TOutID = Convert.ToInt32(dt2.Rows[0]["TeamID"].ToString());
                ctmpc.TOutName = dt2.Rows[0]["TeamName"].ToString();
                ctmpc.TInID = selectedteamid;
                ctmpc.TInName = ddlSelectTeam.SelectedItem.ToString();
                ctmpc.PortalID = PortalId;
                ctmpc.CreatedById = currentUser.Username;
                ctmpc.ModifiedById = currentUser.Username;
                ctmpc.PlayerPosition = dt2.Rows[0]["PlayerPostition"].ToString();
            }

            ctmpcc.InsertTeamPlayerTransfer(ctmpc);

            // Competition Condition Close

            // Delete Match Player Performance
            ctmpcc.DeleteTransferPlayerToMatchPlayerPerformance(masterplayerid);

            dt = ctmpcc.GetMasterPlayerIDByUserID(masterplayerid);

            clsTeamPlayerController ctpcc = new clsTeamPlayerController();
            clsTeamPlayer ctpc = new clsTeamPlayer();

            dt = new clsTeamPlayerController().EditTeamMasterPlayerCoach(Convert.ToInt32(dt.Rows[0]["PlayerID"].ToString()));

            ctmpc.TeamId = selectedteamid;
            ctmpc.PlayerID = masterplayerid;
            if (ddlSelectPosition.SelectedValue == "0")
            {
                ctmpc.PlayerPosition = dt.Rows[0]["PlayerPosition"].ToString();
                ViewState["Store_PlayerPosition"] = dt.Rows[0]["PlayerPosition"].ToString();
            }
            else
            {
                ctmpc.PlayerPosition = ddlSelectPosition.SelectedItem.ToString();
                ViewState["Store_PlayerPosition"] = ddlSelectPosition.SelectedItem.ToString();
            }
            ctmpc.PortalID = PortalId;
            ctmpc.CreatedById = currentUser.Username;
            ctmpc.ModifiedById = currentUser.Username;

            int UserId = ctmpcc.InsertTeamPlayer(ctmpc);

            // Delete Master Player 
            ctmpcc.DeleteTransferPlayerToTeamPlayer(Convert.ToInt32(dt.Rows[0]["PlayerID"].ToString()));

            clsMatchResult matchResult = new clsMatchResult();
            clsMatchResultController matchResultControl = new clsMatchResultController();
            DataTable dt1 = new DataTable();

            dt1 = ctmpcc.MatchWisePlayerPerformancePlayerEntry(selectedteamid);

            if (dt1.Rows.Count != 0)
            {
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    int matchid = 0;
                    int.TryParse(dt1.Rows[j]["MatchID"].ToString(), out matchid);

                    int competitionid = 0;
                    int.TryParse(dt1.Rows[j]["CompetitionID"].ToString(), out competitionid);

                    DataTable dt3 = new DataTable();
                    dt3 = ctmpcc.GetTeamIDByTeamMasterIDandCompetitionID(selectedteamid, competitionid);

                    int TeamIDByMasterIDAndCompetitionID = Convert.ToInt32((dt3.Rows[0]["TeamID"].ToString()));

                    matchResult.CompetitionID = competitionid;
                    matchResult.MatchID = matchid;
                    matchResult.PlayerID = masterplayerid;
                    matchResult.PortalID = PortalId;
                    matchResult.CreatedBy = currentUser.Username;
                    matchResult.ModifyBy = currentUser.Username;
                    matchResult.Goal = 0;
                    matchResult.Assist = 0;
                    matchResult.IsPlayed = 1;
                    matchResult.Yellow = 0;
                    matchResult.Red = 0;
                    matchResult.TeamID = TeamIDByMasterIDAndCompetitionID;

                    matchResultControl.InsertMatchResultPlayerPerformance(matchResult);
                }
            }
            
            btnAddTeamPlayer.Visible = true;
            pnlGridTeamPlayer.Visible = true;
            pnlSelectTeam.Visible = false;
            LoadDocumentsGrid(TeamID);
        }

        protected void btnCancelTeam_Click(object sender, EventArgs e)
        {
            pnlSelectTeam.Visible = false;
            LoadDocumentsGrid(TeamID);
        }
    }
}