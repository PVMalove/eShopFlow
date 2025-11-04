using System.Data;
using Promotion.GRPS.Extensions;
using Promotion.GRPS.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices(builder.Configuration);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbConnection = scope.ServiceProvider.GetRequiredService<IDbConnection>();
    await InitializeDatabase.SeedAsync(dbConnection);
}

app.Run();