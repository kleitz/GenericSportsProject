<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="MENU" src="~/DesktopModules/DDRMenu/Menu.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Meta" Src="~/Admin/Skins/Meta.ascx" %> 

<dnn:Meta charset="utf-8"/>
<dnn:Meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
<dnn:Meta content="black" name="apple-mobile-web-app-status-bar-style"/>
<dnn:Meta name="description" content="Sport is the best theme for sports clubs and centres"/>
<dnn:Meta name="msapplication-TileColor" content="#ffffff"/>
<dnn:Meta name="msapplication-TileImage" content="favicons/mstile-144x144.png"/>

<!-- Load jQuery -->
<dnn:DnnJsInclude runat="server" FilePath="js/vendor/jquery-1.8.3.min.js" PathNameAlias="SkinPath" />

<!-- Adding CSS files into Skin -->

<dnn:DnnCssInclude runat="server" FilePath="css/normalize.min.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="styles.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/font-awesome.min.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/jquery.sidr.light.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="js/media/mediaelementplayer.min.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="js/owl-carousel/owl.carousel.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="js/fancybox/jquery.fancybox.css?v=2.1.4" PathNameAlias="SkinPath"/>

<!-- Slider CSS-->
<dnn:DnnCssInclude runat="server" FilePath="css/mosaic.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/responsive.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/rs-plugin.css" PathNameAlias="SkinPath"/>

<!-- Slider JS -->
<dnn:DnnCssInclude runat="server" FilePath="js/rs-plugin/css/settings.css" PathNameAlias="SkinPath" />
<dnn:DnnCssInclude runat="server" FilePath="css/tooltipster.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/mega.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/full.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/skin2.css" PathNameAlias="SkinPath"/> 


	
<!-- Start Scripts --> 
<dnn:DnnJsInclude runat="server" FilePath="js/rs-plugin/js/jquery.themepunch.tools.min.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/rs-plugin/js/jquery.themepunch.revolution.min.js" PathNameAlias="SkinPath" />
	
<dnn:DnnJsInclude runat="server" FilePath="js/jquery.sidr.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/fancybox/jquery.fancybox.js?v=2.1.4" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/cleantabs.jquery.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/fitvids.min.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/jquery.scrollUp.min.js" PathNameAlias="SkinPath" />
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
<dnn:DnnJsInclude runat="server" FilePath="js/main.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/vendor/modernizr-2.6.2-respond-1.1.0.min.js" PathNameAlias="SkinPath" />

<div id="wrappermain-pix" class="outter-wrapper pre-header-area header-style-3">
	<!-- Pre Header Area -->
	<!-- Login Start -->
	<div class="wrapper clearfix">
		<div class="pre-header-left left">
			<!-- Second Nav -->
			<ul>
			</ul>
		</div>
		<div class="pre-header-right right">
			<ul class="social-links boxy">
				
			</ul>
		</div>
	</div>	
</div>
 
	<!-- Header Area -->
	<div id="displaylogo" class="outter-wrapper header-area header-style-3">
		<div class="wrapper clearfix logo-container">
			<header>
				<div class="clearfix">
    				
					<!-- Start Logo -->
					<a class="logo centered" href="index.html">
						<img src="/portals/_default/Skins/GenericSportSkin/images/logo-white.png" alt="Sport" />
					</a>

				</div>
			</header>
		</div>
	</div>		
    	
	<!-- Post Header Area -->
	<div id="sitemenu" class="outter-wrapper nav-container post-header-area header-style-3">
		<dnn:MENU id="newmenu" runat="server" MenuStyle="DNNMobileNav"></dnn:MENU>
		<dnn:LOGIN ID="dnnLogin" CssClass="LoginLink" runat="server" LegacyMode="false"/>
	</div>
   		
	<!-- Revolution Slider -->
	<div id="mainbanner" class="tp-banner-container">
		<div class="tp-banner">
			<ul>
				<li data-transition="slidehorizontal" data-slotamount="1" data-masterspeed="1000"  data-fstransition="fade" data-fsmasterspeed="1000"  data-saveperformance="off">
					<!-- MAIN IMAGE -->
					<img src="/portals/_default/Skins/GenericSportSkin/images/media/Sprinkle.jpg"  alt="Title"  data-bgposition="left center" data-bgfit="cover" data-bgrepeat="no-repeat">
					<!-- LAYERS -->
			
					<!-- LAYER NR. 1 -->
					<div class="tp-caption tp-fade fadeout fullscreenvideo"
						data-x="0"
						data-y="0"
						data-speed="1000"
						data-start="1100"
						data-easing="Power4.easeOut"
						data-elementdelay="0.01"
						data-endelementdelay="0.1"
						data-endspeed="1500"
						data-endeasing="Power4.easeIn"
						data-autoplay="true"
						data-autoplayonlyfirsttime="false"
						data-nextslideatend="false"
			 			data-volume="mute" data-forceCover="1" data-aspectratio="16:9" data-forcerewind="on" >
				
						<video controls style="width: 100%; height: 100%" poster="/portals/_default/Skins/GenericSportSkin/images/media/Sprinkle.jpg" loop>
						<source src="/portals/_default/Skins/GenericSportSkin/images/media/Sprinkle.webm"  type='video/webm;codecs="vp8, vorbis"' />
						<source src="/portals/_default/Skins/GenericSportSkin/images/media/Sprinkle.mp4"   type='video/mp4;codecs="avc1.42E01E, mp4a.40.2"' />
						<source src="/portals/_default/Skins/GenericSportSkin/images/media/Sprinkle.ogv" type='video/ogg; codecs="theora, vorbis"'>
						</video>
					</div>
					
					<!-- LAYER NR. 2 -->
					<div class="tp-caption fadeout sfr sport-rs-boxed sport-rs-heading"
						data-x="0" data-hoffset="0"
						data-y="215" data-voffset="0"
						data-captionhidden="off"
						data-speed="800"
						data-start="500"
						data-easing="Power4.easeInOut"
						data-splitin="none"
						data-splitout="none"
						data-elementdelay="0.05"
						data-endelementdelay="0.1"
						data-endspeed="1000"
						data-endeasing="Power1.easeOut">
						Our Blood, Our Sweat,<br/> 
						Your Tears <div>- CLUB MOTTO</div>
					</div>
					
					<!-- LAYER NR. 3 -->
					<div class="tp-caption fadeout sfr sport-rs-boxed sport-rs-text"
						data-x="0" data-hoffset="0"
						data-y="340" data-voffset="0"
						data-captionhidden="off"
						data-speed="800"
						data-start="750"
						data-easing="Power4.easeInOut"
						data-splitin="none"
						data-splitout="none"
						data-elementdelay="0.05"
						data-endelementdelay="0.1"
						data-endspeed="1000"
						data-endeasing="Power1.easeOut">
						Cum sociis natoque penatibus et magnis dis parturient montes,<br/> 
						nascetur ridiculus mus. Maecenas sed diam eget risus varius<br/>
						 blandit sit amet non magna... 
					</div>
					
					<!-- LAYER NR. 4 -->
					<div class="tp-caption fadeout sfr tp-resizeme"
						data-x="0" data-hoffset="0"
						data-y="460" data-voffset="0"
						data-speed="800"
						data-start="1000"
						data-easing="Power4.easeInOut"
						data-splitin="none"
						data-splitout="none"
						data-elementdelay="0.05"
						data-endelementdelay="0.1"
						data-endspeed="1000"
						data-endeasing="Power1.easeOut">
						<a class="btn" href="pricing1.html">Join Our Club Today</a>&nbsp; <a class="btn-2" href="about.html">Read More</a>
					</div>	
				</li>
				

			</ul>
		</div>
	</div>
   				
    	
	<!-- Start Outter Wrapper -->
	<div id="startoutterwapper" class="outter-wrapper body-wrapper">		
		<div class="wrapper ad-pad clearfix">
			
			<!-- Column -->
			<div class="col-1-2">
				<h3>Welcome To Our Club</h3>
				
				<p class="lead">Donec ullamcorper nulla non metus auctor fringilla. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
				
				<p>Morbi leo risus, porta ac conse ctetur ac, vestiosite Duis mollis, est non commodo luctus, nisi erat porttitor ligula est non commodo sed diam. Donec id elit non mi porta gravida at eget metus. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Sed posuere consectetur est at lobortis. Curabitur blandit tempus porttitor. Nullam id dolor id nibh ultricies vehicula ut id elit. Fusce dapibus, tellus ac cursus commodo.</p> 
				
				<p><img src="/portals/_default/Skins/GenericSportSkin/images/sig.jpg" alt="Fill" /><br/> <strong>John Doe - Club President</strong></p>
				 					
			</div>
			
			
			
			<!-- Column -->
			<div class="col-1-4">
				<h3>In Profile</h3>
				<div class="mosaic-block circle">
					<a href="#" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
					<div class="corner">Assistant Coach</div><img src="/portals/_default/Skins/GenericSportSkin/images/team-2.jpg" alt="Mock" /></div>
				</div>
				<p>Morbi leo risus, porta ac conse ctetur ac, vestiosite Duis mollis, est non commodo luctus, nisi erat porttitor ligula. Maecenas sed diam eget risus varius
				&#8230;<a href="widgets.html" class="read-more">More</a></p>
			</div>
			

			
			
			<!-- Column -->
			<div class="col-1-4 last">
				<h3>Coming Events</h3>
				<ul class="widget-event-list">
					<li>
						<img class="left stay" src="/portals/_default/Skins/GenericSportSkin/images/thumb-3.jpg" alt="mock" />
						<div class="date">15 Aug</div>
						<h6 class="title"><a href="#">Melbourne Sharks VS Our Team - Home Game</a></h6>
					</li>
					
					<li>
						<img class="left stay" src="/portals/_default/Skins/GenericSportSkin/images/thumb-2.jpg" alt="mock" />
						<div class="date">26 Aug</div>
						<h6 class="title"><a href="#">Quarter Finals - Tigers VS Our Team - Home Game</a></h6>
					</li>
					
					<li>
						<img class="left stay" src="/portals/_default/Skins/GenericSportSkin/images/thumb-1.jpg" alt="mock" />
						<div class="date">31 Aug</div>
						<h6 class="title"><a href="#">Sydney VS Our Team - Away Game</a></h6>
					</li>
				</ul>
						
			</div>
			
			
		</div>
	</div>
    

	<!-- Start Outter Wrapper -->
	<div id="latestNews" class="outter-wrapper">
		<div class="wrapper ad-pad clearfix">
			
			<h3>Latest Club News</h3>
			
			<div class="col-1-4">
			  	<div class="mosaic-block circle">
			  		<a href="post.html" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
			  		<div class="corner date">16 Aug</div><img src="/portals/_default/Skins/GenericSportSkin/images/fill-4.jpg" alt="Mock" /></div>
			  	</div>
			  	
			  	<h6 class="title"><a href="post.html">Pre season camp success</a></h6>
			  	<p>Morbi leo risus, porta ac conse ctetur ac, vestiosite Duis mollis, est non commodo luctus, nisi erat porttitor ligula soren
			  	&#8230;<a href="post.html" class="read-more">More</a></p>
			</div>
			
				
			<div class="col-1-4">
			  	<div class="mosaic-block circle">
			  		<a href="post.html" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
			  		<div class="corner date">26 Aug</div><img src="/portals/_default/Skins/GenericSportSkin/images/fill-3.jpg" alt="Mock" /></div>
			  	</div>
			  	
			  	<h6 class="title"><a href="post.html">Nominated club of the year</a></h6>
			  	<p>Duis mollis, est non commodo luctus, nisi erat porttitor ligula. Maecenas sed diam eget risus varius blandit sit amet non
			  	&#8230;<a href="post.html" class="read-more">More</a></p>
			</div>
			
				
			<div class="col-1-4">
			  	<div class="mosaic-block circle">
			  		<a href="post.html" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
			  		<div class="corner date">17 Sept</div><img src="/portals/_default/Skins/GenericSportSkin/images/fill-5.jpg" alt="Mock" /></div>
			  	</div>
			  	
			  	<h6 class="title"><a href="post.html">Road to the grand finals</a></h6>
			  	<p>Vestiosite Duis mollis, est non commodo luctus, nisi erat porttitor sed diam eget risus varius blandit sit amet non magna
			  	&#8230;<a href="post.html" class="read-more">More</a></p>
			</div>
			
			
			<div class="col-1-4 last">
			  	<div class="mosaic-block circle">
			  		<a href="post.html" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
			  		<div class="corner date">24 Sept</div><img src="/portals/_default/Skins/GenericSportSkin/images/fill-2.jpg" alt="Mock" /></div>
			  	</div>
			  	
			  	<h6 class="title"><a href="post.html">Club facilities upgrade</a></h6>
			  	<p>Mollis, est non commodo luctus, nisi erat porttitor ligula. Maecenas sed diam eget risus varius blandit sit amet non
			  	&#8230;<a href="post.html" class="read-more">More</a></p>
			</div>
						
		</div>
	</div>
    	
	<!-- Start Outter Wrapper -->
	<div id="homepagecontactus" class="outter-wrapper footer-wrapper">		
		<div class="wrapper clearfix">
			<!-- Start Widget -->
			<div class="col-1-4 widget">
				<h3 class="widget-title">Contact Us</h3>
				<ul class="list-1">
					<li>
						<strong>Telephone Enquiry:</strong><br/>  
					   +61 555 555 1234
					</li>
					<li>
						<strong>Facsimile:</strong><br/>  
						+61 555 555 1234
					</li>
					<li><a href="#">info@sports.com</a></li>	
				</ul>	
			</div>
			
			
			
			<!-- Start Widget -->
			<div class="col-1-4 widget">
				<h3 class="widget-title">Our Mission</h3>
				<p>Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Maecenas sed diam eget risus varius blandit sit amet non magna. Fusce dapibus.</p>
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
					<img src="/portals/_default/Skins/GenericSportSkin/images/logo-white.png" alt="Sport" />
				</a>
				<ul class="list-1">
					<li>
	    				<strong>Address:</strong><br/>  
	    				310 Ashfield Ave, Suburbia, Brisbane, Australia, 4000
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
			<div class="left">Powered By <img src="/portals/_default/Skins/GenericSportSkin/images/footer_logo.png" style="width: 30px;" /><a href="http://www.hummingbird-infotech.com" target="_blank">HummingBird Infotech</a></div>
			
			<!-- Social Icons -->
			<ul class="social-links right">
				<li><a class="fa" title="Facebook" href="#">&#xf09a;</a></li>
				<li><a class="fa" title="Twitter" href="#">&#xf099;</a></li>
				<li><a class="fa" title="Google Plus" href="#">&#xf0d5;</a></li>
			</ul>
		</div>
	</div>
