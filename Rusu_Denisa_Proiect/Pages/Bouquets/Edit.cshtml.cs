using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rusu_Denisa_Proiect.Data;
using Rusu_Denisa_Proiect.Models;

namespace Rusu_Denisa_Proiect.Pages.Bouquets
{
    public class EditModel : PageModel
    {
        private readonly Rusu_Denisa_Proiect.Data.ApplicationDbContext _context;

        public EditModel(Rusu_Denisa_Proiect.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bouquet Bouquet { get; set; } = default!;

        private void PopulateFlowerBouquets(ApplicationDbContext context, Bouquet bouquet)
        {
            var allFlowers = context.Flowers;

            var bouquetFlowers = new Dictionary<int, int>((bouquet.FlowerBouquets.Select(fb => new { fb.FlowerId, fb.Quantity })).ToDictionary(x => x.FlowerId, x => x.Quantity));
            var FlowerBouquetsList = new List<FlowerBouquet>();
            foreach (var flower in allFlowers)
            {
                FlowerBouquetsList.Add(
                    new FlowerBouquet
                    {
                        FlowerId = flower.Id,
                        Flower = flower,
                        Quantity = bouquetFlowers.ContainsKey(flower.Id) ? bouquetFlowers[flower.Id] : 0,
                        Bouquet = bouquet,
                        BouquetId = bouquet.Id
                    }
                    );
            }
            bouquet.FlowerBouquets = FlowerBouquetsList;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bouquets == null)
            {
                return NotFound();
            }

            var bouquet =  await _context.Bouquets.Include(b=>b.FlowerBouquets).ThenInclude(fb=>fb.Flower).FirstOrDefaultAsync(m => m.Id == id);
            if (bouquet == null)
            {
                return NotFound();
            }
            PopulateFlowerBouquets(_context, bouquet);
            Bouquet = bouquet;
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
            foreach(var fb in _context.FlowerBouquets.Where(f=>f.BouquetId==Bouquet.Id))
            {
                _context.FlowerBouquets.Remove(fb);
			}
			var listOfFlowers = Bouquet.FlowerBouquets.Where(fb => fb.Quantity != 0).ToList();
			Bouquet.FlowerBouquets = listOfFlowers;
			_context.Attach(Bouquet).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BouquetExists(Bouquet.Id))
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

        private bool BouquetExists(int id)
        {
          return _context.Bouquets.Any(e => e.Id == id);
        }
    }
}
