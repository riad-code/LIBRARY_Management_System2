using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libary_Management_System.Models
{
    [Table("Books")] // 👈 This makes sure the table name will be Books
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        public string Title { get; set; }

        public int CategoryID { get; set; }

        public BookCategory Category { get; set; }
    }
}
