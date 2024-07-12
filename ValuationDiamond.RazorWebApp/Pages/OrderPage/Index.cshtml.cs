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
        public Pager Pager { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            if (!String.IsNullOrEmpty(SearchTerm) && !String.IsNullOrEmpty(SearchField))
            {
                var pageSize = 3;
                var result = await orderBusiness.SearchOrders(SearchField, SearchTerm, pageIndex ?? 1, pageSize);
                if (/*result != null && result.Status > 0 &&*/ result.Data != null)
                {
                    Order = result.Data as List<Order>;
                    Pager = new Pager(result.TotalCount, pageIndex ?? 1, pageSize);
                }
            }
            else
            {
                //var result = await orderBusiness.GetAllOrders();
                //if (result != null && result.Status > 0 && result.Data != null)
                //{
                //    Order = result.Data as List<Order>;
                //}
                var pageSize = 3; // Số lượng mục trên mỗi trang
                var result = await orderBusiness.GetPagedOrders(pageIndex ?? 1, pageSize);
                Order = result.Data as IList<Order>;

                Pager = new Pager(result.TotalCount, pageIndex ?? 1, pageSize);
            }
        }
    }
}
