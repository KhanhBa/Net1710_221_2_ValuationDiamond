
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Task<IValuationDiamondResult> GetAllOnlyDatabase();
    }
    public class ValuationCertificateBusiness : IValuationCertificateBusiness
    {
        // private readonly ValuationCertificateDAO _certificateDAO;
        private readonly UnitOfWork _unitOfWork;
        private readonly RedisManagement _redisManagement;
        public ValuationCertificateBusiness()
        {
            //   _certificateDAO = new ValuationCertificateDAO();
            _unitOfWork ??= new UnitOfWork();
            _redisManagement ??= new RedisManagement();
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
                if (flag)
                {
                    _redisManagement.DeleteData("ListCertificates");
                    return new ValuationDiamondResult(1, "Successfully"); }
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
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var list = _redisManagement.GetData("ListCertificates");    
                if(list==null||list=="[]")
                {
                    //var obj = await _certificateDAO.GetAllAsync();
                    var obj = await _unitOfWork.CertificateRepository.GetListValuationCertificate();

                    if (obj == null)
                    {
                        return new ValuationDiamondResult(-1, "List Null", null);
                    }
                    else
                    {
                        stopwatch.Stop();
                        Console.WriteLine($"Database: {stopwatch.ElapsedMilliseconds} ms");
                        _redisManagement.SetData("ListCertificates",JsonConvert.SerializeObject(obj));
                        return new ValuationDiamondResult(1, "List Data", obj);
                    }
                }
                else
                {
                    var result = JsonConvert.DeserializeObject<List<ValuationCertificate>>(list);
                    Console.WriteLine($"Redis: {stopwatch.ElapsedMilliseconds} ms");
                    return new ValuationDiamondResult(1, "List Data", result);
                }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(-1, ex.Message);
            }
        }
        public async Task<IValuationDiamondResult> GetAllOnlyDatabase()
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();    
                    //var obj = await _certificateDAO.GetAllAsync();
                    var obj = await _unitOfWork.CertificateRepository.GetListValuationCertificate();

                    if (obj == null)
                    {
                        return new ValuationDiamondResult(-1, "List Null", null);
                    }
                    else
                    {
                        stopwatch.Stop();
                        Console.WriteLine($"Database: {stopwatch.ElapsedMilliseconds} ms");
                        return new ValuationDiamondResult(1, "List Data", obj);
                    }
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
                await _redisManagement.AddValuationCertificateToListAsync("ListCertificates", obj);
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
                _redisManagement.DeleteData("ListCertificates");
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