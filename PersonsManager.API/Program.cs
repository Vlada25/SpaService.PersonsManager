using AutoMapper;
using Microsoft.AspNetCore.HttpOverrides;
using PersonsManager.API.Extensions;
using PersonsManager.Domain;

var builder = WebApplication.CreateBuilder(args);


if (args.Length != 0)
{
    int firstIp = int.Parse($"5{args[0]}");
    int secondIp = int.Parse($"7{args[0]}");

    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(firstIp);
        options.ListenAnyIP(secondIp, configure => configure.UseHttps());
    });
}

builder.Services.ConfigureDbServices();

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson();

builder.Services.ConfigureMessageBroker(builder.Configuration);

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();

builder.Services.ConfigureSqlContext(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();


public partial class Program { }
