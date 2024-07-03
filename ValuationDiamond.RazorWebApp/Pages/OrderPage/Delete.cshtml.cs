using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.OrderPage
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderBusiness orderBusiness;

        public DeleteModel()
        {
            orderBusiness ??= new OrderBusiness();
        }

        [BindProperty]
      public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await orderBusiness.ReadOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            Order = order.Data as Order;
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await orderBusiness.DeleteOrder(id);

            return RedirectToPage("./Index");
        }
    }
}
