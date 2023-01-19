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
    public class DetailsModel : PageModel
    {
        private readonly Rusu_Denisa_Proiect.Data.ApplicationDbContext _context;

        public DetailsModel(Rusu_Denisa_Proiect.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Bouquet Bouquet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bouquets == null)
            {
                return NotFound();
            }

            var bouquet = await _context.Bouquets.Include(b => b.FlowerBouquets).ThenInclude(fb => fb.Flower).FirstOrDefaultAsync(m => m.Id == id);
            bouquet.SetPrice();
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
    }
}
