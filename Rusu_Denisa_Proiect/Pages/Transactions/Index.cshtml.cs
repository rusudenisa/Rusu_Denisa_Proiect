using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rusu_Denisa_Proiect.Data;
using Rusu_Denisa_Proiect.Models;

namespace Rusu_Denisa_Proiect.Pages.Transactions
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Rusu_Denisa_Proiect.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

		public IndexModel(Rusu_Denisa_Proiect.Data.ApplicationDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public IList<Transaction> Transaction { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Transactions != null)
            {
                var user =await _userManager.FindByNameAsync(User.Identity.Name);
                Transaction = _context.Transactions.Where(t => t.UserId.Equals(user.Id)).ToList();
            }
        }
    }
}
