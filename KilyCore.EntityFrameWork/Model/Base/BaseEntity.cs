using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KilyCore.EntityFrameWork.Model.Base
{
    /// <summary>
    /// 实体公共字段
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        //[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CreateTime", TypeName = "datetime")]
        public virtual DateTime? CreateTime { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [Column("DeleteTime", TypeName = "datetime")]
        public virtual DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("UpdateTime", TypeName = "datetime")]
        public virtual DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [MaxLength(50)]
        public virtual string CreateUser { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        [MaxLength(50)]
        public virtual string UpdateUser { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        [MaxLength(50)]
        public virtual string DeleteUser { get; set; }
        /// <summary>
        /// 是否软删除：false表示未删除，true表示软删除
        /// </summary>
        [Column("IsDelete")]
        public virtual bool? IsDelete { get; set; }
    }
}
