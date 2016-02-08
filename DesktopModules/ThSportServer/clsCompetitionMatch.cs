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
    public class clsCompetitionMatch
    {
        public int CompetitionMatchId;
        public int CompetitionId;
        public int LocationID;
        public DateTime StartDateTime;
        public DateTime EndDateTime;
        public int TeamAId;
        public int TeamBId;
        public int MatchStatusId;
        public int MatchTypeId;
        public int IsFinalized;
        public int PortalId;
        public int CreatedById { get; set; }
        public int ModifiedById { get; set; }
    }

    public class clsCompetitionMatchController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update Method

        public int InsertCompetitionMatch(clsCompetitionMatch cm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertCompetitionMatch", cm.CompetitionId, cm.LocationID, cm.StartDateTime, cm.EndDateTime, cm.TeamAId, cm.TeamBId, cm.MatchStatusId, cm.MatchTypeId,cm.IsFinalized,cm.PortalId,cm.CreatedById,cm.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateCompetitionMatch(clsCompetitionMatch cm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("[usp_UpdateCompetitionMatch]", cm.CompetitionMatchId, cm.CompetitionId, cm.LocationID, cm.StartDateTime, cm.EndDateTime, cm.TeamAId, cm.TeamBId, cm.MatchStatusId, cm.MatchTypeId, cm.IsFinalized, cm.PortalId, cm.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int DeleteCompetitionMatch(int gid)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_DeleteCompetitionGroup", gid);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        #endregion Insert,Update Method

        #region Getdata Method

        public DataTable GetCompetitionMatchList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCompetitionMatchList"))
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

        public DataTable GetCompetitionMatchDetailByCompetitionGroupID(int matchID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("[usp_GetCompetitionMatchDetailByMatchID]", matchID))
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
