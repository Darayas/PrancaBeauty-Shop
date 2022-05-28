function LoadSlider(_Id, _CountItemXL, _CountItemLG, _CountItemMD, _CountItemSM, _CountItemXS) {
    $('#' + _Id).owlCarousel({
        loop: false,
        nav: true,
        center: false,
        autoplay: false,
        autoplayTimeout: 8000,
        items: _CountItemLG,
        dots: false,
        margin: 7,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        responsiveClass: true,
        responsive: {
            0: {
                items: _CountItemXS,
            },
            573: {
                items: _CountItemSM,
            },
            767: {
                items: _CountItemMD,
            },
            1300: {
                items: _CountItemLG,
            },
            1560: {
                items: _CountItemXL,
            }
        }
    });
}

function LoadTabs(_ShowcaseId) {
    $('#' + _ShowcaseId + ' .product_tab_btn a').on('click', function (e) {
        e.preventDefault();

        var $href = $(this).attr('href');

        $('#' + _ShowcaseId + ' .product_tab_btn a').removeClass('active');
        $(this).addClass('active');

        $('#' + _ShowcaseId + ' .tab-pane').removeClass('active show');
        $('#' + _ShowcaseId + ' ' + $href).addClass('active show');
    });
}

function CalcDuDate(_Id, _Seconds, _Callback = function () { }) {
    var ss = parseFloat(_Seconds);
    var Calc = setInterval(function () {
        var Days = Math.floor(ss / 60 / 60 / 24);
        var Hours = Math.floor(ss / 60 / 60) % 24;
        var Minutes = Math.floor(ss / 60) % 60;
        var Seconds = Math.floor(ss % 60);

        $('#' + _Id + ' .Days .val').text(Days);
        $('#' + _Id + ' .Hours .val').text(Hours);
        $('#' + _Id + ' .Minutes .val').text(Minutes);
        $('#' + _Id + ' .Seconds .val').text(Seconds);

        ss--;

        if (Days == 0 && Hours == 0 && Minutes == 0 && Seconds == 0) {
            if (_Callback != null)
                _Callback();

            clearInterval(Calc);
        }

    }, 1000);
}

function LoadPriceSlider(_SliderId, _AmountMinId, _AmountMaxId, _Min, _Max, _callbackFuncs = function (_Min, _Max) { }) {
    $("#" + _SliderId).slider({
        animate: "fast",
        range: true,
        min: _Min,
        max: _Max,
        values: [_Min, _Max],
        slide: function (event, ui) {
            $("#" + _AmountMinId).val(ui.values[0]);
            $("#" + _AmountMaxId).val(ui.values[1]);
        },
        change: function (event, ui) {
            _callbackFuncs(ui.values[0], ui.values[1]);
        }
    });

    $("#" + _AmountMinId).val($("#" + _SliderId).slider("values", 0));
    $("#" + _AmountMaxId).val($("#" + _SliderId).slider("values", 1));
}

(function ($) {
    "use strict";

    $(document).ready(function () {
        $('.loading').hide();
    });

    new WOW().init();

    /*---background image---*/
    function dataBackgroundImage() {
        $('[data-bgimg]').each(function () {
            var bgImgUrl = $(this).data('bgimg');
            $(this).css({
                'background-image': 'url(' + bgImgUrl + ')', // + meaning concat
            });
        });
    }

    $(window).on('load', function () {
        dataBackgroundImage();
    });


    /*---stickey menu---*/
    $(window).on('scroll', function () {
        var scroll = $(window).scrollTop();
        if (scroll < 100) {
            $(".sticky-header").removeClass("sticky");
        } else {
            $(".sticky-header").addClass("sticky");
        }
    });


    /*---slider activation---*/
    var $slider = $('.slider_area');
    if ($slider.length > 0) {
        $slider.owlCarousel({
            animateOut: 'fadeOut',
            autoplay: true,
            loop: true,
            nav: false,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 1,
            dots: true,
        });
    }


    /*---slider activation---*/
    var $slider4 = $('.slider_four_area');
    if ($slider4.length > 0) {
        $slider4.owlCarousel({
            animateOut: 'fadeOut',
            autoplay: true,
            loop: true,
            nav: true,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 1,
            dots: false,
            navText: ['<i class="ion-ios-arrow-left"></i>', '<i class="ion-ios-arrow-right"></i>'],
        });
    }

    /*---product dl column3 activation---*/
    var $product_dl_column3 = $('.product_dl_column3');
    if ($product_dl_column3.length > 0) {
        $('.product_dl_column3').owlCarousel({
            loop: true,
            nav: true,
            center: true,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 3,
            dots: false,
            margin: 30,
            navText: ['<i class="ion-ios-arrow-left"></i>', '<i class="ion-ios-arrow-right"></i>'],
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1,
                },
                768: {
                    items: 3,
                },
                992: {
                    items: 3,
                },
            }
        });
    }
    function checkClasses() {
        var total = $('.product_dl_column3 .owl-stage .owl-item.active').length;

        $('.product_dl_column3 .owl-stage .owl-item').removeClass('firstActiveItem lastActiveItem');

        $('.product_dl_column3 .owl-stage .owl-item.active').each(function (index) {
            if (index === 0) {
                // this is the first one
                $(this).addClass('firstActiveItem');
            }
            if (index === total - 1 && total > 1) {
                // this is the last one
                $(this).addClass('lastActiveItem');
            }
        });
    }
    checkClasses();
    $product_dl_column3.on('translated.owl.carousel', function (event) {
        checkClasses();
    });



    /*---product column1 activation---*/
    var $product_column1 = $('.product_column1');
    if ($product_column1.length > 0) {
        $('.product_column1').owlCarousel({
            loop: true,
            nav: true,
            center: true,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 1,
            dots: false,
            margin: 30,
            navText: ['<i class="ion-ios-arrow-left"></i>', '<i class="ion-ios-arrow-right"></i>'],
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1,
                },
                768: {
                    items: 1,
                },
            }
        });
    }


    /*---testimonial column3 activation---*/
    var $testimonialColumn3 = $('.testimonial_column3');
    if ($testimonialColumn3.length > 0) {
        $('.testimonial_column3').owlCarousel({
            autoplay: true,
            loop: true,
            nav: true,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 3,
            dots: false,
            navText: ['<i class="ion-ios-arrow-left"></i>', '<i class="ion-ios-arrow-right"></i>'],
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                992: {
                    items: 2,
                },
                1200: {
                    items: 3,
                },
            }
        });
    }



    /*---blog column3 activation---*/
    var $blogColumn3 = $('.blog_column3');
    if ($blogColumn3.length > 0) {
        $('.blog_column3').owlCarousel({
            autoplay: true,
            loop: true,
            nav: true,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 3,
            dots: false,
            navText: ['<i class="ion-ios-arrow-left"></i>', '<i class="ion-ios-arrow-right"></i>'],
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1,
                },
                768: {
                    items: 2,
                },
                992: {
                    items: 3,
                },
                1200: {
                    items: 3,
                },
            }
        });
    }

    /*---brand container activation---*/
    var $brandContainer = $('.brand_container');
    if ($brandContainer.length > 0) {
        $('.brand_container').on('changed.owl.carousel initialized.owl.carousel', function (event) {
            $(event.target).find('.owl-item').removeClass('last').eq(event.item.index + event.page.size - 1).addClass('last')
        }).owlCarousel({
            autoplay: true,
            loop: true,
            nav: true,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 5,
            margin: 20,
            dots: false,
            navText: ['<i class="ion-ios-arrow-left"></i>', '<i class="ion-ios-arrow-right"></i>'],
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1,
                },
                480: {
                    items: 2,
                },
                576: {
                    items: 3,
                },
                768: {
                    items: 4,
                },
                992: {
                    items: 5,
                },
            }
        });
    }


    /*---banner column4 activation---*/
    var $bannerColumn4 = $('.banner_column4');
    if ($bannerColumn4.length > 0) {
        $('.banner_column4').on('changed.owl.carousel initialized.owl.carousel', function (event) {
            $(event.target).find('.owl-item').removeClass('last').eq(event.item.index + event.page.size - 1).addClass('last')
        }).owlCarousel({
            autoplay: true,
            loop: true,
            nav: true,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 4,
            dots: false,
            navText: ['<i class="ion-ios-arrow-left"></i>', '<i class="ion-ios-arrow-right"></i>'],
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1,
                },
                576: {
                    items: 2,
                },
                768: {
                    items: 3,
                },
                992: {
                    items: 4,
                },
            }
        });
    }


    /*---product column4 activation---*/
    var $porductColumn4 = $('.product_column4');
    if ($porductColumn4.length > 0) {
        $porductColumn4.on('changed.owl.carousel initialized.owl.carousel', function (event) {
            $(event.target).find('.owl-item').removeClass('last').eq(event.item.index + event.page.size - 1).addClass('last')
        }).owlCarousel({
            autoplay: true,
            loop: true,
            nav: true,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 4,
            dots: false,
            navText: ['<i class="ion-ios-arrow-left"></i>', '<i class="ion-ios-arrow-right"></i>'],
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1,
                },
                576: {
                    items: 2,
                },
                768: {
                    items: 3,
                },
                992: {
                    items: 3,
                },
                1200: {
                    items: 4,
                },
            }
        });
    }


    /*---categories column6 activation---*/
    var $categoriesColumn6 = $('.categories_column6');
    if ($categoriesColumn6.length > 0) {
        $categoriesColumn6.on('changed.owl.carousel initialized.owl.carousel', function (event) {
            $(event.target).find('.owl-item').removeClass('last').eq(event.item.index + event.page.size - 1).addClass('last')
        }).owlCarousel({
            autoplay: true,
            loop: true,
            nav: true,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 6,
            dots: false,
            navText: ['<i class="ion-ios-arrow-left"></i>', '<i class="ion-ios-arrow-right"></i>'],
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1,
                },
                320: {
                    items: 2,
                },
                576: {
                    items: 3,
                },
                768: {
                    items: 4,
                },
                992: {
                    items: 5,
                },
                1200: {
                    items: 6,
                },
            }
        });
    }


    /*---product column3 activation---*/
    var $porductColumn3 = $('.product_column3');
    if ($porductColumn3.length > 0) {
        $porductColumn3.on('changed.owl.carousel initialized.owl.carousel', function (event) {
            $(event.target).find('.owl-item').removeClass('last').eq(event.item.index + event.page.size - 1).addClass('last')
        }).owlCarousel({
            autoplay: true,
            loop: true,
            nav: true,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 3,
            dots: false,
            navText: ['<i class="ion-ios-arrow-left"></i>', '<i class="ion-ios-arrow-right"></i>'],
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1,
                },
                576: {
                    items: 2,
                },
                768: {
                    items: 3,
                },
                992: {
                    items: 3,
                },
                1200: {
                    items: 3,
                },
            }
        });
    }

    /*---testimonial about two activation---*/
    var $testimonialAboutTwo = $('.testimonial_about_two');
    if ($testimonialAboutTwo.length > 0) {
        $('.testimonial_about_two').owlCarousel({
            autoplay: true,
            loop: true,
            nav: true,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 1,
            dots: false,
            navText: ['<i class="ion-ios-arrow-left"></i>', '<i class="ion-ios-arrow-right"></i>'],
        });
    }

    /*---blog thumb activation---*/
    var $blogThumbActive = $('.blog_thumb_active');
    if ($blogThumbActive.length > 0) {
        $('.blog_thumb_active').owlCarousel({
            autoplay: true,
            loop: true,
            nav: true,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 1,
            navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        });
    }


    /*---single product activation---*/
    var $singleProductActive = $('.single-product-active');
    if ($singleProductActive.length > 0) {
        $('.single-product-active').owlCarousel({
            autoplay: true,
            loop: true,
            nav: true,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 4,
            margin: 15,
            dots: false,
            navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1,
                },
                240: {
                    items: 2,
                },
                310: {
                    items: 3,
                },
                400: {
                    items: 3,
                },
                576: {
                    items: 4,
                },

            }
        });
    }

    /*---product navactive activation---*/
    var $productNavactive = $('.product_navactive');
    if ($productNavactive.length > 0) {
        $('.product_navactive').owlCarousel({
            autoplay: true,
            loop: true,
            nav: true,
            autoplay: false,
            autoplayTimeout: 8000,
            items: 4,
            dots: false,
            navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1,
                },
                250: {
                    items: 2,
                },
                480: {
                    items: 3,
                },
                768: {
                    items: 4,
                },

            }
        });
    }

    $('.modal').on('shown.bs.modal', function (e) {
        $('.product_navactive').resize();
    })

    /*--- video Popup---*/
    $('.video_popup').magnificPopup({
        type: 'iframe',
        removalDelay: 300,
        mainClass: 'mfp-fade'
    });

    /*--- Magnific Popup Video---*/
    $('.port_popup').magnificPopup({
        type: 'image',
        gallery: {
            enabled: true
        }
    });

    /*--- Tooltip Active---*/
    $('.action_links ul li a,.add_to_cart a,.footer_social_link ul li a').tooltip({
        animated: 'fade',
        placement: 'top',
        container: 'body'
    });

    /*--- niceSelect---*/
    $('.select_option').niceSelect();

    /*---  Accordion---*/
    $(".faequently-accordion").collapse({
        accordion: true,
        open: function () {
            this.slideDown(300);
        },
        close: function () {
            this.slideUp(300);
        }
    });



    /*---  ScrollUp Active ---*/
    $.scrollUp({
        scrollText: '<i class="fa fa-angle-double-up"></i>',
        easingType: 'linear',
        scrollSpeed: 900,
        animation: 'fade'
    });

    /*---countdown activation---*/

    $('[data-countdown]').each(function () {
        var $this = $(this), finalDate = $(this).data('countdown');
        $this.countdown(finalDate, function (event) {
            $this.html(event.strftime('<div class="countdown_area"><div class="single_countdown"><div class="countdown_number">%D</div><div class="countdown_title">days</div></div><div class="single_countdown"><div class="countdown_number">%H</div><div class="countdown_title">hours</div></div><div class="single_countdown"><div class="countdown_number">%M</div><div class="countdown_title">mins</div></div><div class="single_countdown"><div class="countdown_number">%S</div><div class="countdown_title">secs</div></div></div>'));

        });
    });



    /*---elevateZoom---*/
    $("#zoom1").elevateZoom({
        gallery: 'gallery_01',
        responsive: true,
        cursor: 'crosshair',
        zoomType: 'inner'
    });


    /*---addClass/removeClass categories---*/
    $("#cat_toggle.has-sub > a").on("click", function () {
        $(this).removeAttr('href');
        $(this).toggleClass('open').next('.categorie_sub').toggleClass('open');
        $(this).parents().siblings().find('#cat_toggle.has-sub > a').removeClass('open');
    });


    /*---MailChimp---*/
    $('#mc-form').ajaxChimp({
        language: 'en',
        callback: mailChimpResponse,
        // ADD YOUR MAILCHIMP URL BELOW HERE!
        url: 'http://devitems.us11.list-manage.com/subscribe/post?u=6bbb9b6f5827bd842d9640c82&amp;id=05d85f18ef'

    });
    function mailChimpResponse(resp) {

        if (resp.result === 'success') {
            $('.mailchimp-success').addClass('active')
            $('.mailchimp-success').html('' + resp.msg).fadeIn(900);
            $('.mailchimp-error').fadeOut(400);

        } else if (resp.result === 'error') {
            $('.mailchimp-error').html('' + resp.msg).fadeIn(900);
        }
    }

    /*---Category menu---*/
    function categorySubMenuToggle() {
        $('.categories_menu_toggle li.menu_item_children > a').on('click', function () {
            if ($(window).width() < 991) {
                $(this).removeAttr('href');
                var element = $(this).parent('li');
                if (element.hasClass('open')) {
                    element.removeClass('open');
                    element.find('li').removeClass('open');
                    element.find('ul').slideUp();
                }
                else {
                    element.addClass('open');
                    element.children('ul').slideDown();
                    element.siblings('li').children('ul').slideUp();
                    element.siblings('li').removeClass('open');
                    element.siblings('li').find('li').removeClass('open');
                    element.siblings('li').find('ul').slideUp();
                }
            }
        });
        $('.categories_menu_toggle li.menu_item_children > a').append('<span class="expand"></span>');
    }
    categorySubMenuToggle();


    /*---shop grid activation---*/
    $('.shop_toolbar_btn > button').on('click', function (e) {

        e.preventDefault();

        $('.shop_toolbar_btn > button').removeClass('active');
        $(this).addClass('active');

        var parentsDiv = $('.shop_wrapper');
        var viewMode = $(this).data('role');


        parentsDiv.removeClass('grid_3 grid_4 grid_5 grid_list').addClass(viewMode);

        if (viewMode == 'grid_3') {
            parentsDiv.children().addClass('col-lg-4 col-md-4 col-sm-6').removeClass('col-lg-3 col-cust-5 col-12');

        }

        if (viewMode == 'grid_4') {
            parentsDiv.children().addClass('col-lg-3 col-md-4 col-sm-6').removeClass('col-lg-4 col-cust-5 col-12');
        }

        if (viewMode == 'grid_list') {
            parentsDiv.children().addClass('col-12').removeClass('col-lg-3 col-lg-4 col-md-4 col-sm-6 col-cust-5');
        }

    });


    /*---Newsletter Popup activation---*/

    setTimeout(function () {
        if ($.cookie('shownewsletter') == 1) $('.newletter-popup').hide();
        $('#subscribe_pemail').keypress(function (e) {
            if (e.which == 13) {
                e.preventDefault();
                email_subscribepopup();
            }
            var name = $(this).val();
            $('#subscribe_pname').val(name);
        });
        $('#subscribe_pemail').change(function () {
            var name = $(this).val();
            $('#subscribe_pname').val(name);
        });
        //transition effect
        if ($.cookie("shownewsletter") != 1) {
            $('.newletter-popup').bPopup();
        }
        $('#newsletter_popup_dont_show_again').on('change', function () {
            if ($.cookie("shownewsletter") != 1) {
                $.cookie("shownewsletter", '1')
            } else {
                $.cookie("shownewsletter", '0')
            }
        });
    }, 2500);


    /*---search account slideToggle---*/
    $(".search_list > a").on("click", function () {
        $(this).toggleClass('active');
        $('.dropdown_search').slideToggle('medium');
    });



    /*---header account slideToggle---*/
    $(".header_account > a").on("click", function () {
        $(this).toggleClass('active');
        $('.dropdown_account').slideToggle('medium');
    });

    /*---slide toggle activation---*/
    $('.mini_cart_wrapper > a').on('click', function (event) {
        if ($(window).width() < 991) {
            $('.mini_cart').slideToggle('medium');
        }
    });


    /*---canvas menu activation---*/
    $('.canvas_open').on('click', function () {
        $('.offcanvas_menu_wrapper,.off_canvars_overlay').addClass('active')
    });

    $('.canvas_close,.off_canvars_overlay').on('click', function () {
        $('.offcanvas_menu_wrapper,.off_canvars_overlay').removeClass('active')
    });



    /*---Off Canvas Menu---*/
    var $offcanvasNav = $('.offcanvas_main_menu'),
        $offcanvasNavSubMenu = $offcanvasNav.find('.sub-menu');
    $offcanvasNavSubMenu.parent().prepend('<span class="menu-expand"><i class="fa fa-angle-down"></i></span>');

    $offcanvasNavSubMenu.slideUp();

    $offcanvasNav.on('click', 'li a, li .menu-expand', function (e) {
        var $this = $(this);
        if (($this.parent().attr('class').match(/\b(menu-item-has-children|has-children|has-sub-menu)\b/)) && ($this.attr('href') === '#' || $this.hasClass('menu-expand'))) {
            e.preventDefault();
            if ($this.siblings('ul:visible').length) {
                $this.siblings('ul').slideUp('slow');
            } else {
                $this.closest('li').siblings('li').find('ul:visible').slideUp('slow');
                $this.siblings('ul').slideDown('slow');
            }
        }
        if ($this.is('a') || $this.is('span') || $this.attr('clas').match(/\b(menu-expand)\b/)) {
            $this.parent().toggleClass('menu-open');
        } else if ($this.is('li') && $this.attr('class').match(/\b('menu-item-has-children')\b/)) {
            $this.toggleClass('menu-open');
        }
    });

    /*---product dl column3 activation---*/
})(jQuery);

$('.card-arrow-down').click(function () {

    var _ParentCard = $(this).parents('.card');

    if (_ParentCard.hasClass("tiny")) {
        _ParentCard.animate({ "height": _ParentCard.attr('height') }, "medium").removeClass("tiny");
        _ParentCard.children('.card-body').slideDown('medium');

        setTimeout(function () {
            _ParentCard.css('height', 'auto');
        }, 1000);
    }
    else {
        _ParentCard.attr("height", _ParentCard.height());
        _ParentCard.animate({ "height": "50px" }, "medium").addClass("tiny");
        _ParentCard.children('.card-body').slideUp('medium');
    }
});

$(document).ready(function () {
    $('#lstMedia').slick({
        dots: false,
        slidesToShow: 12,
        slidesToScroll: 1,
        touchMove: false,
        centerMode: false
    });
});

