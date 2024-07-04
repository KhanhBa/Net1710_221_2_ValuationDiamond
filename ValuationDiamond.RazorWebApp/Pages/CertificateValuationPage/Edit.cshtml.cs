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

namespace ValuationDiamond.RazorWebApp.Pages.CertificateValuationPage
{
    public class EditModel : PageModel
    {
        private readonly IValuationCertificateBusiness _valuationCertificateBusiness;

        public EditModel()
        {
            _valuationCertificateBusiness ??= new ValuationCertificateBusiness();
        }

        [BindProperty]
        public ValuationCertificate ValuationCertificate { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || (_valuationCertificateBusiness.GetAll().Result.Data) == null)
            {
                return NotFound();
            }

            var valuationcertificate = await _valuationCertificateBusiness.GetbyId(id);
            if (valuationcertificate == null)
            {
                return NotFound();
            }
            ValuationCertificate = valuationcertificate.Data as ValuationCertificate;
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
                
                await _valuationCertificateBusiness.Save(ValuationCertificate);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValuationCertificateExists(ValuationCertificate.ValuationCertificateId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ValuationCertificateExists(int id)
        {
          return false;
        }
    }
}
