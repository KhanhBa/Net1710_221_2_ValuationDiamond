using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages
{
    public class CertificateValuationModel : PageModel
    {
        readonly ValuationCertificateBusiness _valuationCertificateBusiness;
        public CertificateValuationModel(ValuationCertificateBusiness valuationCertificateBusiness)
        {
            _valuationCertificateBusiness = valuationCertificateBusiness;
        }
        [BindProperty]
        public List<ValuationCertificate> cers { get; set; }
        [BindProperty]
        public string message { get; set; }
        public ValuationCertificate NewCertificateValuation { get; set; } = new ValuationCertificate();
        public async Task OnGetAsync()
        {
            var result = await _valuationCertificateBusiness.GetAll();
            if (result != null && result.Data != null)
            {
                cers = (List<ValuationCertificate>)result.Data;
            }
        }
        public async Task OnPostCreate()
        {
            var result = await _valuationCertificateBusiness.Create(NewCertificateValuation);
            message = result.Message;
        
        }
    }
}
