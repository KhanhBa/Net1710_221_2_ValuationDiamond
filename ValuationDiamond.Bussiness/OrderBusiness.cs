using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly Net1710_221_2_ValuationDiamondContext _context;
        private readonly ICusTomerBusiness _customerBusiness;
        public async Task<IValuationDiamondResult> GetAllOrders()
        {
            try
            {
                var result = await _context.Orders.ToListAsync();
                return new ValuationDiamondResult(1, "All Orders", result);
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
                var order = await _context.Orders.FindAsync(orderId);
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

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return new ValuationDiamondResult(1, "Order created successfully", order);
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
                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                {
                    return new ValuationDiamondResult(0, "Order not found");
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
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
                var o = await _context.Orders.FindAsync(order.OrderId);
                if (o == null)
                {
                    return new ValuationDiamondResult(0, "Order not found");
                }

                _context.Entry(o).CurrentValues.SetValues(order);
                await _context.SaveChangesAsync();
                return new ValuationDiamondResult(1, "Order updated successfully", o);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }
    }

   
}
