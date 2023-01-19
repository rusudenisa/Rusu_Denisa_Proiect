using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rusu_Denisa_Proiect.Data;
using Rusu_Denisa_Proiect.Models;

namespace Rusu_Denisa_Proiect.Pages.Transactions
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly Rusu_Denisa_Proiect.Data.ApplicationDbContext _context;

        public DetailsModel(Rusu_Denisa_Proiect.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Transaction Transaction { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.Include(t=>t.TransactionItems).ThenInclude(ti=>ti.Flower).Include(t=>t.TransactionItems).ThenInclude(ti=>ti.Bouquet)
                .ThenInclude(b=>b.FlowerBouquets).ThenInclude(fb=>fb.Flower)
                .FirstOrDefaultAsync(m => m.Id == id);
            transaction.SetTotalPrice();
            if (transaction == null)
            {
                return NotFound();
            }
            else 
            {
                Transaction = transaction;
            }
            return Page();
        }
    }
}
