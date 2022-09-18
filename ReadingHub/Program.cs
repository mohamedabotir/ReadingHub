using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReadingHub.Cores.Services;
using ReadingHub.Persistence;
using ReadingHub.Persistence.Models;


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

builder.Services.AddSignalR();


builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://192.168.1.3:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((x) => true)
                .AllowCredentials();
        });
});

var app = builder.Build();

app.UseStaticFiles();

app.UseSwaggerService();

app.UseRouting();

app.UseCors("_myAllowSpecificOrigins");

app.UseAuthentication ();
app.UseAuthorization ();

app.UseEndpoints(endpoints => {
     endpoints.MapHub<RealTimeCommunicationService>(new PathString("/communicate"));
     endpoints.MapControllers();
    
});

app.Run();
