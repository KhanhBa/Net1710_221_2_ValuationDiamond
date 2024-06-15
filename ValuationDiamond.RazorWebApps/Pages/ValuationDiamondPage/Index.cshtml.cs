using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.ValuationDiamondPage
{
    public class IndexModel : PageModel
    {
        private readonly ValuationDiamond.Data.Models.Net1710_221_2_ValuationDiamondContext _context;

        public IndexModel(ValuationDiamond.Data.Models.Net1710_221_2_ValuationDiamondContext context)
        {
            _context = context;
        }

        public IList<ValuateDiamond> ValuateDiamond { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ValuateDiamonds != null)
            {
                ValuateDiamond = await _context.ValuateDiamonds
                .Include(v => v.OrderDetail).ToListAsync();
            }
        }
    }
}
