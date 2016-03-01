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
    public partial class conSponsorList : PortalModuleBase
    {
        DotNetNuke.Entities.Tabs.TabController tabs1 = new Entities.Tabs.TabController();

        DotNetNuke.Entities.Tabs.TabInfo tInfo1 = new Entities.Tabs.TabInfo();

        int SportStageValue = 5;

        private string currentpage;

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (currentpage != "Club Admin")
                titel.Text = "&raquo; Sponser Detail";
            else
                titel.Visible = false;

            clsSponsorController spo = new clsSponsorController();

            DataTable dt = spo.GetSponsorListForUserSide();

            if (dt.Rows.Count > 0)
            {
                SponsorList.DataSource = dt;
                SponsorList.DataBind();
            }

        }

        #endregion Page Events
    }
}