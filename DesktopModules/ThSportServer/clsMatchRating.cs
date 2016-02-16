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
    public class clsMatchRating
    {
        public int MatchRatingId { get; set; }
        public int MatchId { get; set; }
        public int TeamARating { get; set; }
        public int TeamBRating { get; set; }
        public int TeamAId { get; set; }
        public int TeamBId { get; set; }
        public string Description { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public int PortalID { get; set; }
    }

    public class clsMatchRatingController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update,Delete Methods

        public int InsertMatchRating(clsMatchRating ms)
        {
            try
            {
                dataProvider.ExecuteNonQuery("[usp_InsertMatchRating]", ms.MatchId, ms.TeamARating, ms.TeamBRating,ms.TeamAId,ms.TeamBId,ms.Description, ms.PortalID,ms.CreatedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateMatchRating(clsMatchRating ms)
        {
            try
            {
                dataProvider.ExecuteNonQuery("[usp_UpdateMatchRating]",ms.MatchRatingId, ms.MatchId, ms.TeamARating, ms.TeamBRating, ms.TeamAId, ms.TeamBId, ms.Description, ms.PortalID, ms.ModifiedById);
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

        //public int DeleteMatchType(int MatchType_ID)
        //{
        //    try
        //    {
        //        dataProvider.ExecuteNonQuery("usp_DeleteMatchType", MatchType_ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.LogException(ex);
        //    }
        //    return 0;
        //}

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

        public DataTable GetMatchRatingByMatchRatingId(int id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("[usp_GetMatchRatingByMatchRatingId]", id))
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

        public DataTable GetAllMatchRatingByMatchId(int MatchId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("[usp_GetAllMatchRatingByMatchId]", MatchId))
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

        public DataTable GetTeamsDetailByMatchId(int MatchId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("[usp_GetTeamsDetailByMatchId]", MatchId))
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
