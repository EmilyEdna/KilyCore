using System;
using System.Collections.Generic;
using System.Text;

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
}
