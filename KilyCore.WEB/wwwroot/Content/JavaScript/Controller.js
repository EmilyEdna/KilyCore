var controller = controller || {};
var host = "http://localhost:50039/api/";
var WebUrl = "http://localhost:50070/";
//ajax请求
controller.ajax = function (option) {
    defaultOption = {
        url: undefined,
        type: "GET",
        data: undefined,
        dataType: "json",
        crossDomain: undefined,
        xhrFields: undefined,
        async: true,
        cache: false,
        success: null
    };
    var options = $.extend(defaultOption, option);
    options.data.timespan = controller.SetRequestTime();
    return $.ajax({
        url: host + options.url,
        data: options.data,
        dataType: options.dataType,
        type: options.type,
        async: options.async,
        cache: options.cache,
        crossDomain: options.crossDomain,
        xhrFields: options.xhrFields,
        success: function (result) {
            options.success(result);
        },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Token", controller.GetCookie().Token == undefined ? "" : controller.GetCookie().Token);
            xhr.setRequestHeader("ApiKey", controller.GetCookie().ApiKey == undefined ? "" : controller.GetCookie().ApiKey);
            xhr.setRequestHeader("SysKey", controller.GetCookie().SysKey == undefined ? "" : controller.GetCookie().SysKey);
        },
        error: function (xhr, msg) {
            if (xhr.status == 401)
                controller.Confirm("您还未登录系统，请先登录", function (flag) { controller.SetHref("Login") });
            else if (xhr.status == 404)
                controller.Confirm("页面未找到!", function (flag) { });
            else if (xhr.status == 500)
                controller.Confirm("服务器发生错误", function (flag) { });
            else if (xhr.status == 502)
                controller.Confirm("无效的网关", function (flag) { });
            else if (xhr.status==0)
                controller.Confirm("服务器停止运行", function (flag) { });
            else
                controller.Confirm(msg, function (flag) { });
        }
    });
}
//设置Cookie
controller.SetCookie = function (option) {
    var flag = controller.JsonObject(option);
    if (!flag) var obj = JSON.parse(option);
    else var obj = option;
    if (!(obj.hasOwnProperty("RSAToKen") && obj.hasOwnProperty("RSAApiKey") && obj.hasOwnProperty("RSASysKey"))) return controller.Msg("不包含指定列");
    $.cookie("Token", obj.RSAToKen, {
        expires: new Date().setTime(controller.SetRequestTime() + (120 * 120 * 1000)),
        path: '/'
    }) //2小时过期
    $.cookie("ApiKey", obj.RSAApiKey, {
        expires: new Date().setTime(controller.SetRequestTime() + (120 * 120 * 1000)),
        path: '/'
    }) //2小时过期
    $.cookie("SysKey", obj.RSASysKey, {
        expires: new Date().setTime(controller.SetRequestTime() + (120 * 120 * 1000)),
        path: '/'
    }) //2小时过期
    if (obj.SysAdmin != undefined)
        localStorage.UserInfo = JSON.stringify(obj.SysAdmin);//保存用户信息
    else if (obj.ComAdmin != undefined)
        localStorage.CompanyUser = JSON.stringify(obj.ComAdmin);//保存企业用户信息
}
//删除Cookie
controller.DeleteCookie = function () {
    $.removeCookie('Token', { path: '/' });
    $.removeCookie('ApiKey', { path: '/' });
    $.removeCookie('SysKey', { path: '/' });
}
//获取Cookie
controller.GetCookie = function () {
    return {
        Token: $.cookie("Token"),
        ApiKey: $.cookie("ApiKey"),
        SysKey: $.cookie("SysKey")
    }
}
//设置请求时间
controller.SetRequestTime = function () {
    return dt = new Date().getTime();
}
//清空本地存储
controller.ClearLocal = function () {
    localStorage.clear();
}
//获取URL参数
controller.GetParam = function (option) {
    var reg = new RegExp("(^|&)" + option + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return decodeURI(r[2]); return null; //返回参数值
}
//设置表单控件值
controller.SetCtrlValue = function (element, option) {
    for (var key in option) {
        var node = $("[name='" + key + "']", $(element));
        if (node.length > 0) {
            var type = node.attr("type");
            var value = option[key];
            switch (type) {
                case "checkbox":
                    if (value == 1)
                        node.prop("checked", true);
                    else
                        node.prop("checked", false);
                    break;
                case "select":
                    node.find("option[value='" + value + "']").attr("selected", true);
                    break;
                case "image":
                    node.attr("src", value);
                    break;
                default:
                    node.val(value);
                    break;
            }
        }
    }
}
//判断是否Json对象
controller.JsonObject = function (option) {
    return typeof (option) == "object" && Object.prototype.toString.call(option).toLowerCase() == "[object object]" && !option.length; //true or false

}
//对话框
controller.Confirm = function (option, callBack, title) {
    if (!title)
        title = "系统提示";
    top.layer.confirm(option, {
        icon: 6,
        title: title,
        skin: 'layui-layer-molv',
        anim: 4,
        btn: ['确认', '取消']
    }, function (index) {
        callBack(true, index);
        top.layer.close(index);
    }, function (index) {
        callBack(false, index);
        top.layer.close(index);
    });

}
//提示框
controller.Msg = function (option) {
    top.layer.msg(option);
}
//对话框
controller.Alter = function (option, title) {
    if (!title)
        title = "信息";
    top.layer.alert(option,
        {
            title: title,
            anim: 4,
            skin: 'layui-layer-molv'
        });
}
//输入框
controller.Prompt = function (option, callBack) {
    defaultOption = {
        formType: 0, //0文本 1密码 2多行文本
        value: undefined,
        title: undefined,
        area: ['500px', '300px']
    };
    var options = $.extend(defaultOption, option);
    top.layer.prompt(options, callBack);
}
//选项卡
controller.Tab = function (option) {
    top.layer.tab({
        area: ['600px', '300px'],
        tab: option //json数组
    });
}
//吸附卡
controller.Tips = function (title, element, option, flag) {
    if (!flag)
        layer.tips(title, element, option);
    else
        top.layer.tips(title, element, option);

}
//图片框
controller.Photos = function (option) {
    //option格式
    //var option = {
    //    title: "测试",
    //    data: [{
    //        alt: "图面名称",
    //        pid: "图片Id",
    //        src: "Content/HPlus/img/locked.png",//图片地址
    //        thumb: ""//缩略图地址
    //    }]
    //}
    defaultOption = {
        title: undefined,
        id: 1,
        start: 0,
        data: undefined
    }
    var json = $.extend(defaultOption, option);
    layer.photos({
        photos: json,
        anim: 4,
        closeBtn: true
    });
}
//打开视窗
controller.OpenWindow = function (option) {
    var defaultOptions = $.extend({
        id: null,
        title: "系统窗口",
        width: "100px",
        height: "100px",
        url: "",
        shade: 0.3,
        data: null,
        btn: ['确定', '取消'],
        callBack: null,
        maxmin: true,
        scrollbar: false,
        fixed: true,
        resize: true,
    }, option || {});
    var options = {
        id: defaultOptions.id,
        type: 2,
        shade: defaultOptions.shade,
        title: defaultOptions.title,
        fixed: defaultOptions.fixed,
        resize: defaultOptions.resize,
        offset: "50px",
        anim: 4,
        skin: 'layui-layer-molv',
        scrollbar: defaultOptions.scrollbar,
        maxmin: defaultOptions.maxmin,
        area: [defaultOptions.width, defaultOptions.height],
        content: WebUrl + defaultOptions.url,
        success: function (layero, index) {
            var iframeWin = top.window[layero.find('iframe')[0]['name']];
            iframeWin.popup = {
                source: window,
                close: function () {
                    top.layer.close(index);
                },
                data: defaultOptions.data
            };

            if (iframeWin.start)
                iframeWin.start();
        },
        btn: defaultOptions.btn,
        yes: function (index, layero) {
            if (defaultOptions.callBack) {
                var iframeWin = top.window[layero.find("iframe")[0]['name']];
                iframeWin.source = window;
                iframeWin.popClose = function () {
                    top.layer.close(index);
                };
                defaultOptions.callBack(iframeWin);
            }
        }
    };
    top.layer.open(options);
}
//关闭层
controller.Close = function () {
    layer.closeAll();
}
//设置跳转
controller.SetHref = function (option) {
    window.location.href = option;
}
//表格控件
controller.Table = function (element, option) {
    defaultOption = {
        height: undefined,
        striped: true,
        data: [],
        method: 'post',
        url: undefined,
        cache: true,
        async: true,
        dataType: "json",
        queryParamsType: '',
        contentType: "application/x-www-form-urlencoded",
        pagination: true,
        clickToSelect: true,
        toolbar: undefined,
        param: {},
        sidePagination: "server",
        responseHandler: function (res) {
            return res.data;
        }
    };
    var options = $.extend(defaultOption, option);
    options.queryParams = function (params) {
        return $.extend(params, { QueryParam: bootstrapTable.queryParams });
    };
    var bootstrapTable = {
        queryParams: option.param || {},
        bootstrapTable: function () {
            return $table.bootstrapTable.apply($table, arguments);
        },
        search: function (FormElement) {
            $.extend(bootstrapTable.queryParams, (FormElement instanceof Object) ? FormElement : $(this).SerializeJson());
            bootstrapTable.bootstrapTable('refresh', {
                pageNumber: 1
            });
        },
        refresh: function () {
            bootstrapTable.bootstrapTable("refresh");
        }
    };
    //绑定刷新按钮
    if (option.refreshBtn) {
        $(option.refreshBtn).on("click", function () {
            bootstrapTable.refresh();
        });
    };
    //绑定表达查询
    if (option.formElement) {
        $(option.formElement).submit(function (event) {
            event.preventDefault();
            var queryObj = $(this).SerializeJson();
            bootstrapTable.search(queryObj);
        });
    }
    //初始化表格
    var $table = $(element).bootstrapTable({
        height: options.height,
        striped: options.striped,
        pagination: options.pagination,
        cache: options.cache,
        async: options.async,
        sidePagination: options.sidePagination,
        method: options.method,
        url: options.url,
        queryParamsType: options.queryParamsType,
        queryParams: options.queryParams,
        responseHandler: options.responseHandler,
        data: options.data,
        contentType: options.contentType,
        ajax: controller.ajax,
        clickToSelect: options.clickToSelect,
        toolbar: options.toolbar,
        toolbarAlign: 'right'
    });
    return bootstrapTable;
}
//验证表单-针对弹出框
controller.ValidateConfirm = function (element, option) {
    option = option || {};
    return $(element).validate($.extend({
        meta: "validate",
        ignore: [],
        onkeyup: false,
        onfocusout: false,//失去焦点不验证
        showErrors: function (errormap, errorlist) {
            $.each(errorlist, function (index, item) {
                controller.Msg(item.message);
                item.element.focus();
                return false;
            });
        },
        submitHandler: function (form) {
            var $submitForm = $(form);
            var url = $submitForm.attr("action");
            var method = $submitForm.prop("method");
            var data = $submitForm.SerializeJson();
            if (option.AddParam)
                data = option.AddParam(data);
            controller.ajax({
                url: url,
                data: data,
                type: method,
                success: function (data) {
                    if (option.ajaxSuccess)
                        option.ajaxSuccess(data);
                    else {
                        if (window.source)
                            window.source.options.$table.refresh();
                        if (window.popClose)
                            window.popClose();
                    }

                },
                fail: option.ajaxFail
            });
        }
    }, option));
}
//验证表单
controller.Validate = function (element, data) {
    return $(element).validate({
        meta: 'validate',
        ignore: [],
        onkeyup: false,
        onfocusout: false,
        showErrors: function (errormap, errorlist) {
            $.each(errorlist, function (index, item) {
                controller.Msg(item.message);
                item.element.focus();
                return false;
            });
        },
        submitHandler: function (form) {
            var action = $(form).attr("action");
            var method = $(form).attr("method");
            controller.ajax({
                url: action,
                type: method,
                data: data,
                success: function (result) {
                    if (result.flag == 1) {
                        $(form).find("input").val("");
                        controller.Msg(result.data);
                    }
                }
            });
        }
    });
}
//TreeView控件
controller.TreeCtrl = function (element, option, document) {
    var defaultOption = {
        levels: 5,
        data: undefined,                 //数据源
        showCheckbox: true,        //是否显示复选框
        multiSelect: true,    //多选
    };
    var options = $.extend(defaultOption, option);
    return $(element).treeview({
        levels: options.levels,
        data: options.data,                 //数据源
        showCheckbox: options.showCheckbox,        //是否显示复选框
        multiSelect: options.multiSelect,    //多选
        onNodeChecked: NodeChecked,
        onNodeUnchecked: NodeUnchecked
    });
    //选中状态
    var nodeCheckedSilent = false;
    function NodeChecked(event, node) {
        CheckStatus();
        if (nodeCheckedSilent) {
            return;
        }
        nodeCheckedSilent = true;
        CheckAllParent(node);
        CheckAllSon(node);
        nodeCheckedSilent = false;
    }
    function CheckAllParent(node) {
        $(element).treeview('checkNode', node.nodeId, { silent: true });
        var parent = $(element).treeview('getParent', node.nodeId);//返回选中的数组
        if (!(('nodeId') in parent))
            return;
        else
            CheckAllParent(parent);
    }
    function CheckAllSon(node) {
        $(element).treeview('checkNode', node.nodeId, { silent: true });
        if (node.nodes != null && node.nodes.length > 0)
            $.each(node.nodes, function (i, data) {
                CheckAllSon(data);
            });
    }
    //取消选中
    var nodeUncheckedSilent = false;
    function NodeUnchecked(event, node) {
        CheckStatus();
        if (nodeUncheckedSilent)
            return;
        nodeUncheckedSilent = true;
        UnCheckAllParent(node);
        UnCheckAllSon(node);
        nodeUncheckedSilent = false;
    }
    function UnCheckAllParent(node) {
        $(element).treeview('uncheckNode', node.nodeId, { silent: true });
        var siblings = $(element).treeview('getSiblings', node.nodeId);
        var parentNode = $(element).treeview('getParent', node.nodeId);
        if (!("nodeId" in parentNode)) {
            return;
        }
        var isAllUnchecked = true;  //是否全部没选中  
        for (var i in siblings) {
            if (siblings[i].state.checked) {
                isAllUnchecked = false;
                break;
            }
        }
        if (isAllUnchecked) {
            UnCheckAllParent(parentNode);
        }
    }
    function UnCheckAllSon(node) {
        $(element).treeview('uncheckNode', node.nodeId, { silent: true });
        if (node.nodes != null && node.nodes.length > 0) {
            for (var i in node.nodes) {
                UnCheckAllSon(node.nodes[i]);
            }
        }
    }
    //处理选中
    function CheckStatus() {
        var CheckList = $(element).treeview("getChecked", [{ silent: true }]);
        var html = '';
        $.each(CheckList, function (i, data) {
            html += '<p class="text text-info" data-values=' + data.id + '><i class="fa fa-check-circle" aria-hidden="true"></i>' + data.text + '</p>';
        });
        $(document).html(html);
    }
}
//日期选择器
controller.LayDate = function (option) {
    defaultOption = {
        elem: undefined,
        theme: 'molv',
        type: 'datetime',
        format: 'yyyy-MM-dd HH:mm:ss',
        showBottom: true,
        calendar: true
    };
    Setting = $.extend(defaultOption, option);
    laydate.render(Setting);
}
//文件上传
controller.Upload = function (option) {
    defaultOption = {
        url: undefined,
        data: null,
        maxNum: 3,
        element: undefined,
        maxSize:2,
        success: null
    }
    var options = $.extend(defaultOption, option)
    return $(options.element).ajaxImageUpload({
        url: WebUrl+options.url,
        data: options.data,
        maxNum: options.maxNum,
        maxSize: options.maxSize,
        success: function (result) {
            options.success(result);
        },
        error: function (errer) {
            controller.Msg(errer);
        }
    });
}