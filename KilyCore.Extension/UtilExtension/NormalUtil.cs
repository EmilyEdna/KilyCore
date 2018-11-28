using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Extension.UtilExtension
{
    /// <summary>
    /// 一般工具类
    /// </summary>
    public class NormalUtil
    {
        /// <summary>
        /// 创建随机数
        /// </summary>
        /// <param name="StarNo"></param>
        /// <param name="EndNo"></param>
        /// <returns></returns>
        public static string CreateRandomNum(int StarNo = 0, int EndNo = 10)
        {
            Random Rd = new Random();
            int Result = Rd.Next(StarNo, EndNo);
            return Result.ToString();
        }
    }
}
