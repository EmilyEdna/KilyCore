using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点22分
/// </summary>
namespace KilyCore.DataEntity.ResponseMapper.System
{
    /// <summary>
    /// 一级域树
    /// </summary>
    public class ResponseTree
    {
        /// <summary>
        /// 树Id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        /// <summary>
        /// 字体颜色
        /// </summary>
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }
        /// <summary>
        /// 选中时候图标样式 Font Awesome
        /// </summary>
        [JsonProperty(PropertyName = "selectedIcon")]
        public string SelectedIcon { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        [JsonProperty(PropertyName = "backClolor")]
        public string BackClolor { get; set; }
        /// <summary>
        /// 初始状态
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public States State { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        [JsonProperty(PropertyName = "nodes")]
        public IEnumerable<ResponseCityTree> Nodes { get; set; }
    }
    /// <summary>
    /// 二级域树
    /// </summary>
    public class ResponseCityTree
    {
        /// <summary>
        /// 树Id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        /// <summary>
        /// 字体颜色
        /// </summary>
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }
        /// <summary>
        /// 选中时候图标样式 Font Awesome
        /// </summary>
        [JsonProperty(PropertyName = "selectedIcon")]
        public string SelectedIcon { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        [JsonProperty(PropertyName = "backClolor")]
        public string BackClolor { get; set; }
        /// <summary>
        /// 初始状态
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public States State { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        [JsonProperty(PropertyName = "nodes")]
        public IEnumerable<ResponseAreaTree> Nodes { get; set; }
    }
    /// <summary>
    /// 三级域树
    /// </summary>
    public class ResponseAreaTree
    {
        /// <summary>
        /// 树Id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        /// <summary>
        /// 字体颜色
        /// </summary>
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }
        /// <summary>
        /// 选中时候图标样式 Font Awesome
        /// </summary>
        [JsonProperty(PropertyName = "selectedIcon")]
        public string SelectedIcon { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        [JsonProperty(PropertyName = "backClolor")]
        public string BackClolor { get; set; }
        /// <summary>
        /// 初始状态
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public States State { get; set; }
        [JsonProperty(PropertyName = "nodes")]
        public IEnumerable<ResponseTownTree> Nodes { get; set; }
    }
    /// <summary>
    /// 四级域树
    /// </summary>
    public class ResponseTownTree
    {
        /// <summary>
        /// 树Id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        /// <summary>
        /// 字体颜色
        /// </summary>
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }
        /// <summary>
        /// 选中时候图标样式 Font Awesome
        /// </summary>
        [JsonProperty(PropertyName = "selectedIcon")]
        public string SelectedIcon { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        [JsonProperty(PropertyName = "backClolor")]
        public string BackClolor { get; set; }
        /// <summary>
        /// 初始状态
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public States State { get; set; }
    }
    /// <summary>
    /// 一级菜单树
    /// </summary>
    public class ResponseParentTree
    {
        /// <summary>
        /// 树Id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        /// <summary>
        /// 字体颜色
        /// </summary>
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }
        /// <summary>
        /// 选中时候图标样式 Font Awesome
        /// </summary>
        [JsonProperty(PropertyName = "selectedIcon")]
        public string SelectedIcon { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        [JsonProperty(PropertyName = "backClolor")]
        public string BackClolor { get; set; }
        /// <summary>
        /// 初始状态
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public States State { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        [JsonProperty(PropertyName = "nodes")]
        public IQueryable<ResponseChildTree> Nodes { get; set; }
    }
    /// <summary>
    /// 二级菜单树
    /// </summary>
    public class ResponseChildTree
    {
        /// <summary>
        /// 树Id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        /// <summary>
        /// 字体颜色
        /// </summary>
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }
        /// <summary>
        /// 选中时候图标样式 Font Awesome
        /// </summary>
        [JsonProperty(PropertyName = "selectedIcon")]
        public string SelectedIcon { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        [JsonProperty(PropertyName = "backClolor")]
        public string BackClolor { get; set; }
        /// <summary>
        /// 初始状态
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public States State { get; set; }
    }
    /// <summary>
    /// 状态
    /// </summary>
    public class States
    {
        public States()
        {
            Checked = true;
        }
        [JsonProperty(PropertyName = "checked")]
        public bool Checked { get; set; }
    }
}
