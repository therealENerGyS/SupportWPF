using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportWPF.Models.Entities
{
    internal class OrderRowEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Subject { get; set; } = null!;
        public string? Comment { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(11)")]
        public string OrderStatus { get; set; } = null!;
        [Required]
        [Column(TypeName = "varchar(8)")]
        public string Priority { get; set; } = null!;
        public DateTime Deadline { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Guid CustomerId { get; set; }

        public virtual CustomerEntity Customer { get; set; } = null!;
        public virtual ProductEntity Product { get; set; } = null!;
    }
}
