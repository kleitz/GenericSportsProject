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
    public class clsEvent
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventDetail { get; set; }
        public string EventStartDateTime { get; set; }
        public string EventEndDateTime { get ; set; }
        public int EventActive { get; set; }
        public string EventPriority { get; set; }
        public int PortalID { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
    }

    public class clsEventController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsEventController()
        {

        }

        #region Getdata Methods

        public DataTable GetDataEvent()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDataEvent"))
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

        public int InsertEvent(clsEvent cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertEvent", cs.EventName, cs.EventDetail, Convert.ToDateTime(cs.EventStartDateTime), Convert.ToDateTime(cs.EventEndDateTime), cs.EventActive, cs.EventPriority, cs.PortalID, cs.CreatedById, cs.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateEvent(clsEvent cs)
        {
            int i = 0;
            try
            {
                //CompReg.CreatedBy,
                dataProvider.ExecuteNonQuery("usp_UpdateEvent", cs.EventID,cs.EventName,cs.EventDetail,Convert.ToDateTime(cs.EventStartDateTime),Convert.ToDateTime(cs.EventEndDateTime),cs.EventActive,cs.EventPriority,cs.PortalID,cs.ModifiedById);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        #endregion Insert,Update,Delete Methods

        public DataTable GetEventDataByEventID(int EventID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetEventDataByEventID", EventID))
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
