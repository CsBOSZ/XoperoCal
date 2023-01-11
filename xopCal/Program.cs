using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using xopCal.Entity;
using xopCal.Model;
using xopCal.Model.Validators;
using xopCal.Services;

var builder = WebApplication.CreateBuilder(args);

// Wyświetlanie/dodawanie/edytowanie/usuwanie alarmów/zdarzeń do wbudowanego kalendarza
// Wyszukiwanie zdarzeń po nazwie bądź okresie czasu
//     Eksportowanie wybranych zdarzeń
// Importowanie zdarzeń z pliku
// W przypadku alarmu, program powinien wyświetlić powiadomienie (o ile jest uruchomiony) z możliwości snooze/confirm
//     (Bonus, jeśli reszta pójdzie za szybko) Umożliwienie 2-3 w LAN


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EventDbContext>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<UserDto>, UserDtoValidator>();

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