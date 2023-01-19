using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rusu_Denisa_Proiect.Data;
using Rusu_Denisa_Proiect.Models;

namespace Rusu_Denisa_Proiect.Pages.Flowers
{
    public class DeleteModel : PageModel
    {
        private readonly Rusu_Denisa_Proiect.Data.ApplicationDbContext _context;

        public DeleteModel(Rusu_Denisa_Proiect.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Flower Flower { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Flowers == null)
            {
                return NotFound();
            }

            var flower = await _context.Flowers.FirstOrDefaultAsync(m => m.Id == id);

            if (flower == null)
            {
                return NotFound();
            }
            else 
            {
                Flower = flower;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Flowers == null)
            {
                return NotFound();
            }
            var flower = await _context.Flowers.FindAsync(id);

            if (flower != null)
            {
                Flower = flower;
                _context.Flowers.Remove(Flower);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
