using DotNetNuke.Data;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ThSportServer
{
    public class clsCompetitionTeam
    {
        public int CompetitionTeamId;
        public int CompetitionId;
        public int CompetitionGroupId;
        public int TeamId;
        public string CreatedById;
        public string ModifiedById;
    }

    public class clsCompetitionTeamController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update Method

        public int InsertCompetitionTeam(clsCompetitionTeam ct)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertCompetitionTeam", ct.CompetitionId,ct.CompetitionGroupId,ct.TeamId,ct.CreatedById, ct.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateCompetitionTeam(clsCompetitionTeam ct)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateCompetitionTeam", ct.CompetitionTeamId, ct.CompetitionId,ct.CompetitionGroupId,ct.TeamId);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int DeleteCompetitionTeam(int ct_id)
        {
            try
            {

                dataProvider.ExecuteNonQuery("usp_DeleteCompetitionTeam", ct_id);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        #endregion Insert,Update Method

        #region Getdata Method

        public DataTable GetTeamsByCompetitionID(int competition_id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamsByCompetitionID", competition_id))
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


        public DataTable GetAllTeamsByUser(string current_user,int competition_id,int competitiongroup_id,int team_id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllTeamsByUser", current_user, competition_id, competitiongroup_id, team_id))
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

        #endregion

    }

}
