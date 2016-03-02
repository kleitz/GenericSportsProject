<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="conNewsDetail.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.conNewsDetail" %>

<script type="text/javascript">var switchTo5x = true;</script>
<script type="text/javascript" src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/JS/buttons.js")%>"></script>
<script type="text/javascript">stLight.options({ publisher: "204bcd9b-f593-474b-9014-06460f154133", doNotHash: false, doNotCopy: false, hashAddressBar: false });</script>

<div class="breadcrumbs">
   <ul>
      <li><asp:Label ID="title" runat="server" ></asp:Label></li>
   </ul>
</div>

<center>

<panel id="pnlNewsDetail" runat="server">
     
      <div class="news_all_div">
          
          <div class="news_img_div">
              <asp:Image ID="imgNews" runat="server" CssClass="news_img_css"/>
          </div>
          <div class="news_detail_div">
              <div class="right_div_css">
                <span class="st_facebook_large" displaytext="Facebook" st_processed="yes"><span style="text-decoration:none;color:#000000;display:inline-block;cursor:pointer;" class="stButton"><span class="stLarge" style="background-image: url(http://w.sharethis.com/images/facebook_32.png);"></span></span></span>
                <span class="st_twitter_large" displaytext="Tweet" st_processed="yes"><span style="text-decoration:none;color:#000000;display:inline-block;cursor:pointer;" class="stButton"><span class="stLarge" style="background-image: url(http://w.sharethis.com/images/twitter_32.png);"></span></span></span>
                <span class="st_instagram_large" displaytext="Instagram" st_processed="yes"><span style="text-decoration:none;color:#000000;display:inline-block;cursor:pointer;" class="stButton"><span class="stLarge" style="background-image: url(http://w.sharethis.com/images/instagram_32.png);"></span></span></span>
            </div>
           <div class="NewsSinglePageTitle-Name">
                    <asp:Literal ID="ltrlTitle" Text='<%# Eval("NewsTitle") %>' runat="server"></asp:Literal>
           </div>

           <div class="NewsSinglePage-Date"> 
                <asp:Label ID="ltrlDate" runat="server" Text='<%#(Eval("CreatedOnDateChange")) %>' class="TeamAllDetail-Date"/>
           </div>


           <div class="NewsSinglePage-Descri">
                <asp:Literal ID="ltrlCompDesc" Text='<%# Eval("NewsDesc") %>' runat="server"></asp:Literal>
           </div>

              </div>

          <div class="share-post" style="width:100%;" id="divnewssingle" runat="server" visible="false">
              <div class="post-tags" style="float: left !important;padding-top: 50px !important;">
                  tags: 
                  <asp:LinkButton ID="likGolf" runat="server" Text="Golf" OnClick="likGolf_Click">

                  </asp:LinkButton>

                  <asp:LinkButton ID="likKick" runat="server" Text="Kick" OnClick="likKick_Click">

                  </asp:LinkButton>

                  <asp:LinkButton ID="likPlayer" runat="server" Text="Player" OnClick="likPlayer_Click">

                  </asp:LinkButton>

                  <asp:LinkButton ID="likSports" runat="server" Text="Sports" OnClick="likSports_Click">

                  </asp:LinkButton>
              </div>
              <ul class="social-network" style="float: right !important;padding-top: 50px !important;display:none;">
                  <a class="addthis_button_compact btn share-now pix-bgcolr">
                      <i class="fa fa-share-square-o"></i>Share Now</a>

              </ul>  

          </div>
      

          <div class="prev-nex-btn">
               <div class="single-paginate">
			        <div class="next-post-paginate">
                         <asp:LinkButton ID="lbPreviousPost" runat="server" Text="Previous Post" OnClick="lbPreviousPost_Click" CssClass="pix-colr" Visible="false">
                         </asp:LinkButton> 
                        
               <h2 class="px-single-page-title" style="text-align:left;">
                   <asp:Literal ID="litPrv" Text='<%# Eval("NewsTitle") %>' runat="server"></asp:Literal>
                   <asp:HiddenField ID="hdPrv" runat="server" Value='<%# Eval("NewsId") %>'/>
               </h2>

               <ul style="text-align:left;">
                    <li style="float: left !important;margin-right: 5px;margin-left: -16px;">
                        <asp:HyperLink ID="lbPrvReadMore" runat="server" Text="Read More"></asp:HyperLink>
                         
                    </li>  
                    <li><asp:Label ID="lbPrvDate" runat="server" Text='<%#(Eval("CreatedOnDateChange")) %>' class="TeamAllDetail-Date"/></li>
               </ul>
               </div>
               
               <div class="next-post-paginate">
                   <div style="float:right;">
                        <asp:LinkButton ID="lbNextPost" runat="server" Text="Next Post" OnClick="lbNextPost_Click" CssClass="pix-colr" Visible="false">
                        </asp:LinkButton>
                   </div>
                
                    <h2 class="px-single-page-title" style="text-align:left;">
                         <asp:HiddenField ID="hdNextNewsId" runat="server" Value='<%# Eval("NewsId") %>'/>
                        <asp:Literal ID="litNext" Text='<%# Eval("NewsTitle") %>' runat="server"></asp:Literal>
                    </h2>

               <ul style="text-align: left;">

               <li style="margin-left:-16px;">
                   <asp:HyperLink ID="lbNextReadMore" runat="server" Text="Read More"></asp:HyperLink>
                    
               </li>  
               <li>
                        <asp:Label ID="lbNextDate" runat="server" Text='<%#(Eval("CreatedOnDateChange")) %>' class="NewsNext-Date"/>
              </li>
               </ul>
                </div>
              </div>
          </div>
    

</panel>

<asp:PlaceHolder ID="phNewsComment" runat="server"></asp:PlaceHolder>

</center>

<script>
    $('.readMoreWithJs').readmore({ maxHeight: 120 });
</script>




