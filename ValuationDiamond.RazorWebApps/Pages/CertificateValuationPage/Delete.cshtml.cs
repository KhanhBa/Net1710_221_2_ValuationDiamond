using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.CertificateValuationPage
{
    public class DeleteModel : PageModel
    {
        private readonly IValuationCertificateBusiness _valuationCertificateBusiness;

        public DeleteModel()
        {
            _valuationCertificateBusiness ??= new ValuationCertificateBusiness();
        }

        [BindProperty]
      public ValuationCertificate ValuationCertificate { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
           

            var va = await _valuationCertificateBusiness.GetbyId(id);

            if (va== null)
            {
                return NotFound();
            }
            else 
            {
                ValuationCertificate = va.Data as ValuationCertificate;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null || _valuationCertificateBusiness == null)
            {
                return NotFound();
            }
            var valuationcertificate = await _valuationCertificateBusiness.GetbyId(id);

            if (valuationcertificate != null)
            {
                ValuationCertificate = valuationcertificate.Data as ValuationCertificate;
               await _valuationCertificateBusiness.DeleteByID(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
