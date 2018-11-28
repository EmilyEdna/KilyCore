using KilyCore.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.Extension.ApplicationService.DependencyIdentity
{
    public class EngineExtension
    {
        public static IEngine Instance { get; set; }
        /// <summary>
        ///  确保方法同步实例化
        /// </summary>
        /// <param name="ReFind">是否重新查找</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool ReFind)
        {
            if (Instance == null || ReFind) //是否强制重新查找IOC容器
            {
                Instance = CreateEngineInstance();
            }
            return Instance;
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns></returns>
        private static IEngine CreateEngineInstance()
        {
            Type type = Configer.Assembly.SelectMany(t => t.ExportedTypes.Where(x => x.GetInterfaces().Contains(typeof(IEngine)))).FirstOrDefault();
            var defaultEngine = (IEngine)Activator.CreateInstance(type);
            return defaultEngine;
        }

        /// <summary>
        /// 获取当前引擎实例化对象
        /// </summary>
        /// <returns></returns>
        public static IEngine Context
        {
            get
            {
                if (Instance == null)
                {
                    Initialize(true);
                }
                return Instance;
            }
        }

    }
}
