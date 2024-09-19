
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
			builder.Services.AddSwaggerGen(c =>
{
				c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = "Pharmacy Medicine Supply Management API",
					Version = "v1"
				});

				// Define the security scheme
				c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
					"Enter 'Bearer' [space] and then your token in the text input below." +
					"\r\n\r\nExample: 'Bearer 12345abcdef'",
					Name = "Authorization",
					In = Microsoft.OpenApi.Models.ParameterLocation.Header,
					Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				// Apply the security scheme globally
				c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
				{
					{
						new Microsoft.OpenApi.Models.OpenApiSecurityScheme
						{
							Reference = new Microsoft.OpenApi.Models.OpenApiReference
							{
								Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = Microsoft.OpenApi.Models.ParameterLocation.Header,
						},
						new List<string>()
					}
				});
			});


			builder.Services.AddDbContext<MedDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddScoped<IMedicineStockRepo<MedicineStock>, MedicineStockRepo>();
			builder.Services.AddScoped<IMedicineStockService<MedicineStock>, MedicineStockService>();

			builder.Services.AddScoped<IFileReader, FileReader>();
			builder.Services.AddScoped<IMedicalRepresentativeScheduleRepo, MedicalRepresentativeScheduleRepo>();
			builder.Services.AddScoped<IMedicalRepresentativeScheduleService, MedicalRepresentativeScheduleService>();

			builder.Services.AddScoped<IPharmacyMedicineSupplyRepo, PharmacyMedicineSupplyRepo>();
			builder.Services.AddScoped<IPharmacyMedicineSupplyService, PharmacyMedicineSupplyService>();


			// Enable JWT authentication
			var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

			// Add Authentication
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings.Issuer,
					ValidAudience = jwtSettings.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
				};
			});

			builder.Services.AddAuthorization();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication(); // Use Authentication

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
