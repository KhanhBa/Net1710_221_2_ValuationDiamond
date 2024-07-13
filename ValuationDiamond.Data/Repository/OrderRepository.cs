using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuationDiamond.Data.Base;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.Data.Repository
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository()
        {
        }

        public OrderRepository(Net1710_221_2_ValuationDiamondContext context) => _context = context;

        public async Task<Order> GetOrderbyId(int id)
        {
            var result = await _context.Orders.Include(x => x.Customer).FirstOrDefaultAsync(x => x.OrderId == id);
            return result;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _context.Orders.Include(x => x.OrderDetails).Include(x => x.Customer).ToListAsync();

            foreach (var order in orders)
            {
                var quantity = order.OrderDetails.Count();
                var totalAmount = order.OrderDetails.Sum(x => x.Price);
                order.Quantity = quantity;
                order.TotalAmount = totalAmount.Value;
            }

            await _context.SaveChangesAsync();
            return orders;
        }

        public async Task<(IEnumerable<Order> Data, int TotalCount)> GetPagedOrders(int pageIndex, int pageSize)
        {
            var query = _context.Orders.Include(x => x.Customer).AsQueryable();

            var totalItems = await query.CountAsync();

            var orders = await query
                .OrderBy(o => o.OrderId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var ordersList = await _context.Orders.Include(x => x.OrderDetails).Include(x => x.Customer).ToListAsync();

            foreach (var order in ordersList)
            {
                var quantity = order.OrderDetails.Count();
                var totalAmount = order.OrderDetails.Sum(x => x.Price);
                order.Quantity = quantity;
                order.TotalAmount = totalAmount.Value;
            }

            await _context.SaveChangesAsync();

            return (orders, totalItems);
        }

     
        public async Task<(IEnumerable<Order> Data, int TotalCount)> GetPagedSearchOrders(string orderCode, string staffName, string customer, int pageIndex, int pageSize)
        {
            var query = _context.Orders.Include(x => x.Customer).AsQueryable();

            if (!string.IsNullOrEmpty(orderCode))
            {
                query = query.Where(o => o.OrderCode.Contains(orderCode));
            }
            if (!string.IsNullOrEmpty(staffName))
            {
                query = query.Where(o => o.StaffName.Contains(staffName));
            }
            if (!string.IsNullOrEmpty(customer))
            {
                query = query.Where(o => o.Customer.Name.Contains(customer));
            }

            var totalItems = await query.CountAsync();

            var orders = await query
                .OrderBy(o => o.OrderId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var ordersList = await _context.Orders.Include(x => x.OrderDetails).Include(x => x.Customer).ToListAsync();

            foreach (var order in ordersList)
            {
                var quantity = order.OrderDetails.Count();
                var totalAmount = order.OrderDetails.Sum(x => x.Price);
                order.Quantity = quantity;
                order.TotalAmount = totalAmount.Value;
            }

            await _context.SaveChangesAsync();

            return (orders, totalItems);
        }
    }
}
