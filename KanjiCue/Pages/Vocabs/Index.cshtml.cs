using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KanjiCue.Data;
using KanjiCue.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KanjiCue.Pages.Vocabs
{
    public class IndexModel : PageModel
    {
        private readonly KanjiCue.Data.ApplicationDbContext _context;

        public IndexModel(KanjiCue.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Vocab> Vocab { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? VocabClasses { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? VocabClass { get; set; }

        public async Task OnGetAsync()
        {

            IQueryable<string> classQuery = from m in _context.Vocab
                                            orderby m.Class
                                            select m.Class;

            var vocabs = from m in _context.Vocab
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                vocabs = vocabs.Where(s => s.Term.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(VocabClass))
            {
               vocabs = vocabs.Where(x => x.Class == VocabClass);
            }

            VocabClasses = new SelectList(await classQuery.Distinct().ToListAsync());
            Vocab = await vocabs.ToListAsync();
        }
    }
}
