using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libary_Management_System.Models
{
    public class BookRequest
    {
        [Key]
        public int RequestID { get; set; }

        [Required]
        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public IdentityUser User { get; set; }


        [Required]
        public int BookID { get; set; }

        [ForeignKey("BookID")]
        public Book Book { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Pending, Approved, Rejected
    }
}
