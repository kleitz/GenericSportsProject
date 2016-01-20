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
    public class clsCompetitionLeague
    {
        public int CompetitionLeagueId;
        public string CompetitionLeagueName;
        public string CompeititionLeagueAbbr;
        public string CompetitionLeagueDesc;
        public string CompetitionLeagueLogoName;
        public string CompetitionLeagueLogoFile;
        public int ActiveFlagId;
        public int ShowFlagId;
        public int PortalID;
        public string CreatedById;
        public string ModifiedById;
    }

    public class clsCompetitionLeagueController
    {

        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update Method

        public int InsertCompetitionLeague(clsCompetitionLeague cl)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertCompetitionLegaue", cl.CompetitionLeagueName, cl.CompeititionLeagueAbbr, cl.CompetitionLeagueDesc, cl.CompetitionLeagueLogoName, cl.CompetitionLeagueLogoFile, cl.ActiveFlagId, cl.ShowFlagId, cl.PortalID, cl.CreatedById, cl.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateCompetitionLeague(clsCompetitionLeague cl)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateCompetitionLegaue", cl.CompetitionLeagueId,cl.CompetitionLeagueName, cl.CompeititionLeagueAbbr, cl.CompetitionLeagueDesc, cl.CompetitionLeagueLogoName, cl.CompetitionLeagueLogoFile, cl.ActiveFlagId, cl.ShowFlagId, cl.PortalID, cl.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        #endregion Insert,Update Method

        #region Getdata Method

        public DataTable GetCompetitionLeagueList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCompetitionLeagueList"))
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

        public DataTable GetCompetitionLeagueDetailByCompetitionLeagueID(int CompetitionTypeID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetCompetitionLeagueDetailByCompetitionLeagueID", CompetitionTypeID))
                {
                    dt.Load(rdr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        


        #endregion Getdata Methods

    }

}
