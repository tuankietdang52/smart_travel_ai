namespace SmartTravelAI.Config
{
    public static class SwaggerConfig
    {
        public static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.CustomOperationIds(apiDescription =>
                {
                    var action = apiDescription.ActionDescriptor.RouteValues["action"];
                    return action;
                });
            });

            return builder;
        }

        public static WebApplication UseSwaggerWithUI(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
