using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestAuthorRole
    {
        public Guid Id { get; set; }
        public string AuthorName { get; set; }
        public Guid? AuthorRoleLvId { get; set; }
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
