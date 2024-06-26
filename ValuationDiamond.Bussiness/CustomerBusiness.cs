﻿using ValuationDiamond.Data.DAO;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.Business
{
    public interface ICustomerBusiness
    {
        Task<IValuationDiamondResult> GetAllCustomer();
        Task<IValuationDiamondResult> GetCustomer(string customerId);
        Task<IValuationDiamondResult> AddCustomer(Customer customer);
        Task<IValuationDiamondResult> UpdateCustomer(string customerId, Customer updateCustomer);
        Task<IValuationDiamondResult> DeleteCustomer(string customerId);
        Task<bool> CheckCustomerIdExist(int customerId);
    }

    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly CustomerDAO _DAO;

        public CustomerBusiness()
        {
            _DAO = new CustomerDAO();
        }
       

        public async Task<IValuationDiamondResult> GetAllCustomer()
        {
            try {
                #region Business rule
                #endregion

                var customer = await _DAO.GetAllAsync();
            return new ValuationDiamondResult(1, "Success", customer);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> GetCustomer(string customerId)
        {
            try
            {
                var customer = await GetCustomer(customerId);
                return new ValuationDiamondResult(1, "Success", customer);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> AddCustomer(Customer customer)
        {
            try
            {
                await _DAO.CreateAsync(customer);
                return new ValuationDiamondResult(1, "Customer added successfully.");
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> UpdateCustomer(string customerId, Customer updateCustomer)
        {
            try { 
            var existingCustomer = await _DAO.GetByIdAsync(customerId);
            if (existingCustomer == null)
            {
                return new ValuationDiamondResult(0, "Customer not found.");
            }

            existingCustomer.CustomerId = updateCustomer.CustomerId;

            _DAO.UpdateAsync(existingCustomer);
            return new ValuationDiamondResult(1, "Customer updated successfully", updateCustomer);
            }
            catch(Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> DeleteCustomer(string customerId)
        {
            try { 
            var customer = await _DAO.GetByIdAsync(customerId);
            if (customer == null)
            {
                return new ValuationDiamondResult(0, "Customer not found.");
            }

            _DAO.Remove(customer);

            return new ValuationDiamondResult(1, "Customer deleted successfully.");
            }
            catch( Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<bool> CheckCustomerIdExist(int customerId)
        {
            var customer = await _DAO.GetByIdAsync(customerId);
            if (customer == null)
            {
                return false;
            }
            else return true;
        }
    }
   
}
