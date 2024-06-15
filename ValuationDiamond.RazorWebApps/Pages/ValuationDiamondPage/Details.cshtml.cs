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
    public class DetailsModel : PageModel
    {
        private readonly ValuationDiamond.Data.Models.Net1710_221_2_ValuationDiamondContext _context;

        public DetailsModel(ValuationDiamond.Data.Models.Net1710_221_2_ValuationDiamondContext context)
        {
            _context = context;
        }

      public ValuateDiamond ValuateDiamond { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ValuateDiamonds == null)
            {
                return NotFound();
            }

            var valuatediamond = await _context.ValuateDiamonds.FirstOrDefaultAsync(m => m.ValuateDiamondId == id);
            if (valuatediamond == null)
            {
                return NotFound();
            }
            else 
            {
                ValuateDiamond = valuatediamond;
            }
            return Page();
        }
    }
}
