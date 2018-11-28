using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseRoleAuthor
    {
        public string EnterpriseRoleName { get; set; }
        public Guid? EnterpriseRoleId { get; set; }
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
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
