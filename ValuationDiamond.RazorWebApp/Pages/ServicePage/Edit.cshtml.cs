using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.ServicePage
{
    public class EditModel : PageModel
    {
        private readonly IServiceBusiness serviceBusiness;

        public EditModel()
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
            Service = service.Data as Service;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            try
            {
                await serviceBusiness.UpdateService(Service);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CustomerExists(Customer.CustomerId))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return RedirectToPage("./Index");
        }

        //private bool CustomerExists(int id)
        //{
        //  return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        //}
    }
}
