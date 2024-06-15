using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.ValuationDiamondPage
{
    public class CreateModel : PageModel
    {
        private readonly ValuationDiamond.Data.Models.Net1710_221_2_ValuationDiamondContext _context;

        public CreateModel(ValuationDiamond.Data.Models.Net1710_221_2_ValuationDiamondContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["OrderDetailId"] = new SelectList(_context.OrderDetails, "OrderDetailId", "DetailCode");
            return Page();
        }

        [BindProperty]
        public ValuateDiamond ValuateDiamond { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ValuateDiamonds == null || ValuateDiamond == null)
            {
                return Page();
            }

            _context.ValuateDiamonds.Add(ValuateDiamond);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
