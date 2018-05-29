using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.DataEntity.ResponseMapper.System
{
    public class ResponseAuthorRole
    {
        public Guid Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorMenuPath { get; set; }
        public string AuthorRoleLv { get; set; }
        public int? AuthorCount
        {
            get
            {
                if (AuthorMenuPath != null)
                    return AuthorMenuPath.Split(',').ToList().Count;
                else return null;
            }
        }
        public string AuthorMenuName { get; set; }
    }
}
