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
    public class clsCompetitionFormat
    {
        public int CompetitionFormatId;
        public string CompetitionFormatName;
        public string CompetitionFormatDesc;
        public int PortalID;
    }

    public class clsCompetitionFormatController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update Method

        public int InsertCompetitionFormat(clsCompetitionFormat cf)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertCompetitionFormat", cf.CompetitionFormatName,cf.CompetitionFormatDesc,cf.PortalID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateCompetitionFormat(clsCompetitionFormat cf)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateCompetitionFormat", cf.CompetitionFormatId,cf.CompetitionFormatName,cf.CompetitionFormatDesc,cf.PortalID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        //public int DeleteCompetition(int cid)
        //{
        //    try
        //    {
        //        dataProvider.ExecuteNonQuery("usp_DeleteCompetition", cid);
        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.LogException(ex);
        //    }
        //    return 0;
        //}

        #endregion Insert,Update Method

        #region Getdata Method

        public DataTable GetCompetitionFormatList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCompetitionFormatList"))
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

        public DataTable GetCompetitionFormatDetailByCompetitionFormatID(int CompetitionFormatID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetCompetitionFormatDetailByCompetitionFormatID", CompetitionFormatID))
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
