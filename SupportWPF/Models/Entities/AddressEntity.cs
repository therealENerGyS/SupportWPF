using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportWPF.Models.Entities
{
    internal class AddressEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string StreetName { get; set; } = null!;
        [Column(TypeName = "varchar(5)")]
        public string? StreetNumber { get; set; }
        [Required]
        [Column(TypeName = "char(6)")]
        public string PostalCode { get; set; } = null!;
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string City { get; set; } = null!;

        public virtual ICollection<CustomerEntity> Customers { get; } = new List<CustomerEntity>();
    }
}
