using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Data.Models;
using ValuationDiamond.Business;

namespace ValuationDiamond.RazorWebApp.Pages.ValuationDiamondPage
{
    public class IndexModel : PageModel
    {
        private readonly IValuationDiamondBusiness _business;

        public IndexModel()
        {
            _business ??= new ValuationDiamondBusiness();
        }

        public IList<ValuateDiamond> ValuateDiamond { get; set; } = new List<ValuateDiamond>();

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 3;

        public async Task OnGetAsync()
        {
            IValuationDiamondResult result = await _business.GetAll();

            if (result != null && result.Status > 0 && result.Data != null)
            {
                var allDiamonds = result.Data as List<ValuateDiamond>;

                if (!string.IsNullOrEmpty(SearchString))
                {
                    allDiamonds = allDiamonds.Where(v =>
                        (v.ValuationStaffName?.Contains(SearchString) ?? false) ||
                        (v.Color?.Contains(SearchString) ?? false) ||
                        (v.DiamondType?.Contains(SearchString) ?? false) ||
                        (v.Shape?.Contains(SearchString) ?? false)).ToList();
                }

                int totalItems = allDiamonds.Count();
                TotalPages = (int)Math.Ceiling((double)totalItems / PageSize);

                ValuateDiamond = allDiamonds
                    .Skip((CurrentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();
            }
        }
    }
}
