$(function () {
    function c() {
        let o = $(this).attr("href"),
            m = $(this).data("index"),
            l = $.trim($(this).text()),
            k = true;
        if (o == undefined || $.trim(o).length == 0) return false;
        $('.J_menuTab', window.parent.document).each(function () {
            if ($(this).data("id") == o) {
                if (!$(this).hasClass("active")) {
                    $(this).addClass("active").siblings(".J_menuTab").removeClass("active");
                    $(".J_mainContent .J_iframe", window.parent.document).each(function () {
                        if ($(this).data("id") == o) {
                            $(this).show().siblings(".J_iframe").hide();
                            return false;
                        }
                    });
                }
                k = false;
                return false;
            }
        });
        
        if (k) {
            if (l.length > 4) {
                l = l.substr(0, 4);
            }
            var p = '<a href="javascript:void(0);" class="active J_menuTab" data-id="' + o + '">' + l + ' <i class="fa fa-times-circle"></i></a>';
            $(".J_menuTab", window.parent.document).removeClass("active");
            var n = '<iframe class="J_iframe" name="iframe' + m + '" width="100%" height="100%" src="' + o + '" frameborder="0" data-id="' + o + '" seamless></iframe>';
            $(".J_mainContent", window.parent.document).find("iframe.J_iframe", window.parent.document).hide().parents(".J_mainContent", window.parent.document).append(n);
            $(".J_menuTabs .page-tabs-content", window.parent.document).append(p);
        }
        return false;
    };
    //选中class="J_menuItem"的元素，为其添加data-index属性
    $(".J_menuItem").each(function (k) {
        if (!$(this).attr("data-index")) {
            $(this).attr("data-index", k);
        }
    });
    //选中class="J_menuItem"的元素，为其添加点击事件c()
    $(".J_menuItem").on("click", c);
});