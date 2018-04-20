using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.AttributeMapper
{
    /// <summary>
    /// 更新特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MapperAttribute : Attribute
    {
        public Updates Alter { get; set; }
    }
    public enum Updates
    {
        /// <summary>
        /// 必须更新
        /// </summary>
        Require = 10,
        /// <summary>
        /// 忽略更新
        /// </summary>
        Ignore = 20
    }
}
