using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThSportServer;
using System.Data;
using System.IO;
using System.Globalization;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;

namespace DotNetNuke.Modules.ThSport
{
    public partial class frmEvent : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        clsEvent cs = new clsEvent();
        clsEventController csc = new clsEventController();

        string m_controlToLoad;
        string VName;
        int SeasonID = 0;
        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;

        int eventID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdnEventID.Value))
                {
                    int.TryParse(hdnEventID.Value, out id);
                }
                return id;
            }
        }

        #region Page events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        #endregion

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
                dt = csc.GetDataEvent();
            //}

            DataView dv = new DataView();
            dv = dt.AsDataView();
            dv.RowFilter = " EventName like '%%" + txtEventNameSearch.Text.Trim() + "%%'";

            if (dv.ToTable().Rows.Count > 0)
            {
                ViewState["dt"] = dv.ToTable();
               
            }
            gvEvent.DataSource = dv.ToTable();
            gvEvent.DataBind();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            funClearData();
        }

        private void funClearData()
        {
            txtEventName.Text = "";
            txtEventDetail.Text = "";
            txtEventStartDateTime.Text = "";
            txtEventEndDateTime.Text = "";
            ChkIsActive.Checked = false;
            ddlEventPriority.SelectedValue = "0";
        }

        protected void btnSaveEvent_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            if (ddlSports.SelectedValue == "")
            {
                cs.SportsId = 0;
            }
            else
            {
                cs.SportsId = Convert.ToInt32(ddlSports.SelectedValue);
            }

            if (ddlSeason.SelectedValue == "")
            {
                cs.SeasonId = 0;
            }
            else
            {
                cs.SeasonId = Convert.ToInt32(ddlSeason.SelectedValue);
            }

            if (ddlCompetition.SelectedValue == "")
            {
                cs.CompetitionId = 0;
            }
            else
            {
                cs.CompetitionId = Convert.ToInt32(ddlCompetition.SelectedValue);
            }

            if (ddlClub.SelectedValue == "")
            {
                cs.ClubId = 0;
            }
            else
            {
                cs.ClubId = Convert.ToInt32(ddlClub.SelectedValue);
            }

            if (ddlClubOwner.SelectedValue == "")
            {
                cs.ClubOwnersId = 0;
            }
            else
            {
                cs.ClubOwnersId = Convert.ToInt32(ddlClubOwner.SelectedValue);
            }

            if (ddlClubMember.SelectedValue == "")
            {
                cs.ClubMemberId = 0;
            }
            else
            {
                cs.ClubMemberId = Convert.ToInt32(ddlClubMember.SelectedValue);
            }

            if (ddlTeam.SelectedValue == "")
            {
                cs.TeamId = 0;
            }
            else
            {
                cs.TeamId = Convert.ToInt32(ddlTeam.SelectedValue);
            }

            if (ddlTeamMember.SelectedValue == "")
            {
                cs.TeamMemberId = 0;
            }
            else
            {
                cs.TeamMemberId = Convert.ToInt32(ddlTeamMember.SelectedValue);
            }

            if (ddlSponsor.SelectedValue == "")
            {
                cs.SponsorId = 0; 
            }
            else
            {
                cs.SponsorId = Convert.ToInt32(ddlSponsor.SelectedValue); 
            }
              cs.EventName = txtEventName.Text.Trim();
              cs.EventDetail = txtEventDetail.Text.Trim();
              cs.EventStartDateTime = txtEventStartDateTime.Text.Trim();
              cs.EventEndDateTime = txtEventEndDateTime.Text.Trim();
            
            if (ChkIsActive.Checked == true)
            {
                cs.EventActive = 1;
            }
            else
            {
                cs.EventActive = 0;
            }

            cs.EventPriority = ddlEventPriority.SelectedValue;

            cs.PortalID = PortalId;
            cs.CreatedById = currentUser.Username;
            cs.ModifiedById = currentUser.Username;

            int eventid = csc.InsertEvent(cs);

            DataTable dt = new DataTable();
            dt = csc.GetLatestEventID();
            if (dt.Rows.Count > 0)
            {
                cs.EventID = Convert.ToInt32(dt.Rows[0]["EventID"].ToString());
                csc.InsertEventSports(cs);
            }

            pnlEventEntry.Visible = false;
            PnlGridEvent.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void btnAddEvent_Click(object sender, EventArgs e)
        {
            funClearData();
            pnlEventEntry.Visible = true;
            PnlGridEvent.Visible = false;
            btnSaveEvent.Visible = true;
            btnUpdateEvent.Visible = false;
            FillSport();
            FillSeason();
            FillSponsor();
           
            FillTeam();
            FillClub();
           
            FillCompetition();
            FillClubOwner();
            FillClubMember();
            FillTeamMember();
        }

        protected void btnCloseEvent_Click(object sender, EventArgs e)
        {
            pnlEventEntry.Visible = false;
            PnlGridEvent.Visible = true;
            FillGridView();
        }

        protected void btnUpdateEvent_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully()", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            cs.EventID = Convert.ToInt32(hidRegID.Value);

            if (ddlSports.SelectedValue == "")
            {
                cs.SportsId = 0;
            }
            else
            {
                cs.SportsId = Convert.ToInt32(ddlSports.SelectedValue);
            }

            if (ddlSeason.SelectedValue == "")
            {
                cs.SeasonId = 0;
            }
            else
            {
                cs.SeasonId = Convert.ToInt32(ddlSeason.SelectedValue);
            }

            if (ddlCompetition.SelectedValue == "")
            {
                cs.CompetitionId = 0;
            }
            else
            {
                cs.CompetitionId = Convert.ToInt32(ddlCompetition.SelectedValue);
            }

            if (ddlClub.SelectedValue == "")
            {
                cs.ClubId = 0;
            }
            else
            {
                cs.ClubId = Convert.ToInt32(ddlClub.SelectedValue);
            }

            if (ddlClubOwner.SelectedValue == "")
            {
                cs.ClubOwnersId = 0;
            }
            else
            {
                cs.ClubOwnersId = Convert.ToInt32(ddlClubOwner.SelectedValue);
            }

            if (ddlClubMember.SelectedValue == "")
            {
                cs.ClubMemberId = 0;
            }
            else
            {
                cs.ClubMemberId = Convert.ToInt32(ddlClubMember.SelectedValue);
            }

            if (ddlTeam.SelectedValue == "")
            {
                cs.TeamId = 0;
            }
            else
            {
                cs.TeamId = Convert.ToInt32(ddlTeam.SelectedValue);
            }

            if (ddlTeamMember.SelectedValue == "")
            {
                cs.TeamMemberId = 0;
            }
            else
            {
                cs.TeamMemberId = Convert.ToInt32(ddlTeamMember.SelectedValue);
            }

            if (ddlSponsor.SelectedValue == "")
            {
                cs.SponsorId = 0;
            }
            else
            {
                cs.SponsorId = Convert.ToInt32(ddlSponsor.SelectedValue);
            }

            cs.EventName = txtEventName.Text.Trim();
            cs.EventDetail = txtEventDetail.Text.Trim();
            cs.EventStartDateTime = txtEventStartDateTime.Text.Trim();
            cs.EventEndDateTime = txtEventEndDateTime.Text.Trim();

            if (ChkIsActive.Checked == true)
            {
                cs.EventActive = 1;
            }
            else
            {
                cs.EventActive = 0;
            }

            cs.EventPriority = ddlEventPriority.SelectedValue;

            cs.PortalID = PortalId;
            cs.ModifiedById = currentUser.Username;

            int eventid = csc.UpdateEvent(cs);

            int evid = csc.UpdateEventSport(cs);

            pnlEventEntry.Visible = false;
            PnlGridEvent.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (hndDeleteConfirm.Value == "true")
            {
                DeleteEvent();
            }
        }

        protected void gvEvent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEvent.PageIndex = e.NewPageIndex;
            FillGridView();
        }

        protected void lbGo_Click(object sender, EventArgs e)
        {
            FillGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnEventID.Value = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionEventID")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                funClearData();
                FillSport();
                FillSeason();
                FillCompetition();
                FillClub();
                FillClubOwner();
                FillClubMember();
                FillTeam();
                FillTeamMember();
                FillSponsor();



                LinkButton btn = sender as LinkButton;

                clsEvent cs = new clsEvent();
                clsEventController csc = new clsEventController();

                DataTable dt = new DataTable();

                dt = csc.GetEventDataByEventID(eventID);

                if (dt.Rows.Count > 0)
                {
                    hidRegID.Value = dt.Rows[0]["EventID"].ToString();

                    ddlSports.SelectedValue = dt.Rows[0]["SportsId"].ToString();
                    ddlSeason.SelectedValue = dt.Rows[0]["SeasonId"].ToString();
                    ddlCompetition.SelectedValue = dt.Rows[0]["CompetitionId"].ToString();
                    ddlClub.SelectedValue = dt.Rows[0]["ClubId"].ToString();
                    ddlClubOwner.SelectedValue = dt.Rows[0]["ClubOwnersId"].ToString();
                    ddlClubMember.SelectedValue = dt.Rows[0]["ClubMemberId"].ToString();
                    ddlTeam.SelectedValue = dt.Rows[0]["TeamId"].ToString();
                    ddlTeamMember.SelectedValue = dt.Rows[0]["TeamMemberId"].ToString();
                    ddlSponsor.SelectedValue = dt.Rows[0]["SponsorId"].ToString();

                    txtEventName.Text = dt.Rows[0]["EventName"].ToString();
                    txtEventDetail.Text = dt.Rows[0]["EventDetail"].ToString();
                    txtEventStartDateTime.Text = dt.Rows[0]["EventStartDateTime"].ToString();
                    txtEventEndDateTime.Text = dt.Rows[0]["EventEndDateTime"].ToString();


                    if (dt.Rows[0]["EventActive"].ToString() == "1")
                    {
                        ChkIsActive.Checked = true;
                    }
                    else
                    {
                        ChkIsActive.Checked = false;
                    }

                    ddlEventPriority.SelectedValue = dt.Rows[0]["EventPriority"].ToString();

                    pnlEventEntry.Visible = true;
                    PnlGridEvent.Visible = false;
                    btnUpdateEvent.Visible = true;
                    btnSaveEvent.Visible = false;
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

                if (csc.IsEventHasOtherData(eventID).Rows[0]["RefData"].ToString() != "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteConfirm('" + "Delete" + "');;", true);

                }
                else
                {
                    DeleteEvent();
                }
            }
        }

        private void FillSport()
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.GetSportIDAndSportName();
            if (dt.Rows.Count > 0)
            {
                ddlSports.DataSource = dt;
                ddlSports.DataTextField = "SportName";
                ddlSports.DataValueField = "SportID";
                ddlSports.DataBind();
                ddlSports.Items.Insert(0, new ListItem("-- Select Sport --", "0"));
            }
        }

        private void FillSeason()
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.GetSeasonIDAndSeasonName();
            if (dt.Rows.Count > 0)
            {
                ddlSeason.DataSource = dt;
                ddlSeason.DataTextField = "SeasonName";
                ddlSeason.DataValueField = "SeasonID";
                ddlSeason.DataBind();
                ddlSeason.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillSponsor()
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.GetSponsorIDAndSponsorName();
            if (dt.Rows.Count > 0)
            {
                ddlSponsor.DataSource = dt;
                ddlSponsor.DataTextField = "SponsorName";
                ddlSponsor.DataValueField = "SponsorId";
                ddlSponsor.DataBind();
                ddlSponsor.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillCompetition(int SportID)
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.GetCompetitionIDAndCompetitionName(SportID);
            if (dt.Rows.Count > 0)
            {
                ddlCompetition.DataSource = dt;
                ddlCompetition.DataTextField = "CompetitionName";
                ddlCompetition.DataValueField = "CompetitionId";
                ddlCompetition.DataBind();
                ddlCompetition.Items.Insert(0, new ListItem("-- Select Competition --", "0"));
            }
        }

        private void FillClub(int SportID)
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.GetClubIDAndClubName(SportID);
            if (dt.Rows.Count > 0)
            {
                ddlClub.DataSource = dt;
                ddlClub.DataTextField = "ClubName";
                ddlClub.DataValueField = "ClubId";
                ddlClub.DataBind();
                ddlClub.Items.Insert(0, new ListItem("-- Select Club --", "0"));
            }
        }

        private void FillTeam(int SportID, int ClubID)
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.GetTeamIDAndTeamName(SportID, ClubID);
            if (dt.Rows.Count > 0)
            {
                ddlTeam.DataSource = dt;
                ddlTeam.DataTextField = "TeamName";
                ddlTeam.DataValueField = "TeamId";
                ddlTeam.DataBind();
                ddlTeam.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        protected void ddlClub_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int ClubID = Convert.ToInt32(ddlClub.SelectedValue);

            //if (ClubID > 0)
            //{
            //    divclubowner.Visible = true;
            //    divclubmember.Visible = true;
            //    FillClubOwner(ClubID);
            //    FillClubMember(ClubID);
            //    int SportID = Convert.ToInt32(ddlSports.SelectedValue);
            //    FillTeam(SportID, ClubID);
            //}
            //else
            //{
            //    divclubowner.Visible = false;
            //    divclubmember.Visible = false;
            //    FillClubOwner(ClubID);
            //    FillClubMember(ClubID);
            //    int SportID = Convert.ToInt32(ddlSports.SelectedValue);
            //    FillTeam(SportID, ClubID);
            //}

        }

        private void FillClubOwner(int ClubID)
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.GetClubOwnerIDAndClubOwnerName(ClubID);
            if (dt.Rows.Count > 0)
            {
                ddlClubOwner.DataSource = dt;
                ddlClubOwner.DataTextField = "ClubOwnerName";
                ddlClubOwner.DataValueField = "ClubOwnersId";
                ddlClubOwner.DataBind();
                ddlClubOwner.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillClubMember(int ClubID)
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.GetClubMemberIDAndClubMemberName(ClubID);
            if (dt.Rows.Count > 0)
            {
                ddlClubMember.DataSource = dt;
                ddlClubMember.DataTextField = "ClubMemberName";
                ddlClubMember.DataValueField = "ClubMemberId";
                ddlClubMember.DataBind();
                ddlClubMember.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillTeamMember(int TeamID)
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.GetTeamMemberIDAndTeamMemberName(TeamID);
            if (dt.Rows.Count > 0)
            {
                ddlTeamMember.DataSource = dt;
                ddlTeamMember.DataTextField = "TeamMemberName";
                ddlTeamMember.DataValueField = "TeamMemberID";
                ddlTeamMember.DataBind();
                ddlTeamMember.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        protected void ddlSports_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int SportID = Convert.ToInt32(ddlSports.SelectedValue);

            //if (SportID > 0)
            //{
            //    FillCompetition(SportID);
            //    FillClub(SportID);
            //}
            //else
            //{
            //    divclubmember.Visible = false;
            //    divclubowner.Visible = false;
            //    divteammember.Visible = false;
            //    FillCompetition(SportID);
            //    FillClub(SportID);
            //}

        }

        protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int TeamID = Convert.ToInt32(ddlTeam.SelectedValue);

            //if (TeamID > 0)
            //{
            //    divteammember.Visible = true;
            //    FillTeamMember(TeamID);
            //}
            //else
            //{
            //    divteammember.Visible = false;
            //    FillTeamMember(TeamID);
            //}
        }

         private void FillCompetition()
         {
             clsVideos e = new clsVideos();
             clsVideosController ec = new clsVideosController();
             DataTable dt = new DataTable();

             dt = ec.GetAllCompetition();
            if (dt.Rows.Count > 0)
            {
                ddlCompetition.DataSource = dt;
                ddlCompetition.DataTextField = "CompetitionName";
                ddlCompetition.DataValueField = "CompetitionId";
                ddlCompetition.DataBind();
                ddlCompetition.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
         }

        private void FillClub()
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.FillClubIDAndClubName();
            if (dt.Rows.Count > 0)
            {
                ddlClub.DataSource = dt;
                ddlClub.DataTextField = "ClubName";
                ddlClub.DataValueField = "ClubId";
                ddlClub.DataBind();
                ddlClub.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillClubOwner()
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.FillClubOwnerIDAndClubOwnerName();
            if (dt.Rows.Count > 0)
            {
                ddlClubOwner.DataSource = dt;
                ddlClubOwner.DataTextField = "ClubOwnerName";
                ddlClubOwner.DataValueField = "ClubOwnersId";
                ddlClubOwner.DataBind();
                ddlClubOwner.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
            
        private void FillClubMember()
        {
             clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.FillClubMemberIDAndClubMemberName();
            if (dt.Rows.Count > 0)
            {
                ddlClubMember.DataSource = dt;
                ddlClubMember.DataTextField = "ClubMemberName";
                ddlClubMember.DataValueField = "ClubMemberId";
                ddlClubMember.DataBind();
                ddlClubMember.Items.Insert(0, new ListItem("-- Select --", "0"));
            }       
        }

        private void FillTeam()
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.FillTeamIDAndTeamName();
            if (dt.Rows.Count > 0)
            {
                ddlTeam.DataSource = dt;
                ddlTeam.DataTextField = "TeamName";
                ddlTeam.DataValueField = "TeamId";
                ddlTeam.DataBind();
                ddlTeam.Items.Insert(0, new ListItem("-- Select --", "0"));
            }       
        }

        private void FillTeamMember()
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.FillTeamMemberIDAndTeamMemberName();
            if (dt.Rows.Count > 0)
            {
                ddlTeamMember.DataSource = dt;
                ddlTeamMember.DataTextField = "TeamMemberName";
                ddlTeamMember.DataValueField = "TeamMemberID";
                ddlTeamMember.DataBind();
                ddlTeamMember.Items.Insert(0, new ListItem("-- Select --", "0"));
            }       
        }

        public void DeleteEvent()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);
            csc.DeleteEvent(eventID);
            BindGrid();

        }
            
   }
}