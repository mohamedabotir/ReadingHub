namespace ReadingHub.Cores.Services
{
    public static  class ApplicationBuilder
    {
        public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder builder) {
            builder.UseSwagger();
            builder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            return builder;
        }
    }
}
