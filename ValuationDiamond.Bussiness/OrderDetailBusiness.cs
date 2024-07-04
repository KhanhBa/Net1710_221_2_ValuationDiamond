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

namespace ValuationDiamond.Business
{
    public interface IOrderDetailBusiness
    {
        Task<IValuationDiamondResult> GetAll();
        Task<IValuationDiamondResult> GetById(string code);
        Task<IValuationDiamondResult> Save(OrderDetail orderDetail);
        Task<IValuationDiamondResult> Update(OrderDetail orderDetail);
        Task<IValuationDiamondResult> DeleteById(string code);
    }
    public class OrderDetailBusiness : IOrderDetailBusiness
    {
        private readonly OrderDetailDAO _DAO;

        public OrderDetailBusiness()
        {
            _DAO = new OrderDetailDAO();
        }

        public async Task<IValuationDiamondResult> DeleteById(string code)
        {
            try
            {
                var currency = await _DAO.GetByIdAsync(code);
                if (currency != null)
                {
                    var result = await _DAO.RemoveAsync(currency);
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
                    return new ValuationDiamondResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
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

                var orderDetails = await _DAO.GetAllAsync();

                if (orderDetails == null)
                {
                    return new ValuationDiamondResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
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

        public async Task<IValuationDiamondResult> GetById(string code)
        {
            try
            {
                #region Business rule
                #endregion

                var currency = await _DAO.GetByIdAsync(code);

                if (currency == null)
                {
                    return new ValuationDiamondResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new ValuationDiamondResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, currency);
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
                //

                int result = await _DAO.CreateAsync(orderDetail);
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
                int result = await _DAO.UpdateAsync(orderDetail);
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



