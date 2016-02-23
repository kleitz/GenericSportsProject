<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="MENU" src="~/DesktopModules/DDRMenu/Menu.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Meta" Src="~/Admin/Skins/Meta.ascx" %> 

    	<dnn:MENU id="newmenu" runat="server" MenuStyle="DNNMobileNav"></dnn:MENU>
    	<dnn:LOGIN ID="dnnLogin" CssClass="LoginLink" runat="server" LegacyMode="false"/>
		
		
	<!-- Pre Header Area -->
	<div class="outter-wrapper pre-header-area header-style-3">
		<div class="wrapper clearfix">
		
			<div class="pre-header-left left">
				<!-- Second Nav -->
				<ul>
					<li><a href="<%= SkinPath %>index.html">Home</a></li>
					<li><a href="<%= SkinPath %>sitemap.html">Pages</a></li>
					<li><a href="<%= SkinPath %>styles.html">Typography</a></li>
				</ul>
			</div>
		
			<div class="pre-header-right right">
				<ul class="social-links boxy">
					<li><a class="fa" title="Facebook" href="<%= SkinPath %>#">&#xf09a;</a></li>
					<li><a class="fa" title="Twitter" href="<%= SkinPath %>#">&#xf099;</a></li>
					<li><a class="fa" title="Google Plus" href="<%= SkinPath %>#">&#xf0d5;</a></li>
					<li><a class="fa" title="RSS" href="<%= SkinPath %>#">&#xf09e;</a></li>
				</ul>
			</div>
		
		</div>	
	</div> 
    			
    			

		
		
	<!-- Header Area -->
	<div class="outter-wrapper header-area header-style-3">
		<div class="wrapper clearfix logo-container">
			<header>
				<div class="clearfix">
    				
					<!-- Start Logo -->
					<a class="logo centered" href="<%= SkinPath %>index.html">
						<img src="<%= SkinPath %>img/logo-white.png" alt="Sport" />
					</a>

				</div>
			</header>
		</div>
	</div>
		
    	
	<!-- Post Header Area -->
	<div class="outter-wrapper nav-container post-header-area header-style-3">
	
		<!-- Start Mobile Menu Icon -->
		<div id="mobile-header" class="">
			<a id="responsive-menu-button" href="<%= SkinPath %>#sidr-main">
				<em class="fa fa-bars"></em> Menu
			</a>
		</div>
		
    	<div id="navigation" class="clearfix">

	

			<div class="post-header-center centered-menu">
				
				<nav class="nav megamenu_container wrapper">
				    <ul id="nav" class="centered-menu megamenu">
					    <li class="nav-parent"><a href="<%= SkinPath %>#">Home Pages</a>
					    	<ul>
					    		<li><a href="<%= SkinPath %>index.html">Home 1 - Ken Burns Slider</a></li>
					    		<li><a href="<%= SkinPath %>index-2.html">Home 2 - Full Screen Slider</a></li>
					    		<li><a href="<%= SkinPath %>index-3.html">Home 3 - Video Header</a></li>
					    		<li><a href="<%= SkinPath %>index-4.html">Home 4 - Boxed Slider</a></li>
					    		 
					    	</ul>	
					    </li>
					    <li class="nav-parent">
					    	<a href="<%= SkinPath %>#">Feature Pages</a>
					    	<ul>
					    		<li><a href="<%= SkinPath %>about.html">Standard Pages &nbsp; <em class="fa">&#xf105;</em></a>
					    			<ul>
					    				<li><a href="<%= SkinPath %>about.html">Standard Feature</a></li>
					    				<li><a href="<%= SkinPath %>full-feature.html">Full Feature</a></li>
					    				<li><a href="<%= SkinPath %>parallax-feature.html">Parallax Feature</a></li>
						    			<li><a href="<%= SkinPath %>no-feature.html">No Feature Page</a></li>
					    				<li><a href="<%= SkinPath %>full-content.html">Full Content</a></li>
					    				 
					    			</ul>
					    		</li>
					    		
					    		<li><a href="<%= SkinPath %>galleries.html">Our Galleries &nbsp; <em class="fa">&#xf105;</em></a>
					    			<ul>
					    				<li><a href="<%= SkinPath %>gallery-1.html">Isotope Gallery</a></li>
					    				<li><a href="<%= SkinPath %>gallery-2.html">Slider Gallery</a></li>
					    				<li><a href="<%= SkinPath %>gallery-3.html">Singles Gallery</a></li>
					    				<li><a href="<%= SkinPath %>gallery-4.html">Masonry Gallery</a></li>
					    			</ul>
					    		</li>
					    		<li><a href="<%= SkinPath %>team.html">Team Page</a></li>
					    		<li><a href="<%= SkinPath %>member.html">Member Page</a></li>
					    		<li><a href="<%= SkinPath %>event-calender.html">Events Calendar</a></li>
					    		<li><a href="<%= SkinPath %>event.html">Single Event</a></li>
					    		<li><a href="<%= SkinPath %>timetable.html">TimeTable</a></li>
					    		<li><a href="<%= SkinPath %>leaderboard.html">Points Leader Board</a></li>
					    		<li><a href="<%= SkinPath %>faq.html">Frequent Questions</a></li>
					    		<li><a href="<%= SkinPath %>pricing1.html">Pricing Table 1</a></li>
					    		<li><a href="<%= SkinPath %>pricing2.html">Pricing Table 2</a></li>
					    		<li><a href="<%= SkinPath %>contact.html">Contact</a></li>
					    		<li><a href="<%= SkinPath %>contact.php">Contact with Captcha</a></li>
					    		<li><a href="<%= SkinPath %>sitemap.html">Sitemap</a></li>
					    		<li><a href="<%= SkinPath %>404.html">404 Page</a></li>
					    		<li><a href="<%= SkinPath %>results.html">Search Results</a></li>
					    		<li><a href="<%= SkinPath %>styles.html">Typography</a></li>
					    		<li><a href="<%= SkinPath %>widgets.html">Available Widgets</a></li>
					    		
					    	</ul>
					    </li>
					    
					    
					    <li><a href="<%= SkinPath %>#" class="megamenu_drop">Mega Menu <em class="fa">&#xf107;</em></a><!-- Begin Mega Item -->
					    <div class="dropdown_fullwidth"><!-- Begin Item Container -->
		                    
		                    <div class="clearfix">
		                    	<div class="col-2-3 clearfix">
		                    		<h3 class="mega-title">Featured post</h3>
		                    		<a class="col-1-2" href="<%= SkinPath %>post.html"><img src="<%= SkinPath %>img/fill-3.jpg" alt="Mock" /></a>
		                    		<h6 class="title"><a href="<%= SkinPath %>post.html">Nominated club of the year</a></h6>
		                    		<p>Sed posuere consectetur est at lobortis. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis
		                    		&#8230;<a href="<%= SkinPath %>post.html" class="read-more">More</a></p>
		                    	</div>
		                    	
		                    	<div class="col-1-3 last">
		                    		<h3 class="mega-title">Quick Links</h3>
		                    		<ul class="list-2 widget-list">
		                    			<li><a href="<%= SkinPath %>index.html">Home Page</a></li>
		                    			<li><a href="<%= SkinPath %>event-calender.html">Events Calendar</a></li>
		                    			<li><a href="<%= SkinPath %>team.html">Our Team</a></li>
		                    			<li><a href="<%= SkinPath %>pricing1.html">Join Our Club</a></li>
		                    			<li><a href="<%= SkinPath %>blog-rsb.html">Our Blog</a></li>
		                    			<li><a href="<%= SkinPath %>contact.php">Contact Us</a></li>
		                    		</ul>
		                    	</div>
		                    </div>

	                    </div><!-- End Item Container -->
	                    </li><!-- End Mega Item -->
					                
					                
					                
					    <li class="nav-parent">
					    	<a href="<%= SkinPath %>#">Header Variations</a>
					    	<ul>
					    		<li><a href="<%= SkinPath %>header-1.html">Header Layout 1</a></li>
					    		<li><a href="<%= SkinPath %>header-2.html">Header Layout 2</a></li>
					    		<li><a href="<%= SkinPath %>header-3.html">Header Layout 3</a></li>
					    		<li><a href="<%= SkinPath %>header-4.html">Header Layout 4</a></li>
					    		<li><a href="<%= SkinPath %>header-5.html">Header Layout 5</a></li>
					    		<li><a href="<%= SkinPath %>header-1-logotext.html">Logo as text option</a></li>
						    		<li><a href="<%= SkinPath %>header-sticky.html">Sticky Header</a></li>
					    	</ul>	
					    </li>
					    <li class="nav-parent">
					    	<a href="<%= SkinPath %>#">Blog Options</a>
					    	<ul>
					    		<li><a href="<%= SkinPath %>blog-rsb.html">Blog Right Sidebar</a></li>
					    		<li><a href="<%= SkinPath %>blog-lsb.html">Blog Left Sidebar</a></li>
					    		<li><a href="<%= SkinPath %>blog.html">Blog Full Width</a></li>
					    		<li><a href="<%= SkinPath %>blog-rlsb.html">Blog Double Sidebars</a></li>
					    		<li><a href="<%= SkinPath %>blog-double.html">Blog Two Columns</a></li>
					    		<li><a href="<%= SkinPath %>blog-double-rsb.html">Blog Two Columns Sidebar</a></li>
					    		<li><a href="<%= SkinPath %>post.html">Single Post</a></li>
					    		<li><a href="<%= SkinPath %>author.html">Author Page</a></li>
					    	</ul>		
					    </li>
					</ul>
				</nav>

			</div>

    	</div>
	</div>
    	    	
    	
    	
    	
   		
	<!-- Revolution Slider -->
	<div class="tp-banner-container">
		<div class="tp-banner" >
			<ul>
				
				<li data-transition="slidehorizontal" data-slotamount="1" data-masterspeed="1000"  data-fstransition="fade" data-fsmasterspeed="1000"  data-saveperformance="off">
					<!-- MAIN IMAGE -->
					<img src="<%= SkinPath %>img/media/Sprinkle.jpg"  alt="Title"  data-bgposition="left center" data-bgfit="cover" data-bgrepeat="no-repeat">
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
				
						<video controls style="width: 100%; height: 100%" poster="img/media/Sprinkle.jpg" loop>
						<source src="img/media/Sprinkle.webm"  type='video/webm;codecs="vp8, vorbis"' />
						<source src="img/media/Sprinkle.mp4"   type='video/mp4;codecs="avc1.42E01E, mp4a.40.2"' />
						<source src="img/media/Sprinkle.ogv" type='video/ogg; codecs="theora, vorbis"'>
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
						<a class="btn" href="<%= SkinPath %>pricing1.html">Join Our Club Today</a>&nbsp; <a class="btn-2" href="<%= SkinPath %>about.html">Read More</a>
					</div>	
				</li>
				

			</ul>
		</div>
	</div>
   				
    	
	<!-- Start Outter Wrapper -->
	<div class="outter-wrapper body-wrapper">		
		<div class="wrapper ad-pad clearfix">
			
			<!-- Column -->
			<div class="col-1-2">
				<h3>Welcome To Our Club</h3>
				
				<p class="lead">Donec ullamcorper nulla non metus auctor fringilla. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
				
				<p>Morbi leo risus, porta ac conse ctetur ac, vestiosite Duis mollis, est non commodo luctus, nisi erat porttitor ligula est non commodo sed diam. Donec id elit non mi porta gravida at eget metus. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Sed posuere consectetur est at lobortis. Curabitur blandit tempus porttitor. Nullam id dolor id nibh ultricies vehicula ut id elit. Fusce dapibus, tellus ac cursus commodo.</p> 
				
				<p><img src="<%= SkinPath %>img/sig.jpg" alt="Fill" /><br/> <strong>John Doe - Club President</strong></p>
				 					
			</div>
			
			
			
			<!-- Column -->
			<div class="col-1-4">
				<h3>In Profile</h3>
				<div class="mosaic-block circle">
					<a href="<%= SkinPath %>#" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
					<div class="corner">Assistant Coach</div><img src="<%= SkinPath %>img/team-2.jpg" alt="Mock" /></div>
				</div>
				<p>Morbi leo risus, porta ac conse ctetur ac, vestiosite Duis mollis, est non commodo luctus, nisi erat porttitor ligula. Maecenas sed diam eget risus varius
				&#8230;<a href="<%= SkinPath %>widgets.html" class="read-more">More</a></p>
			</div>
			

			
			
			<!-- Column -->
			<div class="col-1-4 last">
				<h3>Coming Events</h3>
				<ul class="widget-event-list">
					<li>
						<img class="left stay" src="<%= SkinPath %>img/thumb-3.jpg" alt="mock" />
						<div class="date">15 Aug</div>
						<h6 class="title"><a href="<%= SkinPath %>#">Melbourne Sharks VS Our Team - Home Game</a></h6>
					</li>
					
					<li>
						<img class="left stay" src="<%= SkinPath %>img/thumb-2.jpg" alt="mock" />
						<div class="date">26 Aug</div>
						<h6 class="title"><a href="<%= SkinPath %>#">Quarter Finals - Tigers VS Our Team - Home Game</a></h6>
					</li>
					
					<li>
						<img class="left stay" src="<%= SkinPath %>img/thumb-1.jpg" alt="mock" />
						<div class="date">31 Aug</div>
						<h6 class="title"><a href="<%= SkinPath %>#">Sydney VS Our Team - Away Game</a></h6>
					</li>
				</ul>
						
			</div>
			
			
		</div>
	</div>
    	
    	
    	
    
    <!-- Start Outter Wrapper -->
    <div class="outter-wrapper divider"></div>	
    	
    	
    	
	<!-- Start Outter Wrapper -->
	<div class="outter-wrapper centered feat-block-1">
		<div class="wrapper ad-pad clearfix">
		
			<!-- Start Carousel -->
			<div class="owl-carousel-container ">
    			<!-- Carousel Nav -->
    			<div class="customNavigation">
    			    <a class="prev"></a>
    			    <a class="next"></a>
    			</div>
			
    			<div id="carousel-single" class="owl-carousel carousel-single">

	    			  <!-- Carousel Item -->	
	    			  <div class="item col-2-3 nofloat">
		    			  	<blockquote>“An athlete cannot run with money in his pockets. He must run with hope in his heart and dreams in his head ”
		    			  	<cite>Emil Zatopek</cite>
		    			  	</blockquote>
	    			  </div>
	    			  
	    			  <!-- Carousel Item -->	
	    			  <div class="item col-2-3 nofloat">
	    			  	  	<blockquote>"Sportsmanship for me is when a guy walks off the field and you really can't tell whether he won or lost, when he carries himself with pride either way." <cite>- Coach John Doe</cite></blockquote>
	    			  </div>
	    			  
	    			  <!-- Carousel Item -->	
	    			  <div class="item col-2-3 nofloat">
	    			  	  	<blockquote>“There may be people that have more talent than you, but theres no excuse for anyone to work harder than you do ”
	    			  	  	<cite>Derek Jeter</cite>
	    			  	  	</blockquote>
	    			  </div>

	    		</div>
			</div>
						
						
			
		</div>
	</div>
	


	
	<!-- Start Outter Wrapper -->
	<div class="outter-wrapper divider"></div>
    	



	<!-- Start Outter Wrapper -->
	<div class="outter-wrapper">
		<div class="wrapper ad-pad clearfix">
			
			<h3>Latest Club News</h3>
			
			<div class="col-1-4">
			  	<div class="mosaic-block circle">
			  		<a href="<%= SkinPath %>post.html" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
			  		<div class="corner date">16 Aug</div><img src="<%= SkinPath %>img/fill-4.jpg" alt="Mock" /></div>
			  	</div>
			  	
			  	<h6 class="title"><a href="<%= SkinPath %>post.html">Pre season camp success</a></h6>
			  	<p>Morbi leo risus, porta ac conse ctetur ac, vestiosite Duis mollis, est non commodo luctus, nisi erat porttitor ligula soren
			  	&#8230;<a href="<%= SkinPath %>post.html" class="read-more">More</a></p>
			</div>
			
				
			<div class="col-1-4">
			  	<div class="mosaic-block circle">
			  		<a href="<%= SkinPath %>post.html" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
			  		<div class="corner date">26 Aug</div><img src="<%= SkinPath %>img/fill-3.jpg" alt="Mock" /></div>
			  	</div>
			  	
			  	<h6 class="title"><a href="<%= SkinPath %>post.html">Nominated club of the year</a></h6>
			  	<p>Duis mollis, est non commodo luctus, nisi erat porttitor ligula. Maecenas sed diam eget risus varius blandit sit amet non
			  	&#8230;<a href="<%= SkinPath %>post.html" class="read-more">More</a></p>
			</div>
			
				
			<div class="col-1-4">
			  	<div class="mosaic-block circle">
			  		<a href="<%= SkinPath %>post.html" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
			  		<div class="corner date">17 Sept</div><img src="<%= SkinPath %>img/fill-5.jpg" alt="Mock" /></div>
			  	</div>
			  	
			  	<h6 class="title"><a href="<%= SkinPath %>post.html">Road to the grand finals</a></h6>
			  	<p>Vestiosite Duis mollis, est non commodo luctus, nisi erat porttitor sed diam eget risus varius blandit sit amet non magna
			  	&#8230;<a href="<%= SkinPath %>post.html" class="read-more">More</a></p>
			</div>
			
			
			<div class="col-1-4 last">
			  	<div class="mosaic-block circle">
			  		<a href="<%= SkinPath %>post.html" class="mosaic-overlay fancybox link" title="Insert Your Title"></a><div class="mosaic-backdrop">
			  		<div class="corner date">24 Sept</div><img src="<%= SkinPath %>img/fill-2.jpg" alt="Mock" /></div>
			  	</div>
			  	
			  	<h6 class="title"><a href="<%= SkinPath %>post.html">Club facilities upgrade</a></h6>
			  	<p>Mollis, est non commodo luctus, nisi erat porttitor ligula. Maecenas sed diam eget risus varius blandit sit amet non
			  	&#8230;<a href="<%= SkinPath %>post.html" class="read-more">More</a></p>
			</div>
						
		</div>
	</div>
    	
    	
    	
    	
    	
    	
    	

    	
    	
    	
    <!-- Start Outter Wrapper -->
    <div class="outter-wrapper breadcrumb-wrapper">		
    	<div class="wrapper">
    		<a href="<%= SkinPath %>index.html" class="fa">&#xf015;</a> <a href="<%= SkinPath %>index.html">Home</a>
    	</div>
    </div>
    
    	
    	
    	
	<!-- Start Outter Wrapper -->
	<div class="outter-wrapper footer-wrapper">		
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
					<li><a href="<%= SkinPath %>#">info@sports.com</a></li>	
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
				<a class="logo" href="<%= SkinPath %>index.html">
					<img src="<%= SkinPath %>img/logo-white.png" alt="Sport" />
				</a>
				<ul class="list-1">
					<li>
	    				<strong>Address:</strong><br/>  
	    				310 Ashfield Ave, Suburbia, Brisbane, Australia, 4000
					</li>
					<li>
						<strong><a href="<%= SkinPath %>contact.html">View on Map</a></strong>
					</li>
				</ul>	
			</div>
		</div>
	</div>
    	
    	
    	
    	
    	
	<!-- Start Outter Wrapper -->
	<div class="outter-wrapper base-wrapper">
		<div class="wrapper clearfix">
			<div class="left">© Copyright Sport 2014</div>
			
			<!-- Social Icons -->
			<ul class="social-links right">
				<li><a class="fa" title="Facebook" href="<%= SkinPath %>#">&#xf09a;</a></li>
				<li><a class="fa" title="Twitter" href="<%= SkinPath %>#">&#xf099;</a></li>
				<li><a class="fa" title="Google Plus" href="<%= SkinPath %>#">&#xf0d5;</a></li>
			</ul>
		</div>
	</div>
		
		
		
		
	<!-- Load jQuery -->
	<script type="text/javascript" src="<%= SkinPath %>js/vendor/jquery-1.8.3.min.js"></script>
	
	<!-- Start Scripts --> 
	<script type="text/javascript" src="<%= SkinPath %>js/rs-plugin/js/jquery.themepunch.tools.min.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/rs-plugin/js/jquery.themepunch.revolution.min.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/jquery.sidr.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/fancybox/jquery.fancybox.js?v=2.1.4"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/cleantabs.jquery.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/fitvids.min.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/jquery.scrollUp.min.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/owl-carousel/owl.carousel.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/selectivizr-min.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/placeholder.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/jquery.stellar.min.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/mosaic.1.0.1.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/jquery.isotope.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/toggle.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/jquery.tooltipster.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/jquery.countdown.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/jquery.sticky.js"></script>
	<script type="text/javascript" src="<%= SkinPath %>js/slider-3.js"></script>
    
    <script type="text/javascript" src="<%= SkinPath %>js/main.js"></script>
	

				
    <!-- Google Analytics: change UA-XXXXX-X to be your site's ID. -->
    <script>
        (function(b,o,i,l,e,r){b.GoogleAnalyticsObject=l;b[l]||(b[l]=
        function(){(b[l].q=b[l].q||[]).push(arguments)});b[l].l=+new Date;
        e=o.createElement(i);r=o.getElementsByTagName(i)[0];
        e.src='//www.google-analytics.com/analytics.js';
        r.parentNode.insertBefore(e,r)}(window,document,'script','ga'));
        ga('create','UA-XXXXX-X');ga('send','pageview');
    </script>
    
    
    