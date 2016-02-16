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
    public partial class frmMatchRating : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        clsSportController spController = new clsSportController();
        clsMatchRating lClass = new clsMatchRating();
        clsMatchRatingController lController = new clsMatchRatingController();

        #region MatchType

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

        int MatchRatingID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdn_MatchRatingID.Value))
                {
                    int.TryParse(hdn_MatchRatingID.Value, out id);
                }
                return id;
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

        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTeamName();
                FillMatchRatingeGridView();
            }
        }


        #region Grid Events

        private void FillMatchRatingeGridView()
        {
            using (DataTable dt = lController.GetAllMatchRatingByMatchId(MatchID))
            {
                gvMatchRating.DataSource = dt;
                gvMatchRating.DataBind();
            }
        }

        protected void gvMatchRating_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {  
                    e.Row.Cells[0].Text = txtMatchTeamAName.Text;
                    e.Row.Cells[1].Text = txtMatchTeamBName.Text;
            }
        }

        protected void gvMatchRating_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMatchRating.PageIndex = e.NewPageIndex;
            FillMatchRatingeGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdn_MatchRatingID.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdnMatchRatingID")).Value;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                pnlEntry.Visible = true;
                pnlList.Visible = false;
                btnSaveMatchRating.Visible = false;
                btnUpdateMatchRating.Visible = true;

                ClearData();
               

                LinkButton btn = sender as LinkButton;

                using (DataTable dt = lController.GetMatchRatingByMatchRatingId(MatchRatingID))
                {
                    if (dt.Rows.Count > 0)
                    {

                        ddlTeamARating.SelectedValue = dt.Rows[0]["TeamARating"].ToString();
                        ddlTeamBRating.SelectedValue = dt.Rows[0]["TeamBRating"].ToString();
                        txtMatchRatingDescription.Text = dt.Rows[0]["MatchRatingDesc"].ToString();
                    }
                }
            }
            //else if (ddlSelectedValue == "Delete")
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);
            //    //lController.DeleteMatchType(MatchType_ID);
            //    //FillMatchTypeGridView();
            //    pnlEntry.Visible = false;
            //    pnlList.Visible = true;
            //}
        }

        #endregion


        protected void btnSaveMatchRating_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "MatchRatingSaveSuccessfully();", true);


                if (ddlTeamARating.SelectedValue == "")
                {
                    lClass.TeamARating = 0;
                }
                else
                {
                    lClass.TeamARating = Convert.ToInt32(ddlTeamARating.SelectedValue);
                }

                if (ddlTeamBRating.SelectedValue == "")
                {
                    lClass.TeamBRating = 0;
                }
                else
                {
                    lClass.TeamBRating = Convert.ToInt32(ddlTeamBRating.SelectedValue);
                }
                lClass.Description = txtMatchRatingDescription.Text;
                lClass.PortalID = currentUser.PortalID;
                lClass.CreatedById = currentUser.Username;
                lClass.ModifiedById = currentUser.Username;
                lClass.MatchId = MatchID;
                lClass.TeamAId =Convert.ToInt32( TeamAID);
                lClass.TeamBId =Convert.ToInt32( TeamBID);
               lController.InsertMatchRating(lClass);

                pnlList.Visible = true;
                pnlEntry.Visible = false;
                FillMatchRatingeGridView();
                ClearData();
            }
        }

        protected void btnAddMatchRating_Click(object sender, EventArgs e)
        {
            ClearData();
            BindTeamName();
            pnlEntry.Visible = true;
            pnlList.Visible = false;
            btnSaveMatchRating.Visible = true;
            btnUpdateMatchRating.Visible = false;

        }

        protected void btnCloseMatchRating_Click(object sender, EventArgs e)
        {
            pnlList.Visible = true;
            pnlEntry.Visible = false;
            FillMatchRatingeGridView();
        }

        protected void btnUpdateMatchRating_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "MatchRatingUpdateSuccessfully()", true);


            if (ddlTeamARating.SelectedValue == "")
            {
                lClass.TeamARating = 0;
            }
            else
            {
                lClass.TeamARating = Convert.ToInt32(ddlTeamARating.SelectedValue);
            }

            if (ddlTeamBRating.SelectedValue == "")
            {
                lClass.TeamBRating = 0;
            }
            else
            {
                lClass.TeamBRating = Convert.ToInt32(ddlTeamBRating.SelectedValue);
            }
            lClass.Description = txtMatchRatingDescription.Text;
            lClass.PortalID = currentUser.PortalID;
            lClass.CreatedById = currentUser.Username;
            lClass.ModifiedById = currentUser.Username;
            lClass.MatchId = MatchID;
            lClass.TeamAId = Convert.ToInt32(TeamAID);
            lClass.TeamBId = Convert.ToInt32(TeamBID);
            lClass.MatchRatingId = MatchRatingID;
            lController.UpdateMatchRating(lClass);

            pnlEntry.Visible = false;
            pnlList.Visible = true;
            FillMatchRatingeGridView();
            ClearData();
        }

        private void ClearData()
        {

            ddlTeamARating.SelectedValue = "0";
            ddlTeamBRating.SelectedValue = "0";
            txtMatchRatingDescription.Text = "";
            //txtMatchTypeDescription.Text = "";

        }
        private void BindTeamName()
        {
            using (DataTable dt = lController.GetTeamsDetailByMatchId(MatchID))
            { 
                if (dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["TeamNameA"].ToString()))
                        {
                            txtMatchTeamAName.Text = dt.Rows[0]["TeamNameA"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["TeamNameB"].ToString()))
                        {
                            txtMatchTeamBName.Text = dt.Rows[0]["TeamNameB"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["TeamAId"].ToString()))
                        {
                            ViewState["TeamAID"] = dt.Rows[0]["TeamAId"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["TeamBId"].ToString()))
                        {
                            ViewState["TeamBID"] = dt.Rows[0]["TeamBId"].ToString();
                        }
                    }
            }
        }
    }
}