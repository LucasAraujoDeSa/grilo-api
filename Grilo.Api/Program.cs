using Grilo.Api.Config;
using Grilo.Api.Dependencies;
using Grilo.Infra;
using Grilo.Shared.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

Cors.Configure(builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
Swagger.Configure(builder);

builder.Services.AddInfra(builder.Configuration);
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
