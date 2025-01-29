using Booky_API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(option => {
	option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
//	.WriteTo.File("log/productLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();
builder.Services.AddControllers(options =>
{
	//options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<ILogging, LoggingV2>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
	app.UseSwaggerUI(options => {
		//options.SwaggerEndpoint("/swagger/v1/swagger.json", "Magic_VillaV1");
		//options.SwaggerEndpoint("/swagger/v2/swagger.json", "Magic_VillaV2");
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
