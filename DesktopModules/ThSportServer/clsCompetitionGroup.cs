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
    public class clsCompetitionGroup
    {
        public int CompetitionGroupId;
        public int CompetitionId;
        public string CompetitionGroupName;
        public string CompetitionGroupAbbr;
        public int NumberofTeams;
        public int ConfirmFlag;
        public int PortalID;
        public string CreatedById;
        public string ModifiedById;
    }

    public class clsCompetitionGroupController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update Method

        public int InsertCompetitionGroup(clsCompetitionGroup cg)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertCompetitionGroup", cg.CompetitionId, cg.CompetitionGroupName, cg.CompetitionGroupAbbr, cg.NumberofTeams, cg.ConfirmFlag, cg.CreatedById, cg.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateCompetitionGroup(clsCompetitionGroup cg)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateCompetitionGroup", cg.CompetitionGroupId, cg.CompetitionId, cg.CompetitionGroupName, cg.CompetitionGroupAbbr, cg.NumberofTeams, cg.ConfirmFlag, cg.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        #endregion Insert,Update Method

        #region Getdata Method

        public DataTable GetCompetitionGroupList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCompetitionGroupList"))
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

        public DataTable GetCompetitionGroupDetailByCompetitionGroupID(int CompetitionGroupID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetCompetitionGroupDetailByCompetitionGroupID", CompetitionGroupID))
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
