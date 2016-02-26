using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using SportSiteServer;
using System.Data;
using System.IO;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;

namespace DotNetNuke.Modules.ThSport
{
    public partial class conTeamList : PortalModuleBase
    {
        PagedDataSource pgsource = new PagedDataSource();
        int findex, lindex;
        DataRow dr;
        clsTeamDirectoryController teamdirectorycontroller = new clsTeamDirectoryController();
        clsCompetitionAllDetailController clsCompetititonController = new clsCompetitionAllDetailController();
        clsTeamDirectory tteamdirectory = new clsTeamDirectory();
        TeamsController teamsController = new TeamsController();
        UtilityFunctions utFunctions = new UtilityFunctions();

        int SportStageValue = 5;

        DotNetNuke.Entities.Tabs.TabController tabs1 = new Entities.Tabs.TabController();
        DotNetNuke.Entities.Tabs.TabInfo tInfo1 = new Entities.Tabs.TabInfo();

        #region Variables

        private string m_ModuelControl = "";
        private string currentpage;

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

        #endregion Variables

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataList();

            if (!IsPostBack)
            {
                FillCompetitionData(SportStageValue);
                FillCompetitionGroupData();
            }

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

        private void BindDataList()
        {
            //Create new DataTable dt
            DataTable dt = GetAllData();

            pgsource.DataSource = dt.DefaultView;

            //Set PageDataSource paging 
            pgsource.AllowPaging = true;

            //Set number of items to be displayed in the Repeater using drop down list

            pgsource.PageSize = 10;


            //Get Current Page Index
            pgsource.CurrentPageIndex = CurrentPage;

            //Store it Total pages value in View state
            ViewState["totpage"] = pgsource.PageCount;

            //Below line is used to show page number based on selection like "Page 1 of 20"
            //lblpage.Text = "Page " + (CurrentPage + 1) + " of " + pgsource.PageCount;

            //Enabled true Link button previous when current page is not equal first page 
            //Enabled false Link button previous when current page is first page
            lnkPrevious.Text = "<i class=\"fa fa-long-arrow-left\"></i>Previous";
            lnkPrevious.Visible = !pgsource.IsFirstPage;
            //Enabled true Link button Next when current page is not equal last page 
            //Enabled false Link button Next when current page is last page
            lnkNext.Text = "Next<i class=\"fa fa-long-arrow-right\"></i>";
            lnkNext.Visible = !pgsource.IsLastPage;

            //Bind resulted PageSource into the Repeater
            rptrTeamListing.DataSource = pgsource;
            rptrTeamListing.DataBind();

            //Create Paging with help of DataList control "RepeaterPaging"
            doPaging();
            //RepeaterPaging.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        }

        private void doPaging()
        {
            DataTable dt = new DataTable();
            //Add two column into the DataTable "dt" 
            //First Column store page index default it start from "0"
            //Second Column store page index default it start from "1"
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");

            //Assign First Index starts from which number in paging data list
            findex = CurrentPage - 5;

            //Set Last index value if current page less than 5 then last index added "5" values to the Current page else it set "10" for last page number
            if (CurrentPage > 5)
            {
                lindex = CurrentPage + 5;
            }
            else
            {
                lindex = 10;
            }

            //Check last page is greater than total page then reduced it to total no. of page is last index
            if (lindex > Convert.ToInt32(ViewState["totpage"]))
            {
                lindex = Convert.ToInt32(ViewState["totpage"]);
                findex = lindex - 10;
            }

            if (findex < 0)
            {
                findex = 0;
            }

            //Now creating page number based on above first and last page index
            for (int i = findex; i < lindex; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            //Finally bind it page numbers in to the Paging DataList "RepeaterPaging"

            RepeaterPaging.DataSource = dt;
            RepeaterPaging.DataBind();

        }

        private int CurrentPage
        {
            get
            {   //Check view state is null if null then return current index value as "0" else return specific page viewstate value
                if (ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                else
                {
                    return ((int)ViewState["CurrentPage"]);
                }
            }
            set
            {
                //Set View statevalue when page is changed through Paging "RepeaterPaging" DataList
                ViewState["CurrentPage"] = value;
            }
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            //If user click Previous Link button assign current index as -1 it reduce existing page index.
            CurrentPage -= 1;
            //refresh "Repeater1" Data
            BindDataList();
        }
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            //If user click Next Link button assign current index as +1 it add one value to existing page index.
            CurrentPage += 1;

            //refresh "Repeater1" Data
            BindDataList();
        }
        protected void RepeaterPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("newpage"))
            {
                //Assign CurrentPage number when user click on the page number in the Paging "RepeaterPaging" DataList
                CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
                //Refresh "Repeater1" control Data once user change page
                BindDataList();
            }
        }
        protected void RepeaterPaging_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Enabled False for current selected Page index
            LinkButton lnkPage = (LinkButton)e.Item.FindControl("Pagingbtn");
            Literal activepage = (Literal)e.Item.FindControl("activepage");

            if (lnkPage == null || activepage == null)
                return;

            activepage.Visible = false;
            lnkPage.Visible = false;
            if (lnkPage.CommandArgument.ToString() == CurrentPage.ToString())
            {
                activepage.Text = "<span class=\"active\">" + (CurrentPage + 1).ToString() + "</span>";
                activepage.Visible = true;
                //lnkPage.BackColor = System.Drawing.Color.FromName("#FFCC01");
            }
            else
                lnkPage.Visible = true;
        }

        #region Methods

        private void LoadModuleControl()
        {
            if (Request.QueryString["mctl"] != null && Request.QueryString["mctl"] != "conTeamList")//3. Read control name from querystring
            {
                m_ModuelControl = Request.QueryString["mctl"].ToString() + ".ascx";
                PortalModuleBase objPortalModuleBase = (PortalModuleBase)LoadControl(m_ModuelControl);
                objPortalModuleBase.ModuleConfiguration = ModuleConfiguration;
                objPortalModuleBase.ID = System.IO.Path.GetFileNameWithoutExtension(m_ModuelControl);
                loadSelectedControl.Controls.Add(objPortalModuleBase);
                pnlTeamDirectory.Visible = false;
                //pnlForBreadCrumbs.Visible = false;
                pnlTeamList.Visible = false;
            }
        }

        private DataTable GetAllData()
        {
            DataTable dt = new DataTable();
            dt = teamdirectorycontroller.GetDetailAllTeamList();

            return dt;

        }

        private void GetAllCompetitionByType(string competition_type)
        {
            DataTable dt = new DataTable();
            dt = teamdirectorycontroller.FetchAllLeaguesList(competition_type);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count >= 10)
                {
                    divTeamDirectoryPaging.Visible = true;
                }
                else
                {
                    divTeamDirectoryPaging.Visible = false;
                }
                rptrTeamListing.DataSource = dt;
                rptrTeamListing.DataBind();
            }
        }

        private void GetAllCompetitionData(int competitionId)
        {
            DataTable dt = new DataTable();
            dt = teamdirectorycontroller.GetDetailAllTeamByCompetitionId(competitionId);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count >= 10)
                {
                    divTeamDirectoryPaging.Visible = true;
                }
                else
                {
                    divTeamDirectoryPaging.Visible = false;
                }
                rptrTeamListing.DataSource = dt;
                rptrTeamListing.DataBind();
            }
        }

        private void FillCompetitionData(int SportStageValue)
        {
            DataTable dt1 = teamsController.GetAllLeagueCompetitionList(SportStageValue);

            DataTable dt2 = teamsController.GetAllCupCompetitionList(SportStageValue);

            dt1.Merge(dt2);

            ddlCompetitionSearch.DataSource = dt1;
            ddlCompetitionSearch.DataTextField = "Comp_Title";
            ddlCompetitionSearch.DataValueField = "Comp_RegID";
            ddlCompetitionSearch.DataBind();
            ddlCompetitionSearch.Items.Insert(0, "All");
        }

        private void FillCompetitionGroupData()
        {
            DataTable dt = new DataTable();
            dt = teamdirectorycontroller.GetDateCompetitioGroupNameAndID();
            ddlGroupSearch.DataSource = dt;
            ddlGroupSearch.DataTextField = "CompetitionGroupName";
            ddlGroupSearch.DataValueField = "CompetitionGroupID";
            ddlGroupSearch.DataBind();
            ddlGroupSearch.Items.Insert(0, "All");
        }

        private void SearchTeamsWithCompeitionData()
        {
            txtTeamSearch.Text = "";
            if (ddlCompetitionSearch.SelectedValue == "All")
            {
                BindDataList();
            }
            else if (ddlCompetitionSearch.SelectedValue == "-1")
            {
                DataTable dt = new DataTable();
                GetAllCompetitionByType("League");

                dt = teamdirectorycontroller.GetCompetitionListByCompetitionType("League");
                ddlGroupSearch.DataSource = dt;
                ddlGroupSearch.DataTextField = "CompetitionGroupName";
                ddlGroupSearch.DataValueField = "CompetitionGroupID";
                ddlGroupSearch.DataBind();
                ddlGroupSearch.Items.Insert(0, "All");
            }
            else if (ddlCompetitionSearch.SelectedValue == "-2")
            {
                DataTable dt = new DataTable();
                GetAllCompetitionByType("Cup");

                dt = teamdirectorycontroller.GetCompetitionListByCompetitionType("Cup");
                ddlGroupSearch.DataSource = dt;
                ddlGroupSearch.DataTextField = "CompetitionGroupName";
                ddlGroupSearch.DataValueField = "CompetitionGroupID";
                ddlGroupSearch.DataBind();
                ddlGroupSearch.Items.Insert(0, "All");
            }
            else
            {
                int competitionid = Convert.ToInt32(ddlCompetitionSearch.SelectedValue);
                GetAllCompetitionData(competitionid);

                DataTable dt = teamdirectorycontroller.GetDateGroupNameAndIDByCompetitionID(competitionid);
                ddlGroupSearch.DataSource = dt;
                ddlGroupSearch.DataTextField = "CompetitionGroupName";
                ddlGroupSearch.DataValueField = "CompetitionGroupID";
                ddlGroupSearch.DataBind();
                ddlGroupSearch.Items.Insert(0, "All");
            }
        }

        private void SearchTeamListing()
        {
            string teamname = txtTeamSearch.Text.Trim();
            DataTable dt = new DataTable();
            dt = teamdirectorycontroller.GetDetailAllTeamListByTeamName(teamname, SportStageValue);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count >= 10)
                {
                    divTeamDirectoryPaging.Visible = true;
                }
                else
                {
                    divTeamDirectoryPaging.Visible = false;
                }
                rptrTeamListing.DataSource = dt;
                rptrTeamListing.DataBind();
            }
        }

        #endregion Methods

        #region Repeter Events
        protected void rptrTeamListing_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image TeamLogoImage = (Image)e.Item.FindControl("TeamLogoImage");
                Literal ltrlTeamImage = (Literal)e.Item.FindControl("ltrlTeamImage");
                HyperLink hprTeamNameLink = (HyperLink)e.Item.FindControl("hprTeamNameLink");
                HiddenField hdnTeamID = (HiddenField)e.Item.FindControl("hdnTeamID");

                HyperLink team_sch_view = (HyperLink)e.Item.FindControl("team_sch_view");
                HyperLink team_res_view = (HyperLink)e.Item.FindControl("team_res_view");
                HyperLink team_roster_view = (HyperLink)e.Item.FindControl("team_roster_view");
                HyperLink team_admin_view = (HyperLink)e.Item.FindControl("team_admin_view");
                HyperLink team_gallerylink = (HyperLink)e.Item.FindControl("team_gallerylink");
                HyperLink team_newslink = (HyperLink)e.Item.FindControl("team_newslink");
                HyperLink team_videolink = (HyperLink)e.Item.FindControl("team_videolink");

                int team_id = 0;
                int.TryParse(hdnTeamID.Value, out team_id);


                #region For Hyperlink Visibility
                TeamsController tm = new TeamsController();
                DataTable tbcheckData = new DataTable();

                //tbcheckData = tm.GetTeamScheduleAndResultsByCompetitionID(team_id);
                tbcheckData = tm.GetTeamFixturesByCompetitionID(team_id);

                if (tbcheckData.Rows.Count != 0)
                {
                    team_sch_view.Visible = true;
                }

                tbcheckData = tm.GetTeamResultsByCompetitionID(team_id);

                if (tbcheckData.Rows.Count != 0)
                {
                    team_res_view.Visible = true;
                }

                tbcheckData = tm.GetAllPlayersWithPosition(team_id, 5);

                if (tbcheckData.Rows.Count != 0)
                {
                    team_roster_view.Visible = true;
                }

                tbcheckData = tm.GetManagementKeyDetailTeamID(team_id);

                if (tbcheckData.Rows.Count != 0)
                {
                    team_admin_view.Visible = true;
                }

                tbcheckData = tm.GetTeamPhotoByTeamID(team_id);

                if (tbcheckData.Rows.Count != 0)
                {
                    team_gallerylink.Visible = true;
                }

                tbcheckData = tm.GetTeamVideoAndOtherVideoPathByTeamID(team_id);

                if (tbcheckData.Rows.Count != 0)
                {
                    team_videolink.Visible = true;
                }

                NewsDetailControl newsDetail = new NewsDetailControl();
                tbcheckData = newsDetail.GetNewsByTeamId(team_id);

                if (tbcheckData.Rows.Count != 0)
                {
                    team_newslink.Visible = true;
                }

                #endregion For Hyperlink Visibility

                team_sch_view.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamAllDetail", "TeamID=" + team_id + "&TeamTabID=1");


                team_res_view.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamAllDetail", "TeamID=" + team_id + "&TeamTabID=2");


                team_roster_view.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamAllDetail", "TeamID=" + team_id + "&TeamTabID=4");


                team_admin_view.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamAllDetail", "TeamID=" + team_id + "&TeamTabID=3");


                team_gallerylink.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamAllDetail", "TeamID=" + team_id + "&TeamTabID=7");


                team_newslink.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamAllDetail", "TeamID=" + team_id + "&TeamTabID=5");


                team_videolink.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamAllDetail", "TeamID=" + team_id + "&TeamTabID=6");


                hprTeamNameLink.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamAllDetail", "TeamID=" + team_id + "&TeamTabID=0");

                if (!System.IO.File.Exists(Server.MapPath("DesktopModules\\SportSite\\" + TeamLogoImage.ImageUrl.ToString())))
                {
                    TeamLogoImage.Visible = false;

                    ltrlTeamImage.Text = utFunctions.GetDefaultImage(hprTeamNameLink.Text, TeamLogoImage.Width.ToString());
                }
                else
                {
                    TeamLogoImage.Visible = true;
                    ltrlTeamImage.Visible = false;
                }
            }
        }

        protected void rptrTeamListing_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("lnkToTeamPage"))
            {
                int team_ID = 0;
                int.TryParse(e.CommandArgument.ToString(), out team_ID);

                Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "TeamAllDetail", "TeamID=" + team_ID));
            }
        }

        #endregion Repeter Events

        #region Other Events

        protected void ddlCompetitionSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchTeamsWithCompeitionData();
        }

        protected void ddlGroupSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroupSearch.SelectedValue == "All")
            {
                SearchTeamsWithCompeitionData();
            }
            else
            {
                int groupid = Convert.ToInt32(ddlGroupSearch.SelectedValue);
                DataTable dt = new DataTable();
                dt = teamdirectorycontroller.GetDetailAllTeamCompetitionGroupByGroupId(groupid);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count >= 10)
                    {
                        divTeamDirectoryPaging.Visible = true;
                    }
                    else
                    {
                        divTeamDirectoryPaging.Visible = false;
                    }
                    rptrTeamListing.DataSource = dt;
                    rptrTeamListing.DataBind();
                }
            }
        }

        protected void txtTeamSearch_TextChanged(object sender, EventArgs e)
        {
            SearchTeamListing();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchTeamListing();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtTeamSearch.Text = "";
            //GetAllData();
            BindDataList();
            FillCompetitionData(SportStageValue);
            FillCompetitionGroupData();
        }

        #endregion

    }
}