using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuationDiamond.Data.DAO;
using ValuationDiamond.Data.Models;
using ValuationDiamond.Data.DAO;
using ValuationDiamond.Business;
using System.Runtime.CompilerServices;
using ValuationDiamond.Common;
using ValuationDiamond.Data;

namespace ValuationDiamond.Business
{
    public interface IOrderDetailBusiness
    {
        Task<IValuationDiamondResult> GetAll();
        Task<IValuationDiamondResult> GetById(int code);
        Task<IValuationDiamondResult> Save(OrderDetail orderDetail);
        Task<IValuationDiamondResult> Update(OrderDetail orderDetail);
        Task<IValuationDiamondResult> DeleteById(int code);
    }
    public class OrderDetailBusiness : IOrderDetailBusiness
    {
        //private readonly OrderDetailDAO _DAO;
        private readonly UnitOfWork _unitOfWork;

        public OrderDetailBusiness()
        {
            // _DAO = new OrderDetailDAO();
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IValuationDiamondResult> DeleteById(int code)
        {
            try
            {
                //var currency = await _DAO.GetByIdAsync(code);
                var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(code);
                if (orderDetail != null)
                {
                    var result = await _unitOfWork.OrderDetailRepository.RemoveAsync(orderDetail);
                    if (result)
                    {
                        return new ValuationDiamondResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new ValuationDiamondResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new ValuationDiamondResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(-4, ex.ToString());
            }
        }

        public async Task<IValuationDiamondResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                // var orderDetails = await _DAO.GetAllAsync();
                var orderDetails = await _unitOfWork.OrderDetailRepository.GetAllAsync();

                if (orderDetails == null)
                {
                    return new ValuationDiamondResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new ValuationDiamondResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetails);
                }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IValuationDiamondResult> GetById(int code)
        {
            try
            {
                #region Business rule
                #endregion

                //var currency = await _DAO.GetByIdAsync(code);

                var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(code);

                if (orderDetail == null)
                {
                    return new ValuationDiamondResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new ValuationDiamondResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetail);
                }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IValuationDiamondResult> Save(OrderDetail orderDetail)
        {
            try
            {


                //int result = await _DAO.CreateAsync(orderDetail);
                _unitOfWork.OrderDetailRepository.PrepareCreate(orderDetail);
                int result = await _unitOfWork.OrderDetailRepository.SaveAsync();
                if (result > 0)
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

        public async Task<IValuationDiamondResult> Update(OrderDetail orderDetail)
        {
            try
            {
                //int result = await _DAO.UpdateAsync(orderDetail);
                int result = await _unitOfWork.OrderDetailRepository.UpdateAsync(orderDetail);
                if (result > 0)
                {
                    return new ValuationDiamondResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new ValuationDiamondResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult(-4, ex.ToString());
            }
        }
    }
}



