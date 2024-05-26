using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using ValuationDiamond.Data.Models;
using ValuationDiamond.Data.Repository;

namespace ValuationDiamond.Data
{
    public class UnitOfWork
    {
        private ValuationCertificateRepository _certificateRepository;
        private Net1710_221_2_ValuationDiamondContext _context;
        
        public UnitOfWork() { }

        public ValuationCertificateRepository CertificateRepository { 
            get { 
                return _certificateRepository ??= new ValuationCertificateRepository(); 
            }
        }
    }
}