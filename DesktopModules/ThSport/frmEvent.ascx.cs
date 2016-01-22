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
        
        #region Page events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillSport();
                FillSeason();
                FillCompetition();
                FillClub();
                FillClubOwner();
                //FillClubMember();
                //FillTeam();
                //FillTeamMember();
                //FillSponsor();
                FillGridView();
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

            if (currentUser.IsSuperUser || currentUser.IsInRole("Club Admin"))
            {
                dt = csc.GetDataEvent();
            }

            DataView dv = new DataView();
            dv = dt.AsDataView();
            dv.RowFilter = " EventName like '%%" + txtEventNameSearch.Text.Trim() + "%%'";

            if (dv.ToTable().Rows.Count > 0)
            {
                ViewState["dt"] = dv.ToTable();
                gvEvent.DataSource = dv.ToTable();
                gvEvent.DataBind();
            }
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

            pnlEventEntry.Visible = false;
            PnlGridEvent.Visible = true;
            FillGridView();
            funClearData();
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
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionEventID")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                funClearData();
                int EventID = 0;
                int.TryParse(str, out EventID);

                LinkButton btn = sender as LinkButton;

                clsEvent cs = new clsEvent();
                clsEventController csc = new clsEventController();

                DataTable dt = new DataTable();

                dt = csc.GetEventDataByEventID(EventID);

                if (dt.Rows.Count > 0)
                {
                    hidRegID.Value = dt.Rows[0]["EventID"].ToString();
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
                ddlSeason.Items.Insert(0, new ListItem("-- Select Season --", "0"));
            }
        }

        private void FillCompetition()
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.GetCompetitionIDAndCompetitionName();
            if (dt.Rows.Count > 0)
            {
                ddlCompetition.DataSource = dt;
                ddlCompetition.DataTextField = "CompetitionName";
                ddlCompetition.DataValueField = "CompetitionId";
                ddlCompetition.DataBind();
                ddlCompetition.Items.Insert(0, new ListItem("-- Select Competition --", "0"));
            }
        }

        private void FillClub()
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.GetClubIDAndClubName();
            if (dt.Rows.Count > 0)
            {
                ddlClub.DataSource = dt;
                ddlClub.DataTextField = "ClubName";
                ddlClub.DataValueField = "ClubId";
                ddlClub.DataBind();
                ddlClub.Items.Insert(0, new ListItem("-- Select Club --", "0"));
            }
        }

        protected void ddlClub_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillClubOwner();
        }

        private void FillClubOwner()
        {
            clsEvent e = new clsEvent();
            clsEventController ec = new clsEventController();
            DataTable dt = new DataTable();

            dt = ec.GetClubOwnerIDAndClubOwnerName();
            if (dt.Rows.Count > 0)
            {
                ddlClub.DataSource = dt;
                ddlClub.DataTextField = "ClubName";
                ddlClub.DataValueField = "ClubId";
                ddlClub.DataBind();
                ddlClub.Items.Insert(0, new ListItem("-- Select Club --", "0"));
            }
        }
   }
}