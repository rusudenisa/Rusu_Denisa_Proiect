using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rusu_Denisa_Proiect.Data;
using Rusu_Denisa_Proiect.Models;

namespace Rusu_Denisa_Proiect.Pages.Bouquets
{
    public class DeleteModel : PageModel
    {
        private readonly Rusu_Denisa_Proiect.Data.ApplicationDbContext _context;

        public DeleteModel(Rusu_Denisa_Proiect.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Bouquet Bouquet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bouquets == null)
            {
                return NotFound();
            }

            var bouquet = await _context.Bouquets.FirstOrDefaultAsync(m => m.Id == id);

            if (bouquet == null)
            {
                return NotFound();
            }
            else 
            {
                Bouquet = bouquet;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Bouquets == null)
            {
                return NotFound();
            }
            var bouquet = await _context.Bouquets.FindAsync(id);

            if (bouquet != null)
            {
                Bouquet = bouquet;
                _context.Bouquets.Remove(Bouquet);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
