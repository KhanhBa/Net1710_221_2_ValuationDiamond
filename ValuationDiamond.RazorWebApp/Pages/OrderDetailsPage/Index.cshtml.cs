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

namespace ValuationDiamond.RazorWebApp.Pages.OrderDetailsPage
{
    public class IndexModel : PageModel
    {
        private readonly OrderDetailBusiness _business;
        private readonly IServiceBusiness _serviceBusiness;
        private readonly IOrderBusiness _orderBusiness;

        public IndexModel(OrderDetailBusiness business)
        {
            _business = business;
            _serviceBusiness = new ServiceBusiness();
            _orderBusiness = new OrderBusiness();
        }


        [BindProperty(SupportsGet = true)]
        public string Status { get; set; }

        [BindProperty(SupportsGet = true)]
        public string DetailCode { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Description { get; set; }


        public IList<OrderDetail> OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _business.GetAll();
            IEnumerable<OrderDetail> orderDetails = new List<OrderDetail>();
            if (result != null && result.Data != null)
            {
                orderDetails = result.Data as List<OrderDetail>;
                if (!string.IsNullOrEmpty(Status) && string.IsNullOrEmpty(DetailCode) && string.IsNullOrEmpty(Description))
                {
                    orderDetails = orderDetails.Where(x => x.Status != null && x.Status.Contains(Status)).ToList();
                }
                else if (!string.IsNullOrEmpty(DetailCode) && string.IsNullOrEmpty(Status) && string.IsNullOrEmpty(Description))
                {
                    orderDetails = orderDetails.Where(x => x.DetailCode != null && x.DetailCode.Contains(DetailCode)).ToList();
                }
                else if (!string.IsNullOrEmpty(Description) && string.IsNullOrEmpty(Status) && string.IsNullOrEmpty(DetailCode))
                {
                    orderDetails = orderDetails.Where(x => x.Description != null && x.Description.Contains(Description)).ToList();
                }
                else if (!string.IsNullOrEmpty(Status) && !string.IsNullOrEmpty(DetailCode) && string.IsNullOrEmpty(Description))
                {
                    orderDetails = orderDetails.Where(x => x.Status != null && x.Status.Contains(Status) && x.DetailCode != null && x.DetailCode.Contains(DetailCode)).ToList();
                }
                else if (!string.IsNullOrEmpty(Status) && !string.IsNullOrEmpty(Description) && string.IsNullOrEmpty(DetailCode))
                {
                    orderDetails = orderDetails.Where(x => x.Status != null && x.Status.Contains(Status) && x.Description != null && x.Description.Contains(Description)).ToList();
                }
                else if (!string.IsNullOrEmpty(DetailCode) && !string.IsNullOrEmpty(Description) && string.IsNullOrEmpty(Status))
                {
                    orderDetails = orderDetails.Where(x => x.DetailCode != null && x.DetailCode.Contains(DetailCode) && x.Description != null && x.Description.Contains(Description)).ToList();
                }
                else if (!string.IsNullOrEmpty(DetailCode) && !string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(Status))
                {
                    orderDetails = orderDetails.Where(x => x.DetailCode != null && x.DetailCode.Contains(DetailCode) && x.Description != null && x.Description.Contains(Description) && x.Status != null && x.Status.Contains(Status)).ToList();
                }

            }
            OrderDetail = orderDetails.ToList();
            return Page();
        }

        

    }
}
