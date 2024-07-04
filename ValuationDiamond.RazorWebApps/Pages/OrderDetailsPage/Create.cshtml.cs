using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.OrderDetailsPage
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
        ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "Payment");
        ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId");
            return Page();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.OrderDetails == null || OrderDetail == null)
            {
                return Page();
            }

            _context.OrderDetails.Add(OrderDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
