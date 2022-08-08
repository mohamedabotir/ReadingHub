using Microsoft.EntityFrameworkCore;
using ReadingHub.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(e => {
    e.UseSqlServer(builder.Configuration["ConnectionStrings"]);
});
builder.Services.AddSwaggerGen(op =>
{
   
});
builder.Services.AddControllers();
var app = builder.Build();

app.UseSwagger(o =>
{
    
});
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


app.UseRouting();
app.UseEndpoints(endpoints => { 
endpoints.MapControllers();
});

app.Run();
