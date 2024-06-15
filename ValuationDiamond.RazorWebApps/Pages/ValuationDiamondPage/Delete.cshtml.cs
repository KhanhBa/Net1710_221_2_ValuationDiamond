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
    public class DeleteModel : PageModel
    {
        private readonly ValuationDiamond.Data.Models.Net1710_221_2_ValuationDiamondContext _context;

        public DeleteModel(ValuationDiamond.Data.Models.Net1710_221_2_ValuationDiamondContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ValuateDiamonds == null)
            {
                return NotFound();
            }
            var valuatediamond = await _context.ValuateDiamonds.FindAsync(id);

            if (valuatediamond != null)
            {
                ValuateDiamond = valuatediamond;
                _context.ValuateDiamonds.Remove(ValuateDiamond);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
