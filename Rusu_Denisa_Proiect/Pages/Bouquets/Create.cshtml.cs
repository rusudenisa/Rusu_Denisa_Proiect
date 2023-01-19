using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rusu_Denisa_Proiect.Data;
using Rusu_Denisa_Proiect.Models;

namespace Rusu_Denisa_Proiect.Pages.Bouquets
{
    public class CreateModel : PageModel
    {
        private readonly Rusu_Denisa_Proiect.Data.ApplicationDbContext _context;

        public CreateModel(Rusu_Denisa_Proiect.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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

        public IActionResult OnGet()
        {
            Bouquet = new Bouquet();
            Bouquet.FlowerBouquets = new List<FlowerBouquet>();
            PopulateFlowerBouquets(_context, Bouquet);
            return Page();
        }

        [BindProperty]
        public Bouquet Bouquet { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var listOfFlowers = Bouquet.FlowerBouquets.Where(fb=>fb.Quantity!=0).ToList();
            Bouquet.FlowerBouquets = listOfFlowers;
            var ids = Bouquet.FlowerBouquets.Select(fb => fb.FlowerId).ToList();

            var flowersCost = _context.Flowers.Where(f => ids.Contains(f.Id)).Sum(f => f.Price);
            Bouquet.Price = flowersCost;
            _context.Bouquets.Add(Bouquet);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
