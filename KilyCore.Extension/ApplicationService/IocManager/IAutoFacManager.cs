using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Extension.ApplicationService.IocManager
{
    public interface IAutoFacManager
    {
        void CompleteBuiler();
        T Resolve<T>();
    }
}
