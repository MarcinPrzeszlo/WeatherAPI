using Microsoft.AspNetCore.Identity;
using NLog.Web;
using WeatherApp;
using WeatherApp.Entities;
using WeatherApp.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDataService, DataServices>();
builder.Services.AddScoped<IAccountService, AccountServices>();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DbSeeder>();
builder.Services.AddDbContext<WeatherAppDbContext>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var connectionValues = new ConnectionValues();
builder.Configuration.GetSection("ConnectionValues").Bind(connectionValues);
builder.Services.AddSingleton(connectionValues);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata= false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidAudience = connectionValues.JwtIssuer,
        ValidIssuer = connectionValues.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(connectionValues.JwtKey))
    };
});



var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=WeatherApp}/{action=SearchByCity}");

app.Run();
