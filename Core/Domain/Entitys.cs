using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain
{
    #region 接口
    public interface IEntity<KeyType>
    {
        [Key]
        KeyType ID { get; set; }
    }

    public interface IIsDelete
    {
        bool IsDelete { get; set; }
    }
    #endregion

    #region 实现
    public class Entity<KeyType> : IEntity<KeyType>
    {
        [Key]
        public KeyType ID { get; set; }
    }

    public class CommonEntity<KeyType> : IEntity<KeyType>
    {
        [Key]
        public KeyType ID { get; set; }
        public DateTime CreatDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public KeyType CreatUserID { get; set; }
        public KeyType UpdateUserID { get; set; }
    }
    #endregion
}
