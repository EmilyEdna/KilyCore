using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.DataEntity.ResponseMapper.Repast
{
    public class ResponseRepastRoleAuthor
    {
        public Guid Id { get; set; }
        public Guid? RepastRoleId { get; set; }
        public string MerchantName { get; set; }
        public string MerchantRoleName { get; set; }
        public string MerchantTypeName { get; set; }
        public string AuthorMenuPath { get; set; }
        public string AuthorMenuCount
        {
            get
            {
                if (!string.IsNullOrEmpty(AuthorMenuPath))
                    return AuthorMenuPath.Split(',').Length.ToString();
                else
                    return null;
            }
        }
    }
    public class ResponseRoleAuthorWeb {
        public string AuthorName { get; set; }
        public Guid Id { get; set; }
        public string AuthorMenuPath { get; set; }
        public string AuthorMenuCount
        {
            get
            {
                if (!string.IsNullOrEmpty(AuthorMenuPath))
                    return AuthorMenuPath.Split(',').Length.ToString();
                else
                    return null;
            }
        }
    }
}
