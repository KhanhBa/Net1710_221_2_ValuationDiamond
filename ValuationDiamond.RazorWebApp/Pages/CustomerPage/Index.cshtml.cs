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
    public class IndexModel : PageModel
    {
        private readonly ICustomerBusiness customerBusiness;

        public IndexModel()
        {
            customerBusiness ??= new CustomerBusiness();
        }

        public IList<Customer> Customer { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchField { get; set; }

        public async Task OnGetAsync()
        {
            if (!String.IsNullOrEmpty(SearchTerm) && !String.IsNullOrEmpty(SearchField))
            {
                var result = await customerBusiness.SearchCustomers(SearchField, SearchTerm);
                if (result != null && result.Status > 0 && result.Data != null)
                {
                    Customer = result.Data as List<Customer>;
                }
            }
            else
            {
                var result = await customerBusiness.GetAllCustomer();
                if (result != null && result.Status > 0 && result.Data != null)
                {
                    Customer = result.Data as List<Customer>;
                }
            }
        }
    }
}
