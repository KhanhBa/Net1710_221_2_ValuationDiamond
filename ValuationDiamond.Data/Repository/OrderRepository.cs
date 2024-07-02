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
    }
}
