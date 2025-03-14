using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Workshops.Web.Utils;

public class RoutePrefixConvention(string prefix) : IControllerModelConvention
{
    private readonly AttributeRouteModel _routePrefix = new(new RouteAttribute(prefix));

    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
        {
            var existingRoute = selector.AttributeRouteModel;
            selector.AttributeRouteModel = existingRoute != null
                ? AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, existingRoute)
                : _routePrefix;
        }
    }
}