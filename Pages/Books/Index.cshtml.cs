using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // filtrarea dupa autor (de la lab 2)
        [BindProperty(SupportsGet = true)]
        public int? AuthorID { get; set; }
        public SelectList AuthorList { get; set; } = default!;

        public async Task OnGetAsync(int? id, int? categoryID)
        {
            BookD = new BookData();
            AuthorList = new SelectList(await _context.Author.ToListAsync(), "ID", "FullName");

            var books = _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                    .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .AsQueryable();

            if (AuthorID != null)
            {
                books = books.Where(b => b.AuthorID == AuthorID);
            }

            BookD.Books = await books.ToListAsync();
            Book = BookD.Books.ToList();

            if (id != null)
            {
                BookID = id.Value;
                Book? book = BookD.Books.SingleOrDefault(i => i.ID == id.Value);
                if (book != null)
                {
                    BookD.Categories = book.BookCategories.Select(s => s.Category);
                }
            }
        }
    }
}