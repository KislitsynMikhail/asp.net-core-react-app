using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMPT.Data.Models
{
    public abstract class BaseModel
    {
        public virtual int Id { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        [ForeignKey(nameof(Creator))]
        public int? CreatorId { get; set; }
        public User Creator { get; set; }

        protected BaseModel(int? creatorId = null)
        {
            CreatorId = creatorId;
        }
    }
}
