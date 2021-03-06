﻿using System;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.Extension.AttributeExtension
{
    public class AttrExtension
    {
        /// <summary>
        /// 获取DescriptAttribute Dictionary方式
        /// </summary>
        /// <typeparam name="DEntity">DTO</typeparam>
        /// <typeparam name="T">例如 DescriptionAttribute</typeparam>
        /// <param name="source">typeof(枚举)</param>
        /// <returns></returns>
        public static IDictionary<object, DEntity> GetDicDescription<DEntity, T>(Type source) where DEntity : class, new() where T : Attribute
        {
            IDictionary<object, DEntity> dic = new Dictionary<object, DEntity>();
            FieldInfo[] fieldInfo = source.GetFields();
            for (int i = 0; i < fieldInfo.Length; i++)
            {
                if (fieldInfo[i].FieldType != typeof(int))
                {
                    DEntity entity = new DEntity();
                    dynamic attr = (T)fieldInfo[i].GetCustomAttribute(typeof(T), false);
                    var value = Convert.ToInt32(Enum.Parse(source, fieldInfo[i].Name));
                    entity.GetType().GetProperty("Name").SetValue(entity, attr.Description);
                    entity.GetType().GetProperty("Value").SetValue(entity, value);
                    dic.Add("Code", entity);
                }
            }
            return dic;
        }

        /// <summary>
        /// 获取DescriptAttribute List方式
        /// </summary>
        /// <typeparam name="DEntity">DTO</typeparam>
        /// <typeparam name="T">例如 DescriptionAttribute</typeparam>
        /// <param name="source">typeof(枚举)</param>
        /// <returns></returns>
        public static IList<DEntity> GetListDescription<DEntity, T>(Type source) where DEntity : class, new() where T : Attribute
        {
            IList<DEntity> dic = new List<DEntity>();
            FieldInfo[] fieldInfo = source.GetFields();
            for (int i = 0; i < fieldInfo.Length; i++)
            {
                if (fieldInfo[i].FieldType != typeof(int))
                {
                    DEntity entity = new DEntity();
                    dynamic attr = (T)fieldInfo[i].GetCustomAttribute(typeof(T), false);
                    var value = Convert.ToInt32(Enum.Parse(source, fieldInfo[i].Name));
                    entity.GetType().GetProperty("Name").SetValue(entity, attr.Description);
                    entity.GetType().GetProperty("Value").SetValue(entity, value);
                    dic.Add(entity);
                }
            }
            return dic;
        }

        /// <summary>
        /// 获取单个DescriptAttribute
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <typeparam name="T">DescriptAttribute</typeparam>
        /// <param name="obj">枚举的值</param>
        /// <returns></returns>
        public static string GetSingleDescription<TEnum, T>(Object obj) where T : Attribute
        {
            if (Convert.ToInt32(obj) == 0)
                return "";
            FieldInfo field = typeof(TEnum).GetField(Enum.GetName(typeof(TEnum), obj));
            dynamic attr = (T)field.GetCustomAttribute(typeof(T), false);
            return attr.Description.ToString();
        }

        /// <summary>
        /// 获取单个DescriptAttribute
        /// </summary>
        /// <typeparam name="T">DescriptAttribute</typeparam>
        /// <param name="Enum">单个枚举</param>
        /// <returns></returns>
        public static string GetSingleDescription<T>(Enum Enum) where T : Attribute
        {
            if (Convert.ToInt32(Enum) == 0)
                return "";
            FieldInfo field = Enum.GetType().GetField(Enum.ToString());
            dynamic attr = (T)field.GetCustomAttribute(typeof(T), false);
            return attr.Description.ToString();
        }

        /// <summary>
        /// 获取某个属性的DescriptAttribute
        /// </summary>
        /// <typeparam name="T">DescriptAttribute</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="FieldName">字段名称</param>
        /// <returns></returns>
        public static string GetSingleDescription<T>(Object entity, String FieldName) where T : Attribute
        {
            dynamic attr = entity.GetType().GetProperty(FieldName).GetCustomAttribute(typeof(T), false);
            return attr.Description.ToString();
        }
    }
}