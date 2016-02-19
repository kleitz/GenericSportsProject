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
    public class clsMatchResult
    {
        public int MatchResultID { get; set; }
        public int MatchID { get; set; }
        public int TeamATotal { get; set; }
        public int TeamBTotal { get; set; }
        public string Descr { get; set; }
         public int WinningTeam { get; set; }
         public int LosingTeam { get; set; }
        public int DrawTeam { get; set; }
         public Boolean IsPanlty { get; set; }
          public int TeamApanlty { get; set; }
        public int TeamBpanlty { get; set; }
         public Boolean isNoShow { get; set; }
         public int TossWinningTeam { get; set; }
        public int NoShowPenaltyPoint { get; set; }
         public string CreatedBy { get; set; }
        public string ModifyBy { get; set; }
        public int PortalID { get; set; }

        public int CompetitionID { get; set; }
        public int PlayerID { get; set; }
        public int Goal { get; set; }
        public int Assist { get; set; }
        public int IsPlayed { get; set; }
        public int Yellow { get; set; }
        public int Red { get; set; }
        public int TeamID { get; set; }
    }

    public class clsMatchResultController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update,Delete Methods

        public int InsertMatchResult(clsMatchResult mr)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertMatchResult", mr.MatchID, mr.TeamATotal, mr.TeamBTotal, mr.Descr, mr.PortalID, mr.CreatedBy, mr.ModifyBy, mr.WinningTeam, mr.LosingTeam, mr.DrawTeam, mr.IsPanlty, mr.TeamApanlty, mr.TeamBpanlty, mr.isNoShow, mr.TossWinningTeam, mr.NoShowPenaltyPoint);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int InsertMatchResultPlayerPerformance(clsMatchResult mr)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertMatchResultPlayerPerformance", mr.CompetitionID, mr.MatchID, mr.PlayerID, mr.Goal, mr.Assist, mr.PortalID, mr.CreatedBy, mr.ModifyBy, mr.Yellow, mr.Red, mr.IsPlayed, mr.TeamID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateMatchResult(clsMatchResult mr)
        {
            try
            {
                dataProvider.ExecuteNonQuery("[usp_UpdateMatchResult]", mr.MatchResultID, mr.MatchID, mr.TeamATotal, mr.TeamBTotal, mr.Descr, mr.PortalID, mr.CreatedBy, mr.WinningTeam, mr.LosingTeam, mr.DrawTeam, mr.ModifyBy, mr.IsPanlty, mr.TeamApanlty, mr.TeamBpanlty, mr.isNoShow, mr.TossWinningTeam,mr.NoShowPenaltyPoint);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }


        public int DeleteMatchResult(int MatchID)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_DeleteMatchResult", MatchID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }
  
        #endregion Insert,Update,Delete Methods



        #region Getdata Methods

        public DataTable GetCountMatchID(int mr)
        {
            DataTable dt = new DataTable();
            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetCountMatchID", mr))
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

        public DataTable GetTeamGoalTotalMatchID(int mr)
        {
            DataTable dt = new DataTable();
            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetTeamGoalTotalMatchID", mr))
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

        #endregion Getdata Methods

    }
}
