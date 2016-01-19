<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>

<!-- start page wrapper --><div id="letterbox">

<!-- start header container -->
<div id="header-background">
  <div class="nav-global-container">
    <div class="login"><a href="<%= SkinPath %>#"><span></span>Customer Login</a></div>
    <div class="logo"><a href="<%= SkinPath %>#"><img src="<%= SkinPath %>images/logo_marketo.gif" width="168" height="73"  alt="Marketo" /></a></div>
		<div class="nav-global">
			<ul>
    		<li><a href="<%= SkinPath %>#" class="nav-g01"><span></span>Home</a></li>
  			<li><a href="<%= SkinPath %>#" class="nav-g02"><span></span>Products</a></li>
  			<li><a href="<%= SkinPath %>#" class="nav-g04"><span></span>B2B Marketing Resources</a></li>
  			<li><a href="<%= SkinPath %>#" class="nav-g05"><span></span>About Marketo</a></li>
			</ul>
		</div>
	</div>
</div>
<!-- end header container -->
<div class="line-grey-tier"></div>

<!-- start page container 2 div-->
<div id="page-container" class="resize"><div id="page-content-inner" class="resize">

<!-- start col-main -->

<div id="col-main" class="resize" style="">



  <!-- start main content  -->
  <div class="main-content resize">

  <div class="action-container" style="display:none;"></div>


<h1>Step 1 of 2 </h1>
<p>
</p>
<br clear="all" />
<div>
  <form id="profileForm" type="actionForm" action="step2.htm" method="get" >


    <div class="error" style="display:none;">
      <img src="<%= SkinPath %>images/warning.gif" alt="Warning!" width="24" height="24" style="float:left; margin: -5px 10px 0px 0px; " />

      <span></span>.<br clear="all"/>
    </div>


    <table cellpadding="0" cellspacing="0" border="0">
      <tr>
        <td class="label"><label for="co_name">Company Name:</label></td>
        <td class="field">
          <input id="co_name" class="required" maxlength="40" name="co_name" size="20" type="text" tabindex="1" value="" />
        </td>
      </tr>
      <tr>
        <td class="label"><label for="co_url">Company URL:</label></td>
        <td class="field">
          <input id="co_url" class="required defaultInvalid url" maxlength="40" name="co_url" style="width:163px" type="text" tabindex="2" value="http://" />
        </td>
      </tr>
      <tr>
        <td/><td/>
      </tr>
      <tr>
        <td class="label"><label for="first_name">First Name:</label></td>
        <td class="field">
          <input id="first_name" class="required" maxlength="40" name="first_name" size="20" type="text" tabindex="3" value="" />
        </td>
      </tr>
      <tr>
        <td class="label"><label for="last_name">Last Name:</label></td>
        <td class="field">
          <input id="last_name" class="required" maxlength="40" name="last_name" size="20" type="text" tabindex="4" value="" />
        </td>
      </tr>
      <tr>
        <td class="label"><label for="address1">Company Address:</label></td>
        <td class="field">
          <input  maxlength="40" class="required" name="address1" size="20" type="text" tabindex="5" value="" />
        </td>
      </tr>
      <tr>
        <td class="label"></td>
        <td class="field">
          <input  maxlength="40" name="address2" size="20" type="text" tabindex="6" value="" />
        </td>
      </tr>
      <tr>
        <td class="label"><label for="city">City:</label></td>
        <td class="field">
          <input  maxlength="40" class="required" name="city" size="20" type="text" tabindex="7" value="" />
        </td>
      </tr>
      <tr>
        <td class="label"><label for="state">State:</label></td>
        <td class="field">
          <select id="state" class="required" name="state" style="margin-left: 4px;" tabindex="8">
            <option value="">Choose State:</option>
            <option value="AL">Alabama</option><option value="AK">Alaska</option><option value="AZ">Arizona</option><option value="AR">Arkansas</option><option value="CA">California</option><option value="CO">Colorado</option><option value="CT">Connecticut</option><option value="DE">Delaware</option><option value="FL">Florida</option><option value="GA">Georgia</option><option value="HI">Hawaii</option><option value="ID">Idaho</option><option value="IL">Illinois</option><option value="IN">Indiana</option><option value="IA">Iowa</option><option value="KS">Kansas</option><option value="KY">Kentucky</option><option value="LA">Louisiana</option><option value="ME">Maine</option><option value="MD">Maryland</option><option value="MA">Massachusetts</option><option value="MI">Michigan</option><option value="MN">Minnesota</option><option value="MS">Mississippi</option><option value="MO">Missouri</option><option value="MT">Montana</option><option value="NE">Nebraska</option><option value="NV">Nevada</option><option value="NH">New Hampshire</option><option value="NJ">New Jersey</option><option value="NM">New Mexico</option><option value="NY">New York</option><option value="NC">North Carolina</option><option value="ND">North Dakota</option><option value="OH">Ohio</option><option value="OK">Oklahoma</option><option value="OR">Oregon</option><option value="PA">Pennsylvania</option><option value="RI">Rhode Island</option><option value="SC">South Carolina</option><option value="SD">South Dakota</option><option value="TN">Tennessee</option><option value="TX">Texas</option><option value="UT">Utah</option><option value="VT">Vermont</option><option value="VA">Virginia</option><option value="WA">Washington</option><option value="WV">West Virginia</option><option value="WI">Wisconsin</option><option value="WY">Wyoming</option>
          </select>
        </td>
      </tr>

      <tr>
        <td class="label"><label for="zip">Zip:</label></td>
        <td class="field">
          <input  maxlength="10" name="zip" style="width: 100px" type="text" class="required zipcode" tabindex="9" value="" />
        </td>
      </tr>
      <tr>
        <td class="label"><label for="phone">Phone:</label></td>
        <td class="field">
          <input id="phone" maxlength="14" name="phone" type="text" class="required phone" tabindex="10" value="" />
        </td>
      </tr>



      <tr>
        <td colspan="2">
          <h2 style="border-bottom: 1px solid #CCCCCC;">Login Information</h2>
        </td>
      </tr>


      <tr>
        <td class="label"><label for="email">Email:</label></td>
        <td class="field">
          <input id="email" class="required email" remote="emails.action" maxlength="40" name="email" size="20" type="text" tabindex="11" value="" />
        </td>
      </tr>

      <tr>
        <td class="label"><label for="password1">Password:</label></td>
        <td class="field">
        	<input id="password1" class="required password" maxlength="40" name="password1" size="20" type="password" tabindex="12" value="" />
        </td>
      </tr>
      <tr>
        <td class="label"><label for="password2">Retype Password:</label></td>
        <td class="field">
          <input id="password2" class="required" equalTo="#password1" maxlength="40" name="password2"  size="20" type="password" tabindex="13" value="" />
          <div class="formError"></div>
        </td>
      </tr>
      <tr>
        <td></td>
        <td>
          <div class="buttonSubmit">
            <span></span>
            <input class="formButton" type="submit" value="Next" style="width: 140px" tabindex="14" />
          </div>

        </td>
      </tr>
    </table><br /><br />
  </form>
  <br clear="all"/>


</div>



	</div>	<!-- end main content  -->
	<br />
</div> <!-- end col-main -->

<!-- start left col -->
<div id="col-left" class="nav-left-back empty resize" style="position: absolute; min-height: 450px;">
  <div class="col-left-header-tab" style="position: absolute;">Signup</div>
  <div class="nav-left">

  </div>


      <div class="left-nav-callout png" style="top: 15px; margin-bottom: 100px;">
        <img src="<%= SkinPath %>images/left-nav-callout-long.png"  class="png" alt="" />
        <h6>Sign Up Process</h6>
        <a style="background-image: url(images/step1-24.gif); font-weight: normal; text-decoration: none; cursor: default;">Sign up with a valid credit card.</a>
        <a style="background-image: url(images/step2-24.gif); font-weight: normal; text-decoration: none; cursor: default;">Connect to your Google AdWords account.  You will need your AdWords Customer ID.</a>
        <a style="background-image: url(images/step3-24.gif); font-weight: normal; text-decoration: none; cursor: default;">Start your 30 day trial.  No payments until trial ends.</a>
      </div>

<div class="footerAddress">
<b>Marketo Inc.</b><br />
1710 S. Amphlett Blvd.<br />
San Mateo, CA 94402 USA<br />
</div>
<br clear="all"/>
</div>	<!-- end left col -->

</div>  </div>  <!-- end page container 2 divs-->

  <div id="footer-container" align="center">
   <div class="footer">
    <ul>
    <li><a href="<%= SkinPath %>..">Home</a></li>
    <li class="line-off"><a href="<%= SkinPath %>step2.htm">Second step</a></li>
    </ul>
    </div></div>



<!-- end page wrapper -->
</div>

    
