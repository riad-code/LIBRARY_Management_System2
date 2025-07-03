using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libary_Management_System.Models
{
    [Table("Categories")]
    public class BookCategory
    {
        [Key]  // ✅ Explicitly declare this as the primary key
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string? Description { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
