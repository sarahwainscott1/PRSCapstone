using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSCapstone.Models {
    public class Request {
        public int Id { get; set; }
        [StringLength(80)]
        public string Description { get; set; } = null!;
        [StringLength(80)]
        public string Justification { get; set; } = null!;
        [StringLength(80)]
        public string? RejectionReason { get; set; }
        [StringLength(20)]
        public string DeliveryMode { get; set; } = "Pickup";
        [StringLength(10)]
        public string Status { get; set; } = "NEW";
        [Column(TypeName = "decimal(11,2)")]
        public decimal Total { get;  set; } = 0;
        public int UserId { get; set; } = 0;
        public virtual User? User { get; set; }
        public virtual IEnumerable<RequestLine>? Requestlines { get; set; }

    }
}
