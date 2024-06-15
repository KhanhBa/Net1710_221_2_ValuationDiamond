using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.ValuationDiamondPage
{
    public class EditModel : PageModel
    {
        private readonly ValuationDiamond.Data.Models.Net1710_221_2_ValuationDiamondContext _context;

        public EditModel(ValuationDiamond.Data.Models.Net1710_221_2_ValuationDiamondContext context)
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

            var valuatediamond =  await _context.ValuateDiamonds.FirstOrDefaultAsync(m => m.ValuateDiamondId == id);
            if (valuatediamond == null)
            {
                return NotFound();
            }
            ValuateDiamond = valuatediamond;
           ViewData["OrderDetailId"] = new SelectList(_context.OrderDetails, "OrderDetailId", "DetailCode");
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

            _context.Attach(ValuateDiamond).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValuateDiamondExists(ValuateDiamond.ValuateDiamondId))
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

        private bool ValuateDiamondExists(int id)
        {
          return (_context.ValuateDiamonds?.Any(e => e.ValuateDiamondId == id)).GetValueOrDefault();
        }
    }
}
