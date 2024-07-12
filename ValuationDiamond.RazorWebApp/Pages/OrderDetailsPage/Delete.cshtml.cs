using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.OrderDetailsPage
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderDetailBusiness _business;

        public DeleteModel()
        {
            _business ??= new OrderDetailBusiness();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {

            var orderdetail = await _business.GetById(id);

            if (orderdetail == null)
            {
                return NotFound();
            }
            else 
            {
                OrderDetail = orderdetail.Data as OrderDetail;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null || _business == null)
            {
                return NotFound();
            }
            var orderdetail = await _business.GetById(id);

            if (orderdetail != null)
            {
                OrderDetail = orderdetail.Data as OrderDetail;
                await _business.DeleteById(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
