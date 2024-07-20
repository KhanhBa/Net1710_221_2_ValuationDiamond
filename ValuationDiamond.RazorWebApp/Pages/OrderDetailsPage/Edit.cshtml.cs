using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;
using ValuationDiamond.Data.Repository;

namespace ValuationDiamond.RazorWebApp.Pages.OrderDetailsPage
{
    public class EditModel : PageModel
    {
        private readonly IOrderDetailBusiness _business;
        private readonly IServiceBusiness _serviceBusiness;
        private readonly IOrderBusiness _orderBusiness;

        public EditModel()
        {
            _business ??= new OrderDetailBusiness();
            _serviceBusiness ??= new ServiceBusiness();
            _orderBusiness ??= new OrderBusiness();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || (_business.GetAll().Result.Data) == null)
            {
                return NotFound();
            }

            ViewData["ServiceId"] = new SelectList(_serviceBusiness.GetAllService().Result.Data as List<Service>, "ServiceId", "ServiceId");
            ViewData["OrderId"] = new SelectList(_orderBusiness.GetAllOrders().Result.Data as List<Order>, "OrderId", "OrderId");

            var orderdetail = await _business.GetById(id);

            if (orderdetail == null)
            {
                return NotFound();
            }
            OrderDetail = orderdetail.Data as OrderDetail;

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

            try
            {
                await _business.Update(OrderDetail);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(OrderDetail.OrderDetailId))
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

        private bool OrderDetailExists(int id)
        {
            return false;
        }
    }
}