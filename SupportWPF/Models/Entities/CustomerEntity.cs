using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportWPF.Models.Entities
{
    internal class CustomerEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } = null!;
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; } = null!;
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; } = null!;
        [Required]
        [Column(TypeName = "char(13)")]
        public string PhoneNumber { get; set; } = null!;

        public int AddressId { get; set; }
        [Required]
        public virtual AddressEntity Address { get; set; } = null!;

        public virtual ICollection<OrderRowEntity> OrderRows { get; } = new List<OrderRowEntity>();
    }
}
