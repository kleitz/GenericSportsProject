<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>

<a id="git-fork" href="https://github.com/rochal/jQuery-slimScroll"><img src="https://s3.amazonaws.com/github/ribbons/forkme_right_red_aa0000.png" alt="Fork me on GitHub"></a>
<div class="examples">


  <div id="testDiv">
  </div>

  <pre class="prettyprint">
  // update content every second
  setInterval(function(){
    var el = $('&lt;div&gt;&lt;/div&gt;').html('#' + $('#testDiv').children().length)
            .css({ padding: '3px', border: '1px solid #ccc', margin: '5px' });
    $('#testDiv').append(el);

    // update slimscroll every time content changes
    $('#testDiv').slimscroll();
  }, 1000);

  $('#testDiv').slimscroll({
    alwaysVisible: true,
    height: 150
  });
  </pre>



</div>

<script type="text/javascript">
    $(function(){

      // update content every second
      setInterval(function(){
        var el = $('<div></div>').html('#' + $('#testDiv').children().length)
                .css({ padding: '3px', border: '1px solid #ccc', margin: '5px' });
        $('#testDiv').append(el);

        // update slimscroll every time content changes
        $('#testDiv').slimscroll();
      }, 1000);

      $('#testDiv').slimscroll({
        alwaysVisible: true,
        height: 150
      });

    });
</script>


<script type="text/javascript">

  //enable syntax highlighter
  prettyPrint();

  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-3112455-22']);
  _gaq.push(['_setDomainName', 'none']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();
</script>

