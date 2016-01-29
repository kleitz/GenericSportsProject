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
    public class clsClubOwner
    {
        public int ClubOwnersId { get; set; }
        public int ClubId { get; set; }
        public int RegistrationId { get; set; }
	    public string OwnerDescription { get; set; } 
	    public int OwnerPercentage { get; set; }
	    public int PortalID { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
    }

    public class clsClubOwnerController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsClubOwnerController()
        {

        }

        #region Insert,Update Method

        public int InsertClubOwner(clsClubOwner cco)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertClubOwner", cco.ClubId, cco.RegistrationId, cco.OwnerDescription, cco.OwnerPercentage, cco.PortalID, cco.CreatedById, cco.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateClubOwner(clsClubOwner cco)
        {
            int i = 0;

            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateClubOwner", cco.ClubOwnersId,cco.ClubId,cco.RegistrationId,cco.OwnerDescription,cco.OwnerPercentage,cco.PortalID,cco.ModifiedById);
                return i;
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return i;
        }

        #endregion Insert,Update Method


        public DataTable GetClubNameByClubID(int ClubID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetClubNameByClubID", ClubID))
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

        public DataTable GetClubOwnerList(int ClubID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetClubOwnerList", ClubID))
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

        public DataTable GetClubOwnerDetailByClubOwnerID(int ClubOwnerID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetClubOwnerDetailByClubOwnerID", ClubOwnerID))
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

        public DataTable GetClubOwnerbyUserForm()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetClubOwnerbyUserForm"))
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

    }
}
