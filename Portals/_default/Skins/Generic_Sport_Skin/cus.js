$(document).ready(function () {   

		$("a[rel^='prettyPhoto']").prettyPhoto(
		{
		allow_resize: true, /* Resize the photos bigger than viewport. true/false */
		}); 
		
		$('.bxslider').bxSlider({
		  
			mode: 'horizontal',
			slideMargin: 3,
			auto: true
		});
		
		$('.bxsliderfor').bxSlider({
		  
			mode: 'horizontal',

			auto: true
		});

		$('.Newsslider').bxSlider({
			mode: 'vertical',
			slideMargin: 0,
			auto:true
		}); 

		$('.flexslider').flexslider({
			animation: "slide",
			controlNav: false,
			start: function(slider){
			$('body').removeClass('loading');
		}
		});
     });
		 
		 (function($) {
	$(function() {
		//$("#sponsorscroller").simplyScroll();
	});
})(jQuery);