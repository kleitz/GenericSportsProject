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
    public class clsPlayerType
    {
           public int PlayerTypeID { get; set; } 
	       public string PlayerTypeName { get; set; } 
	       public string PlayerTypeDesc { get; set; } 
           public int PortalID { get; set; } 
           public string CreatedById { get; set; }
           public string ModifiedById { get; set; } 
    }

    public class clsPlayerTypeController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsPlayerTypeController()
        {

        }

        #region Insert,Update Method

        public int InsertPlayerType(clsPlayerType ccm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertPlayerType", ccm.PlayerTypeName, ccm.PlayerTypeDesc, ccm.PortalID, ccm.CreatedById, ccm.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdatePlayerType(clsPlayerType ccm)
        {
            int i = 0;

            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdatePlayerType", ccm.PlayerTypeID,ccm.PlayerTypeName,ccm.PlayerTypeDesc,ccm.PortalID,ccm.ModifiedById);
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

        public DataTable GetPlayerTypeList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetPlayerTypeList"))
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

        public DataTable GetPlayerTypeDetailByPlayerTypeID(int PlayerTypeID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetPlayerTypeDetailByPlayerTypeID", PlayerTypeID))
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
