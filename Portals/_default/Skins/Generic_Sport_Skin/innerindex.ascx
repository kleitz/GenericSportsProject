<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="MENU" src="~/DesktopModules/DDRMenu/Menu.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Meta" Src="~/Admin/Skins/Meta.ascx" %> 


<!-- Adding CSS files into Skin -->


<dnn:DnnCssInclude runat="server" FilePath="css/jquery-ui.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/prettyphoto.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/flexslider.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/font-awesome.min.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/jquery.sidr.light.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/color.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="js/media/mediaelementplayer.min.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="js/owl-carousel/owl.carousel.css" PathNameAlias="SkinPath"/>

<dnn:DnnCssInclude runat="server" FilePath="js/fancybox/jquery.fancybox.css?v=2.1.4" PathNameAlias="SkinPath"/>

<!-- Slider CSS-->
<dnn:DnnCssInclude runat="server" FilePath="css/mosaic.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/responsive.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/rs-plugin.css" PathNameAlias="SkinPath"/>
 <dnn:DnnCssInclude runat="server" FilePath="jquery.bxslider.css" PathNameAlias="SkinPath"/>

<!-- Slider JS -->
<dnn:DnnCssInclude runat="server" FilePath="js/rs-plugin/css/settings.css" PathNameAlias="SkinPath" />
<dnn:DnnCssInclude runat="server" FilePath="css/tooltipster.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/mega.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/skin2.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/full.css" PathNameAlias="SkinPath"/>

<dnn:DnnCssInclude runat="server" FilePath="css/normalize.min.css" PathNameAlias="SkinPath"/>
 
<dnn:DnnJsInclude runat="server" FilePath="js/vendor/modernizr-2.6.2-respond-1.1.0.min.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="cus.js" PathNameAlias="SkinPath" />
	
<!-- Start Scripts --> 
<dnn:DnnJsInclude runat="server" FilePath="js/rs-plugin/js/jquery.themepunch.tools.min.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/rs-plugin/js/jquery.themepunch.revolution.min.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/jquery.prettyphoto.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/jquery.flexslider.js" PathNameAlias="SkinPath" />


<dnn:DnnJsInclude runat="server" FilePath="js/jquery.sidr.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/fancybox/jquery.fancybox.js?v=2.1.4" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/cleantabs.jquery.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/fitvids.min.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/jquery.scrollUp.min.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/media/mediaelement-and-player.min.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/owl-carousel/owl.carousel.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/selectivizr-min.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/placeholder.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/jquery.stellar.min.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/mosaic.1.0.1.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/jquery.isotope.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/toggle.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/jquery.tooltipster.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/jquery.countdown.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/jquery.sticky.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/slider-3.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/html5media.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="jquery.bxslider.min.js" PathNameAlias="SkinPath" />

<dnn:DnnJsInclude runat="server" FilePath="js/main.js" PathNameAlias="SkinPath" />


	<!-- Header Area -->
	
	<div id="displaylogo" class="outter-wrapper header-area header-style-1">
		<div class="wrapper clearfix logo-container">
			<header>
				<div class="clearfix">

					<div class="main-header-left left adjust-left">
						<!-- Start Logo -->
						<a class="logo" href="index.html">
							<img src="/portals/_default/Skins/Generic_Sport_Skin/images/logo-white.png" alt="Sport" />
						</a>
					</div>
								
					<div class="main-header-right right adjust-right" style="margin:20px;">
						<dnn:LOGIN ID="dnnLogin" CssClass="LoginLink" runat="server" LegacyMode="false"/>
					</div>
					
					<div class="main-header-right right adjust-right">
						<!-- Start Ad -->
						<a href="#" class="ads ad-468">
							<img src="/portals/_default/Skins/Generic_Sport_Skin/images/ads/468x60.png" alt="Ad" />
						</a>
					</div>
					
				</div>
			</header>
		</div>
	</div>	
    	
		<!-- Login Start -->

		<div class="outter-wrapper nav-container post-header-area header-style-3">
			<div id="navigation" class="clearfix">
				<div class="post-header-center centered-menu">
					<div class="nav megamenu_container wrapper">
						<dnn:MENU id="newmenu" runat="server" MenuStyle="DNNMobileNav"></dnn:MENU>
					</div>
				</div>
			</div>
		</div>
		
	<!-- Login Close -->
	
	<!-- Post Header Area -->
 <div id="ContentPane" class="dnn_container" runat="server"></div>
	
		
	<!-- Start Outter Wrapper -->
	<div id="homepagecontactus" class="outter-wrapper footer-wrapper">		
		<div class="wrapper clearfix">
			<!-- Start Widget -->
			<div class="col-1-4 widget">
				<h3 class="widget-title">Contact Us</h3>
				<ul class="list-1">
					<li>
						<strong>Telephone Enquiry:</strong><br/>  
					   +260 954280548
					</li>
					<li><a href="#">info@zambiafutsal.com</a></li>	
				</ul>	
			</div>
			
			
			
			<!-- Start Widget -->
			<div class="col-1-4 widget">
				<h3 class="widget-title">Useful Links</h3>
				<ul>
					<li class="cat-item cat-item-28"><a href="http://www.fifa.com">Fifa</a> </li>
					<li class="cat-item cat-item-24"><a href="http://www.fazfootball.com">FAZ Football</a> </li>
					<li class="cat-item cat-item-1"><a href="http://www.cafonline.com">Caf Online</a> </li>
					<li class="cat-item cat-item-27"><a href="http://www.futsalfocus.net">Futsal Focus</a> </li>
					<li class="cat-item cat-item-25"><a href="http://www.futsalplanet.com">Futsal Planet</a> </li>
					<li class="cat-item cat-item-75"><a href="http://www.pasionfutsal.com.ar/">Pasion Futsal</a> </li>
					<li class="cat-item cat-item-23"><a href="http://www.ilcalcioa5.com/">Ilcalcioa5</a> </li>
					<li class="cat-item cat-item-26"><a href="http://www.coachingfutsal.com">Coaching Futsal</a> </li>
				</ul>
			</div>
			
		
			
			<!-- Start Widget -->
			<div class="col-1-4 widget">
				<h3 class="widget-title">Club House Hours</h3>
				<ul class="open-hours">
					<li>Mondays <span>9am - 10pm</span></li>
					<li>Tue - Fri <span>9am - 9pm</span></li>
					<li>Sat - Sun <span>7am - 9pm</span></li>
					<li>Public Holidays <span>7am - 1am</span></li>
				</ul>	
			</div>
			
			
			<!-- Start Widget -->
			<div class="col-1-4 widget last">
				<a class="logo" href="index.html">
					<img src="/portals/_default/Skins/Generic_Sport_Skin/images/logo-white.png" alt="Sport" />
				</a>
				<ul class="list-1">
					<li>
	    				<strong>Address:</strong><br/>  
	    				Sabrewing Infotech Zambia Limited
						1669 Panganani Road,
						Lusaka 10101, Zambia.
					</li>
					<li>
						<strong><a href="contact.html">View on Map</a></strong>
					</li>
				</ul>	
			</div>
		</div>
	</div>
    	    	
	<!-- Start Outter Wrapper -->
	<div class="outter-wrapper base-wrapper">
		<div class="wrapper clearfix">
			<div class="left">Powered By <img src="/portals/_default/Skins/Generic_Sport_Skin/images/footer_logo.png" style="width: 30px;" /><a href="http://www.hummingbird-infotech.com" target="_blank">HummingBird Infotech</a></div>
			
			<!-- Social Icons -->
			<ul class="social-links right">
				<li><a class="fa" title="Facebook" href="#">&#xf09a;</a></li>
				<li><a class="fa" title="Twitter" href="#">&#xf099;</a></li>
				<li><a class="fa" title="Google Plus" href="#">&#xf0d5;</a></li>
			</ul>
		</div>
	</div>
