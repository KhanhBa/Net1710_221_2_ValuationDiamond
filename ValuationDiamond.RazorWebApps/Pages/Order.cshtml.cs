using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages
{
    public class OrderModel : PageModel
    {
        private readonly IOrderBusiness _OrderBusiness = new OrderBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Order Order { get; set; } = default;
        public List<Order> Orders { get; set; } = new List<Order>();

        public void OnGet()
        {
            Orders = this.GetOrders();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            this.SaveOrder();
            return RedirectToPage();
        }

        //public async Task<IActionResult> OnEdit()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    await this.EditOrder(this.Order.OrderId);
        //    return RedirectToPage();
        //}

        public async Task<IActionResult> OnPostDelete(int orderId)
        {
            var orderResult = _OrderBusiness.DeleteOrder(orderId);
            if (orderResult != null && orderResult.Result.Status > 0)
            {
                Message = orderResult.Result.Message;
            }
            else
            {
                Message = "Error system";
            }
            return RedirectToPage();
        }

        //public async Task<IActionResult> OnDelete()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    this.DeleteOrder(this.Order.OrderId);
        //    return RedirectToPage();
        //}


        private List<Order> GetOrders()
        {
            var OrderResult = _OrderBusiness.GetAllOrders();

            if (OrderResult.Status > 0 && OrderResult.Result.Data != null)
            {
                var orders = (List<Order>)OrderResult.Result.Data;
                return orders;
            }
            return new List<Order>();
        }

        private void SaveOrder()
        {
            var OrderResult = _OrderBusiness.CreateOrder(this.Order);

            if (OrderResult != null)
            {
                this.Message = OrderResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }

        //public async Task<IActionResult> EditOrder(int orderId)
        //{
        //    var orderResult = await _OrderBusiness.ReadOrder(orderId);
        //    if (orderResult.Status > 0)
        //    {
        //        Order = (Order)orderResult.Data;
        //        return Partial("_OrderEditPartial", this);
        //    }
        //    return NotFound();
        //}

        //private void DeleteOrder(int orderId)
        //{
        //    var OrderResult = _OrderBusiness.DeleteOrder(orderId);

        //    if (OrderResult != null)
        //    {
        //        this.Message = OrderResult.Result.Message;
        //    }
        //    else
        //    {
        //        this.Message = "Error system";
        //    }
        //}
    }
}
