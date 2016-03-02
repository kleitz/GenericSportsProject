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
using DotNetNuke.Common;
using DotNetNuke.Services.Exceptions;

namespace DotNetNuke.Modules.ThSport
{
    public partial class conNewsDisplay : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        DotNetNuke.Entities.Tabs.TabController tabs1 = new Entities.Tabs.TabController();
        DotNetNuke.Entities.Tabs.TabInfo tInfo1 = new Entities.Tabs.TabInfo();
        clsSportController spController = new clsSportController();
        clsNewsController newsController = new clsNewsController();

        #region Page Events

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

        protected void Page_Load(object sender, EventArgs e)
        {
            using (DataTable sportData = spController.GetSportDetailBySportName("Football Games"))
            {
                Sport_ID = Convert.ToInt32(sportData.Rows[0]["SportID"].ToString());
            }

            pnlNewsDisplay.Visible = true;
            phNewDeatil.Visible = false;
            title.Text = "&raquo; News";

            FillNews();

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
            if (Request.QueryString["mctl"] != null && Request.QueryString["mctl"] != "conNewsDisplay")//3. Read control name from querystring
            {
                m_ModuelControl = Request.QueryString["mctl"].ToString() + ".ascx";
                PortalModuleBase objPortalModuleBase = (PortalModuleBase)LoadControl(m_ModuelControl);
                objPortalModuleBase.ModuleConfiguration = ModuleConfiguration;
                objPortalModuleBase.ID = System.IO.Path.GetFileNameWithoutExtension(m_ModuelControl);
                phNewDeatil.Controls.Add(objPortalModuleBase);
                pnlNewsDisplay.Visible = false;
                phNewDeatil.Visible = true;
            }
        }

        protected void rptrNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image img = e.Item.FindControl("ltrlImg") as Image;
                if (img != null)
                {
                    img.Visible = true;

                    if (img.ImageUrl == null || img.ImageUrl.Trim() == String.Empty || !img.ImageUrl.Contains("."))
                    {
                        img.Visible = false;
                    }
                    else if (img.ImageUrl != null)
                    {
                        bool FileOK = false;
                        String FileExtension = Path.GetExtension(img.ImageUrl).ToLower();
                        String[] allowedExtensions = { ".png", ".jpg", ".gif", ".jpeg" };
                        for (int i = 0; i < allowedExtensions.Length; i++)
                        {
                            if (FileExtension == allowedExtensions[i])
                            {
                                FileOK = true;
                                break;
                            }
                        }

                        if (FileOK == false)
                        {
                            img.Visible = false;
                        }
                    }
                }
            }
        }

        #endregion Page Events

        #region Methods

        private void FillNews()
        {

            DataTable dt = newsController.GetAllNewsDetailInfo(Sport_ID);

            if (dt.Rows.Count > 0)
            {
                rptrNews.DataSource = dt;
                rptrNews.DataBind();
            }
        }

        public string GetFormatedDate(object newsDate)
        {
            DateTime retVal = DateTime.Now;
            DateTime.TryParse(newsDate.ToString(), out retVal);
            string dateFormat = "<time datetime=\"" + retVal + "\" class=\"icon\">" +
                                "   <em>" + retVal.Year + "</em>" +
                                "    <strong>" + retVal.ToString("MMM") + "</strong>" +
                                "    <span>" + retVal.Day + "</span>" +
                                "</time>";
            return dateFormat;
        }

        #endregion Methods

        #region Repeater Events

        protected void rptrNews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("btnNewsDisplayReadMore"))
            {
                int news_id = 0;
                int.TryParse(e.CommandArgument.ToString(), out news_id);

                Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "conNewsDetail", "NewsID=" + news_id));
            }
        }

        #endregion
    }
}