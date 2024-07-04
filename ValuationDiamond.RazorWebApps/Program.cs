using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using ValuationDiamond.Data.Models;
=======
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;
using ValuationDiamond.Business;
>>>>>>> ccc01c948dd234422eeb6692bbcf13079a222c39

namespace ValuationDiamond.RazorWebApps
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<Net1710_221_2_ValuationDiamondContext>(options =>
            options.UseSqlServer("Data Source=(local);Initial Catalog=NET1710_2221_2_ValuationDiamond;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

            // Add services to the container.
<<<<<<< HEAD
            builder.Services.AddRazorPages();   
=======
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IOrderBusiness, OrderBusiness>();
            builder.Services.AddScoped<ICustomerBusiness, CustomerBusiness>();
            //builder.Services.AddDbContext<Net1710_221_2_ValuationDiamondContext>(options =>
                //options.UseSqlServer(builder.Configuration.GetConnectionString("Net1710_221_2_ValuationDiamondContext") ?? throw new InvalidOperationException("Connection string 'Net1710_221_2_ValuationDiamondContext' not found.")));
>>>>>>> ccc01c948dd234422eeb6692bbcf13079a222c39

            builder.Services.AddScoped<ValuationCertificateBusiness>();
                
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