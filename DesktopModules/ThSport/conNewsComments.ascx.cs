using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using System.IO;
using ThSportServer;
using System.Data;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Common;

namespace DotNetNuke.Modules.ThSport
{
    public partial class conNewsComments : PortalModuleBase
    {
        
        #region "Variables"

        int NewsID
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["NewsID"] != null))
                {
                    int.TryParse(Request.QueryString["NewsID"].ToString(), out retVal);
                    return retVal;
                }
                return 0;
            }
        }

        public int NewsId;

        #endregion "Varaibles"

        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        DotNetNuke.Entities.Tabs.TabController tabs1 = new Entities.Tabs.TabController();
        DotNetNuke.Entities.Tabs.TabInfo tInfo1 = new Entities.Tabs.TabInfo();

        #region "Page Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ltrlSummary.Visible = false;
                plchdrLoggedIn.Visible = true;
                if (currentUser.UserID == -1)
                {
                    plchdrLoggedIn.Visible = false;
                }
                else
                {
                    chkUseCurrentUser.Text = "Comment as " + currentUser.DisplayName;
                }

                NewsId = NewsID; //static
                DataTable dt = new DataTable();
                //dt = new clsNewsCommentsController().GetNewsCommentByNewsId(NewsId);
                pnlViewComments.Visible = dt.Rows.Count == 0 ? false : true;
                btnHideComment.Visible = false;
            }
        }

        #endregion "Page Events"

        #region "Control Events"

        protected void btnSubmitComment_onserverclick(object sender, EventArgs e)
        {
            if (!(Validate()))
            {
                ltrlSummary.Visible = true;
                return;
            }

            try
            {
                //static initialization for testing; delete later
                NewsId = NewsID;
                //clsNewsComments comment = new clsNewsComments();
                //comment.NewsId = NewsId;
                //comment.Email = txtCommentEmail.Value.Trim();

                //if (currentUser.UserID != -1 && chkUseCurrentUser.Checked == true)
                //{
                //    comment.Name = currentUser.DisplayName;
                //    comment.UserId = currentUser.UserID;
                //}
                //else
                //{
                //    if (txtCommentName.Value.Trim() != String.Empty)
                //    {
                //        comment.Name = txtCommentName.Value.Trim();
                //        comment.UserId = 0;
                //    }
                //}

                //comment.Comment = txtComment.Text.Trim();
                //comment.IsApproved = 1; //static
                //comment.ApprovedBy = "SYSTEM"; //static
                //comment.IsDeleted = 0;
                //comment.CreatedOn = DateTime.Now;

                //new clsNewsCommentsController().InsertNewsComment(comment);
                //LoadComments();

                ltrlSummary.Text = "Comment submitted successfully";

            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
                ltrlSummary.Text = "Comment could not be submitted";
            }

            ltrlSummary.Visible = true;
            ResetInputs();
        }

        protected void btnViewComment_Click(object sender, EventArgs e)
        {
            LoadComments();
        }

        protected void btnHideComment_Click(object sender, EventArgs e)
        {
            pnlViewComments.Visible = false;
            btnViewComment.Visible = true;
            btnHideComment.Visible = false;
        }

        #endregion "Control Events"

        #region "Methods"

        private bool Validate()
        {
            ltrlSummary.Text = "Your comment could not be submitted, missing ";

            if (!(currentUser.UserID != -1 && chkUseCurrentUser.Checked == true))
            {
                if (txtCommentName.Value.Trim() == String.Empty)
                {
                    ltrlSummary.Text += " name";
                    return false;
                }
            }

            if (txtComment.Text.Trim() == String.Empty)
            {
                ltrlSummary.Text += " comment";
                return false;
            }

            return true;
        }

        private void ResetInputs()
        {
            txtComment.Text = String.Empty;
            txtCommentName.Value = String.Empty;
            txtCommentEmail.Value = String.Empty;
            chkUseCurrentUser.Checked = false;
        }

        private void LoadComments()
        {
            NewsId = NewsID; //static
            DataTable dt = new DataTable();
            //dt = new clsNewsCommentsController().GetNewsCommentByNewsId(NewsId);
            rptrComments.DataSource = dt;
            rptrComments.DataBind();

            pnlViewComments.Visible = true;
            btnViewComment.Visible = false;
            btnHideComment.Visible = false;
            if (dt.Rows.Count > 0)
            {
                btnHideComment.Visible = true;
            }
        }

        #endregion "Methods"
    }
}