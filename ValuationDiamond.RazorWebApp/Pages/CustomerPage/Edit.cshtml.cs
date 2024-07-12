using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.CustomerPage
{
    public class EditModel : PageModel
    {
        private readonly ICustomerBusiness customerBusiness;

        public EditModel()
        {
            customerBusiness ??= new CustomerBusiness();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        [BindProperty]
        public IFormFile AvatarFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await customerBusiness.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            Customer = customer.Data as Customer;
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

            if (AvatarFile != null)
            {
                var fileName = Path.GetFileName(AvatarFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)); // Ensure the directory exists

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await AvatarFile.CopyToAsync(stream);
                }

                Customer.Avatar = "/uploads/" + fileName; // Save the file path as a string
            }

            try
            {
                await customerBusiness.UpdateCustomer(Customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Uncomment this if you have implemented the CustomerExists method
                // if (!CustomerExists(Customer.CustomerId))
                // {
                //     return NotFound();
                // }
                // else
                // {
                //     throw;
                // }
                throw;
            }

            return RedirectToPage("./Index");
        }

        // Uncomment and implement this if necessary
        // private bool CustomerExists(int id)
        // {
        //   return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        // }
    }
}
