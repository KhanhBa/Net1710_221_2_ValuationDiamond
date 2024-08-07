﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuationDiamond.Data.Base;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.Data.Repository
{
    public class ValuationCertificateRepository : GenericRepository<ValuationCertificate>
    {
        public ValuationCertificateRepository() { }
        public ValuationCertificateRepository(Net1710_221_2_ValuationDiamondContext context) => _context = context;
        public async Task<List<ValuationCertificate>> GetListValuationCertificate()
        {
            var result = await _context.ValuationCertificates.Include(x=>x.ValuateDiamond).ToListAsync();
            return result;
        }
    }
}
