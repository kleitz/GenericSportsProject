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

namespace DotNetNuke.Modules.ThSport
{
    public class clsCompetition
    {
        public int CompetitionId;
        public int SeasonId;
        public int CompeitionLeagueId;
        public int SportId;
        public int CompetitionTypeId;
        public int DivisionId;
        public int CompetitionFormatId;
        public string CompetitionName;
        public string CompetitionAbbr;
        public string CompetitionDesc;
        public string CompetitionLogoName;
        public string CompetitionLogoFile;
        public DateTime StartDate;
        public DateTime EndDate;
        public int NumberofGroups;
        public int NumberofTeams;
        public int ActiveFlagId;
        public int ShowFlagId;
        public int PortalID;
        public string CreatedById;
        public string ModifiedById;
    }

    public class clsCompetitionController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update Method

        public int InsertCompetition(clsCompetition cmp)
        {
            try
            {
                if (cmp.StartDate == DateTime.MinValue)
                {
                    cmp.StartDate = DateTime.ParseExact("01/01/1900", "dd/mm/yyyy", null);
                }

                if (cmp.EndDate == DateTime.MinValue)
                {
                    cmp.EndDate = DateTime.ParseExact("01/01/1900", "dd/mm/yyyy", null);
                }

                dataProvider.ExecuteNonQuery("usp_InsertCompetition", cmp.SeasonId, cmp.CompeitionLeagueId, cmp.SportId, cmp.CompetitionTypeId, cmp.DivisionId, cmp.CompetitionFormatId, cmp.CompetitionName, cmp.CompetitionAbbr, cmp.CompetitionDesc, cmp.CompetitionLogoName, cmp.CompetitionLogoFile, cmp.StartDate, cmp.EndDate, cmp.NumberofGroups, cmp.NumberofTeams, cmp.ActiveFlagId, cmp.ShowFlagId, cmp.PortalID, cmp.CreatedById, cmp.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateCompetition(clsCompetition cmp)
        {
            try
            {

                if (cmp.StartDate == DateTime.MinValue)
                {
                    cmp.StartDate = DateTime.ParseExact("01/01/1900", "dd/mm/yyyy", null);
                }

                if (cmp.EndDate == DateTime.MinValue)
                {
                    cmp.EndDate = DateTime.ParseExact("01/01/1900", "dd/mm/yyyy", null);
                }

                dataProvider.ExecuteNonQuery("usp_UpdateCompetition", cmp.CompetitionId,cmp.SeasonId, cmp.CompeitionLeagueId, cmp.SportId, cmp.CompetitionTypeId, cmp.DivisionId, cmp.CompetitionFormatId, cmp.CompetitionName, cmp.CompetitionAbbr, cmp.CompetitionDesc, cmp.CompetitionLogoName, cmp.CompetitionLogoFile, cmp.StartDate, cmp.EndDate, cmp.NumberofGroups, cmp.NumberofTeams, cmp.ActiveFlagId, cmp.ShowFlagId, cmp.PortalID, cmp.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int DeleteCompetition(int cid)
        {
            try
            {

                dataProvider.ExecuteNonQuery("usp_DeleteCompetition", cid);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public DataTable IsCompetitionHasOtherData(int competitionID)
        {
            string returnvalue = "";
            DataTable dt = new DataTable();
            try
            {

                using (IDataReader reader = dataProvider.ExecuteReader("[usp_IsCompetitionHasOtherData]", competitionID,""))
                {
                    dt.Load(reader);
                    return dt;
                }

                //returnvalue = dataProvider.ExecuteReader("[usp_IsCompetitionHasOtherData]", cmp.CompetitionId);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            //return 0;
            return dt;
        }


        #endregion Insert,Update Method

        #region Getdata Method

        public DataTable GetCompetitionList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCompetitionList"))
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

        public DataTable GetCompetitionDetailByCompetitionID(int CompetitionID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetCompetitionDetailByCompetitionID", CompetitionID))
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