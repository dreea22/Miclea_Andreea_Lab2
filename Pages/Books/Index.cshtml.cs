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

        // autorul ales in dropdown (vine din adresa, ex: /Books?AuthorID=1)
        [BindProperty(SupportsGet = true)]
        public int? AuthorID { get; set; }

        // lista de autori pentru dropdown
        public SelectList AuthorList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            AuthorList = new SelectList(await _context.Author.ToListAsync(), "ID", "FullName");

            var books = _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .AsQueryable();

            // daca s-a ales un autor, pastram doar cartile lui
            if (AuthorID != null)
            {
                books = books.Where(b => b.AuthorID == AuthorID);
            }

            Book = await books.ToListAsync();
        }
    }
}