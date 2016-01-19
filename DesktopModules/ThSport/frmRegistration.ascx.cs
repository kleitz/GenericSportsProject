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
    public partial class frmRegistration : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        clsRegistration cc = new clsRegistration();
        clsRegistrationController ccc = new clsRegistrationController();

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
                FillUserType();
                FillCountry();
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
                dt = ccc.GetDataClub();
            }

            DataView dv = new DataView();
            dv = dt.AsDataView();
            dv.RowFilter = " FirstName like '%%" + txtRegistrationSearch.Text.Trim() + "%%'";

            if (dv.ToTable().Rows.Count > 0)
            {
                ViewState["dt"] = dv.ToTable();
                gvRegistration.DataSource = dv.ToTable();
                gvRegistration.DataBind();
            }
        }

        private void FillUserType()
        {
            DataTable dt = new DataTable();
            dt = ccc.GetUserType();
            if (dt.Rows.Count > 0)
            {
                ddlUserType.DataSource = dt;
                ddlUserType.DataTextField = "UserTypeName";
                ddlUserType.DataValueField = "UserTypeId";
                ddlUserType.DataBind();
                ddlUserType.Items.Insert(0, new ListItem("-- Select User Type --", "0"));
            }
        }

        private void FillCountry()
        {
            DataTable dt = new DataTable();
            dt = ccc.GetCountryList();
            if (dt.Rows.Count > 0)
            {
                ddlCountry.DataSource = dt;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryID";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-- Select Country --", "0"));
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            funClearData();
        }

        private void funClearData()
        {
            ddlSuffix.SelectedValue = "0";
            FillUserType();
            FillCountry();
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            ddlGender.SelectedValue = "0";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtZipPostalCode.Text = "";
            FillCountry();
            txtDateOfBirth.Text = "";
            txtPlaceOfBirth.Text = "";
            txtHeight.Text = "";
            txtWeight.Text = "";
            txtEmailAddress.Text = "";
            txtTelephone.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
            UserLogoImage.ImageUrl = "";
        }

        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnSaveRegistration_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            cc.SuffixId = Convert.ToInt32(ddlSuffix.SelectedValue);
            cc.UserTypeId = Convert.ToInt32(ddlUserType.SelectedValue);
            cc.FirstName = txtFirstName.Text.Trim();
            cc.MiddleName = txtMiddleName.Text.Trim();
            cc.LastName = txtLastName.Text.Trim();
            cc.UserName = txtFirstName.Text.Trim();
            cc.UserPassword = CreateRandomPassword(8);
            cc.Gender = ddlGender.SelectedValue;
            cc.AddressLine1 = txtAddress1.Text.Trim();
            cc.AddressLine2 = txtAddress2.Text.Trim();
            cc.City = txtCity.Text.Trim();
            cc.State = txtState.Text.Trim();
            cc.ZipPostalCode = txtZipPostalCode.Text.Trim();
            cc.Country = Convert.ToInt32(ddlCountry.SelectedValue);
            cc.DateOfBirth = txtDateOfBirth.Text.Trim();
            cc.PlaceOfBirth = txtPlaceOfBirth.Text.Trim();
            cc.Height = txtHeight.Text.Trim();
            cc.Weight = txtWeight.Text.Trim();
            cc.EmailId = txtEmailAddress.Text.Trim();
            cc.TelephoneNumber = txtTelephone.Text.Trim();
            cc.UserPhotoName = txtUserLogoName.Text.Trim();

            cc.UserPhotoFile = imhpathDB + UserLogoFile.PostedFile.FileName.Replace(" ", "");

            if (UserLogoFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(UserLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

            if (!string.IsNullOrEmpty(UserLogoFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                    return;
                }
            }

            if (FileOK)
            {
                if (UserLogoFile.PostedFile.ContentLength > 10485760)
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
                    UserLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + UserLogoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }

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

            ccc.InsertUser(cc);

            DataTable dt = ccc.GetLatestUserID();
            if (dt.Rows.Count > 0)
            {
                cc.UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                cc.UserId_Admin = cc.UserId;
                ccc.InsertRegistration(cc);
            }

            pnlEntryRegistration.Visible = false;
            PnlGridRegistration.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void btnAddRegistration_Click(object sender, EventArgs e)
        {
            funClearData();
            pnlEntryRegistration.Visible = true;
            PnlGridRegistration.Visible = false;
            btnSaveRegistration.Visible = true;
            btnUpdateRegistration.Visible = false;
            FillUserType();
            FillCountry();
        }

        protected void btnCloseRegistration_Click(object sender, EventArgs e)
        {
            pnlEntryRegistration.Visible = false;
            PnlGridRegistration.Visible = true;
            FillGridView();
        }

        protected void btnUpdateRegistration_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully()", true);

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

        protected void gvRegistration_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRegistration.PageIndex = e.NewPageIndex;
            FillGridView();
        }

        protected void lbGo_Click(object sender, EventArgs e)
        {
            FillGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionUserID")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                funClearData();
                int UserID = 0;
                int.TryParse(str, out UserID);

                LinkButton btn = sender as LinkButton;

                clsRegistration cc = new clsRegistration();
                clsRegistrationController ccc = new clsRegistrationController();

                DataTable dt = new DataTable();

                dt = ccc.GetUserDetailsByUserID(UserID);

                if (dt.Rows.Count > 0)
                {
                    hidRegID.Value = dt.Rows[0]["UserId"].ToString();
                    ddlUserType.SelectedValue =  dt.Rows[0]["UserTypeId"].ToString();
                    txtUserLogoName.Text = dt.Rows[0]["UserPhotoName"].ToString();
                    
                    UserLogoImage.ImageUrl = dt.Rows[0]["UserPhotoFile"].ToString();

                    string ufname = dt.Rows[0]["UserPhotoFile"].ToString().Replace(" ", "");
                    UserLogoFile.ResolveUrl("ufname");

                    txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();
                    txtMiddleName.Text = dt.Rows[0]["MiddleName"].ToString();
                    txtLastName.Text = dt.Rows[0]["LastName"].ToString();
                    ddlSuffix.SelectedValue = dt.Rows[0]["SuffixId"].ToString();
                    txtEmailAddress.Text = dt.Rows[0]["EmailId"].ToString();
                    txtTelephone.Text = dt.Rows[0]["TelephoneNumber"].ToString();

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

			us.,us.,us.,us.,us.ActiveFlagId,us.ShowFlagId,
			re.RegistrationId,re.Gender,re.AddressLine1,re.AddressLine2,re.City,re.State,re.ZipPostalCode,re.Country,
			re.DateOfBirth,re.PlaceOfBirth,re.Height,re.Weight


                    txtClubName.Text = dt.Rows[0]["ClubName"].ToString();
                    txtClubAddress.Text = dt.Rows[0]["ClubAbbr"].ToString();
                    txtClubDescription.Text = dt.Rows[0]["ClubDesc"].ToString();
                    txtClubFamousName.Text = dt.Rows[0]["ClubFamousName"].ToString();
                    txtClubLogoName.Text = dt.Rows[0]["ClubLogoName"].ToString();


                    ClubPhotoImage.ImageUrl = dt.Rows[0]["ClubPhotoFile"].ToString();

                    string ufnamephoto = dt.Rows[0]["ClubPhotoFile"].ToString().Replace(" ", "");
                    ClubPhotoFile.ResolveUrl("ufnamephoto");

                    txtClubEstablishedYear.Text = dt.Rows[0]["ClubEstablishedYear"].ToString();

                   

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