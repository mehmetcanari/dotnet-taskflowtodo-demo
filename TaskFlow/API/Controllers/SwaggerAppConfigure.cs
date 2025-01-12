namespace TaskFlow.API.Controllers;

public class SwaggerAppConfigure
{
    public void Configure(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskFlow API v1");
            c.RoutePrefix = string.Empty;
        });

        app.UseHttpsRedirection();
        app.UseRouting();
    }
}