using BankingService.Data;
using EntityModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Headers;
using Radzen;

namespace BankingService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddBankingServiceContext();
			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();
			builder.Services.AddSingleton(new CurrencyService("L4tz65xgEvSuCcSPnYjzjBzhU4EAWd"));
			builder.Services.AddHttpClient("BankAPI", options =>
			{
				options.BaseAddress = new Uri("https://localhost:5001/");
				options.DefaultRequestHeaders.Accept.Add(
					new MediaTypeWithQualityHeaderValue("application/json", 1.0));
			});
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

			app.MapBlazorHub();
			app.MapFallbackToPage("/_Host");

			app.Run();
		}
	}
}