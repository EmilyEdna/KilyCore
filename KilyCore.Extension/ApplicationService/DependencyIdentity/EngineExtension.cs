﻿using KilyCore.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace KilyCore.Extension.ApplicationService.DependencyIdentity
{
    public class EngineExtension
    {

        private static IEngine instance;
        public static IEngine Instance { get => instance; set => instance = value; }
        /// <summary>
        ///  确保方法同步实例化
        /// </summary>
        /// <param name="ReFind">是否重新查找</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool ReFind)
        {
            if (Instance == null || ReFind) //forceRecreate 是否强制重新查找IOC容器
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