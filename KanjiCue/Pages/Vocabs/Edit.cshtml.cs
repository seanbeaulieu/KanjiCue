using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KanjiCue.Data;
using KanjiCue.Models;

namespace KanjiCue.Pages.Vocabs
{
    public class EditModel : PageModel
    {
        private readonly KanjiCue.Data.ApplicationDbContext _context;

        public EditModel(KanjiCue.Data.ApplicationDbContext context)
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

            var vocab =  await _context.Vocab.FirstOrDefaultAsync(m => m.Id == id);
            if (vocab == null)
            {
                return NotFound();
            }
            Vocab = vocab;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Vocab).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VocabExists(Vocab.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool VocabExists(string id)
        {
          return (_context.Vocab?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
