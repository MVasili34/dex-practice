using EntityModels;
using ServicesDb;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:5001/");

//Adding Database context
string connectionString = builder.Configuration.GetConnectionString("DbConnection")!;
builder.Services.AddBankingServiceContext(connectionString);

builder.Services.AddControllers().AddJsonOptions(x =>
	x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddCors();

//Configuring HTTP logging
builder.Services.AddHttpLogging(options =>
{
	options.RequestHeaders.Add("Origin");

	options.LoggingFields = HttpLoggingFields.All;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(options =>
{
	options.WithMethods("GET", "POST", "PUT", "DELETE");
	options.WithOrigins("https://localhost:5005/");

});

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpLogging();

app.UseAuthorization();


app.MapControllers();

app.Run();