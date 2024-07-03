using ValuationDiamond.Common;
using ValuationDiamond.Data;
using ValuationDiamond.Data.DAO;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.Business
{
    public interface ICustomerBusiness
    {
        Task<IValuationDiamondResult> GetAllCustomer();
        Task<IValuationDiamondResult> GetCustomer(int customerId);
        Task<IValuationDiamondResult> AddCustomer(Customer customer);
        Task<IValuationDiamondResult> UpdateCustomer(Customer updateCustomer);
        Task<IValuationDiamondResult> DeleteCustomer(int customerId);
        Task<bool> CheckCustomerIdExist(int customerId);
        Task<IValuationDiamondResult> SaveCustomer(Customer customer);
        Task<IValuationDiamondResult> SearchCustomers(string searchField, string search);
    }

    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public CustomerBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<IValuationDiamondResult> GetAllCustomer()
        {
            try
            {
                var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
                return new ValuationDiamondResult(1, "Success", customers);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ValuationDiamondResult(0, "Failed to retrieve customers", null);
            }
        }

        public async Task<IValuationDiamondResult> GetCustomer(int customerId)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
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
                await _unitOfWork.CustomerRepository.CreateAsync(customer);
                return new ValuationDiamondResult(1, "Customer added successfully.");
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> UpdateCustomer(Customer updateCustomer)
        {
            try
            {
                var existingCustomer = await _unitOfWork.CustomerRepository.GetByIdAsync(updateCustomer.CustomerId);
                if (existingCustomer == null)
                {
                    return new ValuationDiamondResult(0, "Customer not found.");
                }

                existingCustomer.CustomerId = updateCustomer.CustomerId;
                existingCustomer.DoB = updateCustomer.DoB;
                existingCustomer.Status = updateCustomer.Status;
                existingCustomer.Avatar = updateCustomer.Avatar;
                existingCustomer.Cccd = updateCustomer.Cccd;
                existingCustomer.Email = updateCustomer.Email;
                existingCustomer.Phone = updateCustomer.Phone;
                existingCustomer.Password = updateCustomer.Password;
                existingCustomer.Name = updateCustomer.Name;
                existingCustomer.Address = updateCustomer.Address;
                // Update other properties as needed

                await _unitOfWork.CustomerRepository.UpdateAsync(existingCustomer);
                return new ValuationDiamondResult(1, "Customer updated successfully", updateCustomer);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> DeleteCustomer(int customerId)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
                if (customer == null)
                {
                    return new ValuationDiamondResult(0, "Customer not found.");
                }

                await _unitOfWork.CustomerRepository.RemoveAsync(customer);
                return new ValuationDiamondResult(1, "Customer deleted successfully.");
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<bool> CheckCustomerIdExist(int customerId)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                return false;
            }
            else return true;
        }

        public async Task<IValuationDiamondResult> SaveCustomer(Customer customer)
        {
            try
            {
                var existingCustomer = await _unitOfWork.CustomerRepository.GetByIdAsync(customer.CustomerId);
                if (existingCustomer == null)
                {
                    await _unitOfWork.CustomerRepository.CreateAsync(customer);
                    return new ValuationDiamondResult(1, "Customer added successfully.");
                }
                else
                {
                    existingCustomer.Name = customer.Name;
                    existingCustomer.Address = customer.Address;
                    // Update other properties as needed

                    await _unitOfWork.CustomerRepository.UpdateAsync(existingCustomer);
                    return new ValuationDiamondResult(1, "Customer updated successfully.");
                }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> SearchCustomers(string searchField, string search)
        {
            try
            {
                var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
                List<Customer> list = new List<Customer>();

                foreach (Customer c in customers)
                {
                   
                    if (searchField.Equals("Name", StringComparison.OrdinalIgnoreCase) && c.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                    {
                        list.Add(c);
                    }
                    else if (searchField.Equals("Cccd", StringComparison.OrdinalIgnoreCase) && c.Cccd.Contains(search, StringComparison.OrdinalIgnoreCase))
                    {
                        list.Add(c);
                    }
                    else if (searchField.Equals("Email", StringComparison.OrdinalIgnoreCase) && c.Email.Contains(search, StringComparison.OrdinalIgnoreCase))
                    {
                        list.Add(c);
                    }
                }

                if (list == null)
                {
                    return new ValuationDiamondResult(0, "No customers found");
                }
                else
                {
                    return new ValuationDiamondResult(1, "All Customers", list);
                }
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }
    }
}
