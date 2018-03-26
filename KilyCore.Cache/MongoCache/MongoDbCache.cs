﻿using KilyCore.Configure;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KilyCore.Cache.MongoCache
{
    public class MongoDbCache : IMongoDbCache
    {
        private static IMongoDatabase instance;
        public static IMongoDatabase Instance
        {
            get
            {
                if (instance == null)
                    return instance = new MongoClient(Configer.MongoDBConnectionString).GetDatabase(Configer.MongoDBName);
                else
                    return instance;
            }
        }
        /// <summary>
        /// 插入单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Insert<T>(T t)
        {
            Instance.GetCollection<T>(typeof(T).Name).InsertOne(t);
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void InsertMany<T>(IList<T> t)
        {
            Instance.GetCollection<T>(typeof(T).Name).InsertMany(t);
        }
        /// <summary>
        /// 查询单个记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public T Search<T>(Expression<Func<T, bool>> filter)
        {
            return Instance.GetCollection<T>(typeof(T).GetType().Name).Find(filter).FirstOrDefault();
        }
        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IList<T> SearchMany<T>(Expression<Func<T, bool>> filter)
        {
            return Instance.GetCollection<T>(typeof(T).GetType().Name).Find(filter).ToList();
        }
        /// <summary>
        /// 更新单个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Update<T>(Expression<Func<T, bool>> filter, string name, string param)
        {
            return (int)Instance.GetCollection<T>(typeof(T).GetType().Name).UpdateOne(filter, Builders<T>.Update.Set(name, param)).ModifiedCount;
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public int UpdateMany<T>(Expression<Func<T, bool>> filter, T t)
        {
            return (int)Instance.GetCollection<T>(typeof(T).GetType().Name).UpdateMany(filter, Builders<T>.Update.Combine(new List<UpdateDefinition<T>>())).ModifiedCount;
        }
        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int Delete<T>(Expression<Func<T, bool>> filter)
        {
            return (int)Instance.GetCollection<T>(typeof(T).Name).DeleteOne(filter).DeletedCount;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IMongoQueryable<T> GetPage<T>(int PageIndex, int PageSize)
        {
            return Instance.GetCollection<T>(typeof(T).Name).AsQueryable().Skip((PageIndex - 1) * PageSize).Take(PageSize);
        }
    }
}
