<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>


<h1 id="banner"><a href="http://bassistance.de/jquery-plugins/jquery-plugin-validation/">jQuery Validation Plugin</a> Demo</h1>
<div id="main">

<textarea style="display:none" id="template">
	<tr>
		<td>
			<label>{0}. Item</label>
		</td>
		<td class='type'>
			<select name="item-type-{0}">
				<option value="">Select...</option>
				<option value="0">Learning jQuery</option>
				<option value="1">jQuery Reference Guide</option>
				<option value="2">jQuery Cookbook</option>
				<option vlaue="3">jQuery In Action</option>
				<option value="4">jQuery For Designers</option>
			</select>
		</td>
		<td class='quantity'>
			<input size='4' class="quantity" min="1" id="item-quantity-{0}" name="item-quantity-{0}" />
		</td>
		<td class='quantity-error'></td>
	</tr>
</textarea>

<form id="orderform" class="cmxform" method="get" action="foo.html">
	<h2 id="summary"></h2>

	<fieldset>
		<legend>Example with custom methods and heavily customized error display</legend>
		<table id="orderitems">
			<tbody>

			</tbody>
			<tfoot>
				<tr>
					<td colspan="2"><label>Totals (max 25)</label></td>
					<td class="totals"><input id="totals" name="totals" value="0" max="25" readonly="readonly" size='4' /></td>
					<td class="totals-error"></td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
					<td><input class="submit" type="submit" value="Submit"/></td>
				</tr>
			</tfoot>
		</table>
	</fieldset>
</form>

<button id="add">Add another input to the form</button>

<h1 id="warning">Your form contains tons of errors! Please try again.</h1>

<p><a href="<%= SkinPath %>index.html">Back to main page</a></p>

</div>



