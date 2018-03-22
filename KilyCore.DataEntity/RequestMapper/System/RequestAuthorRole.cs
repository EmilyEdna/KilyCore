using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.System
{
    public class RequestAuthorRole
    {
        public string AuthorName { get; set; }
        public Guid? AuthorRoleLvId { get; set; }
        public List<string> AuthorPath { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Town { get; set; }
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
        public string TypePath
        {
            get
            {
                if (!string.IsNullOrEmpty(Province) || !string.IsNullOrEmpty(City) || !string.IsNullOrEmpty(Area)||!string.IsNullOrEmpty(Town))
                    return Province + "," + City + "," + Area+","+Town;
                else return null;
            }
        }
    }
}
