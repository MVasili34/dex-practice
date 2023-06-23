using EntityModels;
using ServicesDb;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpLogging;

namespace BankAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.WebHost.UseUrls("https://localhost:5001/");
                                          //Adding DataBase context
            builder.Services.AddBankingServiceContext();

			// Add services to the container.
			builder.Services.AddControllers().AddJsonOptions(x =>
				x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddScoped<IClientService, ClientService>();
			builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddCors();
			var app = builder.Build();
			app.UseCors(options =>
			{
				options.WithMethods("GET", "POST", "PUT", "DELETE");
				options.WithOrigins("https://localhost:5005/");

            });
			// Configure the HTTP request pipeline.
			app.UseHttpLogging();
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