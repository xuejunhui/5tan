
if (jQuery.browser.msie && jQuery.browser.version == 6.0)
{ }
else {
    var FixedBox = function (el) {
        this.element = el;
        this.BoxY = getXY(this.element).y;
    }
    FixedBox.prototype = {
        setCss: function () {
            var windowST = (document.compatMode && document.compatMode != "CSS1Compat") ? document.body.scrollTop : document.documentElement.scrollTop || window.pageYOffset;

            if (windowST > 80) {
                //this.element.style.cssText = "position:fixed; top:0px; background-color:#cdcdcd;width:100%";
                jQuery(this.element).addClass("header_fixed");
            } else {
                jQuery(this.element).removeClass("header_fixed");
                //this.element.style.cssText = "";
            }
        },
        setDetailHeadCss: function () {
            var windowST = (document.compatMode && document.compatMode != "CSS1Compat") ? document.body.scrollTop : document.documentElement.scrollTop || window.pageYOffset;

            if (windowST > this.BoxY) {
                jQuery(this.element).addClass("navigate-fixed");
            } else {
                jQuery(this.element).removeClass("navigate-fixed");
            }
        },
        setDetailSelectCss: function () {
            var windowST = (document.compatMode && document.compatMode != "CSS1Compat") ? document.body.scrollTop : document.documentElement.scrollTop || window.pageYOffset;

            if ((windowST > this.BoxY - 60 || this.element.id == "product-detail") && windowST < (this.BoxY + jQuery(this.element).height() - 20)) {
                jQuery("#navigate-list a[data-flag=" + this.element.id + "]").attr("class", "active color-style2");
            } else {
                jQuery("#navigate-list a[data-flag=" + this.element.id + "]").attr("class", "");
            }
        }

    };
    function addEvent(elm, evType, fn, useCapture) {
        if (elm.addEventListener) {
            elm.addEventListener(evType, fn, useCapture);
            return true;
        } else if (elm.attachEvent) {
            var r = elm.attachEvent('on' + evType, fn);
            return r;
        }
        else {
            elm['on' + evType] = fn;
        }
    }
    function getXY(el) {
        return document.documentElement.getBoundingClientRect && (function () {//取元素坐标，如元素或其上层元素设置position relative
            var pos = el.getBoundingClientRect();
            return { x: pos.left + document.documentElement.scrollLeft, y: pos.top + document.documentElement.scrollTop };
        })() || (function () {
            var _x = 0, _y = 0;
            do {
                _x += el.offsetLeft;
                _y += el.offsetTop;
            } while (el = el.offsetParent);
            return { x: _x, y: _y };
        })();
    }
    var divA = new FixedBox(document.getElementById("headnav"));
    var divB;
    if (jQuery("#nav-height-holder").length > 0) {
        divB = new FixedBox(document.getElementById("nav-height-holder"));
        jQuery("#navigate-list li a").click(function () {
            var item = new FixedBox(document.getElementById(jQuery(this).attr("data-flag")));
            window.scrollTo('0', item.BoxY - 50);
        });
    }
    addEvent(window, "scroll", function () {
        if (divB) {
            jQuery("#header_fake").removeClass("header_fake");
            jQuery("#headnav").css("position", "relative");
            jQuery(".product-detail-instance").each(function () {
                var item = new FixedBox(this);
                item.setDetailSelectCss();
            });
            divB.setDetailHeadCss();
        } else {
            divA.setCss();
        }
    });
}