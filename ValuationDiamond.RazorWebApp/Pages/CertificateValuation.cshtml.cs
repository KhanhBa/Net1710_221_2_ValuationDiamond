using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages
{
    public class CertificateValuationModel : PageModel
    {
        private readonly ValuationCertificateBusiness _valuationCertificateBusiness;

        public CertificateValuationModel(ValuationCertificateBusiness valuationCertificateBusiness)
        {
            _valuationCertificateBusiness = valuationCertificateBusiness;
        }

        [BindProperty]
        public List<ValuationCertificate> cers { get; set; }

        [BindProperty]
        public string message { get; set; }

        [BindProperty]
        public ValuationCertificate NewCertificateValuation { get; set; } = new ValuationCertificate();

        public async Task OnGetAsync()
        {
            var result = await _valuationCertificateBusiness.GetAll();
            if (result != null && result.Data != null)
            {
                cers = (List<ValuationCertificate>)result.Data;
            }
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    var result = await _valuationCertificateBusiness.GetAll();
            //    if (result != null && result.Data != null)
            //    {
            //        cers = (List<ValuationCertificate>)result.Data;
            //    }
            //    return Page();
            //}

            if (NewCertificateValuation.ValuationCertificateId == 0)
            {
                // Create new valuation
                var createResult = await _valuationCertificateBusiness.Create(NewCertificateValuation);
                message = createResult.Message;
            }
            else
            {
                // Update existing valuation
                var updateResult = await _valuationCertificateBusiness.Save(NewCertificateValuation);
                message = updateResult.Message;
            }

            // Refresh the list of valuations
            var listResult = await _valuationCertificateBusiness.GetAll();
            if (listResult != null && listResult.Data != null)
            {
                cers = (List<ValuationCertificate>)listResult.Data;
            }

            return Page();
        }
    }
}
