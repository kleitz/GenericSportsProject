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

        public string entry_mode_masterplayer
        {
            get
            {
                if ((Request.QueryString["entry_mode_masterplayer"] != null))
                {
                    return Request.QueryString["entry_mode_masterplayer"].ToString();
                }
                return "";
            }
        }

        clsRegistration cc = new clsRegistration();
        clsRegistrationController ccc = new clsRegistrationController();
        
        string m_controlToLoad;
        string VName;
        int SeasonID = 0;
        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\RegistrationImages\\";
        public string imhpathDB = "Images\\RegistrationImages\\";

        Boolean FileOK = false;
        Boolean FileSaved = false;
        Boolean FileOKForUpdate = false;
        Boolean FileSavedForUpdate = false;

        #region Page events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (entry_mode_masterplayer == "editmasterplayer")
            {
                pnlEntryRegistration.Visible = true;
                PnlGridRegistration.Visible = false;
            }

            if (!IsPostBack)
            {
                FillUserRole();
                FillUserType();
                FillSport();
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

            //if (currentUser.IsSuperUser || currentUser.IsInRole("Club Admin"))
            //{
                dt = ccc.GetUserAndRegistrationDetailByUserID();
            //}

            DataView dv = new DataView();
            dv = dt.AsDataView();
            dv.RowFilter = " UserName like '%%" + txtRegistrationSearch.Text.Trim() + "%%'";

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
                ddlUserType.Items.Insert(0, new ListItem("-- Select --", "0"));
                //ddlUserType.SelectedValue = "4";
            }
        }

        private void FillUserRole()
        {
            DataTable dt = new DataTable();
            dt = ccc.GetUserRole();
            if (dt.Rows.Count > 0)
            {
                ddlUserRole.DataSource = dt;
                ddlUserRole.DataTextField = "UserRoleName";
                ddlUserRole.DataValueField = "UserRoleId";
                ddlUserRole.DataBind();
                ddlUserRole.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillPlayerType(int SportID)
        {
            DataTable dt = new DataTable();
            dt = ccc.GetPlayerTypeBySportID(SportID);
            if (dt.Rows.Count > 0)
            {
                ddlPlayerType.DataSource = dt;
                ddlPlayerType.DataTextField = "PlayerTypeName";
                ddlPlayerType.DataValueField = "PlayerTypeId";
                ddlPlayerType.DataBind();
                ddlPlayerType.Items.Insert(0, new ListItem("-- Select --", "0"));
            }  
        }

        private void FillTeamMemberType(int SportID)
        {
            DataTable dt = new DataTable();
            dt = ccc.GetTeamMemberTypeBySportID(SportID);
            if (dt.Rows.Count > 0)
            {
                ddlTeamMemberType.DataSource = dt;
                ddlTeamMemberType.DataTextField = "TeamMemberTypeValue";
                ddlTeamMemberType.DataValueField = "TeamMemberTypeId";
                ddlTeamMemberType.DataBind();
                ddlTeamMemberType.Items.Insert(0, new ListItem("-- Select --", "0"));
            }  
        }

        private void FillClub(int SportID)
        {
            DataTable dt = new DataTable();
            dt = ccc.GetClubBySportID(SportID);
            if (dt.Rows.Count > 0)
            {
                ddlClub.DataSource = dt;
                ddlClub.DataTextField = "ClubName";
                ddlClub.DataValueField = "ClubId";
                ddlClub.DataBind();
                ddlClub.Items.Insert(0, new ListItem("-- Select --", "0"));
            }  
        }

        private void FillClubMemberType(int SportID)
        {
            DataTable dt = new DataTable();
            dt = ccc.GetClubMemberTypeBySportID(SportID);
            if (dt.Rows.Count > 0)
            {
                ddlMemberType.DataSource = dt;
                ddlMemberType.DataTextField = "ClubMemberTypeValue";
                ddlMemberType.DataValueField = "ClubMemberTypeId";
                ddlMemberType.DataBind();
                ddlMemberType.Items.Insert(0, new ListItem("-- Select --", "0"));
            }  
        }

        private void FillSport()
        {
            DataTable dt = new DataTable();
            dt = ccc.GetSport();
            if (dt.Rows.Count > 0)
            {
                ddlSport.DataSource = dt;
                ddlSport.DataTextField = "SportName";
                ddlSport.DataValueField = "SportID";
                ddlSport.DataBind();
                ddlSport.Items.Insert(0, new ListItem("-- Select --", "0"));
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
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            ddlGender.SelectedValue = "0";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtZipPostalCode.Text = "";
           
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

        protected void btnSaveRegistration_Click(object sender, EventArgs e)
        {
            Boolean FileOK = false;
            Boolean FileSaved = false;

            cc.SuffixId = Convert.ToInt32(ddlSuffix.SelectedValue);
            cc.UserRoleId = Convert.ToInt32(ddlUserRole.SelectedValue);
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

            if (ddlUserRole.SelectedItem.ToString() == "Player")
            {
                if (drpSelectionEntry.SelectedValue == "0")
                {
                    cc.UserTypeId = 4;
                    ccc.InsertUser(cc);

                    DataTable dt = ccc.GetLatestUserID();
                    if (dt.Rows.Count > 0)
                    {
                        cc.UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                        cc.UserId_Admin = cc.UserId;
                        ccc.InsertRegistration(cc);
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);
                }
                else
                {
                    cc.UserTypeId = 4;
                    // Entry in User Table 
                    ccc.InsertUser(cc);

                    // Entry in Registration Table
                    DataTable dt = ccc.GetLatestUserID();
                    if (dt.Rows.Count > 0)
                    {
                        cc.UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                        cc.UserId_Admin = cc.UserId;
                        ccc.InsertRegistration(cc);
                    }

                    // Entry in Player Table
                    DataTable dt1 = ccc.GetLatestRegistrationID();
                    if (dt1.Rows.Count > 0)
                    {
                        cc.TeamId = Convert.ToInt32(ddlTeam.SelectedValue);
                        cc.RegistrationId = Convert.ToInt32(dt1.Rows[0]["RegistrationId"].ToString());
                        if (txtPlayerJerseyNo.Text == " ")
                        {
                            cc.PlayerJerseyNo = 0;
                        }
                        else
                        {
                            cc.PlayerJerseyNo = Convert.ToInt32(txtPlayerJerseyNo.Text.Trim());
                        }
                        cc.PlayerJerseyName = txtPlayerJerseyName.Text.Trim();
                        cc.PlayerFamousName = txtPlayerFamousName.Text.Trim();
                        cc.PlayerTypeId = Convert.ToInt32(ddlPlayerType.SelectedValue);
                        cc.PlayerPhoto = imhpathDB + UserLogoFile.PostedFile.FileName.Replace(" ", "");
                       // ccc.InsertPlayer(cc);
                    int teamPlayerID =ccc.InsertTeamPlayer(cc);

                    clsTeamPlayerHistory objTeamPlayerHistory = new clsTeamPlayerHistory();
                    clsTeamPlayerHistoryController objTeamPlayerHistoryController = new clsTeamPlayerHistoryController();
                    objTeamPlayerHistory.TeamPlayerID = teamPlayerID;
                    objTeamPlayerHistory.TeamId = cc.TeamId;
                    objTeamPlayerHistory.RegistrationId = cc.RegistrationId;
                    objTeamPlayerHistory.PlayerJerseyNo = cc.PlayerJerseyNo;
                    objTeamPlayerHistory.PlayerJerseyName = cc.PlayerJerseyName;
                    objTeamPlayerHistory.PlayerFamousName = cc.PlayerFamousName;
                    objTeamPlayerHistory.PlayerTypeId = cc.PlayerTypeId;
                    objTeamPlayerHistory.PlayerPhoto = cc.PlayerPhoto;
                    objTeamPlayerHistory.CreatedById = currentUser.Username;
                    objTeamPlayerHistoryController.InsertTeamPlayerHistory(objTeamPlayerHistory);
                    }

                    clsMatchPlayerPerfomance mpp = new clsMatchPlayerPerfomance();
                                        clsMatchPlayerPerfomanceController mppc = new clsMatchPlayerPerfomanceController();
                       DataTable dt4 = ccc.GetLatestPlayerID();
                    if (dt4.Rows.Count > 0)
                    {
                        mpp.PlayerID = Convert.ToInt32(dt4.Rows[0]["PlayerId"].ToString());

                    }
                    //player assign in match

                      // if select competition
                    if (ddlCompetition.SelectedValue == "0")
                    {

                        DataTable dt2 = new DataTable();

                        dt1 = mppc.MatchWisePlayerPerformancePlayerEntry(cc.TeamId);
                        if (dt1.Rows.Count != 0)
                        {
                            for (int K = 0; K < dt1.Rows.Count; K++)
                            {
                                int matchid = 0;
                                int.TryParse(dt1.Rows[K]["MatchID"].ToString(), out matchid);

                                int competitionid = 0;
                                int.TryParse(dt1.Rows[K]["CompetitionID"].ToString(), out competitionid);

                                mpp.CompetitionID = competitionid;
                                mpp.MatchId = matchid;
                                mpp.PlayerID = mpp.PlayerID;
                                mpp.PortalID = PortalId;
                                mpp.CreatedById = currentUser.Username;
                                mpp.ModifiedById = currentUser.Username;
                                mpp.Goal = 0;
                                mpp.Assist = 0;
                                mpp.IsPlayed = 1;
                                mpp.Yellow = 0;
                                mpp.Red = 0;
                                mpp.TeamID = cc.TeamId;
                                mppc.InsertMatchPlayerPerfomance(mpp);
                            }
                        }

                    }
                    else
                    {
                        DataTable dt2 = new DataTable();
                        int selectCompetitionId = 0;
                        int.TryParse(ddlCompetition.SelectedValue, out selectCompetitionId);

                        dt1 = mppc.MatchWisePlayerPerformancePlayerEntryByCompetitionIdAndTeamID(cc.TeamId,selectCompetitionId);
                        if (dt1.Rows.Count != 0)
                        {
                            for (int K = 0; K < dt1.Rows.Count; K++)
                            {
                                int matchid = 0;
                                int.TryParse(dt1.Rows[K]["MatchID"].ToString(), out matchid);

                                int competitionid = 0;
                                int.TryParse(dt1.Rows[K]["CompetitionID"].ToString(), out competitionid);

                                mpp.CompetitionID = competitionid;
                                mpp.MatchId = matchid;
                                mpp.PlayerID = mpp.PlayerID;
                                mpp.PortalID = PortalId;
                                mpp.CreatedById = currentUser.Username;
                                mpp.ModifiedById = currentUser.Username;
                                mpp.Goal = 0;
                                mpp.Assist = 0;
                                mpp.IsPlayed = 1;
                                mpp.Yellow = 0;
                                mpp.Red = 0;
                                mpp.TeamID = cc.TeamId;
                                mppc.InsertMatchPlayerPerfomance(mpp);
                            }
                        }
 
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);
                }
            }
            else if (ddlUserRole.SelectedItem.ToString() == "Parents / Relatives")
            {
                    ccc.InsertUser(cc);

                    DataTable dt = ccc.GetLatestUserID();
                    if (dt.Rows.Count > 0)
                    {
                        cc.UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                        cc.UserId_Admin = cc.UserId;
                        ccc.InsertRegistration(cc);
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);
                
            }
            else if (ddlUserRole.SelectedItem.ToString() == "Team Member")
            {
                if (drpSelectionEntry.SelectedValue == "0")
                {
                    cc.UserTypeId = 0;
                    ccc.InsertUser(cc);

                    DataTable dt = ccc.GetLatestUserID();
                    if (dt.Rows.Count > 0)
                    {
                        cc.UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                        cc.UserId_Admin = cc.UserId;
                        ccc.InsertRegistration(cc);
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);
                }
                else
                {
                    // Entry In User Table
                    cc.UserTypeId = 0;
                    ccc.InsertUser(cc);

                    // Entry In Registration Table
                    DataTable dt = ccc.GetLatestUserID();
                    if (dt.Rows.Count > 0)
                    {
                        cc.UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                        cc.UserId_Admin = cc.UserId;
                        ccc.InsertRegistration(cc);
                    }

                    // Entry in Team Mamber Table
                    DataTable dt1 = ccc.GetLatestRegistrationID();
                    if (dt1.Rows.Count > 0)
                    {
                        cc.TeamId = Convert.ToInt32(ddlTeam.SelectedValue);
                        cc.RegistrationId = Convert.ToInt32(dt1.Rows[0]["RegistrationId"].ToString());
                        if (txtTeamMemberJerseyNo.Text == " ")
                        {
                            cc.TeamMemberJerseyNo = 0;
                        }
                        else
                        {
                            cc.TeamMemberJerseyNo = Convert.ToInt32(txtTeamMemberJerseyNo.Text.Trim());
                        }
                        cc.TeamMemberJerseyName = txtTeamMemberJerseyName.Text.Trim();
                        cc.TeamMemberFamousName = txtTeamMemberFamousName.Text.Trim();
                        cc.TeamMemberTypeId = Convert.ToInt32(ddlTeamMemberType.SelectedValue);

                        ccc.InsertTeamMember(cc);
                     }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);
                }

            }
            else if (ddlUserRole.SelectedItem.ToString() == "Club Owner")
            {
                if (ddlAssignToClub.SelectedValue == "0")
                {
                    cc.UserTypeId = 0;
                    ccc.InsertUser(cc);

                    DataTable dt = ccc.GetLatestUserID();
                    if (dt.Rows.Count > 0)
                    {
                        cc.UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                        cc.UserId_Admin = cc.UserId;
                        ccc.InsertRegistration(cc);
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);
                }
                else
                {
                    // Entry In User Table
                    cc.UserTypeId = 0;
                    ccc.InsertUser(cc);

                    // Entry In Registration Table
                    DataTable dt = ccc.GetLatestUserID();
                    if (dt.Rows.Count > 0)
                    {
                        cc.UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                        cc.UserId_Admin = cc.UserId;
                        ccc.InsertRegistration(cc);
                    }

                    // Entry in Club Owner Table
                    DataTable dt1 = ccc.GetLatestRegistrationID();
                    if (dt1.Rows.Count > 0)
                    {
                        cc.ClubID = Convert.ToInt32(ddlClub.SelectedValue);
                        cc.RegistrationId = Convert.ToInt32(dt1.Rows[0]["RegistrationId"].ToString());
                        cc.OwnerDescription = txtClubOwnerDescription.Text.Trim();
                        if (txtClubOwnerPercentage.Text == " ")
                        {
                            cc.OwnerPercentage = 0;
                        }
                        else
                        {
                            cc.OwnerPercentage = Convert.ToInt32(txtClubOwnerPercentage.Text.Trim());
                        }

                        ccc.InsertClubOwner(cc);
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);
                }
            }
            else if (ddlUserRole.SelectedItem.ToString() == "Club Member")
            {
                if (ddlAssignToClub.SelectedValue == "0")
                {
                    cc.UserTypeId = 0;
                    ccc.InsertUser(cc);

                    DataTable dt = ccc.GetLatestUserID();
                    if (dt.Rows.Count > 0)
                    {
                        cc.UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                        cc.UserId_Admin = cc.UserId;
                        ccc.InsertRegistration(cc);
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);
                }
                else
                {
                    // Entry In User Table
                    cc.UserTypeId = 0;
                    ccc.InsertUser(cc);

                    // Entry In Registration Table
                    DataTable dt = ccc.GetLatestUserID();
                    if (dt.Rows.Count > 0)
                    {
                        cc.UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                        cc.UserId_Admin = cc.UserId;
                        ccc.InsertRegistration(cc);
                    }

                    // Entry in Club Member Table
                    DataTable dt1 = ccc.GetLatestRegistrationID();
                    if (dt1.Rows.Count > 0)
                    {
                        cc.ClubID = Convert.ToInt32(ddlClub.SelectedValue);
                        cc.RegistrationId = Convert.ToInt32(dt1.Rows[0]["RegistrationId"].ToString());
                        cc.ClubMemberTypeId = Convert.ToInt32(ddlMemberType.SelectedValue);
                        cc.ClubMemberDesc = txtClubMemberDesc.Text;
                        
                        ccc.InsertClubMember(cc);
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);
                }
            }

            pnlEntryRegistration.Visible = false;
            PnlGridRegistration.Visible = true;
            FillGridView();
            funClearData();

            DataTable dt3 = ccc.GetLatestRegistrationID();
            if (dt3.Rows.Count > 0)
            {
               if (entry_mode_masterplayer == "editmasterplayer")
              {
                  int RegistrationId = Convert.ToInt32(dt3.Rows[0]["RegistrationId"].ToString());
                  Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmRegistrationParentOrRelatives" + "&entry_mode_masterplayer=editmasterplayer" + "&RegistrationId=" + RegistrationId));
              }
           }
            
        }

        protected void btnAddRegistration_Click(object sender, EventArgs e)
        {
            funClearData();
            pnlEntryRegistration.Visible = true;
            PnlGridRegistration.Visible = false;
            btnSaveRegistration.Visible = true;
            btnUpdateRegistration.Visible = false;
            divAssignToTeam.Visible = false;
            FillUserRole();
            FillUserType();
            FillCountry();
            FillSport();
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

            cc.UserId = Convert.ToInt32(hidRegID.Value);
            cc.SuffixId = Convert.ToInt32(ddlSuffix.SelectedValue);
            cc.UserRoleId = Convert.ToInt32(ddlUserRole.SelectedValue);
            cc.UserTypeId = Convert.ToInt32(ddlUserType.SelectedValue);
            cc.FirstName = txtFirstName.Text.Trim();
            cc.MiddleName = txtMiddleName.Text.Trim();
            cc.LastName = txtLastName.Text.Trim();
            //cc.UserName = txtFirstName.Text.Trim();
            //cc.UserPassword = CreateRandomPassword(8);
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

            if (UserLogoFile.PostedFile.FileName == "")
            {
                DataTable dt1 = new DataTable();
                cc.UserId = Convert.ToInt32(hidRegID.Value);
                dt1 = ccc.GetUserPhotoByUserID(cc);
                UserLogoImage.ImageUrl = dt1.Rows[0]["UserPhotoFile"].ToString();
                string ufname = dt1.Rows[0]["UserPhotoFile"].ToString().Replace(" ", "");
                UserLogoFile.ResolveUrl("ufname");
                cc.UserPhotoFile = ufname.Replace(" ", "");
                FileOKForUpdate = true;
            }
            else
            {
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
            cc.ModifiedById = currentUser.Username;

            int userid = ccc.UpdateUser(cc);

            int userregistrationid = ccc.UpdateRegistration(cc);

            pnlEntryRegistration.Visible = false;
            PnlGridRegistration.Visible = true;
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
                    ddlUserRole.SelectedValue = dt.Rows[0]["UserRoleId"].ToString();
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

                    ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                    txtAddress1.Text = dt.Rows[0]["AddressLine1"].ToString();
                    txtAddress2.Text = dt.Rows[0]["AddressLine2"].ToString();
                    txtCity.Text = dt.Rows[0]["City"].ToString();
                    txtState.Text = dt.Rows[0]["State"].ToString();
                    txtZipPostalCode.Text = dt.Rows[0]["ZipPostalCode"].ToString();
                    ddlCountry.SelectedValue = dt.Rows[0]["Country"].ToString();
                    txtDateOfBirth.Text = dt.Rows[0]["DateOfBirth"].ToString();
                    txtPlaceOfBirth.Text = dt.Rows[0]["PlaceOfBirth"].ToString();
                    txtHeight.Text = dt.Rows[0]["Height"].ToString();
                    txtWeight.Text = dt.Rows[0]["Weight"].ToString();
                        
                    pnlEntryRegistration.Visible = true;
                    PnlGridRegistration.Visible = false;
                    btnUpdateRegistration.Visible = true;
                    btnSaveRegistration.Visible = false;
                }

                divUserRole.Visible = false;
                ddlUserType.Enabled = false;
                divPlayerType.Visible = false;
                divSport.Visible = false;
                divCompetition.Visible = false;
                divTeam.Visible = false;
                divPlayerJerseyNo.Visible = false;
                divPlayerJerseyName.Visible = false;
                divPlayerFamousName.Visible = false;

                divAssignToTeam.Visible = false;
                divTeamMemberType.Visible = false;
                divteammemberjerseyno.Visible = false;
                divTeamMemberJerseyName.Visible = false;
                divTeamMemberFamousName.Visible = false;
            }
            else if (ddlSelectedValue == "AddDocuments")
            {
                PnlGridRegistration.Visible = false;
                pnlEntryRegistration.Visible = false;
                hidRegID.Value = str;
                int UserID = Convert.ToInt32(hidRegID.Value);
                DataTable dt1 = new DataTable();
                dt1 = ccc.GetRegistrationIDByUserID(UserID);
                if (dt1.Rows.Count > 0)
                {
                    int RegistrationId = Convert.ToInt32(dt1.Rows[0]["RegistrationId"].ToString());
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmAddDocuments", "RegistrationId=" + RegistrationId)); 
                }
            }
            else if (ddlSelectedValue == "AddParentORRelatives")
            {
                PnlGridRegistration.Visible = false;
                pnlEntryRegistration.Visible = false;
                hidRegID.Value = str;
                int UserID = Convert.ToInt32(hidRegID.Value);
                DataTable dt1 = new DataTable();
                dt1 = ccc.GetRegistrationIDByUserID(UserID);
                if (dt1.Rows.Count > 0)
                {
                    int RegistrationId = Convert.ToInt32(dt1.Rows[0]["RegistrationId"].ToString());
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmRegistrationParentOrRelatives", "RegistrationId=" + RegistrationId));
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

        protected void ddlUserRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserRole.SelectedItem.ToString() == "Player")
            {
                divAssignToTeam.Visible = true;
                drpSelectionEntry.SelectedValue = "0";
                divUserType.Visible = false;
                ddlUserType.SelectedValue = "4";
                ddlUserType.Enabled = false;
                divPlayerType.Visible = false;
                divSport.Visible = false;
                divCompetition.Visible = false;
                divTeam.Visible = false;
                divPlayerJerseyNo.Visible = false;
                divPlayerJerseyName.Visible = false;
                divPlayerFamousName.Visible = false;

                divAssignToTeam.Visible = true;
                divTeamMemberType.Visible = false;
                divSport.Visible = false;
                divCompetition.Visible = false;
                divTeam.Visible = false;
                divteammemberjerseyno.Visible = false;
                divTeamMemberJerseyName.Visible = false;
                divTeamMemberFamousName.Visible = false;

                divAssignToClub.Visible = false;
                divClub.Visible = false;
                divClubOwnerDescription.Visible = false;
                divClubOwnerPercentage.Visible = false;

                divClubMemberType.Visible = false;
                divClubMemberDesc.Visible = false;
            }
            else if (ddlUserRole.SelectedItem.ToString() == "Team Member")
            {
                divAssignToTeam.Visible = true;
                
                drpSelectionEntry.SelectedValue = "0";
                divTeamMemberType.Visible = false;
                divSport.Visible = false;
                divCompetition.Visible = false;
                divTeam.Visible = false;
                divteammemberjerseyno.Visible = false;
                divTeamMemberJerseyName.Visible = false;
                divTeamMemberFamousName.Visible = false;

                divUserType.Visible = false;
                ddlUserType.SelectedValue = "4";
                ddlUserType.Enabled = false;
                divPlayerType.Visible = false;
                divSport.Visible = false;
                divCompetition.Visible = false;
                divTeam.Visible = false;
                divPlayerJerseyNo.Visible = false;
                divPlayerJerseyName.Visible = false;
                divPlayerFamousName.Visible = false;

                divAssignToClub.Visible = false;
                divClub.Visible = false;
                divClubOwnerDescription.Visible = false;
                divClubOwnerPercentage.Visible = false;

                divClubMemberType.Visible = false;
                divClubMemberDesc.Visible = false;
            }
            else if (ddlUserRole.SelectedItem.ToString() == "Club Owner")
            {
                divAssignToClub.Visible = true;
                divAssignToTeam.Visible = false;
                ddlAssignToClub.SelectedValue = "0";

                divUserType.Visible = false;
                divPlayerType.Visible = false;
                divPlayerJerseyNo.Visible = false;
                divPlayerJerseyName.Visible = false;
                divPlayerFamousName.Visible = false;

                divTeamMemberType.Visible = false;
                divSport.Visible = false;
                divCompetition.Visible = false;
                divTeam.Visible = false;
                divteammemberjerseyno.Visible = false;
                divTeamMemberJerseyName.Visible = false;
                divTeamMemberFamousName.Visible = false;

                divClubMemberType.Visible = false;
                divClubMemberDesc.Visible = false;
            }
            else if (ddlUserRole.SelectedItem.ToString() == "Club Member")
            {
                divAssignToTeam.Visible = false;
                
                divUserType.Visible = false;
                divPlayerType.Visible = false;
                divPlayerJerseyNo.Visible = false;
                divPlayerJerseyName.Visible = false;
                divPlayerFamousName.Visible = false;

                divTeamMemberType.Visible = false;
                divSport.Visible = false;
                divCompetition.Visible = false;
                divTeam.Visible = false;
                divteammemberjerseyno.Visible = false;
                divTeamMemberJerseyName.Visible = false;
                divTeamMemberFamousName.Visible = false;

                divAssignToClub.Visible = false;
                divClub.Visible = false;
                divClubOwnerDescription.Visible = false;
                divClubOwnerPercentage.Visible = false;

                divAssignToClub.Visible = true;
                ddlAssignToClub.SelectedValue = "0";
            }
            else if (ddlUserRole.SelectedItem.ToString() == "Parents / Relatives")
            {
                divUserType.Visible = true;
                ddlUserType.Enabled = true;
                FillUserType();
                divAssignToTeam.Visible = false;
                divPlayerType.Visible = false;
                divSport.Visible = false;
                divCompetition.Visible = false;
                divTeam.Visible = false;
                divAssignToClub.Visible = false;
            }
            else
            {
                divAssignToTeam.Visible = false;
                divUserType.Visible = false;
                divPlayerType.Visible = false;
                divSport.Visible = false;
                divCompetition.Visible = false;
                divTeam.Visible = false;
                divPlayerJerseyNo.Visible = false;
                divPlayerJerseyName.Visible = false;
                divPlayerFamousName.Visible = false;
            }
        }

        protected void ddlSport_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sportid = Convert.ToInt32(ddlSport.SelectedValue);
            FillCompetition(sportid);
            FillTeam(sportid);
            FillClub(sportid);
            FillPlayerType(sportid);
            FillTeamMemberType(sportid);
            FillClubMemberType(sportid);
        }

        private void FillCompetition(int sportid)
        {
            DataTable dt = new DataTable();
            dt = ccc.GetCompetitionBySportID(sportid);
            if (dt.Rows.Count > 0)
            {
                ddlCompetition.DataSource = dt;
                ddlCompetition.DataTextField = "CompetitionName";
                ddlCompetition.DataValueField = "CompetitionId";
                ddlCompetition.DataBind();
                ddlCompetition.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillTeam(int sportid)
        {
            DataTable dt = new DataTable();
            dt = ccc.GetTeamBySportID(sportid);
            if (dt.Rows.Count > 0)
            {
                ddlTeam.DataSource = dt;
                ddlTeam.DataTextField = "TeamName";
                ddlTeam.DataValueField = "TeamId";
                ddlTeam.DataBind();
                ddlTeam.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        protected void drpSelectionEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSelectionEntry.SelectedValue == "1")
            {
                if (ddlUserRole.SelectedItem.ToString() == "Player")
                {
                    divUserType.Visible = true;
                    ddlUserType.SelectedValue = "4";
                    ddlUserType.Enabled = false;
                    divPlayerType.Visible = true;
                    divSport.Visible = true;
                    divCompetition.Visible = true;
                    divTeam.Visible = true;
                    divPlayerJerseyNo.Visible = true;
                    divPlayerJerseyName.Visible = true;
                    divPlayerFamousName.Visible = true;
                }
                else if (ddlUserRole.SelectedItem.ToString() == "Team Member")
                {
                    divTeamMemberType.Visible = true;
                    divSport.Visible = true;
                    divCompetition.Visible = true;
                    divTeam.Visible = true;
                    divteammemberjerseyno.Visible = true;
                    divTeamMemberJerseyName.Visible = true;
                    divTeamMemberFamousName.Visible = true;
                }
            }
            else
            {
                ddlUserType.Enabled = false;
                divPlayerType.Visible = false;
                divSport.Visible = false;
                divCompetition.Visible = false;
                divTeam.Visible = false;
                divPlayerJerseyNo.Visible = false;
                divPlayerJerseyName.Visible = false;
                divPlayerFamousName.Visible = false;
            }
        }

        protected void ddlAssignToClub_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAssignToClub.SelectedValue == "1")
            {
                if (ddlUserRole.SelectedItem.ToString() == "Club Owner")
                {
                    divSport.Visible = true;
                    divClub.Visible = true;
                    divClubOwnerDescription.Visible = true;
                    divClubOwnerPercentage.Visible = true;
                }
                else if (ddlUserRole.SelectedItem.ToString() == "Club Member")
                {
                    divSport.Visible = true;
                    divClub.Visible = true;
                    divClubMemberType.Visible = true;
                    divClubMemberDesc.Visible = true;
                }
            }
            else
            {
                divSport.Visible = false;
                divClub.Visible = false;
                divClubOwnerDescription.Visible = false;
                divClubOwnerPercentage.Visible = false;

                divClubMemberType.Visible = false;
                divClubMemberDesc.Visible = false;
            }
        }

    }
}