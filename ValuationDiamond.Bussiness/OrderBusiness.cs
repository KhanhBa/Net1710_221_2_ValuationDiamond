using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ValuationDiamond.Data;
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
        Task<(IEnumerable<Order> Data, int TotalCount)> SearchOrders(string orderCode, string staffName, string customer, int pageIndex, int pageSize);
        Task<(IEnumerable<Order> Data, int TotalCount)> GetPagedOrders(int pageIndex, int pageSize);
    }

    public class OrderBusiness : IOrderBusiness
    {

        //private readonly Net1710_221_2_ValuationDiamondContext _context;
        private readonly ICustomerBusiness _customerBusiness;
        //private readonly OrderDAO _DAO;
        private readonly UnitOfWork _unitOfWork;

        public OrderBusiness()
        {
            //_DAO = new OrderDAO();
            _unitOfWork ??= new UnitOfWork();
            _customerBusiness ??= new CustomerBusiness();
        }

        public async Task<(IEnumerable<Order> Data, int TotalCount)> GetPagedOrders(int pageIndex, int pageSize)
        {
            return await _unitOfWork.OrderRepository.GetPagedOrders(pageIndex, pageSize);
        }


        public async Task<IValuationDiamondResult> GetAllOrders()
        {
            try
            {
                //var orders = await _DAO.GetAllAsync();
                var orders = await _unitOfWork.OrderRepository.GetAllOrders();

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

        public async Task<(IEnumerable<Order> Data, int TotalCount)> SearchOrders(string orderCode, string staffName, string customer, int pageIndex, int pageSize)
        {
            return await _unitOfWork.OrderRepository.GetPagedSearchOrders(orderCode, staffName, customer, pageIndex, pageSize);
        }


        public async Task<IValuationDiamondResult> ReadOrder(int orderId)
        {
            try
            {
                //var order = await _DAO.Orders.FindAsync(orderId);
                //var order = await _DAO.GetByIdAsync(orderId);
                var order = await _unitOfWork.OrderRepository.GetOrderbyId(orderId);

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

                var orders = await _unitOfWork.OrderRepository.GetAllOrders();
                foreach (Order o in orders)
                {
                    if (o.OrderCode == order.OrderCode)
                    {
                        return new ValuationDiamondResult(0, "OrderCode Duplicated");
                    }
                }

                //int result = await _DAO.CreateAsync(order);
                order.Quantity = 0;
                order.TotalAmount = 0;
                _unitOfWork.OrderRepository.PrepareCreate(order);
                int result = await _unitOfWork.OrderRepository.SaveAsync();

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
                //var order = await _DAO.GetByIdAsync(orderId);
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);

                if (order == null)
                {
                    return new ValuationDiamondResult(0, "Order not found");
                }

                //await _DAO.RemoveAsync(order);
                await _unitOfWork.OrderRepository.RemoveAsync(order);

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
                //var existingOrder = await _DAO.GetByIdAsync(order.OrderId);
                var existingOrder = await _unitOfWork.OrderRepository.GetOrderbyId(order.OrderId);

                if (existingOrder == null)
                {
                    return new ValuationDiamondResult(0, "Order not found");
                }
                existingOrder.OrderId = order.OrderId;
                existingOrder.PayStatus = order.PayStatus;
                existingOrder.Status = order.Status;
                existingOrder.Day = order.Day;
                //existingOrder.Quantity = order.Quantity;
                //existingOrder.TotalAmount = order.TotalAmount;
                //existingOrder.CustomerId = order.CustomerId;
                existingOrder.Payment = order.Payment;
                //existingOrder.OrderCode = order.OrderCode;
                existingOrder.StaffName = order.StaffName;
                //_context.Entry(o).CurrentValues.SetValues(order);
                //await _DAO.UpdateAsync(existingOrder);
                await _unitOfWork.OrderRepository.UpdateAsync(existingOrder);

                return new ValuationDiamondResult(1, "Order updated successfully", existingOrder);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }
    }


}
