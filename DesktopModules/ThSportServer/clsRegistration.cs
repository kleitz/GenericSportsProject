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
    public class clsRegistration
    {
            public int UserId { get; set; }
            public int UserTypeId { get; set; }
            public string UserName { get; set; }
            public string UserPassword { get; set; }
            public string UserPhotoName { get; set; }
            public string UserPhotoFile { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public int SuffixId { get; set; }
            public string EmailId { get; set; }
            public string TelephoneNumber { get; set; }
            public int ActiveFlagId { get; set; }
            public int ShowFlagId { get; set; }
            public int PortalID { get; set; }
            public string CreatedById { get; set; }
            public string ModifiedById { get; set; }

            public int RegistrationId { get; set; }
            public int UserId_Admin { get; set; }
            public string Gender { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipPostalCode { get; set; }
            public int Country { get; set; }
            public string DateOfBirth { get; set; }
            public string PlaceOfBirth { get; set; }
            public string Height { get; set; }
            public string Weight { get; set; }

    }

    public class clsRegistrationController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsRegistrationController()
        {

        }

        #region Getdata Methods

        public DataTable GetDataClub()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDataClub"))
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

        public DataTable GetCountryList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCountryList"))
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

        public int InsertUser(clsRegistration cc)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertUser", cc.UserTypeId, cc.UserName, cc.UserPassword, cc.UserPhotoName, cc.UserPhotoFile, cc.FirstName, cc.MiddleName, cc.LastName, cc.SuffixId, cc.EmailId, cc.TelephoneNumber, cc.ActiveFlagId, cc.ShowFlagId, cc.PortalID, cc.CreatedById, cc.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int InsertRegistration(clsRegistration cc)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertRegistration", cc.UserId, cc.UserId_Admin, cc.Gender, cc.AddressLine1, cc.AddressLine2, cc.City, cc.State, cc.ZipPostalCode, cc.Country, Convert.ToDateTime(cc.DateOfBirth), cc.PlaceOfBirth, cc.Height, cc.Weight, cc.PortalID, cc.CreatedById, cc.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }


        public int UpdateClub(clsClub cc)
        {
            int i = 0;
            try
            {
                //CompReg.CreatedBy,
                dataProvider.ExecuteNonQuery("usp_UpdateClub", cc.ClubId, cc.ClubName, cc.ClubAbbr, cc.ClubDesc, cc.ClubFamousName, cc.ClubLogoName, cc.ClubLogoFile, cc.ClubPhotoFile, Convert.ToDateTime(cc.ClubEstablishedYear), cc.ActiveFlagId, cc.ShowFlagId, cc.PortalID, cc.ModifiedById);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        public int UpdateClubSports(int ClubID, int SportID)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateClubSports", ClubID, SportID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        #endregion Insert,Update,Delete Methods

        public DataTable GetUserDetailsByUserID(int UserID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetUserDetailsByUserID", UserID))
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

        public DataTable GetLatestUserID()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetLatestUserID"))
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

        public DataTable GetSportIDByClubID(int ClubID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSportIDByClubID", ClubID))
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

        public DataTable GetClubLogo(clsClub cc)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetClubLogo", cc.ClubId))
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

        public DataTable GetClubPhoto(clsClub cc)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_ClubPhoto", cc.ClubId))
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
