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
    public class clsLocation
    {
        public int Loc_LocationID { get; set; }
        public string Loc_LocationName { get; set; }
        public string Loc_LocationAddress { get; set; }
        public string Loc_City { get; set; }
        public string Loc_State { get; set; }
        public string Loc_ZipCode { get; set; }
        public string Loc_Country { get; set; }
        public int PortalID { get; set; }
        public int SportID { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
    }

    public class clsLocationController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update,Delete Methods

        public int InsertLocation(clsLocation loc)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertLocation", loc.Loc_LocationName, loc.Loc_LocationAddress, loc.Loc_City, loc.Loc_State, loc.Loc_ZipCode, loc.Loc_Country, loc.PortalID, loc.SportID,loc.CreatedById, loc.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateLocation(clsLocation loc)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateLocation", loc.Loc_LocationID, loc.Loc_LocationName, loc.Loc_LocationAddress, loc.Loc_City, loc.Loc_State, loc.Loc_ZipCode, loc.Loc_Country, loc.PortalID, loc.SportID,loc.CreatedById, loc.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateLocationName(string Name, int Id)
        {
            int i = 0;
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateLocationName", Name, Id);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        public int DeleteLocation(int location_id)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_DeleteLocation", location_id);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        #endregion Insert,Update,Delete Methods



        #region Getdata Methods


        public DataTable FetchAllLocation(string current_user,string search_country,string search_LocationName)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllLocation", current_user,search_country,search_LocationName))
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

        public DataTable GetLocationByID(int id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetLocationByID", id))
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

        public DataTable GetCountryList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCountryList"))
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

        public DataTable GetCountryByID(int country_id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCountryByID", country_id))
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

        

        public DataTable SearchByAllLocationAndCompetitionAndTeam(string Name, int SelectID, int SelectCountryID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_SearchByAllLocationAndCompetitionAndTeam", Name, SelectID, SelectCountryID))
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
    }

}
