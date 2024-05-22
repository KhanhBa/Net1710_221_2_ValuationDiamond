using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly Net1710_221_2_ValuationDiamondContext _context;

        public async Task<IValuationDiamondResult> GetAllService()
        {
            try
            {
                var service = await _context.Services.ToListAsync();
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
                await _context.Services.AddAsync(service);
                await _context.SaveChangesAsync();
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
                var existingService = await _context.Services.FindAsync(serviceId);
                if (existingService == null)
                {
                    return new ValuationDiamondResult(0, "Service not found.");
                }

                existingService.ServiceId = updateService.ServiceId;
                existingService.Name = updateService.Name;
                existingService.Price = updateService.Price;
                existingService.Decription = updateService.Decription;

                _context.Services.Update(existingService);
                await _context.SaveChangesAsync();
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
                var service = await _context.Services.FindAsync(serviceId);
                if (service == null)
                {
                    return new ValuationDiamondResult(0, "Service not found.");
                }

                _context.Services.Remove(service);
                await _context.SaveChangesAsync();

                return new ValuationDiamondResult(1, "Service deleted successfully.");
            }
            catch (Exception ex)
            {
                return new ValuationDiamondResult();
            }
        }
    }
}
