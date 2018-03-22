using System;
using System.Collections.Generic;
using System.Text;

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
