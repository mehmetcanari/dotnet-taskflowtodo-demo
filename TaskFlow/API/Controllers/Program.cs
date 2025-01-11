using TaskFlow.API.Controllers;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

ApiEndpoints apiEndpoints = new(app);
apiEndpoints.Initialize();

app.Run();