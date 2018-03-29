using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Enterprise
{
    public class RequestEnterpriseRoleAuthor
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public Guid CompanyId { get; set; }
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
