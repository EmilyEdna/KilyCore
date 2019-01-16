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
        /// <summary>
        /// 判断是否为汉字
        /// </summary>
        /// <param name="text"></param>
        /// <returns>真：是汉字；假：不是</returns>
        public static bool CheckStringChineseUn(String Param)
        {
            bool result = false;
            foreach (char item in Param)
            {
                if (item >= 0x4e00 && item <= 0x9fbb)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
