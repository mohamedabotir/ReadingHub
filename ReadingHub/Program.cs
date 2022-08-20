using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ReadingHub.Cores.Services;
using ReadingHub.Persistence;
using ReadingHub.Persistence.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(e =>
{
    e.UseSqlServer(builder.Configuration["ConnectionStrings"]).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}).AddIdentity<User,IdentityRole>()
.AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddSwaggerService();

builder.Services.PlugDIService();

builder.Services.AddControllers();

builder.Services.AddAntiforgery();

builder.Services.AddAuthenticationService(builder.Configuration);

var app = builder.Build();

 

app.UseSwaggerService();


app.UseRouting();

app.UseAuthentication ();
app.UseAuthorization ();
app.UseEndpoints(endpoints => { 
endpoints.MapControllers();
});

app.Run();
