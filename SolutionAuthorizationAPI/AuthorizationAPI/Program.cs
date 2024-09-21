
using AuthorizationAPI.Repository;
using AuthorizationAPI.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

namespace AuthorizationAPI
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

			// For MongoDb

			builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

			builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
				new MongoClient(builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString")));

			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IUserService, UserService>();

			// For JWT

			builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
			builder.Services.AddSingleton<JwtTokenGenerator>(); // Register JwtTokenGenerator

			// Add CORS policy
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAngularApp",
					policy => policy.WithOrigins("http://localhost:4200")  
									.AllowAnyHeader()
									.AllowAnyMethod()
									.AllowCredentials());
			});


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			// Use the CORS policy
			app.UseCors("AllowAngularApp");

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
