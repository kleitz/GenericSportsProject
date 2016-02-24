<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="MENU" src="~/DesktopModules/DDRMenu/Menu.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Meta" Src="~/Admin/Skins/Meta.ascx" %> 

<!-- Adding CSS files into Skin -->
 
<dnn:DnnCssInclude runat="server" FilePath="css/styles.css" PathNameAlias="SkinPath"/>

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
<dnn:DnnCssInclude runat="server" FilePath="css/skin2.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/full.css" PathNameAlias="SkinPath"/>
<dnn:DnnCssInclude runat="server" FilePath="css/normalize.min.css" PathNameAlias="SkinPath"/>
 
 <dnn:DnnJsInclude runat="server" FilePath="js/vendor/modernizr-2.6.2-respond-1.1.0.min.js" PathNameAlias="SkinPath" />
	
<!-- Start Scripts --> 
<dnn:DnnJsInclude runat="server" FilePath="js/rs-plugin/js/jquery.themepunch.tools.min.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude runat="server" FilePath="js/rs-plugin/js/jquery.themepunch.revolution.min.js" PathNameAlias="SkinPath" />
	
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

<dnn:DnnJsInclude runat="server" FilePath="js/main.js" PathNameAlias="SkinPath" />



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
    	
	<!-- Post Header Area -->
 <div id="ContentPane" runat="server"></div>
	
	<!-- Login Start -->

		<div class="outter-wrapper nav-container post-header-area header-style-3">
			<div id="navigation" class="clearfix">
				<div class="post-header-center centered-menu">
					<div class="nav megamenu_container wrapper">
						<dnn:MENU id="newmenu" runat="server" MenuStyle="DNNMobileNav"></dnn:MENU>
					
					</div>
						<dnn:LOGIN ID="dnnLogin" CssClass="LoginLink" runat="server" LegacyMode="false"/>
				</div>
			</div>
		</div>
		
	<!-- Login Close -->
		
	<!-- Revolution Slider -->
	<div id="mainbanner" class="tp-banner-container">
		<div class="tp-banner">
			<ul>
					
				<!-- SLIDE  -->
				<li data-transition="fade" data-masterspeed="500" >
					<!-- MAIN IMAGE -->
					<img src="/portals/_default/Skins/Generic_Sport_Skin/images/mock.jpg" alt="Slider Image 1" data-bgposition="left center" data-kenburns="on" data-duration="14000" data-ease="Linear.easeNone" data-bgfit="100" data-bgfitend="130" data-bgpositionend="right center">
					<!-- LAYERS -->
					<!-- LAYER NR. 1 -->
					<div class="tp-caption fadeout sfr sport-rs-boxed sport-rs-heading"
						data-x="0" data-hoffset="0"
						data-y="155" data-voffset="0"
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
					
					<!-- LAYER NR. 2 -->
					<div class="tp-caption fadeout sfr sport-rs-boxed sport-rs-text"
						data-x="0" data-hoffset="0"
						data-y="280" data-voffset="0"
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
					
					<!-- LAYER NR. 3 -->
					<div class="tp-caption fadeout sfr tp-resizeme"
						data-x="0" data-hoffset="0"
						data-y="400" data-voffset="0"
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
				
				<!-- SLIDE  -->
				<li data-transition="fade" data-masterspeed="500" >
					<!-- MAIN IMAGE -->
					<img src="/portals/_default/Skins/Generic_Sport_Skin/images/mock2.jpg" alt="Slider Image 1" data-bgposition="left center" data-kenburns="on" data-duration="14000" data-ease="Linear.easeNone" data-bgfit="100" data-bgfitend="130" data-bgpositionend="right center">
					<!-- LAYERS -->
					<!-- LAYER NR. 1 -->
					<div class="tp-caption fadeout sfr sport-rs-boxed sport-rs-heading"
						data-x="0" data-hoffset="0"
						data-y="155" data-voffset="0"
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
					<!-- LAYER NR. 2 -->
					<div class="tp-caption fadeout sfr sport-rs-boxed sport-rs-text"
						data-x="0" data-hoffset="0"
						data-y="280" data-voffset="0"
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
					<!-- LAYER NR. 3 -->
					<div class="tp-caption fadeout sfr tp-resizeme"
						data-x="0" data-hoffset="0"
						data-y="400" data-voffset="0"
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
				
				<p><img src="/portals/_default/Skins/Generic_Sport_Skin/images/sig.jpg" alt="Fill" /><br/> <strong>John Doe - Club President</strong></p>
				 					
			</div>
			
			<!-- Column -->
			<div class="col-1-4">
				<h3>In Profile</h3>
				<div class="mosaic-block circle"  style="height: 180px;">
					<a href="#" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
					<div class="corner">Assistant Coach</div><img src="/portals/_default/Skins/Generic_Sport_Skin/images/team-2.jpg" alt="Mock" /></div>
				</div>
				<p>Morbi leo risus, porta ac conse ctetur ac, vestiosite Duis mollis, est non commodo luctus, nisi erat porttitor ligula. Maecenas sed diam eget risus varius
				&#8230;<a href="widgets.html" class="read-more">More</a></p>
			</div>
						
			<!-- Column -->
			<div class="col-1-4 last">
				<h3>Coming Events</h3>
				<ul class="widget-event-list">
					<li>
						<img class="left stay" src="/portals/_default/Skins/Generic_Sport_Skin/images/thumb-3.jpg" alt="mock" />
						<div class="date">15 Aug</div>
						<h6 class="title"><a href="#">Melbourne Sharks VS Our Team - Home Game</a></h6>
					</li>
					<li>
						<img class="left stay" src="/portals/_default/Skins/Generic_Sport_Skin/images/thumb-2.jpg" alt="mock" />
						<div class="date">26 Aug</div>
						<h6 class="title"><a href="#">Quarter Finals - Tigers VS Our Team - Home Game</a></h6>
					</li>
					<li>
						<img class="left stay" src="/portals/_default/Skins/Generic_Sport_Skin/images/thumb-1.jpg" alt="mock" />
						<div class="date">31 Aug</div>
						<h6 class="title"><a href="#">Sydney VS Our Team - Away Game</a></h6>
					</li>
				</ul>
			</div>
		</div>
	</div>



    	
	<!-- Start Outter Wrapper -->
	<div class="outter-wrapper">
		<div class="wrapper ad-padx2 clearfix">
		
			<div class="clearfix">
				<h3 class="left">Latest Photo</h3>
    			<ul class="option-set right" id="filter" data-option-key="filter">
    			 	<li><a href="#show-all" data-option-value="*" class="selected"><em class="fa">&#xf0c9;</em></a></li>
    			 	<li><a href="#cat-1" data-option-value=".cat-1">1st Half</a></li>
    			 	<li><a href="#cat-2" data-option-value=".cat-2">2nd Half</a></li>
    			</ul>
    		</div>	
			
			<!-- Start Isotope -->
			<div class="col-1-1 thumb-gallery super-list variable-sizes clearfix" id="thumb-gallery">
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-1">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-3.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-3.jpg" alt="Mock" /></div>
					
					</div>
					<h6 class="title"><a href="post.html">Pre season camp success</a></h6>
						
    	   		</div>
    	   		
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-1">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-4.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-4.jpg" alt="Mock" /></div></div><h6 class="title"><a href="post.html">Pre season camp success</a></h6>
    	   		</div>
    	   		
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-2">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-5.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-5.jpg" alt="Mock" /></div></div><h6 class="title"><a href="post.html">Pre season camp success</a></h6>
    	   		</div>
    	   		
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-1">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-6.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-6.jpg" alt="Mock" /></div></div><h6 class="title"><a href="post.html">Pre season camp success</a></h6>
    	   		</div>
    	   		
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-2">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-7.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-7.jpg" alt="Mock" /></div></div><h6 class="title"><a href="post.html">Pre season camp success</a></h6>
    	   		</div>
    	   		
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-1">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-8.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-8.jpg" alt="Mock" /></div></div><h6 class="title"><a href="post.html">Pre season camp success</a></h6>
    	   		</div>
    	   		
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-2">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-9.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-9.jpg" alt="Mock" /></div></div><h6 class="title"><a href="post.html">Pre season camp success</a></h6>
    	   		</div>
    	   		
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-1">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-10.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-10.jpg" alt="Mock" /></div></div><h6 class="title"><a href="post.html">Pre season camp success</a></h6>
    	   		</div>
    	   		
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-2">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-11.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-11.jpg" alt="Mock" /></div></div><h6 class="title"><a href="post.html">Pre season camp success</a></h6>
    	   		</div>
    	   		
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-1">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-12.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-12.jpg" alt="Mock" /></div></div><h6 class="title"><a href="post.html">Pre season camp success</a></h6>
    	   		</div>
    	   		
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-2">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-1.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-1.jpg" alt="Mock" /></div></div><h6 class="title"><a href="post.html">Pre season camp success</a></h6>
    	   		</div>
    	   		
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-1">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-3.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-3.jpg" alt="Mock" /></div></div><h6 class="title"><a href="post.html">Pre season camp success</a></h6>
    	   		</div>
    	   		
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-2">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-6.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-6.jpg" alt="Mock" /></div></div><h6 class="title"><a href="post.html">Pre season camp success</a></h6>
    	   		</div>
    	   		
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-1">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-2.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-2.jpg" alt="Mock" /></div></div><h6 class="title"><a href="post.html">Pre season camp success</a></h6>
    	   		</div>
    	   		
    	   		<!-- Start Thumbnail -->
    	   		<div class="col-1-5 element cat-2">
    	   			<div class="mosaic-block circle"><a href="/portals/_default/Skins/Generic_Sport_Skin/images/fill-8.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
    	   			<div class="mosaic-backdrop"><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-8.jpg" alt="Mock" /></div></div><h6 class="title"><a href="post.html">Pre season camp success</a></h6>
    	   		</div>
    	   		
    	   </div>
			
		</div>
	</div>
	
	<!-- Start Outter Wrapper -->

	<!-- Start Outter Wrapper -->
	<div id="latestnew" class="outter-wrapper feat-block-1">
		<div class="wrapper ad-pad clearfix">
		
				<h1>Featured Club News</h1>
				<!-- Start Carousel -->
				<div class="owl-carousel-container">
					<div id="carousel-1" class="owl-carousel">

					  <!-- Carousel Item -->	
					  <div class="item">
		    			  	<div class="mosaic-block circle"  style="height:180px">
		    			  		<a href="post.html" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
		    			  		<div class="corner date">16 Aug</div><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-4.jpg" alt="Mock" /></div>
		    			  	</div>
		    			  	
		    			  	<h6 class="title"><a href="post.html">Pre season camp success</a></h6>
		    			  	<p>Morbi leo risus, porta ac conse ctetur ac, vestiosite Duis mollis, est non commodo luctus, nisi erat porttitor ligula soren
		    			  	&#8230;<a href="post.html" class="read-more">More</a></p>
					  </div>
					  
					  <!-- Carousel Item -->	
					  <div class="item">
					  	  	<div class="mosaic-block circle"   style="height:180px">
					  	  		<a href="post.html" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
					  	  		<div class="corner date">26 Aug</div><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-3.jpg" alt="Mock" /></div>
					  	  	</div>
					  	  	
					  	  	<h6 class="title"><a href="post.html">Nominated club of the year</a></h6>
					  	  	<p>Duis mollis, est non commodo luctus, nisi erat porttitor ligula. Maecenas sed diam eget risus varius blandit sit amet non
					  	  	&#8230;<a href="post.html" class="read-more">More</a></p>
					  </div>
					  
					  <!-- Carousel Item -->	
					  <div class="item">
					  	  	<div class="mosaic-block circle"  style="height:180px" >
					  	  		<a href="post.html" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
					  	  		<div class="corner date">17 Sept</div><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-5.jpg" alt="Mock" /></div>
					  	  	</div>
					  	  	
					  	  	<h6 class="title"><a href="post.html">Road to the grand finals</a></h6>
					  	  	<p>Vestiosite Duis mollis, est non commodo luctus, nisi erat porttitor sed diam eget risus varius blandit sit amet non magna
					  	  	&#8230;<a href="post.html" class="read-more">More</a></p>
					  </div>
					  
					  <!-- Carousel Item -->	
					  <div class="item">
					  	  	<div class="mosaic-block circle"  style="height:180px" >
					  	  		<a href="post.html" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
					  	  		<div class="corner date">24 Sept</div><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-2.jpg" alt="Mock" /></div>
					  	  	</div>
					  	  	
					  	  	<h6 class="title"><a href="post.html">Club facilities upgrade</a></h6>
					  	  	<p>Mollis, est non commodo luctus, nisi erat porttitor ligula. Maecenas sed diam eget risus varius blandit sit amet non
					  	  	&#8230;<a href="post.html" class="read-more">More</a></p>
					  </div>
					  
					  <!-- Carousel Item -->	
					  <div class="item">
					  	  	<div class="mosaic-block circle"   style="height:180px">
					  	  		<a href="post.html" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
					  	  		<div class="corner date">26 Aug</div><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-11.jpg" alt="Mock" /></div>
					  	  	</div>
					  	  	
					  	  	<h6 class="title"><a href="post.html">Pre game rundowns</a></h6>
					  	  	<p>Porta ac conse ctetur ac, vestiosite Duis mollis, est non commodo luctus, nisi erat porttitor ligula maecenas sed diam
					  	  	&#8230;<a href="post.html" class="read-more">More</a></p>
					  </div>
					  
					  <!-- Carousel Item -->	
					  <div class="item">
					  	  	<div class="mosaic-block circle" style="height:180px">
					  	  		<a href="post.html" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
					  	  		<div class="corner date">26 Aug</div><img src="/portals/_default/Skins/Generic_Sport_Skin/images/fill-8.jpg" alt="Mock" /></div>
					  	  	</div>
					  	  	
					  	  	<h6 class="title"><a href="post.html">Divisions and Coaching</a></h6>
					  	  	<p>Conse ctetur ac, vestiosite Duis mollis, est non commodo luctus, nisi erat porttitor ligula. Maecenas sed diam eget dolen
					  	  	&#8230;<a href="post.html" class="read-more">More</a></p>
					  </div>
		
		    		</div>
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
