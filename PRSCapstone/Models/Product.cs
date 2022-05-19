using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSCapstone.Models {
    [Index(nameof(PartNbr), IsUnique = true)]
    public class Product {
        public int Id { get; set; }
        [StringLength(30)]
        public string PartNbr { get; set; } = null!;
        [StringLength(30)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; } = 0;
        [StringLength(30)]
        public string Unit { get; set; } = null!;
        [StringLength(255)]
        public string? PhotoPath { get; set; }
        public int VendorId { get; set; }
        public virtual Vendor? Vendor { get; set; }
    }
}
