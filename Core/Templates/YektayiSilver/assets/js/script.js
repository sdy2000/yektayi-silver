// JavaScript Document
jQuery(document).ready(function($){
	"use strict";
	$('.product-panel-form').owlCarousel({
		loop:true,
		margin:20,
		nav:true,
		rtl:true,
		items: 1,
		autoplay:true,
		autoplayTimeout:5000,
		autoplayHoverPause:true,
		navText:['',''],
		responsive:{
        0:{
            items:1
        },
		400:{
            items:2
        },
        600:{
            items:3
        },
        1000:{
            items:4
        }
    }
	});
	$('.blog-panel-form').owlCarousel({
		loop:true,
		margin:20,
		nav:false,
		rtl:true,
		items: 1,
		autoplay:true,
		autoplayTimeout:5000,
		autoplayHoverPause:true,
		navText:['',''],
		responsive:{
        0:{
            items:1
        },
        600:{
            items:1
        },
        1000:{
            items:1
        }
    }
	});
	$('.blog-panel-car').owlCarousel({
		loop:true,
		margin:20,
		nav:true,
		rtl:true,
		items: 1,
		autoplay:true,
		autoplayTimeout:5000,
		autoplayHoverPause:true,
		navText:['',''],
		responsive:{
        0:{
            items:1
        },
		400:{
            items:1
        },
        600:{
            items:2
        },
        1000:{
            items:4
        }
    }
	});
	$('.brand-panel-form').owlCarousel({
		loop:true,
		margin:20,
		nav:false,
		rtl:true,
		items: 1,
		autoplay:true,
		autoplayTimeout:5000,
		autoplayHoverPause:true,
		navText:['',''],
		responsive:{
        0:{
            items:1
        },
        600:{
            items:2
        },
        1000:{
            items:4
        }
    }
	});
	var swiper = new Swiper('.swiper-container', {
      spaceBetween: 30,
      effect: 'fade',
      pagination: {
        el: '.swiper-pagination',
        clickable: true,
      },
		autoplay: {
    delay: 5000,
		},
		navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
      },
     
    });
	
	
	
	
	
});
