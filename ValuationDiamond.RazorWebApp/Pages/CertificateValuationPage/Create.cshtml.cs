using System;
using System.Collections.Generic;
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
        private readonly IValuationDiamondBusiness _valuateDiamondBusiness;

        public CreateModel(IValuationCertificateBusiness valuationCertificateBusiness, IValuationDiamondBusiness valuateDiamondBusiness)
        {
            _valuationCertificateBusiness = valuationCertificateBusiness;
            _valuateDiamondBusiness = valuateDiamondBusiness;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var valuateDiamonds = (await _valuateDiamondBusiness.GetAll()).Data as List<ValuateDiamond>;
            if (valuateDiamonds != null)
            {
                ViewData["ValuateDiamondId"] = new SelectList(valuateDiamonds, "ValuateDiamondId", "ValuateDiamondId");
                ViewData["ValuateDiamondPrices"] = valuateDiamonds.ToDictionary(v => v.ValuateDiamondId, v => v.Price);
            }

            return Page();
        }

        [BindProperty]
        public ValuationCertificate ValuationCertificate { get; set; } = new ValuationCertificate();

        public async Task<IActionResult> OnPostAsync()
        {
            await _valuationCertificateBusiness.Create(ValuationCertificate);
            return RedirectToPage("./Index");
        }
    }
}
