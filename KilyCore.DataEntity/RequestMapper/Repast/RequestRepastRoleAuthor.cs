﻿using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 名 称 ：RequestRepastRoleAuthor
* 类 描 述 ：
* 命名空间 ：KilyCore.DataEntity.RequestMapper.Repast
* 机器名称 ：DESKTOP-QPIVQ28 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：刘泽华
* 创建时间 ：2018/7/16 15:25:01
*******************************************************************
* Copyright @ 刘泽华 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace KilyCore.DataEntity.RequestMapper.Repast
{
    public class RequestRepastRoleAuthor
    {
        public string AuthorName { get; set; }
        public Guid? RepastRoleId { get; set; }
        public Guid Id { get; set; }
        public string MerchantName { get; set; }
        public List<string> AuthorPath { get; set; }
        public string AuthorMenuPath
        {
            get
            {
                if (AuthorPath != null)
                    return string.Join(',', AuthorPath);
                else
                    return null;
            }
        }
    }
    public class RequestRoleAuthorWeb
    {
        public Guid Id { get; set; }
        public string AuthorName { get; set; }
        public List<string> AuthorPath { get; set; }
        public string AuthorMenuPath
        {
            get
            {
                if (AuthorPath != null)
                    return string.Join(',', AuthorPath);
                else
                    return null;
            }
        }
    }
}
