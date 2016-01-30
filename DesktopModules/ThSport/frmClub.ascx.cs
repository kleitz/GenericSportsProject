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
    public partial class frmClub : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        clsClub cc = new clsClub();
        clsClubController ccc = new clsClubController();

        string m_controlToLoad;
        string VName;
        int SeasonID = 0;
        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\ClubImages\\";
        public string imhpathDB = "Images\\ClubImages\\";

        Boolean FileOK = false;
        Boolean FileSaved = false;
        Boolean FileOKForUpdate = false;
        Boolean FileSavedForUpdate = false;

        #region Page events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillSport();
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

            if (currentUser.IsSuperUser || currentUser.IsInRole("clubadmin"))
            {
                dt = ccc.GetDataClub();
            }

            DataView dv = new DataView();
            dv = dt.AsDataView();
            dv.RowFilter = " ClubName like '%%" + txtClubNameSearch.Text.Trim() + "%%'";

            if (dv.ToTable().Rows.Count > 0)
            {
                ViewState["dt"] = dv.ToTable();
                gvClub.DataSource = dv.ToTable();
                gvClub.DataBind();
            }
        }

        private void FillSport()
        {
            DataTable dt = new DataTable();
            dt = ccc.FillSportDropdown();
            if (dt.Rows.Count > 0)
            {
                ddlSport.DataSource = dt;
                ddlSport.DataTextField = "SportName";
                ddlSport.DataValueField = "SportID";
                ddlSport.DataBind();
                ddlSport.Items.Insert(0, new ListItem("-- Select Sport --", "0"));
                ddlSport.SelectedValue = "1";
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            funClearData();
        }

        private void funClearData()
        {
            ddlSport.SelectedValue = "1";
            txtClubName.Text = "";
            txtClubAddress.Text = "";
            txtClubDescription.Text = "";
            txtClubFamousName.Text = "";
            txtClubLogoName.Text = "";
            ClubLogoImage.ImageUrl = "";
            ClubPhotoImage.ImageUrl = "";
            txtClubEstablishedYear.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
        }

        protected void btnSaveClub_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "ClubSaveSuccessfully();", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            cc.SportID = Convert.ToInt32(ddlSport.SelectedValue);
            cc.ClubName = txtClubName.Text.Trim();
            cc.ClubAbbr = txtClubAddress.Text.Trim();
            cc.ClubDesc = txtClubDescription.Text.Trim();
            cc.ClubFamousName = txtClubFamousName.Text.Trim();
            cc.ClubLogoName = txtClubLogoName.Text.Trim();

            cc.ClubLogoFile = imhpathDB + ClubLogoFile.PostedFile.FileName.Replace(" ", "");

            if (ClubLogoFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(ClubLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

            if (!string.IsNullOrEmpty(ClubLogoFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                    return;
                }
            }

            if (FileOK)
            {
                if (ClubLogoFile.PostedFile.ContentLength > 10485760)
                {
                    //dvMsg.Attributes.Add("style", "display:block;");
                    //return;
                }
                else
                {
                    //dvMsg.Attributes.Add("style", "display:none;");
                }

                try
                {
                    ClubLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + ClubLogoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }

            cc.ClubPhotoFile = imhpathDB + ClubPhotoFile.PostedFile.FileName.Replace(" ", "");

            if (ClubPhotoFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(ClubPhotoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

            if (!string.IsNullOrEmpty(ClubPhotoFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                    return;
                }
            }

            if (FileOK)
            {
                if (ClubPhotoFile.PostedFile.ContentLength > 10485760)
                {
                    //dvMsg.Attributes.Add("style", "display:block;");
                    //return;
                }
                else
                {
                    //dvMsg.Attributes.Add("style", "display:none;");
                }

                try
                {
                    ClubPhotoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + ClubPhotoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }

            cc.ClubEstablishedYear = txtClubEstablishedYear.Text.Trim();

            if (ChkIsActive.Checked == true)
            {
                cc.ActiveFlagId = 1;
            }
            else
            {
                cc.ActiveFlagId = 0;
            }

            if (ChkIsShow.Checked == true)
            {
                cc.ShowFlagId = 1;
            }
            else
            {
                cc.ShowFlagId = 0;
            }

            cc.PortalID = PortalId;
            cc.CreatedById = currentUser.Username;
            cc.ModifiedById = currentUser.Username;

            int clubid = ccc.InsertClub(cc);

            DataTable dt = ccc.GetClubLatestValue();
            if (dt.Rows.Count > 0)
            {
                int ClubID = Convert.ToInt32(dt.Rows[0]["ClubId"].ToString());
                int SportID = Convert.ToInt32(ddlSport.SelectedValue);

                ccc.InsertClubSports(ClubID,SportID);
            }
           
            mainContentClub.Visible = false;
            PnlGridClub.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void btnAddClub_Click(object sender, EventArgs e)
        {
            funClearData();
            mainContentClub.Visible = true;
            PnlGridClub.Visible = false;
            btnSaveClub.Visible = true;
            btnUpdateClub.Visible = false;
            FillSport();
        }

        protected void btnCloseClub_Click(object sender, EventArgs e)
        {
            mainContentClub.Visible = false;
            PnlGridClub.Visible = true;
            FillGridView();
        }

        protected void btnUpdateClub_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "ClubUpdateSuccessfully()", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            cc.ClubId = Convert.ToInt32(hidRegID.Value);
            cc.SportID = Convert.ToInt32(ddlSport.SelectedValue);
            cc.ClubName = txtClubName.Text.Trim();
            cc.ClubAbbr = txtClubAddress.Text.Trim();
            cc.ClubDesc = txtClubDescription.Text.Trim();
            cc.ClubFamousName = txtClubFamousName.Text.Trim();
            cc.ClubLogoName = txtClubLogoName.Text.Trim();

            if (ClubLogoFile.PostedFile.FileName == "")
            {
                DataTable dt1 = new DataTable();
                cc.ClubId = Convert.ToInt32(hidRegID.Value);
                dt1 = ccc.GetClubLogo(cc);
                ClubLogoImage.ImageUrl = dt1.Rows[0]["ClubLogoFile"].ToString();
                string ufname = dt1.Rows[0]["ClubLogoFile"].ToString().Replace(" ", "");
                ClubLogoFile.ResolveUrl("ufname");
                cc.ClubLogoFile = ufname.Replace(" ", "");
                FileOKForUpdate = true;
            }
            else
            {

                cc.ClubLogoFile = imhpathDB + ClubLogoFile.PostedFile.FileName.Replace(" ", "");

                if (ClubLogoFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(ClubLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

                if (!string.IsNullOrEmpty(ClubLogoFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                        return;
                    }
                }

                if (FileOK)
                {
                    if (ClubLogoFile.PostedFile.ContentLength > 10485760)
                    {
                        //dvMsg.Attributes.Add("style", "display:block;");
                        //return;
                    }
                    else
                    {
                        //dvMsg.Attributes.Add("style", "display:none;");
                    }

                    try
                    {
                        ClubLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + ClubLogoFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
            }

            if (ClubPhotoFile.PostedFile.FileName == "")
            {
                DataTable dt1 = new DataTable();
                cc.ClubId = Convert.ToInt32(hidRegID.Value);
                dt1 = ccc.GetClubPhoto(cc);
                ClubPhotoImage.ImageUrl = dt1.Rows[0]["ClubPhotoFile"].ToString();
                string ufname = dt1.Rows[0]["ClubPhotoFile"].ToString().Replace(" ", "");
                ClubPhotoFile.ResolveUrl("ufname");
                cc.ClubPhotoFile = ufname.Replace(" ", "");
                FileOKForUpdate = true;
            }
            else
            {
                cc.ClubPhotoFile = imhpathDB + ClubPhotoFile.PostedFile.FileName.Replace(" ", "");

                if (ClubPhotoFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(ClubPhotoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

                if (!string.IsNullOrEmpty(ClubPhotoFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                        return;
                    }
                }

                if (FileOK)
                {
                    if (ClubPhotoFile.PostedFile.ContentLength > 10485760)
                    {
                        //dvMsg.Attributes.Add("style", "display:block;");
                        //return;
                    }
                    else
                    {
                        //dvMsg.Attributes.Add("style", "display:none;");
                    }

                    try
                    {
                        ClubPhotoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + ClubPhotoFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
            }
            cc.ClubEstablishedYear = txtClubEstablishedYear.Text.Trim();

            if (ChkIsActive.Checked == true)
            {
                cc.ActiveFlagId = 1;
            }
            else
            {
                cc.ActiveFlagId = 0;
            }

            if (ChkIsShow.Checked == true)
            {
                cc.ShowFlagId = 1;
            }
            else
            {
                cc.ShowFlagId = 0;
            }

            cc.PortalID = PortalId;
            cc.CreatedById = currentUser.Username;
            cc.ModifiedById = currentUser.Username;

            int clubid = ccc.UpdateClub(cc);

            int ucsid = ccc.UpdateClubSports(cc.ClubId, cc.SportID);

            mainContentClub.Visible = false;
            PnlGridClub.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void gvClub_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClub.PageIndex = e.NewPageIndex;
            FillGridView();
        }

        protected void lbGo_Click(object sender, EventArgs e)
        {
            FillGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionClubID")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                funClearData();
                int ClubID = 0;
                int.TryParse(str, out ClubID);

                LinkButton btn = sender as LinkButton;

                clsClub cc = new clsClub();
                clsClubController ccc = new clsClubController();

                DataTable dt = new DataTable();

                dt = ccc.GetClubDataByClubID(ClubID);

                if (dt.Rows.Count > 0)
                {
                    hidRegID.Value = dt.Rows[0]["ClubID"].ToString();
                    txtClubName.Text = dt.Rows[0]["ClubName"].ToString();
                    txtClubAddress.Text = dt.Rows[0]["ClubAbbr"].ToString();
                    txtClubDescription.Text = dt.Rows[0]["ClubDesc"].ToString();
                    txtClubFamousName.Text = dt.Rows[0]["ClubFamousName"].ToString();
                    txtClubLogoName.Text = dt.Rows[0]["ClubLogoName"].ToString();

                    ClubLogoImage.ImageUrl = dt.Rows[0]["ClubLogoFile"].ToString();

                    string ufname = dt.Rows[0]["ClubLogoFile"].ToString().Replace(" ", "");
                    ClubLogoFile.ResolveUrl("ufname");

                    ClubPhotoImage.ImageUrl = dt.Rows[0]["ClubPhotoFile"].ToString();

                    string ufnamephoto = dt.Rows[0]["ClubPhotoFile"].ToString().Replace(" ", "");
                    ClubPhotoFile.ResolveUrl("ufnamephoto");

                    txtClubEstablishedYear.Text = dt.Rows[0]["ClubEstablishedYear"].ToString();

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

                    DataTable dt2 = ccc.GetSportIDByClubID(ClubID);
                    if (dt2.Rows.Count > 0)
                    {
                        ddlSport.SelectedValue = dt2.Rows[0]["SportID"].ToString();
                    }
                    else
                    {
                        ddlSport.SelectedValue = "1";
                    }

                    mainContentClub.Visible = true;
                    PnlGridClub.Visible = false;
                    btnUpdateClub.Visible = true;
                    btnSaveClub.Visible = false;
                }
            }
            else if (ddlSelectedValue == "Owner")
            {
                PnlGridClub.Visible = false;
                mainContentClub.Visible = false;
                hidRegID.Value = str;
                int ClubID = Convert.ToInt32(hidRegID.Value);
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubOwner", "ClubID=" + ClubID));
            }
            else if (ddlSelectedValue == "Member")
            {
                PnlGridClub.Visible = false;
                mainContentClub.Visible = false;
                hidRegID.Value = str;
                int ClubID = Convert.ToInt32(hidRegID.Value);
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmClubMember", "ClubID=" + ClubID));
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