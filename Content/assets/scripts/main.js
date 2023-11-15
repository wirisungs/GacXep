/*Js danh mục sp*/

$('.xemthem').click(function (e) {
	e.preventDefault();
	$('.aside-vetical-menu .aside-content>.nav-category>.ul>.nav-item').css('display', 'block');
	$(this).hide();
	$('.thugon').show();
})
$('.thugon').click(function (e) {
	e.preventDefault();
	$('.aside-vetical-menu .aside-content>.nav-category>.ul>.nav-item').css('display', 'none');
	$(this).hide();
	$('.xemthem').show();
})

$(document).on('keydown', '.fixnumber', function (e) { -1 !== $.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) || /65|67|86|88/.test(e.keyCode) && (!0 === e.ctrlKey || !0 === e.metaKey) || 35 <= e.keyCode && 40 >= e.keyCode || (e.shiftKey || 48 > e.keyCode || 57 < e.keyCode) && (96 > e.keyCode || 105 < e.keyCode) && e.preventDefault() });


$(document).ready(function () {
	scrollSliderBarMenu();
	$(".verticalmenu .button-verticalmenu").click(function () {
		var $parent = $(this).parent().parent();
		$parent.toggleClass('open')
		return false;
	});
});

function scrollSliderBarMenu() {
	var menuElement = $(".float-vertical");
	var columnElement = null;
	var maxWindowSize = 990;
	if ($(menuElement).hasClass('float-vertical-right'))
		columnElement = $("#right_column");
	else if ($(menuElement).hasClass('float-vertical-left'))
		columnElement = $("#left_column");
	if ($(columnElement).length && $(window).width() >= maxWindowSize) showOrHideSliderBarMenu(columnElement, menuElement, 1);
	$(".float-vertical-button").click(function () {
		if ($(menuElement).hasClass('active')) showOrHideSliderBarMenu(columnElement, menuElement, 0);
		else showOrHideSliderBarMenu(columnElement, menuElement, 1);
	});
	var lastWidth = $(window).width();
	$(window).resize(function () {
		if ($(window).width() != lastWidth) {
			if ($(window).width() < maxWindowSize) {
				if ($(menuElement).hasClass('active')) showOrHideSliderBarMenu(columnElement, menuElement, 0);
			} else {
				if ($(columnElement).length && !$(menuElement).hasClass('active')) showOrHideSliderBarMenu(columnElement, menuElement, 1);
			}
			lastWidth = $(window).width();
		}
	});
}

function showOrHideSliderBarMenu(columnElement, menuElement, active) {
	if (active) {
		$(menuElement).addClass('active');
		$('.bg-vertical').addClass('fixed');
		if ($(columnElement).length && $(window).width() >= 990)
			columnElement.css('padding-top', ($('.block_content', $(menuElement)).height() - 90) + 'px');
	} else {
		$(menuElement).removeClass('active');
		if ($(columnElement).length) columnElement.css('padding-top', '');
		$('.bg-vertical').removeClass('fixed');
	}
}
$(".bg-vertical").click(function () {
	if ($(this).hasClass('fixed')) {
		$('.bg-vertical').removeClass('fixed');
		$('.float-vertical-button-col').parent().removeClass('active');
	}
});