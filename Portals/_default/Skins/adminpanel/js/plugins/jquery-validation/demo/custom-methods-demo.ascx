<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>


<h1 id="banner"><a href="http://bassistance.de/jquery-plugins/jquery-plugin-validation/">jQuery Validation Plugin</a> Demo</h1>
<div id="main">

<form class="cmxform" id="texttests" method="get" action="foo.html">
	<h2 id="summary"></h2>

	<fieldset>
		<legend>Example with custom methods and heavily customized error display</legend>
		<table>
			<tr>
				<td><label for="number">textarea</label></td>
				<td><input id="number" name="number"
					title="Please enter a number with at least 3 and max 15 characters!" />
				</td>
				<td></td>
			</tr>
			<tr>
				<td><label for="secret">Secret</label></td>
				<td><input name="secret" id="secret" /></td>
				<td></td>
			</tr>
			<tr>
				<td><label for="math">7 + 4 = </label></td>
				<td><input id="math" name="math" title="Please enter the correct result!" /></td>
				<td></td>
			</tr>
		</table>
		<input class="submit" type="submit" value="Submit"/>
	</fieldset>
</form>

<h3 id="warning">Your form contains tons of errors! Please try again.</h3>

<p><a href="<%= SkinPath %>index.html">Back to main page</a></p>

</div>



