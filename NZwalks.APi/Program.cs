using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NZwalks.APi.Data;
using NZwalks.APi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.
    AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddDbContext<NzWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));
});
builder.Services.AddScoped< IRegionRepo, RegionRepo>();
builder.Services.AddScoped<IWalkRepo, WalkRepo>();
builder.Services.AddScoped<Iwalkdifficulty, Walkdifficultyrepo>();


builder.Services.AddAutoMapper(typeof(Program).Assembly);


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
