#region Copyright
// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2014
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion
#region Usings

using System;
using System.Threading;
using System.Web.Security;
using System.Web.UI;

using DotNetNuke.Common.Utilities;
using DotNetNuke.Common;
using DotNetNuke.Entities.Host;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Users.Membership;
using DotNetNuke.Framework;
using DotNetNuke.Instrumentation;
using DotNetNuke.Security;
using DotNetNuke.Security.Membership;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.Log.EventLog;
using DotNetNuke.Services.Mail;
using DotNetNuke.UI.Skins.Controls;
using DotNetNuke.UI.Utilities;
using DotNetNuke.Web.Client.ClientResourceManagement;
using DotNetNuke.Web.UI.WebControls;

#endregion

namespace DotNetNuke.Modules.ThSport
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Password UserModuleBase is used to manage Users Passwords
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// 	[cnurse]	03/03/2006  created
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class Password : UserModuleBase
    {
    	private static readonly ILog Logger = LoggerSource.Instance.GetLogger(typeof (Password));

        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        protected bool UseCaptcha
        {
            get
            {
                return Convert.ToBoolean(GetSetting(PortalId, "Security_CaptchaChangePassword"));
            }
        }
        #region Delegates

        public delegate void PasswordUpdatedEventHandler(object sender, PasswordUpdatedEventArgs e);

        #endregion
		
		#region Public Properties

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gets the UserMembership associated with this control
        /// </summary>
        /// <history>
        /// 	[cnurse]	03/03/2006  Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public UserMembership Membership
        {
            get
            {
                UserMembership _Membership = null;
                if (User != null)
                {
                    _Membership = User.Membership;
                }
                return _Membership;
            }
        }

		
		#endregion

        #region Properties For UserDetail

        protected string user_Name
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["u"]))
                {
                    return Request.QueryString["u"].ToString();
                }
                return "";
            }
        }

        int user_Id
        {
            get
            {
                int i = 0;
                if (ViewState["user_Id"] != null)
                {
                    int.TryParse(ViewState["user_Id"].ToString(), out i);
                    return i;
                }
                return i;
            }
            set
            {
                ViewState["user_Id"] = value;
            }
        }

        int usertype
        {
            get
            {
                int _usertype = 1;
                if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                    int.TryParse(Request.QueryString["type"].ToString(), out _usertype);
                return _usertype;
            }
        }

        #endregion Properties For UserDetail
        

        #region Events


        public event PasswordUpdatedEventHandler PasswordUpdated;
        public event PasswordUpdatedEventHandler PasswordQuestionAnswerUpdated;

		#endregion

		#region Event Methods

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Raises the PasswordUpdated Event
        /// </summary>
        /// <history>
        /// 	[cnurse]	03/08/2006  Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public void OnPasswordUpdated(PasswordUpdatedEventArgs e)
        {
            if (IsUserOrAdmin == false && !currentUser.IsInRole("clubadmin"))
            {
                return;
            }
            if (PasswordUpdated != null)
            {   
                PasswordUpdated(this, e);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Raises the PasswordQuestionAnswerUpdated Event
        /// </summary>
        /// <history>
        /// 	[cnurse]	03/09/2006  Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public void OnPasswordQuestionAnswerUpdated(PasswordUpdatedEventArgs e)
        {
            if (IsUserOrAdmin == false)
            {
                return;
            }
            if (PasswordQuestionAnswerUpdated != null)
            {
                PasswordQuestionAnswerUpdated(this, e);
            }
        }

		#endregion

        #region For Update User Methods

        /// <summary>
        /// method checks to see if its allowed to change the username
        /// valid if a host, or an admin where the username is in only 1 portal
        /// </summary>
        /// <returns></returns>
        private bool CanUpdateUsername()
        {
            UserInfo users = DotNetNuke.Entities.Users.UserController.GetUserByName(user_Name);

            //do not allow for non-logged in users
            //if (Request.IsAuthenticated == false || AddUser)
            if (Request.IsAuthenticated == false)
            {
                return false;
            }

            //can only update username if a host/admin and account being managed is not a superuser
            if (UserController.GetCurrentUserInfo().IsSuperUser || currentUser.IsInRole("clubadmin"))
            {
                //only allow updates for non-superuser accounts
                

                if (users.IsSuperUser == false)
                {
                    return true;
                }
            }

            //if an admin, check if the user is only within this portal
            if ((UserController.GetCurrentUserInfo().IsInRole(PortalSettings.AdministratorRoleName)) || currentUser.IsInRole("clubadmin"))
            {
                //only allow updates for non-superuser accounts
                if (users.IsSuperUser)
                {
                    return false;
                }

                if (PortalController.GetPortalsByUser(users.UserID).Count == 1) return true;
            }

            return false;
        }


        #endregion For Update User Methods

        #region "Private Methods"
        /// <summary>
        /// reset and change password
        /// used by admin/host users who do not need to supply an "old" password
        /// </summary>
        /// <param name="user">user being changed</param>
        /// <param name="newPassword">new password</param>
        /// <returns></returns>
        private static bool ResetAndChangePassword(UserInfo user, string newPassword)
        {
            var portalSettings = PortalController.GetCurrentPortalSettings();
            if (DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo().IsInRole(portalSettings.AdministratorRoleName)
                || DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo().IsInRole("clubadmin"))
            {
                string resetPassword = UserController.ResetPassword(user, String.Empty);
                return UserController.ChangePassword(user, resetPassword, newPassword);
            }
            return false;
        }

        #endregion

        #region Public Methods

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// DataBind binds the data to the controls
        /// </summary>
        /// <history>
        /// 	[cnurse]	03/03/2006  Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public override void DataBind()
        {
            UserInfo user_Detail = DotNetNuke.Entities.Users.UserController.GetUserByName(user_Name);

         
            lblLastChanged.Text = user_Detail.Membership.LastPasswordChangeDate.ToLongDateString();

            

            //Set Password Expiry Label
            if (user_Detail.Membership.UpdatePassword)
            {
                lblExpires.Text = Localization.GetString("ForcedExpiry", LocalResourceFile.Replace("/Admin/Security/", "/"));

                
            }
            else
            {
                lblExpires.Text = PasswordConfig.PasswordExpiry > 0 ? user_Detail.Membership.LastPasswordChangeDate.AddDays(PasswordConfig.PasswordExpiry).ToLongDateString() : Localization.GetString("NoExpiry", LocalResourceFile.Replace("/Admin/Security/", "/"));
            }
			
            //Get UserName For Info
            
            ViewState["user_Id"] = user_Detail.UserID.ToString();


            txtUserName.Text = user_Name;
            if (!string.IsNullOrEmpty(user_Detail.Email))
            {
                txtEmailID.Text = user_Detail.Email;
            }
            


           if (((!MembershipProviderConfig.PasswordRetrievalEnabled) && IsAdmin && (!IsUser)))
            {
                pnlChange.Visible = true;
                cmdUpdate.Visible = true;
                oldPasswordRow.Visible = false;
                lblChangeHelp.Text = Localization.GetString("AdminChangeHelp", LocalResourceFile.Replace("/Admin/Security/", "/"));
            }
            else
            {
                pnlChange.Visible = true;
                cmdUpdate.Visible = true;
				
				//Set up Change Password
                if ((IsAdmin && !IsUser) || currentUser.IsInRole("clubadmin"))
                {
                    lblChangeHelp.Text = Localization.GetString("AdminChangeHelp", LocalResourceFile.Replace("/Admin/Security/", "/"));
                    oldPasswordRow.Visible = false;
                }
                else
                {
                    lblChangeHelp.Text = Localization.GetString("UserChangeHelp", LocalResourceFile.Replace("/Admin/Security/", "/"));
                    if (Request.IsAuthenticated)
                    {
                        pnlChange.Visible = true;
                        cmdUserReset.Visible = false;
                        cmdUpdate.Visible = true;
                    }
                    else
                    {
                        pnlChange.Visible = false;
                        cmdUserReset.Visible = true;
                        cmdUpdate.Visible = false;
                    }
                }
            }
			
            //If Password Reset is not enabled then only the Admin can reset the 
            //Password, a User must Update
            if (!MembershipProviderConfig.PasswordResetEnabled)
            {
                pnlReset.Visible = false;
                cmdReset.Visible = false;
            }
            else
            {
                pnlReset.Visible = true;
                cmdReset.Visible = true;
				
				//Set up Reset Password
                if (IsAdmin && !IsUser || currentUser.IsInRole("clubadmin"))
                {
                    if (MembershipProviderConfig.RequiresQuestionAndAnswer)
                    {
                        pnlReset.Visible = false;
                        cmdReset.Visible = false;
                    }
                    else
                    {
                        lblResetHelp.Text = Localization.GetString("AdminResetHelp", LocalResourceFile.Replace("/Admin/Security/", "/"));
                    }
                    questionRow.Visible = false;
                    answerRow.Visible = false;
                }
                else
                {
                    if (MembershipProviderConfig.RequiresQuestionAndAnswer && IsUser)
                    {
                        lblResetHelp.Text = Localization.GetString("UserResetHelp", LocalResourceFile.Replace("/Admin/Security/", "/"));
                        lblQuestion.Text = User.Membership.PasswordQuestion;
                        questionRow.Visible = true;
                        answerRow.Visible = true;
                    }
                    else
                    {
                        pnlReset.Visible = false;
                        cmdReset.Visible = false;
                    }
                }
            }
			
            //Set up Edit Question and Answer area
            if (MembershipProviderConfig.RequiresQuestionAndAnswer && IsUser)
            {
                pnlQA.Visible = true;
                cmdUpdateQA.Visible = true;
            }
            else
            {
                pnlQA.Visible = false;
                cmdUpdateQA.Visible = false;
            }
        }

		#endregion

		#region Event Handlers

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ClientResourceManager.RegisterScript(Page, "~/Resources/Shared/scripts/dnn.jquery.extensions.js");
            ClientResourceManager.RegisterScript(Page, "~/Resources/Shared/scripts/dnn.jquery.tooltip.js");
            ClientResourceManager.RegisterScript(Page, "~/Resources/Shared/scripts/dnn.PasswordStrength.js");
			ClientResourceManager.RegisterScript(Page, "~/DesktopModules/Admin/Security/Scripts/dnn.PasswordComparer.js");

            jQuery.RequestDnnPluginsRegistration();
        }

        protected override void OnLoad(EventArgs e)
        {
            

            base.OnLoad(e);
            //ClientAPI.RegisterKeyCapture(Parent, cmdUpdate.Controls[0], 13);
            //ClientAPI.RegisterKeyCapture(this, cmdUpdate.Controls[0], 13);
            cmdReset.Click += cmdReset_Click;
            cmdUserReset.Click += cmdUserReset_Click;
            cmdUpdate.Click += cmdUpdate_Click;
            cmdUpdateQA.Click += cmdUpdateQA_Click;

			if (MembershipProviderConfig.RequiresQuestionAndAnswer && User.UserID != UserController.GetCurrentUserInfo().UserID)
			{
				pnlChange.Visible = false;
			    cmdUpdate.Visible = false;
				CannotChangePasswordMessage.Visible = true;
			}


            DataBind();

            if (UseCaptcha)
            {
                captchaRow.Visible = true;
                ctlCaptcha.ErrorMessage = Localization.GetString("InvalidCaptcha", LocalResourceFile.Replace("/Admin/Security/", "/"));
                ctlCaptcha.Text = Localization.GetString("CaptchaText", LocalResourceFile.Replace("/Admin/Security/", "/"));
            }
           
        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);


			if (Host.EnableStrengthMeter)
			{
				passwordContainer.CssClass = "password-strength-container";
				txtNewPassword.CssClass = "password-strength";

				var options = new DnnPaswordStrengthOptions();
				var optionsAsJsonString = Json.Serialize(options);
				var script = string.Format("dnn.initializePasswordStrength('.{0}', {1});{2}",
					"password-strength", optionsAsJsonString, Environment.NewLine);

				if (ScriptManager.GetCurrent(Page) != null)
				{
					// respect MS AJAX
					ScriptManager.RegisterStartupScript(Page, GetType(), "PasswordStrength", script, true);
				}
				else
				{
					Page.ClientScript.RegisterStartupScript(GetType(), "PasswordStrength", script, true);
				}
			}

			var confirmPasswordOptions = new DnnConfirmPasswordOptions()
			{
				FirstElementSelector = "#" + passwordContainer.ClientID + " input[type=password]",
				SecondElementSelector = ".password-confirm",
				ContainerSelector = ".dnnPassword",
				UnmatchedCssClass = "unmatched",
				MatchedCssClass = "matched"
			};

			var confirmOptionsAsJsonString = Json.Serialize(confirmPasswordOptions);
			var confirmScript = string.Format("dnn.initializePasswordComparer({0});{1}", confirmOptionsAsJsonString, Environment.NewLine);

			if (ScriptManager.GetCurrent(Page) != null)
			{
				// respect MS AJAX
				ScriptManager.RegisterStartupScript(Page, GetType(), "ConfirmPassword", confirmScript, true);
			}
			else
			{
				Page.ClientScript.RegisterStartupScript(GetType(), "ConfirmPassword", confirmScript, true);
			}
        }


        private void cmdReset_Click(object sender, EventArgs e)
        {
            if (IsUserOrAdmin == false)
            {
                return;
            }
            string answer = "";
            if (MembershipProviderConfig.RequiresQuestionAndAnswer && !IsAdmin)
            {
                if (String.IsNullOrEmpty(txtAnswer.Text))
                {
                    OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.InvalidPasswordAnswer));
                    return;
                }
                answer = txtAnswer.Text;
            }
            try
            {

                UserInfo usersDetail = DotNetNuke.Entities.Users.UserController.GetUserByName(user_Name);

                //create resettoken valid for 24hrs
                UserController.ResetPasswordToken(usersDetail, 1440);

                bool canSend = Mail.SendMail(usersDetail, MessageType.PasswordReminder, PortalSettings) == string.Empty;


                var message = String.Empty;
                var moduleMessageType = ModuleMessage.ModuleMessageType.GreenSuccess;
                if (canSend)
                {
                    message = Localization.GetString("PasswordSent", LocalResourceFile.Replace("/Admin/Security/", "/"));
                    message = "success msg";
                    LogSuccess();
                }
                else
                {
                    message = Localization.GetString("OptionUnavailable", LocalResourceFile.Replace("/Admin/Security/", "/"));
                    moduleMessageType=ModuleMessage.ModuleMessageType.RedError;
                    LogFailure(message);
                }

               
                UI.Skins.Skin.AddModuleMessage(this, message, moduleMessageType);

                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "User-List" + "&type=" + usertype));

            }
            catch (ArgumentException exc)
            {
                Logger.Error(exc);
                OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.InvalidPasswordAnswer));
            }
            catch (Exception exc)
            {
                Logger.Error(exc);
                OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.PasswordResetFailed));
            }
        }

        private void cmdUserReset_Click(object sender, EventArgs e)
        {
            try
            {
                //send fresh resettoken copy
                bool canSend = UserController.ResetPasswordToken(User,true);

                var message = String.Empty;
                var moduleMessageType = ModuleMessage.ModuleMessageType.GreenSuccess;
                if (canSend)
                {
                    message = Localization.GetString("PasswordSent", LocalResourceFile.Replace("/Admin/Security/", "/"));
                    LogSuccess();
                }
                else
                {
                    message = Localization.GetString("OptionUnavailable", LocalResourceFile.Replace("/Admin/Security/", "/"));
                    moduleMessageType = ModuleMessage.ModuleMessageType.RedError;
                    LogFailure(message);
                }


                UI.Skins.Skin.AddModuleMessage(this, message, moduleMessageType);
            }
            catch (ArgumentException exc)
            {
                Logger.Error(exc);
                OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.InvalidPasswordAnswer));
            }
            catch (Exception exc)
            {
                Logger.Error(exc);
                OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.PasswordResetFailed));
            }
        }

        private void LogSuccess()
        {
            LogResult(string.Empty);
        }

        private void LogFailure(string reason)
        {
            LogResult(reason);
        }

        private void LogResult(string message)
        {
            var portalSecurity = new PortalSecurity();

            var objEventLog = new EventLogController();
            var objEventLogInfo = new LogInfo();

            objEventLogInfo.LogPortalID = PortalSettings.PortalId;
            objEventLogInfo.LogPortalName = PortalSettings.PortalName;
            objEventLogInfo.LogUserID = UserId;
            objEventLogInfo.LogUserName = portalSecurity.InputFilter(User.Username,
                                                                     PortalSecurity.FilterFlag.NoScripting | PortalSecurity.FilterFlag.NoAngleBrackets | PortalSecurity.FilterFlag.NoMarkup);
            if (string.IsNullOrEmpty(message))
            {
                objEventLogInfo.LogTypeKey = "PASSWORD_SENT_SUCCESS";
            }
            else
            {
                objEventLogInfo.LogTypeKey = "PASSWORD_SENT_FAILURE";
                objEventLogInfo.LogProperties.Add(new LogDetailInfo("Cause", message));
            }

            objEventLog.AddLog(objEventLogInfo);
        }

        private void cmdUpdate_Click(Object sender, EventArgs e)
        {
            if ((UseCaptcha && ctlCaptcha.IsValid) || !UseCaptcha)
            {

                UserInfo usersData = DotNetNuke.Entities.Users.UserController.GetUserByName(user_Name);

                if (IsUserOrAdmin == false && !currentUser.IsInRole("clubadmin"))
                {
                    return;
                }
                //1. Check New Password and Confirm are the same
                if (txtNewPassword.Text != txtNewConfirm.Text)
                {
                    OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.PasswordMismatch));
                    return;
                }

                //2. Check New Password is Valid
                if (!UserController.ValidatePassword(txtNewPassword.Text))
                {
                    OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.PasswordInvalid));
                    return;
                }

                //3. Check old Password is Provided
                if (!IsAdmin && String.IsNullOrEmpty(txtOldPassword.Text) && !currentUser.IsInRole("clubadmin"))
                {
                    OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.PasswordMissing));
                    return;
                }

                //4. Check New Password is ddifferent
                if (!IsAdmin && txtNewPassword.Text == txtOldPassword.Text && !currentUser.IsInRole("clubadmin"))
                {
                    OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.PasswordNotDifferent));
                    return;
                }
                //5. Check New Password is not same as username or banned
                var settings = new MembershipPasswordSettings(User.PortalID);

                if (settings.EnableBannedList)
                {
                    var m = new MembershipPasswordController();
                    if (m.FoundBannedPassword(txtNewPassword.Text) || User.Username == txtNewPassword.Text)
                    {
                        OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.BannedPasswordUsed));
                        return;
                    }

                }
                if (!IsAdmin && txtNewPassword.Text == txtOldPassword.Text && !currentUser.IsInRole("clubadmin"))
                {
                    OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.PasswordNotDifferent));
                    return;
                }
                if (!IsAdmin && !currentUser.IsInRole("clubadmin"))
                {
                    try
                    {
                        OnPasswordUpdated(UserController.ChangePassword(User, txtOldPassword.Text, txtNewPassword.Text)
                                              ? new PasswordUpdatedEventArgs(PasswordUpdateStatus.Success)
                                              : new PasswordUpdatedEventArgs(PasswordUpdateStatus.PasswordResetFailed));
                    }
                    catch (MembershipPasswordException exc)
                    {
                        //Password Answer missing
                        Logger.Error(exc);

                        OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.InvalidPasswordAnswer));
                    }
                    catch (ThreadAbortException)
                    {
                        //Do nothing we are not logging ThreadAbortxceptions caused by redirects    
                    }
                    catch (Exception exc)
                    {
                        //Fail
                        Logger.Error(exc);

                        OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.PasswordResetFailed));
                    }
                }
                else
                {
                    try
                    {

                        if (CanUpdateUsername())
                        {
                            UserController.ChangeUsername(user_Id, txtUserName.Text);
                        }

                        UserInfo UserInfoDetails = DotNetNuke.Entities.Users.UserController.GetUserById(PortalId, user_Id);
                        UserInfoDetails.Membership.Email = txtEmailID.Text;

                        UserInfoDetails.Email = txtEmailID.Text;

                        UserController.UpdateUser(PortalId, UserInfoDetails);


                        bool flag = ResetAndChangePassword(usersData, txtNewPassword.Text);

                        OnPasswordUpdated(flag ? new PasswordUpdatedEventArgs(PasswordUpdateStatus.Success)
                                              : new PasswordUpdatedEventArgs(PasswordUpdateStatus.PasswordResetFailed));


                        if (flag)
                        {
                            PwdStatus.Text = "Password Changed Successfully";
                            PwdStatus.Visible = true;
                            statusPassword.Attributes.Add("class", "smallMessage successMessage");
                            statusPassword.Attributes.Add("style", "display:block");
                        }
                        else
                        {
                            PwdStatus.Text = "Password Changed Failed";
                            PwdStatus.Visible = true;
                            statusPassword.Attributes.Add("class", "smallMessage failureMessage");
                            statusPassword.Attributes.Add("style", "display:block");
                        }
                    }

                    

                    catch (MembershipPasswordException exc)
                    {
                        //Password Answer missing
                        Logger.Error(exc);

                        OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.InvalidPasswordAnswer));
                    }
                    catch (ThreadAbortException)
                    {
                        //Do nothing we are not logging ThreadAbortxceptions caused by redirects    
                    }
                    catch (Exception exc)
                    {
                        //Fail
                        Logger.Error(exc);

                        OnPasswordUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.PasswordResetFailed));
                    }

                    //try
                    //{
                        

                    //}

                    //catch (Exception exc)
                    //{
                    //    Logger.Error(exc);

                    //    //var args = new UserUpdateErrorArgs(User.UserID, User.Username, "EmailError");
                    //    //OnUserUpdateError(args);
                    //}
                }
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// cmdUpdate_Click runs when the Update Question and Answer  Button is clicked
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[cnurse]	03/09/2006  created
        /// </history>
        /// -----------------------------------------------------------------------------
        private void cmdUpdateQA_Click(object sender, EventArgs e)
        {
            if (IsUserOrAdmin == false)
            {
                return;
            }
            if (String.IsNullOrEmpty(txtQAPassword.Text))
            {
                OnPasswordQuestionAnswerUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.PasswordInvalid));
                return;
            }
            if (String.IsNullOrEmpty(txtEditQuestion.Text))
            {
                OnPasswordQuestionAnswerUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.InvalidPasswordQuestion));
                return;
            }
            if (String.IsNullOrEmpty(txtEditAnswer.Text))
            {
                OnPasswordQuestionAnswerUpdated(new PasswordUpdatedEventArgs(PasswordUpdateStatus.InvalidPasswordAnswer));
                return;
            }
			
            //Try and set password Q and A
            UserInfo objUser = UserController.GetUserById(PortalId, UserId);
            OnPasswordQuestionAnswerUpdated(UserController.ChangePasswordQuestionAndAnswer(objUser, txtQAPassword.Text, txtEditQuestion.Text, txtEditAnswer.Text)
                                                ? new PasswordUpdatedEventArgs(PasswordUpdateStatus.Success)
                                                : new PasswordUpdatedEventArgs(PasswordUpdateStatus.PasswordResetFailed));
        }
		
		#endregion

        #region Nested type: PasswordUpdatedEventArgs

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// The PasswordUpdatedEventArgs class provides a customised EventArgs class for
        /// the PasswordUpdated Event
        /// </summary>
        /// <history>
        /// 	[cnurse]	03/08/2006  created
        /// </history>
        /// -----------------------------------------------------------------------------
        public class PasswordUpdatedEventArgs
        {
            /// -----------------------------------------------------------------------------
            /// <summary>
            /// Constructs a new PasswordUpdatedEventArgs
            /// </summary>
            /// <param name="status">The Password Update Status</param>
            /// <history>
            /// 	[cnurse]	03/08/2006  Created
            /// </history>
            /// -----------------------------------------------------------------------------
            public PasswordUpdatedEventArgs(PasswordUpdateStatus status)
            {
                UpdateStatus = status;

                //if (status.ToString() == "Success")
                //{
                //    PwdStatus.Text = "Password Changed Successfully";
                //    PwdStatus.Visible = true;
                //    statusPassword.Attributes.Add("class", "smallMessage successMessage");
                //    statusPassword.Attributes.Add("style", "display:block");
                //}
            }

            /// -----------------------------------------------------------------------------
            /// <summary>
            /// Gets and sets the Update Status
            /// </summary>
            /// <history>
            /// 	[cnurse]	03/08/2006  Created
            /// </history>
            /// -----------------------------------------------------------------------------
            public PasswordUpdateStatus UpdateStatus { get; set; }
        }

        #endregion
    }
}