using Microsoft.EntityFrameworkCore;
using groveale.Endpoints;
using groveale.Data;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderDb>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection")));
builder.Services.AddDbContext<TicketDb>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection")));
builder.Services.AddDbContext<OpportunityDb>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection")));
builder.Services.AddDbContext<AccountDb>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "Customer API";
    config.Title = "CustomerAPI v1";
    config.Version = "v1";
});

// Use Azure Managed Identity for authentication
var credential = new DefaultAzureCredential();
builder.Services.AddSingleton(credential);

var app = builder.Build();

app.UseOpenApi();
app.UseSwaggerUi(config =>
{
    config.DocumentTitle = "CustomerAPI";
    config.Path = "/swagger";
    config.DocumentPath = "/swagger/{documentName}/swagger.json";
    config.DocExpansion = "list";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapOrderEndpoints();
app.MapAccountEndpoints();
app.MapTicketEndpoints();
app.MapOpportunityEndpoints();

app.Run();
