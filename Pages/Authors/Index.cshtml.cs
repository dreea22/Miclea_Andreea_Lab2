using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Miclea_Andreea_Lab2.Models;

namespace Miclea_Andreea_Lab2.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly Miclea_Andreea_Lab2Context _context;

        public IndexModel(Miclea_Andreea_Lab2Context context)
        {
            _context = context;
        }

        public IList<Author> Author { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Author = await _context.Author.ToListAsync();
        }
    }
}