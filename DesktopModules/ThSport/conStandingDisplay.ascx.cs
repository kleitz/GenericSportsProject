using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SportSiteServer;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Common;

namespace DotNetNuke.Modules.ThSport
{
    public partial class conStandingDisplay : PortalModuleBase
    {
        public int CompetitionId;
        public int IsMiniControl = 0;

        List<int> distinctIds;
        DataTable standingData = new DataTable();
        UtilityFunctions utFunctions = new UtilityFunctions();
        clsCompetitionController cmpController = new clsCompetitionController();

        DotNetNuke.Entities.Tabs.TabController tabs1 = new Entities.Tabs.TabController();

        DotNetNuke.Entities.Tabs.TabInfo tInfo1 = new Entities.Tabs.TabInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            standingData = cmpController.GetCompetitionPointsByCompetitionID(CompetitionId);

            var distinctIds = standingData.AsEnumerable()
                        .Select(s => new
                        {
                            id = s.Field<int>("CompetitionGroupID"),
                        })
                        .Distinct().ToList();

            if (distinctIds.Count > 1)
            {
                //for grouped competition, remove records that have not been assigned a group
                DataView dv = standingData.AsDataView();
                dv.RowFilter = "CompetitionGroupID <> 0";
                dv.RowFilter = "CompetitionGroupID IS NOT NULL";
                standingData = dv.ToTable();
            }

            if (distinctIds != null)
            {
                rptrGroups.DataSource = distinctIds;
                rptrGroups.DataBind();
            }
        }

        protected void gvTeamsView_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gridview = (GridView)sender;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image imgTeamLogo = (Image)e.Row.FindControl("imgTeamLogo");
                if (imgTeamLogo != null)
                {
                    if (imgTeamLogo.ImageUrl == String.Empty || (!(imgTeamLogo.ImageUrl.Contains('.'))))
                    {
                        imgTeamLogo.ImageUrl = "~/DesktopModules/ThSport/Images/no-available-image.png";
                    }
                }
            }
        }
        protected void gvTeamsView_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int team_ID = 0;
            int.TryParse(e.CommandArgument.ToString(), out team_ID);
            if (e.CommandName.Equals("teamName"))
            {
                tInfo1 = tabs1.GetTabByName("Teams", PortalId);
                Response.Redirect(Globals.NavigateURL(tInfo1.TabID, "", "mctl=" + "conTeamAllDetail", "TeamID=" + team_ID));
            }
        }
        protected void rptrGroups_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                e.Item.Visible = true;
                GridView gvTeamsView = e.Item.FindControl("gvTeamsView") as GridView;
                HiddenField hfDistinctGroupId = e.Item.FindControl("hfDistinctGroupId") as HiddenField;
                Label lblSelectGroup = e.Item.FindControl("lblSelectGroup") as Label;
                if (gvTeamsView != null && hfDistinctGroupId != null)
                {
                    DataView filteredView = standingData.AsDataView();
                    filteredView.RowFilter = "CompetitionGroupID = " + hfDistinctGroupId.Value;
                    if (filteredView.ToTable().Rows.Count > 0)
                    {
                        gvTeamsView.DataBound += (object o, EventArgs ev) =>
                        {
                            gvTeamsView.HeaderRow.TableSection = TableRowSection.TableHeader;
                        };

                        string separator = CompetitionId + "_" + hfDistinctGroupId.Value;
                        if (Session["TeamRank" + separator] == null || Session["TeamRank" + separator] == "")
                        {
                            Session["TeamRank" + separator] = "TeamRank";
                        }
                        if (Session["Sort_Order" + separator] == null || Session["Sort_Order" + separator] == "")
                        {
                            Session["Sort_Order" + separator] = "ASC";
                        }

                        filteredView.Sort = Session["TeamRank" + separator].ToString() + " " + Session["Sort_Order" + separator].ToString();
                        gvTeamsView.DataSource = filteredView.ToTable();
                        gvTeamsView.DataBind();

                        int total = filteredView.ToTable().Rows.Count;
                        int SportStageValue = 5;


                        if (SportStageValue == 5)
                        {

                            #region For Senior League

                            /*For 4 Rows */

                            if (total == 4)
                            {
                                for (int i = 0; i < gvTeamsView.Rows.Count; i = i + 2)
                                {

                                    int j = filteredView.ToTable().Rows.Count / 2;


                                    if (gvTeamsView.Rows[i].RowIndex >= j)
                                    {


                                        string hexDown1 = "#fda4a5";
                                        System.Drawing.Color _colorDown1 = System.Drawing.ColorTranslator.FromHtml(hexDown1);

                                        gvTeamsView.Rows[i].BackColor = _colorDown1;

                                        if (gvTeamsView.Rows[i].RowIndex < total - 1)
                                        {

                                            string hexDown2 = "#fd8183";
                                            System.Drawing.Color _colorDown2 = System.Drawing.ColorTranslator.FromHtml(hexDown2);

                                            gvTeamsView.Rows[i + 1].BackColor = _colorDown2;
                                        }


                                    }
                                    else
                                    {
                                        string hexUp1 = "#7ed5aa";
                                        System.Drawing.Color _colorUp1 = System.Drawing.ColorTranslator.FromHtml(hexUp1);
                                        gvTeamsView.Rows[i].BackColor = _colorUp1;


                                        if (gvTeamsView.Rows[i].RowIndex < total - 1)
                                        {

                                            string hexUp2 = "#9bd4b8";
                                            System.Drawing.Color _colorUp2 = System.Drawing.ColorTranslator.FromHtml(hexUp2);
                                            gvTeamsView.Rows[i + 1].BackColor = _colorUp2;
                                        }
                                    }

                                }
                            }


                            if (total == 6)
                            {
                                for (int i = 0; i < gvTeamsView.Rows.Count; i = i + 2)
                                {



                                    int j = filteredView.ToTable().Rows.Count / 2;


                                    if (gvTeamsView.Rows[i].RowIndex >= j)
                                    {

                                        if (i == 4)
                                        {
                                            string hexDown1 = "#fda4a5";
                                            System.Drawing.Color _colorDown1 = System.Drawing.ColorTranslator.FromHtml(hexDown1);

                                            gvTeamsView.Rows[i].BackColor = _colorDown1;

                                            if (gvTeamsView.Rows[i].RowIndex < total - 1)
                                            {

                                                string hexDown2 = "#fd8183";
                                                System.Drawing.Color _colorDown2 = System.Drawing.ColorTranslator.FromHtml(hexDown2);

                                                gvTeamsView.Rows[i + 1].BackColor = _colorDown2;
                                            }
                                        }


                                    }
                                    else
                                    {
                                        if (i < 2)
                                        {

                                            string hexUp1 = "#7ed5aa";
                                            System.Drawing.Color _colorUp1 = System.Drawing.ColorTranslator.FromHtml(hexUp1);
                                            gvTeamsView.Rows[i].BackColor = _colorUp1;


                                            if (gvTeamsView.Rows[i].RowIndex < total - 1)
                                            {

                                                string hexUp2 = "#9bd4b8";
                                                System.Drawing.Color _colorUp2 = System.Drawing.ColorTranslator.FromHtml(hexUp2);
                                                gvTeamsView.Rows[i + 1].BackColor = _colorUp2;
                                            }
                                        }
                                    }

                                }
                            }



                            if (total == 10)
                            {

                                for (int i = 0; i < gvTeamsView.Rows.Count; i = i + 4)
                                {
                                    int j = filteredView.ToTable().Rows.Count / 2;


                                    if (gvTeamsView.Rows[i].RowIndex >= j)
                                    {

                                        if ((total == 10 && i == 8))
                                        {

                                            string hexDown = "#fda4a5";
                                            System.Drawing.Color _colorDown = System.Drawing.ColorTranslator.FromHtml(hexDown);

                                            gvTeamsView.Rows[i - 2].BackColor = _colorDown;

                                            string _hexDown = "#fd9798";
                                            System.Drawing.Color _color_Down = System.Drawing.ColorTranslator.FromHtml(_hexDown);

                                            gvTeamsView.Rows[i - 1].BackColor = _color_Down;


                                            string hexDown1 = "#fb8c8e";
                                            System.Drawing.Color _colorDown1 = System.Drawing.ColorTranslator.FromHtml(hexDown1);

                                            gvTeamsView.Rows[i].BackColor = _colorDown1;

                                            if (gvTeamsView.Rows[i].RowIndex <= total - 1)
                                            {

                                                string hexDown2 = "#fd8183";
                                                System.Drawing.Color _colorDown2 = System.Drawing.ColorTranslator.FromHtml(hexDown2);

                                                gvTeamsView.Rows[i + 1].BackColor = _colorDown2;
                                            }
                                            //if (gvTeamsView.Rows[i].RowIndex <= total - 1)
                                            //{

                                            //    string hexDown3 = "#fd8183";
                                            //    System.Drawing.Color _colorDown3 = System.Drawing.ColorTranslator.FromHtml(hexDown3);

                                            //    gvTeamsView.Rows[i + 2].BackColor = _colorDown3;
                                            //}

                                        }
                                    }
                                    else
                                    {
                                        if (i < 4)
                                        {

                                            string hexUp1 = "#7ed5aa";
                                            System.Drawing.Color _colorUp1 = System.Drawing.ColorTranslator.FromHtml(hexUp1);
                                            gvTeamsView.Rows[i].BackColor = _colorUp1;


                                            if (gvTeamsView.Rows[i].RowIndex < total - 1)
                                            {

                                                string hexUp2 = "#8cd6b1";
                                                System.Drawing.Color _colorUp2 = System.Drawing.ColorTranslator.FromHtml(hexUp2);
                                                gvTeamsView.Rows[i + 1].BackColor = _colorUp2;
                                            }

                                            if (gvTeamsView.Rows[i].RowIndex < total - 1)
                                            {

                                                string hexUp3 = "#9bd4b8";
                                                System.Drawing.Color _colorUp3 = System.Drawing.ColorTranslator.FromHtml(hexUp3);
                                                gvTeamsView.Rows[i + 2].BackColor = _colorUp3;
                                            }

                                            if (gvTeamsView.Rows[i].RowIndex < total - 1)
                                            {

                                                string hexUp3 = "#b4d2c3";
                                                System.Drawing.Color _colorUp3 = System.Drawing.ColorTranslator.FromHtml(hexUp3);
                                                gvTeamsView.Rows[i + 3].BackColor = _colorUp3;
                                            }
                                        }
                                    }

                                }
                            }


                            /*For More than 10 Rows */
                            if (total == 20)
                            {

                                for (int i = 0; i < gvTeamsView.Rows.Count; i = i + 4)
                                {
                                    int j = filteredView.ToTable().Rows.Count / 2;


                                    if (gvTeamsView.Rows[i].RowIndex >= j)
                                    {


                                        if (i > 12)
                                        {

                                            string hexDown1 = "#fab7b8";
                                            System.Drawing.Color _colorDown1 = System.Drawing.ColorTranslator.FromHtml(hexDown1);

                                            gvTeamsView.Rows[i].BackColor = _colorDown1;

                                            if (gvTeamsView.Rows[i].RowIndex <= total - 1)
                                            {

                                                string hexDown2 = "#fda4a5";
                                                System.Drawing.Color _colorDown2 = System.Drawing.ColorTranslator.FromHtml(hexDown2);

                                                gvTeamsView.Rows[i + 1].BackColor = _colorDown2;
                                            }
                                            if (gvTeamsView.Rows[i].RowIndex <= total - 1)
                                            {

                                                string hexDown3 = "#fd9798";
                                                System.Drawing.Color _colorDown3 = System.Drawing.ColorTranslator.FromHtml(hexDown3);

                                                gvTeamsView.Rows[i + 2].BackColor = _colorDown3;
                                            }

                                            if (gvTeamsView.Rows[i].RowIndex <= total - 1)
                                            {

                                                string hexDown3 = "#fb8c8e";
                                                System.Drawing.Color _colorDown3 = System.Drawing.ColorTranslator.FromHtml(hexDown3);

                                                gvTeamsView.Rows[i + 3].BackColor = _colorDown3;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (i < 4)
                                        {

                                            string hexUp1 = "#7ed5aa";
                                            System.Drawing.Color _colorUp1 = System.Drawing.ColorTranslator.FromHtml(hexUp1);
                                            gvTeamsView.Rows[i].BackColor = _colorUp1;


                                            if (gvTeamsView.Rows[i].RowIndex < total - 1)
                                            {

                                                string hexUp2 = "#8cd6b1";
                                                System.Drawing.Color _colorUp2 = System.Drawing.ColorTranslator.FromHtml(hexUp2);
                                                gvTeamsView.Rows[i + 1].BackColor = _colorUp2;
                                            }

                                            if (gvTeamsView.Rows[i].RowIndex < total - 1)
                                            {

                                                string hexUp3 = "#9bd4b8";
                                                System.Drawing.Color _colorUp3 = System.Drawing.ColorTranslator.FromHtml(hexUp3);
                                                gvTeamsView.Rows[i + 2].BackColor = _colorUp3;
                                            }

                                            if (gvTeamsView.Rows[i].RowIndex < total - 1)
                                            {

                                                string hexUp3 = "#b4d2c3";
                                                System.Drawing.Color _colorUp3 = System.Drawing.ColorTranslator.FromHtml(hexUp3);
                                                gvTeamsView.Rows[i + 3].BackColor = _colorUp3;
                                            }


                                        }
                                    }
                                }
                            }



                            #endregion

                        }

                        else if (SportStageValue == 4)
                        {
                            #region For Junior League


                            //if (total == 10)
                            //{
                            for (int i = 0; i < gvTeamsView.Rows.Count; i++)
                            {
                                if (i == 0)
                                {
                                    string hexUp1 = "#259025";
                                    System.Drawing.Color _colorUp1 = System.Drawing.ColorTranslator.FromHtml(hexUp1);
                                    gvTeamsView.Rows[i].BackColor = _colorUp1;
                                }

                                if (i == 1)
                                {
                                    string hexUp1 = "#3a9b3a";
                                    System.Drawing.Color _colorUp1 = System.Drawing.ColorTranslator.FromHtml(hexUp1);
                                    gvTeamsView.Rows[i].BackColor = _colorUp1;
                                }

                                if (i == 2)
                                {
                                    string hexUp1 = "#50a650";
                                    System.Drawing.Color _colorUp1 = System.Drawing.ColorTranslator.FromHtml(hexUp1);
                                    gvTeamsView.Rows[i].BackColor = _colorUp1;
                                }

                                if (i == 3)
                                {
                                    string hexUp1 = "#66b166";
                                    System.Drawing.Color _colorUp1 = System.Drawing.ColorTranslator.FromHtml(hexUp1);
                                    gvTeamsView.Rows[i].BackColor = _colorUp1;
                                }

                                if (i == 4)
                                {
                                    string hexUp1 = "#7cbc7c";
                                    System.Drawing.Color _colorUp1 = System.Drawing.ColorTranslator.FromHtml(hexUp1);
                                    gvTeamsView.Rows[i].BackColor = _colorUp1;
                                }

                                if (i == 5)
                                {
                                    string hexUp1 = "#92c792";
                                    System.Drawing.Color _colorUp1 = System.Drawing.ColorTranslator.FromHtml(hexUp1);
                                    gvTeamsView.Rows[i].BackColor = _colorUp1;
                                }

                                if (i == 6)
                                {
                                    string hexUp1 = "#a7d2a7";
                                    System.Drawing.Color _colorUp1 = System.Drawing.ColorTranslator.FromHtml(hexUp1);
                                    gvTeamsView.Rows[i].BackColor = _colorUp1;
                                }

                                if (i == 7)
                                {
                                    string hexUp1 = "#bdddbd";
                                    System.Drawing.Color _colorUp1 = System.Drawing.ColorTranslator.FromHtml(hexUp1);
                                    gvTeamsView.Rows[i].BackColor = _colorUp1;
                                }

                                if (i == 8)
                                {
                                    string hexUp1 = "#FD9798";
                                    System.Drawing.Color _colorUp1 = System.Drawing.ColorTranslator.FromHtml(hexUp1);
                                    gvTeamsView.Rows[i].BackColor = _colorUp1;
                                }

                                if (i == 9)
                                {
                                    string hexUp1 = "#FD8183";
                                    System.Drawing.Color _colorUp1 = System.Drawing.ColorTranslator.FromHtml(hexUp1);
                                    gvTeamsView.Rows[i].BackColor = _colorUp1;
                                }
                            }
                            // }

                            #endregion
                        }



                        lblSelectGroup.Text = filteredView.ToTable().Rows[0]["CompetitionGroupName"].ToString();
                    }
                    else
                    {
                        e.Item.Visible = false;
                    }

                    if (IsMiniControl == 1)
                    {
                        Literal ltrlWin = e.Item.FindControl("ltrlWin") as Literal;
                        if (ltrlWin != null)
                            ltrlWin.Visible = false;

                    }
                }
            }
        }

        protected void gvTeamsView_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt1 = new DataTable();
            GridView gvGoalStats = (GridView)sender;
            if (gvGoalStats != null && gvGoalStats.Rows.Count > 0)
            {
                dt1 = (DataTable)gvGoalStats.DataSource;
                string separator = CompetitionId + "_" + dt1.Rows[0]["CompetitionGroupID"];

                if (Session["Sort_Order" + separator].ToString() == "ASC")
                {
                    RebindData(e.SortExpression, "DESC", dt1, gvGoalStats);
                }
                else
                {
                    RebindData(e.SortExpression, "ASC", dt1, gvGoalStats);
                }
            }
        }

        protected void RebindData(string sColimnName, string sSortOrder, DataTable dt, GridView gvGoalStats)
        {
            dt.DefaultView.Sort = sColimnName + " " + sSortOrder;
            if (dt.Rows.Count > 0)
            {
                string separator = CompetitionId + "_" + dt.Rows[0]["CompetitionGroupID"];
                gvGoalStats.DataSource = dt;
                gvGoalStats.DataBind();
                Session["TeamRank" + separator] = sColimnName;
                Session["Sort_Order" + separator] = sSortOrder;

                HiddenField hf = (HiddenField)this.Parent.FindControl("currentTabIndex");
                if (hf != null)
                {
                    hf.Value = "1";
                }
            }
        }
    }
}