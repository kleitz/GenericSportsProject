using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Globalization;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework;
using DotNetNuke.Entities.Modules;
using System.Web.UI.HtmlControls;
using ThSportServer;

namespace DotNetNuke.Modules.ThSport
{
    public partial class MatchResult : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();
        clsCompetitionMatch cmClass = new clsCompetitionMatch();
        clsCompetitionMatchController cmControl = new clsCompetitionMatchController();
        clsTeamPlayer tpClass = new clsTeamPlayer();
        clsTeamPlayerController tpControl = new clsTeamPlayerController();
        clsMatchPlayerPerfomance mpClass = new clsMatchPlayerPerfomance();
        clsMatchPlayerPerfomanceController mpControl = new clsMatchPlayerPerfomanceController();
        clsMatchResult matchResultClass = new clsMatchResult();
        clsMatchResultController matchResultControl = new clsMatchResultController();

        //clsMatchResult mr = new clsMatchResult();
        //clsPlayerHistory playerhistory = new clsPlayerHistory();
        //clsPlayerHistoryController playerController = new clsPlayerHistoryController();
        clsCompetitionPlayerPerfomance clsCompetitionPlayer = new clsCompetitionPlayerPerfomance();
        clsCompetitionPlayerPerfomanceController competitionPlayerController = new clsCompetitionPlayerPerfomanceController();
        //clsPlayerMultiplePhotoUploadController PlayerPhotoController = new clsPlayerMultiplePhotoUploadController();
        //clsPlayerMultiplePhotoUpload PlayerPhoto = new clsPlayerMultiplePhotoUpload();

        public DataTable player_performance_data = new DataTable();

        #region variables

        int MatchID
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["MatchID"] != null))
                {
                    int.TryParse(Request.QueryString["MatchID"].ToString(), out retVal);
                    return retVal;
                }
                return 0;
            }
        }

        string currentId
        {
            get
            {
                if (ViewState["currentId"] != null)
                    return ViewState["currentId"].ToString();
                return null;
            }
        }

        string TeamAID
        {
            get
            {
                if (ViewState["TeamAID"] != null)
                    return ViewState["TeamAID"].ToString();
                return null;
            }
        }

        string TeamBID
        {
            get
            {
                if (ViewState["TeamBID"] != null)
                    return ViewState["TeamBID"].ToString();
                return null;
            }
        }

        string teamAtotalgoal
        {
            get
            {
                if (ViewState["teamAgoal"] != null)
                    return ViewState["teamAgoal"].ToString();
                return null;
            }
            set
            {
                ViewState["teamAgoal"] = value;
            }
        }

        string teamBtotalgoal
        {
            get
            {
                if (ViewState["teamBgoal"] != null)
                    return ViewState["teamBgoal"].ToString();
                return null;
            }
            set
            {
                ViewState["teamBgoal"] = value;
            }
        }

        int CompetitionID
        {
            get
            {
                if (ViewState["CompetitionID"] != null)
                    return Convert.ToInt32(ViewState["CompetitionID"]);
                return 0;
            }
            set
            {
                ViewState["CompetitionID"] = value;
            }
        }

        int page_index
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["page_index"] != null))
                {
                    int.TryParse(Request.QueryString["page_index"].ToString(), out retVal);
                    return retVal;
                }

                return 0;
            }
        }

        #endregion variables

        #region Page Events

        private void Page_Init(object sender, EventArgs e)
        {
            ServicesFramework.Instance.RequestAjaxScriptSupport();
            ServicesFramework.Instance.RequestAjaxAntiForgerySupport();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdteama.Value))
            {
                txtTeamATotal.Text = hdteama.Value;
            }
            if (!string.IsNullOrEmpty(hdteamb.Value))
            {
                txtTeamBTotal.Text = hdteamb.Value;
            }
            if (!Page.IsPostBack)
            {
                DataTable compAndteamdt = cmControl.GetCompetitionAndTeamDetaibyMatchID(MatchID);                                                                                                                                       
                if (compAndteamdt.Rows.Count > 0)
                {
                    CompetitionID = Convert.ToInt32(compAndteamdt.Rows[0]["CompetitionID"]);
                    hdnCompetitionID.Value = CompetitionID.ToString();
                }

                if (!string.IsNullOrEmpty(compAndteamdt.Rows[0]["NoShowGoal"].ToString()))
                {
                    hdnNoShowGoal.Value = compAndteamdt.Rows[0]["NoShowGoal"].ToString();
                }

                txtTeamATotal.Text = hdteama.Value;      
                txtTeamBTotal.Text = hdteamb.Value;
                lblTeamAName.Text = compAndteamdt.Rows[0]["TeamAName"].ToString();
                lblTeamBName.Text = compAndteamdt.Rows[0]["TeamBName"].ToString();
                this.rdbtoss.Items[0].Text = compAndteamdt.Rows[0]["TeamAName"].ToString();
                this.rdbtoss.Items[1].Text = compAndteamdt.Rows[0]["TeamBName"].ToString();
                ViewState["TeamAID"] = compAndteamdt.Rows[0]["TeamAID"].ToString();
                ViewState["TeamBID"] = compAndteamdt.Rows[0]["TeamBID"].ToString();
                foreach (ListItem item in rdbtoss.Items)
                {
                    item.Attributes.Add("class", "radio line radio_height");
                }

                FillTeamAPlayer();
                FillTeamBPlayer();

                reloaddata();
                //ckpanlty.Enabled = false;
                Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: disablecheck(1); ", true);
            }
            else
            {
                txtTeamATotal.Text = hdteama.Value;
                txtTeamBTotal.Text = hdteamb.Value;
                Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: disablecheck(0); ", true);
            }

            hdnModuleId.Value = ModuleId.ToString();
        }

        #endregion Page Events

        #region RepeaterA Events

        protected void rptleftTeamA_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string fuA = "";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton downSwap = (LinkButton)e.Item.FindControl("downSwap");
                //downSwap.Attributes.Add("onclick", "javascript:return ConfirmationBox('')");

                HiddenField lblPlayerID = (HiddenField)e.Item.FindControl("lblPlayerID");
                HtmlGenericControl div = (HtmlGenericControl)e.Item.FindControl("dialogA");
                div.ID = div.ID + lblPlayerID.Value;

                int IsSuspended = 0;

                int hdnplayerId = 0;
                int.TryParse(lblPlayerID.Value, out hdnplayerId);

                int SuspentionCount = 0;

                int no_of_yellow = 0;
                int no_of_red = 0;

                player_performance_data = mpControl.GetSuspendedFlag(CompetitionID, hdnplayerId);

                if (player_performance_data.Rows.Count > 0)
                {
                    int.TryParse(player_performance_data.Rows[0]["PlayerSuspended"].ToString(), out SuspentionCount);
                    int.TryParse(player_performance_data.Rows[0]["Yellow"].ToString(), out no_of_yellow);
                    int.TryParse(player_performance_data.Rows[0]["Red"].ToString(), out no_of_red);
                   // IsSuspended = mrc.GetPlayerPerformanceSuspendedFlag(MatchID, CompetitionID, hdnplayerId);
                    if (SuspentionCount > 0)
                    {
                        fuA += " disableRepeaterRow('teamAtable" + e.Item.ItemIndex + "'); ";
                    }
                }
            }

            if (!string.IsNullOrEmpty(fuA))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Javascript", "$(document).ready(function(){" + fuA + " });", true);
            }
        }


        protected void rptrTeamA1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                HiddenField hdnFlag = (HiddenField)e.Item.FindControl("hdnFlag");
                LinkButton upSwap = (LinkButton)e.Item.FindControl("upSwap");


                //if (hdnFlag.Value == "1")
                //{
                //    upSwap.Enabled = false;
                //}
                //else
                //{
                //    upSwap.Enabled = true;
                //}

            }
        }


        protected void rptleftTeamA_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SwapDownPlayer")
            {
                int match_player_performance_id = 0;
                int.TryParse(e.CommandArgument.ToString(), out match_player_performance_id);

                mpControl.UpdatePlayerPerformanceFlag(match_player_performance_id, 0);

                mpClass.CompetitionID = CompetitionID;
                mpClass.MatchId = MatchID;

                int player_id = Convert.ToInt32(e.CommandArgument);
            }

            //else if (e.CommandName == "updatecards")
            //{
            //    TextBox txtNoteA = (TextBox)e.Item.FindControl("txtNoteA");
            //    TextBox txtteamAassist = (TextBox)e.Item.FindControl("txtteamAassist");
            //    TextBox txtPlayerGoalA = (TextBox)e.Item.FindControl("txtPlayerGoalA");
            //    TextBox txtAongoal = (TextBox)e.Item.FindControl("txtAongoal");
            //    DropDownList dlYellowA = (DropDownList)e.Item.FindControl("dlYellowA");
            //    CheckBox chkIsRedA = (CheckBox)e.Item.FindControl("chkIsRedA");
            //    HiddenField hfId = (HiddenField)e.Item.FindControl("hfId");

            //    int match_player_performance_id = 0;

            //    if (!string.IsNullOrEmpty(hfId.Value))
            //    {
            //        int.TryParse(hfId.Value, out match_player_performance_id);
            //        mpClass.MatchPlayerPerfomanceID = match_player_performance_id;
            //    }

            //    int player_id = Convert.ToInt32(e.CommandArgument);

            //    mpClass.CompetitionID = CompetitionID;
            //    mpClass.MatchId = MatchID;
            //    mpClass.PlayerID = player_id;

            //    int resVal = 0;
            //    int.TryParse(txtAongoal.Text, out resVal);
            //    mpClass.OwnGoal = resVal;

            //    resVal = 0;
            //    int.TryParse(txtPlayerGoalA.Text, out resVal);
            //    mpClass.Goal = resVal;

            //    resVal = 0;
            //    int.TryParse(txtteamAassist.Text, out resVal);
            //    mpClass.Assist = resVal;

            //    mpClass.CreatedById = mpClass.ModifiedById = currentUser.Username;
            //    mpClass.PortalID = PortalId;

            //    DataTable playerteamDetail = new clsCompetitionMatchController().GetTeamByCompetitionIdAndMatchID(MatchID, CompetitionID);

            //    int team_id = 0;
            //    int.TryParse(playerteamDetail.Rows[0]["TeamAID"].ToString(), out team_id);


            //    mpClass.CompetitionID = CompetitionID;
            //    mpClass.MatchId = MatchID;
            //    mpClass.TeamID = team_id;
            //    mpClass.TeamID = team_id;
            //    mpClass.PlayerID  = player_id;

            //    string CardName = "";

            //    if (!string.IsNullOrEmpty(hdnCardName.Value))
            //    {
            //        CardName = hdnCardName.Value;
            //        mpClass.CardName = CardName;
            //    }


            //    mpClass.CreatedById = mpClass.ModifiedById =  currentUser.Username;
            //    mpClass.PortalID =  PortalId;

            //    mpClass.Remark = txtNoteA.Text;

            //    int Selected_Yellow_Card = 0;
            //    int Previous_Yellow_Card = 0;
            //    int Current_Yellow_Card = 0;

            //    int.TryParse(dlYellowA.SelectedValue, out Selected_Yellow_Card);

            //    if (CardName == "Red")
            //    {
            //        mpClass.Red = 1;
            //    }
            //    else if (CardName == "Yellow")
            //    {

            //        DataTable yellow_red_Detail = mpControl.GetSuspendedFlag(CompetitionID, player_id);

            //        if (Selected_Yellow_Card == 1)
            //        {
            //            mpClass.Red = 0;
            //            mpClass.PlayerSuspended = 0;
            //        //DataTable yellow_red_Detail = competitionPlayerController.GetCompetitionPlayerPerfomanceData(CompetitionID, player_id);

            //        //if (Selected_Yellow_Card == 1)
            //        //{
            //        //    clsCompetitionPlayer.NoOfRedCard = clsCompetitionPlayer.NoOfRedCardTheirYellowCard = 0;
            //        //    clsCompetitionPlayer.Suspended = 0;

            //            //if (yellow_red_Detail.Rows.Count == 0)
            //            //{
            //            //    clsCompetitionPlayer.NoOfYellowCard = Selected_Yellow_Card;
            //            //    competitionPlayerController.InsertCompetitionPlayerPerfomance(clsCompetitionPlayer);
            //            //}
            //            //else
            //            //{
            //                int.TryParse(yellow_red_Detail.Rows[0]["Yellow"].ToString(), out Previous_Yellow_Card);

            //                Current_Yellow_Card = Previous_Yellow_Card + Selected_Yellow_Card;

            //                mpClass.Yellow= Current_Yellow_Card;

            //                //competitionPlayerController.UpdateCompetitionPlayerPerfomance(clsCompetitionPlayer);
            //            //}
            //        }

            //    }


            //    if (Current_Yellow_Card > 0)
            //    {
            //        mpClass.Yellow = Current_Yellow_Card;
            //    }
            //    else
            //    {
            //        mpClass.Yellow = Selected_Yellow_Card;
            //    }

            //    mpClass.PlayerSuspended = 0;

            //    if (chkIsRedA.Checked)
            //    {
            //        mpClass.Red = 1;
            //    }
            //    else
            //    {
            //        mpClass.Red = 0;
            //    }

            //    //Save/Update Match Player Performance Entry
            //    //mrc.UpdateMatchResultPlayerPerformance(mr);

            //    if (mpClass.Red == 1 || Selected_Yellow_Card == 2 || Current_Yellow_Card == 3)
            //    {
            //        if (!chkIsRedA.Checked && Current_Yellow_Card != 3)
            //        {
            //            mpClass.Yellow = 1;
            //        }

            //        if (Selected_Yellow_Card == 2)
            //        {
            //            mpClass.Yellow = Selected_Yellow_Card;
            //        }

            //        else if (Current_Yellow_Card == 3)
            //        {
            //            mpClass.Yellow = Current_Yellow_Card;
            //        }

            //        DataTable dt1 = mpControl.GetSuspendedFlag(CompetitionID, player_id);

            //        //DataTable dt1 = competitionPlayerController.GetCompetitionPlayerPerfomanceData(CompetitionID, player_id);
            //        //if (dt1.Rows.Count == 0)
            //        //{
            //        //    clsCompetitionPlayer.Suspended = 1;

            //        //    competitionPlayerController.InsertCompetitionPlayerPerfomance(clsCompetitionPlayer);
            //        //}
            //        //else
            //        //{
            //        if (dt1.Rows[0]["PlayerSuspended"].ToString() == "0")
            //        {
            //            mpClass.PlayerSuspended = 1;
            //            mpControl.UpdateSuspendedCount(mpClass.PlayerSuspended, player_id, CompetitionID);
            //        }

            //        mpControl.UpdatePlayerPerformance(mpClass);
            //        mpControl.InsertPlayerCardDetail(mpClass);
            //    }
            //    else
            //    {
            //        mpClass.IsPlayed = 1;
            //        mpControl.UpdatePlayerPerformance(mpClass);
            //        mpControl.InsertPlayerCardDetail(mpClass);
            //    }

               
            //}
            else if (e.CommandName == "updatecards")
            {
                TextBox txtNoteA = (TextBox)e.Item.FindControl("txtNoteA");
                TextBox txtteamAassist = (TextBox)e.Item.FindControl("txtteamAassist");
                TextBox txtPlayerGoalA = (TextBox)e.Item.FindControl("txtPlayerGoalA");
                TextBox txtAongoal = (TextBox)e.Item.FindControl("txtAongoal");
                DropDownList dlYellowA = (DropDownList)e.Item.FindControl("dlYellowA");
                CheckBox chkIsRedA = (CheckBox)e.Item.FindControl("chkIsRedA");
                HiddenField hfId = (HiddenField)e.Item.FindControl("hfId");

                int match_player_performance_id = 0;

                if (!string.IsNullOrEmpty(hfId.Value))
                {
                    int.TryParse(hfId.Value, out match_player_performance_id);
                   mpClass. MatchPlayerPerfomanceID = match_player_performance_id;
                }

                int player_id = Convert.ToInt32(e.CommandArgument);

                mpClass.CompetitionID = CompetitionID;
                mpClass.MatchId = MatchID;
                mpClass.PlayerID = player_id;

                int resVal = 0;
                int.TryParse(txtAongoal.Text, out resVal);
                mpClass.OwnGoal = resVal;

                resVal = 0;
                int.TryParse(txtPlayerGoalA.Text, out resVal);
                mpClass.Goal = resVal;

                resVal = 0;
                int.TryParse(txtteamAassist.Text, out resVal);
                mpClass.Assist = resVal;

                mpClass.CreatedById = mpClass.ModifiedById = currentUser.Username;
                mpClass.PortalID = PortalId;

                //DataTable playerteamDetail = mrc.GetCompetitionIDbyMatchID(MatchID);
                DataTable playerteamDetail = new clsCompetitionMatchController().GetTeamByCompetitionIdAndMatchID(MatchID, CompetitionID);

                int team_id = 0;
                int.TryParse(playerteamDetail.Rows[0]["TeamAID"].ToString(), out team_id);


                clsCompetitionPlayer.CompetitionID = CompetitionID;
                mpClass.MatchId = MatchID;
                mpClass.TeamID = team_id;
                mpClass.TeamID = team_id;
                clsCompetitionPlayer.PlayerID = mpClass.PlayerID = player_id;

                string CardName = "";

                if (!string.IsNullOrEmpty(hdnCardName.Value))
                {
                    CardName = hdnCardName.Value;
                    mpClass.CardName = CardName;
                }


                clsCompetitionPlayer.CreatedBy = clsCompetitionPlayer.ModifyBy = mpClass.CreatedById = mpClass.ModifiedById = currentUser.Username;
                clsCompetitionPlayer.PortalID = mpClass.PortalID = PortalId;

                mpClass.Remark = txtNoteA.Text;

                int Selected_Yellow_Card = 0;
                int Previous_Yellow_Card = 0;
                int Current_Yellow_Card = 0;

                int.TryParse(dlYellowA.SelectedValue, out Selected_Yellow_Card);

                if (CardName == "Red")
                {
                    clsCompetitionPlayer.NoOfRedCard = 1;
                }
                else if (CardName == "Yellow")
                {
                    DataTable yellow_red_Detail = competitionPlayerController.GetCompetitionPlayerPerfomanceData(CompetitionID, player_id);

                    if (Selected_Yellow_Card == 1)
                    {
                        clsCompetitionPlayer.NoOfRedCard = clsCompetitionPlayer.NoOfRedCardTheirYellowCard = 0;
                        clsCompetitionPlayer.Suspended = 0;

                        if (yellow_red_Detail.Rows.Count == 0)
                        {
                            clsCompetitionPlayer.NoOfYellowCard = Selected_Yellow_Card;
                            competitionPlayerController.InsertCompetitionPlayerPerfomance(clsCompetitionPlayer);
                        }
                        else
                        {
                            int.TryParse(yellow_red_Detail.Rows[0]["NoOfYellowCard"].ToString(), out Previous_Yellow_Card);

                            Current_Yellow_Card = Previous_Yellow_Card + Selected_Yellow_Card;

                            clsCompetitionPlayer.NoOfYellowCard = Current_Yellow_Card;

                            competitionPlayerController.UpdateCompetitionPlayerPerfomance(clsCompetitionPlayer);
                        }
                    }

                }

                mpClass.Yellow = Selected_Yellow_Card;

                mpClass.PlayerSuspended = 0;

                if (chkIsRedA.Checked)
                {
                    mpClass.Red = 1;
                }
                else
                {
                    mpClass.Red = 0;
                }

                //Save/Update Match Player Performance Entry
                mpControl.UpdateMatchResultPlayerPerformance(mpClass);

                if (mpClass.Red == 1 || Selected_Yellow_Card == 2 || Current_Yellow_Card == 3)
                {
                    if (!chkIsRedA.Checked && Current_Yellow_Card != 3)
                    {
                        clsCompetitionPlayer.NoOfRedCardTheirYellowCard = 1;
                    }

                    if (Selected_Yellow_Card == 2)
                    {
                        clsCompetitionPlayer.NoOfYellowCard = Selected_Yellow_Card;
                    }

                    else if (Current_Yellow_Card == 3)
                    {
                        clsCompetitionPlayer.NoOfYellowCard = Current_Yellow_Card;
                    }


                    DataTable dt1 = competitionPlayerController.GetCompetitionPlayerPerfomanceData(CompetitionID, player_id);
                    if (dt1.Rows.Count == 0)
                    {
                        clsCompetitionPlayer.Suspended = 1;

                        competitionPlayerController.InsertCompetitionPlayerPerfomance(clsCompetitionPlayer);
                    }
                    else
                    {
                        if (dt1.Rows[0]["Suspended"].ToString() == "0")
                        {
                            clsCompetitionPlayer.Suspended = 1;
                            mpClass.PlayerSuspended = 1;
                            competitionPlayerController.UpdateSuspendedCount(clsCompetitionPlayer.Suspended, player_id, CompetitionID);
                            //mpControl.UpdateSuspendedCount(mpClass.PlayerSuspended, player_id, CompetitionID);
                        }

                        competitionPlayerController.UpdateCompetitionPlayerPerfomance(clsCompetitionPlayer);
                    }

                }

                mpControl.InsertPlayerCardDetail(mpClass);
            }
            FillTeamAPlayer();
            FillTeamBPlayer();
            reloaddata();
        }

        protected void rptrTeamA1_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SwapUpPlayer")
            {
                mpClass.CompetitionID = CompetitionID;
                mpClass.MatchId = MatchID;

                int player_id = Convert.ToInt32(e.CommandArgument);

                mpClass.PlayerID = player_id;
                mpClass.PortalID = PortalId;
                mpClass.CreatedById = currentUser.Username;
                mpClass.ModifiedById = currentUser.Username;
                mpClass.Goal = 0;
                mpClass.Assist = 0;
                mpClass.IsPlayed = 1;
                mpClass.Yellow = 0;
                mpClass.Red = 0;


                DataTable dt3 = new DataTable();
                dt3 =  new clsCompetitionMatchController().GetTeamByCompetitionIdAndMatchID(MatchID, CompetitionID);
                int TeamIDByMasterIDAndCompetitionID = Convert.ToInt32((dt3.Rows[0]["TeamAID"].ToString()));

               mpClass.TeamID = TeamIDByMasterIDAndCompetitionID;

               using (DataTable dtForPerformance = mpControl.GetMatchPlayerExists(MatchID, player_id))
                {
                    if (dtForPerformance.Rows.Count > 0)
                    {
                        //If Exists in Performance 

                        int performance_id = 0;
                        int.TryParse(dtForPerformance.Rows[0]["MatchPlayerPerfomanceID"].ToString(), out performance_id);
                        mpControl.UpdatePlayerPerformanceFlag(performance_id, 1);
                    }
                    else
                    {
                        // Not Exists then Add in Performance
                        mpControl.InsertMatchPlayerPerfomance(mpClass);
                    }
                }


                FillTeamAPlayer();
                reloaddata();
            }
        }

        protected void rdbteamA_OnSelectedIndexChanged(Object sender, EventArgs e)
        {
            if (rdbteamA.SelectedIndex == 0)
            {
                rdbteamb.SelectedIndex = 1;
            }
            else if (rdbteamA.SelectedIndex == 1)
                rdbteamb.SelectedIndex = 0;
            else
                rdbteamb.SelectedIndex = 2;
        }

        protected void txtPlayerGoalA_OnTextChanged(object sender, EventArgs e)
        {
            txtTeamATotal.Text = "";
            TextBox tb1 = ((TextBox)(sender));

            RepeaterItem rp1 = ((RepeaterItem)(tb1.NamingContainer));

            foreach (RepeaterItem i in rptleftTeamA.Items)
            {
                TextBox txtExample = (TextBox)i.FindControl("txtPlayerGoalA");
                if (txtExample != null)
                {
                    txtTeamATotal.Text = (Convert.ToInt32(txtExample.Text == "" ? 0 : Convert.ToInt32(txtExample.Text)) + (Convert.ToInt32(txtTeamATotal.Text == "" ? 0 : Convert.ToInt32(txtTeamATotal.Text)))).ToString();

                }
            }
        }


        #endregion RepeaterA Events

        #region RepeaterB Events

        protected void rptrTeamB1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                HiddenField hdnFlag = (HiddenField)e.Item.FindControl("hdnFlag");
                LinkButton upSwap = (LinkButton)e.Item.FindControl("upSwap");



                //if (hdnFlag.Value == "1")
                //{
                //    upSwap.Enabled = false;
                //}
                //else
                //{
                //    upSwap.Enabled = true;
                //}

            }
        }

        protected void rptrightTeamB_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string fu = "";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton downSwap = (LinkButton)e.Item.FindControl("downSwap");
                //downSwap.Attributes.Add("onclick", "javascript:return ConfirmationBox('')");

                HiddenField lblPlayerID = (HiddenField)e.Item.FindControl("lblPlayerID");
                HtmlGenericControl div = (HtmlGenericControl)e.Item.FindControl("dialogB");
                div.ID = div.ID + lblPlayerID.Value;

                int IsSuspended = 0;

                int hdnplayerId = 0;
                int.TryParse(lblPlayerID.Value, out hdnplayerId);

                int SuspentionCount = 0;
                int no_of_yellow = 0;
                int no_of_red = 0;

                player_performance_data = mpControl.GetSuspendedFlag(CompetitionID, hdnplayerId);

                if (player_performance_data.Rows.Count > 0)
                {
                   
                    int.TryParse(player_performance_data.Rows[0]["PlayerSuspended"].ToString(), out SuspentionCount);
                    int.TryParse(player_performance_data.Rows[0]["Yellow"].ToString(), out no_of_yellow);
                    int.TryParse(player_performance_data.Rows[0]["Red"].ToString(), out no_of_red);

                    if (SuspentionCount > 0 )
                    {
                        fu += " disableRepeaterRow('teamBtable" + e.Item.ItemIndex + "'); ";
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "<script language='javascript'>disableRepeaterRow('teamBtable" + e.Item.ItemIndex + "');</script>");
                    }
                }
            }

            if (!string.IsNullOrEmpty(fu))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Javascript", "$(document).ready(function(){ " + fu + " });", true);
            }
        }

        protected void rptrightTeamB_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SwapDownPlayer")
            {
                int match_player_performance_id = 0;
                int.TryParse(e.CommandArgument.ToString(), out match_player_performance_id);

                mpControl.UpdatePlayerPerformanceFlag(match_player_performance_id, 0);

                mpClass.CompetitionID = CompetitionID;
                mpClass.MatchId = MatchID;

                int player_id = Convert.ToInt32(e.CommandArgument);

            }
            //else if (e.CommandName == "updatecards")
            //{
            //    TextBox txtNoteB = (TextBox)e.Item.FindControl("txtNoteB");
            //    TextBox txtteamBassist = (TextBox)e.Item.FindControl("txtteamBassist");
            //    TextBox txtPlayerGoalB = (TextBox)e.Item.FindControl("txtPlayerGoalB");
            //    TextBox txtBongoal = (TextBox)e.Item.FindControl("txtBongoal");
            //    DropDownList dlYellowB = (DropDownList)e.Item.FindControl("dlYellowB");
            //    CheckBox chkIsRedB = (CheckBox)e.Item.FindControl("chkIsRedB");
            //    HiddenField rptrBhfId = (HiddenField)e.Item.FindControl("rptrBhfId");

            //    HiddenField lblPlayerID = (HiddenField)e.Item.FindControl("lblPlayerID");

            //    int match_player_performance_id = 0;

            //    if (!string.IsNullOrEmpty(rptrBhfId.Value))
            //    {
            //        int.TryParse(rptrBhfId.Value, out match_player_performance_id);
            //        mpClass.MatchPlayerPerfomanceID = match_player_performance_id;
            //    }

            //    int player_id = Convert.ToInt32(e.CommandArgument);

            //    mpClass.CompetitionID = CompetitionID;
            //    mpClass.MatchId = MatchID;
            //    mpClass.PlayerID = player_id;

            //    int resVal = 0;
            //    int.TryParse(txtBongoal.Text, out resVal);
            //    mpClass.OwnGoal = resVal;

            //    resVal = 0;
            //    int.TryParse(txtPlayerGoalB.Text, out resVal);
            //    mpClass.Goal = resVal;

            //    resVal = 0;
            //    int.TryParse(txtteamBassist.Text, out resVal);
            //    mpClass.Assist = resVal;

            //    mpClass.CreatedById = mpClass.ModifiedById = currentUser.Username;
            //    mpClass.PortalID = PortalId;

            //    DataTable playerteamDetail = cmControl.GetCompetitionAndTeamDetaibyMatchID(MatchID); ;

            //    int team_id = 0;
            //    int.TryParse(playerteamDetail.Rows[0]["TeamBID"].ToString(), out team_id);

            //    mpClass.TeamID = team_id;
            //    //clsCompetitionPlayer.CompetitionID = playerhistory.CompetitionID = CompetitionID;
            //    //playerhistory.MatchID = MatchID;
            //    //playerhistory.TeamID = team_id;
            //    //mr.TeamID = team_id;
            //    //clsCompetitionPlayer.PlayerID = playerhistory.PlayerID = player_id;

            //    string CardName = "";

            //    if (!string.IsNullOrEmpty(hdnCardName.Value))
            //    {
            //        CardName = hdnCardName.Value;
            //        mpClass.CardName = CardName;
            //    }


            //    mpClass.CreatedById = mpClass.ModifiedById =currentUser.Username;
            //    mpClass.PortalID =  PortalId;

            //    mpClass.Remark = txtNoteB.Text;

            //    int Selected_Yellow_Card = 0;
            //    int.TryParse(dlYellowB.SelectedValue, out Selected_Yellow_Card);


            //    int Previous_Yellow_Card = 0;
            //    int Current_Yellow_Card = 0;

            //    if (CardName == "Red")
            //    {
            //        mpClass.Red = 1;
            //    }
            //    else if (CardName == "Yellow")
            //    {
            //        DataTable yellow_red_Detail = mpControl.GetSuspendedFlag(CompetitionID, player_id);

            //        if (Selected_Yellow_Card == 1)
            //        {
            //            mpClass.Red = 0;
            //            mpClass.PlayerSuspended = 0;

            //            //if (yellow_red_Detail.Rows.Count == 0)
            //            //{
            //            //    mpClass.Yellow = Selected_Yellow_Card;
            //            //    mpControl.InsertMatchPlayerPerfomance(mpClass);
            //            //}
            //            //else
            //            //{
            //                int.TryParse(yellow_red_Detail.Rows[0]["Yellow"].ToString(), out Previous_Yellow_Card);

            //                Current_Yellow_Card = Previous_Yellow_Card + Selected_Yellow_Card;

            //                //mpClass.Yellow = Current_Yellow_Card;

            //               // mpControl.UpdatePlayerPerformance(mpClass);
            //                //mpClass.Yellow = Current_Yellow_Card;
            //              //  mpControl.InsertPlayerCardDetail(mpClass);
            //        //    }
            //        }

            //    }

              

            //    mpClass.PlayerSuspended = 0;

            //    if (Current_Yellow_Card > 0)
            //    {
            //        mpClass.Yellow = Current_Yellow_Card;
            //    }
            //    else
            //    {
            //        mpClass.Yellow = Selected_Yellow_Card;
            //    }

            //    if (chkIsRedB.Checked)
            //    {
            //        mpClass.Red = 1;
            //    }
            //    else
            //    {
            //        mpClass.Red = 0;
            //    }

            //    //Save/Update Match Player Performance Entry
            //   // mrc.UpdateMatchResultPlayerPerformance(mr);

            //    if (mpClass.Red == 1 || Selected_Yellow_Card == 2 || Current_Yellow_Card == 3)
            //    {
            //        if (!chkIsRedB.Checked && Current_Yellow_Card != 3)
            //        {
            //            mpClass.Red = 1;
            //        }

            //        if (Selected_Yellow_Card == 2)
            //        {
            //            mpClass.Yellow = Selected_Yellow_Card;
            //        }

            //        else if (Current_Yellow_Card == 3)
            //        {
            //            mpClass.Yellow = Current_Yellow_Card;
            //        }


            //        DataTable dt1 = mpControl.GetSuspendedFlag(CompetitionID, player_id);
            //        //if (dt1.Rows.Count == 0)
            //        //{
            //        //    clsCompetitionPlayer.Suspended = 1;

            //        //    competitionPlayerController.InsertCompetitionPlayerPerfomance(clsCompetitionPlayer);
            //        //}
            //        //else
            //        //{
            //        if (dt1.Rows[0]["PlayerSuspended"].ToString() == "0")
            //        {
            //            mpClass.PlayerSuspended = 1;
                      
            //        }
            //        mpControl.UpdatePlayerPerformance(mpClass);
            //        mpControl.InsertPlayerCardDetail(mpClass);
            //        //}

            //    }
            //    else
            //    {
            //        mpClass.IsPlayed = 1;
            //        mpControl.UpdatePlayerPerformance(mpClass);
                    
            //        mpControl.InsertPlayerCardDetail(mpClass);

            //    }


            //}

            else if (e.CommandName == "updatecards")
            {
                TextBox txtNoteB = (TextBox)e.Item.FindControl("txtNoteB");
                TextBox txtteamBassist = (TextBox)e.Item.FindControl("txtteamBassist");
                TextBox txtPlayerGoalB = (TextBox)e.Item.FindControl("txtPlayerGoalB");
                TextBox txtBongoal = (TextBox)e.Item.FindControl("txtBongoal");
                DropDownList dlYellowB = (DropDownList)e.Item.FindControl("dlYellowB");
                CheckBox chkIsRedB = (CheckBox)e.Item.FindControl("chkIsRedB");
                HiddenField rptrBhfId = (HiddenField)e.Item.FindControl("rptrBhfId");

                HiddenField lblPlayerID = (HiddenField)e.Item.FindControl("lblPlayerID");

                int match_player_performance_id = 0;

                if (!string.IsNullOrEmpty(rptrBhfId.Value))
                {
                    int.TryParse(rptrBhfId.Value, out match_player_performance_id);
                    mpClass.MatchPlayerPerfomanceID = match_player_performance_id;
                }

                int player_id = Convert.ToInt32(e.CommandArgument);

                mpClass.CompetitionID = CompetitionID;
                mpClass.MatchId = MatchID;
                mpClass.PlayerID = player_id;

                int resVal = 0;
                int.TryParse(txtNoteB.Text, out resVal);
                mpClass.OwnGoal = resVal;

                resVal = 0;
                int.TryParse(txtPlayerGoalB.Text, out resVal);
                mpClass.Goal = resVal;

                resVal = 0;
                int.TryParse(txtteamBassist.Text, out resVal);
                mpClass.Assist = resVal;

                mpClass.CreatedById = mpClass.ModifiedById = currentUser.Username;
                mpClass.PortalID = PortalId;

                //DataTable playerteamDetail = mrc.GetCompetitionIDbyMatchID(MatchID);
                DataTable playerteamDetail = new clsCompetitionMatchController().GetTeamByCompetitionIdAndMatchID(MatchID, CompetitionID);

                int team_id = 0;
                int.TryParse(playerteamDetail.Rows[0]["TeamBID"].ToString(), out team_id);


                clsCompetitionPlayer.CompetitionID = CompetitionID;
                mpClass.MatchId = MatchID;
                mpClass.TeamID = team_id;
                mpClass.TeamID = team_id;
                clsCompetitionPlayer.PlayerID = mpClass.PlayerID = player_id;

                string CardName = "";

                if (!string.IsNullOrEmpty(hdnCardName.Value))
                {
                    CardName = hdnCardName.Value;
                    mpClass.CardName = CardName;
                }


                clsCompetitionPlayer.CreatedBy = clsCompetitionPlayer.ModifyBy = mpClass.CreatedById = mpClass.ModifiedById = currentUser.Username;
                clsCompetitionPlayer.PortalID = mpClass.PortalID = PortalId;

                mpClass.Remark = txtNoteB.Text;

                int Selected_Yellow_Card = 0;
                int Previous_Yellow_Card = 0;
                int Current_Yellow_Card = 0;

                int.TryParse(dlYellowB.SelectedValue, out Selected_Yellow_Card);

                if (CardName == "Red")
                {
                    clsCompetitionPlayer.NoOfRedCard = 1;
                }
                else if (CardName == "Yellow")
                {
                    DataTable yellow_red_Detail = competitionPlayerController.GetCompetitionPlayerPerfomanceData(CompetitionID, player_id);

                    if (Selected_Yellow_Card == 1)
                    {
                        clsCompetitionPlayer.NoOfRedCard = clsCompetitionPlayer.NoOfRedCardTheirYellowCard = 0;
                        clsCompetitionPlayer.Suspended = 0;

                        if (yellow_red_Detail.Rows.Count == 0)
                        {
                            clsCompetitionPlayer.NoOfYellowCard = Selected_Yellow_Card;
                            competitionPlayerController.InsertCompetitionPlayerPerfomance(clsCompetitionPlayer);
                        }
                        else
                        {
                            int.TryParse(yellow_red_Detail.Rows[0]["NoOfYellowCard"].ToString(), out Previous_Yellow_Card);

                            Current_Yellow_Card = Previous_Yellow_Card + Selected_Yellow_Card;

                            clsCompetitionPlayer.NoOfYellowCard = Current_Yellow_Card;

                            competitionPlayerController.UpdateCompetitionPlayerPerfomance(clsCompetitionPlayer);
                        }
                    }

                }

                mpClass.Yellow = Selected_Yellow_Card;

                mpClass.PlayerSuspended = 0;

                if (chkIsRedB.Checked)
                {
                    mpClass.Red = 1;
                }
                else
                {
                    mpClass.Red = 0;
                }

                //Save/Update Match Player Performance Entry
                mpControl.UpdateMatchResultPlayerPerformance(mpClass);

                if (mpClass.Red == 1 || Selected_Yellow_Card == 2 || Current_Yellow_Card == 3)
                {
                    if (!chkIsRedB.Checked && Current_Yellow_Card != 3)
                    {
                        clsCompetitionPlayer.NoOfRedCardTheirYellowCard = 1;
                    }

                    if (Selected_Yellow_Card == 2)
                    {
                        clsCompetitionPlayer.NoOfYellowCard = Selected_Yellow_Card;
                    }

                    else if (Current_Yellow_Card == 3)
                    {
                        clsCompetitionPlayer.NoOfYellowCard = Current_Yellow_Card;
                    }


                    DataTable dt1 = competitionPlayerController.GetCompetitionPlayerPerfomanceData(CompetitionID, player_id);
                    if (dt1.Rows.Count == 0)
                    {
                        clsCompetitionPlayer.Suspended = 1;

                        competitionPlayerController.InsertCompetitionPlayerPerfomance(clsCompetitionPlayer);
                    }
                    else
                    {
                        if (dt1.Rows[0]["Suspended"].ToString() == "0")
                        {
                            clsCompetitionPlayer.Suspended = 1;
                            competitionPlayerController.UpdateSuspendedCount(clsCompetitionPlayer.Suspended, player_id, CompetitionID);
                        }

                        competitionPlayerController.UpdateCompetitionPlayerPerfomance(clsCompetitionPlayer);
                    }

                }

                mpControl.InsertPlayerCardDetail(mpClass);
            }

            FillTeamBPlayer();
            FillTeamAPlayer();
            reloaddata();
        }

        protected void rptrTeamB1_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SwapUpPlayer")
            {
                mpClass.CompetitionID = CompetitionID;
                mpClass.MatchId = MatchID;

                int player_id = Convert.ToInt32(e.CommandArgument);
                mpClass.PlayerID = player_id;
                mpClass.PortalID = PortalId;
                mpClass.CreatedById= currentUser.Username;
                mpClass.ModifiedById = currentUser.Username;
                mpClass.Goal = 0;
                mpClass.Assist = 0;
                mpClass.IsPlayed = 1;

                DataTable dt3 = new DataTable();
                dt3 = new clsCompetitionMatchController().GetTeamByCompetitionIdAndMatchID(MatchID, CompetitionID); 
                int TeamIDByMasterIDAndCompetitionID = Convert.ToInt32((dt3.Rows[0]["TeamBID"].ToString()));

                mpClass.TeamID = TeamIDByMasterIDAndCompetitionID;


                using (DataTable dtForPerformance = mpControl.GetMatchPlayerExists(MatchID, player_id))
                {
                    if (dtForPerformance.Rows.Count > 0)
                    {
                        //If Exists in Performance 

                        int performance_id = 0;
                        int.TryParse(dtForPerformance.Rows[0]["MatchPlayerPerfomanceID"].ToString(), out performance_id);
                        mpControl.UpdatePlayerPerformanceFlag(performance_id, 1);
                    }
                    else
                    {
                        // Not Exists then Add in Performance
                        mpControl.InsertMatchPlayerPerfomance(mpClass);
                    }
                }


                FillTeamBPlayer();
                reloaddata();
            }
        }

        protected void dlYellowB_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:dnnModal.show('/Cup')", true);
            string url = "/Cup";
            string s = "dnnModal.show('" + url + "', 'popup_window', 'width=400,height=200,left=100,top=100,resizable=yes');";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "<script language='javascript'>OpenPopup('Yellow');</script>");
        }

        protected void rdbteamB_OnSelectedIndexChanged(Object sender, EventArgs e)
        {
            if (rdbteamb.SelectedIndex == 0)
            {
                rdbteamA.SelectedIndex = 1;
            }
            else if (rdbteamb.SelectedIndex == 1)
                rdbteamA.SelectedIndex = 0;
            else
                rdbteamA.SelectedIndex = 2;
        }

        protected void txtPlayerGoalB_OnTextChanged(object sender, EventArgs e)
        {
            txtTeamBTotal.Text = "";
            TextBox tb1 = ((TextBox)(sender));

            RepeaterItem rp1 = ((RepeaterItem)(tb1.NamingContainer));

            foreach (RepeaterItem i in rptrightTeamB.Items)
            {
                TextBox txtExample = (TextBox)i.FindControl("txtPlayerGoalB");
                if (txtExample != null)
                {
                    txtTeamBTotal.Text = (Convert.ToInt32(txtExample.Text == "" ? 0 : Convert.ToInt32(txtExample.Text)) + (Convert.ToInt32(txtTeamBTotal.Text == "" ? 0 : Convert.ToInt32(txtTeamBTotal.Text)))).ToString();
                }
            }
        }

        #endregion RepeaterB Events

        #region Methods
        
        private void reloaddata()
        {
            DataTable dt = new DataTable();
            dt = matchResultControl.GetCountMatchID(MatchID);
            int
                i = Convert.ToInt32(dt.Rows[0]["MatchID"].ToString());

            if (i == 1)
            {
                DataTable dt3 = new DataTable();
                dt3 = matchResultControl.GetTeamGoalTotalMatchID(MatchID);
                txtTeamATotal.Text = dt3.Rows[0]["TeamATotal"].ToString();
                txtTeamBTotal.Text = dt3.Rows[0]["TeamBTotal"].ToString();
                hdteama.Value = dt3.Rows[0]["TeamATotal"].ToString();
                hdteamb.Value = dt3.Rows[0]["TeamBTotal"].ToString();
                if (!string.IsNullOrEmpty(dt3.Rows[0]["IsNoShow"].ToString()))
                {
                    if (Convert.ToBoolean(dt3.Rows[0]["IsNoShow"].ToString()) != true)
                    {
                        txtTeamATotal.Text = dt3.Rows[0]["TeamATotal"].ToString();
                        txtTeamBTotal.Text = dt3.Rows[0]["TeamBTotal"].ToString();
                        hdteama.Value = dt3.Rows[0]["TeamATotal"].ToString();
                        hdteamb.Value = dt3.Rows[0]["TeamBTotal"].ToString();

                        HdnFroshowon.Value = "0";
                        txtDescription.Text = dt3.Rows[0]["Descr"].ToString();
                        int revel = 0;
                        int.TryParse(dt3.Rows[0]["Ispanlty"].ToString(), out revel);
                        if (!string.IsNullOrEmpty(dt3.Rows[0]["Ispanlty"].ToString()))
                        {
                            if (Convert.ToBoolean(dt3.Rows[0]["Ispanlty"].ToString()) == true)
                            {
                                chkpanlty.Checked = true;
                                tblpanlty.Visible = true;
                                tblpanltyTeamA.Visible = true;
                                tblpanltyTeamB.Visible = true;
                                hdteamApenalty.Value = "1";
                                hdteamBpenalty.Value = "1";
                            }
                            else
                            {
                                chkpanlty.Checked = false;
                                //tblpanlty.Visible = false;
                                tblpanltyTeamA.Attributes.Add("style", "display:none;");
                                tblpanltyTeamB.Attributes.Add("style", "display:none;");
                                hdteamApenalty.Value = "0";
                                hdteamBpenalty.Value = "0";
                                Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: disablecheck(0); ", true);
                            }
                        }
                        if (!string.IsNullOrEmpty(dt3.Rows[0]["TossWinningTeam"].ToString()))
                        {
                            int TossWinningTeam = 0;
                            DataTable dt2 = new DataTable();
                            dt2 = cmControl.GetCompetitionAndTeamDetaibyMatchID(MatchID);
                            int.TryParse(dt3.Rows[0]["TossWinningTeam"].ToString(), out TossWinningTeam);
                            if (TossWinningTeam == Convert.ToInt32(dt2.Rows[0]["TeamAID"].ToString()))
                            {
                                rdbtoss.SelectedIndex = 0;
                            }
                            else if (TossWinningTeam == Convert.ToInt32(dt2.Rows[0]["TeamBID"].ToString()))
                            {
                                rdbtoss.SelectedIndex = 1;
                            }
                        }
                        txtpanltypointA.Text = dt3.Rows[0]["TeamApanlty"].ToString();
                        txtpanltypoint.Text = dt3.Rows[0]["TeamBpanlty"].ToString();
                        ViewState["currentId"] = dt3.Rows[0]["MatchResultID"].ToString();

                        hdnmatchresultid.Value = currentId;

                        int winningteam = 0;
                        int.TryParse(dt3.Rows[0]["WinningTeam"].ToString(), out winningteam);
                        int losingteam = 0;
                        int.TryParse(dt3.Rows[0]["Losingteam"].ToString(), out losingteam);
                        int DrawMatch = 0;
                        int.TryParse(dt3.Rows[0]["Drawteam"].ToString(), out  DrawMatch);

                        string fuA = "";
                        foreach (RepeaterItem ri in rptleftTeamA.Items)
                        {
                            HiddenField txtExample = (HiddenField)ri.FindControl("lblPlayerID");
                            TextBox txtGoal = (TextBox)ri.FindControl("txtPlayerGoalA");
                            TextBox txtAssist = (TextBox)ri.FindControl("txtteamAassist");

                            TextBox txtOWnGoal = (TextBox)ri.FindControl("txtAongoal");
                            //TextBox txtYellowA = (TextBox)ri.FindControl("txtYellowA");
                            DropDownList dlYellowA = (DropDownList)ri.FindControl("dlYellowA");

                            CheckBox chkIsRedA = (CheckBox)ri.FindControl("chkIsRedA");

                            if (txtExample != null)
                            {
                                DataTable dt1 = new DataTable();
                                dt1 = cmControl.GetCompetitionAndTeamDetaibyMatchID(MatchID);
                                mpClass.CompetitionID = Convert.ToInt32(dt1.Rows[0]["CompetitionID"].ToString());
                                if (winningteam == Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString()))
                                {
                                    rdbteamA.SelectedIndex = 0;
                                    rdbteamb.SelectedIndex = 1;
                                }
                                else if (losingteam == Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString()))
                                {
                                    rdbteamA.SelectedIndex = 1;
                                    rdbteamb.SelectedIndex = 0;
                                }
                                if (DrawMatch == 1)
                                {
                                    rdbteamA.SelectedIndex = 2;
                                    rdbteamb.SelectedIndex = 2;
                                }

                                mpClass.MatchId = MatchID;
                                mpClass.PlayerID = Convert.ToInt32(txtExample.Value);
                                DataTable dt2 = new DataTable();
                                dt2 = mpControl.getPlayerGoal(mpClass);
                                if (dt2.Rows.Count > 0)
                                {
                                    txtGoal.Text = dt2.Rows[0]["Goal"].ToString();
                                    txtAssist.Text = dt2.Rows[0]["Assist"].ToString();
                                    txtOWnGoal.Text = dt2.Rows[0]["OwnGoal"].ToString();
                                }
                                else
                                {
                                    txtGoal.Text = "0";
                                    txtAssist.Text = "0";
                                    txtOWnGoal.Text = "0";
                                }

                                mpClass.MatchId = MatchID;
                                mpClass.PlayerID = Convert.ToInt32(txtExample.Value);

                                int IsSuspended = 0;

                                int hdnplayerId = 0;
                                int.TryParse(txtExample.Value, out hdnplayerId);

                                int SuspentionCount = 0;

                                int no_of_yellow = 0;
                                int no_of_red = 0;

                                player_performance_data = mpControl.GetSuspendedFlag(CompetitionID, hdnplayerId);

                                if (player_performance_data.Rows.Count > 0)
                                {

                                    int.TryParse(player_performance_data.Rows[0]["PlayerSuspended"].ToString(), out SuspentionCount);
                                    int.TryParse(player_performance_data.Rows[0]["Yellow"].ToString(), out no_of_yellow);
                                    int.TryParse(player_performance_data.Rows[0]["Red"].ToString(), out no_of_red);
                                    if (SuspentionCount > 0 )
                                    {
                                        fuA += "disableRepeaterRow('teamAtable" + ri.ItemIndex + "');";
                                    }
                                }

                                DataTable dtYellowRed = new DataTable();
                                dtYellowRed = mpControl.getYelloowRed(mpClass);
                                if (dtYellowRed.Rows.Count > 0)
                                {
                                    //txtYellowA.Text = dtYellowRed.Rows[0]["Yellow"].ToString();

                                    dlYellowA.SelectedValue = dtYellowRed.Rows[0]["Yellow"].ToString();

                                    if (dtYellowRed.Rows[0]["Red"].ToString() == "True")
                                    {
                                        chkIsRedA.Checked = true;
                                    }
                                    else
                                    {
                                        chkIsRedA.Checked = false;
                                    }

                                }
                                else
                                {
                                    //txtYellowA.Text = "0";
                                    dlYellowA.SelectedIndex = 0;
                                    chkIsRedA.Checked = false;

                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(fuA))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Javascript", "$(document).ready(function(){ " + fuA + " });", true);
                        }
                        string fu = "";

                        foreach (RepeaterItem ri in rptrightTeamB.Items)
                        {
                            HiddenField txtExample = (HiddenField)ri.FindControl("lblPlayerID");
                            TextBox txtGoal = (TextBox)ri.FindControl("txtPlayerGoalB");
                            TextBox txtAssist = (TextBox)ri.FindControl("txtteamBassist");
                            TextBox txtOwnGoal = (TextBox)ri.FindControl("txtBongoal");
                            //TextBox txtYellowB = (TextBox)ri.FindControl("txtYellowB");
                            CheckBox chkIsRedB = (CheckBox)ri.FindControl("chkIsRedB");

                            DropDownList dlYellowB = (DropDownList)ri.FindControl("dlYellowB");

                            if (txtExample != null)
                            {
                                DataTable dt1 = new DataTable();
                                dt1 = new clsCompetitionMatchController().GetCompetitionAndTeamDetaibyMatchID(MatchID);
                               mpClass.CompetitionID = Convert.ToInt32(dt1.Rows[0]["CompetitionID"].ToString());
                                mpClass.MatchId = MatchID;
                                mpClass.PlayerID = Convert.ToInt32(txtExample.Value);
                                DataTable dt2 = new DataTable();
                                dt2 = mpControl.getPlayerGoal(mpClass);
                                if (dt2.Rows.Count > 0)
                                {
                                    txtGoal.Text = dt2.Rows[0]["Goal"].ToString();
                                    txtAssist.Text = dt2.Rows[0]["Assist"].ToString();
                                    txtOwnGoal.Text = dt2.Rows[0]["OwnGoal"].ToString();
                                }
                                else
                                {
                                    txtGoal.Text = "0";
                                    txtAssist.Text = "0";
                                    txtOwnGoal.Text = "0";
                                }

                                mpClass.MatchId = MatchID;
                                mpClass.PlayerID = Convert.ToInt32(txtExample.Value);

                                int IsSuspended = 0;

                                int hdnplayerId = 0;
                                int.TryParse(txtExample.Value, out hdnplayerId);

                                int SuspentionCount = 0;

                                int no_of_yellow = 0;
                                int no_of_red = 0;

                                player_performance_data = mpControl.GetSuspendedFlag(CompetitionID, hdnplayerId);

                                if (player_performance_data.Rows.Count > 0)
                                {

                                    int.TryParse(player_performance_data.Rows[0]["PlayerSuspended"].ToString(), out SuspentionCount);
                                    int.TryParse(player_performance_data.Rows[0]["Yellow"].ToString(), out no_of_yellow);
                                    int.TryParse(player_performance_data.Rows[0]["Red"].ToString(), out no_of_red);
                                    if (SuspentionCount > 0 )
                                    {
                                        fu += " disableRepeaterRow('teamBtable" + ri.ItemIndex + "'); ";
                                    }
                                }


                                DataTable dtYellowRed = new DataTable();
                                dtYellowRed = mpControl.getYelloowRed(mpClass);
                                if (dtYellowRed.Rows.Count > 0)
                                {
                                    //txtYellowB.Text = dtYellowRed.Rows[0]["Yellow"].ToString();
                                    dlYellowB.SelectedValue = dtYellowRed.Rows[0]["Yellow"].ToString();

                                    if (dtYellowRed.Rows[0]["Red"].ToString() == "True")
                                    {
                                        chkIsRedB.Checked = true;
                                    }
                                    else
                                    {
                                        chkIsRedB.Checked = false;
                                    }

                                }
                                else
                                {
                                    //txtYellowB.Text = "0";
                                    dlYellowB.SelectedIndex = 0;
                                    chkIsRedB.Checked = false;
                                }

                            }
                        }
                        if (!string.IsNullOrEmpty(fu))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Javascript", "$(document).ready(function(){ " + fu + " });", true);
                        }
                    }
                    else
                    {
                        checkNoshow.Checked = true;
                        HdnFroshowon.Value = "1";

                        txtNoShowPenalty.Text = dt3.Rows[0]["NoShowPanltyPoint"].ToString();

                        int winningteam = 0;
                        int.TryParse(dt3.Rows[0]["WinningTeam"].ToString(), out winningteam);
                        int losingteam = 0;
                        int.TryParse(dt3.Rows[0]["Losingteam"].ToString(), out losingteam);
                        int DrawMatch = 0;
                        int.TryParse(dt3.Rows[0]["Drawteam"].ToString(), out  DrawMatch);
                        ViewState["currentId"] = dt3.Rows[0]["MatchResultID"].ToString();

                        hdnmatchresultid.Value = currentId;

                        DataTable dt1 = new DataTable();
                        dt1 = new clsCompetitionMatchController().GetCompetitionAndTeamDetaibyMatchID(MatchID);
                        mpClass.CompetitionID = Convert.ToInt32(dt1.Rows[0]["CompetitionID"].ToString());
                        if (winningteam == Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString()))
                        {
                            rdbteamA.SelectedIndex = 0;
                            rdbteamb.SelectedIndex = 1;
                        }
                        else if (losingteam == Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString()))
                        {
                            rdbteamA.SelectedIndex = 1;
                            rdbteamb.SelectedIndex = 0;
                        }
                        if (DrawMatch == 1)
                        {
                            rdbteamA.SelectedIndex = 2;
                            rdbteamb.SelectedIndex = 2;
                        }
                        Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: disablecheck(0); ", true);
                    }

                    btnMatchResultSave.Visible = false;
                    btnMatchResultUpdateData.Visible = true;
                }

            }
            else
            {
                string fuA = "";
                foreach (RepeaterItem ri in rptleftTeamA.Items)
                {
                    HiddenField txtExample = (HiddenField)ri.FindControl("lblPlayerID");
                    TextBox txtGoal = (TextBox)ri.FindControl("txtPlayerGoalA");
                    TextBox txtAssist = (TextBox)ri.FindControl("txtteamAassist");

                    TextBox txtOWnGoal = (TextBox)ri.FindControl("txtAongoal");
                    //TextBox txtYellowA = (TextBox)ri.FindControl("txtYellowA");
                    DropDownList dlYellowA = (DropDownList)ri.FindControl("dlYellowA");

                    CheckBox chkIsRedA = (CheckBox)ri.FindControl("chkIsRedA");

                    if (txtExample != null)
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = new clsCompetitionMatchController().GetCompetitionAndTeamDetaibyMatchID(MatchID);
                        mpClass.CompetitionID = Convert.ToInt32(dt1.Rows[0]["CompetitionID"].ToString());
                        //if (winningteam == Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString()))
                        //{
                        //    rdbteamA.SelectedIndex = 0;
                        //    rdbteamb.SelectedIndex = 1;
                        //}
                        //else if (losingteam == Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString()))
                        //{
                        //    rdbteamA.SelectedIndex = 1;
                        //    rdbteamb.SelectedIndex = 0;
                        //}
                        //if (DrawMatch == 1)
                        //{
                        //    rdbteamA.SelectedIndex = 2;
                        //    rdbteamb.SelectedIndex = 2;
                        //}

                        mpClass.MatchId = MatchID;
                        mpClass.PlayerID = Convert.ToInt32(txtExample.Value);


                        int IsSuspended = 0;

                        int hdnplayerId = 0;
                        int.TryParse(txtExample.Value, out hdnplayerId);

                        int SuspentionCount = 0;

                        int no_of_yellow = 0;
                        int no_of_red = 0;

                        player_performance_data = mpControl.GetSuspendedFlag(CompetitionID, hdnplayerId);

                        if (player_performance_data.Rows.Count > 0)
                        {

                            int.TryParse(player_performance_data.Rows[0]["PlayerSuspended"].ToString(), out SuspentionCount);
                            int.TryParse(player_performance_data.Rows[0]["Yellow"].ToString(), out no_of_yellow);
                            int.TryParse(player_performance_data.Rows[0]["Red"].ToString(), out no_of_red);

                            if (SuspentionCount > 0 )
                            {
                                fuA += "disableRepeaterRow('teamAtable" + ri.ItemIndex + "');";
                            }
                        }

                        DataTable dt2 = new DataTable();
                        dt2 = mpControl.getPlayerGoal(mpClass);
                        if (dt2.Rows.Count > 0)
                        {
                            txtGoal.Text = dt2.Rows[0]["Goal"].ToString();
                            txtAssist.Text = dt2.Rows[0]["Assist"].ToString();
                            txtOWnGoal.Text = dt2.Rows[0]["OwnGoal"].ToString();
                        }
                        else
                        {
                            txtGoal.Text = "0";
                            txtAssist.Text = "0";
                            txtOWnGoal.Text = "0";
                        }

                        mpClass.MatchId = MatchID;
                        mpClass.PlayerID = Convert.ToInt32(txtExample.Value);
                        DataTable dtYellowRed = new DataTable();
                        dtYellowRed = mpControl.getYelloowRed(mpClass);
                        if (dtYellowRed.Rows.Count > 0)
                        {
                            //txtYellowA.Text = dtYellowRed.Rows[0]["Yellow"].ToString();

                            dlYellowA.SelectedValue = dtYellowRed.Rows[0]["Yellow"].ToString();

                            if (dtYellowRed.Rows[0]["Red"].ToString() == "True")
                            {
                                chkIsRedA.Checked = true;
                            }
                            else
                            {
                                chkIsRedA.Checked = false;
                            }

                        }
                        else
                        {
                            //txtYellowA.Text = "0";
                            dlYellowA.SelectedIndex = 0;
                            chkIsRedA.Checked = false;

                        }
                    }
                }

                if (!string.IsNullOrEmpty(fuA))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Javascript", "$(document).ready(function(){ " + fuA + " });", true);
                }

                string fu = "";
                foreach (RepeaterItem ri in rptrightTeamB.Items)
                {
                    HiddenField txtExample = (HiddenField)ri.FindControl("lblPlayerID");
                    TextBox txtGoal = (TextBox)ri.FindControl("txtPlayerGoalB");
                    TextBox txtAssist = (TextBox)ri.FindControl("txtteamBassist");
                    TextBox txtOwnGoal = (TextBox)ri.FindControl("txtBongoal");
                    //TextBox txtYellowB = (TextBox)ri.FindControl("txtYellowB");
                    CheckBox chkIsRedB = (CheckBox)ri.FindControl("chkIsRedB");

                    DropDownList dlYellowB = (DropDownList)ri.FindControl("dlYellowB");

                    if (txtExample != null)
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = new clsCompetitionMatchController().GetCompetitionAndTeamDetaibyMatchID(MatchID);
                        mpClass.CompetitionID = Convert.ToInt32(dt1.Rows[0]["CompetitionID"].ToString());
                        mpClass.MatchId = MatchID;
                        mpClass.PlayerID = Convert.ToInt32(txtExample.Value);
                        DataTable dt2 = new DataTable();
                        dt2 = mpControl.getPlayerGoal(mpClass);
                        if (dt2.Rows.Count > 0)
                        {
                            txtGoal.Text = dt2.Rows[0]["Goal"].ToString();
                            txtAssist.Text = dt2.Rows[0]["Assist"].ToString();
                            txtOwnGoal.Text = dt2.Rows[0]["OwnGoal"].ToString();
                        }
                        else
                        {
                            txtGoal.Text = "0";
                            txtAssist.Text = "0";
                            txtOwnGoal.Text = "0";
                        }

                        mpClass.MatchId = MatchID;
                        mpClass.PlayerID = Convert.ToInt32(txtExample.Value);


                        int IsSuspended = 0;

                        int hdnplayerId = 0;
                        int.TryParse(txtExample.Value, out hdnplayerId);

                        int SuspentionCount = 0;

                        int no_of_yellow = 0;
                        int no_of_red = 0;

                        player_performance_data = mpControl.GetSuspendedFlag(CompetitionID, hdnplayerId);

                        if (player_performance_data.Rows.Count > 0)
                        {

                            int.TryParse(player_performance_data.Rows[0]["PlayerSuspended"].ToString(), out SuspentionCount);
                            int.TryParse(player_performance_data.Rows[0]["Yellow"].ToString(), out no_of_yellow);
                            int.TryParse(player_performance_data.Rows[0]["Red"].ToString(), out no_of_red);
                            if (SuspentionCount > 0 )
                            {
                                fu += " disableRepeaterRow('teamBtable" + ri.ItemIndex + "'); ";
                            }
                        }

                        DataTable dtYellowRed = new DataTable();
                        dtYellowRed = mpControl.getYelloowRed(mpClass);
                        if (dtYellowRed.Rows.Count > 0)
                        {
                            
                            dlYellowB.SelectedValue = dtYellowRed.Rows[0]["Yellow"].ToString();

                            if (dtYellowRed.Rows[0]["Red"].ToString() == "True")
                            {
                                chkIsRedB.Checked = true;
                            }
                            else
                            {
                                chkIsRedB.Checked = false;
                            }

                        }
                        else
                        {
                            //txtYellowB.Text = "0";
                            dlYellowB.SelectedIndex = 0;
                            chkIsRedB.Checked = false;
                        }

                    }
                }
                if (!string.IsNullOrEmpty(fu))
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Javascript", "$(document).ready(function(){ " + fu + " });", true);
                }
            }
        }

        private void funClearData()
        {
            txtTeamATotal.Text = "";
            txtTeamBTotal.Text = "";
            txtDescription.Text = "";
            txtTeamATotal.Text = "";
            foreach (RepeaterItem i in rptleftTeamA.Items)
            {
                TextBox txtExample = (TextBox)i.FindControl("txtPlayerGoalA");
                txtExample.Text = "";
            }
            foreach (RepeaterItem i in rptrightTeamB.Items)
            {
                TextBox txtExample = (TextBox)i.FindControl("txtPlayerGoalB");
                txtExample.Text = "";
            }
        }

        private void FillTeamAPlayer()
        {
            
            DataTable dt = new DataTable();
            dt = tpControl.GetTeamAPlayerByMatchID(MatchID,Convert.ToInt32( TeamAID));
            rptleftTeamA.DataSource = dt;
            rptleftTeamA.DataBind();

            if (rptleftTeamA.Items.Count == 0)
            {
                lblForTeamA.Text = "No Data Available";
                lblForTeamA.Visible = true;
                statusForTeamA.Attributes.Add("class", "smallMessage successMessage");
                statusForTeamA.Attributes.Add("style", "display:block;padding:7px;");
            }
            else
            {
                lblForTeamA.Visible = true;
                statusForTeamA.Attributes.Add("style", "display:none");
            }

            dt = tpControl.GetTeamA1PlayerByMatchID(MatchID,Convert.ToInt32( TeamAID));
            rptrTeamA1.DataSource = dt;
            rptrTeamA1.DataBind();

            int TotalCount = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TotalCount += Convert.ToInt32(dt.Rows[i]["Goal"]);
            }

            hdntotalGoal.Value = TotalCount.ToString();
            TotalCount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i]["OwnGoal"].ToString()))

                    TotalCount += Convert.ToInt32(dt.Rows[i]["OwnGoal"]);

            }

            hdongolaA.Value = TotalCount.ToString();
            if (rptrTeamA1.Items.Count == 0)
            {
                lblForTeamA1.Text = "No Data Available";
                lblForTeamA1.Visible = true;
                statusForTeamA1.Attributes.Add("class", "smallMessage successMessage");
                statusForTeamA1.Attributes.Add("style", "display:block;padding:7px;");
            }
            else
            {
                lblForTeamA1.Visible = true;
                statusForTeamA1.Attributes.Add("style", "display:none");
            }
        }

        private void FillTeamBPlayer()
        {
            //clsMatchResultController mrc = new clsMatchResultController();
            //clsMatchResult mr = new clsMatchResult();
            DataTable dt = new DataTable();
            dt = tpControl.GetTeamAPlayerByMatchID(MatchID, Convert.ToInt32(TeamBID));

            //DataView dv = dt.AsDataView();            

            //dt = dv.ToTable(true, "User_RegID");

            rptrightTeamB.DataSource = dt;
            rptrightTeamB.DataBind();

            if (rptrightTeamB.Items.Count == 0)
            {
                lblForTeamB.Text = "No Data Available";
                lblForTeamB.Visible = true;
                statusForTeamB.Attributes.Add("class", "smallMessage successMessage");
                statusForTeamB.Attributes.Add("style", "display:block;padding:7px;");
            }
            else
            {
                lblForTeamB.Visible = false;
                statusForTeamB.Attributes.Add("style", "display:none");
            }

            dt = tpControl.GetTeamA1PlayerByMatchID(MatchID, Convert.ToInt32(TeamBID));
            rptrTeamB1.DataSource = dt;
            rptrTeamB1.DataBind();

            int GoalForTeamB = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GoalForTeamB += Convert.ToInt32(dt.Rows[i]["Goal"]);
            }

            hdntotalGoalForB.Value = GoalForTeamB.ToString();
            GoalForTeamB = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i]["OwnGoal"].ToString()))

                    GoalForTeamB += Convert.ToInt32(dt.Rows[i]["OwnGoal"]);

            }

            hdnTptablongol.Value = GoalForTeamB.ToString();
            if (rptrTeamB1.Items.Count == 0)
            {
                lblForTeamB1.Text = "No Data Available";
                lblForTeamB1.Visible = true;
                statusForTeamB1.Attributes.Add("class", "smallMessage successMessage");
                statusForTeamB1.Attributes.Add("style", "display:block;padding:7px;");
            }
            else
            {
                lblForTeamB1.Visible = false;
                statusForTeamB1.Attributes.Add("style", "display:none");
            }
        }

        public void resetPlayerData()
        {
            foreach (RepeaterItem ri in rptleftTeamA.Items)
            {
                HiddenField txtExample = (HiddenField)ri.FindControl("lblPlayerID");
                HiddenField hfrptleftTeamA = (HiddenField)ri.FindControl("hfrptleftTeamA");


                HiddenField hfId = (HiddenField)ri.FindControl("hfId");
                if (hfId != null)
                {
                    if (txtExample != null)
                    {
                        int temp = 0;
                        int.TryParse(hfId.Value, out temp);

                        int teamA_ID = 0;

                        if (!string.IsNullOrEmpty(hfrptleftTeamA.Value))
                        {
                            int.TryParse(hfrptleftTeamA.Value, out teamA_ID);
                        }

                        mpClass.TeamID = teamA_ID;
                        mpClass.MatchPlayerPerfomanceID = temp;
                        mpClass.MatchId = MatchID;
                        mpClass.CompetitionID = CompetitionID;
                        mpClass.PlayerID = Convert.ToInt32(txtExample.Value);
                        mpClass.Goal = 0;
                        mpClass.OwnGoal = 0;
                        mpClass.Assist = 0;
                        mpClass.Red = 0;
                        mpClass.Yellow = 0;
                        mpClass.PlayerSuspended = 0;
                        mpClass.CreatedById = currentUser.Username;
                        mpClass.ModifiedById = currentUser.Username;
                        mpClass.PortalID = PortalId;
                        mpControl.UpdatePlayerPerformance(mpClass);
                    }
                }
            }

            foreach (RepeaterItem ri in rptrightTeamB.Items)
            {
                HiddenField txtExample = (HiddenField)ri.FindControl("lblPlayerID");
                HiddenField hfrptrightTeamB = (HiddenField)ri.FindControl("hfrptrightTeamB");

                HiddenField hfId = (HiddenField)ri.FindControl("rptrBhfId");
                if (hfId != null)
                {
                    if (txtExample != null)
                    {
                        int temp = 0;
                        int.TryParse(hfId.Value, out temp);
                        mpClass.MatchPlayerPerfomanceID = temp;

                        int teamB_ID = 0;
                        if (!string.IsNullOrEmpty(hfrptrightTeamB.Value))
                        {
                            int.TryParse(hfrptrightTeamB.Value, out teamB_ID);
                        }

                        mpClass.TeamID = teamB_ID;
                        mpClass.MatchId = MatchID;
                        mpClass.CompetitionID = CompetitionID;
                        mpClass.PlayerID = Convert.ToInt32(txtExample.Value);
                        mpClass.Goal = 0;
                        mpClass.OwnGoal = 0;
                        mpClass.Assist = 0;
                        mpClass.Red = 0;
                        mpClass.Yellow = 0;
                        mpClass.PlayerSuspended = 0;
                        mpClass.CreatedById = currentUser.Username;
                        mpClass.ModifiedById = currentUser.Username;
                        mpClass.PortalID = PortalId;
                        mpControl.UpdatePlayerPerformance(mpClass);
                    }
                }
            }
        }

        #endregion

        #region Button Click Events

        protected void btnMatchResultSave_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            if (!checkNoshow.Checked)
            {
                //    mr.NoShowPenaltyPoint = 0;
                matchResultClass.MatchID = MatchID;
                matchResultClass.TeamATotal = (hdteama.Value == "" ? 0 : Convert.ToInt32(hdteama.Value));
                matchResultClass.TeamBTotal = (hdteamb.Value == "" ? 0 : Convert.ToInt32(hdteamb.Value));
                matchResultClass.Descr = txtDescription.Text;
                matchResultClass.PortalID = PortalId;
                matchResultClass.CreatedBy = currentUser.Username;
                matchResultClass.ModifyBy = currentUser.Username;
                matchResultClass.isNoShow = false;
                DataTable dt1 = new DataTable();
                dt1 = cmControl.GetCompetitionAndTeamDetaibyMatchID(MatchID);

                int teamApenalty = (hdteamABpenalty.Value == "" ? 0 : Convert.ToInt32(hdteamABpenalty.Value));
                int teambpenalty = (hdteamBpenalty.Value == "" ? 0 : Convert.ToInt32(hdteamBpenalty.Value));

                if (hdteamApenalty.Value == "1")
                {
                    matchResultClass.IsPanlty = true;

                    if (matchResultClass.TeamATotal > matchResultClass.TeamBTotal)
                    {
                        matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                        matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                        matchResultClass.DrawTeam = 0;
                    }
                    else if (matchResultClass.TeamBTotal > matchResultClass.TeamATotal)
                    {
                        matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                        matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                        matchResultClass.DrawTeam= 0;
                    }
                    else
                    {
                        matchResultClass.WinningTeam = 0;
                        matchResultClass.LosingTeam= 0;
                        matchResultClass.DrawTeam = 1;
                        string statusteamA = rdbteamA.SelectedValue;
                        string statusteamB = rdbteamb.SelectedValue;
                        switch (statusteamA)
                        {
                            case "1":
                                matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                                matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                                matchResultClass.DrawTeam = 0;
                                break;
                            case "2":
                                matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                                matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                                matchResultClass.DrawTeam = 0;
                                break;
                            case "3":
                                matchResultClass.WinningTeam = 0;
                                matchResultClass.LosingTeam = 0;
                                matchResultClass.DrawTeam = 1;
                                break;
                        }
                    }
                }
                else
                {
                    matchResultClass.IsPanlty = false;

                    if (matchResultClass.TeamATotal > matchResultClass.TeamBTotal)
                    {
                        matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                        matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                        matchResultClass.DrawTeam = 0;

                    }
                    else if (matchResultClass.TeamBTotal > matchResultClass.TeamATotal)
                    {
                        matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                        matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                        matchResultClass.DrawTeam = 0;
                    }
                    else
                    {
                        matchResultClass.WinningTeam = 0;
                        matchResultClass.LosingTeam = 0;
                        matchResultClass.DrawTeam = 1;
                        string statusteamA = rdbteamA.SelectedValue;
                        string statusteamB = rdbteamb.SelectedValue;
                        switch (statusteamA)
                        {
                            case "1":
                                matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                                matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                                matchResultClass.DrawTeam = 0;
                                break;
                            case "2":
                                matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                                matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                                matchResultClass.DrawTeam = 0;
                                break;
                            case "3":
                                matchResultClass.WinningTeam = 0;
                                matchResultClass.LosingTeam = 0;
                                matchResultClass.DrawTeam = 1;
                                break;

                        }
                    }
                }
                string str = rdbtoss.SelectedValue;
                switch (str)
                {
                    case "1":
                        matchResultClass.TossWinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                        break;
                    case "2":
                        matchResultClass.TossWinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                        break;

                }
                int revel = 0;
                int.TryParse(txtTeamATotal.Text, out revel);
                matchResultClass.TeamApanlty = revel;
                revel = 0;
                int.TryParse(txtTeamBTotal.Text, out revel);
                matchResultClass.TeamBpanlty = revel;
                matchResultControl.InsertMatchResult(matchResultClass);

                clsCompetitionMatchController cm = new clsCompetitionMatchController();
                clsCompetitionMatch cmm = new clsCompetitionMatch();
                cmm.IsPlayed = 1;
               int a = cm.UpdateIsPlayedForMatch(MatchID, cmm.IsPlayed);

                foreach (RepeaterItem i in rptleftTeamA.Items)
                {
                    HiddenField txtPlayerId = (HiddenField)i.FindControl("lblPlayerID");

                    HiddenField hfId = (HiddenField)i.FindControl("hfId");
                    TextBox txtGoal = (TextBox)i.FindControl("txtPlayerGoalA");
                    TextBox txtassist = (TextBox)i.FindControl("txtteamAassist");
                    TextBox txtOwnGoal = (TextBox)i.FindControl("txtAongoal");
                    //TextBox txtYellowA = (TextBox)i.FindControl("txtYellowA");
                    CheckBox chkIsRedA = (CheckBox)i.FindControl("chkIsRedA");

                    DropDownList dlYellowA = (DropDownList)i.FindControl("dlYellowA");

                    int player_id = 0;
                    if (txtPlayerId != null)
                    {
                        //Update Player Performance And Match Suspended Count

                        int isSuspended = 0;
                        int no_of_yellow = 0;
                        int no_of_red = 0;
                        int.TryParse(txtPlayerId.Value, out player_id);
                        int Total_Suspended_Count = 0;
                        DataTable competition_player_detail = competitionPlayerController.GetCompetitionPlayerPerfomanceData(CompetitionID, player_id);

                        if (competition_player_detail.Rows.Count > 0)
                        {

                            int.TryParse(competition_player_detail.Rows[0]["Suspended"].ToString(), out Total_Suspended_Count);

                            int no_of_red_thorough_yellow = 0;
                            int.TryParse(competition_player_detail.Rows[0]["NoOfRedCardThroughYellowCard"].ToString(), out no_of_red_thorough_yellow);

                            int total_yellowcard = 0;
                            int.TryParse(competition_player_detail.Rows[0]["Yellow"].ToString(), out total_yellowcard);

                            int no_of_redcard = 0;
                            int.TryParse(competition_player_detail.Rows[0]["Red"].ToString(), out no_of_redcard);

                            DataTable performance_data = mpControl.GetPlayerSuspendedFromPerformance(CompetitionID, MatchID, player_id);

                            int.TryParse(performance_data.Rows[0]["Yellow"].ToString(), out no_of_yellow);

                            int.TryParse(performance_data.Rows[0]["PlayerSuspended"].ToString(), out isSuspended);

                            if (performance_data.Rows[0]["Red"].ToString() == "True")
                            {
                                no_of_red = 1;
                            }
                            else
                            {
                                no_of_red = 0;
                            }

                            if (no_of_yellow != 1)
                            {
                                if (isSuspended == 0 && (no_of_yellow == 2 || no_of_red == 1))
                                {

                                }

                                else
                                {
                                    if (Total_Suspended_Count > 0)
                                    {
                                        if (total_yellowcard == 3)
                                        {
                                            if (no_of_yellow == 2)
                                            {
                                                no_of_red_thorough_yellow = 1;
                                            }
                                            else
                                            {
                                                no_of_red_thorough_yellow = 0;
                                            }
                                        }


                                        Total_Suspended_Count = Total_Suspended_Count - 1;

                                        clsCompetitionPlayer.Suspended = Total_Suspended_Count;

                                        competitionPlayerController.UpdateRedYellow(total_yellowcard, no_of_red_thorough_yellow, CompetitionID, player_id);


                                        competitionPlayerController.UpdateSuspendedCount(Total_Suspended_Count, player_id, CompetitionID);

                                        mpControl.UpdateSuspendedFlag(MatchID, player_id, 1);
                                    }
                                }
                            }
                            if (isSuspended == 1)
                            {
                                mpControl.UpdateSuspendedFlag(MatchID, player_id, 1);
                            }

                        }

                        int reval = 0;
                        int.TryParse(txtassist.Text, out reval);
                        DataTable dt = new DataTable();
                        dt = cmControl.GetCompetitionAndTeamDetaibyMatchID(MatchID);
                        mpClass.CompetitionID = Convert.ToInt32(dt.Rows[0]["CompetitionID"].ToString());
                        mpClass.MatchId = MatchID;
                        mpClass.PlayerID = Convert.ToInt32(txtPlayerId.Value);
                        mpClass.Assist = reval;
                        mpClass.Goal = Convert.ToInt32(txtGoal.Text == "" ? 0 : Convert.ToInt32(txtGoal.Text));
                        //mr.Yellow = Convert.ToInt32(txtYellowA.Text == "" ? 0 : Convert.ToInt32(txtYellowA.Text));
                        mpClass.OwnGoal = Convert.ToInt32(txtOwnGoal.Text == "" ? 0 : Convert.ToInt32(txtOwnGoal.Text));
                        mpClass.Yellow = Convert.ToInt32(dlYellowA.SelectedValue);

                        if (chkIsRedA.Checked)
                        {
                            mpClass.Red = 1;
                        }
                        else
                        {
                            mpClass.Red = 0;
                        }
                        mpClass.MatchPlayerPerfomanceID = Convert.ToInt32(hfId.Value);
                        mpClass.PortalID = PortalId;
                        mpClass.CreatedById = currentUser.Username;
                        mpClass.ModifiedById = currentUser.Username;
                        mpClass.IsPlayed = 1;
                        mpClass.TeamID = Convert.ToInt32(dt.Rows[0]["TeamAID"].ToString());
                        ViewState["TeamAID"] = dt.Rows[0]["TeamAID"].ToString();
                        //mrc.InsertMatchResultPlayerPerformance(mr);
                        mpControl.UpdatePlayerPerformance(mpClass);

                    }
                }

                foreach (RepeaterItem i in rptrightTeamB.Items)
                {
                    HiddenField txtPlayerId = (HiddenField)i.FindControl("lblPlayerID");
                    HiddenField rptrBhfId = (HiddenField)i.FindControl("rptrBhfId");
                    TextBox txtGoal = (TextBox)i.FindControl("txtPlayerGoalB");
                    TextBox txtassist = (TextBox)i.FindControl("txtteamBassist");
                    TextBox txtOwnGoal = (TextBox)i.FindControl("txtBongoal");
                    //TextBox txtYellowB = (TextBox)i.FindControl("txtYellowB");
                    CheckBox chkIsRedB = (CheckBox)i.FindControl("chkIsRedB");

                    DropDownList dlYellowB = (DropDownList)i.FindControl("dlYellowB");

                    if (txtPlayerId != null)
                    {
                        int player_id = 0;
                        //Update Player Performance And Match Suspended Count

                        int isSuspended = 0;
                        int no_of_yellow = 0;
                        int no_of_red = 0;

                        int.TryParse(txtPlayerId.Value, out player_id);

                        int Total_Suspended_Count = 0;
                        DataTable competition_player_detail = competitionPlayerController.GetCompetitionPlayerPerfomanceData(CompetitionID, player_id);

                        if (competition_player_detail.Rows.Count > 0)
                        {

                            int.TryParse(competition_player_detail.Rows[0]["Suspended"].ToString(), out Total_Suspended_Count);

                            int no_of_red_thorough_yellow = 0;
                            int.TryParse(competition_player_detail.Rows[0]["NoOfRedCardThroughYellowCard"].ToString(), out no_of_red_thorough_yellow);

                            int total_yellowcard = 0;
                            int.TryParse(competition_player_detail.Rows[0]["NoOfYellowCard"].ToString(), out total_yellowcard);

                            int no_of_redcard = 0;
                            int.TryParse(competition_player_detail.Rows[0]["NoOfRedCard"].ToString(), out no_of_redcard);

                            DataTable performance_data = mpControl.GetPlayerSuspendedFromPerformance(CompetitionID, MatchID, player_id);

                            //int.TryParse(performance_data.Rows[0]["Red"].ToString(), out no_of_red);
                            if (performance_data.Rows[0]["Red"].ToString() == "True")
                            {
                                no_of_red = 1;
                            }
                            else
                            {
                                no_of_red = 0;
                            }

                            int.TryParse(performance_data.Rows[0]["Yellow"].ToString(), out no_of_yellow);

                            int.TryParse(performance_data.Rows[0]["PlayerSuspended"].ToString(), out isSuspended);

                            if (no_of_yellow != 1)
                            {
                                if (isSuspended == 0 && (no_of_yellow == 2 || no_of_red == 1))
                                {

                                }

                                else
                                {
                                    if (Total_Suspended_Count > 0)
                                    {
                                        if (total_yellowcard == 3)
                                        {
                                            if (no_of_yellow == 2)
                                            {
                                                no_of_red_thorough_yellow = 1;
                                            }
                                            else
                                            {
                                                no_of_red_thorough_yellow = 0;
                                            }
                                        }


                                        Total_Suspended_Count = Total_Suspended_Count - 1;

                                        clsCompetitionPlayer.Suspended = Total_Suspended_Count;

                                        competitionPlayerController.UpdateRedYellow(total_yellowcard, no_of_red_thorough_yellow, CompetitionID, player_id);


                                        competitionPlayerController.UpdateSuspendedCount(Total_Suspended_Count, player_id, CompetitionID);

                                        mpControl.UpdateSuspendedFlag(MatchID, player_id, 1);
                                    }
                                }
                            }

                            if (isSuspended == 1)
                            {
                                mpControl.UpdateSuspendedFlag(MatchID, player_id, 1);
                            }
                        }

                        int reval = 0;
                        int.TryParse(txtassist.Text, out reval);
                        DataTable dt = new DataTable();
                        dt = cmControl.GetCompetitionAndTeamDetaibyMatchID(MatchID);
                        mpClass.MatchPlayerPerfomanceID =
                        mpClass.CompetitionID = Convert.ToInt32(dt.Rows[0]["CompetitionID"].ToString());
                        mpClass.MatchId = MatchID;
                        mpClass.Assist = reval;
                        mpClass.PlayerID = Convert.ToInt32(txtPlayerId.Value);
                        mpClass.Goal = Convert.ToInt32(txtGoal.Text == "" ? 0 : Convert.ToInt32(txtGoal.Text));
                        mpClass.OwnGoal = Convert.ToInt32(txtOwnGoal.Text == "" ? 0 : Convert.ToInt32(txtOwnGoal.Text));
                        //mr.Yellow = Convert.ToInt32(txtYellowB.Text == "" ? 0 : Convert.ToInt32(txtYellowB.Text));

                        mpClass.Yellow = Convert.ToInt32(dlYellowB.SelectedValue);

                        if (chkIsRedB.Checked)
                        {
                            mpClass.Red = 1;
                        }
                        else
                        {
                            mpClass.Red = 0;
                        }
                        mpClass.PortalID = PortalId;
                        mpClass.CreatedById = currentUser.Username;
                        mpClass.ModifiedById = currentUser.Username;
                        mpClass.IsPlayed = 1;
                        mpClass.MatchPlayerPerfomanceID = Convert.ToInt32(rptrBhfId.Value);
                        mpClass.TeamID = Convert.ToInt32(dt.Rows[0]["TeamBID"].ToString());
                        ViewState["TeamBID"] = dt.Rows[0]["TeamBID"].ToString();
                        mpControl.UpdatePlayerPerformance(mpClass);
                    }
                }

                funClearData();
                lblMessage.Text = "Save Successfully.";
                FillTeamAPlayer();
                FillTeamBPlayer();
                reloaddata();

                //DataTable dt5 = new DataTable();
                //dt5 = mrc.GetCompetitionIDByMatchID(MatchID);
                //mr.CompetitionID = Convert.ToInt32(dt5.Rows[0]["CompetitionID"].ToString());
                //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Match", "CompetitionID=" + mr.CompetitionID));
            }
            else
            {
                //If Checked
                matchResultClass.NoShowPenaltyPoint = Convert.ToInt32(txtNoShowPenalty.Text == "" ? 0 : Convert.ToInt32(txtNoShowPenalty.Text));

                matchResultClass.MatchID = MatchID;
                if (!string.IsNullOrEmpty(hdteama.Value))

                    matchResultClass.TeamATotal = Convert.ToInt32(hdteama.Value);

                if (!string.IsNullOrEmpty(hdteamb.Value))

                    matchResultClass.TeamBTotal = Convert.ToInt32(hdteamb.Value);
                matchResultClass.Descr = txtDescription.Text;
                matchResultClass.PortalID = PortalId;
                matchResultClass.CreatedBy = currentUser.Username;
                matchResultClass.ModifyBy = currentUser.Username;
                DataTable dt1 = new DataTable();
               dt1 = cmControl.GetCompetitionAndTeamDetaibyMatchID(MatchID);
                int i = 0;
                string str = rdbteamA.SelectedValue;
                switch (str)
                {
                    case "1":
                        {
                            matchResultClass.IsPanlty = false;
                            matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                            matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                            matchResultClass.DrawTeam = 0;
                            break;
                        }
                    case "2":
                        {
                            matchResultClass.IsPanlty = false;
                            matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                            matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                            matchResultClass.DrawTeam = 0;
                            break;
                        }
                }

                matchResultClass.TeamApanlty = 0;
                matchResultClass.TeamBpanlty = 0;
                matchResultClass.isNoShow = true;
                matchResultClass.TossWinningTeam = 0;
                matchResultControl.InsertMatchResult(matchResultClass);
                clsCompetitionMatchController cm = new clsCompetitionMatchController();
                int k = cm.UpdateIsPlayedForMatch(MatchID, 1);
                reloaddata();
            }

             Response.Redirect(Request.RawUrl);

            // Start - Assign Multiple Photo For Player Transfer Latest Player Other Wise Season Wise

            //DataTable dtteamdetail = PlayerPhotoController.GetMatchDetailTeamAIDAndTeamBIDByMatchID(CompetitionID);

            //if (dtteamdetail.Rows.Count > 0)
            //{
            //    for (int j = 0; j < dtteamdetail.Rows.Count; j++)
            //    {
            //        int teamid = 0;
            //        int.TryParse(dtteamdetail.Rows[j]["TeamID"].ToString(), out teamid);

            //        int teammasterid = 0;
            //        int.TryParse(dtteamdetail.Rows[j]["TeamMasterID"].ToString(), out teammasterid);

            //        int seasonid = 0;
            //        int.TryParse(dtteamdetail.Rows[j]["SeasonID"].ToString(), out seasonid);

            //        DataTable dtplist = PlayerPhotoController.GetPlayerListByCompetitionIDTeamID(CompetitionID, teamid);

            //        for (int b = 0; b < dtplist.Rows.Count; b++)
            //        {
            //            int playid = 0;
            //            int.TryParse(dtplist.Rows[b]["PlayerID"].ToString(), out playid);

            //            DataTable dtpainteam = PlayerPhotoController.GetAssignPlayerMultipalPhotoDetainEntryorNot(seasonid, teammasterid, playid);

            //            if ((dtpainteam.Rows[0]["Cnt"].ToString()) == "0")
            //            {
            //                 Assign Player Multipal Photo 
            //                 Player Entry in other table 

            //                PlayerPhoto.MasterPlayerImageNameID = 3;
            //                PlayerPhoto.PlayerID = playid;
            //                PlayerPhoto.PortalID = PortalId;
            //                PlayerPhoto.CreatedBy = currentUser.Username;
            //                PlayerPhoto.ModifyBy = currentUser.Username;
            //                PlayerPhoto.SeasonID = seasonid;

            //                DataTable dtpnoinseason = PlayerPhotoController.GetAssignPlayerMultipalPhotoDetainByTeamMasterIDAndPlayerID(teammasterid, playid);

            //                if (dtpnoinseason.Rows.Count > 0)
            //                {
            //                    PlayerPhoto.PlayerPhotoPath = dtpnoinseason.Rows[0]["PlayerPhotoPath"].ToString().Replace(" ", "");
            //                    PlayerPhoto.PlayerPosition = dtpnoinseason.Rows[0]["PlayerPosition"].ToString();
            //                    PlayerPhoto.JerseyNo = Convert.ToInt32(dtpnoinseason.Rows[0]["JerseyNo"].ToString());
            //                }
            //                else
            //                {
            //                    DataTable dtp = PlayerPhotoController.GetPlayerDetailByPlayerID(playid);
            //                    PlayerPhoto.PlayerPhotoPath = dtp.Rows[0]["PlayerPhotoPath"].ToString().Replace(" ", "");
            //                    PlayerPhoto.PlayerPosition = dtp.Rows[0]["PlayerPosition"].ToString();
            //                    PlayerPhoto.JerseyNo = Convert.ToInt32(dtp.Rows[0]["JerseyNo"].ToString());
            //                }

            //                PlayerPhoto.IsNational = 0;
            //                PlayerPhoto.TeamMasterID = teammasterid;

            //                PlayerPhotoController.InsertPlayerPhoto(PlayerPhoto);
            //            }
            //            else
            //            {
            //                PlayerPhoto.PlayerID = playid;
            //                PlayerPhoto.PortalID = PortalId;
            //                PlayerPhoto.CreatedBy = currentUser.Username;
            //                PlayerPhoto.ModifyBy = currentUser.Username;
            //                PlayerPhoto.SeasonID = seasonid;

            //                DataTable dtpnoinseason = PlayerPhotoController.GetAssignPlayerMultipalPhotoDetainByTeamMasterIDAndPlayerID(teammasterid, playid);

            //                if (dtpnoinseason.Rows.Count > 0)
            //                {
            //                    PlayerPhoto.PlayerPhotoPath = dtpnoinseason.Rows[0]["PlayerPhotoPath"].ToString().Replace(" ", "");
            //                    PlayerPhoto.PlayerPosition = dtpnoinseason.Rows[0]["PlayerPosition"].ToString();
            //                    PlayerPhoto.JerseyNo = Convert.ToInt32(dtpnoinseason.Rows[0]["JerseyNo"].ToString());
            //                }
            //                else
            //                {
            //                    DataTable dtp = PlayerPhotoController.GetPlayerDetailByPlayerID(playid);
            //                    PlayerPhoto.PlayerPhotoPath = dtp.Rows[0]["PlayerPhotoPath"].ToString().Replace(" ", "");
            //                    PlayerPhoto.PlayerPosition = dtp.Rows[0]["PlayerPosition"].ToString();
            //                    PlayerPhoto.JerseyNo = Convert.ToInt32(dtp.Rows[0]["JerseyNo"].ToString());
            //                }

            //                PlayerPhoto.TeamMasterID = teammasterid;

            //                PlayerPhotoController.UpdatePlayerPhotoPositionJerseyNo(PlayerPhoto);
            //            }
            //        }
            //    }
            //}

             //End - Assign Multiple Photo For Player Transfer Latest Player Other Wise Season Wise

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "Match", "CompetitionID=" + CompetitionID + "&page_index=" + page_index));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);
        }

        protected void btnMatchResultUpdateData_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            matchResultClass.MatchResultID = Convert.ToInt32(ViewState["currentId"].ToString());

            if (!checkNoshow.Checked)
            {
                matchResultClass.NoShowPenaltyPoint = 0;
                matchResultClass.MatchID = MatchID;
                matchResultClass.TeamATotal = (hdteama.Value == "" ? 0 : Convert.ToInt32(hdteama.Value));
                matchResultClass.TeamBTotal = (hdteamb.Value == "" ? 0 : Convert.ToInt32(hdteamb.Value));
                matchResultClass.Descr = txtDescription.Text;
                matchResultClass.PortalID = PortalId;
                matchResultClass.CreatedBy = currentUser.Username;
                matchResultClass.ModifyBy = currentUser.Username;
                DataTable dt1 = new DataTable();
                dt1 = cmControl.GetCompetitionAndTeamDetaibyMatchID(MatchID); 
                int teamApenalty = (hdteamABpenalty.Value == "" ? 0 : Convert.ToInt32(hdteamABpenalty.Value));
                int teambpenalty = (hdteamBpenalty.Value == "" ? 0 : Convert.ToInt32(hdteamBpenalty.Value));

                if (hdteamApenalty.Value == "1")
                {
                    matchResultClass.IsPanlty = true;


                    if (teamApenalty > teambpenalty)
                    {
                        matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                        matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                        matchResultClass.DrawTeam = 0;

                    }
                    else if (teambpenalty > teamApenalty)
                    {
                        matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                        matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                        matchResultClass.DrawTeam = 0;
                    }
                    else
                    {
                        matchResultClass.WinningTeam = 0;
                        matchResultClass.LosingTeam = 0;
                        matchResultClass.DrawTeam = 1;
                        string statusteamA = rdbteamA.SelectedValue;
                        string statusteamB = rdbteamb.SelectedValue;
                        switch (statusteamA)
                        {
                            case "1":
                                matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                                matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                                matchResultClass.DrawTeam = 0;
                                break;
                            case "2":
                                matchResultClass.WinningTeam  = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                                matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                                matchResultClass.DrawTeam = 0;
                                break;
                            case "3":
                                matchResultClass.WinningTeam = 0;
                                matchResultClass.LosingTeam = 0;
                                matchResultClass.DrawTeam = 1;
                                break;
                            default:
                                {
                                    matchResultClass.WinningTeam = 0;
                                    matchResultClass.LosingTeam = 0;
                                    matchResultClass.DrawTeam = 0;
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    matchResultClass.IsPanlty = false;

                    if (matchResultClass.TeamATotal > matchResultClass.TeamBTotal)
                    {
                        matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                        matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                        matchResultClass.DrawTeam = 0;
                    }
                    else if (matchResultClass.TeamBTotal > matchResultClass.TeamATotal)
                    {
                        matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                        matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                        matchResultClass.DrawTeam = 0;
                    }
                    else
                    {
                        matchResultClass.WinningTeam = 0;
                        matchResultClass.LosingTeam = 0;
                        matchResultClass.DrawTeam = 1;
                        string statusteamA = "0";
                        string statusteamB = "0";
                        if (hftmp.Value == "0")
                        {
                            statusteamA = "0";
                            statusteamB = "0";
                        }
                        else
                        {
                            statusteamA = rdbteamA.SelectedValue;
                            statusteamB = rdbteamb.SelectedValue;
                        }
                        switch (statusteamA)
                        {
                            case "1":
                                matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                                matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                                matchResultClass.DrawTeam = 0;
                                break;
                            case "2":
                                matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                                matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                                matchResultClass.DrawTeam = 0;
                                break;
                            case "3":
                                matchResultClass.WinningTeam = 0;
                                matchResultClass.LosingTeam = 0;
                                matchResultClass.DrawTeam = 1;
                                break;

                            default:
                                {
                                    matchResultClass.WinningTeam = 0;
                                    matchResultClass.LosingTeam = 0;
                                    matchResultClass.DrawTeam = 0;
                                    break;
                                }
                        }
                    }
                }
                string str = rdbtoss.SelectedValue;
                switch (str)
                {
                    case "1":
                        matchResultClass.TossWinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                        break;
                    case "2":
                        matchResultClass.TossWinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                        break;
                }
                int revel = 0;
                int.TryParse(txtpanltypointA.Text, out revel);
                matchResultClass.TeamApanlty = revel;
                revel = 0;
                int.TryParse(txtpanltypoint.Text, out revel);
                matchResultClass.TeamBpanlty = revel;
                matchResultClass.isNoShow = false;
                matchResultControl.UpdateMatchResult(matchResultClass);
                clsCompetitionMatchController cm = new clsCompetitionMatchController();
                int k = cm.UpdateIsPlayedForMatch(MatchID, 1);

                foreach (RepeaterItem i in rptleftTeamA.Items)
                {
                    HiddenField hfId = (HiddenField)i.FindControl("hfId");
                    HiddenField txtPlayerId = (HiddenField)i.FindControl("lblPlayerID");
                    TextBox txtGoal = (TextBox)i.FindControl("txtPlayerGoalA");
                    TextBox txtassist = (TextBox)i.FindControl("txtteamAassist");
                    TextBox txtOwnGoal = (TextBox)i.FindControl("txtAongoal");
                    //TextBox txtYellowA = (TextBox)i.FindControl("txtYellowA");
                    CheckBox chkIsRedA = (CheckBox)i.FindControl("chkIsRedA");
                    DropDownList dlYellowA = (DropDownList)i.FindControl("dlYellowA");
                    //TextBox txtnote = (TextBox)i.FindControl("txtnote");

                    int player_id = 0;
                    if (txtPlayerId != null)
                    {
                        //Update Player Performance And Match Suspended Count

                        int isSuspended = 0;
                        int no_of_yellow = 0;
                        int no_of_red = 0;
                        int.TryParse(txtPlayerId.Value, out player_id);
                        int Total_Suspended_Count = 0;
                        DataTable competition_player_detail = competitionPlayerController.GetCompetitionPlayerPerfomanceData(CompetitionID, player_id);

                        if (competition_player_detail.Rows.Count > 0)
                        {

                            int.TryParse(competition_player_detail.Rows[0]["Suspended"].ToString(), out Total_Suspended_Count);

                            int no_of_red_thorough_yellow = 0;
                            int.TryParse(competition_player_detail.Rows[0]["NoOfRedCardThroughYellowCard"].ToString(), out no_of_red_thorough_yellow);

                            int total_yellowcard = 0;
                            int.TryParse(competition_player_detail.Rows[0]["NoOfYellowCard"].ToString(), out total_yellowcard);

                            int no_of_redcard = 0;
                            int.TryParse(competition_player_detail.Rows[0]["NoOfRedCard"].ToString(), out no_of_redcard);

                            DataTable performance_data = mpControl.GetPlayerSuspendedFromPerformance(CompetitionID, MatchID, player_id);

                            int.TryParse(performance_data.Rows[0]["Yellow"].ToString(), out no_of_yellow);

                            int.TryParse(performance_data.Rows[0]["PlayerSuspended"].ToString(), out isSuspended);

                            if (performance_data.Rows[0]["Red"].ToString() == "True")
                            {
                                no_of_red = 1;
                            }
                            else
                            {
                                no_of_red = 0;
                            }

                            if (no_of_yellow != 1)
                            {
                                if (isSuspended == 0 && (no_of_yellow == 2 || no_of_red == 1))
                                {

                                }
                                else
                                {
                                    if (Total_Suspended_Count > 0)
                                    {
                                        if (total_yellowcard == 3)
                                        {
                                            if (no_of_yellow == 2)
                                            {
                                                no_of_red_thorough_yellow = 1;
                                            }
                                            else
                                            {
                                                no_of_red_thorough_yellow = 0;
                                            }
                                        }

                                        Total_Suspended_Count = Total_Suspended_Count - 1;

                                        clsCompetitionPlayer.Suspended = Total_Suspended_Count;

                                        competitionPlayerController.UpdateRedYellow(total_yellowcard, no_of_red_thorough_yellow, CompetitionID, player_id);

                                        competitionPlayerController.UpdateSuspendedCount(Total_Suspended_Count, player_id, CompetitionID);

                                        mpControl.UpdateSuspendedFlag(MatchID, player_id, 1);
                                    }
                                }
                            }
                            if (isSuspended == 1)
                            {
                                mpControl.UpdateSuspendedFlag(MatchID, player_id, 1);
                            }
                        }
                        int reval = 0;
                        int.TryParse(txtassist.Text, out reval);
                        //DataTable dt4 = new DataTable();
                        //dt4 = mrc.GetPlayerPerfoIDByMatchID(Convert.ToInt32(txtExample.Value));
                        //if (dt4.Rows.Count > 0)
                        mpClass.MatchPlayerPerfomanceID = Convert.ToInt32(hfId.Value);
                        //Convert.ToInt32(dt4.Rows[0]["MatchPlayerPerformanceID"].ToString());

                        DataTable dt = new DataTable();
                        dt = cmControl.GetCompetitionAndTeamDetaibyMatchID(MatchID);
                        if (dt.Rows.Count > 0)
                            mpClass.CompetitionID = Convert.ToInt32(dt.Rows[0]["CompetitionID"].ToString());
                        mpClass.MatchId = MatchID;
                        mpClass.Assist = reval;
                        mpClass.PlayerID = Convert.ToInt32(txtPlayerId.Value);
                        mpClass.Goal = Convert.ToInt32(txtGoal.Text == "" ? 0 : Convert.ToInt32(txtGoal.Text));
                        mpClass.OwnGoal = Convert.ToInt32(txtOwnGoal.Text == "" ? 0 : Convert.ToInt32(txtOwnGoal.Text));
                        //mr.Yellow = Convert.ToInt32(txtYellowA.Text == "" ? 0 : Convert.ToInt32(txtYellowA.Text));
                        mpClass.Yellow = Convert.ToInt32(dlYellowA.SelectedValue);

                        if (chkIsRedA.Checked)
                        {
                            mpClass.Red = 1;
                        }
                        else
                        {
                            mpClass.Red = 0;
                        }

                        mpClass.PortalID = PortalId;
                        mpClass.CreatedById = currentUser.Username;
                        mpClass.ModifiedById = currentUser.Username;
                        mpClass.TeamID = Convert.ToInt32(dt.Rows[0]["TeamAID"].ToString());
                        ViewState["TeamAID"] = dt.Rows[0]["TeamAID"].ToString();
                        mpControl.UpdateMatchResultPlayerPerformance(mpClass);
                    }
                }

                foreach (RepeaterItem i in rptrightTeamB.Items)
                {
                    HiddenField txtPlayerId = (HiddenField)i.FindControl("lblPlayerID");
                    HiddenField rptrBhfId = (HiddenField)i.FindControl("rptrBhfId");
                    TextBox txtGoal = (TextBox)i.FindControl("txtPlayerGoalB");
                    TextBox txtassist = (TextBox)i.FindControl("txtteamBassist");
                    TextBox txtOwnGoal = (TextBox)i.FindControl("txtBongoal");
                    //TextBox txtYellowB = (TextBox)i.FindControl("txtYellowB");
                    CheckBox chkIsRedB = (CheckBox)i.FindControl("chkIsRedB");
                    TextBox txtNote = (TextBox)i.FindControl("txtnote");
                    DropDownList dlYellowB = (DropDownList)i.FindControl("dlYellowB");

                    if (txtPlayerId != null)
                    {
                        int player_id = 0;
                        //Update Player Performance And Match Suspended Count

                        int isSuspended = 0;
                        int no_of_yellow = 0;
                        int no_of_red = 0;

                        int.TryParse(txtPlayerId.Value, out player_id);

                        int Total_Suspended_Count = 0;
                        DataTable competition_player_detail = competitionPlayerController.GetCompetitionPlayerPerfomanceData(CompetitionID, player_id);

                        if (competition_player_detail.Rows.Count > 0)
                        {
                            int.TryParse(competition_player_detail.Rows[0]["Suspended"].ToString(), out Total_Suspended_Count);

                            int no_of_red_thorough_yellow = 0;
                            int.TryParse(competition_player_detail.Rows[0]["NoOfRedCardThroughYellowCard"].ToString(), out no_of_red_thorough_yellow);

                            int total_yellowcard = 0;
                            int.TryParse(competition_player_detail.Rows[0]["NoOfYellowCard"].ToString(), out total_yellowcard);

                            int no_of_redcard = 0;
                            int.TryParse(competition_player_detail.Rows[0]["NoOfRedCard"].ToString(), out no_of_redcard);

                            DataTable performance_data = mpControl.GetPlayerSuspendedFromPerformance(CompetitionID, MatchID, player_id);

                            int.TryParse(performance_data.Rows[0]["Yellow"].ToString(), out no_of_yellow);

                            int.TryParse(performance_data.Rows[0]["PlayerSuspended"].ToString(), out isSuspended);

                            if (performance_data.Rows[0]["Red"].ToString() == "True")
                            {
                                no_of_red = 1;
                            }
                            else
                            {
                                no_of_red = 0;
                            }

                            if (no_of_yellow != 1)
                            {
                                if (isSuspended == 0 && (no_of_yellow == 2 || no_of_red == 1))
                                {

                                }
                                else
                                {
                                    if (Total_Suspended_Count > 0)
                                    {
                                        if (total_yellowcard == 3)
                                        {
                                            if (no_of_yellow == 2)
                                            {
                                                no_of_red_thorough_yellow = 1;
                                            }
                                            else
                                            {
                                                no_of_red_thorough_yellow = 0;
                                            }
                                        }

                                        Total_Suspended_Count = Total_Suspended_Count - 1;

                                        clsCompetitionPlayer.Suspended = Total_Suspended_Count;

                                        competitionPlayerController.UpdateRedYellow(total_yellowcard, no_of_red_thorough_yellow, CompetitionID, player_id);

                                        competitionPlayerController.UpdateSuspendedCount(Total_Suspended_Count, player_id, CompetitionID);

                                        mpControl.UpdateSuspendedFlag(MatchID, player_id, 1);
                                    }
                                }
                            }
                            if (isSuspended == 1)
                            {
                                mpControl.UpdateSuspendedFlag(MatchID, player_id, 1);
                            }

                        }
                        int reval = 0;
                        int.TryParse(txtassist.Text, out reval);

                        mpClass.MatchPlayerPerfomanceID = Convert.ToInt32(rptrBhfId.Value);

                        DataTable dt = new DataTable();
                        dt = cmControl.GetCompetitionAndTeamDetaibyMatchID(MatchID);
                        if (dt.Rows.Count > 0)
                            mpClass.Assist = reval;
                        mpClass.CompetitionID = Convert.ToInt32(dt.Rows[0]["CompetitionID"].ToString());
                        mpClass.MatchId = MatchID;
                        mpClass.PlayerID = Convert.ToInt32(txtPlayerId.Value);
                        mpClass.Goal = Convert.ToInt32(txtGoal.Text == "" ? 0 : Convert.ToInt32(txtGoal.Text));
                        mpClass.OwnGoal = Convert.ToInt32(txtOwnGoal.Text == "" ? 0 : Convert.ToInt32(txtOwnGoal.Text));
                        //mr.Yellow = Convert.ToInt32(txtYellowB.Text == "" ? 0 : Convert.ToInt32(txtYellowB.Text));
                        mpClass.Yellow = Convert.ToInt32(dlYellowB.SelectedValue);

                        if (chkIsRedB.Checked)
                        {
                            mpClass.Red = 1;
                        }
                        else
                        {
                            mpClass.Red = 0;
                        }
                        mpClass.PortalID = PortalId;
                        mpClass.CreatedById = currentUser.Username;
                        mpClass.ModifiedById = currentUser.Username;
                        mpClass.TeamID = Convert.ToInt32(dt.Rows[0]["TeamBID"].ToString());
                        ViewState["TeamBID"] = dt.Rows[0]["TeamBID"].ToString();
                        mpControl.UpdateMatchResultPlayerPerformance(mpClass);
                    }
                }

                FillTeamAPlayer();
                FillTeamBPlayer();
                reloaddata();
            }
            else
            {
                //CheckBox is checked

                matchResultClass.NoShowPenaltyPoint = Convert.ToInt32(txtNoShowPenalty.Text == "" ? 0 : Convert.ToInt32(txtNoShowPenalty.Text));
                matchResultClass.MatchID = MatchID;
                matchResultClass.TeamATotal = Convert.ToInt32(hdteama.Value);
                matchResultClass.TeamBTotal = Convert.ToInt32(hdteamb.Value);
                matchResultClass.Descr = txtDescription.Text;
                matchResultClass.PortalID = PortalId;
                matchResultClass.CreatedBy = currentUser.Username;
                matchResultClass.ModifyBy = currentUser.Username;
                DataTable dt1 = new DataTable();
                dt1 = cmControl.GetCompetitionAndTeamDetaibyMatchID(MatchID);
                int i = 0;

                string str = rdbteamA.SelectedValue;
                if (hftmp.Value == "0")
                {
                    str = "0";
                }
                else
                {
                    str = rdbteamA.SelectedValue;
                }

                switch (str)
                {
                    case "1":
                        {
                            matchResultClass.IsPanlty = false;
                            matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                            matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                            matchResultClass.DrawTeam = 0;
                            break;
                        }
                    case "2":
                        {
                            matchResultClass.IsPanlty = false;
                            matchResultClass.WinningTeam = Convert.ToInt32(dt1.Rows[0]["TeamBID"].ToString());
                            matchResultClass.LosingTeam = Convert.ToInt32(dt1.Rows[0]["TeamAID"].ToString());
                            matchResultClass.DrawTeam = 0;
                            break;

                        }
                    default:
                        {
                            matchResultClass.IsPanlty = false;
                            matchResultClass.WinningTeam = 0;
                            matchResultClass.LosingTeam = 0;
                            matchResultClass.DrawTeam = 0;
                            break;
                        }
                }

                matchResultClass.TeamApanlty = 0;
                matchResultClass.TeamBpanlty = 0;
                matchResultClass.isNoShow = true;
                matchResultClass.TossWinningTeam = 0;
                matchResultControl.UpdateMatchResult(matchResultClass);
                clsCompetitionMatchController cm = new clsCompetitionMatchController();
                int k = cm.UpdateIsPlayedForMatch(MatchID, 1);
                reloaddata();
            }

            ////Response.Redirect(Request.RawUrl);

            //// Start - Assign Multiple Photo For Player Transfer Latest Player Other Wise Season Wise

            //DataTable dtteamdetail = PlayerPhotoController.GetMatchDetailTeamAIDAndTeamBIDByMatchID(CompetitionID);

            //if (dtteamdetail.Rows.Count > 0)
            //{
            //    for (int j = 0; j < dtteamdetail.Rows.Count; j++)
            //    {
            //        int teamid = 0;
            //        int.TryParse(dtteamdetail.Rows[j]["TeamID"].ToString(), out teamid);

            //        int teammasterid = 0;
            //        int.TryParse(dtteamdetail.Rows[j]["TeamMasterID"].ToString(), out teammasterid);

            //        int seasonid = 0;
            //        int.TryParse(dtteamdetail.Rows[j]["SeasonID"].ToString(), out seasonid);

            //        DataTable dtplist = PlayerPhotoController.GetPlayerListByCompetitionIDTeamID(CompetitionID, teamid);

            //        for (int b = 0; b < dtplist.Rows.Count; b++)
            //        {
            //            int playid = 0;
            //            int.TryParse(dtplist.Rows[b]["PlayerID"].ToString(), out playid);

            //            DataTable dtpainteam = PlayerPhotoController.GetAssignPlayerMultipalPhotoDetainEntryorNot(seasonid, teammasterid, playid);

            //            if ((dtpainteam.Rows[0]["Cnt"].ToString()) == "0")
            //            {
            //                // Assign Player Multipal Photo 
            //                // Player Entry in other table 

            //                PlayerPhoto.MasterPlayerImageNameID = 3;
            //                PlayerPhoto.PlayerID = playid;
            //                PlayerPhoto.PortalID = PortalId;
            //                PlayerPhoto.CreatedBy = currentUser.Username;
            //                PlayerPhoto.ModifyBy = currentUser.Username;
            //                PlayerPhoto.SeasonID = seasonid;

            //                DataTable dtpnoinseason = PlayerPhotoController.GetAssignPlayerMultipalPhotoDetainByTeamMasterIDAndPlayerID(teammasterid, playid);

            //                if (dtpnoinseason.Rows.Count > 0)
            //                {
            //                    PlayerPhoto.PlayerPhotoPath = dtpnoinseason.Rows[0]["PlayerPhotoPath"].ToString().Replace(" ", "");
            //                    PlayerPhoto.PlayerPosition = dtpnoinseason.Rows[0]["PlayerPosition"].ToString();
            //                    PlayerPhoto.JerseyNo = Convert.ToInt32(dtpnoinseason.Rows[0]["JerseyNo"].ToString());
            //                }
            //                else
            //                {
            //                    DataTable dtp = PlayerPhotoController.GetPlayerDetailByPlayerID(playid);
            //                    PlayerPhoto.PlayerPhotoPath = dtp.Rows[0]["PlayerPhotoPath"].ToString().Replace(" ", "");
            //                    PlayerPhoto.PlayerPosition = dtp.Rows[0]["PlayerPosition"].ToString();
            //                    PlayerPhoto.JerseyNo = Convert.ToInt32(dtp.Rows[0]["JerseyNo"].ToString());
            //                }

            //                PlayerPhoto.IsNational = 0;
            //                PlayerPhoto.TeamMasterID = teammasterid;

            //                PlayerPhotoController.InsertPlayerPhoto(PlayerPhoto);
            //            }
            //            else
            //            {
            //                PlayerPhoto.PlayerID = playid;
            //                PlayerPhoto.PortalID = PortalId;
            //                PlayerPhoto.CreatedBy = currentUser.Username;
            //                PlayerPhoto.ModifyBy = currentUser.Username;
            //                PlayerPhoto.SeasonID = seasonid;

            //                DataTable dtpnoinseason = PlayerPhotoController.GetAssignPlayerMultipalPhotoDetainByTeamMasterIDAndPlayerID(teammasterid, playid);

            //                if (dtpnoinseason.Rows.Count > 0)
            //                {
            //                    PlayerPhoto.PlayerPhotoPath = dtpnoinseason.Rows[0]["PlayerPhotoPath"].ToString().Replace(" ", "");
            //                    PlayerPhoto.PlayerPosition = dtpnoinseason.Rows[0]["PlayerPosition"].ToString();
            //                    PlayerPhoto.JerseyNo = Convert.ToInt32(dtpnoinseason.Rows[0]["JerseyNo"].ToString());
            //                }
            //                else
            //                {
            //                    DataTable dtp = PlayerPhotoController.GetPlayerDetailByPlayerID(playid);
            //                    PlayerPhoto.PlayerPhotoPath = dtp.Rows[0]["PlayerPhotoPath"].ToString().Replace(" ", "");
            //                    PlayerPhoto.PlayerPosition = dtp.Rows[0]["PlayerPosition"].ToString();
            //                    PlayerPhoto.JerseyNo = Convert.ToInt32(dtp.Rows[0]["JerseyNo"].ToString());
            //                }
            //                PlayerPhoto.TeamMasterID = teammasterid;

            //                PlayerPhotoController.UpdatePlayerPhotoPositionJerseyNo(PlayerPhoto);
            //            }
            //        }
            //    }
            //}

            //// End - Assign Multiple Photo For Player Transfer Latest Player Other Wise Season Wise

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmCompetitionMatch", "CompetitionID=" + CompetitionID + "&page_index=" + page_index));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "ResetSuccessfully();", true);
            matchResultControl.DeleteMatchResult(MatchID);
            // mrc.DeleteMatchPlayerPerformance(MatchID);

            resetPlayerData();
            checkNoshow.Checked = false;
            reloaddata();
            FillTeamAPlayer();
            FillTeamBPlayer();
            Response.Redirect(Request.RawUrl);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "ResetSuccessfully();", true);
        }

        protected void btnGoToCompetition_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmCompetitionMatch", "CompetitionID=" + CompetitionID));
        }

        protected void btnMatchResultClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmCompetitionMatch", "CompetitionID=" + CompetitionID + "&page_index=" + page_index));
        }

        #endregion

    }
}
