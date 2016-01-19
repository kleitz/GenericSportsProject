<%@ Control Language="C#" AutoEventWireup="false" Inherits="DotNetNuke.Modules.ThSport.Password" Codebehind="Password.ascx.cs" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/DesktopModules/ThSport/Admin/Security/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls"%>

<center>
<div class="userlist-container">

<div class="dnnForm dnnPassword dnnClear"  id="dnnPassword">

    <div style="margin-top: 20px;width: 930px;margin-left:25px;margin-bottom:10px;">
        <div class="form-header-title" style="text-align:left;">
		    Website User
	    </div>


        <div class="form-area">
        <div class="form-header">

            
              <div style="width: 100%;margin-top:10px;" >
                                            <asp:ValidationSummary ID="ValidationSummary1" CssClass="dnnFormMessage dnnFormValidationSummary"
                                                HeaderText="You must enter a value in the following fields:   " DisplayMode="SingleParagraph"
                                                ValidationGroup="Sports" EnableClientScript="true" runat="server" />
                                        </div>

    <fieldset>
		<asp:Panel runat="server" ID="CannotChangePasswordMessage" CssClass="dnnFormMessage dnnFormWarning" Visible="False"><%=LocalizeString("CannotChangePassword") %></asp:Panel>

        <!-- New Design-->
        
		<asp:panel id="pnlChange" runat="server">
		    <%--<h2 class="dnnFormSectionHead"><asp:label id="lblChangePasswordHeading" runat="server" resourceKey="ChangePassword" /></h2>--%>
            <div class="dnnFormItem"><asp:label id="lblChangeHelp" runat="server" /></div>
            <br />
            <div id="oldPasswordRow" runat="server" class="dnnFormItem">
                <dnn:label id="plOldPassword" runat="server" Text="Old Password:"  controlname="txtOldPassword" />
                <asp:textbox id="txtOldPassword" runat="server" textmode="Password" size="25" maxlength="128" />
            </div>


            <div class="dnnFormItem">
                <dnn:label id="lblusername" runat="server" Text="UserName:" controlname="txtUserName" />
               <div style="position:relative;">
                 <asp:TextBox ID="txtUserName" runat="server" Width="28%"></asp:TextBox> <asp:RequiredFieldValidator ID="RFVVideoName" runat="server" ErrorMessage="User Name,"
                ControlToValidate="txtUserName" SetFocusOnError="true"  ValidationGroup="Sports"  Text="Please Enter Name" ClientIDMode="Static" CssClass="errorfordnn"></asp:RequiredFieldValidator></div>
            </div>

            <div class="dnnFormItem">
                <dnn:label id="lbluseremailid" runat="server" Text="EmailID:" controlname="txtEmailID" />
                <div style="position:relative;">
                <asp:TextBox ID="txtEmailID" runat="server" Width="28%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email ID,"
                ControlToValidate="txtEmailID" SetFocusOnError="true"  ValidationGroup="Sports"  Text="Please Enter Email ID" ClientIDMode="Static" CssClass="errorfordnn"></asp:RequiredFieldValidator></div>
            </div>

            <div class="dnnFormItem">
                <dnn:label id="plNewPassword" runat="server" Text="New Password:" controlname="txtNewPassword" />
                <asp:Panel ID="passwordContainer" runat="server" style="margin-left:1px;"><div style="position:relative;">
                    <asp:textbox id="txtNewPassword" runat="server" textmode="Password" size="25" maxlength="20" Width="28%" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password,"
                ControlToValidate="txtNewPassword" SetFocusOnError="true"  ValidationGroup="Sports"  Text="Please Enter Password" ClientIDMode="Static" CssClass="errorfordnn"></asp:RequiredFieldValidator></div>
                </asp:Panel>
            </div>
            <div class="dnnFormItem">
                <dnn:label id="plNewConfirm" Text="Confirm Password:" runat="server" controlname="txtNewConfirm" />
                <div style="position:relative;">
                <asp:textbox id="txtNewConfirm" runat="server" textmode="Password" size="25" maxlength="128" CssClass="password-confirm" Width="28%" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Password,"
                ControlToValidate="txtNewConfirm" SetFocusOnError="true"  ValidationGroup="Sports"  Text="Please Enter Password" ClientIDMode="Static" CssClass="errorfordnn"></asp:RequiredFieldValidator></div>
               
            </div>
            <div id="captchaRow" runat="server" visible="false" class="dnnFormItem dnnCaptcha">
                <dnn:label id="captchaLabel" controlname="ctlCaptcha" runat="server" />
                <dnn:captchacontrol id="ctlCaptcha" captchawidth="130" captchaheight="40" ErrorStyle-CssClass="dnnFormMessage dnnFormError dnnCaptcha" runat="server" />
            </div>
            <div id="statusPassword" runat="server" class="smallMessage">
                        <asp:Label ID="PwdStatus" runat="server" CssClass="prodetail-lable" Text="" Visible="false" />
                    </div>

            <div class="dnnClear"></div>


            <ul class="dnnActions dnnClear">
                 <li><asp:LinkButton id="cmdUpdate" Text="Save" runat="server" CssClass="dnnPrimaryAction"  ValidationGroup="Sports" ResourceKey="ChangePassword" /></li>
            </ul>  
            
            
                 
		</asp:panel>

        

       	<asp:panel id="pnlReset" runat="server">
            <h2 class="dnnFormSectionHead"><asp:label id="lblResetHeading" runat="server" ResourceKey="ResetPassword" /></h2>
            <div class="dnnFormItem"><asp:label id="lblResetHelp" runat="server"></asp:label></div>
            <div id="questionRow" runat="server" class="dnnFormItem">
                <dnn:label id="plQuestion" runat="server" controlname="lblQuestion" />
                <asp:label id = "lblQuestion" runat="server" />
            </div>
            <div id="answerRow" runat="server" class="dnnFormItem">
                <dnn:label id="plAnswer" runat="server" controlname="txtAnswer" />
                <asp:textbox id="txtAnswer" runat="server" size="25" maxlength="20" />
            </div>
		</asp:panel>

        
       
		<asp:panel id="pnlQA" runat="server">
            <div class="dnnFormItem"><asp:label id="lblChangeQA" runat="server" resourceKey="ChangeQA" /></div>
            <div class="dnnFormItem"><asp:label id="lblQAHelp" resourcekey="QAHelp" cssclass="Normal" runat="server" /></div>
            <div class="dnnFormItem">
                <dnn:label id="plQAPassword" runat="server" controlname="txtQAPassword" />
                <asp:textbox id="txtQAPassword" runat="server" textmode="Password" size="25" maxlength="20" />
            </div>
            <div class="dnnFormItem">
                <dnn:label id="plEditQuestion" runat="server" controlname="lblQuetxtEditQuestionstion" />
                <asp:textbox id="txtEditQuestion" runat="server" size="25" maxlength="20" />
            </div>
            <div class="dnnFormItem">
                <dnn:label id="plEditAnswer" runat="server" controlname="txtEditAnswer" />
                <asp:textbox id="txtEditAnswer" runat="server" size="25" maxlength="20" />
            </div>
		</asp:panel>
        <br />
        <div class="dnnFormItem">
            <dnn:label id="plLastChanged" Text="Password Last Changed:" runat="server" />
            <asp:label id = "lblLastChanged" runat="server" />
        </div>
        
        <div class="dnnFormItem">
            <dnn:label id="plExpires" Text="Password Expires:" runat="server"/>
            <asp:label id = "lblExpires" runat="server"/>
        </div>

        

    </fieldset>
    <ul class="dnnActions dnnClear" style="float:left;">
        <li><asp:LinkButton id="cmdReset" Text="Send Password Reset Link" runat="server" CssClass="dnnPrimaryAction" resourcekey="ResetPassword" /></li>
        <li><asp:LinkButton id="cmdUserReset" runat="server" CssClass="dnnPrimaryAction" resourcekey="ResetPassword" Visible="False" /></li>
        <li><asp:LinkButton id="cmdUpdateQA" runat="server" CssClass="dnnSecondaryAction" resourcekey="SaveQA" /></li>
    </ul>  

    </div>

        </div>
         </div>
</div>

</div>
</center>