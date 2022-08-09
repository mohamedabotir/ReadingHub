using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReadingHub.Cores.Services;
using ReadingHub.Persistence;
using ReadingHub.Persistence.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(e =>
{
    e.UseSqlServer(builder.Configuration["ConnectionStrings"]);
}).AddIdentity<User,IdentityRole>()
.AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddSwaggerService();

builder.Services.PlugDIService();

builder.Services.AddControllers();
var app = builder.Build();

app.UseSwaggerService();


app.UseRouting();
app.UseEndpoints(endpoints => { 
endpoints.MapControllers();
});

app.Run();
