using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DotNetNuke.Data;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using System.Collections;

namespace ThSportServer
{
    [Serializable]
    public class clsClubSport
    {
        public int ClubId { get; set; }
        public int SportID { get; set; }
        public int PortalID { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
    }

    public class clsClubSportController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsClubSportController()
        {

        }

        #region Getdata Methods

        public DataTable GetClubSportListByClubId(int clubID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetClubSportListByClubId",clubID))
                    {
                        dt.Load(reader);
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    Exceptions.LogException(ex);
                }

                return dt;
            }
        }


        public DataTable GetSportNotAssignByClubId(int clubID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSportNotAssignByClubId", clubID))
                    {
                        dt.Load(reader);
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    Exceptions.LogException(ex);
                }

                return dt;
            }
        }

    

        #endregion Getdata Methods

        #region Insert,Update,Delete Methods

        public int InsertClubSports(clsClubSport cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertClubSports", cs.ClubId, cs.SportID, cs.PortalID,cs.CreatedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }
      
        public int UpdateClubSports(int ClubID, int SportID)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateClubSports", ClubID, SportID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int DeleteClubSports(int ClubSportID)
        {
            try
            {

                dataProvider.ExecuteNonQuery("[usp_DeleteClubSports]", ClubSportID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }
        #endregion Insert,Update,Delete Methods

        
    }
}
