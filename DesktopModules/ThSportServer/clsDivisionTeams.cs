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
    public class clsDivisionTeams
    {
        public int DivisionTeamDetailId;
        public int DivisionId;
        public int TeamId;
        public int ConfirmFlag;
        public string CreatedById;
        public string ModifiedById;
    }

    public class clsDivisionTeamController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update Method

        public int InsertDivisionTeam(clsDivisionTeams dt)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertDivisionTeam", dt.DivisionId, dt.TeamId, dt.ConfirmFlag,dt.CreatedById, dt.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateDivisionTeam(clsDivisionTeams dt)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateDivisionTeam", dt.DivisionTeamDetailId, dt.DivisionId, dt.TeamId,dt.ConfirmFlag, dt.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int DeleteDivisionTeam(int dt_id)
        {
            try
            {

                dataProvider.ExecuteNonQuery("usp_DeleteDivisionTeam", dt_id);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        #endregion Insert,Update Method

        #region Getdata Method

        public DataTable GetTeamsByDivisionID(int Division_id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamsByDivisionID", Division_id))
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


        public DataTable GetDivisionTeamsByUser(string current_user, int Division_id, int team_id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDivisionTeamsByUser", current_user, Division_id, team_id))
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
