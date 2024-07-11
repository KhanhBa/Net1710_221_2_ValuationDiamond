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
    public class DetailsModel : PageModel
    {
        private readonly IServiceBusiness serviceBusiness;

        public DetailsModel()
        {
            serviceBusiness ??= new ServiceBusiness();
        }

        public Service Service { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await serviceBusiness.GetService(id);
            if (service == null)
            {
                return NotFound();
            }
            else
            {
                Service = service.Data as Service;
            }
            return Page();
        }
    }
}

