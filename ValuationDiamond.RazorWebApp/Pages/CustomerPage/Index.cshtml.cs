using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalPages { get; set; }

        public async Task OnGetAsync()
        {
            List<Customer> customers;

            if (!String.IsNullOrEmpty(SearchTerm))
            {
                var result = await customerBusiness.SearchCustomers(SearchTerm);
                customers = result?.Data as List<Customer> ?? new List<Customer>();
            }
            else
            {
                var result = await customerBusiness.GetAllCustomer();
                customers = result?.Data as List<Customer> ?? new List<Customer>();
            }

            // Calculate the total number of pages
            TotalPages = (int)Math.Ceiling(customers.Count / (double)PageSize);

            // Get the customers for the current page
            Customer = customers
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}
