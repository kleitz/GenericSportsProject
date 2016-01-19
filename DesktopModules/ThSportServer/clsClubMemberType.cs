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
    public class clsClubMemberType
    {
       public int ClubMemberTypeId { get; set;}
       public string ClubMemberTypeValue { get; set; }
       public string ClubMemberTypeDesc { get; set; }
       public int ActiveFlagId { get; set; }
       public int ShowFlagId { get; set;}
       public int PortalID { get; set; }
       public string CreatedById { get; set; }
       public string ModifiedById { get; set; }
    }

    public class clsClubMemberTypeController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsClubMemberTypeController()
        {

        }

        #region Insert,Update Method

        public int InsertClubMemberType(clsClubMemberType ccm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertClubMemberType", ccm.ClubMemberTypeValue, ccm.ClubMemberTypeDesc, ccm.ActiveFlagId, ccm.ShowFlagId, ccm.PortalID, ccm.CreatedById, ccm.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateClubMemberType(clsClubMemberType ccm)
        {
            int i = 0;

            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateClubMemberType", ccm.ClubMemberTypeId,ccm.ClubMemberTypeValue,ccm.ClubMemberTypeDesc,ccm.ActiveFlagId,ccm.ShowFlagId,ccm.PortalID,ccm.ModifiedById);
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

        public DataTable GetClubMemberTypeList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetClubMemberTypeList"))
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

        public DataTable GetClubMemberTypeDetailByClubMemberTypeID(int ClubMemberTypeID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetClubMemberTypeDetailByClubMemberTypeID", ClubMemberTypeID))
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

        public DataTable GetClubMemberType()
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetClubMemberType"))
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
