using System;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Service.ConstMessage
{
    public class ServiceMessage
    {
        /// <summary>
        /// 新增成功
        /// </summary>
        public const string INSERTSUCCESS = "新增成功!";

        /// <summary>
        /// 新增失败
        /// </summary>
        public const string INSERTFAIL = "新增失败!";

        /// <summary>
        /// 更新成功
        /// </summary>
        public const string UPDATESUCCESS = "更新成功!";

        /// <summary>
        /// 更新失败
        /// </summary>
        public const string UPDATEFAIL = "更新失败!";

        /// <summary>
        /// 删除成功
        /// </summary>
        public const string REMOVESUCCESS = "删除成功!";

        /// <summary>
        /// 删除失败
        /// </summary>
        public const string REMOVEFAIL = "删除失败!";

        /// <summary>
        /// 操作成功
        /// </summary>
        public const string HANDLESUCCESS = "操作成功!";

        /// <summary>
        /// 操作失败
        /// </summary>
        public const string HANDLEFAIL = "操作失败!";

        /// <summary>
        /// 保存成功后将不可更改
        /// </summary>
        public const string SAVENOTUPDATESUCCESS = "保存成功后将不可更改!";

        /// <summary>
        /// 体验版1W枚
        /// </summary>
        public const Int64 TEST = 10000;

        /// <summary>
        /// 基础版10W枚
        /// </summary>
        public const Int64 BASE = 100000;

        /// <summary>
        /// 升级版100W枚
        /// </summary>
        public const Int64 LEVEL = 1000000;

        /// <summary>
        /// 旗舰版100W枚
        /// </summary>
        public const Int64 ENTERPRISE = 1000000;
    }
}