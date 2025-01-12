using TaskFlow.API.Controllers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
SwaggerBuilder swaggerBuilder = new(builder);
swaggerBuilder.Build();

WebApplication webApplication = builder.Build();
SwaggerAppConfigure swaggerAppConfigure = new();

swaggerAppConfigure.Configure(webApplication);

ApiEndpoints apiEndpoints = new(webApplication);
apiEndpoints.Initialize();

webApplication.Run();