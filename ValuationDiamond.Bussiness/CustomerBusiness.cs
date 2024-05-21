using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.Business
{
    public interface ICusTomerBusiness
    {
        Task<IValuationDiamondResult> GetAllCustomers();
        Task<bool> CheckCustomerIdExist(int customerId);
    }

    public class CustomerBusiness:ICusTomerBusiness
    {
        private readonly Net1710_221_2_ValuationDiamondContext _Dbcontext;
        public async Task<IValuationDiamondResult> GetAllCustomers()
        {
            var result = await _Dbcontext.Customers.OrderByDescending(x=>x.CustomerId).Include(x=>x.Orders).ThenInclude(x=>x.OrderDetails).ToListAsync();
            return new ValuationDiamondResult(1, "List Customer", result);
        }

        public async Task<bool> CheckCustomerIdExist(int customerId)
        {
            return await _Dbcontext.Customers.AnyAsync(c => c.CustomerId == customerId);
        }
    }
   
}
