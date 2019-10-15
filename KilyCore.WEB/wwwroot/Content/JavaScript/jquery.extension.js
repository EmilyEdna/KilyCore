/*
作者:Emily
blog:http://www.cnblogs.com/edna-lzh/
github:https://github.com/EmilyEdna
email:847432003@qq.com
*/
var UserInfo = UserInfo || {};
var CompanyUser = CompanyUser || {};
var RepastInfo = RepastInfo || {};
var Cooker = Cooker || {};
var Govt = Govt || {};
/**
 * 设置菜单
 * @param {any} 数据
 */
$.fn.SetTree = function (data) {
    var html = '';
    $.each(data, function (i, obj) {
        //父菜单
        html += '<li id="' + obj.MenuId + '"><a href="#"><i class="' + obj.MenuIcon + '"></i><span class="nav-label">' + obj.MenuName + '</span><span class="fa arrow"></span></a>';
        if (obj.HasChildrenNode) {
            $.each(obj.MenuChildren, function (n, child) {
                html += '<ul class="nav nav-second-level"><li id="' + child.MenuId + '"><a class="J_menuItem" href="' + child.MenuAddress + '" id="' + child.MenuName+'">' + child.MenuName + '</a></li></ul>';
            });
            html += '</li>';
        } else {
            html += '</li>';
        }
    });
    this.append(html);
}
/**
 * Jq拓展方法 - 序列化表单
 * */
$.fn.SerializeJson = function () {
    var serializeObj = {};
    var array = this.serializeArray();
    var form = this;
    $(array).each(function () {
        if (serializeObj[this.name]) {
            if ($.isArray(serializeObj[this.name])) {
                serializeObj[this.name].push(this.value);
            } else {
                serializeObj[this.name] = [serializeObj[this.name], this.value];
                serializeObj[this.name]= $(form).find("input[name='" + this.name + "']:checked").val();
            }
        } else {
            serializeObj[this.name] = this.value;
        }
    });
    return serializeObj;
};
/**
 * 获取用户信息
 * */
$.fn.GetUserInfo = function () {
    if (localStorage.UserInfo != undefined) {
        UserInfo = JSON.parse(localStorage.UserInfo);
        return false;
    }
    else
        return true;
}
/**
 * 获取企业用户信息
 * */
$.fn.GetCompanyInfo = function () {
    if (localStorage.CompanyUser != undefined) {
        CompanyUser = JSON.parse(localStorage.CompanyUser);
        return false;
    }
    else
        return true;
}
/**
 * 获取餐饮企业用户信息
 * */
$.fn.GetRepastInfo = function () {
    if (localStorage.RepastUser != undefined) {
        RepastInfo = JSON.parse(localStorage.RepastUser);
        return false;
    }
    else
        return true;
}
/**
 * 获取厨师信息
 * */
$.fn.GetCookInfo = function () {
    if (localStorage.Cooker != undefined) {
        Cooker = JSON.parse(localStorage.Cooker);
        return false;
    }
    else
        return true;
}
/**
 * 获取政府监管信息
 * */
$.fn.GetGovtInfo = function () {
    if (localStorage.Govt != undefined) {
        Govt = JSON.parse(localStorage.Govt);
        return false;
    }
    else
        return true;
}
/**
 * 序列化表单-只针对角色页面使用
 * */
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