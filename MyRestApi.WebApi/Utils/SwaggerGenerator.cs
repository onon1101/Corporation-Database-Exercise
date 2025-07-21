using System.ComponentModel;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Utils;

public class SwaggerGenerator
{
    static public void Init(SwaggerGenOptions options)
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }
}