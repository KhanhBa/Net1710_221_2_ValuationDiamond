using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.ValuationDiamondPage
{
    public class DeleteModel : PageModel
    {
        private readonly IValuationDiamondBusiness business;

        public DeleteModel()
        {
            business ??= new ValuationDiamondBusiness();
        }


        [BindProperty]
      public ValuateDiamond ValuateDiamond { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valuatediamond = await business.GetById(id);

            if (valuatediamond == null)
            {
                return NotFound();
            }
            else 
            {
                ValuateDiamond = valuatediamond.Data as ValuateDiamond;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var valuatediamond = await business.DeleteByID(id);


            return RedirectToPage("./Index");
        }
    }
}
