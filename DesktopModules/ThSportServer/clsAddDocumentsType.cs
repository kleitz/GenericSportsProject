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
    public class clsAddDocumentsType
    {
        public int RegistrationDocTypeId { get; set; }
        public string RegistrationDocName { get; set; }
        public string RegistrationDocDesc { get; set; }
        public int PortalID { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
    }

    public class clsAddDocumentsTypeController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsAddDocumentsTypeController()
        {

        }

        #region Insert,Update Method

        public int InsertDocumentType(clsAddDocumentsType ccm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertDocumentType", ccm.RegistrationDocName,ccm.RegistrationDocDesc,ccm.PortalID,ccm.CreatedById,ccm.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateDocumentType(clsAddDocumentsType ccm)
        {
            int i = 0;

            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateDocumentType", ccm.RegistrationDocTypeId,ccm.RegistrationDocName,ccm.RegistrationDocDesc,ccm.PortalID,ccm.ModifiedById);
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

        public DataTable GetDocumentTypeList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDocumentTypeList"))
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

        public DataTable GetDocumentTypeByDocumentTypeID(int DocumentTypeID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetDocumentTypeByDocumentTypeID", DocumentTypeID))
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

