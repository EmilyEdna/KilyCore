using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.Extension.ApplicationService.IocManager
{
    /// <summary>
    /// AutoFac拓展
    /// </summary>
    public class AutoFacManager : IAutoFacManager
    {
        public static readonly Lazy<AutoFacManager> lazy = new Lazy<AutoFacManager>(() => new AutoFacManager());
        public static AutoFacManager IocInstance => lazy.Value;
        public ContainerBuilder Builder = null;
        public AutoFacManager()
        {
            Builder = new ContainerBuilder();
        }
        /// <summary>
        /// 取得容器
        /// </summary>
        public IContainer Container { get; set; }
        /// <summary>
        /// 取出实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return Container == null ? default(T) : Container.Resolve<T>();
        }
        /// <summary>
        /// 完成注册
        /// </summary>
        public void CompleteBuiler()
        {
            Container = Builder.Build();
        }
    }
}
