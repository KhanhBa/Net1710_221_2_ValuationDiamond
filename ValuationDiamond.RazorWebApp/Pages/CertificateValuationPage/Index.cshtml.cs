using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace ValuationDiamond.RazorWebApp.Pages.CertificateValuationPage
{
    public class IndexModel : PageModel
    {
        private readonly IValuationCertificateBusiness _valuationCertificateBusiness;

        public IndexModel(IValuationCertificateBusiness valuationCertificateBusiness)
        {
            _valuationCertificateBusiness = valuationCertificateBusiness;
        }

        public IPagedList<ValuationCertificate> ValuationCertificates { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? PageNumber { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Status { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CustomerName { get; set; }
        [BindProperty(SupportsGet = true)]
        public double Price { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _valuationCertificateBusiness.GetAll();

            IEnumerable<ValuationCertificate> valuationCertificates = new List<ValuationCertificate>();

            if (result != null && result.Data != null)
            {
                valuationCertificates = result.Data as List<ValuationCertificate>;

                if (!string.IsNullOrEmpty(CustomerName) && string.IsNullOrEmpty(Status) && Price == 0)
                {
                    valuationCertificates = valuationCertificates.Where(b => b.CustomerName != null && b.CustomerName.Contains(CustomerName)).ToList();
                }
                else if (!string.IsNullOrEmpty(Status) && string.IsNullOrEmpty(CustomerName) && Price == 0)
                {
                    valuationCertificates = valuationCertificates.Where(b => b.Status != null && b.Status.Contains(Status)).ToList();
                }
                else if (Price != 0 && string.IsNullOrEmpty(Status) && string.IsNullOrEmpty(CustomerName))
                {
                    valuationCertificates = valuationCertificates.Where(b => b.Price == Price).ToList();
                }
                else if (!string.IsNullOrEmpty(CustomerName) && !string.IsNullOrEmpty(Status) && Price == 0)
                {
                    valuationCertificates = valuationCertificates.Where(b => b.CustomerName != null && b.CustomerName.Contains(CustomerName) && b.Status != null && b.Status.Contains(Status)).ToList();
                }
                else if (!string.IsNullOrEmpty(CustomerName) && Price != 0 && string.IsNullOrEmpty(Status))
                {
                    valuationCertificates = valuationCertificates.Where(b => b.CustomerName != null && b.CustomerName.Contains(CustomerName) && b.Price == Price).ToList();
                }
                else if (!string.IsNullOrEmpty(Status) && Price != 0 && string.IsNullOrEmpty(CustomerName))
                {
                    valuationCertificates = valuationCertificates.Where(b => b.Status != null && b.Status.Contains(Status) && b.Price == Price).ToList();
                }
                else if (!string.IsNullOrEmpty(Status) && Price != 0 && !string.IsNullOrEmpty(CustomerName))
                {
                    valuationCertificates = valuationCertificates.Where(b => b.Status != null && b.Status.Contains(Status) && b.Price == Price && b.CustomerName != null && b.CustomerName.Contains(CustomerName)).ToList();
                }
            }

            var pageNumber = PageNumber ?? 1;
            var pageSize = 3;

            ValuationCertificates = valuationCertificates.ToPagedList(pageNumber, pageSize);

            return Page();
        }
    }
}
