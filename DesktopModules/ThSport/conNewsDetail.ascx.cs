using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using System.IO;
using ThSportServer;
using System.Data;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Common;

namespace DotNetNuke.Modules.ThSport
{
    public partial class conNewsDetail : PortalModuleBase
    {
        DotNetNuke.Entities.Tabs.TabController tabs1 = new Entities.Tabs.TabController();
        DotNetNuke.Entities.Tabs.TabInfo tInfo1 = new Entities.Tabs.TabInfo();

        clsNewsController newsController = new clsNewsController();

        int NewsID
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["NewsID"] != null))
                {
                    int.TryParse(Request.QueryString["NewsID"].ToString(), out retVal);
                    return retVal;
                }
                return 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FillNews(NewsID);

            //string m_ModuelControl = "conNewsComments.ascx";
            //PortalModuleBase objPortalModuleBase = (PortalModuleBase)LoadControl(m_ModuelControl);
            //objPortalModuleBase.ModuleConfiguration = ModuleConfiguration;
            //objPortalModuleBase.ID = System.IO.Path.GetFileNameWithoutExtension(m_ModuelControl);
            //phNewsComment.Controls.Add(objPortalModuleBase);

            try
            {
                LoadModuleControl();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void LoadModuleControl()
        {
            string m_ModuelControl = "";
            if (Request.QueryString["mctl"] != null && Request.QueryString["mctl"] != "conNewsDetail")//3. Read control name from querystring
            {
                m_ModuelControl = Request.QueryString["mctl"].ToString() + ".ascx";
                PortalModuleBase objPortalModuleBase = (PortalModuleBase)LoadControl(m_ModuelControl);
                objPortalModuleBase.ModuleConfiguration = ModuleConfiguration;
                objPortalModuleBase.ID = System.IO.Path.GetFileNameWithoutExtension(m_ModuelControl);
                phNewsComment.Controls.Add(objPortalModuleBase);

                pnlNewsDetail.Visible = false;
                phNewsComment.Visible = true;
            }
        }

        private void FillNews(int NewsID)
        {
            DataTable dt = newsController.GetNewsDataByNewsID(NewsID);

            if (dt.Rows.Count > 0)
            {
                ltrlTitle.Text = dt.Rows[0]["NewsTitle"].ToString();
                ltrlDate.Text = dt.Rows[0]["CreatedOnDateChange"].ToString();
                ltrlCompDesc.Text = dt.Rows[0]["NewsDesc"].ToString();

                if (!System.IO.File.Exists(Server.MapPath("DesktopModules\\ThSport\\" + dt.Rows[0]["NewsPicture"].ToString())))
                {
                    imgNews.ImageUrl = "~/DesktopModules/ThSport/Images/NewsImages/no_news.png";
                }
                else
                {
                    imgNews.ImageUrl = Page.ResolveUrl("~/DesktopModules/ThSport/" + dt.Rows[0]["NewsPicture"].ToString());
                }
            }

            int prvid = 0;
            prvid = NewsID - 1;
            dt = newsController.GetNewsDataByNewsID(prvid);

            if (dt.Rows.Count > 0)
            {
                hdPrv.Value = dt.Rows[0]["NewsId"].ToString();
                litPrv.Text = dt.Rows[0]["NewsTitle"].ToString();
                lbPrvDate.Text = dt.Rows[0]["CreatedOnDateChange"].ToString();

                lbPrvReadMore.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "conNewsDetail", "NewsID=" + prvid);
            }
            else
            {
                lbPrvReadMore.Visible = false;
            }

            int NextID = 0;
            NextID = NewsID + 1;
            dt = newsController.GetNewsDataByNewsID(NextID);

            if (dt.Rows.Count > 0)
            {
                hdNextNewsId.Value = dt.Rows[0]["NewsId"].ToString();
                litNext.Text = dt.Rows[0]["NewsTitle"].ToString();
                lbNextDate.Text = dt.Rows[0]["CreatedOnDateChange"].ToString();

                lbNextReadMore.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "conNewsDetail", "NewsID=" + NextID);
            }
            else
            {
                lbNextReadMore.Visible = false;
            }
        }

        protected void likGolf_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "conNewsDisplay"));
        }

        protected void likKick_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "conNewsDisplay"));
        }

        protected void likPlayer_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "conNewsDisplay"));
        }

        protected void likSports_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "conNewsDisplay"));
        }

        protected void lbNextPost_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "conNewsDisplay"));
        }

        protected void lbPreviousPost_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "conNewsDisplay"));
        }
    }
}