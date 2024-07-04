using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages
{
    public class OrderDetailModel : PageModel
    {
        private readonly IOrderDetailBusiness _orderDetailBusiness = new OrderDetailBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default;
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public void OnGet()
        {
            OrderDetails = this.GetCurrencies();
        }

        public void OnPost()
        {
            this.SaveCurrency();
        }

        public void OnDelete()
        {
        }


        private List<OrderDetail> GetCurrencies()
        {
            var orderDetailResult = _orderDetailBusiness.GetAll();

            if (orderDetailResult.Status > 0 && orderDetailResult.Result.Data != null)
            {
                var currencies = (List<OrderDetail>)orderDetailResult.Result.Data;
                return currencies;
            }
            return new List<OrderDetail>();
        }

        private void SaveCurrency()
        {
            var orderDetailResult = _orderDetailBusiness.Save(this.OrderDetail);

            if (orderDetailResult != null)
            {
                this.Message = orderDetailResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }

        //private readonly OrderDetailRepository _orderRepository;
        //public OrderDetailModel()
        //{
        //    _orderRepository = new OrderDetailRepository();
        //}

        //public List<OrderDetail> orderDetails { get; set; }

        //public void OnGet()
        //{
        //    orderDetails = _orderRepository.GetAll().ToList();
        //}
    }
}
