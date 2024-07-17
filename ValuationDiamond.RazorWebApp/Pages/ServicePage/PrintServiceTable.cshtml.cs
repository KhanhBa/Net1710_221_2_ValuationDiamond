using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.ServicePage
{
    public class PrintServiceTableModel : PageModel
    {
        private readonly IServiceBusiness serviceBusiness;

        public PrintServiceTableModel()
        {
            serviceBusiness ??= new ServiceBusiness();
        }

        public IList<Service> Service { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await serviceBusiness.GetAllService();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                Service = result.Data as List<Service>;
            }
        }
    }
}
