using Payments.Application;
using Serilog;

var builder = WebApplication.CreateSlimBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
var host = builder.Host;
var web = builder.WebHost;

host.UseSerilog((context, options) =>
    options.ReadFrom.Configuration(context.Configuration));

web.ConfigureKestrel((context, options) =>
    options.Configure(context.Configuration));

await services.AddApplicationInjection(configuration);

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddOpenApi();
services.AddSwaggerGen();
services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Payments.API v1");
    });
}

app.UseHttpsRedirection();
app.UseHealthChecks("/health");
app.UseSerilogRequestLogging();

await app.RunAsync();
