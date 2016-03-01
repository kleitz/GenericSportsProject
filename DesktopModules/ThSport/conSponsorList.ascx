<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="conSponsorList.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.conSponsorList" %>



<div class="outter-wrapper body-wrapper">
				
		<div class="wrapper ad-pad clearfix">
		
			<!-- Start Main Column  -->
			<div class="col-1-1">
				
				<!-- Start Gallery Section -->
				<div class="clearfix">
				
						
						
					<!-- Start Isotope -->
					<div class="col-3-4 last thumb-gallery super-list variable-sizes clearfix" id="thumb-gallery">
						<!-- Start Thumbnail -->
						<div class="col-1-3 element cat-1">
							<div class="mosaic-block circle"><a href="img/fill-3.jpg" class="mosaic-overlay fancybox" data-fancybox-group="gallery" title="Insert Title"></a>
							<div class="mosaic-backdrop"><img src="img/fill-3.jpg" alt="Mock" /></div></div>
						</div>
						
							
					<!-- End Isotope -->		
					</div>

				</div>
			</div>
			
		</div>
	</div>
    	
<script type="text/javascript">
    $(document).ready(function () {
        $('.image-popup-no-margins').magnificPopup({
            type: 'image',
            closeOnContentClick: true,
            closeBtnInside: false,
            fixedContentPos: true,
            mainClass: 'mfp-no-margins mfp-with-zoom', // class to remove default margin from left and right side
            image: {
                verticalFit: true
            },
            zoom: {
                enabled: true,
                duration: 300 // don't foget to change the duration also in CSS
            }
        });
    });
</script>

<style type="text/css">
/* padding-bottom and top for image */
.mfp-no-margins img.mfp-img {
	padding: 0;
}
/* position of shadow behind the image */
.mfp-no-margins .mfp-figure:after {
	top: 0;
	bottom: 0;
}
/* padding for main container */
.mfp-no-margins .mfp-container {
	padding: 0;
}


/* 

for zoom animation 
uncomment this part if you haven't added this code anywhere else

*/


.mfp-with-zoom .mfp-container,
.mfp-with-zoom.mfp-bg {
	opacity: 0.001; 
	-webkit-backface-visibility: hidden;
	-webkit-transition: all 0.3s ease-out; 
	-moz-transition: all 0.3s ease-out; 
	-o-transition: all 0.3s ease-out; 
	transition: all 0.3s ease-out;
}

.mfp-with-zoom.mfp-ready .mfp-container {
		opacity: 1;
}
.mfp-with-zoom.mfp-ready.mfp-bg {
		opacity: 0.8;
}

.mfp-with-zoom.mfp-removing .mfp-container, 
.mfp-with-zoom.mfp-removing.mfp-bg {
	opacity: 0;
}

</style>


<div class="breadcrumbs">
            <ul>
                <li class="home"><asp:HyperLink id="titela" runat="server"></asp:HyperLink> </li>
                <li><asp:Label ID="titel" runat="server" ></asp:Label></li>
               
            </ul>
        </div>


<panel id="pnlCompetitionStandings" runat="server">
<center>
       
    
    <header class="pix-heading-title">
	    <h2 class="pix-section-title heading-color">
			<asp:Label runat="server" ID="lblTeamTitle" Text="Our Sponsors"></asp:Label>
		</h2>
	</header>
        
    <div class="TeamAllDetail-Tabs">
				<div class="TeamAllDetail-TabContainer">

                <div class="element_size_100 page_listing">
			    	<div class="gallerysec gallery">
					    <asp:Repeater ID="SponsorList" runat="server">
				    		<HeaderTemplate>
							<ul class="gallery-three-col lightbox clearfix">
						</HeaderTemplate>
						    <ItemTemplate>
							<li class="video-gallery-img">
								<figure>
									<asp:Image ID="Image" alt="" runat="server" ImageUrl='<%#Eval("SponsorLogoFile") %>' Height="218px"></asp:Image>
									<figcaption>
											<a data-rel="prettyPhoto" href='<%# Page.ResolveUrl("~/DesktopModules/SportSite/" + Eval("SponsorLogoFile") + "") %>' data-title="" rel="prettyPhoto[gallery1]">
											<i class="fa fa-plus"></i>                            </a>
									</figcaption>
								</figure>

							</li>
							
						</ItemTemplate>
    						<FooterTemplate>
							</ul>
						</FooterTemplate>
	    				</asp:Repeater>
				    </div>
		    	</div>
            </div>
        </div> 
   </center> 
</panel> 
