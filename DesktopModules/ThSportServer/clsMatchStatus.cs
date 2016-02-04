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
    public class clsMatchStatus
    {
        public int MatchStatusId { get; set; }
        public string MatchStatusName { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
    }

    public class clsMatchStatusController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update,Delete Methods

        public int InsertMatchStatus(clsMatchStatus ms)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertMatchStatus", ms.MatchStatusName ,ms.CreatedById, ms.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateMatchStatus(clsMatchStatus ms)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateMatchStatus", ms.MatchStatusId,ms.MatchStatusName, ms.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        //public int UpdateLocationName(string Name, int Id)
        //{
        //    int i = 0;
        //    try
        //    {
        //        dataProvider.ExecuteNonQuery("usp_UpdateLocationName", Name, Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return i;
        //    }
        //    return i;
        //}

        public int DeleteMatchStatus(int location_id)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_DeleteMatchStatus", location_id);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        #endregion Insert,Update,Delete Methods



        #region Getdata Methods


        //public DataTable FetchAllLocation(string current_user, string search_country, string search_LocationName)
        //{

        //    using (DataTable dt = new DataTable())
        //    {
        //        try
        //        {
        //            using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllLocation", current_user, search_country, search_LocationName))
        //            {
        //                dt.Load(reader);
        //                return dt;
        //            }
        //        }

        //        catch (Exception ex)
        //        {
        //            Exceptions.LogException(ex);
        //        }

        //        return dt;
        //    }
        //}

        public DataTable usp_GetMatchStatusByMatchStatusId(int id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetMatchStatusByMatchStatusId", id))
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

        public DataTable GetAllMatchStatus()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllMatchStatus"))
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

    }
}
