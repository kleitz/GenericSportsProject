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
    public class clsMatchPlayerPerfomance
    {
        public int MatchPlayerPerfomanceID { get; set; }
        public int CompetitionID { get; set; }
        public int MatchId { get; set; }
        public int TeamID { get; set; }
        public int PlayerID { get; set; }
        public int Goal { get; set; }
        public int Assist { get; set; }
        public int Red { get; set; }
        public int Yellow { get; set; }
        public int IsPlayed { get; set; }
        public int OwnGoal { get; set; }
        public int PlayerSuspended { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public int PortalID { get; set; }

        // tblPlayerCardDetail

       
        public string CardName { get; set; }
        public string Remark { get; set; }
     

    }

    public class clsMatchPlayerPerfomanceController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update,Delete Methods

        public int InsertMatchPlayerPerfomance(clsMatchPlayerPerfomance mp)
        {
            try
            {
                dataProvider.ExecuteNonQuery("[usp_InsertMatchPlayerPerfomance]", mp.CompetitionID, mp.MatchId, mp.TeamID,mp.PlayerID, mp.Goal, mp.Assist, mp.Red, mp.Yellow, mp.IsPlayed,mp.OwnGoal,mp.PlayerSuspended,mp.PortalID, mp.CreatedById,mp.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int InsertPlayerCardDetail(clsMatchPlayerPerfomance mp)
        {
            try
            {
                dataProvider.ExecuteNonQuery("[usp_InsertPlayerCardDetail]", mp.MatchPlayerPerfomanceID, mp.CardName, mp.Remark);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }
        public int UpdatePlayerPerformance(clsMatchPlayerPerfomance mp)
        {
            try
            {
                dataProvider.ExecuteNonQuery("[usp_UpdatePlayerPerformance]",mp.MatchPlayerPerfomanceID, mp.CompetitionID,mp.MatchId,mp.TeamID,mp.PlayerID,mp.Goal,mp.Assist,mp.Red,mp.Yellow,mp.IsPlayed,mp.OwnGoal,mp.PlayerSuspended,mp.ModifiedById,mp.PortalID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdatePlayerPerformanceFlag(int MatchPlayerPerfomanceID, int flag)
        {
            try
            {
                dataProvider.ExecuteNonQuery("[usp_UpdatePlayerPerformanceFlag]", MatchPlayerPerfomanceID, flag);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateSuspendedCount(int PlayerSuspended, int playerID , int competitionId)
        {
            try
            {
                dataProvider.ExecuteNonQuery("[usp_UpdateSuspendedCount]",playerID,competitionId, PlayerSuspended );
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateSuspendedFlag(int matchId, int playerId, int isSuspended)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateSuspendedFlag", isSuspended, matchId, playerId);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateMatchResultPlayerPerformance(clsMatchPlayerPerfomance mr)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateMatchResultPlayerPerformance", mr.MatchPlayerPerfomanceID, mr.CompetitionID, mr.MatchId, mr.PlayerID, mr.Goal, mr.Assist, mr.PortalID, mr.CreatedById, mr.ModifiedById, mr.Yellow, mr.Red, mr.OwnGoal, mr.TeamID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int DeleteMatchPlayerPerformance(int MatchID, int PlayerID)
        {
            try
            {
                dataProvider.ExecuteNonQuery("[usp_DeleteMatchPlayerPerformance]", MatchID,PlayerID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        #endregion Insert,Update,Delete Methods



        #region Getdata Methods


        public DataTable GetMatchPlayerExists(int MatchID, int playerId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("[usp_GetMatchPlayerExists]", MatchID,playerId ))
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

        public DataTable GetSuspendedFlag(int CompetitionId,int PlayerId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("[usp_GetSuspendedFlag]", CompetitionId,PlayerId))
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

        public DataTable getPlayerGoal(clsMatchPlayerPerfomance mr)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetPlayerGoalByMatchPlayerID", mr.CompetitionID, mr.MatchId, mr.PlayerID))
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

        public DataTable getYelloowRed(clsMatchPlayerPerfomance mr)
        {
            DataTable dt = new DataTable();
            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_getYelloowRed", mr.CompetitionID, mr.MatchId, mr.PlayerID))
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

        public DataTable GetPlayerSuspendedFromPerformance(int competitionId, int matchId, int playerId)
        {
            using (DataTable returnTable = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetPlayerSuspendedFromPerformance", competitionId, matchId, playerId))
                    {
                        returnTable.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    Exceptions.LogException(ex);
                }
                return returnTable;
            }
        }

        public DataTable MatchWisePlayerPerformancePlayerEntry(int TeamID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_MatchWisePlayerPerformancePlayerEntry", TeamID))
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
        public DataTable MatchWisePlayerPerformancePlayerEntryByCompetitionIdAndTeamID(int TeamID,int competitionId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_MatchWisePlayerPerformancePlayerEntryByCompetitionIdAndTeamID", TeamID, competitionId))
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
