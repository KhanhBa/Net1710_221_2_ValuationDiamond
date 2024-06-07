using Microsoft.EntityFrameworkCore;
using ValuationDiamond.Data.Models;

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
            builder.Services.AddRazorPages();   

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