using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Miclea_Andreea_Lab2.Models;

namespace Miclea_Andreea_Lab2.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly Miclea_Andreea_Lab2Context _context;

        public IndexModel(Miclea_Andreea_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Publisher = await _context.Publisher.ToListAsync();
        }
    }
}