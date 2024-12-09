using Microsoft.EntityFrameworkCore;
using groveale.Endpoints;
using groveale.Data;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<TicketDb>(opt =>
//     opt.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection")));
// builder.Services.AddDbContext<OrderDb>(opt => 
//     opt.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection")));

// builder.Services.AddDbContext<OpportunityDb>(opt =>
//     opt.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection")));
// builder.Services.AddDbContext<CustomerDb>(opt =>
//     opt.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection")));

builder.Services.AddDbContext<OrderDb>(opt => opt.UseInMemoryDatabase("OrderList"));
builder.Services.AddDbContext<TicketDb>(opt => opt.UseInMemoryDatabase("TicketList"));
builder.Services.AddDbContext<OpportunityDb>(opt => opt.UseInMemoryDatabase("OpportunityList"));
builder.Services.AddDbContext<CustomerDb>(opt => opt.UseInMemoryDatabase("CustomerList"));
builder.Services.AddDbContext<OrderHeaderDb>(opt => opt.UseInMemoryDatabase("OrderHeaderList"));



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "Customer API";
    config.Title = "CustomerAPI v1";
    config.Version = "v1";
});

//Use Azure Managed Identity for authentication
var credential = new DefaultAzureCredential();
builder.Services.AddSingleton(credential);

var app = builder.Build();

// Ensure databases are created
app.EnsureDatabasesCreated();

app.UseOpenApi();
app.UseSwaggerUi(config =>
{
    config.DocumentTitle = "CustomerAPI";
    config.Path = "/swagger";
    config.DocumentPath = "/swagger/{documentName}/swagger.json";
    config.DocExpansion = "list";
});



app.MapCustomerEndpoints();
app.MapTicketEndpoints();
app.MapOpportunityEndpoints();
app.MapOrderEndpoints();

app.Run();
