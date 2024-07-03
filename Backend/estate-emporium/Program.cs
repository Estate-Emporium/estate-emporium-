using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using estate_emporium;

using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using DotNetEnv;
using estate_emporium.Models;
using estate_emporium.Utils;
using estate_emporium.Services;
using System.Runtime.ConstrainedExecution;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                      policy.WithOrigins(
                                            Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "https://localhost:80/").WithMethods("GET", "POST", "PUT", "DELETE")
                 .WithHeaders("Authorization", "Content-Type", "Accept");
                    });
});

// Add services to the container.
builder.Services.AddControllers(options =>
{
  options.Filters.Add<ValidateBodyUtils>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

DbUtils.initDB(builder);

builder.Services.AddMvc();

builder.Services.AddHttpClient(nameof(HttpClientEnum.property_manager), httpClient =>
{
  httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("property_URL"));
  httpClient.DefaultRequestHeaders.Add("X-Origin", "real_estate_sales");

})
.AddPolicyHandler(PollyUtils.GetRetryPolicy());

builder.Services.AddHttpClient(nameof(HttpClientEnum.home_loans), httpClient =>
{
  httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("home_loans_URL"));
  httpClient.DefaultRequestHeaders.Add("X-Origin", "real_estate_sales");

}).AddPolicyHandler(PollyUtils.GetRetryPolicy());

builder.Services.AddHttpClient(nameof(HttpClientEnum.retail_bank), httpClient =>
{
  httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("retail_bank_URL"));
  httpClient.DefaultRequestHeaders.Add("X-Origin", "real_estate_sales");

}).AddPolicyHandler(PollyUtils.GetRetryPolicy());

builder.Services.AddHttpClient(nameof(HttpClientEnum.persona), httpClient =>
{
  httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("persona_URL"));
  httpClient.DefaultRequestHeaders.Add("X-Origin", "real_estate_sales");

}).AddPolicyHandler(PollyUtils.GetRetryPolicy());

builder.Services.AddHttpClient(nameof(HttpClientEnum.stock), httpClient =>
{
  httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("stock_URL"));
  httpClient.DefaultRequestHeaders.Add("X-Origin", "real_estate_sales");

}).AddPolicyHandler(PollyUtils.GetRetryPolicy());

//Add all services in the services namespace to be dependency injected correctly
builder.Services.AddServicesFromNamespace(Assembly.GetExecutingAssembly(), "estate_emporium.Services");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
  });
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();
