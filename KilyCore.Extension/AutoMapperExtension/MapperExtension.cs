using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
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
            Mapper.Initialize(t => t.CreateMap<T, K>());
            return Mapper.Map<K>(Obj);
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
            Mapper.Initialize(t => t.CreateMap(Obj.GetType(), typeof(T)));
            return Mapper.Map<T>(Obj);
        }
        /// <summary>
        /// 自定义过滤映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T MapToEntity<T>(this Object Obj, String PropName)
        {
            Mapper.Initialize(t => t.CreateMap(Obj.GetType(), typeof(T)).ForMember((PropName), x => { x.Ignore(); }));
            return Mapper.Map<T>(Obj);
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
            Mapper.Initialize(t => t.CreateMap<T, K>());
            return Mapper.Map<List<K>>(Obj);
        }
        /// <summary>
        /// 集合映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IList<T> MapToList<T>(this IEnumerable<T> Obj)
        {
            Mapper.Initialize(t => t.CreateMap(Obj.GetType(), typeof(T)));
            return Mapper.Map<List<T>>(Obj);
        }
    }
}
