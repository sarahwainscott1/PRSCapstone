using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PRSCapstone.Models {
    [Index(nameof(Username), IsUnique = true)]
    public class User {
        public int Id { get; set; }
        [StringLength(30)]
        public string Username { get; set; } = null!;
        [StringLength(30)]
        public string Password { get; set; } = null!;
        [StringLength(30)]
        public string Firstname { get; set; } = null!;
        [StringLength(30)]
        public string Lastname { get; set; } = null!;
        [StringLength(12)]
        public string? Phone { get; set; }
        [StringLength(255)]
        public string? Email { get; set; }
        public bool IsReviewer { get; set; }
        public bool IsAdmin { get; set; }

    }
}
