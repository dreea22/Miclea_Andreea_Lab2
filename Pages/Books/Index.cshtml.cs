using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Miclea_Andreea_Lab2.Models;

namespace Miclea_Andreea_Lab2.Pages.BookPages
{
    public class IndexModel : PageModel
    {
        private readonly Miclea_Andreea_Lab2Context _context;

        public IndexModel(Miclea_Andreea_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;
        public BookData BookD { get; set; } = default!;
        public int BookID { get; set; }
        public int CategoryID { get; set; }

        // pasul 8: pentru sortare
        public string TitleSort { get; set; } = "";
        public string AuthorSort { get; set; } = "";

        // pasul 11: textul cautat
        public string CurrentFilter { get; set; } = "";

        // pasii 9 si 12: sortOrder si searchString
        public async Task OnGetAsync(int? id, int? categoryID, string? sortOrder, string? searchString)
        {
            BookD = new BookData();

            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";

            CurrentFilter = searchString ?? "";

            BookD.Books = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                    .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            // cautare dupa titlu sau dupa numele autorului
            if (!String.IsNullOrEmpty(searchString))
            {
                BookD.Books = BookD.Books.Where(s =>
                    (s.Author != null &&
                        (s.Author.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        || s.Author.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase)))
                    || s.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            if (id != null)
            {
                BookID = id.Value;
                Book? book = BookD.Books.SingleOrDefault(i => i.ID == id.Value);
                if (book != null)
                {
                    BookD.Categories = book.BookCategories.Select(s => s.Category);
                }
            }

            // sortare
            switch (sortOrder)
            {
                case "title_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Title);
                    break;
                case "author_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Author != null ? s.Author.FullName : "");
                    break;
                case "author":
                    BookD.Books = BookD.Books.OrderBy(s => s.Author != null ? s.Author.FullName : "");
                    break;
                default:
                    BookD.Books = BookD.Books.OrderBy(s => s.Title);
                    break;
            }

            Book = BookD.Books.ToList();
        }
    }
}