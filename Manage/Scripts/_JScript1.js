(function() {
    var config = window.lineDetail.config = {};
    config.getCalendarProductType = function(id) {
        var calendarProductType = ["套餐", "成人", "人均"];
        return calendarProductType[id];
    };
})();; (function() {
    var util = window.lineDetail.util = {};
    util.checkeDocHeight = function(callBack, interval) {
        var lastHeight = document.body.clientHeight,
        newHeight, timer; (function run() {
            newHeight = document.body.clientHeight;
            if (lastHeight !== newHeight) {
                callBack();
            }
            lastHeight = newHeight;
            timer = setTimeout(run, interval ? interval: 200);
        })();
    }
})();; (function() {
    var config = window.lineDetail.config;
    var CalendarView = Backbone.Marionette.ItemView.extend({
        template: "#calendar-template",
        events: {
            "click #month-up": "monthUp",
            "click #month-down": "monthDown",
            "click .main-day": "selectDay",
            "mouseenter .main-day": "mouseEnter",
            "mouseleave .main-day": "mouseLeave"
        },
        initialize: function(options) {
            var self = this;
            _.extend(this, options);
            $.getJSON(this.sourceURL,
            function(response) {
                self.sourceData = response;
                self.render();
            });
        },
        mouseEnter: function(e) {
            var $currentTarget;
            $currentTarget = $(e.currentTarget);
            if ($currentTarget.data("another") === undefined && $currentTarget.find(".price-item").length) {
                $currentTarget.addClass("hover-date");
            }
        },
        mouseLeave: function(e) {
            var $currentTarget = $(e.currentTarget);
            $currentTarget.removeClass("hover-date");
        },
        selectDay: function(e) {
            var $currentTarget = $(e.currentTarget),
            selectedDate = $currentTarget.data("date"),
            eventResult;
            if (!$currentTarget.find(".price-item").length) {
                return;
            }
            _.each(this.sourceData,
            function(product) {
                if (product.date === selectedDate) {
                    eventResult = product;
                }
            });
            this.trigger("calendar:dateSelected", eventResult);
            this.$el.hide();
        },
        monthUp: function(e) {
            var $currentTarget = $(e.currentTarget);
            if ($currentTarget.hasClass("disable-up")) {
                return;
            }
            var currentMonth = this.$el.find("#current-month").data("current-month");
            var months = this.generateMonths();
            this.$el.find("#month-down").removeClass("disable-down");
            offset = _.indexOf(months, currentMonth);
            this.renderMonth(months[--offset]);
            this.renderCalendarMain(months[offset]);
            if (!offset) {
                $currentTarget.addClass("disable-up");
            }
        },
        monthDown: function(e) {
            var $currentTarget, currentMonth, months, offset;
            $currentTarget = $(e.currentTarget);
            if ($currentTarget.hasClass("disable-down")) {
                return;
            }
            currentMonth = this.$el.find("#current-month").data("current-month");
            this.$el.find("#month-up").removeClass("disable-up");
            months = this.generateMonths();
            offset = _.indexOf(months, currentMonth);
            this.renderMonth(months[++offset]);
            this.renderCalendarMain(months[offset]);
            if (++offset === months.length) {
                $currentTarget.addClass("disable-down");
            }
        },
        dataCallback: function(response) {},
        generateDays: function() {
            var dateCollection, days, diffValue, end, i, result, start;
            dateCollection = this.sourceData;
            result = [];
            days = [];
            start = moment(dateCollection[0].date, "YYYY-MM-DD").startOf("month");
            end = moment(dateCollection[dateCollection.length - 1].date, "YYYY-MM-DD").endOf("month");
            diffValue = end.diff(start, "days");
            days.push(start.format("YYYY-MM-DD"));
            i = 1;
            while (i < diffValue + 1) {
                days.push(start.add(1, "day").format("YYYY-MM-DD"));
                i++;
            }
            _.each(days,
            function(day) {
                var notAvailable, _available;
                notAvailable = {};
                _available = _.findWhere(dateCollection, {
                    date: day
                });
                if (_available) {
                    return result.push(_available);
                } else {
                    notAvailable.date = day;
                    return result.push(notAvailable);
                }
            });
            return result;
        },
        generateMonths: function() {
            var days, months, self;
            days = this.generateDays();
            months = [];
            self = this;
            _.each(days,
            function(day) {
                return months.push(day.date.slice(0, -3));
            });
            return _.uniq(months);
        },
        generateWeekDays: function() {
            var weekOfDays;
            weekOfDays = "日_一_二_三_四_五_六".split("_");
            return weekOfDays;
        },
        templateHelpers: function() {
            var result;
            result = {
                daysOfTheWeek: this.generateWeekDays(),
                month: this.generateMonths()[0]
            };
            return result;
        },
        removeBorder: function() {
            var $groupClooection;
            $groupClooection = this.$el.find(".week-group");
            $groupClooection.each(function() {
                var className, current;
                current = $(this).find(".main-day:last-child")[0];
                className = $(current).attr("class");
                current.className = className + " remove-border";
            });
        },
        monthHandler: function(destMonth) {
            var months;
            months = this.generateMonths();
            if (_.first(months) === destMonth) {
                this.$el.find("#month-up").addClass("disable-up");
            }
            if (_.last(months) === destMonth) {
                this.$el.find("#month-down").addClass("disable-down");
            }
        },
        renderMonth: function(month) {
            var monthTemplate, splitDate, templateData;
            monthTemplate = $("#calendar-month-template").html();
            splitDate = month.split("-");
            templateData = {
                date: month,
                month: parseInt(splitDate[splitDate.length - 1])
            };
            this.$el.find("#calendar-main-left").html(_.template(monthTemplate, templateData));
        },
        renderCalendarMain: function(month) {
            var calendarTemplate, currentMonth, daysInMonth, diff, i, resultDays, self, templateData, weekday, _tmp;
            calendarTemplate = $("#calendar-mian-template").html();
            daysInMonth = moment(month, "YYYY-MM").endOf("month");
            daysInMonth = daysInMonth.date();
            templateData = [];
            resultDays = [];
            currentMonth = moment(month, "YYYY-MM").startOf("month");
            weekday = currentMonth.weekday();
            currentMonth.startOf("week");
            currentMonth.subtract(1, "day");
            self = this;
            i = 0;
            while (i < weekday) {
                templateData.push({
                    date: currentMonth.add(1, "day").format("YYYY-MM-DD"),
                    currentMonth: false
                });
                i++;
            }
            i = 0;
            while (i < daysInMonth) {
                templateData.push({
                    date: currentMonth.add(1, "day").format("YYYY-MM-DD"),
                    currentMonth: true
                });
                i++;
            }
            diff = 6 - currentMonth.weekday();
            i = 0;
            while (i < diff) {
                templateData.push({
                    date: currentMonth.add(1, "day").format("YYYY-MM-DD"),
                    currentMonth: false
                });
                i++;
            }
            if (templateData.length === 35) {
                i = 0;
                while (i < 7) {
                    templateData.push({
                        date: currentMonth.add(1, "day").format("YYYY-MM-DD"),
                        currentMonth: false
                    });
                    i++;
                }
            }
            _.each(templateData,
            function(dataItem) {
                var splitDate;
                splitDate = dataItem.date.split("-");
                dataItem.day = splitDate[splitDate.length - 1];
                _.each(self.sourceData,
                function(product) {
                    if (dataItem.date === product.date) {
                        if (typeof product.type !== "undefined") {
                            product.typeName = config.getCalendarProductType(product.type);
                        }
                        _.extend(dataItem, product);
                    }
                });
            });
            i = 0;
            while (i < templateData.length / 7) {
                _tmp = templateData.slice(i * 7, ++i * 7);
                resultDays.push(_tmp);
            }
            this.$el.find("#calendar-main-right").html(_.template(calendarTemplate, {
                days: resultDays
            }));
            this.removeBorder();
            this.toolTip();
        },
        toolTip: function(e) {
            var $detailCollection;
            $detailCollection = $("#calendar-main-right").find(".main-day");
            $detailCollection.each(function() {
                if ($(this).attr("tip-content")) {
                    $(this).poptip({
                        place: 6
                    });
                }
            });
        },
        onRender: function() {
            this.renderCalendarMain(this.generateMonths()[0]);
            this.renderMonth(this.generateMonths()[0]);
            this.monthHandler(this.generateMonths()[0]);
        }
    });
    window.lineDetail.calendar = CalendarView;
})();; (function() {
    var calendar = window.lineDetail.calendar,
    util = window.lineDetail.util,
    config = window.lineDetail.config;
    var readyCallBack = function(e) {
        var $navigateHeader = $("#product-detail-header"),
        navBarHeight = $navigateHeader.height(),
        $navigateList = $("#navigate-list"),
        $navigateLinkCollection = $navigateList.find("a"),
        $win = $(window),
        $trafficTypeCollection = $("#traffic-type").find("li"),
        $trafficContainerCollection = $("#traffic-container").find(".traffic-instance"),
        $travelFixed = $("#travel-fixed"),
        $travelFixedItems = $travelFixed.find("li"),
        $calendarArea = $("#calendar-area"); (function() {
            $trafficTypeCollection.eq(0).addClass("active");
            $trafficContainerCollection.eq(0).show();
            $navigateLinkCollection.eq(0).addClass("active");
            if ($travelFixed.length) {
                $travelFixedItems.eq(0).addClass("active");
            }
            $(".product-overview-protect").height($(".product-overview").height());
        })(); (function() {
            var $slidesListContainer = $("#overview-slide-show");
            if (!$slidesListContainer.find("li").length) {
                return;
            }
            $slidesListContainer.PikaChoose({
                carousel: true,
                autoPlay: true,
                showCaption: false
            });
        })(); (function() {
            var $priceExplain = $("#product-price-explain"),
            $priceViewMore = $("#price-view-more"),
            $priceViewMoreIcon = $("#price-view-more-icon");
            $(".overview-description").find(".tagsback").each(function() {
                $(this).poptip({
                    place: 6
                });
            });
            $priceExplain.poptip({
                place: 7
            });
            $priceViewMore.poptip({
                place: 10
            });
            $priceViewMore.bind({
                mouseenter: function(e) {
                    $priceViewMoreIcon.removeClass("price-view-more-out");
                    return $priceViewMoreIcon.addClass("price-view-more-over");
                },
                mouseleave: function(e) {
                    $priceViewMoreIcon.removeClass("price-view-more-over");
                    return $priceViewMoreIcon.addClass("price-view-more-out");
                }
            });
        })(); (function() {
            $navigateLinkCollection.bind("click",
            function(e) {
                e.preventDefault();
                var currentTarget = $(e.currentTarget),
                flag = currentTarget.attr("data-flag"),
                dest = $("#" + flag),
                destTopOffset = dest.offset().top;
                if (!dest.length) {
                    return;
                }
                $win.scrollTop((destTopOffset - navBarHeight));
            });
        })(); (function() {
            var automaticHeight = function() {
                var $trafficRight = $(".container .right");
                $trafficRight.each(function() {
                    var $prev = $(this).prev();
                    var prevHeight = $prev.actual("height");
                    $(this).css({
                        "height": prevHeight + "px",
                        "line-height": prevHeight + "px"
                    });
                });
            }
            automaticHeight();
        })(); (function() {
            $trafficTypeCollection.bind("click",
            function(e) {
                var $currentTarget = $(e.currentTarget),
                dest = $currentTarget.data("traffic");
                $trafficTypeCollection.removeClass("active");
                $currentTarget.addClass("active");
                $trafficContainerCollection.hide();
                $("#" + dest + "-container").show();
                automaticHeight();
            });
        })(); 
        (function() {
            var $navHeightHolder = $("#nav-height-holder"),
            navHeightHolderHeight = $navHeightHolder.height(),
            $productPreorder = $("#product-preorder"),
            $preorderAdjust = $("#preorder-adjust"),
            $preorderHeightHolder = $("#preorder-height-holder"),
            productContentOffset = {},
            travelDaysOffset = {},
            travelStart,
            adjustHeight,
            preorderHolderOffset,
            travelEnd,
            productTravelHeight,
            navTopOffset;
            var docHeightCallBack = function() {
                var productTravelHeight = $("#product-travel").height();
                navTopOffset = $navHeightHolder.offset().top;
                $navigateLinkCollection.each(function() {
                    var selectorId = $(this).attr("data-flag"),
                    $detailItem = $("#" + selectorId);
                    productContentOffset[selectorId] = $detailItem.offset().top;
                });
                if ($travelFixed.length) {
                    travelStart = $travelFixed.offset().top - 50;
                    travelEnd = travelStart + productTravelHeight - $travelFixed.height();
                    $travelFixedItems.each(function() {
                        var selectorId = $(this).attr("data-flag"),
                        $travelItem = $("#" + selectorId);
                        travelDaysOffset[selectorId] = $travelItem.offset().top;
                    });
                }
                adjustHeight = $preorderAdjust.height() if (adjustHeight) {
                    preorderHolderOffset = $preorderHeightHolder.offset().top;
                }
            }
            util.checkeDocHeight(docHeightCallBack);
            docHeightCallBack();
            var scrollCallBack = function() {
                var currentWinTopOffset = $win.scrollTop();
                if (currentWinTopOffset - navTopOffset >= 0) {
                    $navigateHeader.addClass("navigate-fixed");
                } else {
                    $navigateHeader.removeClass("navigate-fixed");
                }
                for (var i in productContentOffset) {
                    if (currentWinTopOffset + navHeightHolderHeight >= productContentOffset[i]) {
                        $navigateLinkCollection.removeClass("active color-style2");
                        $navigateList.find('[data-flag="' + i + '"]').addClass("active color-style2");
                    }
                }
                if ($travelFixed.length) {
                    if (currentWinTopOffset >= travelStart && currentWinTopOffset <= travelEnd) {
                        $travelFixed.addClass("travel-nav-fixed");
                    } else {
                        $travelFixed.removeClass("travel-nav-fixed");
                    }
                    for (var i in travelDaysOffset) {
                        if (currentWinTopOffset >= (travelDaysOffset[i] - navBarHeight)) {
                            $travelFixedItems.removeClass("active");
                            $travelFixed.find('[data-flag="' + i + '"]').addClass("active");
                        }
                    }
                }
                if (adjustHeight) {
                    if (currentWinTopOffset >= preorderHolderOffset && currentWinTopOffset <= (preorderHolderOffset + adjustHeight)) {
                        $productPreorder.addClass("navigate-fixed");
                    } else {
                        $productPreorder.removeClass("navigate-fixed");
                    }
                }
            }
            scrollCallBack();
            $win.scroll(scrollCallBack);
        })();
        var PreorderView = Backbone.Marionette.ItemView.extend({
            events: {
                "click #trip-time": "showDatapicker",
                "click #preorder-confirm-button": "preorderStart"
            },
            initialize: function(options) {
                _.extend(this, options);
                this.calendarView.bind("calendar:dateSelected", this.dateSelected, this);
                this.selectStyle();
            },
            dateSelected: function(data) {
                var $button = this.$el.find("#preorder-confirm-button");
                $button.html("立即预定");
                $button.attr("data-lock", true);
                $("#product-preorder").css({
                    "border-bottom": "1px solid #FFB346"
                });
                $("#preorder-start-time").val(data.date);
                window.lineDetail.javaCallback.dateSelected(e, data.date);
            },
            selectStyle: function() {
                pandora.selectModel({
                    autoWidth: false,
                    selectElement: this.$el.find(".adult-count")
                });
                pandora.selectModel({
                    autoWidth: false,
                    selectElement: this.$el.find(".children-count")
                });
                pandora.selectModel({
                    autoWidth: false,
                    selectElement: this.$el.find("#preorder-quantity")
                });
            },
            preorderStart: function(e) {
                e.stopPropagation();
                var $currentTarget = $(e.currentTarget);
                if ($currentTarget.attr("data-disable")) {
                    return;
                }
                if ($currentTarget.attr("data-lock")) {
                    window.lineDetail.javaCallback.submitData(e);
                    return;
                }
                if (!$calendarArea.is(":visible")) {
                    $calendarArea.show();
                }
            },
            showDatapicker: function(e) {
                e.stopPropagation();
                $calendarArea.show();
            }
        });
        $("body").bind("click",
        function(e) {
            $calendarArea.hide();
        });
        $calendarArea.bind("click",
        function(e) {
            e.stopPropagation();
        });
        var calendarView = new calendar({
            el: "#calendar-area",
            sourceURL: "./data.json?productId=" + window.lineDetail.productId
        });
        new PreorderView({
            el: "#product-preorder",
            calendarView: calendarView
        });
        var eventUtil = {
            toggleDetail: function(e) {
                var $currentTarget = $(e.currentTarget),
                $arrow = $currentTarget.find("i"),
                $detail = $currentTarget.parents(".adjust-product-item").find(".adjust-product-item-detail");
                if (!$arrow.length) {
                    return;
                }
                if ($arrow.hasClass("arrow-up")) {
                    $arrow.attr("class", "arrow");
                    $detail.hide();
                } else {
                    $arrow.addClass("arrow-up");
                    $detail.show();
                }
            },
            collapseDetail: function(e) {
                var $currentTarget = $(e.currentTarget),
                $dist = $currentTarget.parents(".adjust-product-item-detail"),
                $arrow = $dist.parents(".adjust-product-item").find(".toggle-detail .arrow");
                $dist.hide();
                $arrow.attr("class", "arrow");
            },
            doSelectAction: function(e) {
                e.stopPropagation();
                var $currentTarget = $(e.currentTarget),
                $currentItem = $currentTarget.parents(".adjust-product-item"),
                $target = $currentTarget.parents(".detail").find(".default"),
                targetHTML = $target.html(),
                $other = $currentTarget.parents(".other");
                $currentItem.find(".status").html('<i class="product-item-checked-icon"></i>');
                $target.html($currentItem[0].outerHTML);
                $currentItem.remove();
                $other.append(targetHTML);
                $other.find(".status").each(function() {
                    $(this).html('<button class="btn btn-mini do-select-action">选择</button>');
                });
                if ($currentItem.data("type") === "package") {
                    window.lineDetail.javaCallback.packageDoSelectAction(e, $currentItem);
                } else {
                    window.lineDetail.javaCallback.doSelectAction(e, $currentItem);
                }
            },
            replaceAction: function(e) {
                e.preventDefault();
                var $e = $(e.currentTarget);
                if ($e.data("toggle-text") === "insurance") {
                    window.lineDetail.javaCallback.insuranceReplaceCallback(e);
                    return;
                }
                window.lineDetail.javaCallback.replaceCallback(e);
            },
            upgradeOperator: function(e) {
                var $currentTarget = $(e.currentTarget),
                $status = $currentTarget.parent(),
                $operatorCollection = $currentTarget.parents(".detail-item").find(".product-upgrade-operator"),
                $infoNode = $currentTarget.parents(".adjust-product-item"),
                type = $infoNode.data("type"),
                id = $infoNode.data("id"),
                quantity = $infoNode.find(".optional-change-quantity").val(),
                price = $infoNode.data("price"),
                itemId = $infoNode.data("item-id"),
                visitTime = $infoNode.find(".change-date").val(),
                multipleSelect = $currentTarget.data("multiple-select");
                if (!multipleSelect) {
                    $operatorCollection.each(function() {
                        var $current = $(this);
                        if (this === $currentTarget[0]) {
                            return;
                        }
                        $current.html('<i class="product-item-checked-icon"></i>');
                    });
                }
                if ($currentTarget.find("i").length) {
                    $currentTarget.html('<span>取消</span>');
                } else {
                    $currentTarget.html('<i class="product-item-checked-icon"></i>');
                }
                window.lineDetail.javaCallback.checkCallback(e);
            },
            changeEvent: function(e) {
                window.lineDetail.javaCallback.dateSelected(e);
            },
            selectUpgradeAction: function(e) {
                window.lineDetail.javaCallback.selectUpgradeAction(e);
                var destHTML = '<i class="product-item-checked-icon"></i><a class="abort-upgrade-action">取消</a>';
                var $e = $(e.currentTarget);
                var $container = $e.parents(".upgrade-item-status");
                var collect = $e.parents(".upgrade-other").find(".upgrade-item-status");
                collect.each(function() {
                    $(this).html('<button class="btn btn-mini select-upgrade-action">选择</button>');
                });
                $container.html(destHTML);
            },
            abortUpgradeAction: function(e) {
                window.lineDetail.javaCallback.abortUpgradeAction(e);
                var destHTML = '<button class="btn btn-mini select-upgrade-action">选择</button>';
                var $e = $(e.currentTarget);
                var $container = $e.parents(".upgrade-item-status");
                $container.html(destHTML);
            }
        };
        var PreorderAdjust = Backbone.Marionette.ItemView.extend({
            initialize: function(options) {
                _.extend(this, options);
            },
            events: {
                "change .default select.change-quantity": "changeQuantity",
                "change .optional-change-quantity": "changeQuantity",
                "change .addtion-change-quantity": "changeAddtionQuantity",
                "change .default select.change-date": "changeDate",
                "change #adjust-optional select.change-date": "changeDate",
                "click .do-select-action": "doSelectAction",
                "click .reselect-date": "reselectDate",
                "click .show-more-date": "showMoreDate",
                "click .select-upgrade-action": "selectUpgradeAction",
                "click .abort-upgrade-action": "abortUpgradeAction",
            },
            doSelectAction: function(e) {
                eventUtil.doSelectAction(e);
            },
            reselectDate: function(e) {
                var $e = $(e.currentTarget);
                var $preorderStartTime = $("#preorder-start-time");
                $preorderStartTime.val($e.data("date"));
                window.lineDetail.javaCallback.dateSelected(e, data.date);
            },
            showMoreDate: function(e) {
                e.stopPropagation();
                $calendarArea.show();
            },
            changeAddtionQuantity: function(e) {
                var $current = $(e.currentTarget);
                if (parseInt($current.val())) {
                    $current.parents(".adjust-product-item").find(".product-upgrade-operator").html('<i class="product-item-checked-icon"></i>');
                } else {
                    $current.parents(".adjust-product-item").find(".product-upgrade-operator").html('');
                }
            },
            changeQuantity: function(e) {
                var $currentTarget = $(e.currentTarget),
                $infoNode = $currentTarget.parents(".adjust-product-item");
                productType = $infoNode.data("type"),
                id = $infoNode.data("id"),
                itemId = $infoNode.data("item-id"),
                price = $infoNode.data("price"),
                quantity = parseInt($currentTarget.val());
                $infoNode.find(".price span").html(price * quantity);
            },
            changeDate: function(e) {},
            selectUpgradeAction: function(e) {
                e.preventDefault();
                e.stopPropagation();
                eventUtil.selectUpgradeAction(e);
            },
            abortUpgradeAction: function(e) {
                e.preventDefault();
                e.stopPropagation();
                eventUtil.abortUpgradeAction(e);
            }
        });
        var preorderAdjust = new PreorderAdjust({
            el: "#preorder-adjust"
        }); (function() {
            $(".travel-fixed-item").click(function(e) {
                var $current = $(e.currentTarget);
                var flag = $current.data("flag");
                var destination = $("#" + flag)[0].offsetTop;
                $win.scrollTop(destination - navBarHeight);
                $(".travel-fixed-item").each(function() {
                    $(this).removeClass("active");
                });
                $current.addClass("active");
            });
        })(); (function() {
            $("body").delegate(".toggle-detail", "click", eventUtil.toggleDetail);
            $("body").delegate(".toggle-others", "click", eventUtil.replaceAction);
            $("body").delegate(".replace-button", "click", eventUtil.replaceAction);
            $("body").delegate("#adult-count", "change", eventUtil.changeEvent);
            $("body").delegate("#children-count", "change", eventUtil.changeEvent);
            $("body").delegate("#preorder-quantity", "change", eventUtil.changeEvent);
            $("body").delegate(".do-select-action", "click", eventUtil.doSelectAction);
            $("body").delegate(".collapse-detail", "click", eventUtil.collapseDetail);
            $("body").delegate(".select-upgrade-action", "click", eventUtil.selectUpgradeAction);
            $("body").delegate(".abort-upgrade-action", "click", eventUtil.abortUpgradeAction);
        })();
    };
    $(readyCallBack);
})();;
$(function() {
    if (!window.lineDetail.baidumap.length) {
        return;
    }
    var map = new BMap.Map("line-baidu-map");
    start(window.lineDetail.baidumap);
    function start(objs) {
        for (var i = 0; i < objs.length; i++) {
            point = new BMap.Point(objs[i].longitude, objs[i].latitude);
            addMarker(point);
        };
    }
    function addMarker(point) {
        var marker = new BMap.Marker(point);
        map.addOverlay(marker);
        marker.addEventListener("click",
        function() {
            this.openInfoWindow(infoWindow);
            document.getElementById('imgDemo').onload = function() {
                infoWindow.redraw();
            };
        });
    }
    map.centerAndZoom(point, 15);
    map.enableScrollWheelZoom();
});