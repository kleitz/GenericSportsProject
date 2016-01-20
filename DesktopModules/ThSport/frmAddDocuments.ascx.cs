using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Framework;
using DotNetNuke.Entities.Modules;
using ThSportServer;
using DotNetNuke.Entities.Users;
using System.Data;
using System.IO;

namespace DotNetNuke.Modules.ThSport
{
    public partial class frmAddDocuments : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        int RegistrationId
        {
            get
            {
                int retVal = 0;
                if ((Request.QueryString["RegistrationId"] != null))
                {
                    int.TryParse(Request.QueryString["RegistrationId"].ToString(), out retVal);
                    return retVal;
                }
                return 0;
            }
        }

        clsAddDocuments cc = new clsAddDocuments();
        clsAddDocumentsController ccc = new clsAddDocumentsController();

        string m_controlToLoad;
        string VName;
        int SeasonID = 0;
        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\AllImage\\";
        public string imhpathDB = "Images\\AllImage\\";

        Boolean FileOK = false;
        Boolean FileSaved = false;
        Boolean FileOKForUpdate = false;
        Boolean FileSavedForUpdate = false;

        #region Page events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDocumentType();
                FillGridView();
            }
        }

        #endregion

        #region Grid Editing Related Events

        protected void BindGrid()
        {
            FillGridView();
        }

        #endregion

        private void FillGridView()
        {
            DataTable dt = new DataTable();

            if (currentUser.IsSuperUser || currentUser.IsInRole("Club Admin"))
            {
                dt = ccc.GetDocumentList();
            }

            if (dt.Rows.Count > 0)
            {
                gvDocument.DataSource = dt;
                gvDocument.DataBind();
            }
        }

        private void FillDocumentType()
        {
            DataTable dt = new DataTable();
            dt = ccc.FillDropDownDocumentType();
            if (dt.Rows.Count > 0)
            {
                ddlDocumentType.DataSource = dt;
                ddlDocumentType.DataTextField = "RegistrationDocName";
                ddlDocumentType.DataValueField = "RegistrationDocTypeId";
                ddlDocumentType.DataBind();
                ddlDocumentType.Items.Insert(0, new ListItem("-- Select Document Type --", "0"));
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            funClearData();
        }

        private void funClearData()
        {
            txtRegistrationDocNumber.Text = "";
            txtRegistrationDocCountryOfIssue.Text = "";
            txtRegistrationDocDateOfIssue.Text = "";
            txtRegistrationDocDateOfExpiry.Text = "";
        }

        protected void btnSaveDocument_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            if ((DocumentFileUpload.PostedFile != null) && (DocumentFileUpload.PostedFile.ContentLength > 0))
            {
                string extension = DocumentFileUpload.PostedFile.FileName.Substring(DocumentFileUpload.PostedFile.FileName.LastIndexOf('.'));
                string friendlyName = DocumentFileUpload.PostedFile.FileName.Replace(extension, "");
                string fn = RegistrationId + "_" + friendlyName + "_" + DateTime.Now.ToShortDateString().Replace("/", "_") + extension;
                string SaveLocationAll = Server.MapPath("DesktopModules/ThSport/DocRegFile") + "\\" + fn;
                DocumentFileUpload.SaveAs(Server.MapPath("DesktopModules/ThSport/DocRegFile") + "\\" + fn);
                string SaveLocation = Path.GetFileName(("~/DesktopModules/ThSport/DocRegFile") + "\\" + fn);

                bool isExists = System.IO.Directory.Exists(Server.MapPath("~/DesktopModules/ThSport/DocRegFile"));
                if (!isExists)
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/DesktopModules/ThSport/DocRegFile"));

                try
                {
                    cc.RegistrationId = RegistrationId;
                    cc.RegistrationDocTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cc.RegistrationDocNumber = txtRegistrationDocNumber.Text.Trim();
                    cc.RegistrationDocCountryOfIssue = txtRegistrationDocCountryOfIssue.Text.Trim();
                    cc.RegistrationDocDateOfIssue = txtRegistrationDocDateOfIssue.Text.Trim();
                    cc.RegistrationDocDateOfExpiry = txtRegistrationDocDateOfExpiry.Text.Trim();
                    cc.RegistrationDocFile = SaveLocation;
                    cc.PortaID = PortalId;
                    cc.CreatedById = currentUser.Username;
                    cc.ModifiedById = currentUser.Username;

                    ccc.InsertDocument(cc);

                    pnlEntryDocument.Visible = false;
                    PnlGridDocument.Visible = true;
                    FillGridView();
                    funClearData();
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void btnAddDocument_Click(object sender, EventArgs e)
        {
            funClearData();
            pnlEntryDocument.Visible = true;
            PnlGridDocument.Visible = false;
            btnSaveDocument.Visible = true;
            btnUpdateDocument.Visible = false;
            FillDocumentType();
        }

        protected void btnCloseDocument_Click(object sender, EventArgs e)
        {
            pnlEntryDocument.Visible = false;
            PnlGridDocument.Visible = true;
            FillGridView();
        }

        protected void btnUpdateDocument_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully()", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            if ((DocumentFileUpload.PostedFile != null) && (DocumentFileUpload.PostedFile.ContentLength > 0))
            {
                string extension = DocumentFileUpload.PostedFile.FileName.Substring(DocumentFileUpload.PostedFile.FileName.LastIndexOf('.'));
                string friendlyName = DocumentFileUpload.PostedFile.FileName.Replace(extension, "");
                string fn = RegistrationId + "_" + friendlyName + "_" + DateTime.Now.ToShortDateString().Replace("/", "_") + extension;
                string SaveLocationAll = Server.MapPath("DesktopModules/ThSport/DocRegFile") + "\\" + fn;
                DocumentFileUpload.SaveAs(Server.MapPath("DesktopModules/ThSport/DocRegFile") + "\\" + fn);
                string SaveLocation = Path.GetFileName(("~/DesktopModules/ThSport/DocRegFile") + "\\" + fn);

                bool isExists = System.IO.Directory.Exists(Server.MapPath("~/DesktopModules/ThSport/DocRegFile"));
                if (!isExists)
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/DesktopModules/ThSport/DocRegFile"));

                try
                {
                    cc.RegistrationDocId = Convert.ToInt32(hidRegID.Value);
                    cc.RegistrationId = RegistrationId;
                    cc.RegistrationDocTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
                    cc.RegistrationDocNumber = txtRegistrationDocNumber.Text.Trim();
                    cc.RegistrationDocCountryOfIssue = txtRegistrationDocCountryOfIssue.Text.Trim();
                    cc.RegistrationDocDateOfIssue = txtRegistrationDocDateOfIssue.Text.Trim();
                    cc.RegistrationDocDateOfExpiry = txtRegistrationDocDateOfExpiry.Text.Trim();
                    cc.RegistrationDocFile = SaveLocation;
                    cc.PortaID = PortalId;
                    cc.ModifiedById = currentUser.Username;

                    ccc.UpdateDocument(cc);

                    pnlEntryDocument.Visible = false;
                    PnlGridDocument.Visible = true;
                    FillGridView();
                    funClearData();
                }
                catch (Exception ex)
                {

                }
            }
           
            pnlEntryDocument.Visible = false;
            PnlGridDocument.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void gvDocument_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDocument.PageIndex = e.NewPageIndex;
            FillGridView();
        }

        protected void lbGo_Click(object sender, EventArgs e)
        {
            FillGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionRegistrationDocId")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                funClearData();
                int DocumentID = 0;
                int.TryParse(str, out DocumentID);

                LinkButton btn = sender as LinkButton;

                clsAddDocuments cc = new clsAddDocuments();
                clsAddDocumentsController ccc = new clsAddDocumentsController();

                DataTable dt = new DataTable();

                dt = ccc.GetRegDocDetailByRegDocumentID(DocumentID);

                if (dt.Rows.Count > 0)
                {
                    hidRegID.Value = dt.Rows[0]["RegistrationDocId"].ToString();
                    ddlDocumentType.SelectedValue = dt.Rows[0]["RegistrationDocTypeId"].ToString();
                    txtRegistrationDocNumber.Text = dt.Rows[0]["RegistrationDocNumber"].ToString();
                    txtRegistrationDocCountryOfIssue.Text = dt.Rows[0]["RegistrationDocCountryOfIssue"].ToString();
                    txtRegistrationDocDateOfIssue.Text = dt.Rows[0]["RegistrationDocDateOfIssue"].ToString();
                    txtRegistrationDocDateOfExpiry.Text = dt.Rows[0]["RegistrationDocDateOfExpiry"].ToString();

                    pnlEntryDocument.Visible = true;
                    PnlGridDocument.Visible = false;
                    btnUpdateDocument.Visible = true;
                    btnSaveDocument.Visible = false;
                }
            }
            else if (ddlSelectedValue == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "javascript:confirm('Are You Sure? Want To Delete.');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", "javascript:Confirmation();", true);
                int DocRegID = 0;
                int.TryParse(str, out DocRegID);
                ccc.DeleteDocReg(DocRegID);
                FillGridView();
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmAddDocuments", "RegistrationId=" + RegistrationId));
            }
        }

        protected void gvDocument_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("download"))
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

                string downloadpath = Server.MapPath("DesktopModules/ThSport/DocRegFile") + "\\" + e.CommandArgument.ToString();
                System.IO.FileInfo file = new System.IO.FileInfo(downloadpath);
                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
            }
        }

    }
}