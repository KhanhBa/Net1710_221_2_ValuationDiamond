using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuationDiamond.Data.DAO;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.Business
{
    public interface IOrderDetailBusiness
    {
        Task<IValuationDiamondResult> getAllOrderDetail();
        Task<IValuationDiamondResult> ReadOrderDetail(int orderId);
        Task<IValuationDiamondResult> CreateOrderDetail(Order order);
        Task<IValuationDiamondResult> UpdateOrderDetail(Order order);
        Task<IValuationDiamondResult> DeleteOrderDetail(int orderId);
    }

    public class OrderDetailBusiness : IOrderDetailBusiness
    {

        private readonly OrderDetailDAO _DAO;
        private readonly IOrderDetailBusiness _orderDetail;

        public OrderDetailBusiness()
        { 
            _DAO = new OrderDetailDAO();
        }
        public async Task<IValuationDiamondResult> CreateOrderDetail(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<IValuationDiamondResult> DeleteOrderDetail(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<IValuationDiamondResult> getAllOrderDetail()
        {
                var orderDetails = await _DAO.GetAllAsync();
            if (orderDetails == null)
            {
                return new ValuationDiamondResult(1, "No Order Detail");
            }
            else {
                return new ValuationDiamondResult(1, "List Order detail", orderDetails);
            }
           
                
            
        }

        public Task<IValuationDiamondResult> ReadOrderDetail(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IValuationDiamondResult> UpdateOrderDetail(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
