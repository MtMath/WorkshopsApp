using Asp.Versioning;

namespace Workshops.Web.CrossCutting;

/// <summary>
/// 
/// </summary>
public static class AppExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    public static void UseSwaggerExtension(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var groupName in app.DescribeApiVersions().Select(info => info.GroupName))
            {
                options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
            }
        });
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public static void AddApiVersioningExtension(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1,0);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Version"));
            
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}