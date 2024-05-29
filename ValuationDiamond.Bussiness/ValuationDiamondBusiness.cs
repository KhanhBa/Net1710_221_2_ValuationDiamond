using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuationDiamond.Common;
using ValuationDiamond.Data.DAO;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.Business
{
    public interface IValuationDiamondBusiness
    {
        Task<IValuationDiamondResult> GetAll();
        Task<IValuationDiamondResult> DeleteByID(int ID);
        Task<IValuationDiamondResult> Create(ValuateDiamond valuateDiamond);
        Task<IValuationDiamondResult> Update(int ID, ValuateDiamond valuateDiamond);


    }

    public class ValuationDiamondBusiness: IValuationDiamondBusiness
    {
        private readonly ValuationDiamondDAO _valuationDiamondDAO;
        public ValuationDiamondBusiness()
        {
            _valuationDiamondDAO = new ValuationDiamondDAO();
        }

        public async Task<IValuationDiamondResult> Create(ValuateDiamond valuateDiamond)
        {
            try
            {
                var result = await _valuationDiamondDAO.CreateAsync(valuateDiamond);
                if (result == null)
                {
                    return new ValuationDiamondResult(Const.SUCCESS_CREATE_CODE,Const.SUCCESS_CREATE_MSG);
                }
                return new ValuationDiamondResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, result);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
        }

        public async Task<IValuationDiamondResult> DeleteByID(int ID)
        {
            try
            {
                var obj = await _valuationDiamondDAO.GetByIdAsync(ID);
                if (obj == null)
                {
                    return new ValuationDiamondResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                var flag = await _valuationDiamondDAO.RemoveAsync(obj);
                if (flag) { return new ValuationDiamondResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG); }
                else { return new ValuationDiamondResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG); }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(Const.FAIL_DELETE_CODE,Const.FAIL_DELETE_MSG);
            };
        }

        public async Task<IValuationDiamondResult> GetAll()
        {
            try
            {
                var result =await _valuationDiamondDAO.GetAllAsync();
                if (result == null)
                {
                    return new ValuationDiamondResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG, null);
                }
                else { return new ValuationDiamondResult(Const.SUCCESS_READ_CODE,Const.SUCCESS_READ_MSG, result); }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }
        }

        public async Task<IValuationDiamondResult> Update(int ID, ValuateDiamond valuateDiamond)
        {
            try
            {
                var result = await _valuationDiamondDAO.GetByIdAsync(ID);
                if (result == null)
                {
                    return new ValuationDiamondResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
                result.ValuationStaffName = valuateDiamond.ValuationStaffName;
                result.ValuationCertificates = valuateDiamond.ValuationCertificates;
                result.OrderDetail = valuateDiamond.OrderDetail;
                result.Price = valuateDiamond.Price;
                result.Time = valuateDiamond.Time;

               _valuationDiamondDAO.Update(result);
                return new ValuationDiamondResult(Const.SUCCESS_UPDATE_CODE,Const.SUCCESS_CREATE_MSG, result);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }
    }

  
}
