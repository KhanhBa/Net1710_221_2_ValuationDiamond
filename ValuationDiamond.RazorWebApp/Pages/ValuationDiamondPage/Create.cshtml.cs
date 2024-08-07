﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.ValuationDiamondPage
{
    public class CreateModel : PageModel
    {
        private readonly IValuationDiamondBusiness business;

        public CreateModel()
        {
            business ??= new ValuationDiamondBusiness();
        }

        public IActionResult OnGet()
        {
        //ViewData["OrderDetailId"] = new SelectList(_context.OrderDetails, "OrderDetailId", "DetailCode");
            return Page();
        }

        [BindProperty]
        public ValuateDiamond ValuateDiamond { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid )
            {
                return Page();
            }

           
            await business.Create(ValuateDiamond);

            return RedirectToPage("./Index");
        }
    }
}
