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
    public class clsCompetitionPlayerPerfomance
    {
        public int CompetitionPlayerPerfomanceID { get; set; }
        public int CompetitionID { get; set; }
        public int PlayerID { get; set; }
        public int NoOfRedCard { get; set; }
        public int NoOfYellowCard { get; set; }
        public int NoOfRedCardTheirYellowCard { get; set; }
        public int Suspended { get; set; }
        public int PortalID { get; set; }
        public string CreatedBy { get; set; }
        public string ModifyBy { get; set; }
    }

    public class clsCompetitionPlayerPerfomanceController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update Method

        public int InsertCompetitionPlayerPerfomance(clsCompetitionPlayerPerfomance cpp)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertCompetitionPlayerPerfomance", cpp.CompetitionID, cpp.PlayerID, cpp.NoOfRedCard, cpp.NoOfYellowCard, cpp.NoOfRedCardTheirYellowCard, cpp.Suspended, cpp.PortalID, cpp.CreatedBy, cpp.ModifyBy);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateCompetitionPlayerPerfomance(clsCompetitionPlayerPerfomance cpp)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateCompetitionPlayerPerfomance", cpp.CompetitionID, cpp.PlayerID, cpp.NoOfRedCard, cpp.NoOfYellowCard, cpp.NoOfRedCardTheirYellowCard, cpp.CreatedBy, cpp.ModifyBy);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateSuspendedCount(int SuspendedCount, int playerId, int competitionId)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateSuspendedCount", SuspendedCount, playerId, competitionId);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }


        public int UpdateRedYellow(int total_yellow_card, int RTYCard, int competitionId, int playerId)
        {
            string query = String.Empty;

            try
            {
                if (RTYCard == 0)
                {
                    if (total_yellow_card == 3)
                    {
                        query = "Update tblCompetitionPlayerPerfomance set NoOfYellowCard  = 0 Where PlayerID = " + playerId + " And CompetitionID = " + competitionId;
                    }
                    else
                    {
                        query = "Update tblCompetitionPlayerPerfomance set NoOfRedCard  = 0 Where PlayerID = " + playerId + " And CompetitionID = " + competitionId;
                    }
                }
                else if (RTYCard == 1 && total_yellow_card == 2)
                {
                    query = "Update tblCompetitionPlayerPerfomance set NoOfRedCard  = 0,NoOfRedCardThroughYellowCard = 0 Where PlayerID = " + playerId + " And CompetitionID = " + competitionId;
                }

                dataProvider.ExecuteSQL(query);
            }

            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        #endregion Insert,Update Method

        #region Getdata Method

        public DataTable GetCompetitionPlayerPerfomanceData(int competitionId, int playerId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCompetitionPlayerPerfomanceData", competitionId, playerId))
                    {
                        dt.Load(reader);
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
