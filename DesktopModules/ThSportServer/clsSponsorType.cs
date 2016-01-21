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
    public class clsSponsorType
    {
         public int SponsorTypeId { get; set; }
         public string SponsorTypeValue { get; set; }
	     public string SponsorTypeDesc { get; set; }
	     public int PortalID { get; set; }
	     public string CreatedById { get; set; }
	     public string ModifiedById { get; set; }
    }

    public class clsSponsorTypeController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsSponsorTypeController()
        {

        }

        #region Insert,Update Method

        public int InsertSponsorType(clsSponsorType ccm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertSponsorType", ccm.SponsorTypeValue, ccm.SponsorTypeDesc, ccm.PortalID, ccm.CreatedById, ccm.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateSponsorType(clsSponsorType ccm)
        {
            int i = 0;

            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateSponsorType", ccm.SponsorTypeId, ccm.SponsorTypeValue, ccm.SponsorTypeDesc, ccm.PortalID, ccm.ModifiedById);
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

        public DataTable GetSponsorTypeList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSponsorTypeList"))
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

        public DataTable GetSponsorTypeDetailBySponsorTypeID(int SponsorTypeID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetSponsorTypeDetailBySponsorTypeID", SponsorTypeID))
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
