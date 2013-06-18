(function($){
    $.fn.Dropdown = function( options ){
        var defaults = {
        };

        var $this = $(this);

        var clearDropdown = function(){
            $(".dropdown-menu").each(function(){
                if ( $(this).css('position') == 'static' ) return;
                $(this).slideUp('fast', function(){});
                $(this).parent().removeClass("active");
            });
        };

        var initSelectors = function(selectors){
            selectors.on('click', function(e){
                e.stopPropagation();
                //e.preventDefault();
                //$("[data-role=dropdown]").removeClass("active");

                //clearDropdown();
                $(this).parents("ul").css("overflow", "visible");

                var $m = $(this).children(".dropdown-menu, .sidebar-dropdown-menu");
                if ($m[0].tagName.toLowerCase() === "div" && $m.hasClass('dropdown-form')) {
                    $($m[0]).on('click', function(event){
                        event.stopPropagation();
                    });
                    if ($m.css('display') === "block") {
                        $m.slideUp('fast');
                        $(this).removeClass("active");
                    } else {
                        $m.slideDown('fast');
                        $(this).addClass("active");
                    }
                } else if ($m.hasClass('multidropdown')) {
                    if ($m.css('display') === "block" && $(e.target).parent().data('role') === 'dropdown') {
                        $m.slideUp('fast');
                        $(this).removeClass("active");
                    } else if ($(e.target).parent().data('role') === 'dropdown') {
                        $m.slideDown('fast');
                        $(this).addClass("active");
                    }
                } else {
                    if ($m.css('display') === "block") {
                        $m.slideUp('fast');
                        $(this).removeClass("active");
                    } else {
                        $m.slideDown('fast');
                        $(this).addClass("active");
                    }
                }
            }).on("mouseleave", function(){
                //$(this).children(".dropdown-menu").hide();
            });
            $('html').on("click", function(e){
                clearDropdown();
            });
        };

        return this.each(function(){
            if ( options ) {
                $.extend(defaults, options);
            }

            initSelectors($this);
        });
    };

    $(function () {
        $('[data-role="dropdown"]').each(function () {
            $(this).Dropdown();
        });
    });
})(window.jQuery);


(function($){
    $.fn.PullDown = function( options ){
        var defaults = {
        };

        var $this = $(this)
            ;

        var initSelectors = function(selectors){

            selectors.on('click', function(e){
                e.preventDefault();
                var $m = $this.parent().children("ul");
                if ($m.css('display') == "block") {
                    $m.slideUp('fast');
                } else {
                    $m.slideDown('fast');
                }
                //$(this).toggleClass("active");
            });
        };

        return this.each(function(){
            if ( options ) {
                $.extend(defaults, options);
            }

            initSelectors($this);
        });
    };

    $(function () {
        $('.menu-pull').each(function () {
            $(this).PullDown();
        });
    });
})(window.jQuery);