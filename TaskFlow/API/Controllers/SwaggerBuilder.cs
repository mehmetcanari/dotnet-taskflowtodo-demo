namespace TaskFlow.API.Controllers;

public class SwaggerBuilder(WebApplicationBuilder builder)
{
    public void Build()
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "TaskFlow API",
                Version = "v1",
                Description = "A simple TaskFlow API for managing todo items"
            });
        });
    }
}