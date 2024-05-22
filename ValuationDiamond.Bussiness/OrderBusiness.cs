using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ValuationDiamond.Data.DAO;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.Business
{
    public interface IOrderBusiness
    {
        Task<IValuationDiamondResult> GetAllOrders();
        Task<IValuationDiamondResult> ReadOrder(int orderId);
        Task<IValuationDiamondResult> CreateOrder(Order order);
        Task<IValuationDiamondResult> UpdateOrder(Order order);
        Task<IValuationDiamondResult> DeleteOrder(int orderId);
    }

    public class OrderBusiness : IOrderBusiness
    {
        //private readonly Net1710_221_2_ValuationDiamondContext _context;
        private readonly ICustomerBusiness _customerBusiness;

        private readonly OrderDAO _DAO;
        public OrderBusiness()
        {
            _DAO = new OrderDAO();
        }

        public async Task<IValuationDiamondResult> GetAllOrders()
        {
            try
            {
                var orders = await _DAO.GetAllAsync();
                if (orders == null)
                {
                    return new ValuationDiamondResult(0, "No orders found");
                }
                else return new ValuationDiamondResult(1, "All Orders", orders);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> ReadOrder(int orderId)
        {
            try
            {
                //var order = await _DAO.Orders.FindAsync(orderId);
                var order = await _DAO.GetByIdAsync(orderId);
                if (order == null)
                {
                    return new ValuationDiamondResult(0, "Order not found");
                }
                else return new ValuationDiamondResult(1, "View Order", order);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> CreateOrder(Order order)
        {
            try
            {
                if (!await _customerBusiness.CheckCustomerIdExist(order.CustomerId))
                {
                    return new ValuationDiamondResult(0, "Invalid CustomerID");
                }

                int result = await _DAO.CreateAsync(order);
                if (result > 0)
                {
                    return new ValuationDiamondResult(1, "Order created successfully", order);
                }
                else return new ValuationDiamondResult(0, "Create fail!");
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> DeleteOrder(int orderId)
        {
            try
            {
                var order = await _DAO.GetByIdAsync(orderId);
                if (order == null)
                {
                    return new ValuationDiamondResult(0, "Order not found");
                }

                await _DAO.RemoveAsync(order);
                return new ValuationDiamondResult(1, "Order removed successfully");
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> UpdateOrder(Order order)
        {
            try
            {
                var exsitingOrder = await _DAO.GetByIdAsync(order.OrderId);
                if (exsitingOrder == null)
                {
                    return new ValuationDiamondResult(0, "Order not found");
                }
                exsitingOrder.OrderId = order.OrderId;
                //_context.Entry(o).CurrentValues.SetValues(order);
                await _DAO.UpdateAsync(exsitingOrder);
                return new ValuationDiamondResult(1, "Order updated successfully", exsitingOrder);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }
    }


}
