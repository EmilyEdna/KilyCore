using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseRoleAuthor
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
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
}
