using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseRoleAuthor
    {
        public Guid Id { get; set; }
        public Guid? EnterpriseRoleId { get; set; }
        public string CompanyName { get; set; }
        public string EnterpriseRoleName { get; set; }
        public string CompanyTypeName { get; set; }
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
