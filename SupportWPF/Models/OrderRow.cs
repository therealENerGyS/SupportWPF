using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportWPF.Models
{
    internal class OrderRow
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Subject { get; set; } = null!;
        public Guid ArticleNumber { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Comment { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public string StreetNumber { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string OrderStatus { get; set; } = null!;
        public string Priority { get; set; } = null!;
        public DateTime Deadline { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";

        public string Address => $"{StreetName} {StreetNumber}, {PostalCode} {City}";
    }
}
