using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.CustomerPage
{
    public class ChartModel : PageModel
    {
        private readonly ICustomerBusiness customerBusiness;

        public ChartModel()
        {
            customerBusiness ??= new CustomerBusiness();
        }

        public Customer Customer { get; set; } = default!;

        public string ChartDataJson { get; set; } = default!;

        public int TotalCustomers { get; set; }
        public int ActiveCustomers { get; set; }
        public int IncompleteCustomers { get; set; }

        public async Task OnGetAsync()
        {
            List<Customer> customers;
            var result = await customerBusiness.GetAllCustomer();

            customers = result?.Data as List<Customer> ?? new List<Customer>();

            TotalCustomers = customers.Count;
            ActiveCustomers = customers.Count(c => c.Status == true);
            IncompleteCustomers = customers.Count(c => string.IsNullOrEmpty(c.Name) ||
                                                       string.IsNullOrEmpty(c.Cccd) ||
                                                       string.IsNullOrEmpty(c.Email) ||
                                                       string.IsNullOrEmpty(c.Password) ||
                                                       string.IsNullOrEmpty(c.Address) ||
                                                       string.IsNullOrEmpty(c.Phone) ||
                                                       c.DoB == DateTime.MinValue ||
                                                       string.IsNullOrEmpty(c.Avatar));

            var customerStatusCounts = customers.GroupBy(c => c.Status)
                                                 .Select(g => new { Status = g.Key, Count = g.Count() })
                                                 .ToList();
            ChartDataJson = JsonSerializer.Serialize(customerStatusCounts);
        }
    }
}
