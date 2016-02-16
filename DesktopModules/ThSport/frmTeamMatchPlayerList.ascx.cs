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
    public partial class frmTeamMatchPlayerList : PortalModuleBase
    {
        clsMatchPlayerPerfomance cmpClass = new clsMatchPlayerPerfomance();
        clsMatchPlayerPerfomanceController cmpController = new clsMatchPlayerPerfomanceController();

        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region variables

        int matchId
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["matchId"] != null))
                {
                    int.TryParse(Request.QueryString["matchId"].ToString(), out retVal);
                }

                return retVal;
            }
        }

        int competition_Id
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["competition_Id"] != null))
                {
                    int.TryParse(Request.QueryString["competition_Id"].ToString(), out retVal);
                }

                return retVal;
            }
        }

        int teamId
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["teamId"] != null))
                {
                    int.TryParse(Request.QueryString["teamId"].ToString(), out retVal);
                }

                return retVal;
            }
        }

        #endregion variables

        protected void Page_Load(object sender, EventArgs e)
        {
            FillPlayersGrid();
        }

        private void FillPlayersGrid()
        {
            DataTable dt = new DataTable();

            dt =new  clsTeamPlayerController().GetTeamPlayerDetailByTeamID(teamId);

            if (dt.Rows.Count == 0)
            {
                lblNoData.Text = "No Data Available";
                lblNoData.Visible = true;
                noDatastatus.Attributes.Add("class", "alert alert-success");
            }
            else
            {
                gvTeamMatchPlayerList.DataSource = dt;
                gvTeamMatchPlayerList.DataBind();
            }
        }

        protected void checkAllPlayer_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAll = (CheckBox)sender;

            foreach (GridViewRow row in gvTeamMatchPlayerList.Rows)
            {
                HiddenField hdnPlayerID = (HiddenField)row.FindControl("hdnPlayerID");

                CheckBox chkview = (CheckBox)row.FindControl("chkview");

                cmpClass.CompetitionID = competition_Id;
                cmpClass.MatchId = matchId;

                int Player_ID = 0;
                int.TryParse(hdnPlayerID.Value, out Player_ID);
                cmpClass.PlayerID = Player_ID;

                if (chkAll.Checked == true)
                {
                    if (chkview.Checked == false)
                    {
                        cmpClass.PortalID = PortalId;
                        cmpClass.PortalID = PortalId;
                        cmpClass.CreatedById = currentUser.Username;
                        cmpClass.ModifiedById = currentUser.Username;
                        cmpClass.Goal = 0;
                        cmpClass.Assist = 0;
                        cmpClass.IsPlayed = 1;
                        cmpClass.Yellow = 0;
                        cmpClass.Red = 0;
                        cmpController. InsertMatchPlayerPerfomance(cmpClass);
                        chkview.Checked = true;
                    }
                }
                else
                {
                    if (chkview.Checked == true)
                    {
                        cmpController.DeleteMatchPlayerPerformance(matchId, Player_ID);
                        FillPlayersGrid();
                        chkview.Checked = false;
                    }
                }
            }
            //GridViewRow gr = (GridViewRow)chkAll.Parent.Parent;
        }

        protected void chkview_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chk.Parent.Parent;

            if (chk.Checked == true)
            {
                cmpClass.CompetitionID = competition_Id;
                cmpClass.MatchId = matchId;

                int player_id = 0;
                int.TryParse(((HiddenField)gr.FindControl("hdnPlayerID")).Value, out player_id);
                cmpClass.TeamID = teamId;
                cmpClass.PlayerID = player_id;
                cmpClass.PortalID = PortalId;
                cmpClass.CreatedById = currentUser.Username;
                cmpClass.ModifiedById = currentUser.Username;
                cmpClass.Goal = 0;
                cmpClass.Assist = 0;
                cmpClass.IsPlayed = 1;
                cmpClass.Yellow = 0;
                cmpClass.Red = 0;
                cmpClass.PlayerSuspended = 0;
                cmpController.InsertMatchPlayerPerfomance(cmpClass);
            }
            else
            {
                cmpClass.CompetitionID = competition_Id;
                cmpClass.MatchId = matchId;

                int player_id = 0;
                int.TryParse(((HiddenField)gr.FindControl("hdnPlayerID")).Value, out player_id);

                cmpClass.PlayerID = player_id;

                cmpController.DeleteMatchPlayerPerformance(matchId, player_id);
                FillPlayersGrid();
            }
        }
        protected void gvTeamMatchPlayerList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gridview = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnPlayerID = (HiddenField)e.Row.FindControl("hdnPlayerID");
                DataTable dt1 = new DataTable();
                int hiddenPlayerID = 0;
                int.TryParse(hdnPlayerID.Value, out hiddenPlayerID);

                dt1 = cmpController.GetMatchPlayerExists(matchId, hiddenPlayerID);

                if (dt1.Rows.Count != 0)
                {
                    CheckBox chkview = (CheckBox)e.Row.FindControl("chkview");
                    chkview.Checked = true;
                    //chkview.Enabled = false;
                }
            }
        }
        protected void gvTeamMatchPlayerList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTeamMatchPlayerList.PageIndex = e.NewPageIndex;
            FillPlayersGrid();
        }

    }
}