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
    public class DeleteModel : PageModel
    {
        private readonly IServiceBusiness serviceBusiness;

        public DeleteModel()
        {
            serviceBusiness ??= new ServiceBusiness();
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await serviceBusiness.DeleteService(id);



            return RedirectToPage("./Index");
        }
    }
}
