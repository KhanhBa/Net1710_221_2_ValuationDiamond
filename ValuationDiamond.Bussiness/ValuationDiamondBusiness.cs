using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuationDiamond.Common;
using ValuationDiamond.Data.DAO;
using ValuationDiamond.Data.Models;
using ValuationDiamond.Data;

namespace ValuationDiamond.Business
{
    public interface IValuationDiamondBusiness
    {
        Task<IValuationDiamondResult> GetAll();
        Task<IValuationDiamondResult> DeleteByID(int ID);
        Task<IValuationDiamondResult> Create(ValuateDiamond valuateDiamond);
        Task<IValuationDiamondResult> Update(int ID, ValuateDiamond valuateDiamond);
        Task<IValuationDiamondResult> Save(ValuateDiamond valuateDiamond);
        Task<IValuationDiamondResult> GetById(int ID);
        Task<IValuationDiamondResult> Search(string searchTerm);


    }

    public class ValuationDiamondBusiness : IValuationDiamondBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public ValuationDiamondBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<IValuationDiamondResult> Create(ValuateDiamond valuateDiamond)
        {
            try
            {
                var result = await _unitOfWork.valuationDiamondRepository.CreateAsync(valuateDiamond);
                if (result == null)
                {
                    return new ValuationDiamondResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
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
                var obj = await _unitOfWork.valuationDiamondRepository.GetByIdAsync(ID);
                if (obj == null)
                {
                    return new ValuationDiamondResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                var flag = await _unitOfWork.valuationDiamondRepository.RemoveAsync(obj);
                if (flag) { return new ValuationDiamondResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG); }
                else { return new ValuationDiamondResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG); }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
            };
        }

        public async Task<IValuationDiamondResult> GetAll()
        {
            try
            {
                var result = await _unitOfWork.valuationDiamondRepository.GetAllAsync();
                if (result == null)
                {
                    return new ValuationDiamondResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                else { return new ValuationDiamondResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result); }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> GetById(int ID)
        {
            try
            {
                var valuationDiamond = await _unitOfWork.valuationDiamondRepository.GetByIdAsync(ID);
                if (valuationDiamond == null)
                {
                    return new ValuationDiamondResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                return new ValuationDiamondResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, valuationDiamond);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }
        }

        public async Task<IValuationDiamondResult> Save(ValuateDiamond valuateDiamond)
        {
            try
            {
                //

                var result = await _unitOfWork.valuationDiamondRepository.CreateAsync(valuateDiamond);
                if (result != null)
                {
                    return new ValuationDiamondResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new ValuationDiamondResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IValuationDiamondResult> Update(int ID, ValuateDiamond valuateDiamond)
        {
            try
            {
                var result = await _unitOfWork.valuationDiamondRepository.GetByIdAsync(ID);
                if (result == null)
                {
                    return new ValuationDiamondResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
                result.ValuationStaffName = valuateDiamond.ValuationStaffName;
                //result.ValuationCertificates = valuateDiamond.ValuationCertificates;
                result.OrderDetail = valuateDiamond.OrderDetail;
                result.Price = valuateDiamond.Price;
                result.Time = valuateDiamond.Time;
                result.Carat = valuateDiamond.Carat;
                result.Clarity = valuateDiamond.Clarity;
                result.Color = valuateDiamond.Color;
                result.DiamondType = valuateDiamond.DiamondType;
                result.Shape = valuateDiamond.Shape;
                result.IsDiamond = valuateDiamond.IsDiamond;
                result.ValuationStaffName = valuateDiamond.ValuationStaffName;


                _unitOfWork.valuationDiamondRepository.Update(result);
                return new ValuationDiamondResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_CREATE_MSG, result);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }
        public async Task<IValuationDiamondResult> Search(string searchTerm)
        {
            try
            {
                var allDiamonds = await _unitOfWork.valuationDiamondRepository.GetAllAsync();

                if (allDiamonds == null || !allDiamonds.Any())
                {
                    return new ValuationDiamondResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }

                var filteredDiamonds = allDiamonds.Where(diamond =>
                    (diamond.ValuationStaffName?.Contains(searchTerm) ?? false) ||
                    (diamond.OrderDetailId != null && diamond.OrderDetailId.ToString().Contains(searchTerm)) ||
                    (diamond.Color?.Contains(searchTerm) ?? false) ||
                    (diamond.Price?.ToString().Contains(searchTerm) ?? false) ||
                    (diamond.Shape?.Contains(searchTerm) ?? false) ||
                    (diamond.DiamondType?.Contains(searchTerm) ?? false) ||
                    (diamond.Carat?.ToString().Contains(searchTerm) ?? false)

                );

                if (!filteredDiamonds.Any())
                {
                    return new ValuationDiamondResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }

                return new ValuationDiamondResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, filteredDiamonds.ToList());
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }
        }

    }
}




