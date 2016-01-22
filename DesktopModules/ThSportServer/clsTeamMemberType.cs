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
    public class clsTeamMemberType
    {
            public int TeamMemberTypeId { get; set; }
            public string TeamMemberTypeValue { get; set; }
            public string TeamMemberTypeDesc { get; set; }
            public int ActiveFlagId { get; set; }
            public int ShowFlagId { get; set; }
            public int PortalID { get; set; }
            public string CreatedById { get; set; }
            public string ModifiedById { get; set; }
    }

     public class clsTeamMemberTypeController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsTeamMemberTypeController()
        {

        }

        #region Insert,Update Method

        public int InsertTeamMemberType(clsTeamMemberType ccm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertTeamMemberType", ccm.TeamMemberTypeValue, ccm.TeamMemberTypeDesc, ccm.ActiveFlagId, ccm.ShowFlagId, ccm.PortalID, ccm.CreatedById, ccm.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateTeamMemberType(clsTeamMemberType ccm)
        {
            int i = 0;

            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateTeamMemberType", ccm.TeamMemberTypeId,ccm.TeamMemberTypeValue,ccm.TeamMemberTypeDesc,ccm.ActiveFlagId,ccm.ShowFlagId,ccm.PortalID,ccm.ModifiedById);
                return i;
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return i;
        }

        #endregion Insert,Update Method

        #region Getdata Method

        public DataTable GetTeamMemberTypeList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamMemberTypeList"))
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

        public DataTable GetTeamMemberTypeDetailByTeamMemberTypeID(int TeamMemberTypeID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetTeamMemberTypeDetailByTeamMemberTypeID", TeamMemberTypeID))
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
