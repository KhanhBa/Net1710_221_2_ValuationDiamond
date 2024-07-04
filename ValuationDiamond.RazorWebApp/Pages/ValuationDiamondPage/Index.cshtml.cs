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

        public IList<ValuateDiamond> ValuateDiamond { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }

        public async Task OnGetAsync()
        {
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchString))
            {
                var result = await _business.Search(searchString);
                if (result != null && result.Status > 0 && result.Data != null)
                {
                    ValuateDiamond = result.Data as List<ValuateDiamond>;
                }
            }
            else
            {
                var result = await _business.GetAll();
                if (result != null && result.Status > 0 && result.Data != null)
                {
                    ValuateDiamond = result.Data as List<ValuateDiamond>;
                }
            }


        }
    }
}
