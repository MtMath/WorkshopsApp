using Workshops.Infrastructure.CrossCutting;
using Workshops.Infrastructure.Identity;
using Workshops.Web.CrossCutting;
using Workshops.Web.CrossCutting.Options;
using Workshops.Web.Utils;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RoutePrefixConvention("api"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioningExtension();

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwaggerExtension();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();