using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.OrderDetailsPage
{
    public class CreateModel : PageModel
    {
        private readonly IOrderDetailBusiness _business;

        public CreateModel()
        {
            _business ??= new OrderDetailBusiness();
        }

        public IActionResult OnGet()
        {
        //ViewData["OrderId"] = new SelectList(, "OrderId", "OrderCode");
        //ViewData["ServiceId"] = new SelectList(, "ServiceId", "ServiceId");
            return Page();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || (await _business.GetAll()) == null || OrderDetail == null)
            {
                return Page();
            }

            _business.Save(OrderDetail);
            
            return RedirectToPage("./Index");
        }
    }
}
