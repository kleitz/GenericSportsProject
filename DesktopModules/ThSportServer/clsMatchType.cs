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
    public class clsMatchType
    {
        public int MatchTypeId { get; set; }
        public int SportID { get; set; }
        public string MatchTypeName { get; set; }
        public string MatchTypeDescription { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public int PortalID { get; set; }
    }

    public class clsMatchTypeController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update,Delete Methods

        public int InsertMatchType(clsMatchType ms)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertMatchType",ms.SportID, ms.MatchTypeName ,ms.MatchTypeDescription, ms.PortalID,ms.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateMatchType(clsMatchType ms)
        {
            try
            {
                dataProvider.ExecuteNonQuery("[usp_UpdateMatchType]",ms.SportID, ms.MatchTypeId, ms.MatchTypeName, ms.MatchTypeDescription,ms.ModifiedById);
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

        public int DeleteMatchType(int MatchType_ID)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_DeleteMatchType", MatchType_ID);
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

        public DataTable usp_GetMatchTypeByMatchTypeId(int id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("[usp_GetMatchTypeByMatchTypeId]", id))
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

        public DataTable GetAllMatchType()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("[usp_GetAllMatchType]"))
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
