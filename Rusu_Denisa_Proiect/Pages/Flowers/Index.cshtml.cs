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
    public class IndexModel : PageModel
    {
        private readonly Rusu_Denisa_Proiect.Data.ApplicationDbContext _context;

        public IndexModel(Rusu_Denisa_Proiect.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Flower> Flower { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Flowers != null)
            {
                Flower = await _context.Flowers.ToListAsync();
            }
        }
    }
}
