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
    public class clsMerchandise
    {
        public int MerchandiseId;
        public int MerchandiseTypeId;
        public string MerchandiseTitle;
 
        public string MerchandiseDesc;
        public int MerchandisePrice;
        public int ActiveFlagId;
        public int ShowFlagId;
      
        public int PortalID;
        public string CreatedById;
        public string ModifiedById;
    }

    public class clsMerchandiseType
    {
    
        public int MerchandiseTypeId;
        public int SportId;
        public string MerchandiseType;

        public string MerchandiseDesc;
      
        public int PortalID;
        public string CreatedById;
        public string ModifiedById;
    }

    public class clsMerchandiseController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update Method

        public int InsertMerchandise(clsMerchandise m)
        {
            try
            {
                return Convert.ToInt32(dataProvider.ExecuteScalar("usp_InsertMerchandise", m.MerchandiseTypeId, m.MerchandiseTitle,m.MerchandiseDesc,m.MerchandisePrice,m.ActiveFlagId,m.ShowFlagId, m.PortalID, m.CreatedById));
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateMerchandise(clsMerchandise m)
        {
            try
            {
                return Convert.ToInt32(dataProvider.ExecuteScalar("usp_UpdateMerchandise", m.MerchandiseId, m.MerchandiseTypeId, m.MerchandiseTitle, m.MerchandiseDesc, m.MerchandisePrice, m.ActiveFlagId, m.ShowFlagId, m.PortalID, m.CreatedById));
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int DeleteMerchandise(int merchandiseId)
        {
            try
            {
                return Convert.ToInt32(dataProvider.ExecuteScalar("usp_DeleteMerchandise", merchandiseId));
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }


       

        #endregion Insert,Update Method

        #region Getdata Method


        public DataTable GetAllMerchandise()
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("[usp_GetAllMerchandise]"))
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

        public DataTable GetMerchandiseByMerchandiseId(int TeamID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("[usp_GetMerchandiseByMerchandise]", TeamID))
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


    public class clsMerchandiseTypeController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update Method

        public int InsertMerchandiseType(clsMerchandiseType mt)
        {
            try
            {
                return Convert.ToInt32(dataProvider.ExecuteScalar("usp_InsertMerchandiseType", mt.MerchandiseType, mt.MerchandiseDesc,mt.SportId, mt.PortalID, mt.CreatedById));
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateMerchandiseType(clsMerchandiseType mt)
        {
            try
            {
                return Convert.ToInt32(dataProvider.ExecuteScalar("usp_UpdateMerchandiseType", mt.MerchandiseTypeId, mt.MerchandiseType, mt.MerchandiseDesc, mt.SportId, mt.PortalID, mt.ModifiedById));
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int DeleteMerchandiseType(int merchandiseId)
        {
            try
            {
                return Convert.ToInt32(dataProvider.ExecuteScalar("usp_DeleteMerchandiseType", merchandiseId));
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }


        #endregion Insert,Update Method

        #region Getdata Method


        public DataTable GetAllMerchandiseType()
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("[usp_GetAllMerchandiseType]"))
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

        public DataTable GetMerchandiseTypeByMerchandiseTypeId(int TeamID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("[usp_GetMerchandiseTypeByMerchandiseTypeId]", TeamID))
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
