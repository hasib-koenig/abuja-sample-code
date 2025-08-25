using Microsoft.EntityFrameworkCore;
using Server.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApplication",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:5173","http://localhost:5174") 
                .AllowAnyMethod() // Allow GET, POST, PUT, DELETE
                .AllowAnyHeader() // Allow headers like Authorization, Content-Type
                .AllowCredentials(); // Allow cookies and credentials
        });
});


builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    }); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("Default")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ReactApplication");

app.UseAuthorization();

app.MapControllers();

app.Run();
