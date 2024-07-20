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
        private readonly IServiceBusiness _serviceBusiness;
        private readonly IOrderBusiness _orderBusiness;

        public CreateModel()
        {
            _business ??= new OrderDetailBusiness();
            _serviceBusiness = new ServiceBusiness();
            _orderBusiness = new OrderBusiness();
        }

        public IActionResult OnGet()
        {
            ViewData["ServiceId"] = new SelectList(_serviceBusiness.GetAllService().Result.Data as List<Service>, "ServiceId", "ServiceId");
            ViewData["OrderId"] = new SelectList(_orderBusiness.GetAllOrders().Result.Data as List<Order>, "OrderId", "OrderId");
            //ViewData["OrderId"] = new SelectList(, "OrderId", "OrderCode");
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