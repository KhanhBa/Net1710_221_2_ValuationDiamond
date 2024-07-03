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
    public class DetailsModel : PageModel
    {
        private readonly IOrderBusiness orderBusiness;

        public DetailsModel()
        {
            orderBusiness ??= new OrderBusiness();
        }

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
            else 
            {
                Order = order.Data as Order;
            }
            return Page();
        }
    }
}
