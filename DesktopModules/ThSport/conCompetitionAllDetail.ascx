<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="conCompetitionAllDetail.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.conCompetitionAllDetail" %>

<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnJsInclude  runat="server" FilePath="~/DesktopModules/ThSport/Js/readmore.min.js" />
<dnn:DnnCssInclude  runat="server" FilePath="~/DesktopModules/ThSport/CSS/jquery.simplyscrollDetail.css"/>
<dnn:DnnCssInclude  runat="server" FilePath="~/DesktopModules/ThSport/CSS/magnific-popup.css" />
<dnn:DnnJsInclude  runat="server" FilePath="~/DesktopModules/ThSport/Js/jquery.magnific-popup.min.js" />

<script type="text/javascript">
    function imgError(image) {
        image.onerror = "";
        image.src = "/DesktopModules/ThSport/Images/no-photo-available.jpg";
        return true;
    }

</script>



<script type="text/javascript">
    $(document).ready(function () {
        $("#competitionTabs").tabs({ hide: { effect: "fadeOut", duration: 300 }, show: { effect: "fadeIn", duration: 300 } });
        $("#competitionTabs").css('min-height', 500);
        var currTab = $("#<%= currentTabIndex.ClientID %>").val();
		$("#competitionTabs").tabs({ active: currTab });
	});
    function imgErrorForTeam(image) {
        image.onerror = "";
        image.src = "\\DesktopModules\\ThSport\\Images\\Team_Logo.png";
        return true;
    }
</script>

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

<script type="text/javascript">var switchTo5x = true;</script>
<script type="text/javascript" src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/JS/buttons.js")%>"></script>
<script type="text/javascript">stLight.options({ publisher: "204bcd9b-f593-474b-9014-06460f154133", doNotHash: false, doNotCopy: false, hashAddressBar: false });</script>

<asp:HiddenField ID="currentTabIndex" runat="server" />

<div class="breadcrumbs">
	<ul>
		<li class="home"><asp:HyperLink id="titela" runat="server"></asp:HyperLink> </li>
		<li><asp:Label ID="titel" runat="server" ></asp:Label></li>
	</ul>
</div>

<panel>
	<center>

		<header class="pix-heading-title">
			<h2 class="pix-section-title heading-color">
				<asp:Label runat="server" ID="lblCompetitionTitle" Text=""/>
		    </h2>
            <div class="right_div_css">
                    <span class='st_facebook_large' displayText='Facebook'></span>
                    <span class='st_twitter_large' displayText='Tweet'></span>
                    <span class='st_instagram_large' displayText='Instagram'></span>
                </div>
		</header>

		<div class="TeamAllDetail-MainContainer" style="padding-top:10px;">
            <div class="TeamAllDetail-tabWrapper">
             <div id="competitionTabs" class="teamTabs">
				<ul style="background-color:black;">
					<li><a href="#competitionTabs1" class="open">Home</a></li>
					<li><a href="#competitionTabs7" class="open">Group & Teams</a></li>
					<li><a href="#competitionTabs2" class="open">Fixtures</a></li>
                    <li><a href="#competitionTabs8" class="open">Results</a></li>
					<li><a href="#competitionTabs4" class="open">News</a></li>
					<li><a href="#competitionTabs5" class="open">Video</a></li>
					<li><a href="#competitionTabs6" class="open">Gallery</a></li>
				</ul>

			    <div id="competitionTabs1" clientidmode="static" class="current" runat="server" style="padding:0px;">
				<div class="CompetitionAllDetail-TabContainer" >
					<div class="CompetitionAllDetail-area justify">
                        
						<div class="CompetitionDetailImage">
							<asp:Image ID="ImgCompetitionPhoto" runat="server" onError="imgError(this);"  CssClass="competitionHistoryImage" Width="400px"/>   
						</div>

						<div class="CompetitionDetailDesc">
							<asp:Literal runat ="server" ID="litCompetitionHistory"/>
						</div>

					</div>
				</div>
			</div>

  	    		<div id="competitionTabs2" clientidmode="static" runat="server" style="padding:0px;">
				<div class="CompetitionAllDetail-TabContainer">
					<center>
						<asp:Label ID="lblNoResultsFinal" runat="server" Text="No data available." CssClass='emptyInfoLabel' Visible="false" ></asp:Label> 
					</center>
				</div>
				<div class="CompetitionAllDetail-TabContainer" id="rptFinal" runat="server" visible="false" clientidmode="Static">
					<div class="event event-listing">
							<center>
                                <h5 class="matchtype_header">
                                    <asp:Label ID="Label7"  runat="server" Text="Final"  />
                                </h5>
							</center>   
			     			<asp:Repeater ID="rptCompetitionSchedulesResultsFinal" runat="server" OnItemDataBound="rptCompetitionSchedulesResultsFinal_ItemDataBound">
								<HeaderTemplate>
								</HeaderTemplate>
								<ItemTemplate>
									<asp:HiddenField ID='hdnStartDate' runat="server" Value='<%#Eval("StartDate") %>' />
									<asp:HiddenField ID='hdnEndDate' runat="server" Value='<%#Eval("EndDate") %>' />
									<asp:HiddenField ID='hdnMatchID' runat="server" Value='<%#Eval("MatchID") %>' />
									<asp:HiddenField ID='hdnMatchResultId' runat="server" Value='<%#Eval("MatchResultId") %>' />
									<asp:Panel ID="SchedulesResultsDatePanel" runat="server">
										<h5>
										    <asp:Literal ID="litSchedulesResultsDate" runat="server" Text='<%#  Eval("StartDate") %>' />
										</h5>
									</asp:Panel>
									<article>
										<div class="calendar-date">
											<asp:Literal ID="litStartDate" runat="server"  />
											@<%#Eval("LocationName") %></div>

										<div class="text" style="width:300px;">
											<div class="top-event">
												<h2 class="pix-post-title">
													<asp:HyperLink CssClass="pix-hover" ID="hlnkTeamAFixtures" runat="server" >
                                                        <asp:Image ID="TeamALogo" runat="server" ImageUrl='<%#Eval("TeamALogo") %>' 
                                                                   BorderStyle="None" onError="imgErrorForTeam(this);"
                                                                   CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX" 
                                                                   AlternateText="" Width="30px" />&nbsp;
														<asp:Label ID="literalTeamAName" runat="server" Text='<%# Eval("TeamAName") %>' />
													</asp:HyperLink>
													<asp:HiddenField ID="hdnTeamAID" runat="server"  Value='<%#Eval("TeamAID")%>' />
												</h2>
											</div>
										</div>

                        <div class="match_result_for_ResultMenu"> 
                            <asp:Panel ID="noShowRegion" Visible="false" runat="server"  CssClass="results_div_panel">        
                                <div class="match-result">
                                   <center>
                                    <span>
                                        <asp:Literal ID="ltrnoshow" runat="server"></asp:Literal>
                                    </span>
                                       </center>
                                </div>
                                </asp:Panel>

                                <asp:Panel ID="penaltyRegion" runat="server" Visible="false">
                                </asp:Panel> 
                                        <div class="match-result results_div_panel">
                                            <center>
                                        <span>
                                           <asp:Label ID="ltrpenalty" runat="server" Text='<%# Eval("TeamApenalty") %>' 
                                                      Visible="false"></asp:Label>
                                        </span>
                                            <br />
                                        <span class="fixturePenalty">
                                            <asp:Label ID="ltrpenaltyText" runat="server" 
                                                       Text="PENALTY" Visible="false"></asp:Label> 
                                        </span>
                                        </div>      
                                            <asp:HiddenField ID="HiddenField1" runat="server"  Value='<%#Eval("TeamApenalty")%>' />
                                            <asp:HiddenField ID="HiddenField2" runat="server"  Value='<%#Eval("TeamBpenalty")%>' />
                                        </div>

                                    <div class="match-result match_result_for_ResultMenu">
                                      <span>
		   								<asp:Panel id="pnlResult" runat="server"  CssClass="results_div_panel">
                                          <asp:HyperLink ID="hprLinkForGame" runat="server" CssClass="hprLinkColor">
											<asp:Label runat="server" ID="lita" Text='<%#Eval("TeamAScore") %>'></asp:Label>
												-
											<asp:Label runat="server" ID="litb" Text='<%#Eval("TeamBScore") %>'></asp:Label>
                                          </asp:HyperLink>
			    					      <asp:HiddenField ID="hidTeamAScoreFixturesCup" runat="server" Value='<%#Eval("TeamAScore")%>' />
										  <asp:HiddenField ID="hidTeamBScoreFixturesCup" runat="server"  Value='<%#Eval("TeamBScore")%>' />
										  <asp:HiddenField ID="hdnNoshow" runat="server"  Value='<%#Eval("IsNoShow")%>' />
										  <asp:HiddenField ID="hdnwinningteam" runat="server" Value='<%#Eval("WinningTeam")%>' />
										  <asp:HiddenField ID="hdnloosingteam" runat="server" Value='<%#Eval("LosingTeam")%>' />
										</asp:Panel>
										<asp:Panel id="pnlFixture" runat="server">
											<div class="match-fixture-text">
												<asp:Literal ID="daysToKick" runat="server" />
											</div>
										</asp:Panel>
                                       </span>
                                     </div>


                                   <div class="match-result match_result_for_ResultMenu results_div_panel">
                                      <center>
                                        <span>
                                            <asp:Label ID="ltrpenaltyB" runat="server" Text='<%# Eval("TeamBpenalty") %>' Visible="false"></asp:Label>
                                        </span>
                                        <br />
                                        <span class="fixturePenalty">
                                            <asp:Label ID="ltrpenaltyTextB" runat="server" Text="PENALTY" Visible="false"></asp:Label> 
                                        </span>
                                      </center>
                                    </div>

                                   <asp:Panel ID="noShowRegionB" Visible="false" runat="server"  CssClass="results_div_panel">        
                                     <div class="match_result_for_ResultMenu">
                                       <center>
                                         <span>
                                              <asp:Literal ID="ltrlnoshowB" runat="server"></asp:Literal>
                                         </span>
                                       </center>
                                     </div>
                                   </asp:Panel>

                                   <div class="text right_div_css">
                                     <div class="top-event">
                                        <h2 class="pix-post-title">
                                           <asp:HyperLink CssClass="pix-hover" ID="hlnkTeamBFixtures" runat="server" >
									     	  <asp:Label ID="literalTeamBName" runat="server" Text='<%# Eval("TeamBName") %>' />
                                              <asp:Image ID="TeamBLogo"  runat="server" ImageUrl='<%#Eval("TeamBLogo") %>' BorderStyle="None" CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX pix_results_img" AlternateText="" 
                                                  Width="30px" onError="imgErrorForTeam(this);" />
									       </asp:HyperLink>
										   <asp:HiddenField ID="hdnTeamBID" runat="server"  Value='<%#Eval("TeamBID")%>' />
                                        </h2>
                                     </div>
                                   </div>
							</article>
								</ItemTemplate>
								<FooterTemplate>
								</FooterTemplate>
							</asp:Repeater>
					  </div>
				</div>

				<div class="CompetitionAllDetail-TabContainer" id="rptSemiFinal" runat="server" visible="false" 
                     clientidmode="Static">
					<div class="event event-listing">
					    <center>
                            <h5 class="matchtype_header">
                                 <asp:Label ID="Label1"  runat="server" Text="Semi Final"  />
                            </h5>
					    </center>   
                        <center>
                            <asp:Label ID="lblNoResultsSemiFinal" runat="server" Text="No data available." CssClass='emptyInfoLabel' Visible="false"></asp:Label>
                        </center>

						<asp:Repeater ID="rptCompetitionSchedulesResultsSemiFinal" runat="server" OnItemDataBound="rptCompetitionSchedulesResultsSemiFinal_ItemDataBound">
							<HeaderTemplate>
							</HeaderTemplate>
							<ItemTemplate>

								<asp:HiddenField ID='hdnStartDate' runat="server" Value='<%#Eval("StartDate") %>' />
								<asp:HiddenField ID='hdnEndDate' runat="server" Value='<%#Eval("EndDate") %>' />
								<asp:HiddenField ID='hdnMatchID' runat="server" Value='<%#Eval("MatchID") %>' />
								<asp:HiddenField ID='hdnMatchResultId' runat="server" Value='<%#Eval("MatchResultId") %>' />
                                    
								<asp:Panel ID="SchedulesResultsDatePanel" runat="server">
							      <h5>
									 <asp:Literal ID="litSchedulesResultsDate" runat="server" Text='<%#  Eval("StartDate") %>' />
							      </h5>
								</asp:Panel>

								<article>

								<div class="calendar-date">
    								<asp:Literal ID="litStartDate" runat="server" />
	    								@<%#Eval("LocationName") %>
		    					</div>

								<div class="text" style="width:300px;">
								    <div class="top-event">
										<h2 class="pix-post-title">
									    	<asp:HyperLink CssClass="pix-hover" ID="hlnkTeamAFixtures" runat="server" >
                                                <asp:Image ID="TeamALogo" runat="server" ImageUrl='<%#Eval("TeamALogo") %>' 
                                                           BorderStyle="None" onError="imgErrorForTeam(this);"
                                                           CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX" 
                                                           AlternateText="" Width="30px" />&nbsp;
											    <asp:Label ID="literalTeamAName" runat="server" 
                                                           Text='<%# Eval("TeamAName") %>' />
										    </asp:HyperLink>
					    					<asp:HiddenField ID="hdnTeamAID" runat="server"  Value='<%#Eval("TeamAID")%>' />
						    			</h2>
									</div>
							    </div>

				
                        <div class="match_result_for_ResultMenu"> 
                            <asp:Panel ID="noShowRegion" Visible="false" runat="server"  CssClass="results_div_panel">        
                                <div class="match-result">
                                  <center>
                                    <span>
                                        <asp:Literal ID="ltrnoshow" runat="server"></asp:Literal>
                                    </span>
                                  </center>
                                </div>
                                </asp:Panel>

                                <asp:Panel ID="penaltyRegion" runat="server" Visible="false">
                                </asp:Panel> 

                                        <div class="match-result results_div_panel">
                                           <center>
                                               <span>
                                                  <asp:Label ID="ltrpenalty" runat="server" Text='<%# Eval("TeamApenalty") %>' 
                                                             Visible="false"></asp:Label>
                                               </span>
                                                <br />
                                               <span class="fixturePenalty">
                                                   <asp:Label ID="ltrpenaltyText" runat="server" Text="PENALTY" Visible="false">
                                                   </asp:Label> 
                                               </span>
                                        </div>
                                  
                                            <asp:HiddenField ID="HiddenField1" runat="server"  Value='<%#Eval("TeamApenalty")%>' />
                                            <asp:HiddenField ID="HiddenField2" runat="server"  Value='<%#Eval("TeamBpenalty")%>' />
                                            
                          </div>

                          <div class="match-result match_result_for_ResultMenu">
                             <span>
								<asp:Panel id="pnlResult" runat="server"  CssClass="results_div_panel">

                                  <asp:HyperLink ID="hprLinkForGame" runat="server" CssClass="hprLinkColor">
    							    <asp:Label runat="server" ID="lita" Text='<%#Eval("TeamAScore") %>'></asp:Label>
	    							    -
								    <asp:Label runat="server" ID="litb" Text='<%#Eval("TeamBScore") %>'></asp:Label>
                                  </asp:HyperLink>
		    					
                                  <asp:HiddenField ID="hidTeamAScoreFixturesCup" runat="server" Value='<%#Eval("TeamAScore")%>' />
				    			  <asp:HiddenField ID="hidTeamBScoreFixturesCup" runat="server" Value='<%#Eval("TeamBScore")%>' />
								  <asp:HiddenField ID="hdnNoshow" runat="server"  Value='<%#Eval("IsNoShow")%>' />
								  <asp:HiddenField ID="hdnwinningteam" runat="server"  Value='<%#Eval("WinningTeam")%>' />
								  <asp:HiddenField ID="hdnloosingteam" runat="server"  Value='<%#Eval("LosingTeam")%>' />
								</asp:Panel>

							    <asp:Panel id="pnlFixture" runat="server">
								   <div class="match-fixture-text">
								    	<asp:Literal ID="daysToKick" runat="server" />
								   </div>
								</asp:Panel>

                             </span>
                           </div>


                          <div class="match-result match_result_for_ResultMenu results_div_panel">
                             <center>
                                <span>
                                   <asp:Label ID="ltrpenaltyB" runat="server" Text='<%# Eval("TeamBpenalty") %>' Visible="false">
                                   </asp:Label>
                                </span>
                                <br />
                               <span class="fixturePenalty">
                                    <asp:Label ID="ltrpenaltyTextB" runat="server" Text="PENALTY" Visible="false">
                                    </asp:Label>
                               </span>
                             </center>
                           </div>

                           <asp:Panel ID="noShowRegionB" Visible="false" runat="server"  CssClass="results_div_panel">        
                             <div class="match_result_for_ResultMenu">
                                <center>
                                  <span>
                                      <asp:Literal ID="ltrlnoshowB" runat="server"></asp:Literal>
                                  </span>
                                </center>
                              </div>
                           </asp:Panel>

                          <div class="text right_div_css">
                             <div class="top-event">
                                <h2 class="pix-post-title">
                                   <asp:HyperLink CssClass="pix-hover" ID="hlnkTeamBFixtures" runat="server">
						
                                       <asp:Label ID="literalTeamBName" runat="server" Text='<%# Eval("TeamBName") %>' />
                                       
                                       <asp:Image ID="TeamBLogo"  runat="server" onError="imgErrorForTeam(this);"
                                                  ImageUrl='<%#Eval("TeamBLogo") %>' BorderStyle="None" 
                                                  CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX pix_results_img" 
                                                  AlternateText="" Width="30px" />
							       </asp:HyperLink>
							       <asp:HiddenField ID="hdnTeamBID" runat="server"  Value='<%#Eval("TeamBID")%>' />
                                </h2>
                            </div>
                          </div>

									</article>
								</ItemTemplate>
								<FooterTemplate>
								</FooterTemplate>
							</asp:Repeater>
					</div>
				</div>

				<div class="CompetitionAllDetail-TabContainer" id="rptQuater" runat="server" visible="false" clientidmode="Static">
				   <div class="event event-listing">
	    			  <center>
                        <h5 class="matchtype_header">
                           <asp:Label ID="Label2"  runat="server" Text="Round of 8"  />
                        </h5>
	    			  </center>   

					  <center>
                        <asp:Label ID="lblNoResultsQuater" runat="server" Text="No data available." 
                                   CssClass='emptyInfoLabel' Visible="false">
                        </asp:Label> 
					  </center>
						
					  <asp:Repeater ID="rptCompetitionSchedulesResultsQuater" runat="server" OnItemDataBound="rptCompetitionSchedulesResultsQuater_ItemDataBound">
						<HeaderTemplate>
						</HeaderTemplate>
					  	<ItemTemplate>

							<asp:HiddenField ID='hdnStartDate' runat="server" Value='<%#Eval("StartDate") %>' />
							<asp:HiddenField ID='hdnEndDate' runat="server" Value='<%#Eval("EndDate") %>' />
							<asp:HiddenField ID='hdnMatchID' runat="server" Value='<%#Eval("MatchID") %>' />
							<asp:HiddenField ID='hdnMatchResultId' runat="server" Value='<%#Eval("MatchResultId") %>' />
                                    
							<asp:Panel ID="SchedulesResultsDatePanel" runat="server">
				     		  <h5>
					    		 <asp:Literal ID="litSchedulesResultsDate" runat="server" Text='<%#  Eval("StartDate") %>' />
							  </h5>
							</asp:Panel>

							<article>
						    	<div class="calendar-date">
									<asp:Literal ID="litStartDate" runat="server" />
											@<%#Eval("LocationName") %>
						    	</div>

								<div class="text" style="width:300px;">
									<div class="top-event">
										<h2 class="pix-post-title">
											<asp:HyperLink CssClass="pix-hover" ID="hlnkTeamAFixtures" runat="server">
                                                <asp:Image ID="TeamALogo" runat="server" ImageUrl='<%#Eval("TeamALogo") %>' 
                                                           BorderStyle="None" onError="imgErrorForTeam(this);"
                                                           CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX" 
                                                           AlternateText="" Width="30px" />&nbsp;
												<asp:Label ID="literalTeamAName" runat="server" Text='<%# Eval("TeamAName") %>' />
											</asp:HyperLink>
											<asp:HiddenField ID="hdnTeamAID" runat="server"  Value='<%#Eval("TeamAID")%>' />
										</h2>
									</div>
								</div>

						
                        <div class="match_result_for_ResultMenu"> 
                            <asp:Panel ID="noShowRegion" Visible="false" runat="server"  CssClass="results_div_panel">        
                                <div class="match-result">
                                   <center>
                                     <span>
                                        <asp:Literal ID="ltrnoshow" runat="server"></asp:Literal>
                                     </span>
                                   </center>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="penaltyRegion" runat="server" Visible="false">
                            </asp:Panel> 

                            <div class="match-result results_div_panel">
                               <center>
                                 <span>
                                      <asp:Label ID="ltrpenalty" runat="server" 
                                                 Text='<%# Eval("TeamApenalty") %>' Visible="false">
                                      </asp:Label>
                                 </span>
                                 <br />
                                 <span class="fixturePenalty">
                                    <asp:Label ID="ltrpenaltyText" runat="server" Text="PENALTY" Visible="false"></asp:Label> 
                                 </span>
                            </div>      
                           
                            <asp:HiddenField ID="HiddenField1" runat="server"  Value='<%#Eval("TeamApenalty")%>' />
                            <asp:HiddenField ID="HiddenField2" runat="server"  Value='<%#Eval("TeamBpenalty")%>' />
                      
                        </div>

                        <div class="match-result match_result_for_ResultMenu">
                          <span>
						   	<asp:Panel id="pnlResult" runat="server"  CssClass="results_div_panel">
                              <asp:HyperLink ID="hprLinkForGame" runat="server" CssClass="hprLinkColor">
								  <asp:Label runat="server" ID="lita" Text='<%#Eval("TeamAScore") %>'></asp:Label>
									-
								  <asp:Label runat="server" ID="litb" Text='<%#Eval("TeamBScore") %>'></asp:Label>
                              </asp:HyperLink>
							
                              <asp:HiddenField ID="hidTeamAScoreFixturesCup" runat="server"  
                                               Value='<%#Eval("TeamAScore")%>' />
							  <asp:HiddenField ID="hidTeamBScoreFixturesCup" runat="server"  
                                               Value='<%#Eval("TeamBScore")%>' />

							  <asp:HiddenField ID="hdnNoshow" runat="server"  Value='<%#Eval("IsNoShow")%>' />
							  <asp:HiddenField ID="hdnwinningteam" runat="server"  Value='<%#Eval("WinningTeam")%>' />
							  <asp:HiddenField ID="hdnloosingteam" runat="server"  Value='<%#Eval("LosingTeam")%>' />
						    </asp:Panel>
							
                            <asp:Panel id="pnlFixture" runat="server">
							    <div class="match-fixture-text">
									<asp:Literal ID="daysToKick" runat="server" />
								</div>
							</asp:Panel>
                          </span>
                        </div>

                        <div class="match-result match_result_for_ResultMenu results_div_panel">
                           <center>
                              <span>
                                 <asp:Label ID="ltrpenaltyB" runat="server" Text='<%# Eval("TeamBpenalty") %>' 
                                            Visible="false"></asp:Label>
                              </span>
                              <br />
                              <span class="fixturePenalty">
                                 <asp:Label ID="ltrpenaltyTextB" runat="server" Text="PENALTY" Visible="false">
                                 </asp:Label> 
                              </span>
                           </center>
                         </div>

                         <asp:Panel ID="noShowRegionB" Visible="false" runat="server"  CssClass="results_div_panel">        
                            <div class="match_result_for_ResultMenu">
                               <center>
                                  <span>
                                       <asp:Literal ID="ltrlnoshowB" runat="server"></asp:Literal>
                                  </span>
                               </center>
                            </div>
                        </asp:Panel>

                        <div class="text right_div_css">
                          <div class="top-event">
                             <h2 class="pix-post-title">
                                <asp:HyperLink CssClass="pix-hover" ID="hlnkTeamBFixtures" runat="server" >
						        	<asp:Label ID="literalTeamBName" runat="server" Text='<%# Eval("TeamBName") %>' />
                                    <asp:Image ID="TeamBLogo"  runat="server" ImageUrl='<%#Eval("TeamBLogo") %>' 
                                               BorderStyle="None" onError="imgErrorForTeam(this);"
                                               CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX pix_results_img" 
                                               AlternateText="" Width="30px"/>
								</asp:HyperLink>
								<asp:HiddenField ID="hdnTeamBID" runat="server"  Value='<%#Eval("TeamBID")%>' />
                             </h2>
                           </div>
                         </div>

						</article>
				    </ItemTemplate>
					<FooterTemplate>
					</FooterTemplate>
					</asp:Repeater>
					</div>
	     		</div>

				<div ID="rptrQuaterSecondary" class="CompetitionAllDetail-TabContainer" runat="server" 
                     Visible="false" clientidmode="Static">

					<div class="event event-listing">
					  <center>
                        <h5 class="matchtype_header">
                            <asp:Label ID="Label3"  runat="server" Text="Round of 8 [Secondary]"  />
                        </h5>
					  </center>   

					<center>
                            <asp:Label ID="lblNoResultsForSecondaryQuater" runat="server" Text="No data available." 
                                    CssClass='emptyInfoLabel' Visible="false" ></asp:Label>
					</center>
						
					<asp:Repeater ID="rptrCompetitionQuaterSecondary" runat="server" 
                                  OnItemDataBound="rptrCompetitionQuaterSecondary_ItemDataBound">

						<HeaderTemplate>
							
						</HeaderTemplate>

						<ItemTemplate>

					    <asp:HiddenField ID='hdnStartDate' runat="server" Value='<%#Eval("StartDate") %>' />
						<asp:HiddenField ID='hdnEndDate' runat="server" Value='<%#Eval("EndDate") %>' />
						<asp:HiddenField ID='hdnMatchID' runat="server" Value='<%#Eval("MatchID") %>' />
						<asp:HiddenField ID='hdnMatchResultId' runat="server" Value='<%#Eval("MatchResultId") %>' />
                                    
					    <asp:Panel ID="SchedulesResultsDatePanel" runat="server">
						    <h5>
								<asp:Literal ID="litSchedulesResultsDate" runat="server" Text='<%#  Eval("StartDate") %>' />
							</h5>
						</asp:Panel>

						<article>
						
                        <div class="calendar-date">
							<asp:Literal ID="litStartDate" runat="server" />
										@<%#Eval("LocationName") %>
                        </div>

						<div class="text" style="width:300px;">
						    <div class="top-event">
								<h2 class="pix-post-title">
							    	<asp:HyperLink CssClass="pix-hover" ID="hlnkTeamAFixtures" runat="server" >
                                        <asp:Image ID="TeamALogo" runat="server" ImageUrl='<%#Eval("TeamALogo") %>' 
                                                   BorderStyle="None" onError="imgErrorForTeam(this);"
                                                   CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX" 
                                                   AlternateText="" Width="30px" />&nbsp;
								    	<asp:Label ID="literalTeamAName" runat="server" Text='<%# Eval("TeamAName") %>' />
									</asp:HyperLink>
									<asp:HiddenField ID="hdnTeamAID" runat="server"  Value='<%#Eval("TeamAID")%>' />
								</h2>
							</div>
						</div>

				        <div class="match_result_for_ResultMenu"> 
                            <asp:Panel ID="noShowRegion" Visible="false" runat="server"  CssClass="results_div_panel">        
                                <div class="match-result">
                                   <center>
                                      <span>
                                         <asp:Literal ID="ltrnoshow" runat="server"></asp:Literal>
                                      </span>
                                   </center>
                                </div>
                            </asp:Panel>

                           <asp:Panel ID="penaltyRegion" runat="server" Visible="false">
                           </asp:Panel> 
                         
                           <div class="match-result results_div_panel">
                             <center>
                                <span>
                                     <asp:Label ID="ltrpenalty" runat="server" Text='<%# Eval("TeamApenalty") %>' 
                                                Visible="false"></asp:Label>
                                </span>
                                <br />
                                <span class="fixturePenalty">
                                    <asp:Label ID="ltrpenaltyText" runat="server" Text="PENALTY" Visible="false">
                                    </asp:Label>
                                </span>
                           </div>      
                         
                           <asp:HiddenField ID="HiddenField1" runat="server"  Value='<%#Eval("TeamApenalty")%>' />
                           <asp:HiddenField ID="HiddenField2" runat="server"  Value='<%#Eval("TeamBpenalty")%>' />
                                            
                        </div>

                        <div class="match-result match_result_for_ResultMenu">
                           <span>
						      <asp:Panel id="pnlResult" runat="server"  CssClass="results_div_panel">
                                 <asp:HyperLink ID="hprLinkForGame" runat="server" CssClass="hprLinkColor">
									<asp:Label runat="server" ID="lita" Text='<%#Eval("TeamAScore") %>'></asp:Label>
										-
									<asp:Label runat="server" ID="litb" Text='<%#Eval("TeamBScore") %>'></asp:Label>
                                 </asp:HyperLink>
								
                                 <asp:HiddenField ID="hidTeamAScoreFixturesCup" runat="server" Value='<%#Eval("TeamAScore")%>' />
								 <asp:HiddenField ID="hidTeamBScoreFixturesCup" runat="server" Value='<%#Eval("TeamBScore")%>' />
								 <asp:HiddenField ID="hdnNoshow" runat="server"  Value='<%#Eval("IsNoShow")%>' />
								 <asp:HiddenField ID="hdnwinningteam" runat="server" Value='<%#Eval("WinningTeam")%>' />
								 <asp:HiddenField ID="hdnloosingteam" runat="server" Value='<%#Eval("LosingTeam")%>' />
							 </asp:Panel>
							
                             <asp:Panel id="pnlFixture" runat="server">
								<div class="match-fixture-text">
									<asp:Literal ID="daysToKick" runat="server" />
								</div>
							 </asp:Panel>
                          </span>
                        </div>


                        <div class="match-result match_result_for_ResultMenu results_div_panel">
                           <center>
                              <span>
                                 <asp:Label ID="ltrpenaltyB" runat="server" Text='<%# Eval("TeamBpenalty") %>' 
                                            Visible="false"></asp:Label>
                              </span>
                              <br />
                              <span class="fixturePenalty">
                                  <asp:Label ID="ltrpenaltyTextB" runat="server" Text="PENALTY" Visible="false">
                                  </asp:Label>
                              </span>
                            </center>
                        </div>

                        <asp:Panel ID="noShowRegionB" Visible="false" runat="server"  CssClass="results_div_panel">        
                          <div class="match_result_for_ResultMenu">
                            <center>
                              <span>
                                <asp:Literal ID="ltrlnoshowB" runat="server"></asp:Literal>
                              </span>
                            </center>
                           </div>
                        </asp:Panel>

                        <div class="text right_div_css">
                          <div class="top-event">
                            <h2 class="pix-post-title">
                              <asp:HyperLink CssClass="pix-hover" ID="hlnkTeamBFixtures" runat="server" >
					              <asp:Label ID="literalTeamBName" runat="server" Text='<%# Eval("TeamBName") %>' />
                                  <asp:Image ID="TeamBLogo"  runat="server" ImageUrl='<%#Eval("TeamBLogo") %>' 
                                             BorderStyle="None" onError="imgErrorForTeam(this);"
                                             CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX pix_results_img" 
                                             AlternateText="" Width="30px"/>
						      </asp:HyperLink>
							  <asp:HiddenField ID="hdnTeamBID" runat="server"  Value='<%#Eval("TeamBID")%>' />
                            </h2>
                          </div>
                        </div>

									</article>
								</ItemTemplate>
								<FooterTemplate>
								</FooterTemplate>
							</asp:Repeater>
    					</div>
					</div>


				<div ID="rptRoundOf16" CLASS="CompetitionAllDetail-TabContainer" runat="server" 
                     Visible="false" Clientidmode="Static">

					<div class="event event-listing">
        				<center>
                            <h5 class="matchtype_header">
                                <asp:Label ID="Label4"  runat="server" Text="Round of 16"  />
                            </h5>
				    	</center>   

				        <center>
                               <asp:Label ID="lblNoResultsRoundOf16" runat="server" Text="No data available." 
                                          CssClass='emptyInfoLabel' Visible="false">
                                </asp:Label> 
				        </center>
						
						
    				<asp:Repeater ID="rptCompetitionRoundOf16" runat="server"  
                                  OnItemDataBound="rptCompetitionRoundOf16_ItemDataBound">
						
                        <HeaderTemplate>
						</HeaderTemplate>
					
                      	<ItemTemplate>

					    	<asp:HiddenField ID='hdnStartDate' runat="server" Value='<%#Eval("StartDate") %>' />
							<asp:HiddenField ID='hdnEndDate' runat="server" Value='<%#Eval("EndDate") %>' />
							<asp:HiddenField ID='hdnMatchID' runat="server" Value='<%#Eval("MatchID") %>' />
							<asp:HiddenField ID='hdnMatchResultId' runat="server" Value='<%#Eval("MatchResultId") %>' />

							<asp:Panel ID="SchedulesResultsDatePanel" runat="server">
						        <h5>
							        <asp:Literal ID="litSchedulesResultsDate" runat="server" Text='<%#  Eval("StartDate") %>' />
								</h5>
							</asp:Panel>

							<article>
							
                           	<div class="calendar-date">
								<asp:Literal ID="litStartDate" runat="server"/>
											@<%#Eval("LocationName") %>
                           	</div>

     						<div class="text" style="width:300px;">
	         				  <div class="top-event">
								<h2 class="pix-post-title">
								  <asp:HyperLink CssClass="pix-hover" ID="hlnkTeamAFixtures" runat="server">
                                     <asp:Image ID="TeamALogo" runat="server" onError="imgErrorForTeam(this);"
                                                ImageUrl='<%#Eval("TeamALogo") %>' BorderStyle="None" 
                                                CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX" 
                                                AlternateText="" Width="30px" />&nbsp;
									 <asp:Label ID="literalTeamAName" runat="server" Text='<%# Eval("TeamAName") %>' />
								  </asp:HyperLink>
								  <asp:HiddenField ID="hdnTeamAID" runat="server"  Value='<%#Eval("TeamAID")%>' />
							   </h2>
							  </div>
							</div>

					<div class="match_result_for_ResultMenu"> 
                      <asp:Panel ID="noShowRegion" Visible="false" runat="server"  CssClass="results_div_panel">        
                        <div class="match-result">
                          <center>
                            <span>
                              <asp:Literal ID="ltrnoshow" runat="server"></asp:Literal>
                            </span>
                          </center>
                        </div>
                      </asp:Panel>

                      <asp:Panel ID="penaltyRegion" runat="server" Visible="false">
                      </asp:Panel> 

                      <div class="match-result results_div_panel">
                        <center>
                          <span>
                            <asp:Label ID="ltrpenalty" runat="server" Text='<%# Eval("TeamApenalty") %>' Visible="false">
                            </asp:Label>
                          </span>
                          <br />
                          <span class="fixturePenalty"> 
                              <asp:Label ID="ltrpenaltyText" runat="server" Text="PENALTY" Visible="false">
                              </asp:Label>
                          </span>
                      </div>      

                      <asp:HiddenField ID="HiddenField1" runat="server"  Value='<%#Eval("TeamApenalty")%>' />
                      <asp:HiddenField ID="HiddenField2" runat="server"  Value='<%#Eval("TeamBpenalty")%>' />
                                           
                    </div>

                    <div class="match-result match_result_for_ResultMenu">
                      <span>
						<asp:Panel id="pnlResult" runat="server"  CssClass="results_div_panel">
                          <asp:HyperLink ID="hprLinkForGame" runat="server" CssClass="hprLinkColor">
							<asp:Label runat="server" ID="lita" Text='<%#Eval("TeamAScore") %>'></asp:Label>
								-
							<asp:Label runat="server" ID="litb" Text='<%#Eval("TeamBScore") %>'></asp:Label>
                          </asp:HyperLink>

						  <asp:HiddenField ID="hidTeamAScoreFixturesCup" runat="server"  Value='<%#Eval("TeamAScore")%>' />
						  <asp:HiddenField ID="hidTeamBScoreFixturesCup" runat="server"  Value='<%#Eval("TeamBScore")%>' />
						  <asp:HiddenField ID="hdnNoshow" runat="server"  Value='<%#Eval("IsNoShow")%>' />
					      <asp:HiddenField ID="hdnwinningteam" runat="server"  Value='<%#Eval("WinningTeam")%>' />
						  <asp:HiddenField ID="hdnloosingteam" runat="server"  Value='<%#Eval("LosingTeam")%>' />

						</asp:Panel>
						
                        <asp:Panel id="pnlFixture" runat="server">
							<div class="match-fixture-text">
					    		<asp:Literal ID="daysToKick" runat="server" />
							</div>
						</asp:Panel>
                      </span>
                    </div>


                    <div class="match-result match_result_for_ResultMenu results_div_panel">
                      <center>
                        <span>
                          <asp:Label ID="ltrpenaltyB" runat="server" 
                                     Text='<%# Eval("TeamBpenalty") %>' Visible="false"></asp:Label>
                        </span>
                        <br />
                        <span class="fixturePenalty">
                           <asp:Label ID="ltrpenaltyTextB" runat="server" Text="PENALTY" Visible="false"></asp:Label> 
                        </span>
                      </center>
                    </div>

                    <asp:Panel ID="noShowRegionB" Visible="false" runat="server"  CssClass="results_div_panel">        
                      <div class="match_result_for_ResultMenu">
                        <center>
                          <span>
                               <asp:Literal ID="ltrlnoshowB" runat="server"></asp:Literal>
                          </span>
                        </center>
                      </div>
                    </asp:Panel>

                    <div class="text right_div_css">
                       <div class="top-event">
                         <h2 class="pix-post-title">
                           <asp:HyperLink CssClass="pix-hover" ID="hlnkTeamBFixtures" runat="server" >
    	        			  <asp:Label ID="literalTeamBName" runat="server" Text='<%# Eval("TeamBName") %>' />
                              <asp:Image ID="TeamBLogo"  runat="server" ImageUrl='<%#Eval("TeamBLogo") %>' 
                                         BorderStyle="None" onError="imgErrorForTeam(this);"
                                         CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX pix_results_img" 
                                         AlternateText="" Width="30px"  />
						    </asp:HyperLink>
							<asp:HiddenField ID="hdnTeamBID" runat="server"  Value='<%#Eval("TeamBID")%>' />
                         </h2>
                       </div>
                     </div>


					</article>
				</ItemTemplate>
				<FooterTemplate>
				</FooterTemplate>
	    	</asp:Repeater>
		</div>
	</div>

	<div class="CompetitionAllDetail-TabContainer" id="rptScheduleAndResults" runat="server" visible="false" clientidmode="Static">
	    <div class="event event-listing">
		   <center>
               <h5 class="matchtype_header">
                  <asp:Label ID="Label5" runat="server" Text="Group Stage"></asp:Label>
               </h5>
		   </center>   
		   <center>
               <asp:Label ID="lblNoResults" runat="server" Text="No data available." 
                          CssClass='emptyInfoLabel' Visible="false" ></asp:Label> 
		   </center>
						

		 <asp:Repeater ID="rptCompetitionSchedulesResults" runat="server" 
                       OnItemDataBound="rptCompetitionSchedulesResults_ItemDataBound">
			
            <HeaderTemplate>
			</HeaderTemplate>
			
            <ItemTemplate>

			<asp:HiddenField ID='hdnStartDate' runat="server" Value='<%#Eval("StartDate") %>' />
			<asp:HiddenField ID='hdnEndDate' runat="server" Value='<%#Eval("EndDate") %>' />
			<asp:HiddenField ID='hdnMatchID' runat="server" Value='<%#Eval("MatchID") %>' />
			<asp:HiddenField ID='hdnMatchResultId' runat="server" Value='<%#Eval("MatchResultId") %>' />

			<asp:Panel ID="SchedulesResultsDatePanel" runat="server">
			    <h5> 
				   <asp:Literal ID="litSchedulesResultsDate" runat="server" Text='<%#  Eval("StartDate") %>' />
				</h5>
			</asp:Panel>

			<article>

			<div class="calendar-date">
				<asp:Literal ID="litStartDate" runat="server" />
					@<%#Eval("LocationName") %>
			</div>

			<div class="text" style="width:300px;">
			  <div class="top-event">
				 <h2 class="pix-post-title">
					<asp:HyperLink CssClass="pix-hover" ID="hlnkTeamAFixtures" runat="server" >
                        <asp:Image ID="TeamALogo" runat="server" ImageUrl='<%#Eval("TeamALogo") %>' onError="imgErrorForTeam(this);"
                                   BorderStyle="None" CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX" 
                                   AlternateText="" Width="30px" />&nbsp;
					    <asp:Label ID="literalTeamAName" runat="server" Text='<%# Eval("TeamAName") %>' />
					</asp:HyperLink>
					<asp:HiddenField ID="hdnTeamAID" runat="server"  Value='<%#Eval("TeamAID")%>' />
				 </h2>
			  </div>
			</div>

	
            <div class="match_result_for_ResultMenu"> 

                   <asp:Panel ID="noShowRegion" Visible="false" runat="server"  CssClass="results_div_panel">        
                        <div class="match-result">
                           <center>
                              <span>
                                   <asp:Literal ID="ltrnoshow" runat="server"></asp:Literal>
                              </span>
                           </center>
                        </div>
                   </asp:Panel>

                   <asp:Panel ID="penaltyRegion" runat="server" Visible="false">
                   </asp:Panel> 
                  
                  <div class="match-result results_div_panel">
                    <center>
                      <span>
                        <asp:Label ID="ltrpenalty" runat="server" Text='<%# Eval("TeamApenalty") %>' 
                                   Visible="false"></asp:Label>
                      </span>
                      <br />
                      <span class="fixturePenalty">
                          <asp:Label ID="ltrpenaltyText" runat="server" Text="PENALTY" Visible="false">
                          </asp:Label>
                      </span>
                  </div>      
                      
                  <asp:HiddenField ID="HiddenField1" runat="server"  Value='<%#Eval("TeamApenalty")%>' />
                  <asp:HiddenField ID="HiddenField2" runat="server"  Value='<%#Eval("TeamBpenalty")%>' />

             </div>

            <div class="match-result match_result_for_ResultMenu">
              <span>
			   	<asp:Panel id="pnlResult" runat="server"  CssClass="results_div_panel">
                  <asp:HyperLink ID="hprLinkForGame" runat="server" CssClass="hprLinkColor">
				    <asp:Label runat="server" ID="lita" Text='<%#Eval("TeamAScore") %>'></asp:Label>
					    -
				    <asp:Label runat="server" ID="litb" Text='<%#Eval("TeamBScore") %>'></asp:Label>
                  </asp:HyperLink>
			
                  <asp:HiddenField ID="hidTeamAScoreFixturesCup" runat="server"  Value='<%#Eval("TeamAScore")%>' />
				  <asp:HiddenField ID="hidTeamBScoreFixturesCup" runat="server"  Value='<%#Eval("TeamBScore")%>' />
				  <asp:HiddenField ID="hdnNoshow" runat="server"  Value='<%#Eval("IsNoShow")%>' />
				  <asp:HiddenField ID="hdnwinningteam" runat="server"  Value='<%#Eval("WinningTeam")%>' />
				  <asp:HiddenField ID="hdnloosingteam" runat="server"  Value='<%#Eval("LosingTeam")%>' />
				</asp:Panel>
				
                <asp:Panel id="pnlFixture" runat="server">
				    <div class="match-fixture-text">
					    <asp:Literal ID="daysToKick" runat="server" />
					</div>
				</asp:Panel>
              </span>
            </div>


            <div class="match-result match_result_for_ResultMenu results_div_panel">
              <center>
                <span>
                  <asp:Label ID="ltrpenaltyB" runat="server" Text='<%# Eval("TeamBpenalty") %>' Visible="false">
                  </asp:Label>
                </span>
                <br />
                <span class="fixturePenalty">
                  <asp:Label ID="ltrpenaltyTextB" runat="server" Text="PENALTY" Visible="false">
                  </asp:Label> 
                </span>
              </center>
            </div>

            <asp:Panel ID="noShowRegionB" Visible="false" runat="server"  CssClass="results_div_panel">        
              <div class="match_result_for_ResultMenu">
                <center>
                  <span>
                    <asp:Literal ID="ltrlnoshowB" runat="server"></asp:Literal>
                  </span>
                </center>
              </div>
            </asp:Panel>

            <div class="text right_div_css">
              <div class="top-event">
                <h2 class="pix-post-title">
                  <asp:HyperLink CssClass="pix-hover" ID="hlnkTeamBFixtures" runat="server" >
				     <asp:Label ID="literalTeamBName" runat="server" Text='<%# Eval("TeamBName") %>' />
                     <asp:Image ID="TeamBLogo"  runat="server" ImageUrl='<%#Eval("TeamBLogo") %>' 
                                BorderStyle="None" onError="imgErrorForTeam(this);"
                                CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX pix_results_img" 
                                AlternateText="" Width="30px" />
		    	  </asp:HyperLink>
			      <asp:HiddenField ID="hdnTeamBID" runat="server"  Value='<%#Eval("TeamBID")%>' />
                </h2>
              </div>
            </div>

				      </article>
					</ItemTemplate>
				<FooterTemplate>
				</FooterTemplate>
			    </asp:Repeater>
    		</div>
		</div>
     </div>

    <div id="competitionTabs8" clientidmode="static" runat="server" style="padding:0px;">
         
        <div class="CompetitionAllDetail-TabContainer">
    	    <center>
				<asp:Label ID="lblNoForResults" runat="server" Text="No data available." CssClass='emptyInfoLabel' Visible="false">
				</asp:Label> 
			</center>
		</div>

        <div class="CompetitionAllDetail-TabContainer" id="final_Results" runat="server" visible="false" clientidmode="Static">
          <div class="event event-listing">
			<center>
              <h5 class="matchtype_header">
                 <asp:Label ID="final_ResultsMsg"  runat="server" Text="Final"  />
              </h5>
			</center>   
                        
		  <asp:Repeater ID="rptCompetitionResultsFinal" runat="server" 
                        OnItemDataBound="rptCompetitionResultsFinal_ItemDataBound">

			  <HeaderTemplate>
									
		      </HeaderTemplate>
				
              <ItemTemplate>

				<asp:HiddenField ID='hdnStartDate' runat="server" Value='<%#Eval("StartDate") %>' />
				<asp:HiddenField ID='hdnEndDate' runat="server" Value='<%#Eval("EndDate") %>' />
				<asp:HiddenField ID='hdnMatchID' runat="server" Value='<%#Eval("MatchID") %>' />
				<asp:HiddenField ID='hdnMatchResultId' runat="server" Value='<%#Eval("MatchResultId") %>' />
                                    
			<asp:Panel ID="SchedulesResultsDatePanel" runat="server">
				<h5>
				   <asp:Literal ID="litSchedulesResultsDate" runat="server" Text='<%#  Eval("StartDate") %>' />
				</h5>
			</asp:Panel>

			<article>

				<div class="calendar-date">
					<asp:Literal ID="litStartDate" runat="server"  />
			 					@<%#Eval("LocationName") %>
				</div>

				<div class="text" style="width:300px;">
					<div class="top-event">
						<h2 class="pix-post-title">
							<asp:HyperLink CssClass="pix-hover" ID="hlnkTeamAFixtures" runat="server">
                                 <asp:Image ID="TeamALogo" runat="server" onError="imgErrorForTeam(this);"
                                            ImageUrl='<%#Eval("TeamALogo") %>' 
                                            BorderStyle="None" 
                                            CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX" 
                                            AlternateText="" Width="30px" />&nbsp;
								 <asp:Label ID="literalTeamAName" runat="server" Text='<%# Eval("TeamAName") %>' />
							</asp:HyperLink>
							<asp:HiddenField ID="hdnTeamAID" runat="server"  Value='<%#Eval("TeamAID")%>' />
						</h2>
					</div>
				</div>

			 <div class="match_result_for_ResultMenu"> 

               <asp:Panel ID="noShowRegion" Visible="false" runat="server"  CssClass="results_div_panel">        
                 <div class="match-result">
                   <center>
                     <span>
                        <asp:Literal ID="ltrnoshow" runat="server"></asp:Literal>
                     </span>
                   </center>
                 </div>
               </asp:Panel>

               <asp:Panel ID="penaltyRegion" runat="server" Visible="false">
               </asp:Panel> 
 
               <div class="match-result results_div_panel">
                 <center>
                   <span>
                     <asp:Label ID="ltrpenalty" runat="server" Text='<%# Eval("TeamApenalty") %>' 
                                Visible="false"></asp:Label>
                   </span>
                   <br />
                   <span class="fixturePenalty" ><asp:Label ID="ltrpenaltyText" runat="server" 
                         Text="PENALTY" Visible="false"></asp:Label> 
                   </span>
               </div>      

                     <asp:HiddenField ID="HiddenField1" runat="server"  Value='<%#Eval("TeamApenalty")%>' />
                     <asp:HiddenField ID="HiddenField2" runat="server"  Value='<%#Eval("TeamBpenalty")%>' />
                                            
       </div>

       <div class="match-result match_result_for_ResultMenu">
         <span>
			<asp:Panel id="pnlResult" runat="server"  CssClass="results_div_panel">
              <asp:HyperLink ID="hprLinkForGame" runat="server" CssClass="hprLinkColor">
		  			<asp:Label runat="server" ID="lita" Text='<%#Eval("TeamAScore") %>'></asp:Label>
		       			-
			       	<asp:Label runat="server" ID="litb" Text='<%#Eval("TeamBScore") %>'></asp:Label>
              </asp:HyperLink>
			
              <asp:HiddenField ID="hidTeamAScoreFixturesCup" runat="server"  Value='<%#Eval("TeamAScore")%>' />
			  <asp:HiddenField ID="hidTeamBScoreFixturesCup" runat="server"  Value='<%#Eval("TeamBScore")%>' />
			  <asp:HiddenField ID="hdnNoshow" runat="server"  Value='<%#Eval("IsNoShow")%>' />
			  <asp:HiddenField ID="hdnwinningteam" runat="server"  Value='<%#Eval("WinningTeam")%>' />
			  <asp:HiddenField ID="hdnloosingteam" runat="server"  Value='<%#Eval("LosingTeam")%>' />
			</asp:Panel>
		    <asp:Panel id="pnlFixture" runat="server">
			   <div class="match-fixture-text">
				   <asp:Literal ID="daysToKick" runat="server" />
				</div>
			</asp:Panel>
          </span>
       </div>


       <div class="match-result match_result_for_ResultMenu results_div_panel">
         <center>
           <span>
              <asp:Label ID="ltrpenaltyB" runat="server" Text='<%# Eval("TeamBpenalty") %>' Visible="false">
              </asp:Label>
           </span>
           <br />
           <span class="fixturePenalty">
              <asp:Label ID="ltrpenaltyTextB" runat="server" Text="PENALTY" Visible="false">
              </asp:Label> 
           </span>
         </center>
      </div>

        <asp:Panel ID="noShowRegionB" Visible="false" runat="server"  CssClass="results_div_panel">        
          <div class="match_result_for_ResultMenu">
            <center>
              <span>
                 <asp:Literal ID="ltrlnoshowB" runat="server"></asp:Literal>
              </span>
            </center>
          </div>
        </asp:Panel>

        <div class="text right_div_css">
          <div class="top-event">
            <h2 class="pix-post-title">
              <asp:HyperLink CssClass="pix-hover" ID="hlnkTeamBFixtures" runat="server" >
				<asp:Label ID="literalTeamBName" runat="server" Text='<%# Eval("TeamBName") %>' />
                <asp:Image ID="TeamBLogo" runat="server" ImageUrl='<%#Eval("TeamBLogo") %>' 
                           BorderStyle="None" onError="imgErrorForTeam(this);"
                           CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX pix_results_img" 
                           AlternateText="" Width="30px" />
		      </asp:HyperLink>
			  <asp:HiddenField ID="hdnTeamBID" runat="server"  Value='<%#Eval("TeamBID")%>' />
            </h2>
          </div>
        </div>

			  </article>
		   </ItemTemplate>
			<FooterTemplate>
			</FooterTemplate>
	</asp:Repeater>
							
		</div>
	</div>

            	<div class="CompetitionAllDetail-TabContainer" id="semifinal_Results" runat="server" visible="false" clientidmode="Static">
    	<div class="event event-listing">
	        <center>
               <h5 class="matchtype_header">
                   <asp:Label ID="Label9"  runat="server" Text="Semi Final" />
               </h5>
           </center>   
	
           <center>
       
                    <asp:Label ID="semifinal_ResultsMsg" runat="server" Text="No data available." CssClass='emptyInfoLabel' 
                           Visible="false">
                    </asp:Label>

           </center>

		   <asp:Repeater runat="server" ID="rptCompetitionResultsSemiFinal" 
                         OnItemDataBound="rptCompetitionResultsSemiFinal_ItemDataBound">
				<HeaderTemplate>
				</HeaderTemplate>
			
                <ItemTemplate>

				<asp:HiddenField ID='hdnStartDate' runat="server" Value='<%#Eval("StartDate") %>' />
				<asp:HiddenField ID='hdnEndDate' runat="server" Value='<%#Eval("EndDate") %>' />
				<asp:HiddenField ID='hdnMatchID' runat="server" Value='<%#Eval("MatchID") %>' />
				<asp:HiddenField ID='hdnMatchResultId' runat="server" Value='<%#Eval("MatchResultId") %>' />
                                    
				<asp:Panel ID="SchedulesResultsDatePanel" runat="server">
					<h5>
						<asp:Literal ID="litSchedulesResultsDate" runat="server" Text='<%# Eval("StartDate") %>' />
					</h5>
				</asp:Panel>

			    <article>

					<div class="calendar-date">
						<asp:Literal ID="litStartDate" runat="server"  />
								@<%#Eval("LocationName") %></div>
                    
                    <div class="text" style="width:300px;">
						<div class="top-event">
							<h2 class="pix-post-title">
								<asp:HyperLink CssClass="pix-hover" ID="hlnkTeamAFixtures" runat="server" >
                                    <asp:Image ID="TeamALogo" runat="server" ImageUrl='<%#Eval("TeamALogo") %>' BorderStyle="None" 
                                           CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX" onError="imgErrorForTeam(this);"
                                           AlternateText="" Width="30px" />&nbsp;
								    <asp:Label ID="literalTeamAName" runat="server" Text='<%# Eval("TeamAName") %>' />
								</asp:HyperLink>
								<asp:HiddenField ID="hdnTeamAID" runat="server"  Value='<%#Eval("TeamAID")%>' />
							</h2>
						</div>
					</div>

              <div class="match_result_for_ResultMenu"> 
         
                   <asp:Panel ID="noShowRegion" Visible="false" runat="server"  CssClass="results_div_panel">        
                     <div class="match-result">
                        <center>
                            <span>
                                 <asp:Literal ID="ltrnoshow" runat="server"></asp:Literal>
                            </span>
                        </center>
                     </div>
                   </asp:Panel>

                   <asp:Panel ID="penaltyRegion" runat="server" Visible="false">
                   </asp:Panel> 
                  
                   <div class="match-result results_div_panel">
                       <center>
                          <span>
                               <asp:Label ID="ltrpenalty" runat="server" Text='<%# Eval("TeamApenalty") %>' Visible="false">
                               </asp:Label>
                          </span>
                          <br />
                          <span class="fixturePenalty">
                              <asp:Label ID="ltrpenaltyText" runat="server" Text="PENALTY" Visible="false">
                              </asp:Label> 
                          </span>
                   </div>      
 
                   <asp:HiddenField ID="HiddenField1" runat="server"  Value='<%#Eval("TeamApenalty")%>' />
                   <asp:HiddenField ID="HiddenField2" runat="server"  Value='<%#Eval("TeamBpenalty")%>' />
                                            
           </div>

           <div class="match-result match_result_for_ResultMenu">
              <span>
				<asp:Panel id="pnlResult" runat="server"  CssClass="results_div_panel">
                   <asp:HyperLink ID="hprLinkForGame" runat="server" CssClass="hprLinkColor">
						<asp:Label runat="server" ID="lita" Text='<%#Eval("TeamAScore") %>'></asp:Label>
							-
						<asp:Label runat="server" ID="litb" Text='<%#Eval("TeamBScore") %>'></asp:Label>
                   </asp:HyperLink>
		     	   <asp:HiddenField ID="hidTeamAScoreFixturesCup" runat="server"  Value='<%#Eval("TeamAScore")%>' />
				   <asp:HiddenField ID="hidTeamBScoreFixturesCup" runat="server"  Value='<%#Eval("TeamBScore")%>' />
				   <asp:HiddenField ID="hdnNoshow" runat="server"  Value='<%#Eval("IsNoShow")%>' />
				   <asp:HiddenField ID="hdnwinningteam" runat="server"  Value='<%#Eval("WinningTeam")%>' />
				   <asp:HiddenField ID="hdnloosingteam" runat="server"  Value='<%#Eval("LosingTeam")%>' />
				</asp:Panel>
				<asp:Panel id="pnlFixture" runat="server">
			    	<div class="match-fixture-text">
				    	<asp:Literal ID="daysToKick" runat="server" />
					</div>
				</asp:Panel>
              </span>
           </div>


           <div class="match-result match_result_for_ResultMenu results_div_panel">
              <center>
                 <span>
                    <asp:Label ID="ltrpenaltyB" runat="server" Text='<%# Eval("TeamBpenalty") %>' Visible="false"></asp:Label>
                 </span>
                 <br />
                 <span class="fixturePenalty">
                     <asp:Label ID="ltrpenaltyTextB" runat="server" Text="PENALTY" Visible="false">
                     </asp:Label> 
                 </span>
               </center>
            </div>

           <asp:Panel ID="noShowRegionB" Visible="false" runat="server"  CssClass="results_div_panel">        
              <div class="match_result_for_ResultMenu">
                 <center>
                    <span>
                       <asp:Literal ID="ltrlnoshowB" runat="server"></asp:Literal>
                    </span>
                 </center>
               </div>
           </asp:Panel>

           <div class="text right_div_css">
              <div class="top-event">
                 <h2 class="pix-post-title">
                    <asp:HyperLink CssClass="pix-hover" ID="hlnkTeamBFixtures" runat="server" >
						<asp:Label ID="literalTeamBName" runat="server" Text='<%# Eval("TeamBName") %>' />
                        <asp:Image ID="TeamBLogo"  runat="server" ImageUrl='<%#Eval("TeamBLogo") %>' onError="imgErrorForTeam(this);"
                                   BorderStyle="None" CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX pix_results_img" 
                                   AlternateText="" Width="30px"  />
					</asp:HyperLink>
					<asp:HiddenField ID="hdnTeamBID" runat="server"  Value='<%#Eval("TeamBID")%>' />
                 </h2>
              </div>
           </div>
	
        		</article>
	    	</ItemTemplate>
		<FooterTemplate>
		</FooterTemplate>
	</asp:Repeater>

	</div>
</div>

				<div class="CompetitionAllDetail-TabContainer" id="roundof8_Results" runat="server" visible="false" clientidmode="Static">
					<div class="event event-listing">

	    				<center>
                            
                            <h5 class="matchtype_header">
                                 <asp:Label ID="Label11"  runat="server" Text="Round of 8"  />
                            </h5>
	    				</center>   

						<center>
                            <asp:Label ID="roundof8_ResultsMsg" runat="server" Text="No data available." CssClass='emptyInfoLabel' Visible="false" >
                            </asp:Label> 
						</center>
						
						<asp:Repeater runat="server" ID="rptCompetitionResultsRoundOf8" OnItemDataBound="rptCompetitionResultsRoundOf8_ItemDataBound">
								<HeaderTemplate>
								</HeaderTemplate>
							    	<ItemTemplate>

									<asp:HiddenField ID='hdnStartDate' runat="server" Value='<%#Eval("StartDate") %>' />
									<asp:HiddenField ID='hdnEndDate' runat="server" Value='<%#Eval("EndDate") %>' />
									<asp:HiddenField ID='hdnMatchID' runat="server" Value='<%#Eval("MatchID") %>' />
									<asp:HiddenField ID='hdnMatchResultId' runat="server" Value='<%#Eval("MatchResultId") %>' />
                                    
									<asp:Panel ID="SchedulesResultsDatePanel" runat="server">
										<h5 >
										    <asp:Literal ID="litSchedulesResultsDate" runat="server" Text='<%#  Eval("StartDate") %>' />
										</h5>
									</asp:Panel>

									<article>

										<div class="calendar-date">
											<asp:Literal ID="litStartDate" runat="server"  />
											@<%#Eval("LocationName") %></div>

										<div class="text" style="width:300px;">
											<div class="top-event">
												<h2 class="pix-post-title">
													<asp:HyperLink CssClass="pix-hover" ID="hlnkTeamAFixtures" runat="server" >
                                                        <asp:Image ID="TeamALogo" runat="server" ImageUrl='<%#Eval("TeamALogo") %>' BorderStyle="None" CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX" AlternateText="" 
                                                            Width="30px" onError="imgErrorForTeam(this);" />&nbsp;
														<asp:Label ID="literalTeamAName" runat="server" Text='<%# Eval("TeamAName") %>' />
													</asp:HyperLink>
													<asp:HiddenField ID="hdnTeamAID" runat="server"  Value='<%#Eval("TeamAID")%>' />
													<%--<span>VS</span> --%>
													
												</h2>
											</div>
										</div>

                        <div class="match_result_for_ResultMenu"> 

                            <asp:Panel ID="noShowRegion" Visible="false" runat="server"  CssClass="results_div_panel">        
                                <div class="match-result">
                                        <center>
                                    <span>
                                        <asp:Literal ID="ltrnoshow" runat="server"></asp:Literal>
                                    </span>
                                       </center>
                                </div>
                                </asp:Panel>

                                <asp:Panel ID="penaltyRegion" runat="server" Visible="false">
                                </asp:Panel> 
                                        <div class="match-result results_div_panel">
                                            <center>
                                        <span>
                                              <asp:Label ID="ltrpenalty" runat="server" Text='<%# Eval("TeamApenalty") %>' Visible="false"></asp:Label>
                                        </span>
                                            <br />
                                        <span class="fixturePenalty" ><asp:Label ID="ltrpenaltyText" runat="server" Text="PENALTY" Visible="false"></asp:Label> </span>
                                        </div>      
                                            <asp:HiddenField ID="HiddenField1" runat="server"  Value='<%#Eval("TeamApenalty")%>' />
                                            <asp:HiddenField ID="HiddenField2" runat="server"  Value='<%#Eval("TeamBpenalty")%>' />
                                            
                                        </div>

                                        <div class="match-result match_result_for_ResultMenu">
                                            <span>
													<asp:Panel id="pnlResult" runat="server"  CssClass="results_div_panel">
                                                        <asp:HyperLink ID="hprLinkForGame" runat="server" CssClass="hprLinkColor">
														<asp:Label runat="server" ID="lita" Text='<%#Eval("TeamAScore") %>'></asp:Label>
														-
														<asp:Label runat="server" ID="litb" Text='<%#Eval("TeamBScore") %>'></asp:Label>
                                                        </asp:HyperLink>
														<asp:HiddenField ID="hidTeamAScoreFixturesCup" runat="server"  Value='<%#Eval("TeamAScore")%>' />
														<asp:HiddenField ID="hidTeamBScoreFixturesCup" runat="server"  Value='<%#Eval("TeamBScore")%>' />
														<asp:HiddenField ID="hdnNoshow" runat="server"  Value='<%#Eval("IsNoShow")%>' />
														<asp:HiddenField ID="hdnwinningteam" runat="server"  Value='<%#Eval("WinningTeam")%>' />
														<asp:HiddenField ID="hdnloosingteam" runat="server"  Value='<%#Eval("LosingTeam")%>' />
													</asp:Panel>
													<asp:Panel id="pnlFixture" runat="server">
														<div class="match-fixture-text">
															<asp:Literal ID="daysToKick" runat="server" />
														</div>
													</asp:Panel>
                                            </span>
                                        </div>


                                        <div class="match-result match_result_for_ResultMenu results_div_panel">
                                                        <center>
                                                    <span>
                                                        <asp:Label ID="ltrpenaltyB" runat="server" Text='<%# Eval("TeamBpenalty") %>' Visible="false"></asp:Label>
                                                        
                                                    </span>
                                                        <br />
                                                    <span class="fixturePenalty" ><asp:Label ID="ltrpenaltyTextB" runat="server" Text="PENALTY" Visible="false"></asp:Label> </span>
                                                            </center>
                                                    </div>

                                        <asp:Panel ID="noShowRegionB" Visible="false" runat="server"  CssClass="results_div_panel">        
                                        <div class="match_result_for_ResultMenu">
                                                        <center>
                                                    <span>
                                                        
                                                        <asp:Literal ID="ltrlnoshowB" runat="server"></asp:Literal>
                                                    
                                                    </span>
                                                            </center>
                                                </div>
                                            </asp:Panel>

                                        <div class="text right_div_css">
                                        <div class="top-event">
                                                <h2 class="pix-post-title">
                                                <asp:HyperLink CssClass="pix-hover" ID="hlnkTeamBFixtures" runat="server" >
													<asp:Label ID="literalTeamBName" runat="server" Text='<%# Eval("TeamBName") %>' />
                                                    <asp:Image ID="TeamBLogo"  runat="server" ImageUrl='<%#Eval("TeamBLogo") %>' BorderStyle="None" CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX pix_results_img" AlternateText="" 
                                                        Width="30px" onError="imgErrorForTeam(this);" />
													</asp:HyperLink>
													<asp:HiddenField ID="hdnTeamBID" runat="server"  Value='<%#Eval("TeamBID")%>' />
                                                    </h2>
                                                </div>
                                            </div>
									</article>

								</ItemTemplate>
								<FooterTemplate>
								</FooterTemplate>
							</asp:Repeater>

						

					</div>
	     		</div>

				<div class="CompetitionAllDetail-TabContainer" id="RoundOf8Secondary_Results" runat="server" visible="false" clientidmode="Static" >
					<div class="event event-listing">
					<center>
                        
                        <h5 class="matchtype_header">
                                 <asp:Label ID="Label13"  runat="server" Text="Round of 8 [Secondary]"  />
                            </h5>
					</center>   
							<center><asp:Label ID="RoundOf8Secondary_Results_Msg" runat="server" Text="No data available." CssClass='emptyInfoLabel' Visible="false" ></asp:Label> </center>
						
						<asp:Repeater runat="server" ID="rptCompetitionResultsRoundOf8Secondary" OnItemDataBound="rptCompetitionResultsRoundOf8Secondary_ItemDataBound">
								<HeaderTemplate>
									
								</HeaderTemplate>
								<ItemTemplate>

									<asp:HiddenField ID='hdnStartDate' runat="server" Value='<%#Eval("StartDate") %>' />
									<asp:HiddenField ID='hdnEndDate' runat="server" Value='<%#Eval("EndDate") %>' />
									<asp:HiddenField ID='hdnMatchID' runat="server" Value='<%#Eval("MatchID") %>' />
									<asp:HiddenField ID='hdnMatchResultId' runat="server" Value='<%#Eval("MatchResultId") %>' />
                                    
									<asp:Panel ID="SchedulesResultsDatePanel" runat="server">
										<h5 >
										<asp:Literal ID="litSchedulesResultsDate" runat="server" Text='<%#  Eval("StartDate") %>' />
										</h5>
									</asp:Panel>

									<article>

										<div class="calendar-date">
											<asp:Literal ID="litStartDate" runat="server"  />
											@<%#Eval("LocationName") %></div>

										<div class="text" style="width:300px;">
											<div class="top-event">
												<h2 class="pix-post-title">
													<asp:HyperLink CssClass="pix-hover" ID="hlnkTeamAFixtures" runat="server" >
                                                        <asp:Image ID="TeamALogo" runat="server" ImageUrl='<%#Eval("TeamALogo") %>' BorderStyle="None" CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX" AlternateText=""
                                                             Width="30px" onError="imgErrorForTeam(this);" />&nbsp;
														<asp:Label ID="literalTeamAName" runat="server" Text='<%# Eval("TeamAName") %>' />
													</asp:HyperLink>
													<asp:HiddenField ID="hdnTeamAID" runat="server"  Value='<%#Eval("TeamAID")%>' />
													<%--<span>VS</span> --%>
													
												</h2>
											</div>
										</div>

                        <div class="match_result_for_ResultMenu"> 

                            <asp:Panel ID="noShowRegion" Visible="false" runat="server"  CssClass="results_div_panel">        
                                <div class="match-result">
                                        <center>
                                    <span>
                                        <asp:Literal ID="ltrnoshow" runat="server"></asp:Literal>
                                    </span>
                                       </center>
                                </div>
                                </asp:Panel>

                                <asp:Panel ID="penaltyRegion" runat="server" Visible="false">
                                </asp:Panel> 
                                        <div class="match-result results_div_panel">
                                            <center>
                                        <span>
                                              <asp:Label ID="ltrpenalty" runat="server" Text='<%# Eval("TeamApenalty") %>' Visible="false"></asp:Label>
                                        </span>
                                            <br />
                                        <span class="fixturePenalty" ><asp:Label ID="ltrpenaltyText" runat="server" Text="PENALTY" Visible="false"></asp:Label> </span>
                                        </div>      
                                            <asp:HiddenField ID="HiddenField1" runat="server"  Value='<%#Eval("TeamApenalty")%>' />
                                            <asp:HiddenField ID="HiddenField2" runat="server"  Value='<%#Eval("TeamBpenalty")%>' />
                                            
                                        </div>

                                        <div class="match-result match_result_for_ResultMenu">
                                            <span>
													<asp:Panel id="pnlResult" runat="server"  CssClass="results_div_panel">
                                                        <asp:HyperLink ID="hprLinkForGame" runat="server" CssClass="hprLinkColor">
														<asp:Label runat="server" ID="lita" Text='<%#Eval("TeamAScore") %>'></asp:Label>
														-
														<asp:Label runat="server" ID="litb" Text='<%#Eval("TeamBScore") %>'></asp:Label>
                                                        </asp:HyperLink>
														<asp:HiddenField ID="hidTeamAScoreFixturesCup" runat="server"  Value='<%#Eval("TeamAScore")%>' />
														<asp:HiddenField ID="hidTeamBScoreFixturesCup" runat="server"  Value='<%#Eval("TeamBScore")%>' />
														<asp:HiddenField ID="hdnNoshow" runat="server"  Value='<%#Eval("IsNoShow")%>' />
														<asp:HiddenField ID="hdnwinningteam" runat="server"  Value='<%#Eval("WinningTeam")%>' />
														<asp:HiddenField ID="hdnloosingteam" runat="server"  Value='<%#Eval("LosingTeam")%>' />
													</asp:Panel>
													<asp:Panel id="pnlFixture" runat="server">
														<div class="match-fixture-text">
															<asp:Literal ID="daysToKick" runat="server" />
														</div>
													</asp:Panel>
                                            </span>
                                        </div>


                                        <div class="match-result match_result_for_ResultMenu results_div_panel">
                                                        <center>
                                                    <span>
                                                        <asp:Label ID="ltrpenaltyB" runat="server" Text='<%# Eval("TeamBpenalty") %>' Visible="false"></asp:Label>
                                                        
                                                    </span>
                                                        <br />
                                                    <span class="fixturePenalty" ><asp:Label ID="ltrpenaltyTextB" runat="server" Text="PENALTY" Visible="false"></asp:Label> </span>
                                                            </center>
                                                    </div>

                                        <asp:Panel ID="noShowRegionB" Visible="false" runat="server"  CssClass="results_div_panel">        
                                        <div class="match_result_for_ResultMenu">
                                                        <center>
                                                    <span>
                                                        
                                                        <asp:Literal ID="ltrlnoshowB" runat="server"></asp:Literal>
                                                    
                                                    </span>
                                                            </center>
                                                </div>
                                            </asp:Panel>

                                        <div class="text right_div_css">
                                        <div class="top-event">
                                                <h2 class="pix-post-title">
                                                <asp:HyperLink CssClass="pix-hover" ID="hlnkTeamBFixtures" runat="server" >
													<asp:Label ID="literalTeamBName" runat="server" Text='<%# Eval("TeamBName") %>' />
                                                    <asp:Image ID="TeamBLogo"  runat="server" ImageUrl='<%#Eval("TeamBLogo") %>' BorderStyle="None" CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX pix_results_img" AlternateText="" 
                                                        Width="30px"  onError="imgErrorForTeam(this);" />
													</asp:HyperLink>
													<asp:HiddenField ID="hdnTeamBID" runat="server"  Value='<%#Eval("TeamBID")%>' />
                                                    </h2>
                                                </div>
                                            </div>
									</article>
								</ItemTemplate>
								<FooterTemplate>
								</FooterTemplate>
							</asp:Repeater>

						

					</div>
					</div>

            	<div class="CompetitionAllDetail-TabContainer" id="RoundOf16Secondary_Results" runat="server" visible="false" clientidmode="Static" >
					<div class="event event-listing">
					<center>
                        
                        <h5 class="matchtype_header">
                            <asp:Label ID="Label15"  runat="server" Text="Round of 16"  />
                        </h5>

					</center>   
							<center><asp:Label ID="RoundOf16Secondary_ResultsMsg" runat="server" Text="No data available." CssClass='emptyInfoLabel' Visible="false" ></asp:Label> </center>
						
						
						<asp:Repeater runat="server" ID="rptCompetitionResultsRoundOf16" OnItemDataBound="rptCompetitionResultsRoundOf16_ItemDataBound">
								<HeaderTemplate>
									
								</HeaderTemplate>
								<ItemTemplate>

									<asp:HiddenField ID='hdnStartDate' runat="server" Value='<%#Eval("StartDate") %>' />
									<asp:HiddenField ID='hdnEndDate' runat="server" Value='<%#Eval("EndDate") %>' />
									<asp:HiddenField ID='hdnMatchID' runat="server" Value='<%#Eval("MatchID") %>' />
									<asp:HiddenField ID='hdnMatchResultId' runat="server" Value='<%#Eval("MatchResultId") %>' />

									<asp:Panel ID="SchedulesResultsDatePanel" runat="server">
										<h5 >
										<asp:Literal ID="litSchedulesResultsDate" runat="server" Text='<%#  Eval("StartDate") %>' />
										</h5>
									</asp:Panel>

									<article>

										<div class="calendar-date">
											<asp:Literal ID="litStartDate" runat="server"  />
											@<%#Eval("LocationName") %></div>

										<div class="text" style="width:300px;">
											<div class="top-event">
												<h2 class="pix-post-title">
													<asp:HyperLink CssClass="pix-hover" ID="hlnkTeamAFixtures" runat="server" >
                                                        <asp:Image ID="TeamALogo" runat="server" ImageUrl='<%#Eval("TeamALogo") %>' BorderStyle="None" CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX" AlternateText="" Width="30px"
                                                            onError="imgErrorForTeam(this);" />&nbsp;
														<asp:Label ID="literalTeamAName" runat="server" Text='<%# Eval("TeamAName") %>' />
													</asp:HyperLink>
													<asp:HiddenField ID="hdnTeamAID" runat="server"  Value='<%#Eval("TeamAID")%>' />
													<%--<span>VS</span> --%>
													
												</h2>
											</div>
										</div>

										



                        <div class="match_result_for_ResultMenu"> 

                            <asp:Panel ID="noShowRegion" Visible="false" runat="server"  CssClass="results_div_panel">        
                                <div class="match-result">
                                        <center>
                                    <span>
                                        <asp:Literal ID="ltrnoshow" runat="server"></asp:Literal>
                                    </span>
                                       </center>
                                </div>
                                </asp:Panel>

                                <asp:Panel ID="penaltyRegion" runat="server" Visible="false">
                                </asp:Panel> 
                                        <div class="match-result results_div_panel">
                                            <center>
                                        <span>
                                              <asp:Label ID="ltrpenalty" runat="server" Text='<%# Eval("TeamApenalty") %>' Visible="false"></asp:Label>
                                        </span>
                                            <br />
                                        <span class="fixturePenalty" ><asp:Label ID="ltrpenaltyText" runat="server" Text="PENALTY" Visible="false"></asp:Label> </span>
                                        </div>      
                                            <asp:HiddenField ID="HiddenField1" runat="server"  Value='<%#Eval("TeamApenalty")%>' />
                                            <asp:HiddenField ID="HiddenField2" runat="server"  Value='<%#Eval("TeamBpenalty")%>' />
                                            
                                        </div>

                                        <div class="match-result match_result_for_ResultMenu">
                                            <span>
													<asp:Panel id="pnlResult" runat="server"  CssClass="results_div_panel">
                                                        <asp:HyperLink ID="hprLinkForGame" runat="server" CssClass="hprLinkColor">
														<asp:Label runat="server" ID="lita" Text='<%#Eval("TeamAScore") %>'></asp:Label>
														-
														<asp:Label runat="server" ID="litb" Text='<%#Eval("TeamBScore") %>'></asp:Label>
                                                        </asp:HyperLink>
														<asp:HiddenField ID="hidTeamAScoreFixturesCup" runat="server"  Value='<%#Eval("TeamAScore")%>' />
														<asp:HiddenField ID="hidTeamBScoreFixturesCup" runat="server"  Value='<%#Eval("TeamBScore")%>' />
														<asp:HiddenField ID="hdnNoshow" runat="server"  Value='<%#Eval("IsNoShow")%>' />
														<asp:HiddenField ID="hdnwinningteam" runat="server"  Value='<%#Eval("WinningTeam")%>' />
														<asp:HiddenField ID="hdnloosingteam" runat="server"  Value='<%#Eval("LosingTeam")%>' />
													</asp:Panel>
													<asp:Panel id="pnlFixture" runat="server">
														<div class="match-fixture-text">
															<asp:Literal ID="daysToKick" runat="server" />
														</div>
													</asp:Panel>
                                            </span>
                                        </div>


                                        <div class="match-result match_result_for_ResultMenu results_div_panel">
                                                        <center>
                                                    <span>
                                                        <asp:Label ID="ltrpenaltyB" runat="server" Text='<%# Eval("TeamBpenalty") %>' Visible="false"></asp:Label>
                                                        
                                                    </span>
                                                        <br />
                                                    <span class="fixturePenalty" ><asp:Label ID="ltrpenaltyTextB" runat="server" Text="PENALTY" Visible="false"></asp:Label> </span>
                                                            </center>
                                                    </div>

                                        <asp:Panel ID="noShowRegionB" Visible="false" runat="server"  CssClass="results_div_panel">        
                                        <div class="match_result_for_ResultMenu">
                                                        <center>
                                                    <span>
                                                        
                                                        <asp:Literal ID="ltrlnoshowB" runat="server"></asp:Literal>
                                                    
                                                    </span>
                                                            </center>
                                                </div>
                                            </asp:Panel>

                                        <div class="text right_div_css">
                                        <div class="top-event">
                                                <h2 class="pix-post-title">
                                                <asp:HyperLink CssClass="pix-hover" ID="hlnkTeamBFixtures" runat="server" >
													<asp:Label ID="literalTeamBName" runat="server" Text='<%# Eval("TeamBName") %>' />
                                                    <asp:Image ID="TeamBLogo"  runat="server" ImageUrl='<%#Eval("TeamBLogo") %>' BorderStyle="None" CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX pix_results_img" AlternateText=""
                                                        onError="imgErrorForTeam(this);" Width="30px"  />
													</asp:HyperLink>
													<asp:HiddenField ID="hdnTeamBID" runat="server"  Value='<%#Eval("TeamBID")%>' />
                                                    </h2>
                                                </div>
                                            </div>
									</article>
								</ItemTemplate>
								<FooterTemplate>
								</FooterTemplate>
							</asp:Repeater>

						

					</div>
					</div>

				<div class="CompetitionAllDetail-TabContainer" id="GroupStage_results" runat="server" visible="false" clientidmode="Static" >
					<div class="event event-listing">
					<center>
                        <%--<div class="MatchTypeHeader"><asp:Label runat="server" Text="Group Stage" CssClass='emptyInfoLabel' /></div> --%>
                        <h5 class="matchtype_header">
                        <asp:Label ID="Label17" runat="server" Text="Group Stage"></asp:Label>
                        </h5>
					</center>   
							<center><asp:Label ID="GroupStage_resultsMsg" runat="server" Text="No data available." CssClass='emptyInfoLabel' Visible="false" ></asp:Label> </center>
						

						<asp:Repeater runat="server" ID="rptCompetitionResultsGroupStage" OnItemDataBound="rptCompetitionResultsGroupStage_ItemDataBound">
								<HeaderTemplate>
									
								</HeaderTemplate>
								<ItemTemplate>

									<asp:HiddenField ID='hdnStartDate' runat="server" Value='<%#Eval("StartDate") %>' />
									<asp:HiddenField ID='hdnEndDate' runat="server" Value='<%#Eval("EndDate") %>' />
									<asp:HiddenField ID='hdnMatchID' runat="server" Value='<%#Eval("MatchID") %>' />
									<asp:HiddenField ID='hdnMatchResultId' runat="server" Value='<%#Eval("MatchResultId") %>' />

									<asp:Panel ID="SchedulesResultsDatePanel" runat="server">
										<h5 >
										<asp:Literal ID="litSchedulesResultsDate" runat="server" Text='<%#  Eval("StartDate") %>' />
										</h5>
									</asp:Panel>

									<article>

										<div class="calendar-date">
											<asp:Literal ID="litStartDate" runat="server"  />
											@<%#Eval("LocationName") %></div>

										<div class="text" style="width:300px;">
											<div class="top-event">
												<h2 class="pix-post-title">
													<asp:HyperLink CssClass="pix-hover" ID="hlnkTeamAFixtures" runat="server" >
                                                        <asp:Image ID="TeamALogo" runat="server" ImageUrl='<%#Eval("TeamALogo") %>' BorderStyle="None" CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX" AlternateText="" Width="30px" 
                                                            onError="imgErrorForTeam(this);" />&nbsp;
														<asp:Label ID="literalTeamAName" runat="server" Text='<%# Eval("TeamAName") %>' />
													</asp:HyperLink>
													<asp:HiddenField ID="hdnTeamAID" runat="server"  Value='<%#Eval("TeamAID")%>' />
													<%--<span>VS</span> --%>
													
												</h2>
											</div>
										</div>

										



                        <div class="match_result_for_ResultMenu"> 

                            <asp:Panel ID="noShowRegion" Visible="false" runat="server"  CssClass="results_div_panel">        
                                <div class="match-result">
                                        <center>
                                    <span>
                                        <asp:Literal ID="ltrnoshow" runat="server"></asp:Literal>
                                    </span>
                                       </center>
                                </div>
                                </asp:Panel>

                                <asp:Panel ID="penaltyRegion" runat="server" Visible="false">
                                </asp:Panel> 
                                        <div class="match-result results_div_panel">
                                            <center>
                                        <span>
                                              <asp:Label ID="ltrpenalty" runat="server" Text='<%# Eval("TeamApenalty") %>' Visible="false"></asp:Label>
                                        </span>
                                            <br />
                                        <span class="fixturePenalty" ><asp:Label ID="ltrpenaltyText" runat="server" Text="PENALTY" Visible="false"></asp:Label> </span>
                                        </div>      
                                            <asp:HiddenField ID="HiddenField1" runat="server"  Value='<%#Eval("TeamApenalty")%>' />
                                            <asp:HiddenField ID="HiddenField2" runat="server"  Value='<%#Eval("TeamBpenalty")%>' />
                                            
                                        </div>

                                        <div class="match-result match_result_for_ResultMenu">
                                            <span>
													<asp:Panel id="pnlResult" runat="server"  CssClass="results_div_panel">
                                                        <asp:HyperLink ID="hprLinkForGame" runat="server" CssClass="hprLinkColor">
														<asp:Label runat="server" ID="lita" Text='<%#Eval("TeamAScore") %>'></asp:Label>
														-
														<asp:Label runat="server" ID="litb" Text='<%#Eval("TeamBScore") %>'></asp:Label>
                                                        </asp:HyperLink>
														<asp:HiddenField ID="hidTeamAScoreFixturesCup" runat="server"  Value='<%#Eval("TeamAScore")%>' />
														<asp:HiddenField ID="hidTeamBScoreFixturesCup" runat="server"  Value='<%#Eval("TeamBScore")%>' />
														<asp:HiddenField ID="hdnNoshow" runat="server"  Value='<%#Eval("IsNoShow")%>' />
														<asp:HiddenField ID="hdnwinningteam" runat="server"  Value='<%#Eval("WinningTeam")%>' />
														<asp:HiddenField ID="hdnloosingteam" runat="server"  Value='<%#Eval("LosingTeam")%>' />
													</asp:Panel>
													<asp:Panel id="pnlFixture" runat="server">
														<div class="match-fixture-text">
															<asp:Literal ID="daysToKick" runat="server" />
														</div>
													</asp:Panel>
                                            </span>
                                        </div>


                                        <div class="match-result match_result_for_ResultMenu results_div_panel">
                                                        <center>
                                                    <span>
                                                        <asp:Label ID="ltrpenaltyB" runat="server" Text='<%# Eval("TeamBpenalty") %>' Visible="false"></asp:Label>
                                                        
                                                    </span>
                                                        <br />
                                                    <span class="fixturePenalty" ><asp:Label ID="ltrpenaltyTextB" runat="server" Text="PENALTY" Visible="false"></asp:Label> </span>
                                                            </center>
                                                    </div>

                                        <asp:Panel ID="noShowRegionB" Visible="false" runat="server"  CssClass="results_div_panel">        
                                        <div class="match_result_for_ResultMenu">
                                                        <center>
                                                    <span>
                                                        
                                                        <asp:Literal ID="ltrlnoshowB" runat="server"></asp:Literal>
                                                    
                                                    </span>
                                                            </center>
                                                </div>
                                            </asp:Panel>

                                        <div class="text right_div_css">
                                        <div class="top-event">
                                                <h2 class="pix-post-title">
                                                <asp:HyperLink CssClass="pix-hover" ID="hlnkTeamBFixtures" runat="server" >
													<asp:Label ID="literalTeamBName" runat="server" Text='<%# Eval("TeamBName") %>' />
                                                    <asp:Image ID="TeamBLogo"  runat="server" ImageUrl='<%#Eval("TeamBLogo") %>' BorderStyle="None" CssClass="fixtureTeamLogo TeamPage-Logo-Wrapper-25PX pix_results_img" AlternateText="" 
                                                        onError="imgErrorForTeam(this);" Width="30px"  />
													</asp:HyperLink>
													<asp:HiddenField ID="hdnTeamBID" runat="server"  Value='<%#Eval("TeamBID")%>' />
                                                    </h2>
                                                </div>
                                            </div>
									</article>
								</ItemTemplate>
								<FooterTemplate>
								</FooterTemplate>
							</asp:Repeater>

						

					</div>
					</div>


			</div>

    			<div id="competitionTabs4" clientidmode="static" runat="server" class="TeamAllDetail-Tabs"> 
				<div class="TeamAllDetail-TabContainer">    
                         
					<center>
						<asp:Label ID="lblNoNews" runat="server" Text="No data available." CssClass='CompetitionAllDetail-area emptyInfoLabel' Visible="false" ></asp:Label>
					</center>

					<asp:Repeater ID="rptrNews" OnItemDataBound="rptrNews_ItemDataBound" runat="server" OnItemCommand="rptrNews_ItemCommand">
						<ItemTemplate>
							<div class="TeamAllDetail-News">
								<div class="readMoreWithJsTitle">
                
										<div class="NewsTitle">
											<asp:Literal ID="ltrlTitle" Text='<%# Eval("NewsTitle") %>' runat="server"/>
										</div>
							</div>
                            <div>
                                <asp:Label ID="ltrlDate" runat="server" Text='<%#(Eval("CreatedOnDateChange")) %>' class="TeamAllDetail-Date"/>
                            </div>

							<div class="readMoreWithJsDescription">
								<div class="TeamAllDetail-grid-cell-inner" style="display: inline-block;height: 57px;margin-bottom: 12px;">
									<asp:Literal ID="ltrlDescription" Text='<%# Eval("NewsDesc") %>' runat="server"></asp:Literal>
								</div> 
								<br />
                                <asp:LinkButton ID="likReadMore" runat="server" Text="Read More" CssClass="TeamAllDetail-NewsbtnReadMore" CommandArgument='<%# Eval("NewsId") %>' CommandName="btnNewsDisplayReadMore">
                                </asp:LinkButton>
								
								
                             
							</div>

							</div>
						</ItemTemplate>
					</asp:Repeater>

				</div>
			</div>

	    	    <div id="competitionTabs5" runat="server" clientidmode="static"  class="TeamAllDetail-Tabs">
			<div class="TeamAllDetail-TabContainer">
				<center>
					<asp:Label ID="lblNoVideo" runat="server" Text="No data available." CssClass="emptyInfoLabel" Visible="false">
					</asp:Label> 
				</center>
				<asp:Panel ID="pnlForVideoTable" runat="server">

			            <div class="element_size_100 page_listing">
		            		<div class="gallerysec gallery">
			            		<asp:Repeater ID="rptleftVideo" runat="server">
            						<HeaderTemplate>
					            		<ul class="gallery-three-col lightbox clearfix">
					            	</HeaderTemplate>
						            <ItemTemplate>
							<li class="video-gallery-img">
								<figure>
									<img src='<%#"http://i.ytimg.com/vi/" +  Eval("VideoYouTubeFile") + "/default.jpg" %>' alt=""/>
									<figcaption>
											<a data-rel="prettyPhoto" href='<%# "https://www.youtube.com/watch?v=" + Eval("VideoYouTubeFile") %>' data-title="" rel="prettyPhoto">
											<i class="fa fa-video-camera"></i>                            </a>
									</figcaption>
								</figure>
								<div class="text">
                                	<h2><asp:Literal ID="litCompetitionVideoTitle" runat="server" Text='<%# Eval("VideoTitle") %>'></asp:Literal></h2>
								</div>
							</li>
					
						</ItemTemplate>
            						<FooterTemplate>
							</ul>
						</FooterTemplate>
					            </asp:Repeater>
		            		</div>
		            	</div>

                      <div class="element_size_100 page_listing">
		            		<div class="gallerysec gallery">
			            		<asp:Repeater ID="rptleftothervideo" runat="server">
            						<HeaderTemplate>
					            		<ul class="gallery-three-col lightbox clearfix">
					            	</HeaderTemplate>
						            <ItemTemplate>
							            <li class="video-gallery-img">
							                <iframe width="348" height="211" src="<%# "/DesktopModules/ThSport/" + Eval("VideoOtherFile") %>">
                                            </iframe>
    							        
                                            <div class="text">
                                	            <h2><asp:Literal ID="litCompetitionVideoTitle" runat="server" Text='<%# Eval("VideoTitle") %>'></asp:Literal></h2>
								            </div>
							            </li>
					
						            </ItemTemplate>
            						<FooterTemplate>
							            </ul>
						            </FooterTemplate>
					            </asp:Repeater>
		            		</div>
		            	</div>

    			</asp:Panel>
			</div>
		</div>

			    <div id="competitionTabs6" clientidmode="static" runat="server" class="TeamAllDetail-Tabs">
				<div class="TeamAllDetail-TabContainer">
				<center>
					<asp:Label ID="lblNoPhotos" runat="server" Text="No data available." CssClass='emptyInfoLabel' Visible="false" ></asp:Label> 
				</center>

                    <div class="element_size_100 page_listing">
			    	<div class="gallerysec gallery">
					    <asp:Repeater ID="competitionphoto" runat="server">
				    		<HeaderTemplate>
							<ul class="gallery-three-col lightbox clearfix">
						</HeaderTemplate>
						    <ItemTemplate>
							<li class="video-gallery-img">
								<figure>
									<asp:Image ID="Image" alt="" runat="server" ImageUrl='<%#Eval("CompGalleryPath") %>' Height="218px" onError="imgErrorForTeam(this);"></asp:Image>
									<figcaption>
											<a data-rel="prettyPhoto" href='<%# Page.ResolveUrl("~/DesktopModules/ThSport/" + Eval("CompGalleryPath") + "") %>' data-title="" rel="prettyPhoto[gallery1]">
											<i class="fa fa-plus"></i>                            </a>
									</figcaption>
								</figure>
								<div class="text">
                                	<h2><asp:Literal ID="litCompetitionGalleryName" runat="server" Text='<%# Eval("PictureTitle") %>'></asp:Literal></h2>
								</div>
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

			    <div id="competitionTabs7" clientidmode="static" runat="server" style="padding:0px;">
                <div class="CompetitionAllDetail-TabContainer">
				    <div class="CompetitionAllDetail-area">
			            <asp:PlaceHolder ID="plcPoints" runat="server"></asp:PlaceHolder>
			        </div>
			    </div>

				<div class="CompetitionAllDetail-TabContainer" >
					<div class="CompetitionAllDetail-area">
					<asp:Repeater ID="rptTeamsView" runat="server" OnItemDataBound="rptTeamsView_ItemDataBound" OnItemCommand="rptTeamsView_OnIteamCommand">
							<HeaderTemplate>
									<table  class="groupTeamTable">
							</HeaderTemplate>
							<ItemTemplate>
									<asp:Panel ID="GroupPanel" runat="server">
									<tr>
										<td colspan="11" >
											<div class="CompetitionAllDetail-area competionAllDetailGroupName">
												<asp:Literal ID="lblSelectGroup" runat="server" Text='<%#Eval("CompetitionGroupName") %>' />
											</div>
										</td>
									</tr>
									<tr>
									<th width="100px" >POS</th>
									<th colspan="2" width="400px" class="" >TEAM</th>
									<th width="50px">PTS</th>
									<th width="50px">GD</th>
									<th width="50px">P</th>
									<th width="50px">W</th>
									<th width="50px">D</th>
									<th width="50px">L</th>
									<th width="50px">GF</th>
									<th width="50px">GA</th>
									</tr>
									</asp:Panel>
									<tr>
										<td>
											<div class="grid-cell-inner" style="padding: 15px 5px;" ><asp:Literal ID="ltrlPos" runat="server" Text='<%#Eval("TeamRank") %>' /></div>
										</td>
										<td width="100px">
											<asp:ImageButton ID="imgTeamLogo" runat="server" ImageUrl='<%#Eval("TeamLogo") %>' 
													AlternateText='<%#Eval("TeamName") %>' CssClass='competionAllDetailTeamLogo' 
													CommandName="teamName" CommandArgument='<%# Eval("TeamID") %>'  onError="imgErrorForTeam(this);" />   
										</td>
										<td width="300px" class="groupTeamTableLink" >
											<div class="grid-cell-inner" style="padding: 15px 5px;" >
												<asp:LinkButton ID="lnkTeamName" runat="server" Text='<%#Eval("TeamName") %>' CommandName="teamName" CommandArgument='<%# Eval("TeamID") %>' ></asp:LinkButton>
											</div>
										</td>
                                        
										<td>
											<div class="grid-cell-inner" style="padding: 15px 5px;" ><asp:Literal ID="ltrlPts" runat="server" Text='<%#Eval("Pts") %>' /></div>
											<asp:HiddenField ID="hdnPts" runat="server" Value='<%#Eval("Pts") %>' />
										</td>
										<td><div class="grid-cell-inner" style="padding: 15px 5px;" ><asp:Literal ID="Literal5" runat="server" Text='<%#Eval("GD") %>' /></div></td>

										<td>
											<div class="grid-cell-inner" style="padding: 15px 5px;" ><asp:Literal ID="Literal6" runat="server" Text='<%#Eval("Played") %>' /></div>
										</td>

										<td>
											<div class="grid-cell-inner" style="padding: 15px 5px;" ><asp:Literal ID="Literal1" runat="server" Text='<%#Eval("Win") %>' /></div>
										</td>

										<td>
											<div class="grid-cell-inner" style="padding: 15px 5px;" ><asp:Literal ID="Literal2" runat="server" Text='<%#Eval("Draw") %>' /></div>
										</td>

										<td>
											<div class="grid-cell-inner" style="padding: 15px 5px;" ><asp:Literal ID="Literal4" runat="server" Text='<%#Eval("Loss") %>' /></div>
										</td>

										<td><div class="grid-cell-inner" style="padding: 15px 5px;" ><asp:Literal ID="Literal7" runat="server" Text='<%#Eval("GF") %>' /></div></td>

										<td><div class="grid-cell-inner" style="padding: 15px 5px;" ><asp:Literal ID="Literal15" runat="server" Text='<%#Eval("GA") %>' /></div></td>
                                         
									</tr>
							</ItemTemplate>
							<FooterTemplate>
										</table>
							</FooterTemplate>
						</asp:Repeater>
							<div id="statusMessage" runat="server">
							<asp:Label Id="lblMessage" runat="server"  Text="" Visible="false"></asp:Label>
						</div>
					<input type="hidden" runat="server" id="hidRegID" />
						<div class="Standings_Legend_data">POS - Position, PTS - Points, GD - Goal Difference, P - Played,<br /> W - Win, D - Draw, L - Loss, GF - Goal For, GA - Goal Against</div>
					</div>
					</div>

     		</div>

	         </div>

	    	</div>
      </div>


    </center>
</panel>
	
<script>
    $('.readMoreWithJs').readmore({ maxHeight: 120 });
</script>



