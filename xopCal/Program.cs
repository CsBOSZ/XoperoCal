using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using xopCal;
using xopCal.Entity;
using xopCal.Hubs;
using xopCal.Model;
using xopCal.Model.Validators;
using xopCal.Services;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddCors(o =>
{
    o.AddPolicy("MyPolicy", corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
    
    o.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });

});

var authSetting = new AuthenticationSettings();
// {
//     
//     JwtKey= "PRIVATE_KEY_DONT_SHARE",
//     JwtExpireDays= 15,
//     JwtIssuer= "http://eventcalapi.com"
//     
// };

builder.Configuration.GetSection("Authentication").Bind(authSetting);

builder.Services.AddSingleton(authSetting);
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = "Bearer";
    o.DefaultScheme = "Bearer";
    o.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = authSetting.JwtIssuer,
        ValidAudience = authSetting.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSetting.JwtKey))
    };


});

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EventDbContext>(o =>
{

    o.UseNpgsql(builder.Configuration.GetConnectionString("Npgsql"));

});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEventCalService, EventCalService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


builder.Services.AddScoped<IValidator<UserDtoIn>, UserDtoValidator>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
builder.Services.AddScoped<IValidator<TimeDto>, TimeDtoValidator>();
builder.Services.AddScoped<IValidator<EventCalDtoIn>, EventCalDtoValidator>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

// app.UseCors("MyPolicy");
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.MapHub<EventHub>("/EventHub");

app.Run();