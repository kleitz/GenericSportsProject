<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="conNewsDisplay.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.conNewsDisplay" %>

<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
  
<div class="breadcrumbs">
   <ul>
      <li><asp:Label ID="title" runat="server" ></asp:Label></li>
   </ul>
</div>

<script type="text/javascript">var switchTo5x = true;</script>
<script type="text/javascript" src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/JS/buttons.js")%>"></script>
<script type="text/javascript">stLight.options({ publisher: "204bcd9b-f593-474b-9014-06460f154133", doNotHash: false, doNotCopy: false, hashAddressBar: false });</script>

<center>

<panel id="pnlNewsDisplay" runat="server">
    <header class="pix-heading-title">
        <h2 class="pix-section-title heading-color">
	       <asp:Literal ID="Literal1" runat="server" Text="News"/>
        </h2>
        <div class="right_div_css">
            <span class='st_facebook_large' displayText='Facebook'></span>
            <span class='st_twitter_large' displayText='Tweet'></span>
            <span class='st_instagram_large' displayText='Instagram'></span>
        </div>
    </header>

<asp:Panel ID="pnlGeneralNews" runat="server">
    <asp:Repeater ID="rptrNews" OnItemDataBound="rptrNews_ItemDataBound" runat="server" OnItemCommand="rptrNews_ItemCommand">
            <ItemTemplate>
                    <div class="NewsDisplayPageTitle-MainDiv">
                        <div class="readMoreWithJsTitle">
                           <div class="NewsDisplayPageImage">
                                <asp:Image ID="ltrlImg" Class="NewsImage" ImageUrl='<%# Eval("NewsPicture") %>' runat="server" Width="250px" Height="150px" />
                           </div>
                               <div class="NewsDisplayPageTitle">
                                     <div class="NewsDisplayPageTitle-Name">
                                           <asp:Literal ID="ltrlTitle" Text='<%# Eval("NewsTitle") %>' runat="server"></asp:Literal>
                                     </div>
	                                <div class="NewsDisplayPageTitle-Description" style="float:left;">
                                        <asp:Literal ID="ltrlCompDesc" Text='<%# Eval("NewsDesc") %>' runat="server"></asp:Literal>
                                         
                                    </div>
                                    <div style="float:left;margin-top:14px;"> 
                                       <asp:Label ID="ltrlDate" runat="server" Text='<%#(Eval("CreatedOnDateChange")) %>' class="TeamAllDetail-Date"/>
                                   </div>
                                   <asp:LinkButton ID="likReadMore" runat="server" Text="Read More" CssClass="NewsDisplayReadMoreButton" CommandArgument='<%# Eval("NewsId") %>' CommandName="btnNewsDisplayReadMore">
                                   </asp:LinkButton>
                             </div>
                        </div>
                    </div>
            </ItemTemplate>
        </asp:Repeater> 
</asp:Panel>
</panel>

<asp:PlaceHolder ID="phNewDeatil" runat="server"></asp:PlaceHolder>

</center>

<script>
    $('.readMoreWithJs').readmore({ maxHeight: 120 });
</script>




