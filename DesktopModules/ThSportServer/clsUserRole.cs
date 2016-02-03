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
     public class clsUserRole
    {
         public int  UserRoleId { get; set; }
         public string UserRoleName { get; set; }
         public string UserRoleAbbr { get; set; }
         public string UserRoleDesc { get; set; }
         public int PortalId { get; set; }
         public string CreatedById { get; set; }
         public string ModifiedById { get; set; }
    }

     public class clsUserRoleController
     {
         private readonly DataProvider dataProvider = DataProvider.Instance();
         private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

         public clsUserRoleController()
         {

         }

         #region Insert,Update Method

         public int InsertUserRole(clsUserRole ccm)
         {
             try
             {
                 dataProvider.ExecuteNonQuery("usp_InsertUserRole", ccm.UserRoleName, ccm.UserRoleAbbr, ccm.UserRoleDesc, ccm.PortalId, ccm.CreatedById, ccm.ModifiedById);
             }
             catch (Exception ex)
             {
                 Exceptions.LogException(ex);
             }
             return 0;
         }

         public int UpdateUserRole(clsUserRole ccm)
         {
             int i = 0;

             try
             {
                 dataProvider.ExecuteNonQuery("usp_UpdateUserRole", ccm.UserRoleId,ccm.UserRoleName,ccm.UserRoleAbbr,ccm.UserRoleDesc,ccm.PortalId,ccm.ModifiedById);
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

         public DataTable GetUserRoleList()
         {
             using (DataTable dt = new DataTable())
             {
                 try
                 {
                     using (IDataReader reader = dataProvider.ExecuteReader("usp_GetUserRoleList"))
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

         public DataTable GetUserRoleDetailByUserRoleID(int UserRoleID)
         {
             DataTable dt = new DataTable();

             try
             {
                 using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetUserRoleDetailByUserRoleID", UserRoleID))
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
