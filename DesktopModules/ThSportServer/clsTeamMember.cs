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
    public class clsTeamMember
    {
        public int TeamMemberID { get; set; }
        public int TeamID { get; set; }
        public int RegistrationId { get; set; }
        public int TeamMemberJerseyNo { get; set; }
        public string TeamMemberJerseyName { get; set; }
        public string TeamMemberFamousName { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public int TeamMemberTypeId { get; set; }
        public int PortalId { get; set; }
    }

    public class clsTeamMemberController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsTeamMemberController()
        {

        }

        #region Insert,Update Method

        public int InsertTeamMember(clsTeamMember ccm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertTeamMember", ccm.TeamID, ccm.RegistrationId, ccm.TeamMemberTypeId, ccm.TeamMemberJerseyNo, ccm.TeamMemberJerseyName, ccm.TeamMemberFamousName, ccm.PortalId, ccm.CreatedById, ccm.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateTeamMember(clsTeamMember ccm)
        {
            int i = 0;

            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateTeamMember", ccm.TeamMemberID,ccm.TeamID,ccm.RegistrationId,ccm.TeamMemberTypeId,ccm.TeamMemberJerseyNo,ccm.TeamMemberJerseyName,ccm.TeamMemberFamousName,ccm.PortalId,ccm.ModifiedById);
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

        public DataTable GetTeamMemberListByTeamID(int TeamID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamMemberListByTeamID", TeamID))
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

        public DataTable GetTeamMemberDetailByTeamMemberID(int TeamMemberID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetTeamMemberDetailByTeamMemberID", TeamMemberID))
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

        public DataTable GetTeamMemberType()
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetTeamMemberType"))
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

        public DataTable GetTeamMemberDetail()
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetTeamMemberDetail"))
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

    }
}
