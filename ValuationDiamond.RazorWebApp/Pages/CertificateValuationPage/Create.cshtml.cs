using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.CertificateValuationPage
{
    public class CreateModel : PageModel
    {
        private readonly IValuationCertificateBusiness _valuationCertificateBusiness;

        public CreateModel()
        {
            _valuationCertificateBusiness ??= new ValuationCertificateBusiness();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ValuationCertificate ValuationCertificate { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || (await _valuationCertificateBusiness.GetAll()) == null || ValuationCertificate == null)
            {
                return Page();
            }

           _valuationCertificateBusiness.Create(ValuationCertificate);
            return RedirectToPage("./Index");
        }
    }
}
