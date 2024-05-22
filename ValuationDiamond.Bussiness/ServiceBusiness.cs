using ValuationDiamond.Data.DAO;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.Business
{
    public interface IServiceBusiness
    {
        Task<IValuationDiamondResult> GetAllService();
        Task<IValuationDiamondResult> GetService(string serviceId);
        Task<IValuationDiamondResult> AddService(Service service);
        Task<IValuationDiamondResult> UpdateService(string serviceId, Service service);
        Task<IValuationDiamondResult> DeleteService(string serviceId);
    }

    public class ServiceBusiness : IServiceBusiness
    {
        private readonly ServiceDAO _DAO;

        public async Task<IValuationDiamondResult> GetAllService()
        {
            try
            {
                var service = await _DAO.GetAllAsync();
                return new ValuationDiamondResult(1, "Success", service);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> GetService(string serviceId)
        {
            try
            {
                var service = await GetService(serviceId);
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
                await _DAO.CreateAsync(service);
                return new ValuationDiamondResult(1, "Service added successfully.");
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> UpdateService(string serviceId, Service updateService)
        {
            try
            {
                var existingService = await _DAO.GetByIdAsync(serviceId);
                if (existingService == null)
                {
                    return new ValuationDiamondResult(0, "Service not found.");
                }

                existingService.ServiceId = updateService.ServiceId;
                existingService.Name = updateService.Name;
                existingService.Price = updateService.Price;
                existingService.Decription = updateService.Decription;

                _DAO.UpdateAsync(existingService);
                return new ValuationDiamondResult(1, "Service updated successfully", updateService);
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }

        public async Task<IValuationDiamondResult> DeleteService(string serviceId)
        {
            try
            {
                var service = await _DAO.GetByIdAsync(serviceId);
                if (service == null)
                {
                    return new ValuationDiamondResult(0, "Service not found.");
                }

                _DAO.RemoveAsync(service);

                return new ValuationDiamondResult(1, "Service deleted successfully.");
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }
    }
}
