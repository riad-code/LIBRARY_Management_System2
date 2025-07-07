using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libary_Management_System.Models
{
    public class BorrowRecord
    {
        [Key]
        public int BorrowID { get; set; }

        [Required]
        public int BookID { get; set; }

        [ForeignKey("BookID")]
        public Book Book { get; set; }

        [Required]
        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public IdentityUser User { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? FineAmount { get; set; }
    }
}
