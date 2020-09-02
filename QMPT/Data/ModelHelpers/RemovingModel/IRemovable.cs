using QMPT.Data.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMPT.Data.ModelHelpers.RemovingModel
{
    public interface IRemovable
    {
        [ForeignKey(nameof(Remover))]
        public int? RemoverId { get; set; }
        public DateTime? RemovalTime { get; set; }
        public bool IsRemoved { get; set; }

        public User Remover { get; set; }
    }
}
