using Azure.Identity;
using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHostedService<WeatherdataService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var final = builder.Configuration.GetSection("ConnectionDomain");
var queueName = "weatherdata";
builder.Services.AddAzureClients(builder =>
{


    builder.AddClient<QueueClient, QueueClientOptions>((_, _, _) =>
    {
        return new QueueClient(final.Value, queueName);
    });
    //builder.AddServiceBusClient(connectionString);  // Removed this since it is not needed for managed Identitu
});

var app = builder.Build();

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
