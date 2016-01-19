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
    public class clsSport
    {
        public int SportID { get; set; }
        public string SportName { get; set; }
        public string SportDesc { get; set; }
        public string SportMainImageFile { get; set; }
        public string SportMainImageDesc { get; set; }
        public string SportLogoImageFile { get; set;}
        public string SportLogoImageDesc { get; set; }
        public string SportSmallImageFile { get; set; }
        public string SportSmallImageDesc { get; set; }
        public int ActiveFlagID { get; set; }
        public int ShowFlagID { get; set; }
        public int PortalID { get; set; }
        public string CreatedByID { get; set; }
        public string ModifiedByID { get; set; }
    }

    public class clsSportController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsSportController()
        {

        }

        #region Getdata Methods

        public DataTable GetDataSport()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDataSport"))
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

        public int InsertSport(clsSport cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertSport", cs.SportName,cs.SportDesc,cs.SportMainImageFile,cs.SportMainImageDesc,cs.SportLogoImageFile,cs.SportLogoImageDesc,cs.SportSmallImageFile,cs.SportSmallImageDesc,cs.ActiveFlagID,cs.ShowFlagID,cs.PortalID,cs.CreatedByID,cs.ModifiedByID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateSport(clsSport cs)
        {
            int i = 0;
            try
            {
                //CompReg.CreatedBy,
                dataProvider.ExecuteNonQuery("usp_UpdateSport", cs.SportID,cs.SportName,cs.SportDesc,cs.SportMainImageFile,cs.SportMainImageDesc,cs.SportLogoImageFile,cs.SportLogoImageDesc,cs.SportSmallImageFile,cs.SportSmallImageDesc,cs.ActiveFlagID,cs.ShowFlagID,cs.PortalID,cs.ModifiedByID);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        #endregion Insert,Update,Delete Methods

        public DataTable GetSportDetailBySportID(int SportID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSportDetailBySportID", SportID))
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

        public DataTable GetSportMainImageBySportID(clsSport cs)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSportMainImageBySportID", cs.SportID))
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

        public DataTable GetSportLogoImageBySportID(clsSport cs)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSportLogoImageBySportID", cs.SportID))
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

        public DataTable GetSportSmallImageBySportID(clsSport cs)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSportSmallImageBySportID", cs.SportID))
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
