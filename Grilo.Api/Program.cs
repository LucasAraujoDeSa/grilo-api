using System.Text.Json.Serialization;
using Grilo.Api.Config;
using Grilo.Api.Dependencies;

var builder = WebApplication.CreateBuilder(args);

Cors.Configure(builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
Swagger.Configure(builder);

Database.Initialize(builder);
Adapters.Initialize(builder);
Account.Initialize(builder);
Item.Initialize(builder);
Order.Initialize(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("_myAllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
