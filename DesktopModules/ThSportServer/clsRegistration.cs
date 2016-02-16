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
        // Filed in User Table
            public int UserId { get; set; }
            public int UserRoleId { get; set; }
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

        // Filed in Registration
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

        // Filed in Player Table
            public int TeamId { get; set; }
            public int PlayerJerseyNo { get; set; }
            public string PlayerJerseyName { get; set; }
            public string PlayerFamousName { get; set; }
            public int PlayerTypeId { get; set; }

        // Filed in Team Member Table
            public int TeamMemberJerseyNo { get; set; }
            public string TeamMemberJerseyName { get; set; }
            public string TeamMemberFamousName { get; set; }
            public int TeamMemberTypeId { get; set; }

        //Filed in Clud Table
            public int ClubID { get; set; }
            public string OwnerDescription { get; set; }
            public int OwnerPercentage { get; set; }

        //Filed in Club Member Type 
            public int ClubMemberTypeId { get; set;}
            public string ClubMemberDesc { get; set;}
    }

    public class clsRegistrationController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsRegistrationController()
        {

        }

        #region Getdata Methods

        public DataTable GetUserAndRegistrationDetailByUserID()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetUserAndRegistrationDetailByUserID"))
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

        public DataTable GetUserRole()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetUserRole"))
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

        public DataTable GetPlayerTypeBySportID(int SportID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetPlayerTypeBySportID", SportID))
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

        public DataTable GetTeamMemberTypeBySportID(int SportID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamMemberTypeBySportID", SportID))
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

        public DataTable GetClubBySportID(int SportID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetClubBySportID", SportID))
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

        public DataTable GetClubMemberTypeBySportID(int SportID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetClubMemberTypeBySportID", SportID))
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

        

        public DataTable GetSport()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSport"))
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

        public DataTable GetCompetitionBySportID(int sportid)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCompetitionBySportID", sportid))
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

        public DataTable GetTeamBySportID(int sportid)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamBySportID", sportid))
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
                dataProvider.ExecuteNonQuery("usp_InsertUser", cc.UserTypeId, cc.UserName, cc.UserPassword, cc.UserPhotoName, cc.UserPhotoFile, cc.FirstName, cc.MiddleName, cc.LastName, cc.SuffixId, cc.EmailId, cc.TelephoneNumber, cc.ActiveFlagId, cc.ShowFlagId, cc.PortalID, cc.CreatedById, cc.ModifiedById,cc.UserRoleId);
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

        public int InsertPlayer(clsRegistration cc)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertPlayer", cc.TeamId, cc.RegistrationId, cc.PlayerJerseyNo, cc.PlayerJerseyName, cc.PlayerFamousName, cc.PortalID, cc.CreatedById, cc.ModifiedById, cc.PlayerTypeId);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int InsertTeamMember(clsRegistration cc)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertTeamMember", cc.TeamId, cc.RegistrationId, cc.TeamMemberTypeId, cc.TeamMemberJerseyNo, cc.TeamMemberJerseyName, cc.TeamMemberFamousName, cc.PortalID, cc.CreatedById, cc.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int InsertClubOwner(clsRegistration cc)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertClubOwner", cc.ClubID, cc.OwnerDescription, cc.OwnerPercentage, cc.PortalID, cc.CreatedById, cc.ModifiedById, cc.RegistrationId);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int InsertClubMember(clsRegistration cc)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertClubMember", cc.RegistrationId, cc.ClubMemberTypeId, cc.ClubID, cc.ClubMemberDesc, cc.PortalID, cc.CreatedById, cc.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        

        public int UpdateUser(clsRegistration cc)
        {
            int i = 0;
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateUser", cc.UserId,cc.UserTypeId,cc.UserPhotoName,cc.UserPhotoFile,cc.FirstName,cc.MiddleName,cc.LastName,cc.SuffixId,cc.EmailId,cc.TelephoneNumber,cc.ActiveFlagId,cc.ShowFlagId,cc.PortalID,cc.ModifiedById);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        public int UpdateRegistration(clsRegistration cc)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateRegistration", cc.UserId,cc.UserId_Admin,cc.Gender,cc.AddressLine1,cc.AddressLine2,cc.City,cc.State,cc.ZipPostalCode,cc.Country,cc.DateOfBirth,cc.PlaceOfBirth,cc.Height,cc.Weight,cc.ActiveFlagId,cc.ShowFlagId,cc.PortalID,cc.ModifiedById);
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

        public DataTable GetLatestPlayerID()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetLatestPlayerID"))
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

        public DataTable GetUserPhotoByUserID(clsRegistration cc)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetUserPhotoByUserID", cc.UserId))
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

        public DataTable GetRegistrationIDByUserID(int UserID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetRegistrationIDByUserID", UserID))
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

        public DataTable GetLatestRegistrationID()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetLatestRegistrationID"))
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
