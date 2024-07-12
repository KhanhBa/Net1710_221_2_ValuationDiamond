using ValuationDiamond.Data;
using ValuationDiamond.Data.DAO;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.Business
{
    public interface IServiceBusiness
    {
        Task<IValuationDiamondResult> GetAllService();
        Task<IValuationDiamondResult> GetService(int serviceId);
        Task<IValuationDiamondResult> AddService(Service service);
        Task<IValuationDiamondResult> UpdateService(Service updateService);
        Task<IValuationDiamondResult> DeleteService(int serviceId);
        Task<IValuationDiamondResult> SearchServices(string searchField, string search);
    }

    public class ServiceBusiness : IServiceBusiness
    {

        
        private readonly UnitOfWork _unitOfWork;

        public ServiceBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<IValuationDiamondResult> GetAllService()
        {
            try
            {
                var service = await _unitOfWork.ServiceRepository.GetAllAsync();
                return new ValuationDiamondResult(1, "Success", service);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> GetService(int serviceId)
        {
            try
            {
                var service = await _unitOfWork.ServiceRepository.GetByIdAsync(serviceId);
                return new ValuationDiamondResult(1, "Success", service);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> AddService(Service service)
        {
            try
            {
                await _unitOfWork.ServiceRepository.CreateAsync(service);
                return new ValuationDiamondResult(1, "Service added successfully.");
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> UpdateService(Service updateService)
        {
            try
            {
                var existingService = await _unitOfWork.ServiceRepository.GetByIdAsync(updateService.ServiceId);
                if (existingService == null)
                {
                    return new ValuationDiamondResult(0, "Service not found.");
                }

                existingService.ServiceId = updateService.ServiceId;
                existingService.Name = updateService.Name;
                existingService.Price = updateService.Price;
                existingService.Decription = updateService.Decription;

                await _unitOfWork.ServiceRepository.UpdateAsync(existingService);
                return new ValuationDiamondResult(1, "Service updated successfully", updateService);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> DeleteService(int serviceId)
        {
            try
            {
                var service = await _unitOfWork.CustomerRepository.GetByIdAsync(serviceId);
                if (service == null)
                {
                    return new ValuationDiamondResult(0, "Service not found.");
                }

                _unitOfWork.CustomerRepository.RemoveAsync(service);

                return new ValuationDiamondResult(1, "Service deleted successfully.");
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> SearchServices(string searchField, string search)
        {
            try
            {
                var services = await _unitOfWork.ServiceRepository.GetAllAsync();
                List<Service> list = new List<Service>();

                foreach (Service c in services)
                {

                    if (searchField.Equals("Name", StringComparison.OrdinalIgnoreCase) && c.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
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
