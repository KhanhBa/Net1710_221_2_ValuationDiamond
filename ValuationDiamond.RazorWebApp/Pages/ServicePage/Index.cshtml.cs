using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.ServicePage
{
    public class IndexModel : PageModel
    {
        private readonly IServiceBusiness serviceBusiness;

        public IndexModel()
        {
            serviceBusiness ??= new ServiceBusiness();
        }

        public IList<Service> Service { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchField { get; set; }

        public async Task OnGetAsync()
        {
            if (!String.IsNullOrEmpty(SearchTerm) && !String.IsNullOrEmpty(SearchField))
            {
                var result = await serviceBusiness.SearchServices(SearchField, SearchTerm);
                if (result != null && result.Status > 0 && result.Data != null)
                {
                    Service = result.Data as List<Service>;
                }
            }
            else
            {
                var result = await serviceBusiness.GetAllService();
                if (result != null && result.Status > 0 && result.Data != null)
                {
                    Service = result.Data as List<Service>;
                }
            }
        }
    }
}
