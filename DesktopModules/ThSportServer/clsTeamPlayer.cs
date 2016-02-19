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
    public class clsTeamPlayer
    {
           public int PlayerID { get; set; }
           public int TeamId { get; set; }
           public int RegistrationId { get; set; }
           public int PlayerJerseyNo { get; set; }
           public string PlayerJerseyName { get; set; }
            public string PlayerFamousName { get; set; }
            public string CreatedById { get; set; }
            public string ModifiedById { get; set; }
            public int PlayerTypeId { get; set; }
            public int PortalID { get; set; }

            public int TOutID { get; set; }
            public string TOutName { get; set; }
            public int TInID { get; set; }
            public string TInName { get; set; }
            public string PlayerPosition { get; set; }
    }

    public class clsTeamPlayerController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsTeamPlayerController()
        {

        }

        #region Insert,Update Method

        public int InsertTeamPlayer(clsTeamPlayer ccm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertTeamPlayer", ccm.TeamId, ccm.RegistrationId, ccm.PlayerJerseyNo, ccm.PlayerJerseyName, ccm.PlayerFamousName, ccm.CreatedById, ccm.ModifiedById, ccm.PlayerTypeId, ccm.PortalID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateTeamPlayer(clsTeamPlayer ccm)
        {
            int i = 0;

            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateTeamPlayer", ccm.PlayerID,ccm.TeamId,ccm.RegistrationId,ccm.PlayerJerseyNo,ccm.PlayerJerseyName,ccm.PlayerFamousName,ccm.ModifiedById,ccm.PlayerTypeId,ccm.PortalID);
                return i;
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return i;
        }

        #endregion Insert,Update Method
        
        public DataTable GetTeamNameByTeamID(int TeamID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetTeamNameByTeamID", TeamID))
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

        #region Getdata Method

        public DataTable GetTeamPlayerListByTeamID(int TeamID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamPlayerListByTeamID", TeamID))
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

        public DataTable GetPlayerDetailByPlayerID(int PlayerID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetPlayerDetailByPlayerID", PlayerID))
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

        public DataTable GetTeamPlayerType()
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetTeamPlayerType"))
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

        public DataTable GetPlayerList()
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetPlayerList"))
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

        public DataTable GetAllPlayerList()
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetAllPlayerList"))
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

        public DataTable GetTeamPlayerDetailByTeamID(int teamId)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("[usp_GetTeamPlayerDetailByTeamID]", teamId))
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

        public DataTable GetTeamAPlayerByMatchID(int MatchID,int TeamID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("[usp_GetTeamAPlayerByMatchID]", MatchID,TeamID))
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

        public DataTable GetTeamA1PlayerByMatchID(int MatchID,int TeamID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("[usp_GetTeamA1PlayerByMatchID]", MatchID,TeamID))
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

        public int DeleteTransferPlayerToMatchPlayerPerformance(int PlayerID)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_DeleteTransferPlayerToMatchPlayerPerformance", PlayerID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int DeleteTeamPlayer(int PlayerID)
        {
            int i = 0;

            try
            {
                dataProvider.ExecuteNonQuery("usp_DeleteTeamPlayer", PlayerID);
                return i;
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return i;
        }

        public DataTable GetTransferPlayerToOtherTeam(int TeamPlayerID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTransferPlayerToOtherTeam", TeamPlayerID))
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

        public DataTable GetPlayerNameByPlayerID(int PlayerID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetPlayerNameByPlayerID", PlayerID))
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

        public DataTable GetPlayerAllPositionList(int spoid)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetPlayerAllPositionList", spoid))
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

        public DataTable GetSportByPlayerID(int PlayerID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSportByPlayerID", PlayerID))
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

        // Get Player Detail By Player ID in Team Player 
        public DataTable GetPlayerDetailsBySelectedPlayerID(int PlayerID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetPlayerDetailsBySelectedPlayerID", PlayerID))
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

        // Transfer Player Entry Method     
        public int InsertTeamPlayerTransfer(clsTeamPlayer tpc)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertTeamPlayerTransfer", tpc.PlayerID, tpc.TOutID, tpc.TOutName, tpc.TInID, tpc.TInName, tpc.PortalID, tpc.CreatedById, tpc.ModifiedById, tpc.PlayerPosition);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public DataTable GetMasterPlayerIDByUserID(int PlayerID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetMasterPlayerIDByUserID", PlayerID))
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

        public DataTable EditTeamMasterPlayerCoach(int PlayerID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_EditTeamMasterPlayerCoach", PlayerID))
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

        public int DeleteTransferPlayerToTeamPlayer(int PlayerID)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_DeleteTransferPlayerToTeamPlayer", PlayerID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public DataTable MatchWisePlayerPerformancePlayerEntry(int TeamMasterID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_MatchWisePlayerPerformancePlayerEntry", TeamMasterID))
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

        public DataTable GetTeamIDByTeamMasterIDandCompetitionID(int TeamMasterID, int CompetitionID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamIDByTeamMasterIDandCompetitionID", TeamMasterID, CompetitionID))
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
