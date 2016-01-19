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
    public class clsCompetitionType
    {
        public int CompetitionTypeId;
        public string CompetitionTypeName;
        public string CompetitionTypeDesc;
        public int ActiveFlagId;
        public int ShowFlagId;
        public int PortalID;
        public string CreatedById;
        public string ModifiedById;

    }

    public class clsCompetitionTypeController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update Method

        public int InsertCompetitionType(clsCompetitionType ccm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertCompetitionType", ccm.CompetitionTypeName,ccm.CompetitionTypeDesc,ccm.ActiveFlagId,ccm.ShowFlagId,ccm.PortalID,ccm.CreatedById,ccm.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateCompetitionType(clsCompetitionType ccm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateCompetitionType", ccm.CompetitionTypeId , ccm.CompetitionTypeName, ccm.CompetitionTypeDesc, ccm.ActiveFlagId, ccm.ShowFlagId, ccm.PortalID, ccm.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        #endregion Insert,Update Method

        #region Getdata Method

        public DataTable GetCompetitionTypeList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCompetitionTypeList"))
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

        public DataTable GetCompetitionTypeDetailByCompetitionTypeID(int CompetitionTypeID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetCompetitionTypeDetailByCompetitionTypeID", CompetitionTypeID))
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
