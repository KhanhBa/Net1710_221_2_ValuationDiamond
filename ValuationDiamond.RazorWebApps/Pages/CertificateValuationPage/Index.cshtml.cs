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
    public class IndexModel : PageModel
    {
        private readonly IValuationCertificateBusiness _valuationCertificateBusiness;

        public IndexModel()
        {
           _valuationCertificateBusiness ??= new ValuationCertificateBusiness();
        }

        public IList<ValuationCertificate> ValuationCertificate { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await _valuationCertificateBusiness.GetAll();
            if (_valuationCertificateBusiness != null)
            {
                ValuationCertificate = result.Data as List<ValuationCertificate>;
            }
        }
    }
}
