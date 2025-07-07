using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libary_Management_System.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(150)]
        public string? Author { get; set; }

        [Required]
        [MaxLength(20)]
        public string ISBN { get; set; }

        [MaxLength(100)]
        public string? Publisher { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PublishedDate { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public BookCategory? Category { get; set; }

        public string? CoverImagePath { get; set; }

        [Required]
        [Range(1, 1000)]
        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }
    }
}
