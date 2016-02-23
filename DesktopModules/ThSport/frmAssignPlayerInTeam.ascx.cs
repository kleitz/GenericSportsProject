using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
    public partial class frmAssignPlayerInTeam : PortalModuleBase
    {

        clsCompetitionMatch competitionMatchClass = new clsCompetitionMatch();
        clsCompetitionMatchController competitionMatchController = new clsCompetitionMatchController();

        clsMatchPlayerPerfomance matchPlayerPerfomanceClass = new clsMatchPlayerPerfomance();
        clsMatchPlayerPerfomanceController matchPlayerPerfomanceController = new clsMatchPlayerPerfomanceController();

        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public int Team_ID = 0;
        string CompetitionID
        {
            get
            {
                if (ViewState["CompetitionID"] != null)
                    return ViewState["CompetitionID"].ToString();
                return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillCompetition();
            }
        }

        protected void ddlTeamList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
          int.TryParse(ddlTeamList.SelectedValue, out Team_ID);

            FillAllMatch(Team_ID);
        }

        protected void ddlCompetition_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int compid = 0;
            int.TryParse(ddlCompetitionList.SelectedValue, out compid);
            FillTeamList(compid);
           FillAllMatch(0);
        }

        protected void gvMatch_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gridview = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnPlayerID = (HiddenField)e.Row.FindControl("hdnPlayerID");
                HyperLink hyperAssignPlayer = (HyperLink)e.Row.FindControl("hyperAssignPlayer");

                LinkButton checkall = (LinkButton)e.Row.FindControl("checkall");

                HiddenField hdnMatchID = (HiddenField)e.Row.FindControl("hdnMatchID");
                HiddenField hfComp_ID = (HiddenField)e.Row.FindControl("hfComp_ID");

                HiddenField hdnTeamID = (HiddenField)e.Row.FindControl("hdnTeamID");

                int hiddenTeamID = 0;
                int.TryParse(hdnTeamID.Value, out hiddenTeamID);

                int hiddenValueID = 0;
                int.TryParse(hdnMatchID.Value, out hiddenValueID);

                int hiddenCompId = 0;
                int.TryParse(hfComp_ID.Value, out hiddenCompId);

                //Check For Player Assigned or Not

                DataTable dtPlayer = new DataTable();
                //if (currentUser.IsInRole("TeamManager"))
                //{
                //    //dtPlayer = userControl.GetTeamPlayerDetail(TeamId, hiddenCompId);
                //}
                //else
                //{
                //    //dtPlayer = userControl.GetTeamPlayerDetail(hiddenTeamID, hiddenCompId);
                //}
                for (int i = 0; i <= dtPlayer.Rows.Count; i++)
                {
                    hyperAssignPlayer.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmTeamMatchPlayerList" + "&competition_Id=" + hiddenCompId + "&teamId=" + hiddenTeamID + "&matchId=" + hiddenValueID);
                }
            }
        }

        protected void gvMatch_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "checkall")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvrow = gvMatch.Rows[rowindex];
                int matchID = 0;
                int.TryParse(gvMatch.DataKeys[gvrow.RowIndex].Values["MatchID"].ToString(), out matchID);
                int competitionID = 0;
                int.TryParse(gvMatch.DataKeys[gvrow.RowIndex].Values["CompetitionID"].ToString(), out competitionID);
                int teamID = 0;
                int.TryParse(gvMatch.DataKeys[gvrow.RowIndex].Values["Team_ID"].ToString(), out teamID);

                AssignPlayers(teamID, competitionID, matchID);
                lblAssignStatus.Visible = true;
            }
        }

        protected void gvMatch_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMatch.PageIndex = e.NewPageIndex;
            int team = 0;
            int.TryParse(gvMatch.DataKeys[0].Values["Team_ID"].ToString(), out team);
           // FillAllMatch(team);
        }

        protected void btnAssign_OnClick(object sender, EventArgs e)
        {

            if (ddlCompetitionList.SelectedValue == "0")
            {

            }
            else
            {
               DataTable dt = new DataTable();
               int.TryParse(ddlTeamList.SelectedValue, out Team_ID);
               int competitionID = 0;
               int.TryParse(ddlCompetitionList.SelectedValue, out competitionID);

                if (ddlTeamList.SelectedIndex == 0)
                {
                    dt = new DataTable();
                   
                  foreach (ListItem team in ddlTeamList.Items)
                   {
                       int Teamid = 0;
                         int.TryParse(team.Value, out Teamid);

                         DataTable tempTable = new DataTable();
                         tempTable = competitionMatchController.GetCompetitionMatchByCompetitionIDAndTeamID(competitionID, Teamid);
                         dt.Merge(tempTable);
                  }
                }
                else
                {
                   dt = competitionMatchController.GetCompetitionMatchByCompetitionIDAndTeamID(competitionID, Team_ID);
                }

                if (dt.Rows.Count != 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        int match_id = 0;
                        int.TryParse(dt.Rows[j]["MatchID"].ToString(), out match_id);

                        int competition_id = 0;
                        int.TryParse(dt.Rows[j]["CompetitionID"].ToString(), out competition_id);

                        int team_id = 0;
                        int.TryParse(dt.Rows[j]["Team_ID"].ToString(), out team_id);

                        AssignPlayers(team_id, competition_id, match_id);
                    }

                    lblAssignStatus.Visible = true;
                }
            }

        }

        private void FillCompetition()
        {
            DataTable dt = new clsCompetitionController().GetCompetitionList();
            ddlCompetitionList.DataSource = dt;
            ddlCompetitionList.DataTextField = "CompetitionName";
            ddlCompetitionList.DataValueField = "CompetitionId";
            ddlCompetitionList.DataBind();
            ddlCompetitionList.Items.Insert(0, new ListItem("--Select Competition--", "0"));

            ddlTeamList.Items.Clear();
            ddlTeamList.Items.Insert(0, new ListItem("--Select--", "0"));
            FillAllMatch(0);
        }

        private void FillTeamList(int competitionid)
        {
            DataTable dt1 = new clsCompetitionTeamController().GetTeamsByCompetitionID(competitionid);
            DataView dv = dt1.AsDataView();
           
            ViewState["CompetitionID"] = competitionid;
            ddlTeamList.DataSource = dv.ToTable();
            ddlTeamList.DataTextField = "TeamName";
            ddlTeamList.DataValueField = "TeamID";
            ddlTeamList.DataBind();
            ddlTeamList.Items.Insert(0, new ListItem("--All Teams--", "0"));

          FillAllMatch(0);
        }
        private void FillAllMatch(int Team_ID)
        {
            lblAssignStatus.Visible = false;
            int competitionId= 0;
            int.TryParse(CompetitionID,out competitionId);
            DataTable dt = new DataTable();
            if (Team_ID > 0)
            {
                dt = new clsCompetitionMatchController().GetCompetitionMatchByCompetitionIDAndTeamID(competitionId, Team_ID);
                gvMatch.Visible = true;
                matchDetailHeader.Visible = true;
            }
            else if (Team_ID == 0)
            {
                dt = new DataTable();
            }

            gvMatch.DataSource = dt;
            gvMatch.DataBind();
        }

        private void AssignPlayers(int Team_Id, int Competition_Id, int Match_Id)
        {
            DataTable dt = new DataTable();
            dt = new clsTeamPlayerController().GetTeamPlayerDetailByTeamID(Team_Id);

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                int Player_ID = 0;
                int.TryParse(dt.Rows[i]["RegistrationId"].ToString(), out Player_ID);

                int TeamID = 0;
                int.TryParse(dt.Rows[i]["TeamID"].ToString(), out TeamID);

                DataTable dt1 = new DataTable();

                dt1 = matchPlayerPerfomanceController.GetMatchPlayerExists(Match_Id, Player_ID);

                if (dt1.Rows.Count == 0)
                {
                    matchPlayerPerfomanceClass.PlayerID = Player_ID;
                    matchPlayerPerfomanceClass.CompetitionID = Competition_Id;
                    matchPlayerPerfomanceClass.MatchId = Match_Id;
                    matchPlayerPerfomanceClass.PortalID = PortalId;
                    matchPlayerPerfomanceClass.CreatedById = currentUser.Username;
                    matchPlayerPerfomanceClass.ModifiedById = currentUser.Username;
                    matchPlayerPerfomanceClass.Goal = 0;
                    matchPlayerPerfomanceClass.Assist = 0;
                    matchPlayerPerfomanceClass.IsPlayed = 1;
                    matchPlayerPerfomanceClass.Yellow = 0;
                    matchPlayerPerfomanceClass.Red = 0;
                    matchPlayerPerfomanceClass.TeamID = TeamID;
                    matchPlayerPerfomanceController.InsertMatchPlayerPerfomance(matchPlayerPerfomanceClass);
                }
            }
        }

    }
}