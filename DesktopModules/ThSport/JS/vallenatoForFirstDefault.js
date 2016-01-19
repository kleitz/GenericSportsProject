/*!
 * Vallenato 1.0
 * A Simple JQuery Accordion
 *
 * Designed by Switchroyale
 * 
 * Use Vallenato for whatever you want, enjoy!
 */

$(document).ready(function () {
    //Add Inactive Class To All Accordion Headers
    $('.accordion-headerDefault').toggleClass('inactive-headerDefault');

    //Set The Accordion Content Width
    var contentwidth = $('.accordion-headerDefault').width();
    $('.accordion-content').css({ 'width': contentwidth });

    //Open The First Accordion Section When Page Loads
    $('.accordion-headerDefault').first().toggleClass('active-headerDefault').toggleClass('inactive-headerDefault');
    $('.accordion-content').first().slideDown().toggleClass('open-content');

    // The Accordion Effect
    $('.accordion-headerDefault').click(function () {
        if ($(this).is('.inactive-headerDefault')) {
            $('.active-headerDefault').toggleClass('active-headerDefault').toggleClass('inactive-headerDefault').next().slideToggle().toggleClass('open-content');
            $(this).toggleClass('active-headerDefault').toggleClass('inactive-headerDefault');
            $(this).next().slideToggle().toggleClass('open-content');
        }

        else {
            $(this).toggleClass('active-headerDefault').toggleClass('inactive-headerDefault');
            $(this).next().slideToggle().toggleClass('open-content');
        }
    });

    return false;
});