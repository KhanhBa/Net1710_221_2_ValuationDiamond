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
    public class DetailsModel : PageModel
    {
        private readonly IValuationCertificateBusiness _valuationCertificateBusiness;
        public DetailsModel()
        {
            _valuationCertificateBusiness ??= new ValuationCertificateBusiness();
        }

        [BindProperty]
        public ValuationCertificate ValuationCertificate { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || _valuationCertificateBusiness == null)
            {
                return NotFound();
            }

            var valuationcertificate = await _valuationCertificateBusiness.GetbyId(id);
            if (valuationcertificate == null)
            {
                return NotFound();
            }
            else 
            {
                ValuationCertificate = valuationcertificate.Data as ValuationCertificate;
            }
            return Page();
        }
    }
}
