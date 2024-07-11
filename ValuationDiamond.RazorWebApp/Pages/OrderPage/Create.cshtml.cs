using System.Collections.Generic;
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
        private readonly ICustomerBusiness customerBusiness;

        public CreateModel()
        {
            orderBusiness ??= new OrderBusiness();
            customerBusiness ??= new CustomerBusiness();
        }

        [BindProperty]
        public Order Order { get; set; } = default;

        public SelectList CustomerList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var customers = await customerBusiness.GetAllCustomer();

            CustomerList = new SelectList(customers.Data as List<Customer>, "CustomerId", "Name");

            return Page();
        }

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
