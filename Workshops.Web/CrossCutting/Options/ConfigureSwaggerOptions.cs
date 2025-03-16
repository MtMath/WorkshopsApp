using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Workshops.Web.CrossCutting.Options;

public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IOptions<ApiVersioningOptions> options)
    : IConfigureOptions<SwaggerGenOptions>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var version in provider.ApiVersionDescriptions)
        {
            var apiInfo = new OpenApiInfo
            {
                Title = "Workshops API",
                Version = version.ApiVersion.ToString(),
                Description =
                    "An API designed to manage software development workshops that occur quarterly on Thursdays from 4-5pm. " +
                    "While attendance is optional, these workshops provide valuable opportunities for team members to learn in a relaxed environment " +
                    "outside their regular routine. This API supports the workshop organizing committee's need for deeper insights by " +
                    "providing comprehensive workshop details, attendance records, and participant analytics through a web interface. " +
                    "The data collected helps drive continuous improvement of our professional development initiatives.",
                Contact = new OpenApiContact
                {
                    Name = "Matheus Costa",
                    Email = "matheus.design12@gmail.com",
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT"),
                },
            };

            options.SwaggerDoc(version.GroupName, apiInfo);
            
            //Bearer token authentication
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description =
                    "JWT Authorization header using the Bearer scheme.\r\n\r\n" +
                    "Enter 'Bearer' [space] followed by your valid token in the field below.\r\n\r\n" +
                    "Example: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...\"",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    Array.Empty<string>()
                }
            });

            //options.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\Workshops.Web.xml");
        }
    }
}