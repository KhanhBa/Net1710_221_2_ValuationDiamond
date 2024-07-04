using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.ValuationDiamondPage
{
    public class EditModel : PageModel
    {
        private readonly IValuationDiamondBusiness Business;

        public EditModel()
        {
            Business ??= new ValuationDiamondBusiness();
        }


        [BindProperty]
        public ValuateDiamond ValuateDiamond { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valuatediamond =  await Business.GetById(id);
            if (valuatediamond == null)
            {
                return NotFound();
            }
            ValuateDiamond = valuatediamond.Data as ValuateDiamond;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await Business.Update(ValuateDiamond.ValuateDiamondId, ValuateDiamond);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ValuateDiamondExists(ValuateDiamond.ValuateDiamondId))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return RedirectToPage("./Index");
        }

        //private bool ValuateDiamondExists(int id)
        //{
        //  return (_context.ValuateDiamonds?.Any(e => e.ValuateDiamondId == id)).GetValueOrDefault();
        //}
    }
}
