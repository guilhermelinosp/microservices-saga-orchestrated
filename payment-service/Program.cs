using Serilog;

var builder = WebApplication.CreateSlimBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
var host = builder.Host;

host.UseSerilog((context, options) =>
    options.ReadFrom.Configuration(context.Configuration));

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "payment-service");
    });
}

app.UseHealthChecks("/health");
app.UseSerilogRequestLogging();

await app.RunAsync();
