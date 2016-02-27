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
    [Serializable]
    public class clsClub
    {
        public int ClubId { get; set; }
        public int SportID { get; set; }
        public string ClubName { get; set; }
        public string ClubAbbr { get; set; }
        public string ClubDesc { get; set; }
        public string ClubFamousName { get; set; }
        public string ClubLogoName { get; set; }
        public string ClubLogoFile { get; set; }
        public string ClubPhotoFile { get; set; }
        public string ClubEstablishedYear { get; set; }
        public int ActiveFlagId { get; set; }
        public int ShowFlagId { get; set; }
        public int PortalID { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
    }

    public class clsClubController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsClubController()
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

        public DataTable FillSportDropdown()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_FillSportDropdown"))
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

        public int InsertClub(clsClub cc)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertClub", cc.ClubName,cc.ClubAbbr,cc.ClubDesc,cc.ClubFamousName,cc.ClubLogoName,cc.ClubLogoFile,cc.ClubPhotoFile,Convert.ToDateTime(cc.ClubEstablishedYear),cc.ActiveFlagId,cc.ShowFlagId,cc.PortalID,cc.CreatedById,cc.ModifiedById);
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
                dataProvider.ExecuteNonQuery("usp_UpdateClub", 	cc.ClubId,cc.ClubName,cc.ClubAbbr,cc.ClubDesc,cc.ClubFamousName,cc.ClubLogoName,cc.ClubLogoFile,cc.ClubPhotoFile,Convert.ToDateTime(cc.ClubEstablishedYear),cc.ActiveFlagId,cc.ShowFlagId,cc.PortalID,cc.ModifiedById);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

       
        #endregion Insert,Update,Delete Methods

        public DataTable GetClubDataByClubID(int ClubID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetClubDataByClubID", ClubID))
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

        public DataTable GetClubLatestValue()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetMaxValue"))
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
