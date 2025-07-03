namespace Libary_Management_System.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }

        // Foreign key to BookCategory
        public int CategoryID { get; set; }
        public BookCategory Category { get; set; }
    }
}
