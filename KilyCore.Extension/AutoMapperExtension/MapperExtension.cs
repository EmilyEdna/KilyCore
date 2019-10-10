using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Extension.AutoMapperExtension
{
    /// <summary>
    /// AutoMapper拓展
    /// </summary>
    public static class MapperExtension
    {
        /// <summary>
        ///  实体转数据传输对象，数据传输对象转实体
        /// </summary>
        /// <typeparam name="T">实体或数据传输对象</typeparam>
        /// <typeparam name="K">实体或数据传输对象</typeparam>
        /// <param name="obj">实体或数据传输对象参数</param>
        /// <returns></returns>
        public static K MapToObj<T, K>(this T Obj)
        {
            if (Obj == null) return default(K);
            IMapper mapper = new MapperConfiguration(t => t.CreateMap(Obj.GetType(), typeof(K))).CreateMapper();
            return mapper.Map<K>(Obj);
        }

        /// <summary>
        /// 数据传输对象转实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T MapToEntity<T>(this Object Obj)
        {
            if (Obj == null) return default(T);
            IMapper mapper = new MapperConfiguration(t => t.CreateMap(Obj.GetType(), typeof(T))).CreateMapper();
            return mapper.Map<T>(Obj);
        }

        /// <summary>
        /// 自定义过滤映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T MapToEntity<T>(this Object Obj, String PropName)
        {
            if (Obj == null) return default(T);
            MapperConfigurationExpression expression = new MapperConfigurationExpression();
            IMappingExpression mapping = expression.CreateMap(Obj.GetType(), typeof(T));
            mapping.ForMember(PropName, x => x.Ignore());
            IMapper mapper = new MapperConfiguration(expression).CreateMapper();
            return mapper.Map<T>(Obj);
        }

        /// <summary>
        /// 集合映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<K> MapToList<T, K>(this IEnumerable<T> Obj)
        {
            IMapper mapper = new MapperConfiguration(t => t.CreateMap(Obj.GetType(), typeof(K))).CreateMapper();
            return mapper.Map<List<K>>(Obj);
        }

        /// <summary>
        /// 集合映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IList<T> MapToList<T>(this IEnumerable<T> Obj)
        {
            IMapper mapper = new MapperConfiguration(t => t.CreateMap(Obj.GetType(), typeof(T))).CreateMapper();
            return mapper.Map<List<T>>(Obj);
        }
    }
}