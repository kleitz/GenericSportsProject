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
    public class clsAddDocuments
    {
            public int RegistrationDocId { get; set; }
	        public int RegistrationId { get; set; }
            public int RegistrationDocTypeId { get; set; }
            public string RegistrationDocNumber { get; set; }
            public string RegistrationDocCountryOfIssue { get; set; }
            public string RegistrationDocDateOfIssue { get; set; }
            public string RegistrationDocDateOfExpiry { get ; set; }
            public string RegistrationDocFile { get; set; }
            public int PortaID { get; set; }
	        public string CreatedById { get; set; }
            public string ModifiedById { get; set; }
    }

     public class clsAddDocumentsController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsAddDocumentsController()
        {

        }

        #region Insert,Update Method

        public int InsertDocument(clsAddDocuments ccm)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertDocument", ccm.RegistrationId, ccm.RegistrationDocTypeId, ccm.RegistrationDocNumber, ccm.RegistrationDocCountryOfIssue, Convert.ToDateTime(ccm.RegistrationDocDateOfIssue), Convert.ToDateTime(ccm.RegistrationDocDateOfExpiry), ccm.RegistrationDocFile, ccm.PortaID, ccm.CreatedById, ccm.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateDocument(clsAddDocuments ccm)
        {
            int i = 0;

            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateDocument", ccm.RegistrationDocId, ccm.RegistrationId, ccm.RegistrationDocTypeId, ccm.RegistrationDocNumber, ccm.RegistrationDocCountryOfIssue, Convert.ToDateTime(ccm.RegistrationDocDateOfIssue), Convert.ToDateTime(ccm.RegistrationDocDateOfExpiry), ccm.RegistrationDocFile, ccm.PortaID, ccm.ModifiedById);
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

        public DataTable GetDocumentList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDocumentList"))
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

        public DataTable FillDropDownDocumentType()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_FillDropDownDocumentType"))
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

        public DataTable GetRegDocDetailByRegDocumentID(int RegDocumentID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetRegDocDetailByRegDocumentID", RegDocumentID))
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


        public DataTable DeleteDocReg(int RegDocumentID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_DeleteDocReg", RegDocumentID))
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
