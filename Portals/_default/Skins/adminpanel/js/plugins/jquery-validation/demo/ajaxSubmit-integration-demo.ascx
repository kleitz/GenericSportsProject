<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>


<h1 id="banner"><a href="http://bassistance.de/jquery-plugins/jquery-plugin-validation/">jQuery Validation Plugin</a> Demo</h1>
<div id="main">

<form method="post" class="cmxform" id="form" action="login.action">
	<fieldset>
		<legend>Login Form (Enter "foobar" as password)</legend>
		<p>
			<label for="user">Username</label>
			<input id="user" name="user" title="Please enter your username (at least 3 characters)" class="required" minlength="3" />
		</p>
		<p>
			<label for="pass">Password</label>
			<input type="password" name="password" id="password" class="required" minlength"5" />
		</p>
		<p>
			<input class="submit" type="submit" value="Login"/>
		</p>
	</fieldset>
</form>

<div id="result" class="warning">Please login!</div>

<br/>

<button id="reset">Programmatically reset above form!</button>

<a href="<%= SkinPath %>index.html">Back to main page</a>

</div>



