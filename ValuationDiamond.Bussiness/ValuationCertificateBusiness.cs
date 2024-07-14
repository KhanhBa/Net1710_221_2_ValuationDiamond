
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuationDiamond.Data;
using ValuationDiamond.Data.DAO;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.Business
{
    public interface IValuationCertificateBusiness
    {
        Task<IValuationDiamondResult> GetAll();
        Task<IValuationDiamondResult> Save(ValuationCertificate valuationCertificate);
        Task<IValuationDiamondResult> DeleteByID(int ID);
        Task<IValuationDiamondResult> Create(ValuationCertificate valuationCertificate);
        Task<IValuationDiamondResult> GetbyId(int id);
    }
    public class ValuationCertificateBusiness : IValuationCertificateBusiness
    {
        // private readonly ValuationCertificateDAO _certificateDAO;
        private readonly UnitOfWork _unitOfWork;
        public ValuationCertificateBusiness()
        {
            //   _certificateDAO = new ValuationCertificateDAO();
            _unitOfWork ??= new UnitOfWork();
        }


        public async Task<IValuationDiamondResult> DeleteByID(int ID)
        {
            try
            {
                //var obj = await _certificateDAO.GetByIdAsync(ID);
                var obj = await _unitOfWork.CertificateRepository.GetByIdAsync(ID);
                if (obj == null)
                {
                    return new ValuationDiamondResult(-1, "Can not find");
                }
                //  var flag = await _certificateDAO.RemoveAsync(obj);
                var flag = await _unitOfWork.CertificateRepository.RemoveAsync(obj);
                if (flag) { return new ValuationDiamondResult(1, "Successfully"); }
                else { return new ValuationDiamondResult(-1, "Fail"); }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(-1, ex.Message);
            }
        }

        public async Task<IValuationDiamondResult> GetAll()
        {
            try
            {
                //var obj = await _certificateDAO.GetAllAsync();
                var obj = await _unitOfWork.CertificateRepository.GetListValuationCertificate();
                if (obj == null)
                {
                    return new ValuationDiamondResult(-1, "List Null", null);
                }
                else { return new ValuationDiamondResult(1, "List Data", obj); }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(-1, ex.Message);
            }
        }

        public async Task<IValuationDiamondResult> Create(ValuationCertificate valuationCertificate)
        {
            try
            {
                var obj = await _unitOfWork.CertificateRepository.CreateAsync(valuationCertificate);
                //   var obj = await _certificateDAO.CreateAsync(valuationCertificate);
                if (obj == null)
                {
                    return new ValuationDiamondResult(-1, "Can not Create");
                }
                return new ValuationDiamondResult(1, "Create Successfully", obj);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(-1, ex.Message);
            }
        }

        public async Task<IValuationDiamondResult> Save(ValuationCertificate valuationCertificate)
        {
            try
            {
                // var obj = await _certificateDAO.GetByIdAsync(valuationCertificate.ValuationId);
                //var obj = await _unitOfWork.CertificateRepository.GetByIdAsync(valuationCertificate.ValuationId);
                //if (obj == null)
                //{
                //    return new ValuationDiamondResult(0, "Order not found");
                //}
                //obj = valuationCertificate;
                //_context.Entry(o).CurrentValues.SetValues(order);
                // await _certificateDAO.UpdateAsync(obj);
                await _unitOfWork.CertificateRepository.UpdateAsync(valuationCertificate);
                return new ValuationDiamondResult(1, "Order updated successfully", valuationCertificate);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }
        public async Task<IValuationDiamondResult> GetbyId(int id)
        {
            try
            {
                if (id == null)
                {
                    return new ValuationDiamondResult(-1, "Fail");
                }
                //var obj = await _certificateDAO.GetByIdAsync(ID);
                var obj = await _unitOfWork.CertificateRepository.GetByIdAsync(id);
                if (obj == null)
                {
                    return new ValuationDiamondResult(-1, "Can not find");
                }
                else
                {
                    return new ValuationDiamondResult(1, "List Data", obj);
                }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(-1, ex.Message);
            }
        }
    }
}