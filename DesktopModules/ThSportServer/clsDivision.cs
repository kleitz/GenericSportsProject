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
    public class clsDivision
    {
        public int DivisionId;
        public int SeasonId;
        public string DivisionName;
        public string DivisionAbbr;
        public string DivisionDesc;
        public string DivisionLogoName;
        public string DivisionLogoFile;
        public int DivisionLevel;
        public int TotalNumofTeams;
        public int PromotedNum;
        public int DemotedNum;
        public int ActiveFlagId;
        public int ShowFlagId;
        public int PortalID;
        public string CreatedById;
        public string ModifiedById;
    }

    public class clsDivisionController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region Insert,Update Method

        public int InsertDivision(clsDivision dv)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertDivision", dv.SeasonId, dv.DivisionName , dv.DivisionAbbr, dv.DivisionDesc, dv.DivisionLogoName , dv.DivisionLogoFile , dv.DivisionLevel,dv.TotalNumofTeams,dv.PromotedNum,dv.DemotedNum,dv.PortalID, dv.CreatedById, dv.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateDivision(clsDivision dv)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateDivision", dv.DivisionId, dv.SeasonId, dv.DivisionName, dv.DivisionAbbr, dv.DivisionDesc, dv.DivisionLogoName, dv.DivisionLogoFile, dv.DivisionLevel, dv.TotalNumofTeams, dv.PromotedNum, dv.DemotedNum, dv.PortalID, dv.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int DeleteDivision(int cid)
        {
            try
            {

                dataProvider.ExecuteNonQuery("usp_DeleteDivision", cid);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        #endregion Insert,Update Method

        #region Getdata Method

        public DataTable GetDivisionList()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDivisionList"))
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

        public DataTable GetDivisionByDivisionID(int DivisionID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetDivisionByDivisionID", DivisionID))
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
