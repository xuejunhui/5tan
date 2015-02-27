var isShowBox = false; //是否需要提示離開保存

$(document).ready(function () {
    //如果有在頁面內進行操作 跳轉頁面時提示是否保存
    $(".forminfo input,.forminfo textarea").change(function () {
        isShowBox = true;
    });

    //單條刪除
    $("table a[class='tablelink del']").click(function () {
        var a = $(this);
        var id = a.parent().parent().find("input[name=cbno]").val();
        deleteBox("/Ajax/" + a.data("value"), id);
    });

    //全選
    $("#cball").change(function () {
        $("table input[name=cbno]").attr("checked", this.checked);
    });

    $("#BulkDelete").click(function () {
        var id = getCheckBoxValues();
        if (id != "")
            deleteBox("/Ajax/" + $(this).data("value"), id);
    });

    //移除提示信息
    if ($("#msgInfo").length > 0) {
        $("#msgInfo .close").click(function () {
            removeMsgInfo();
        });
        var timeout = setTimeout(function () {
            removeMsgInfo();
            clearTimeout(timeout);
        }, 3000);
    }

    $(".select1").uedSelect({
        width: 345
    });
    $(".select2").uedSelect({
        width: 167
    });
    $(".select3").uedSelect({
        width: 100
    });

    $("select[id^=categoryDropDownBox]").each(function () {
        changeDropDown(this);
    });

    $("select[id^=categoryDropDownBox]").change(function () {
        changeDropDown(this);
    });


    $("li[id^=setEnableState]").click(function () {
        changeState(this, $(this).data("value"));
    });

    $("li[id^=topRecommend]").click(function () {
        changeTopState(this, $(this).data("value"));
    });

    $("body").keydown(function (event) {
        if (isShowBox) {
            if ((event.altKey) &&
          ((event.keyCode == 37) ||   //屏蔽 Alt+ 方向键 ←   
           (event.keyCode == 39)))   //屏蔽 Alt+ 方向键 →   
            {
                event.returnValue = false;
                showPromptBox("此页已屏蔽Alt+方向键进行页面刷新");
                return false;
            }
            if (event.keyCode == 8) {
                var elem = event.relatedTarget || event.srcElement || event.target || event.currentTarget;
                var name = elem.nodeName;
                if (name == 'BODY') {
                    showPromptBox("此頁已經屏蔽退格删除键進行頁面刷新");
                    return false; //屏蔽退格删除键    
                }
            }
            if (event.keyCode == 116) {
                showPromptBox("此页已屏蔽F5键进行页面刷新");
                return false; //屏蔽F5刷新键   
            }
            if ((event.ctrlKey) && (event.keyCode == 82)) {
                showPromptBox("此页已屏蔽alt+R组合键进行页面刷新");
                return false; //屏蔽alt+R   
            }
        }
    });
});

function changeTopState(obj, state) {
    var id = getCheckBoxValues();
    var button = $(obj);
    if (id != "") {
        $.ajax({
            url: "/Ajax/" + button.data("action"),
            type: 'POST',
            data: "{'id':'" + id + "','state':" + state + "}",
            contentType: 'application/json',
            beforeSend: function () {
            },
            complete: function () {
            },
            error: function (msg) {
                alert("置顶状态修改出现意外错误.");
            },
            success: function (result) {
                if (result)
                    refreshRightPanel(); //删除成功即刷新
                else
                    alert("置顶状态改失败.");
            }
        });
    }
}

function changeState(obj,state) {
    var id = getCheckBoxValues();
    var button = $(obj);
    if (id != "") {
        $.ajax({
            url: "/Ajax/" + button.data("action"),
            type: 'POST',
            data: "{'id':'" + id + "','state':" + state + "}",
            contentType: 'application/json',
            beforeSend: function () {
            },
            complete: function () {
            },
            error: function (msg) {
                alert("审核状态修改出现意外错误.");
            },
            success: function (result) {
                if (result)
                    refreshRightPanel(); //删除成功即刷新
                else
                    alert("审核状态修改失败.");
            }
        });
    }
}

function getCheckBoxValues() {
    var id = "";
    $("table input[name=cbno]:checked").each(function () {
        id += (id == "" ? "" : ",") + this.value;
    });
    if (id == "") {
        isShowBox = true;
        showPromptBox("请选择需要操作的数据后再点击按钮.");
        isShowBox = false;
    }
    return id;
}

function changeDropDown(obj) {
    if ($(obj).val() == "")
        return;
    var dname = obj.id.substring(obj.id.indexOf("_") + 1);
    var option = $(obj).find("option:selected");
    if (parseInt(option.data("value")) > 0) {
        LoadDropDown(option.data("autoeky"), option.data("selected"), dname, obj);
    } else {
        var panel = $(obj).parent().parent();
        removeEndAllChild(panel);
        $("select[name=" + dname + "]").attr("name", "");
        $(obj).attr("name", dname);
    }
}

function LoadDropDown(parentid, value, dname, obj) {
    var panel = $(obj).closest("li div.dropdownbox");
    var selectpanel = $(obj).parent().parent();
    $.ajax({
        url: "/Ajax/DropDownBox?parentid=" + parentid + "&value=" + value + "&dname=" + dname,
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "text",
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (msg) {
            alert("加载下拉框出现异常.");
        },
        success: function (result) {
            removeEndAllChild(selectpanel);
            $(result).appendTo(panel);
            var box = $("#categoryDropDownBox" + parentid + "_" + dname);
            box.change(function () {
                changeDropDown(this);
            });
            changeDropDown(box[0]);
            box.uedSelect({
                width: 167
            });
        }
    });
}

function removeEndAllChild(obj) {
    var r = true;
    while (r) {
        r = $(obj).next().length > 0;
        if (r)
            $(obj).next().remove();
    }
}

function removeMsgInfo() {
    $("#msgInfo").fadeOut("slow", function () {
        $("#msgInfo").remove();
    });
}

function print() {
    $("body").jqprint();
}

function showJumpBox(url) {
    showBox("showJumpBox", "页面跳转提示", "该页面内容尚未保存,您确定要离开此页面吗？", "如果是请点击确定,反之请点击取消.", "showsure", url);
}
function showPromptBox(title) {
    showBox("showPromptBox", "页面提示", title, "如果了解了请点击我知道了关闭此提示");
}

function deleteBox(url, id) {
    window.scrollTo(0, 0);
    if ($("#deleteBox").length == 0) {
        var html = "<div class=\"tip\" id=\"deleteBox\"><div class=\"tiptop\"><span>信息删除提示</span><a></a></div><div class=\"tipinfo\"><span><img src=\"/content/account/ticon.png\" /></span><div class=\"tipright\"><p>您确定要对所选数据进行删除吗 ？</p><cite>如果是请点确定,否则请点取消</cite></div></div><div class=\"tipbtn\"><input type=\"button\"  class=\"sure\" value=\"确定\" />&nbsp;<input type=\"button\"  class=\"cancel\" value=\"取消\" /></div></div>";
        $(html).appendTo($("body"));
    }

    $("#deleteBox").fadeIn(200);

    $("#deleteBox a,#deleteBox .cancel").click(function () {
        $("#deleteBox").fadeOut(200);
    });

    $("#deleteBox .sure").click(function () {
        $.ajax({
            url: url,
            type: 'POST',
            data: "{'id':'" + id + "'}",
            contentType: 'application/json',
            beforeSend: function () {
            },
            complete: function () {
                $("#deleteBox").fadeOut(200);
            },
            error: function (msg) {
                alert("信息删除出现意外错误.");
            },
            success: function (result) {
                if (result)
                    refreshRightPanel(); //删除成功即刷新
                else
                    alert("信息删除失败.");
            }
        });
    });

}

function refreshRightPanel() {
    window.parent.frames["rightFrame"].location.href = window.parent.frames["rightFrame"].location.href;
}

function showBox(boxid, tipTop, title, content, buttontype, url) {
    window.scrollTo(0, 0);
    if (isShowBox) {
        if ($("#" + boxid).length == 0) {
            var html = "<div class=\"tip\" id=\"" + boxid + "\"><div class=\"tiptop\"><span>" + tipTop + "</span><a></a></div><div class=\"tipinfo\"><span><img src=\"/content/account/ticon.png\" /></span><div class=\"tipright\"><p id=\"" + boxid + "title\">" + title + "</p><cite id=\"" + boxid + "content\">" + content + "</cite></div></div><div class=\"tipbtn\">" + (buttontype == "showsure" ? "<input type=\"button\"  class=\"sure\" value=\"确定\" />&nbsp;" : "") + "<input type=\"button\"  class=\"cancel\" value=\"" + (buttontype == "showsure" ? "取消" : "知道了") + "\" /></div></div>";
            $(html).appendTo($("body"));
        } else {
            $("#" + boxid + "content").html(content);
            $("#" + boxid + "title").html(title);
        }

        $("#" + boxid).fadeIn(200);

        $("#" + boxid + " a").click(function () {
            $("#" + boxid).fadeOut(200);
        });

        if (buttontype == "showsure") {
            $("#" + boxid + " .sure").click(function () {
                window.parent.frames["rightFrame"].location.href = url;
                $("#" + boxid).fadeOut(100);
            });
        }

        $("#" + boxid + " .cancel").click(function () {
            $("#" + boxid).fadeOut(100);
        });
    } else {
        window.parent.frames["rightFrame"].location.href = url;
    }
}


//屏蔽右鍵
$(document).bind("contextmenu", function (e) {
    return false;
});

function clearCache() {
    $.ajax({
        url: "/Ajax/ClearCache",
        type: 'POST',
        contentType: 'application/json',
        beforeSend: function () {
        },
        complete: function () {
            alert("清除缓存完成");
        },
        error: function (msg) {
            alert("清除缓存发生意外错误.");
        },
        success: function () {
           
        }
    });
}