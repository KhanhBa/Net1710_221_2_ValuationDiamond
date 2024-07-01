<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;
=======
using ValuationDiamond.Business;
>>>>>>> 48ca876242f680a9a18046acb81f17f6fe964638

namespace ValuationDiamond.RazorWebApps
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
<<<<<<< HEAD
            builder.Services.AddScoped<IOrderBusiness, OrderBusiness>();
            builder.Services.AddScoped<ICustomerBusiness, CustomerBusiness>();
            //builder.Services.AddDbContext<Net1710_221_2_ValuationDiamondContext>(options =>
                //options.UseSqlServer(builder.Configuration.GetConnectionString("Net1710_221_2_ValuationDiamondContext") ?? throw new InvalidOperationException("Connection string 'Net1710_221_2_ValuationDiamondContext' not found.")));

=======
            builder.Services.AddScoped<ValuationCertificateBusiness>();
                
>>>>>>> 48ca876242f680a9a18046acb81f17f6fe964638
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}