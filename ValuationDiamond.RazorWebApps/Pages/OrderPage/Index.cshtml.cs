using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages.OrderPage
{
    public class IndexModel : PageModel
    {
        private readonly IOrderBusiness orderBusiness;

        public IndexModel()
        {
            orderBusiness ??= new OrderBusiness();
        }

        public IList<Order> Order { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchField { get; set; }

        //public async Task OnGetAsync()
        //{
        //    if (SearchTerm == null)
        //    {
        //        var result = await orderBusiness.GetAllOrders();
        //        if (result != null && result.Status > 0 && result.Data != null)
        //        {
        //            Order = result.Data as List<Order>;
        //        }
        //    }
        //    else
        //    {
        //        var result = await orderBusiness.SearchOrders(SearchTerm);
        //        if (result != null && result.Status > 0 && result.Data != null)
        //        {
        //            Order = result.Data as List<Order>;
        //        }
        //    }
        //}

        public async Task OnGetAsync()
        {
            if (!String.IsNullOrEmpty(SearchTerm) && !String.IsNullOrEmpty(SearchField))
            {
                var result = await orderBusiness.SearchOrders(SearchField, SearchTerm);
                if (result != null && result.Status > 0 && result.Data != null)
                {
                    Order = result.Data as List<Order>;
                }
            }
            else
            {
                var result = await orderBusiness.GetAllOrders();
                if (result != null && result.Status > 0 && result.Data != null)
                {
                    Order = result.Data as List<Order>;
                }
            }
        }
    }
}
