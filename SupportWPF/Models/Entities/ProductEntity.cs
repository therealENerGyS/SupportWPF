using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SupportWPF.Models.Entities
{
    internal class ProductEntity
    {
        [Key]
        public Guid ArticleNumber { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string ProductName { get; set; } = null!;

        public virtual ICollection<OrderRowEntity> OrderRows { get; } = new List<OrderRowEntity>();
    }
}
