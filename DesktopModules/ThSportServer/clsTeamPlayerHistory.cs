using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.OleDb;
using System.Configuration;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Modules;
using System.Data;
using DotNetNuke.Data;
using System.Data.SqlClient;
using DotNetNuke.Services.Exceptions;

namespace ThSportServer
{
    public class clsTeamPlayerHistory
    {
           public int TeamPlayerID { get; set; }
           public int TeamId { get; set; }
           public int RegistrationId { get; set; }
           public int PlayerJerseyNo { get; set; }
           public string PlayerJerseyName { get; set; }
            public string PlayerFamousName { get; set; }
            public string CreatedById { get; set; }
            public string ModifiedById { get; set; }
            public int PlayerTypeId { get; set; }
            public int PortalID { get; set; }
            public string PlayerPhoto { get; set; }
}

    public class clsTeamPlayerHistoryController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsTeamPlayerHistoryController()
        {

        }

        #region Insert,Update Method

        public int InsertTeamPlayerHistory(clsTeamPlayerHistory ccm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertTeamPlayerHistory",ccm.TeamPlayerID, ccm.TeamId, ccm.RegistrationId, ccm.PlayerJerseyNo, ccm.PlayerJerseyName, ccm.PlayerFamousName, ccm.CreatedById, ccm.ModifiedById, ccm.PlayerTypeId, ccm.PortalID, ccm.PlayerPhoto);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

       
        #endregion Insert,Update Method
        
     
      
       

    }
}
