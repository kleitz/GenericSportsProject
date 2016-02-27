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
    public class clsSeason
    {
        public int SeasonID { get; set; }
        public int CountryID { get; set; }
        public string SeasonName { get; set; }
        public string SeasonDesc { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SeasonLogoName { get; set; }
        public string SeasonLogoFile { get; set; }
        public int ActiveFlagID { get; set; }
        public int ShowFlagID { get; set; }
        public int PortalID { get; set; }
        public string CreatedByID { get; set; }
        public string ModifiedById { get; set; }
        
    }

    public class clsSeasonController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsSeasonController()
        {

        }

        #region Getdata Methods

        public DataTable GetDataSeason()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDataSeason"))
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

        public DataTable FillCountryDropdown()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_FillCountryDropdown"))
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

        public int InsertSeason(clsSeason cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertSeason", cs.CountryID,cs.SeasonName,cs.SeasonDesc, Convert.ToDateTime(cs.StartDate),Convert.ToDateTime(cs.EndDate),cs.SeasonLogoName,cs.SeasonLogoFile,cs.ActiveFlagID,cs.ShowFlagID,cs.PortalID,cs.CreatedByID,cs.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateSeason(clsSeason cs)
        {
            int i = 0;
            try
            {
                //CompReg.CreatedBy,
                dataProvider.ExecuteNonQuery("usp_UpdateSeason", cs.SeasonID, cs.CountryID, cs.SeasonName, cs.SeasonDesc, Convert.ToDateTime(cs.StartDate), Convert.ToDateTime(cs.EndDate), cs.SeasonLogoName, cs.SeasonLogoFile, cs.ActiveFlagID, cs.ShowFlagID, cs.PortalID, cs.ModifiedById);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        #endregion Insert,Update,Delete Methods

        public DataTable GetSeasonDataBySeasonID(int TorID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSeasonDataBySeasonID", TorID))
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

        public DataTable GetSeasonLogoBySeasonID(clsSeason cs)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSeasonLogoBySeasonID", cs.SeasonID))
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

        public DataTable GetSeasonListForCompetitionCupAndLeague(int sportID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSeasonListForCompetitionCupAndLeague", sportID))
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
