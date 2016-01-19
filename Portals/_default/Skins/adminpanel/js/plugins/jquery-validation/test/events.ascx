<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>

<form class="cmxform" id="commentForm" method="get" action="">
	<fieldset>
		<legend>A simple comment form with submit validation and default messages</legend>
		<p>
			<label for="cname">Name (required, at least 2 characters)</label>
			<input id="cname" name="name" class="some other styles {required:true,minLength:2}" />
		<p>
			<label for="cemail">E-Mail (required)</label>
			<input id="cemail" name="email" class="{required:true,email:true}" />
		</p>
		<p>
			<label for="curl">URL (optional)</label>
			<input id="curl" name="url" class="{url:true}" value="" />
		</p>
		<p>
			<label for="ccomment">Your comment (required)</label>
			<textarea id="ccomment" name="comment" class="{required:true}"></textarea>
		</p>
		<p>
			<input class="submit" type="submit" value="Submit"/>
		</p>
	</fieldset>
</form>

<button id="remove">Remove focus handler</button>


