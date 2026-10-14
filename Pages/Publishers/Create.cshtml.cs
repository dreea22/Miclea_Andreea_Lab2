using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Miclea_Andreea_Lab2.Models;

namespace Miclea_Andreea_Lab2.Pages.Publishers
{
    public class CreateModel : PageModel
    {
        private readonly Miclea_Andreea_Lab2Context _context;

        public CreateModel(Miclea_Andreea_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Publisher.Add(Publisher);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}