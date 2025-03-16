using Workshops.Application;
using Workshops.Web.Utils;
using Workshops.Web.CrossCutting;
using Workshops.Web.CrossCutting.Options;
using Workshops.Infrastructure.CrossCutting;
using Workshops.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationLayer()
    .AddInfrastructureLayer(builder.Configuration);

builder.Services.AddExceptionHandler<AppExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers(options => options.Conventions.Add(new RoutePrefixConvention("api")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioningExtension();

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwaggerExtension();

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapIdentityGroup();
    
await app.RunAsync();