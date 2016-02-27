using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using ThSportServer;
using System.Data;
using System.IO;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using System.Text;

namespace DotNetNuke.Modules.ThSport
{
    public partial class conCompetitionList : PortalModuleBase
    {
        clsSeasonController snController = new clsSeasonController();
        clsSportController spController = new clsSportController();
        clsCompetitionController cmpController = new clsCompetitionController();
        private string currentpage;
        clsNewsController newsController = new clsNewsController();
        
        DotNetNuke.Entities.Tabs.TabController tabs = new Entities.Tabs.TabController();
        DotNetNuke.Entities.Tabs.TabInfo tInfo = new Entities.Tabs.TabInfo();

        #region Variables

        
        int SeasonID = 0;

        private string m_ModuelControl = "";
        private int competitionCount = 0;

        public int Sport_ID
        {
            get
            {
                return (int)ViewState["Sport_ID"];
            }
            set
            {
                ViewState["Sport_ID"] = value;
            }
        }

        protected string current_module_control
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["mctl"]))
                {
                    return Request.QueryString["mctl"].ToString();
                }
                return "";
            }
        }

        public string CurrentPageName
        {
            get
            {
                DotNetNuke.Entities.Tabs.TabController tabController = new DotNetNuke.Entities.Tabs.TabController();
                DotNetNuke.Entities.Tabs.TabInfo tabInfo = tabController.GetTab(this.TabId);
                return tabInfo.TabName;
            }
        }

        protected string GetFormattedDate(object dateval)
        {
            if (dateval != null && !string.IsNullOrEmpty(dateval.ToString()))
            {
                DateTime dateRes;
                DateTime.TryParse(dateval.ToString(), out dateRes);
                return string.Format("{0:dd-MMM-yyyy}", dateRes);
            }

            return "";
        }

        #endregion Variables

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            using (DataTable sportData = spController.GetSportDetailBySportName("Football Games"))
            {
                Sport_ID = Convert.ToInt32(sportData.Rows[0]["SportID"].ToString());
            }

            if (current_module_control != "conCompetitionAllDetail")
            {
                if (CurrentPageName == "Cup")
                {
                    titel.Text = "&raquo; Cup List";
                    Literal1.Text = "Competition Cup";
                    ddlCompetitionListCup.Visible = true;
                    ddlCompetitionListLeague.Visible = false;
                }

                if (CurrentPageName == "League")
                {
                    titel.Text = "&raquo; Leagues with all Division";
                    Literal1.Text = "Competition League";
                    ddlCompetitionListCup.Visible = false;
                    ddlCompetitionListLeague.Visible = true;
                }
            }
            else
            {
                pnlForBreadCrumbs.Visible = false;
                breadcrumbdiv.Visible = false;
            }
            
            if (!Page.IsPostBack)
            {
                titel.Text = "Leagues with all Division";
                Literal1.Text = "Competition League";

                if (CurrentPageName == "Cup")
                {
                    titel.Text = "Cup List";
                    Literal1.Text = "Competition Cup";
                }

                if (CurrentPageName == "League")
                {
                    titel.Text = "&raquo; Leagues with all Division";
                    Literal1.Text = "Competition League";
                }

                FillSeasonList();
                FillCompetitionListCup(SeasonID);
                FillCompetitionListLeague(SeasonID);
            }

            GetAllCompetitionList();

            try
            {
                LoadModuleControl();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion Page Events

        #region DropDownList Fill

        private void FillSeasonList()
        {
            DataTable dt = new DataTable();
            dt = snController.GetSeasonListForCompetitionCupAndLeague(Sport_ID);
            if (dt.Rows.Count > 0)
            {
                ddlSeason.DataSource = dt;
                ddlSeason.DataTextField = "SeasonName";
                ddlSeason.DataValueField = "SeasonID";
                ddlSeason.DataBind();
                ddlSeason.Items.Insert(0, new ListItem(" -- Select Season --", "0"));
            }
        }

        private void FillCompetitionListCup(int SeasonID)
        {
            int.TryParse(ddlSeason.SelectedValue, out SeasonID);
            DataTable dt = new DataTable();
            dt = cmpController.GetCompetitionOnlyCup(Sport_ID, SeasonID);
            if (dt.Rows.Count > 0)
            {
                ddlCompetitionListCup.DataSource = dt;
                ddlCompetitionListCup.DataTextField = "CompetitionName";
                ddlCompetitionListCup.DataValueField = "CompetitionID";
                ddlCompetitionListCup.DataBind();
                ddlCompetitionListCup.Items.Insert(0, new ListItem(" -- All Cups --", "0"));
            }
        }

        private void FillCompetitionListLeague(int SeasonID)
        {
            int.TryParse(ddlSeason.SelectedValue, out SeasonID);
            DataTable dt = new DataTable();
            dt = cmpController.GetCompetitionOnlyLeague(Sport_ID, SeasonID);
            if (dt.Rows.Count > 0)
            {
                ddlCompetitionListLeague.DataSource = dt;
                ddlCompetitionListLeague.DataTextField = "CompetitionName";
                ddlCompetitionListLeague.DataValueField = "CompetitionID";
                ddlCompetitionListLeague.DataBind();
                ddlCompetitionListLeague.Items.Insert(0, new ListItem(" -- All Leagues --", "0"));
            }
        }


        #endregion DropDownList Fill


        #region GridView Events

        protected void gvCompetitionList_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("competitionlistedit"))
            {
                pnlCompetitionList.Visible = false;
                int editid = 0;
                int.TryParse(e.CommandArgument.ToString(), out editid);
                Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "CompetitionAllDetail", "CompetitionID=" + editid));
            }

            if (e.CommandName.Equals("competitionbrackets"))
            {
                pnlCompetitionList.Visible = false;
                int editid = 0;
                int.TryParse(e.CommandArgument.ToString(), out editid);
                int CompTabID = 1;
                Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "CompetitionAllDetail", "CompetitionID=" + editid + "&CompTabID=" + CompTabID));
            }

            if (e.CommandName.Equals("competitionscheduleresults"))
            {
                pnlCompetitionList.Visible = false;
                int editid = 0;
                int.TryParse(e.CommandArgument.ToString(), out editid);
                int CompTabID = 2;
                Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "CompetitionAllDetail", "CompetitionID=" + editid + "&CompTabID=" + CompTabID));
            }


        }

        protected void gvCompetitionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // gvCompetitionList.PageIndex = e.NewPageIndex;
            //GetAllCompetitionList(SportStageValue);
        }

        #endregion GridView Events

        #region Methods

        private void LoadModuleControl()
        {
            if (Request.QueryString["mctl"] != null && Request.QueryString["mctl"] != "CompetitionList")//3. Read control name from querystring
            {
                m_ModuelControl = Request.QueryString["mctl"].ToString() + ".ascx";
                PortalModuleBase objPortalModuleBase = (PortalModuleBase)LoadControl(m_ModuelControl);
                objPortalModuleBase.ModuleConfiguration = ModuleConfiguration;
                objPortalModuleBase.ID = System.IO.Path.GetFileNameWithoutExtension(m_ModuelControl);
                loadSelectedControl.Controls.Add(objPortalModuleBase);
                pnlCompetitionList.Visible = false;
                breadcrumbdiv.Visible = false;
                pnlForBreadCrumbs.Visible = false;
            }
        }

        private void GetAllCompetitionList()
        {
            int CompetitionID = 0;
            int SeasonID = 0;
            int.TryParse(ddlSeason.SelectedValue, out SeasonID);
            DataTable dt = new DataTable();
            if (CurrentPageName == "Cup")
            {
                dt = cmpController.GetDetailAllCompetitionList(CompetitionID, Sport_ID, SeasonID);
            }
            else
            {
                dt = cmpController.GetDetailAllLeagueList(CompetitionID, Sport_ID, SeasonID);
            }

            if (dt.Rows.Count > 0)
            {
                divNoDataAvailable.Visible = false;
                pnlCompetitionList.Visible = true;
                rptrCompetitionList.DataSource = dt;
                rptrCompetitionList.DataBind();
                competitionCount = dt.Rows.Count;
            }
            else
            {
                divNoDataAvailable.Visible = true;
                pnlCompetitionList.Visible = false;
            }
        }

        #endregion Methods

        #region Repeater Events

        protected void rptrCompetitionList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal ltrlTotalGroup = e.Item.FindControl("ltrlTotalGroup") as Literal;
                Literal ltrlTotalTeam = e.Item.FindControl("ltrlTotalTeam") as Literal;
                HiddenField hdnCompId = e.Item.FindControl("hdnCompId") as HiddenField;
                HyperLink hlnkCompTitle = e.Item.FindControl("hlnkCompTitle") as HyperLink;
                HyperLink comp_group_view = e.Item.FindControl("comp_group_view") as HyperLink;
                HyperLink comp_sch_view = e.Item.FindControl("comp_sch_view") as HyperLink;
                HyperLink comp_res_view = e.Item.FindControl("comp_res_view") as HyperLink;
                HyperLink comp_newslink = e.Item.FindControl("comp_newslink") as HyperLink;
                HyperLink comp_videolink = e.Item.FindControl("comp_videolink") as HyperLink;
                HyperLink comp_gallerylink = e.Item.FindControl("comp_gallerylink") as HyperLink;


                int cid = 0;
                int.TryParse(hdnCompId.Value, out cid);

                #region For Hyperlink Visibility

                DataTable tbcheckData = new DataTable();

                tbcheckData = cmpController.GetCompetitionFixturesByCompetitionID(cid);

                if (tbcheckData.Rows.Count != 0)
                {
                    comp_sch_view.Visible = true;
                }

                tbcheckData = cmpController.GetCompetitionResultsByCompetitionID(cid);

                if (tbcheckData.Rows.Count != 0)
                {
                    comp_res_view.Visible = true;
                }

                //tbcheckData = cmpController.GetCompetitionPointsByCompetitionID(cid);

                //if (tbcheckData.Rows.Count != 0)
                //{
                //    comp_group_view.Visible = true;
                //}

                tbcheckData = cmpController.GetCompetitionPhotoByCompetitionID(cid);

                if (tbcheckData.Rows.Count != 0)
                {
                    comp_gallerylink.Visible = true;
                }

                tbcheckData = cmpController.GetCompetitionVideoByCompetitionIDCheck(cid);

                if (tbcheckData.Rows.Count != 0)
                {
                    comp_videolink.Visible = true;
                }

                tbcheckData = cmpController.GetNewsByCompetitionId(cid,PortalId);

                if (tbcheckData.Rows.Count != 0)
                {
                    comp_newslink.Visible = true;
                }

                #endregion For Hyperlink Visibility

                if (hlnkCompTitle != null && cid > 0)
                {
                    hlnkCompTitle.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "CompetitionAllDetail", "CompetitionID=" + cid);
                }

                if (comp_newslink != null && cid > 0)
                {
                    comp_newslink.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "CompetitionAllDetail", "CompetitionID=" + cid, "CompTabID=4");
                }

                if (comp_videolink != null && cid > 0)
                {
                    comp_videolink.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "CompetitionAllDetail", "CompetitionID=" + cid, "CompTabID=5");
                }

                if (comp_gallerylink != null && cid > 0)
                {
                    comp_gallerylink.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "CompetitionAllDetail", "CompetitionID=" + cid, "CompTabID=6");
                }

                if (comp_group_view != null && cid > 0)
                {
                    comp_group_view.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "CompetitionAllDetail", "CompetitionID=" + cid, "CompTabID=1");
                }

                if (comp_sch_view != null && cid > 0)
                {
                    comp_sch_view.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "CompetitionAllDetail", "CompetitionID=" + cid, "CompTabID=2");
                }

                if (comp_res_view != null && cid > 0)
                {
                    comp_res_view.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "CompetitionAllDetail", "CompetitionID=" + cid, "CompTabID=3");
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("<div class=\"competition-allteam\">");

                if (ltrlTotalTeam.Text == "0" || ltrlTotalTeam.Text == String.Empty)
                {
                    ltrlTotalTeam.Visible = false;
                }
                else
                {
                    ltrlTotalTeam.Text = "Teams : <font color=\"Black\">" + ltrlTotalTeam.Text + "</font>";
                }

                if (ltrlTotalGroup.Text == "0" || ltrlTotalGroup.Text == String.Empty)
                {
                    ltrlTotalGroup.Visible = false;
                    //ltrlGroupText.Visible = false;

                    DataTable dt = cmpController.GetDetailAllTeamCompetitionGroupByCompetitionId(cid);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            sb.Append("<div class=\"nogroup competition-groupcontainer\">");
                            sb.Append("<ul class=\"competition-group\">");
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                sb.Append("<li class=\"competition-team\">");
                                sb.Append("<a href='" + Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamAllDetail", "TeamID=" + dt.Rows[i]["TeamID"]) + "' >");
                                string imagePath = "/Images/Team_Logo.png";

                                if (dt.Rows[i]["TeamLogo"] != null)
                                {
                                    if (System.IO.File.Exists(Server.MapPath("DesktopModules\\ThSport\\" + dt.Rows[i]["TeamLogo"].ToString())))
                                    {
                                        sb.Append("<img alt=\"\" src=\"\\DesktopModules\\ThSport\\" + dt.Rows[i]["TeamLogo"] + "\" class=\"flag\">");
                                    }
                                    else
                                    {
                                        sb.Append("<img alt=\"\" src=\"\\DesktopModules\\ThSport\\" + imagePath + "\" class=\"flag\">");
                                    }

                                }

                                if (dt.Rows[i]["TeamName"] != null)
                                {
                                    sb.Append("<span class=\"competition-team-name\">" + dt.Rows[i]["TeamName"] + "</span>");
                                }
                                sb.Append("</a>");
                                sb.Append("</li>");
                            }
                            sb.Append("</ul>");
                            sb.Append("</div>");
                        }
                    }
                }
                else
                {
                    ltrlTotalGroup.Text = "Groups: <font color=\"Black\">" + ltrlTotalGroup.Text + "</font>";
                    DataTable dt = cmpController.GetDetailAllTeamCompetitionGroupByCompetitionId(cid);
                    DataView dv = dt.AsDataView();
                    dv.RowFilter = "CompetitionGroupName IS NOT NULL";
                    dt = dv.ToTable();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                string groupname = dt.Rows[i]["CompetitionGroupName"] == null ? String.Empty : dt.Rows[i]["CompetitionGroupName"].ToString();
                                sb.Append("<div class=\"competition-groupcontainer\">");
                                sb.Append("<ul class=\"competition-group\">");
                                sb.Append("<li class=\"competition-group-name\">" + dt.Rows[i]["CompetitionGroupName"].ToString() + "</a></li>");
                            }
                            else if (dt.Rows[i]["CompetitionGroupName"].ToString() != dt.Rows[i - 1]["CompetitionGroupName"].ToString())
                            {
                                string groupname = dt.Rows[i]["CompetitionGroupName"] == null ? String.Empty : dt.Rows[i]["CompetitionGroupName"].ToString();
                                sb.Append("</div><div class=\"competition-groupcontainer\">");
                                sb.Append("</ul><ul class=\"competition-group\">");
                                sb.Append("<li class=\"competition-group-name\">" + dt.Rows[i]["CompetitionGroupName"].ToString() + "</li>");
                            }

                            sb.Append("<li class=\"competition-team\">");
                            sb.Append("<a href='" + Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamAllDetail", "TeamID=" + dt.Rows[i]["TeamID"]) + "' >");

                            string imagePath = "/Images/Team_Logo.png";

                            if (dt.Rows[i]["TeamLogo"] != null)
                            {
                                if (System.IO.File.Exists(Server.MapPath("DesktopModules\\ThSport\\" + dt.Rows[i]["TeamLogo"].ToString())))
                                {
                                    sb.Append("<img alt=\"\" src=\"\\DesktopModules\\ThSport\\" + dt.Rows[i]["TeamLogo"] + "\" class=\"flag\">");
                                }
                                else
                                {
                                    sb.Append("<img alt=\"\" src=\"\\DesktopModules\\ThSport\\" + imagePath + "\" class=\"flag\">");
                                }
                            }

                            if (dt.Rows[i]["TeamName"] != null)
                            {
                                sb.Append("<span class=\"competition-team-name\">" + dt.Rows[i]["TeamName"] + "</span>");
                            }
                            sb.Append("</a>");
                            sb.Append("</li>");
                        }

                        sb.Append("</ul>");
                        sb.Append("</div>");
                    }
                }

                sb.Append("</div>");
                Literal ltrlTeams = e.Item.FindControl("ltrlTeams") as Literal;
                ltrlTeams.Text = sb.ToString();
            }
        }

        protected void rptrCompetitionList_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {

        }

        #endregion Repeater Events

        protected void ddlCompetitionListCup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            int CompetitionID = 0;
            int.TryParse(ddlCompetitionListCup.SelectedValue, out CompetitionID);
            int SeasonID = 0;
            int.TryParse(ddlSeason.SelectedValue, out SeasonID);

            dt = cmpController.GetDetailAllCompetitionList(CompetitionID, Sport_ID, SeasonID);
            if (dt.Rows.Count != 0)
            {
                rptrCompetitionList.DataSource = dt;
                rptrCompetitionList.DataBind();
            }
        }

        protected void ddlCompetitionListLeague_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            int CompetitionID = 0;
            int.TryParse(ddlCompetitionListLeague.SelectedValue, out CompetitionID);
            int SeasonID = 0;
            int.TryParse(ddlSeason.SelectedValue, out SeasonID);

            dt = cmpController.GetDetailAllLeagueList(CompetitionID, Sport_ID, SeasonID);
            if (dt.Rows.Count != 0)
            {
                rptrCompetitionList.DataSource = dt;
                rptrCompetitionList.DataBind();
            }
        }

        protected void ddlSeason_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            int SeasonID = 0;
            int.TryParse(ddlSeason.SelectedValue, out SeasonID);

            if (SeasonID == 0)
            {
                divdlcompetitioncup.Visible = false;
            }
            else
            {
                divdlcompetitioncup.Visible = true;
            }

            FillCompetitionListCup(SeasonID);
            FillCompetitionListLeague(SeasonID);
            GetAllCompetitionList();
        }

    }
}