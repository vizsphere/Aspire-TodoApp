using TodoApi;
using TodoApi.Controllers;
using TodoApi.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.AddServiceDefaults();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.AddRedis("cache");
builder.Services.AddSingleton<IRedisTodoRepository, RedisTodoRepository>();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.MapDefaultEndpoints();

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
