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
    public class clsTeam
    {
        public int TeamId;
        public int ClubId;
        public int SportId;
        public int TeamTypeId;
        public string TeamName;
        public string TeamAbbr;
        public string TeamDesc;
        public string TeamFamousName;
        public string TeamLogoName;
        public string TeamLogoFile;
        public string TeamPhotoFile;
        public DateTime TeamEstablishedYear;
        public string TeamAnthemAudioFile;
        public int ActiveFlagId;
        public int ShowFlagId;
        public int PortalID;
        public string CreatedById;
        public string ModifiedById;
    }

    public class clsTeamController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update Method

        public int InsertTeam(clsTeam tm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertTeam", tm.ClubId, tm.SportId, tm.TeamName, tm.TeamAbbr,tm.TeamDesc, tm.TeamFamousName, tm.TeamLogoName, tm.TeamLogoFile, tm.TeamPhotoFile, tm.TeamEstablishedYear, tm.TeamAnthemAudioFile, tm.ActiveFlagId, tm.ShowFlagId, tm.PortalID, tm.CreatedById, tm.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateTeam(clsTeam tm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateTeam", tm.TeamId, tm.ClubId, tm.SportId, tm.TeamName, tm.TeamAbbr, tm.TeamDesc, tm.TeamFamousName, tm.TeamLogoName, tm.TeamLogoFile, tm.TeamPhotoFile, tm.TeamEstablishedYear, tm.TeamAnthemAudioFile, tm.ActiveFlagId, tm.ShowFlagId, tm.PortalID, tm.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int DeleteTeam(int cid)
        {
            try
            {

                dataProvider.ExecuteNonQuery("usp_DeleteTeam", cid);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        #endregion Insert,Update Method

        #region Getdata Method

        public DataTable GetTeamList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamList"))
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

        public DataTable GetTeamDetailByTeamID(int TeamID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetTeamDetailByTeamID", TeamID))
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

        public DataTable GetMasterTeamsNotInCompetitionTeam(int competition_id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetMasterTeamsNotInCompetitionTeam",competition_id))
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
        }

        #endregion Getdata Methods

    }

}
