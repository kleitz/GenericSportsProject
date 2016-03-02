<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="conNewsComments.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.conNewsComments" %>


<asp:UpdatePanel ID="upnlPollCollect" runat="server">
    <ContentTemplate>
<div style="width:100%;display:inline-block;" >
    <asp:Button ID="btnViewComment" OnClick="btnViewComment_Click" 
                CssClass="NewsViewComment" runat="server" Text="View Comments" />
    <asp:Button ID="btnHideComment" Visible="false" OnClick="btnHideComment_Click" 
                CssClass="NewsViewComment" runat="server" Text="Hide Comments" />
    <asp:Panel ID="pnlViewComments" runat="server">
        <table style="width:100%;">
        <asp:Repeater ID="rptrComments" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="20%"><%# Eval("Name") %></td>
                    <td width="80%"><%# Eval("Comment") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </asp:Panel>
    
    <div class="pix-content-wrap">
		<div id="respond" class="comment-respond">
            <div style="width: 58%;float: left;">
			<h3 id="reply-title" class="comment-reply-title" style="float:left;">
                Leave a Comment 
			</h3>
                </div> 

			<form id="commentform" class="comment-form">
                <div style="width: 58%;float: left;">
               <p class="comment-notes">
                    <asp:Label ID="ltrlSummary" CssClass="dnnFormMessage smallMessage" Visible="false" runat="server"/>
                </p>
                    </div> 
               
                <p class="comment-form-author">
                    <div style="width: 58%;float: left;">
                        <asp:PlaceHolder ID="plchdrLoggedIn" runat="server">
                            <asp:CheckBox ID="chkUseCurrentUser" runat="server" style="float:left;"/>
                        </asp:PlaceHolder>
                        </div> 
                </p><!-- #form-section-author .form-section -->
            
                <p class="comment-form-author">
                    <div class="NewsCommentName">
					    <input id="txtCommentName" runat="server" type="text" name="txtCommentName" placeholder="Name" style="width:92%;" />
                        <label class="form-icons">
						    <small class="fa fa-ellipsis-v"></small>
						    <i class="fa fa-user"></i>
					    </label>
                    </div>
                    
                </p><!-- #form-section-author .form-section -->
                
                <p class="comment-form-email">
                    <div class="NewsCommentName">
                       <input id="txtCommentEmail" runat="server" class="emailinput" type="text" name="txtCommentEmail" placeholder="Email" style="width:92%;"/>
                            <label class="form-icons">
					            <small class="fa fa-ellipsis-v"></small>
					            <i class="fa fa-envelope"></i>
					        </label>
                    </div> 
                </p><!-- #form-section-email .form-section -->
                
                <p class="comment-form-comment fullwidth">
                    <div class="NewsComment">
                        
                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Height="98px"></asp:TextBox>
                    </div> 
                </p><!-- #form-section-comment .form-section -->								
                
                <p class="form-submit">
                    <div style="width: 58%;float: left;margin-top:25px;">
                        <input runat="server" type="submit" id="btnSubmitComment" class="NewsCommentSubmitButton" onserverclick="btnSubmitComment_onserverclick" />
                     </div> 
			    </p>
			
        	</form>
		</div><!-- #respond -->
	</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>

