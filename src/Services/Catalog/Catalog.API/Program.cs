var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices(builder.Configuration);

var app = builder.Build();
app.UseCustomServices();

app.Run();