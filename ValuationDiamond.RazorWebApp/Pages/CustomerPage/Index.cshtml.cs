using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.CustomerPage
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerBusiness customerBusiness;

        public IndexModel()
        {
            customerBusiness = new CustomerBusiness();
        }

        public List<Customer> Customer { get; set; } = new List<Customer>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; }
        public Pager Pager { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            List<Customer> customers = new List<Customer>();

            if (!String.IsNullOrEmpty(SearchTerm))
            {
                var result = await customerBusiness.SearchCustomers(SearchTerm);
                customers = result?.Data as List<Customer> ?? new List<Customer>();
                Customer = customers;
                Pager = new Pager(customers.Count, pageIndex ?? 1, 5);
            }
            else
            {
                var pageSize = 5;
                var result = await customerBusiness.GetPagedCustomers(pageIndex ?? 1, pageSize);
                Customer = result.Data as List<Customer>;

                Pager = new Pager(result.TotalCount, pageIndex ?? 1, pageSize);
            }
        }
    }
}
