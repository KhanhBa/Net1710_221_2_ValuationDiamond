using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;
using X.PagedList;

namespace ValuationDiamond.RazorWebApp.Pages.CustomerPage
{
    public class ChartModel : PageModel
    {
        private readonly ICustomerBusiness customerBusiness;

        public ChartModel()
        {
            customerBusiness ??= new CustomerBusiness();
        }

        public IPagedList<Customer> Customer { get; set; } = default!;

        public string ChartDataJson { get; set; } = default!;

        public async Task OnGetAsync()
        {
            List<Customer> customers;
            var result = await customerBusiness.GetAllCustomer();

            customers = result?.Data as List<Customer> ?? new List<Customer>();

            var customerStatusCounts = customers.GroupBy(c => c.Status)
                                               .Select(g => new { Status = g.Key, Count = g.Count() })
                                               .ToList();
            ChartDataJson = JsonSerializer.Serialize(customerStatusCounts);
        }
    }
}
