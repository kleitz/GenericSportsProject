using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThSportServer;
using System.Data;
using System.IO;
using System.Globalization;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;

namespace DotNetNuke.Modules.ThSport
{
    public partial class frmSports : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        clsSport cs = new clsSport();
        clsSportController csc = new clsSportController();

        string m_controlToLoad;
        string VName;
        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\SportsImages\\";
        public string imhpathDB = "Images\\SportsImages\\";
       
        Boolean FileOK = false;
        Boolean FileSaved = false;
        Boolean FileOKForUpdate = false;
        Boolean FileSavedForUpdate = false;
        
        #region Page events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

            //if (currentUser.IsSuperUser || currentUser.IsInRole("clubadmin"))
            //{
                dt = csc.GetDataSport();
            //}

            DataView dv = new DataView();
            dv = dt.AsDataView();
            dv.RowFilter = " SportName like '%%" + txtSportNameSearch.Text.Trim() + "%%'";

            if (dv.ToTable().Rows.Count > 0)
            {
                ViewState["dt"] = dv.ToTable();
                gvSport.DataSource = dv.ToTable();
                gvSport.DataBind();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            funClearData();
        }

        private void funClearData()
        {
            txtSportName.Text = "";
            txtSportDescription.Text = "";
            SportMainImage.ImageUrl = "";
            txtMainImageDesc.Text = "";
            SportLogoImage.ImageUrl = "";
            txtLogoImageDesc.Text = "";
            SportSmallImage.ImageUrl = "";
            txtSmallImageDesc.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
        }

        protected void btnSaveSport_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SportSaveSuccessfully();", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            cs.SportName = txtSportName.Text.Trim();
            cs.SportDesc = txtSportDescription.Text.Trim();

            // main image file 
            cs.SportMainImageFile = imhpathDB + SportMainFile.PostedFile.FileName.Replace(" ", "");

            if (SportMainFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(SportMainFile.PostedFile.FileName.Replace(" ", "")).ToLower();
                String[] allowedExtensions = { ".png", ".jpg", ".gif", ".jpeg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (FileExtension == allowedExtensions[i])
                    {
                        FileOK = true;
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(SportMainFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    return;
                }
            }

            if (FileOK)
            {
                if (SportMainFile.PostedFile.ContentLength > 10485760)
                {
                    
                }
                else
                {
                  
                }

                try
                {
                    SportMainFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + SportMainFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }

            cs.SportMainImageDesc = txtMainImageDesc.Text.Trim();

            // logo image file 
            cs.SportLogoImageFile = imhpathDB + SportLogoFile.PostedFile.FileName.Replace(" ", "");

            if (SportLogoFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(SportLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
                String[] allowedExtensions = { ".png", ".jpg", ".gif", ".jpeg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (FileExtension == allowedExtensions[i])
                    {
                        FileOK = true;
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(SportLogoFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    return;
                }
            }

            if (FileOK)
            {
                if (SportLogoFile.PostedFile.ContentLength > 10485760)
                {

                }
                else
                {

                }

                try
                {
                    SportLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + SportLogoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }

            cs.SportLogoImageDesc = txtLogoImageDesc.Text.Trim();

            // small image file 
            cs.SportSmallImageFile = imhpathDB + SportSmallFile.PostedFile.FileName.Replace(" ", "");

            if (SportSmallFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(SportSmallFile.PostedFile.FileName.Replace(" ", "")).ToLower();
                String[] allowedExtensions = { ".png", ".jpg", ".gif", ".jpeg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (FileExtension == allowedExtensions[i])
                    {
                        FileOK = true;
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(SportSmallFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    return;
                }
            }

            if (FileOK)
            {
                if (SportSmallFile.PostedFile.ContentLength > 10485760)
                {

                }
                else
                {

                }

                try
                {
                    SportSmallFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + SportSmallFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }

            cs.SportSmallImageDesc = txtSmallImageDesc.Text.Trim();

            if (ChkIsActive.Checked == true)
            {
                cs.ActiveFlagID = 1;
            }
            else
            {
                cs.ActiveFlagID = 0;
            }

            if (ChkIsShow.Checked == true)
            {
                cs.ShowFlagID = 1;
            }
            else
            {
                cs.ShowFlagID = 0;
            }

            cs.PortalID = PortalId;
            cs.CreatedByID = currentUser.Username;
            cs.ModifiedByID = currentUser.Username;
            
            int SportID = csc.InsertSport(cs);

            pnlSportEntry.Visible = false;
            PnlGridSport.Visible = true;
            funClearData();
            FillGridView();
        }

        protected void btnAddSport_Click(object sender, EventArgs e)
        {
            pnlSportEntry.Visible = true;
            PnlGridSport.Visible = false;
            btnSaveSport.Visible = true;
            btnUpdateSport.Visible = false;
            funClearData();
        }

        protected void btnCloseSport_Click(object sender, EventArgs e)
        {
            pnlSportEntry.Visible = false;
            PnlGridSport.Visible = true;
            funClearData();
            FillGridView();
        }

        protected void btnUpdateSport_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SportUpdateSuccessfully()", true);
                        
            Boolean FileOK = false;
            Boolean FileSaved = false;

            cs.SportID = Convert.ToInt32(hidRegID.Value);
            cs.SportName = txtSportName.Text.Trim();
            cs.SportDesc = txtSportDescription.Text.Trim();

            if (SportMainFile.PostedFile.FileName == "")
            {
                DataTable dt1 = new DataTable();
                cs.SportID = Convert.ToInt32(hidRegID.Value);
                dt1 = csc.GetSportMainImageBySportID(cs);
                SportMainImage.ImageUrl = dt1.Rows[0]["SportMainImageFile"].ToString();
                string ufname = dt1.Rows[0]["SportMainImageFile"].ToString().Replace(" ", "");
                SportMainFile.ResolveUrl("ufname");
                cs.SportMainImageFile = ufname.Replace(" ", "");
                FileOKForUpdate = true;
            }
            else
            {
                // main image file 
                cs.SportMainImageFile = imhpathDB + SportMainFile.PostedFile.FileName.Replace(" ", "");

                if (SportMainFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(SportMainFile.PostedFile.FileName.Replace(" ", "")).ToLower();
                    String[] allowedExtensions = { ".png", ".jpg", ".gif", ".jpeg" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(SportMainFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        return;
                    }
                }

                if (FileOK)
                {
                    if (SportMainFile.PostedFile.ContentLength > 10485760)
                    {

                    }
                    else
                    {

                    }

                    try
                    {
                        SportMainFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + SportMainFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
            }

            cs.SportMainImageDesc = txtMainImageDesc.Text.Trim();

            if (SportLogoFile.PostedFile.FileName == "")
            {
                DataTable dt1 = new DataTable();
                cs.SportID = Convert.ToInt32(hidRegID.Value);
                dt1 = csc.GetSportLogoImageBySportID(cs);
                SportLogoImage.ImageUrl = dt1.Rows[0]["SportLogoImageFile"].ToString();
                string ufname = dt1.Rows[0]["SportLogoImageFile"].ToString().Replace(" ", "");
                SportLogoFile.ResolveUrl("ufname");
                cs.SportLogoImageFile = ufname.Replace(" ", "");
                FileOKForUpdate = true;
            }
            else
            {
                // logo image file 
                cs.SportLogoImageFile = imhpathDB + SportLogoFile.PostedFile.FileName.Replace(" ", "");

                if (SportLogoFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(SportLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
                    String[] allowedExtensions = { ".png", ".jpg", ".gif", ".jpeg" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(SportLogoFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        return;
                    }
                }

                if (FileOK)
                {
                    if (SportLogoFile.PostedFile.ContentLength > 10485760)
                    {

                    }
                    else
                    {

                    }

                    try
                    {
                        SportLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + SportLogoFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
            }

            cs.SportLogoImageDesc = txtLogoImageDesc.Text.Trim();

            if (SportSmallFile.PostedFile.FileName == "")
            {
                DataTable dt1 = new DataTable();
                cs.SportID = Convert.ToInt32(hidRegID.Value);
                dt1 = csc.GetSportSmallImageBySportID(cs);
                SportSmallImage.ImageUrl = dt1.Rows[0]["SportSmallImageFile"].ToString();
                string ufname = dt1.Rows[0]["SportSmallImageFile"].ToString().Replace(" ", "");
                SportSmallFile.ResolveUrl("ufname");
                cs.SportSmallImageFile = ufname.Replace(" ", "");
                FileOKForUpdate = true;
            }
            else
            {

                // small image file 
                cs.SportSmallImageFile = imhpathDB + SportSmallFile.PostedFile.FileName.Replace(" ", "");

                if (SportSmallFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(SportSmallFile.PostedFile.FileName.Replace(" ", "")).ToLower();
                    String[] allowedExtensions = { ".png", ".jpg", ".gif", ".jpeg" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(SportSmallFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        return;
                    }
                }

                if (FileOK)
                {
                    if (SportSmallFile.PostedFile.ContentLength > 10485760)
                    {

                    }
                    else
                    {

                    }

                    try
                    {
                        SportSmallFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + SportSmallFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
            }

            cs.SportSmallImageDesc = txtSmallImageDesc.Text.Trim();

            if (ChkIsActive.Checked == true)
            {
                cs.ActiveFlagID = 1;
            }
            else
            {
                cs.ActiveFlagID = 0;
            }

            if (ChkIsShow.Checked == true)
            {
                cs.ShowFlagID = 1;
            }
            else
            {
                cs.ShowFlagID = 0;
            }

            cs.PortalID = PortalId;
            cs.ModifiedByID = currentUser.Username;

            int SportID = csc.UpdateSport(cs);

            pnlSportEntry.Visible = false;
            funClearData();
            FillGridView();
            PnlGridSport.Visible = true;
        }

        protected void gvSport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSport.PageIndex = e.NewPageIndex;
            FillGridView();
        }

        protected void lbGo_Click(object sender, EventArgs e)
        {
            FillGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionSportID")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                funClearData();
                int sportID = 0;
                int.TryParse(str, out sportID);

                LinkButton btn = sender as LinkButton;

                clsSport cs = new clsSport();
                clsSportController csc = new clsSportController();

                DataTable dt = new DataTable();

                dt = csc.GetSportDetailBySportID(sportID);

                if (dt.Rows.Count > 0)
                {
                    hidRegID.Value = dt.Rows[0]["SportID"].ToString();
                    txtSportName.Text = dt.Rows[0]["SportName"].ToString();
                    txtSportDescription.Text = dt.Rows[0]["SportDesc"].ToString();

                    // main image 
                    SportMainImage.ImageUrl = dt.Rows[0]["SportMainImageFile"].ToString();

                    string ufname = dt.Rows[0]["SportMainImageFile"].ToString();
                    SportMainFile.ResolveUrl("ufname");

                    txtMainImageDesc.Text = dt.Rows[0]["SportMainImageDesc"].ToString();

                    // loge iamge 
                    SportLogoImage.ImageUrl = dt.Rows[0]["SportLogoImageFile"].ToString();

                    string ufnamelogo = dt.Rows[0]["SportLogoImageFile"].ToString().Replace(" ", "");
                    SportLogoFile.ResolveUrl("ufnamelogo");

                    txtLogoImageDesc.Text = dt.Rows[0]["SportLogoImageDesc"].ToString();

                    // small image 
                    SportSmallImage.ImageUrl = dt.Rows[0]["SportSmallImageFile"].ToString();

                    string ufnamesmall = dt.Rows[0]["SportSmallImageFile"].ToString().Replace(" ", "");
                    SportSmallFile.ResolveUrl("ufnamesmall");

                    txtSmallImageDesc.Text = dt.Rows[0]["SportSmallImageDesc"].ToString();

                    if (dt.Rows[0]["ActiveFlagId"].ToString() == "1")
                    {
                        ChkIsActive.Checked = true;
                    }
                    else
                    {
                        ChkIsActive.Checked = false;
                    }

                    if (dt.Rows[0]["ShowFlagId"].ToString() == "1")
                    {
                        ChkIsShow.Checked = true;
                    }
                    else
                    {
                        ChkIsShow.Checked = false;
                    }

                    int revel = 0;
                    int.TryParse(hidRegID.Value, out revel);

                    pnlSportEntry.Visible = true;
                    PnlGridSport.Visible = false;
                    btnUpdateSport.Visible = true;
                    btnSaveSport.Visible = false;
                }
            }
            else if (ddlSelectedValue == "Delete")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "javascript:confirm('Are You Sure? Want To Delete.');", true);

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", "javascript:Confirmation();", true);

                //int competition_Id = 0;

                //int.TryParse(str, out competition_Id);

                //CompRegInfo.DeleteCompetitionReg(competition_Id);

                //FillGridView();
            }
        }
    }
}