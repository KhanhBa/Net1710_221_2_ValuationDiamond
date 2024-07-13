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
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository() 
        {

        }

        public async Task<(IEnumerable<Customer> Data, int TotalCount)> GetPagedCustomers(int pageIndex, int pageSize)
        {
            var query = _context.Customers.AsQueryable();

            var totalItems = await query.CountAsync();

            var customers = await query
                .OrderBy(c => c.CustomerId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (customers, totalItems);
        }

        public CustomerRepository(Net1710_221_2_ValuationDiamondContext context) => _context = context;
    }
}
