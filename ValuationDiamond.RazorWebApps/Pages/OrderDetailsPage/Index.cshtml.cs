using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.OrderDetailsPage
{
    public class IndexModel : PageModel
    {
        private readonly OrderDetailBusiness _business;

        public IndexModel(OrderDetailBusiness business)
        {
            _business = business;
        }

        public IList<OrderDetail> OrderDetail { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_business != null)
            {
                
            }   
        }
    }
}
