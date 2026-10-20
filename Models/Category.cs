namespace Miclea_Andreea_Lab2.Models
{
    public class Category
    {
        public int ID { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Category")]
        public string CategoryName { get; set; }

        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}