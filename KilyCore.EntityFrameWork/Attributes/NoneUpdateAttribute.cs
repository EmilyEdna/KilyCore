using System;

namespace KilyCore.EntityFrameWork.Attributes
{
    /// <summary>
    /// 忽略更新字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NoneUpdateAttribute : Attribute
    {
    }
}