<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>


<h1 id="banner"><a href="http://bassistance.de/jquery-plugins/jquery-plugin-validation/">jQuery Validation Plugin</a> Demo</h1>
<div id="main">

<p>Take a look at the source to see how messages can be customized with metadata.</p>

<!-- Custom rules and messages via data- attributes -->
<form class="cmxform" id="commentForm" method="post" action="">
	<fieldset>
		<legend>Please enter your email address</legend>
		<p>

			<label for="cemail">E-Mail *</label>
			<input id="cemail" name="email" data-rule-required="true" data-rule-email="true" data-msg-required="Please enter your email address" data-msg-email="Please enter a valid email address" />
		</p>
		<p>
			<input class="submit" type="submit" value="Submit"/>
		</p>
	</fieldset>
</form>

<!-- Custom message for "required" in metadata is overridden by a validate option -->
<form class="cmxform" id="commentForm2" method="post" action="">
	<fieldset>
		<legend>Please enter your email address</legend>
		<p>

			<label for="cemail">E-Mail *</label>
			<input id="cemail" name="email" data-rule-required="true" data-rule-email="true" data-msg-email="Please enter a valid email address" />
		</p>
		<p>
			<input class="submit" type="submit" value="Submit"/>
		</p>
	</fieldset>
</form>

<a href="<%= SkinPath %>index.html">Back to main page</a>


