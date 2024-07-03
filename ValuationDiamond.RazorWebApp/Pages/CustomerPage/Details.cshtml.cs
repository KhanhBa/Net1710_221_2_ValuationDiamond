using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.CustomerPage
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerBusiness customerBusiness;

        public DetailsModel()
        {
            customerBusiness ??= new CustomerBusiness();
        }

      public Customer Customer { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var customer = await customerBusiness.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            else 
            {
                Customer = customer.Data as Customer;
            }
            return Page();
        }
    }
}
