<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="MENU" src="~/DesktopModules/DDRMenu/Menu.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>

<%@ Register TagPrefix="dnn" TagName="Meta" Src="~/Admin/Skins/Meta.ascx" %> 
<dnn:Meta runat="server" Name="viewport" Content="initial-scale=1.0,width=device-width" />

<dnn:DnnCssInclude runat="server" FilePath="css/css/bootstrap.min.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/css/bootstrap-responsive.min.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/font-awesome/css/font-awesome.min.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/style-metro.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/style.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/style-responsive.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/themes/default.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="js/plugins/uniform/css/uniform.default.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="js/plugins/data-tables/DT_bootstrap.css" PathNameAlias="SkinPath"/>

<dnn:DnnCssInclude runat="server" FilePath="css/color.css" PathNameAlias="SkinPath"/>


<dnn:DnnJsInclude runat="server" FilePath="js/plugins/bootstrap/js/bootstrap.min.js" PathNameAlias="SkinPath"/>
<dnn:DnnJsInclude runat="server" FilePath="js/plugins/bootstrap-hover-dropdown/twitter-bootstrap-hover-dropdown.min.js" PathNameAlias="SkinPath"/>
<dnn:DnnJsInclude runat="server" FilePath="js/plugins/jquery-slimscroll/jquery.slimscroll.min.js" PathNameAlias="SkinPath"/>
<dnn:DnnJsInclude runat="server" FilePath="js/plugins/jquery.blockui.min.js" PathNameAlias="SkinPath"/>
<dnn:DnnJsInclude runat="server" FilePath="js/plugins/jquery.cookie.min.js" PathNameAlias="SkinPath"/>
<dnn:DnnJsInclude runat="server" FilePath="js/plugins/uniform/jquery.uniform.min.js" PathNameAlias="SkinPath"/>


<dnn:DnnJsInclude runat="server" FilePath="js/load.js" PathNameAlias="SkinPath"/>
<dnn:DnnJsInclude runat="server" FilePath="js/plugins/select2/select2.min.js" PathNameAlias="SkinPath"/>
<dnn:DnnJsInclude runat="server" FilePath="js/plugins/data-tables/jquery.dataTables.js" PathNameAlias="SkinPath"/>


<dnn:DnnJsInclude runat="server" FilePath="js/scripts/app.js" PathNameAlias="SkinPath"/>
<dnn:DnnJsInclude runat="server" FilePath="js/scripts/table-managed.js" PathNameAlias="SkinPath"/>


    <div class="page">
<!--<header id="header">

		<div id="mainheader">
            <div class="container">
				
			</div>
        </div>

    </header>-->
	<!-- Header END -->


    <!-- Main START -->
	<div class="header_page">
			
		<div class="row-fluid">
		<div class="span2">
			<div id="ChangeSport" runat="server" style="float:left; margin: 23px 0;">
					
					</div>
		</div>
		<div class="span3" style="margin-left:65px;">
			
						<div>
							<span style="color:white;font-size:30px;padding:14px 0;float:left;"> ADMIN </span><br/>
							<span style="color:red;border-top: 1px solid #fff;width:auto;font-size:12px;float:left;clear:both;padding:10px 0;"> CONTROL PANEL </span>
						</div>
		</div>
		<div class="span7" style="margin-left:-32px;">
			<div style="float:left;">
				<!-- <img src="/portals/_default/skins/adminpanel/img/logo2.png"  style="height:88px;" alt="Zambia Futsal"/> -->
			</div>
			<div style="float:right">
				<div style="float:left;">
					<img src="/portals/_default/skins/adminpanel/img/footer_logo.png" style="height:84px;" />
				</div>
				<div style="float:left;">
					<span style="color:red;font-size:22px;display:block;padding:10px 0;">SPORTS MANAGEMENT SYSTEM</span>
					<span style="color:white;border-top: 1px solid #fff;display:block;padding:10px 0;font-size:22px;">SABREWING INFOTECH (Z) LTD.</span>
				</div>
			</div>
		</div>
</div>		
				
		
		
	
		</div>
		
	<div style="clear:both;"></div>	
    <div class="page-container row-fluid">
	    <div id="ContentPane" runat="server"></div>
	</div>

	<div id="HitCounter" runat="server">
					</div>
    <!-- Main END -->

    <!-- BEGIN FOOTER -->
	<center>
	<div class="footer">
		<div class="footer-inner">
			Powered By <img src="/portals/_default/skins/adminpanel/img/footer_logo.png" style="width: 30px;" /><a href="http://www.hummingbird-infotech.com" target="_blank" style="color:red;">HummingBird Infotech</a>
		</div>
		<div class="footer-tools">
			<span class="go-top">
			<i class="icon-angle-up"></i>
			</span>
		</div>
	</div>
	</center>
	<!-- END FOOTER -->

    </div>




