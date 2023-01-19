using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rusu_Denisa_Proiect.Data;
using Rusu_Denisa_Proiect.Models;

namespace Rusu_Denisa_Proiect.Pages.Transactions
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly Rusu_Denisa_Proiect.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

		public CreateModel(Rusu_Denisa_Proiect.Data.ApplicationDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		private void PopulateTransactionItemList(ApplicationDbContext context, Transaction transaction)
        {
            var allFlowers = context.Flowers;
            var allBouquets = context.Bouquets;
            var bouquetsDict = new Dictionary<int, int>((transaction.TransactionItems.Where(ti=>ti.Bouquet!=null).Select(ti => new {ti.BouquetId, ti.Quantity})).ToDictionary(x=>x.BouquetId.Value, x=>x.Quantity));
			var flowersDict = new Dictionary<int, int>((transaction.TransactionItems.Where(ti => ti.Flower != null).Select(ti => new { ti.FlowerId, ti.Quantity })).ToDictionary(x => x.FlowerId.Value, x => x.Quantity));
            var transactionItemList = new List<TransactionItem>();
            foreach(var flower in allFlowers)
            {
                transactionItemList.Add(new TransactionItem
                {
                    FlowerId = flower.Id,
                    Flower = flower,
                    Transaction = transaction,
                    TransactionId = transaction.Id,
                    Quantity = flowersDict.ContainsKey(flower.Id) ? flowersDict[flower.Id] : 0
                });
            }
            foreach(var bouquet in allBouquets)
            {
                transactionItemList.Add(new TransactionItem
                {
                    BouquetId = bouquet.Id,
                    Bouquet = bouquet,
                    TransactionId = transaction.Id,
                    Transaction = transaction,
                    Quantity = bouquetsDict.ContainsKey(bouquet.Id) ? bouquetsDict[bouquet.Id] : 0
                });
            }
            transaction.TransactionItems = transactionItemList;
		}

		public IActionResult OnGet()
        {
            var transaction = new Transaction();
            transaction.TransactionItems = new List<TransactionItem>();
            Transaction = transaction;
            PopulateTransactionItemList(_context, Transaction);
            return Page();
        }

        [BindProperty]
        public Transaction Transaction { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            
			var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
			Transaction.User = user;
			Transaction.UserId = user.Id;
			var listOfItems = Transaction.TransactionItems.Where(ti => ti.Quantity > 0).ToList();
			Transaction.TransactionItems = listOfItems;
			_context.Transactions.Add(Transaction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
