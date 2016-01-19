<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>

	<h1 id="qunit-header">
		<a href="http://bassistance.de/jquery-plugins/jquery-plugin-validation/">jQuery Validation Plugin</a> Test Suite
		<a href="<%= SkinPath %>?jquery=1.6.4">jQuery 1.6.4</a>
		<a href="<%= SkinPath %>?jquery=1.7.2">jQuery 1.7.2</a>
		<a href="<%= SkinPath %>?jquery=1.8.3">jQuery 1.8.3</a>
		<a href="<%= SkinPath %>?jquery=1.9.0">jQuery 1.9.0</a>
		<a href="<%= SkinPath %>?jquery=git">jQuery Latest (git)</a>
		</h1>
	<div>
	</div>
	<h2 id="qunit-banner"></h2>
	<div id="qunit-testrunner-toolbar"></div>
	<h2 id="qunit-userAgent"></h2>
	<ol id="qunit-tests"></ol>

	<!-- Test HTML -->
	<div id="other" style="display:none;">
		<input type="password" name="pw1" id="pw1" value="engfeh" />
		<input type="password" name="pw2" id="pw2" value="" />
	</div>
	<div id="qunit-fixture">
		<p id="firstp">See <a id="simon1" href="http://simon.incutio.com/archive/2003/03/25/#getElementsBySelector" rel="bookmark">this blog entry</a> for more information.</p>
		<p id="ap">
			Here are some links in a normal paragraph: <a id="google" href="http://www.google.com/" title="Google!">Google</a>,
			<a id="groups" href="http://groups.google.com/">Google Groups</a>.
			This link has <code><a href="<%= SkinPath %>#" id="anchor1">class="blog"</a></code>:
			<a href="http://diveintomark.org/" class="blog" hreflang="en" id="mark">diveintomark</a>

		</p>
		<div id="foo">
			<p id="sndp">Everything inside the red border is inside a div with <code>id="foo"</code>.</p>
			<p lang="en" id="en">This is a normal link: <a id="yahoo" href="http://www.yahoo.com/" class="blogTest">Yahoo</a></p>
			<p id="sap">This link has <code><a href="<%= SkinPath %>#2" id="anchor2">class="blog"</a></code>: <a href="http://simon.incutio.com/" class="blog link" id="simon">Simon Willison's Weblog</a></p>

		</div>
		<p id="first">Try them out:</p>
		<ul id="firstUL"></ul>
		<ol id="empty"></ol>

		<form id="testForm1">
			<input type="text" data-rule-required="true" data-rule-minlength="2" title="buga" name="firstname" id="firstname" />
			<label id="errorFirstname" for="firstname" class="error">error for firstname</label>
			<input type="text" data-rule-required="true" title="buga" name="lastname" id="lastname" />
			<input type="text" data-rule-required="true" title="something" name="something" id="something" value="something" />
		</form>

		<form id="testForm1clean">
			<input title="buga" name="firstname" id="firstnamec" />
			<label id="errorFirstname" for="firstname" class="error">error for firstname</label>
			<input title="buga" name="lastname" id="lastnamec" />
			<input name="username" id="usernamec" />
		</form>

		<form id="userForm">
			<input type="text" data-rule-required="true" name="username" id="username" />
			<input type="submit" name="submitButton" value="submitButtonValue" />
		</form>

		<form id="signupForm" action="form.php">
			<input id="user" name="user" title="Please enter your username (at least 3 characters)" data-rule-required="true" data-rule-minlength="3" />
			<input type="password" name="password" id="password" data-rule-required="true" data-rule-minlength="5" />
		</form>

		<form id="testForm2">
			<input data-rule-required="true" type="radio" name="agree" id="agb" />
			<label for="agree" id="agreeLabel" class="xerror">error for agb</label>
		</form>

		<form id="testForm3">
			<select data-rule-required="true" name="meal" id="meal" >
				<option value="">Please select...</option>
				<option value="1">Food</option>
				<option value="2">Milk</option>
			</select>
		</form>
		<div class="error" id="errorContainer">
			<ul>
				<li class="error" id="errorWrapper">
					<label for="meal" id="mealLabel" class="error">error for meal</label>
				</li>
			</ul>
		</div>

		<form id="testForm4">
			<input data-rule-foo="true" name="f1" id="f1" />
			<input data-rule-bar="true" name="f2" id="f2" />
		</form>

		<form id="testForm5">
			<input data-rule-equalto="#x2" value="x" name="x1" id="x1" />
			<input data-rule-equalto="#x1" value="y" name="x2" id="x2" />
		</form>

		<form id="testForm6">
			<input data-rule-required="true" data-rule-minlength="2" type="checkbox" name="check" id="form6check1" />
			<input type="checkbox" name="check" id="form6check2" />
		</form>

		<form id="testForm7">
			<select data-rule-required="true" data-rule-minlength="2" name="selectf7" id="selectf7" multiple="multiple">
				<option id="optionxa" value="0">0</option>
				<option id="optionxb" value="1">1</option>
				<option id="optionxc" value="2">2</option>
				<option id="optionxd" value="3">3</option>
			</select>
		</form>

		<form id="dateRangeForm">
			<input id="fromDate" name="fromDate" class="requiredDateRange" value="x" />
			<input id="toDate" name="toDate" class="requiredDateRange" value="y" />
			<span class="errorContainer"></span>
		</form>

		<form id="testForm8">
			<input id="form8input" data-rule-required="true" data-rule-number="true" data-rule-rangelength="2,8" name="abc" />
			<input type="radio" name="radio1"/>
		</form>

		<form id="testForm9">
			<input id="testEmail9" data-rule-required="true" data-rule-email="true" data-msg-required="required" data-msg-email="email" />
		</form>

		<form id="testForm10">
			<input type="radio" name="testForm10Radio" value="1" id="testForm10Radio1" />
			<input type="radio" name="testForm10Radio" value="2" id="testForm10Radio2" />
		</form>

		<form id="testForm11">
			<!-- HTML5 -->
			<input required type="text" name="testForm11Text" id="testForm11text1" />
		</form>

		<form id="testForm12">
			<!-- empty "type" attribute -->
			<input name="testForm12text" id="testForm12text" data-rule-required="true" />
		</form>

		<form id="dataMessages">
			<input name="dataMessagesName" id="dataMessagesName" class="required" data-msg-required="You must enter a value here" />
		</form>

		<div id="simplecontainer">
			<h3></h3>
		</div>

		<div id="container" style="min-height:1px"></div>

		<ol id="labelcontainer"></ol>

		<form id="elementsOrder">
			<select class="required" name="order1" id="order1"><option value="">none</option></select>
			<input class="required" name="order2" id="order2"/>
			<input class="required" name="order3" type="checkbox" id="order3"/>
			<input class="required" name="order4" id="order4"/>
			<input class="required" name="order5" type="radio" id="order5"/>
			<input class="required" name="order6" id="order6"/>
			<ul id="orderContainer">
			</ul>
		</form>

		<form id="form" action="formaction">
			<input type="text" name="action" value="Test" id="text1"/>
			<input type="text" name="text2" value="   " id="text1b"/>
			<input type="text" name="text2" value="T " id="text1c"/>
			<input type="text" name="text2" value="T" id="text2"/>
			<input type="text" name="text2" value="TestTestTest" id="text3"/>

			<input type="text" name="action" value="0" id="value1"/>
			<input type="text" name="text2" value="10" id="value2"/>
			<input type="text" name="text2" value="1000" id="value3"/>

			<input type="radio" name="radio1" id="radio1"/>
			<input type="radio" name="radio1" id="radio1a"/>
			<input type="radio" name="radio2" id="radio2" checked="checked"/>
			<input type="radio" name="radio" id="radio3"/>
			<input type="radio" name="radio" id="radio4" checked="checked"/>

			<input type="checkbox" name="check" id="check1" checked="checked"/>
			<input type="checkbox" name="check" id="check1b" />

			<input type="checkbox" name="check2" id="check2"/>

			<input type="checkbox" name="check3" id="check3" checked="checked"/>
			<input type="checkbox" name="check3" checked="checked"/>
			<input type="checkbox" name="check3" checked="checked"/>
			<input type="checkbox" name="check3" checked="checked"/>
			<input type="checkbox" name="check3" checked="checked"/>

			<input type="hidden" name="hidden" id="hidden1"/>
			<input type="text" style="display:none;" name="foo[bar]" id="hidden2"/>

			<input type="text" readonly="readonly" id="name" name="name" value="name" />

			<button name="button">Button</button>

			<textarea id="area1" name="area1">foobar</textarea>


			<textarea id="area2" name="area2"></textarea>

			<select name="select1" id="select1">
				<option id="option1a" value="">Nothing</option>
				<option id="option1b" value="1">1</option>
				<option id="option1c" value="2">2</option>
				<option id="option1d" value="3">3</option>
			</select>
			<select name="select2" id="select2">
				<option id="option2a" value="">Nothing</option>
				<option id="option2b" value="1">1</option>
				<option id="option2c" value="2">2</option>
				<option id="option2d" selected="selected" value="3">3</option>
			</select>
			<select name="select3" id="select3" multiple="multiple">
				<option id="option3a" value="">Nothing</option>
				<option id="option3b" selected="selected" value="1">1</option>
				<option id="option3c" selected="selected" value="2">2</option>
				<option id="option3d" value="3">3</option>
			</select>
			<select name="select4" id="select4" multiple="multiple">
				<option id="option4a" selected="selected" value="1">1</option>
				<option id="option4b" selected="selected" value="2">2</option>
				<option id="option4c" selected="selected" value="3">3</option>
				<option id="option4d" selected="selected" value="4">4</option>
				<option id="option4e" selected="selected" value="5">5</option>
			</select>
			<select name="select5" id="select5" multiple="multiple">
				<option id="option5a" value="0">0</option>
				<option id="option5b" value="1">1</option>
				<option id="option5c" value="2">2</option>
				<option id="option5d" value="3">3</option>
			</select>
		</form>

		<form id="v2">
			<input id="v2-i1" name="v2-i1" class="required" />
			<input id="v2-i2" name="v2-i2" class="required email" />
			<input id="v2-i3" name="v2-i3" class="url" />
			<input id="v2-i4" name="v2-i4" class="required" minlength="2" />
			<input id="v2-i5" name="v2-i5" class="required" minlength="2" maxlength="5" customMethod1="123" />
			<input id="v2-i6" name="v2-i6" class="required customMethod2" data-rule-maxlength="5" data-rule-minlength="2" />
			<input id="v2-i7" name="v2-i7" />
		</form>

		<form id="checkables">
			<input type="checkbox" id="checkable1" name="checkablesgroup" class="required" />
			<input type="checkbox" id="checkable2" name="checkablesgroup" />
			<input type="checkbox" id="checkable3" name="checkablesgroup" />
		</form>

		<form id="subformRequired">
			<div class="billingAddressControl">
            	<input type="checkbox" id="bill_to_co" name="bill_to_co" class="toggleCheck" checked="checked" style="width: auto;" tabindex="1" />
            	<label for="bill_to_co" style="cursor:pointer">Same as Company Address</label>
          	</div>
			<div id="subform">
				<input  maxlength="40" class="billingRequired" name="bill_first_name" size="20" type="text" tabindex="2" value="" />
			</div>
			<input id="co_name" class="required" maxlength="40" name="co_name" size="20" type="text" tabindex="1" value="" />
		</form>

		<form id="withTitle">
			<input class="required" name="hastitle" type="text" title="fromtitle" />
		</form>

		<form id="ccform" method="get" action="">
			<input id="cardnumber" name="cardnumber" />
		</form>

		<form id="productInfo">
			<input class="productInfo" name="partnumber">
			<input class="productInfo" name="description">
			<input class="productInfo" name="color">
			<input class="productInfo" type="checkbox" name="discount" />
		</form>

		<form id="updateLabel">
			<input class="required" name="updateLabelInput" id="updateLabelInput" data-msg-required="You must enter a value here" />
			<label id="targetLabel" class="error" for="updateLabelInput">Some server-side error</label>
		</form>

		<form id="rangesMinDateInvalid">
			<input type="date" id="minDateInvalid" name="minDateInvalid" min="2012-12-21" value="2012-11-21"/>
		</form>
		<form id="ranges">
			<input type="date" id="maxDateInvalid" ngame="maxDateInvalid" max="2012-12-21" value="2013-01-21"/>
			<input type="date" id="rangeDateInvalidGreater" name="rangeDateInvalidGreater" min="2012-11-21" max="2013-01-21" value="2013-02-21"/>
			<input type="date" id="rangeDateInvalidLess" name="rangeDateInvalidLess" min="2012-11-21" max="2013-01-21" value="2012-10-21"/>

			<input type="date" id="maxDateValid" name="maxDateValid" max="2013-01-21" value="2012-12-21"/>
			<input type="date" id="rangeDateValid" name="rangeDateValid" min="2012-11-21" max="2013-01-21" value="2012-12-21"/>

			<!-- input type text is not supposed to have min/max according to html5,
			     but for backward compatibility with 1.10.0 we treat it as number.
			     you can also use type="number", in which case the browser may also
			     do validation, and mobile browsers may offer a numeric keypad to edit 
			     the value.
			     Type absent is treated like type="text".
			  -->
			<input type="text" id="rangeTextInvalidGreater" name="rangeTextInvalidGreater" min="50" max="200" value="1000"/>
			<input type="text" id="rangeTextInvalidLess" name="rangeTextInvalidLess" min="200" max="1000" value="50"/>
			<input id="rangeAbsentInvalidGreater" name="rangeAbsentInvalidGreater" min="50" max="200" value="1000"/>
			<input id="rangeAbsentInvalidLess" name="rangeAbsentInvalidLess" min="200" max="1000" value="50"/>

			<input type="text" id="rangeTextValid" name="rangeTextValid" min="50" max="1000" value="200"/>
			<input id="rangeAbsentValid" name="rangeTextValid" min="50" max="1000" value="200"/>

			<!-- ranges are like numbers in html5, except that browser is not required 
			     to demand an exact value.  User interface could be a slider.
			  -->
			<input type="range" id="rangeRangeValid" name="rangeRangeValid" min="50" max="1000" value="200"/>

			<input type="number" id="rangeNumberValid" name="rangeNumberValid" min="50" max="1000" value="200"/>
			<input type="number" id="rangeNumberInvalidGreater" name="rangeNumberInvalidGreater" min="50" max="200" value="1000"/>
			<input type="number" id="rangeNumberInvalidLess" name="rangeNumberInvalidLess" min="50" max="200" value="6"/>

		</form>
		<form id="rangeMinDateValid">
			<input type="date" id="minDateValid" name="minDateValid" min="2012-11-21" value="2012-12-21"/>
		</form>
		
		<form id="bypassValidation">
				<input type="text" required/>
				<input id="normalSubmit" type="submit" value="submit"/>
				<input id="bypassSubmitWithCancel" type="submit" class="cancel" value="bypass1"/>
				<input id="bypassSubmitWithNoValidate1" type="submit" formnovalidate value="bypass1"/>
				<input id="bypassSubmitWithNoValidate2" type="submit" formnovalidate="formnovalidate" value="bypass2"/>
		</form>
	</div>


