using Booky_API;
using Booky_API.Data;
using Booky_API.Models;
using Booky_API.Repository;
using Booky_API.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(options => {
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
	options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
});
builder.Services.AddIdentity<ApplicationIdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDBContext>();
builder.Services.AddResponseCaching();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddAutoMapper(typeof(MappingConfig));
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
//	.WriteTo.File("log/productLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();

var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(x => {
		x.RequireHttpsMetadata = false;
		x.SaveToken = true;
		x.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});


builder.Services.AddControllers(option =>
{
	option.CacheProfiles.Add("Default30",
	   new CacheProfile()
	   {
		   Duration = 30
	   });
	//options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSingleton<ILogging, LoggingV2>();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description =
			"JWT Authorization header using the Bearer scheme. \r\n\r\n " +
			"Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
			"Example: \"Bearer 12345abcdef\"",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Scheme = "Bearer"
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
				Scheme = "oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header
			},
			new List<string>()
		}
	});
});

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
