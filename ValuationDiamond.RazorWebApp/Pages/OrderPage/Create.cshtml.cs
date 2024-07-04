using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.OrderPage
{
    public class CreateModel : PageModel
    {
        private readonly IOrderBusiness orderBusiness;
        public CreateModel()
        {
            orderBusiness ??= new OrderBusiness();
        }

        public IActionResult OnGet()
        {
        //ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        public List<Customer> Options { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            await orderBusiness.CreateOrder(Order);

            return RedirectToPage("./Index");
        }
    }
}
