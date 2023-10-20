using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KanjiCue.Data;
using KanjiCue.Models;

namespace KanjiCue.Pages.Vocabs
{
    public class DeleteModel : PageModel
    {
        private readonly KanjiCue.Data.ApplicationDbContext _context;

        public DeleteModel(KanjiCue.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Vocab Vocab { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Vocab == null)
            {
                return NotFound();
            }

            var vocab = await _context.Vocab.FirstOrDefaultAsync(m => m.Id == id);

            if (vocab == null)
            {
                return NotFound();
            }
            else 
            {
                Vocab = vocab;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.Vocab == null)
            {
                return NotFound();
            }
            var vocab = await _context.Vocab.FindAsync(id);

            if (vocab != null)
            {
                Vocab = vocab;
                _context.Vocab.Remove(Vocab);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
