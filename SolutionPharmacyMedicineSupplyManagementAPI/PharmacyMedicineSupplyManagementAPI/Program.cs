
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupplyManagementAPI.Models;
using PharmacyMedicineSupplyManagementAPI.Repositories;
using PharmacyMedicineSupplyManagementAPI.Services;

namespace PharmacyMedicineSupplyManagementAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<MedDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddScoped<IMedicineStockRepo<MedicineStock>, MedicineStockRepo>();
			builder.Services.AddScoped<IMedicineStockService<MedicineStock>, MedicineStockService>();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
