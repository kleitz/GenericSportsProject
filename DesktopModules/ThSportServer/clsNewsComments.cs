using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetNuke.Entities.Users;
using DotNetNuke.Data;
using DotNetNuke.Services.Exceptions;
using System.Data;

namespace ThSportServer
{
    public class clsNewsComments
	{
        //public int CommmentId { get; set; }
        //public int NewsId { get; set; }
        //public int UserId { get; set; }
        //public string Name { get; set; }
        //public string Email { get; set; }
        //public string Comment { get; set; }
        //public int IsApproved { get; set; }
        //public string ApprovedBy { get; set; }
        //public int IsDeleted { get; set; }
        //public DateTime CreatedOn { get; set; }
	}

    public class clsNewsCommentsController
    {

        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsNewsCommentsController()
        {

        }

        //#region Getdata Methods

        //public DataTable GetNewsCommentByNewsId(int newsId)
        //{

        //    using (DataTable dt = new DataTable())
        //    {
        //        try
        //        {
        //            using (IDataReader reader = dataProvider.ExecuteReader("usp_GetNewsCommentByNewsId", newsId))
        //            {
        //                dt.Load(reader);
        //                return dt;
        //            }
        //        }

        //        catch (Exception ex)
        //        {
        //            Exceptions.LogException(ex);
        //        }

        //        return dt;
        //    }
        //}

        //#endregion Getdata Methods

        //#region Insert/Update/Delete Methods

        //public int InsertNewsComment(clsNewsComments comment)
        //{
        //    try
        //    {
        //        dataProvider.ExecuteNonQuery("usp_InsertNewsComment", comment.NewsId, comment.UserId, comment.Name, comment.Email, comment.Comment, comment.IsApproved,comment.ApprovedBy, comment.IsDeleted, comment.CreatedOn);
        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.LogException(ex);
        //    }
        //    return 0;
        //}

        //#endregion Insert/Update/Delete Methods
    }
}
