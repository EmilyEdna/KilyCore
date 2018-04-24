var UserInfo = UserInfo || {};
//设置菜单
$.fn.SetTree = function (data) {
    var html = '';
    $.each(data, function (i, obj) {
        //父菜单
        html += '<li id="' + obj.MenuId + '"><a href="#"><i class="' + obj.MenuIcon + '"></i><span class="nav-label">' + obj.MenuName + '</span><span class="fa arrow"></span></a>';
        if (obj.HasChildrenNode) {
            $.each(obj.MenuChildren, function (n, child) {
                html += '<ul class="nav nav-second-level"><li id="' + child.MenuId + '"><a class="J_menuItem" href="' + child.MenuAddress + '">' + child.MenuName + '</a></li></ul>';
            });
            html += '</li>';
        } else {
            html += '</li>';
        }
    });
    this.append(html);
}
//Jq拓展方法 - 序列化表单
$.fn.SerializeJson = function () {
    var serializeObj = {};
    var array = this.serializeArray();
    var str = this.serialize();
    $(array).each(function () {
        if (serializeObj[this.name]) {
            if ($.isArray(serializeObj[this.name])) {
                serializeObj[this.name].push(this.value);
            } else {
                serializeObj[this.name] = [serializeObj[this.name], this.value];
            }
        } else {
            serializeObj[this.name] = this.value;
        }
    });
    return serializeObj;
};
//获取用户信息
$.fn.GetUserInfo = function () {
    UserInfo = JSON.parse(localStorage.UserInfo);
}
//获取企业用户信息
$.fn.GetCompanyInfo = function () {
    CompanyUser = JSON.parse(localStorage.CompanyUser);
}
//只针对角色页面使用
$.fn.SerializeOver = function () {
    var serializeObj = {};
    var datas = [];
    $('[data-values]', this).each(function () {
        datas.push($(this).data().values);
    });
    var array = this.serializeArray();
    var str = this.serialize();
    $(array).each(function () {
        if (serializeObj[this.name]) {
            if ($.isArray(serializeObj[this.name])) {
                serializeObj[this.name].push(this.value);
            } else {
                serializeObj[this.name] = [serializeObj[this.name], this.value];
            }
        } else {
            serializeObj[this.name] = this.value;
        }
    });
    serializeObj["AuthorPath"] = datas;
    return serializeObj;
}