using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Quartz
{
    public class QuartzCoreFactory
    {
        public static IQuartzCore QuartzCore()
        {
            return new QuartzCore();
        }

    }
}
