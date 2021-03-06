﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：ResponseGovtRoleAuthor
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.ResponseMapper.Govt
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：$刘泽华$
* 创建时间 ：2018/9/7 11:33:01
*******************************************************************
* Copyright @ $刘泽华$ 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.ResponseMapper.Govt
{
    public class ResponseGovtRoleAuthor
    {
        public Guid Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorMenuPath { get; set; }
        public int? AuthorCount
        {
            get
            {
                if (AuthorMenuPath != null)
                    return AuthorMenuPath.Split(',').ToList().Count;
                else return null;
            }
        }
    }
    public class ResponseGovtDistribut {
        public string Id { set; get; }
        public string Name { get; set; }
        public string LngAndLat { get; set; }
        public string Address { get; set; }
        public string CompanyType { get; set; }
        public string CompanyImg { set; get; }
        public string CompanyUser { set; get; }
        public string CompanyCode { get; set; }
        public string Lng => !string.IsNullOrEmpty(LngAndLat) ? LngAndLat.Split(",")[0] : null;
        public string Lat => !string.IsNullOrEmpty(LngAndLat) ? LngAndLat.Split(",")[1] : null;
    }
}
