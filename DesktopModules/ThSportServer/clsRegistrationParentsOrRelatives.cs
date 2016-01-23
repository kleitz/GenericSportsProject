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
    public class clsRegistrationParentsOrRelatives
    {
        public int RegisteredUserRelationId { get; set; }
        public int RegistrationId { get; set; }
        public int UserIdRelated { get; set; }
        public int UserTypeID { get; set; }
    }

    public class clsRegistrationParentsOrRelativesController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsRegistrationParentsOrRelativesController()
        {

        }

        #region Getdata Methods

        public DataTable GetRegistrationParentDetail(int RegistrationId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetRegistrationParentDetail", RegistrationId))
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


        public DataTable GetUserType()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetUserType"))
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

        public DataTable GetRegistrationParentsDetails()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetRegistrationParentsDetails"))
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

        #endregion Getdata Methods

        #region Insert,Update,Delete Methods

        public int InsertRegistrationParents(clsRegistrationParentsOrRelatives cc)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertRegistrationParents", cc.RegistrationId, cc.UserIdRelated, cc.UserTypeID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateAdminIDForRegistrationForm(clsRegistrationParentsOrRelatives cc)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateAdminIDForRegistrationForm", cc.RegistrationId, cc.UserIdRelated);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }
        

        public int UpdateRegistrationParents(clsRegistrationParentsOrRelatives cc)
        {
            int i = 0;
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateRegistrationParents", cc.RegisteredUserRelationId,cc.RegistrationId,cc.UserIdRelated,cc.UserTypeID);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        #endregion Insert,Update,Delete Methods

        public DataTable GetRegistationParentsDetailsByParentID(int UserRelationID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetRegistationParentsDetailsByParentID", UserRelationID))
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

        public DataTable GetUserNameByRegistrantionID(int RegistrationId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetUserNameByRegistrantionID", RegistrationId))
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
