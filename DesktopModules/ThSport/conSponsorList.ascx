<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="conSponsorList.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.conSponsorList" %>




<div class="breadcrumbs">
            <ul>
                <li class="home"><asp:HyperLink id="titela" runat="server"></asp:HyperLink> </li>
                <li><asp:Label ID="titel" runat="server" ></asp:Label></li>
               
            </ul>
</div>


<panel id="pnlCompetitionStandings" runat="server" >
    <center>
       
    
    <header class="pix-heading-title">
	    <h2 class="pix-section-title heading-color">
			<asp:Label runat="server" ID="lblTeamTitle" Text="Our Sponsors"></asp:Label>
		</h2>
	</header>
        
    <div class="outter-wrapper body-wrapper" >
				
	<%--	<div class="wrapper ad-pad clearfix">--%>
		
			<!-- Start Main Column  -->
			<div class="col-1-1">
				<div class="col-3-4 last thumb-gallery super-list variable-sizes clearfix" id="thumb-gallery" style="margin-bottom: 100px;left:-4px;margin-top:10px;">
					<asp:Repeater ID="SponsorList" runat="server">
				    	<%--<HeaderTemplate>
						
						</HeaderTemplate>--%>
						 <ItemTemplate>
							<div class="col-1-4 element cat-1" >
								<div class="mosaic-block circle">
									<div class="mosaic-backdrop">
									<a  href='<%# Page.ResolveUrl("~/DesktopModules/ThSport/" + Eval("SponsorLogoFile") + "") %>' data-title="Insert Title" class="mosaic-overlay fancybox" data-fancybox-group="gallery" ></a>

                                        <asp:Image ID="Image" alt="" runat="server" ImageUrl='<%#Eval("SponsorLogoFile") %>'></asp:Image>

									</div>
								</div>

							</div>
							
						</ItemTemplate>
                        <AlternatingItemTemplate>
							<div class="col-1-4 element cat-2" >
								<div class="mosaic-block circle">

									<div class="mosaic-backdrop">
                                    <a  href='<%# Page.ResolveUrl("~/DesktopModules/ThSport/" + Eval("SponsorLogoFile") + "") %>' data-title="Insert Title" class="mosaic-overlay fancybox" data-fancybox-group="gallery" ></a>

                                        <asp:Image ID="Image" alt="" runat="server" ImageUrl='<%#Eval("SponsorLogoFile") %>'></asp:Image>

									</div>
								</div>

							</div>
							
						</AlternatingItemTemplate>
    					<%--<FooterTemplate>
							
						</FooterTemplate>--%>
	                </asp:Repeater>
                   </div>
                </div>
            </div>
       <%-- </div>--%>
				    
   </center> 
</panel> 
