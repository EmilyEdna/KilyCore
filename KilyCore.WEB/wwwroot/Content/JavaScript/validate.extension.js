/*
作者:Emily
blog:http://www.cnblogs.com/edna-lzh/
github:https://github.com/EmilyEdna
email:847432003@qq.com
*/

//验证电话
jQuery.validator.addMethod('IsPhone', function (value, element) {
    var reg = /^1[3|4|5|7|8][0-9]{9}$/;
    var length = value.length;
    return this.optional(element) || (length == 11 && reg.test(value));
}, "请填写正确的手机号!");
//验证邮箱
jQuery.validator.addMethod('IsEmail', function (value, element) {
    var reg = /^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$/;
    return this.optional(element) || reg.test(value);
}, "请输入正确的邮箱地址!");
//验证身份证
jQuery.validator.addMethod('IsIDCard', function (value, element) {
    var reg = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/;
    return this.optional(element) || reg.test(value);
}, "请输入正确的身份证号!");
//校验密码强度6-12位
jQuery.validator.addMethod('PassWordSave', function (value, element) {
    var length = $.trim(value).length;
    if (length < 6 || length > 17)
        return false;
    return true;
}, "请输入6-12位密码!");
//验证是否为数字
jQuery.validator.addMethod("IsNumber", function (value, element) {
    var reg = /^(?:0|[1-9]\d*)(?:\.\d*[1-9])?$/;
    return this.optional(element) || reg.test(value);
}, "请输入数字!");