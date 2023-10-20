using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KanjiCue.Data;
using KanjiCue.Models;

namespace KanjiCue.Pages.Vocabs
{
    public class CreateModel : PageModel
    {
        private readonly KanjiCue.Data.ApplicationDbContext _context;

        public CreateModel(KanjiCue.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Vocab Vocab { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Vocab == null || Vocab == null)
            {
                return Page();
            }

            _context.Vocab.Add(Vocab);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
