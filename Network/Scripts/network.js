$(document).ready(function () {
    $(".newsTitle span").click(function () {
        $(".newsContent ul").attr("class", "news_part_Content");
        $(".newsContent ul").eq(parseInt($(this).data("value"))).attr("class", "news_part_Content hover");
        $(".newsTitle span").attr("class", "left cp");
        $(this).attr("class", "left cp hover");
    });
});

var commentIndex = 0;
var commentValue = 3;
var bH1C = new Array();
var bH3C = new Array();
var bBH = new Array();
var banner;
var bannerBgColor = ["#F2044D", "#7CDCD8", "#F7E73A", "#D47BF7", "#79d179"];
var bannerImg = ["/Upload/banner_home_left_01.png", "/Upload/banner_home_left_02.png", "/Upload/banner_home_left_03.png", "/Upload/banner_home_left_04.png", "/Upload/banner_home_left_05.png"];//定义左边的图片
var bannerIcon = ["/Upload/banner_home_right_icon_01.png", "/Upload/banner_home_right_icon_02.png", "/Upload/banner_home_right_icon_03.png", "/Upload/banner_home_right_icon_04.png", "/Upload/banner_home_right_icon_05.png"];

$(".bannerInner div").each(function () {
    bH1C.push($(this).data("title"));
    bH3C.push($(this).data("content"));
    bBH.push($(this).data("link"));
});

var bannerNowIndex = 0;
var bannerNumber = $(".bannerInner div").length - 1;
var bannerAutoPlayHolder;
function chageTime() {
    var gTid = Math.floor(Math.random() * 1000 + 600);
    return gTid;
}
$(".CButtonRight").click(function () {
    commentIndex++;
    $(this).css({ background: "#F8044E" });
    $(".CButtonRight , .CButtonLeft").css({ visibility: "visible", background: "#f3f3f3" });
    if (commentIndex == commentValue) {
        $(this).css({ visibility: "hidden" });
    }
    for (var j = 0; j < commentIndex ; j++) {
        $(".commentContent").eq(j).animate({ marginLeft: "-394px", opacity: 0 }, 300);
    }
    $(".commentContent").eq(commentIndex).animate({ marginLeft: "0px", opacity: 1 }, 300);
})
$(".CButtonLeft").click(function () {
    commentIndex--;
    $(this).css({ background: "#F8044E" });
    $(".CButtonRight , .CButtonLeft").css({ visibility: "visible", background: "#f3f3f3" });
    if (commentIndex == 0) {
        $(".CButtonLeft").css({ visibility: "hidden" });
    }
    $(".commentContent").eq(commentIndex).animate({ marginLeft: "0px", opacity: 1 }, 300);
})
function chageState() { var gid = Math.floor(Math.random() * 29); switch (gid) { case 0: x = 'easeInQuad'; break; case 1: x = 'easeOutQuad'; break; case 2: x = 'easeInOutQuad'; break; case 3: x = 'easeInCubic'; break; case 4: x = 'easeOutCubic'; break; case 5: x = 'easeInOutCubic'; break; case 6: x = 'easeInQuart'; break; case 7: x = 'easeOutQuart'; break; case 8: x = 'easeInOutQuart'; break; case 9: x = 'easeInQuint'; break; case 10: x = 'easeOutQuint'; break; case 11: x = 'easeInOutQuint'; break; case 12: x = 'easeInSine'; break; case 13: x = 'easeOutSine'; break; case 14: x = 'easeInOutSine'; break; case 15: x = 'easeInExpo'; break; case 16: x = 'easeOutExpo'; break; case 17: x = 'easeInOutExpo'; break; case 18: x = 'easeInCirc'; break; case 19: x = 'easeOutCirc'; break; case 20: x = 'easeInOutCirc'; break; case 21: x = 'easeInElastic'; break; case 22: x = 'easeOutElastic'; break; case 23: x = 'easeInOutElastic'; break; case 24: x = 'easeInBack'; break; case 25: x = 'easeOutBack'; break; case 26: x = 'easeInOutBack'; break; case 27: x = 'easeInBounce'; break; case 28: x = 'easeOutBounce'; break; case 29: x = 'easeInOutBounce'; break; } return x; };
function bannerAutoPlay() {
    bannerNowIndex++;
    if (bannerNowIndex > bannerNumber) { bannerNowIndex = 0; }
    changWallPaper();
}
function changWallPaper(btnIndex) {
    $(".bannerImage  , .bannerSlogan").remove().empty().detach();

    if (btnIndex != null) { bannerNowIndex = btnIndex; }

    $(".Banner").css({ background: bannerBgColor[bannerNowIndex] });
    switch (bannerNowIndex) {
        case 0:
            createBanner(0);
            $(".bannerImage").animate({ top: "0", opacity: 1 }, chageTime(), chageState());
            $(".bannerSlogan").animate({ top: "200px", right: "0", opacity: 1 }, chageTime(), chageState(), function () { changeTip(); });
            break;
        case 1:
            createBanner(1);
            $(".bannerImage").animate({ top: "180px", opacity: 1 }, chageTime(), chageState());
            $(".bannerSlogan").animate({ bottom: "100px", opacity: 1 }, chageTime(), chageState(), function () { changeTip(); });
            break;
        case 2:
            createBanner(2);
            $(".bannerImage").animate({ right: "500px", opacity: 1 }, chageTime(), chageState());
            $(".bannerSlogan").animate({ top: "200px", opacity: 1 }, chageTime(), chageState(), function () { changeTip(); });
            break;
        case 3:
            createBanner(3);
            $(".bannerImage").animate({ top: "135px", opacity: 1 }, chageTime(), chageState());
            $(".bannerSlogan").animate({ bottom: "100px", opacity: 1 }, chageTime(), chageState(), function () { changeTip(); });
            break;
        case 4:
            createBanner(4);
            $(".bannerImage").animate({ top: "103px", opacity: 1 }, chageTime(), chageState());
            $(".bannerSlogan").animate({ bottom: "100px", opacity: 1 }, chageTime(), chageState(), function () { changeTip(); });
            break;
        default:
            break;
    }
    $(".bannerControl li").css({ background: "black" });
    $(".bannerControl li").eq(bannerNowIndex).css({ background: "#FF009D" });
}
function createBanner(i) {
    banner = "<img src='" + bannerImg[i] + "' class='bannerImage bannerI" + i + "'  />" +
            "<div class='bannerSlogan bannerS" + i + "'>" +
                "<div class='bAS C'>" +
                    "<div class='bannerIcon'>" +
                        "<img src='" + bannerIcon[0] + "' /></div>" +
                        "<div class='bannerTitle'>" + bH1C[i] + "</div>" +
                        "<div class='bannerDesc'>" + bH3C[i] + "</div>" +
                        "<div class='bannerButton'>" +
                            "<a href='" + bBH[i] + "'>ReadMore</a>" +
                        "</div></div></div>";

    $(".bannerInner").append(banner);
    $(".bannerImage").css("position", "absolute");
}

if ($.browser.msie && ($.browser.version == "6.0")) {
    window.browserIE6 = true;
}

if (window.browserIE6) {
    $(".bannerInner , .bannerLeftBar , .bannerControl , .bannerRightBar").css({ display: "none" });
    $(".Banner").css({ background: "url(/Upload/IEBANNER.jpg) no-repeat center" });
} else {
    
    function changeTip() {
        $(".bannerIcon").animate({ left: 0 }, 500, chageState());
        $(".bannerTitle").animate({ top: "20px" }, 500, chageState());
        $(".bannerDesc").animate({ left: "5px" }, 500, chageState());
        $(".bannerButton").animate({ right: "85px" }, 500, chageState());
    }




    $(".bannerControl li").click(function () {
        clearInterval(bannerAutoPlayHolder);
        var blI = $(this).index();
        changWallPaper(blI);
        bannerAutoPlayHolder = setInterval(bannerAutoPlay, 6000);
    })


    $(".bannerLeftBar").click(function () {
        clearInterval(bannerAutoPlayHolder);
        bannerNowIndex--;
        if (bannerNowIndex < 0) { bannerNowIndex = bannerNumber; }
        changWallPaper();
        bannerAutoPlayHolder = setInterval(bannerAutoPlay, 6000);
    })
    $(".bannerRightBar").click(function () {
        clearInterval(bannerAutoPlayHolder);
        bannerNowIndex++;
        if (bannerNowIndex > bannerNumber) { bannerNowIndex = 0; }
        changWallPaper();
        bannerAutoPlayHolder = setInterval(bannerAutoPlay, 6000);
    });
    $(".bannerLeftBar , .bannerRightBar").css({ opacity: 0 });
    $(".Banner").hover(function () {
        $(".bannerLeftBar , .bannerRightBar").animate({ opacity: 1 }, 200).dequeue();
    }, function () {
        $(".bannerLeftBar , .bannerRightBar").animate({ opacity: 0 }, 200).dequeue();
    })

    changWallPaper(0);
    bannerAutoPlayHolder = setInterval(bannerAutoPlay, 6000);
}
