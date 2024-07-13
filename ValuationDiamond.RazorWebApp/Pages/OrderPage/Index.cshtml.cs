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

        public IList<Order> Order { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string OrderCode { get; set; }
        [BindProperty(SupportsGet = true)]
        public string StaffName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Customer { get; set; }
        public Pager Pager { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            var pageSize = 3;
            var result = await orderBusiness.SearchOrders(OrderCode, StaffName, Customer, pageIndex ?? 1, pageSize);
            if (result.Data != null)
            {
                Order = result.Data as List<Order>;
                Pager = new Pager(result.TotalCount, pageIndex ?? 1, pageSize);
            }
            else
            {
                result = await orderBusiness.GetPagedOrders(pageIndex ?? 1, pageSize);
                Order = result.Data as IList<Order>;
                Pager = new Pager(result.TotalCount, pageIndex ?? 1, pageSize);
            }
        }
    }
}

