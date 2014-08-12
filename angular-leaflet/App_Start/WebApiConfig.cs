using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace angular_leaflet
{
  public static class WebApiConfig
  {
    private const string guidPattern = @"^[a-zA-F0-9]{8}(?:-[a-zA-F0-9]{4}){3}-[a-zA-F0-9]{12}$";
    private const string guidOptional = @"^([a-zA-F0-9]{8}(?:-[a-zA-F0-9]{4}){3}-[a-zA-F0-9]{12})?$";

    public static void Register(HttpConfiguration config)
    {
      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          constraints: new { id = guidOptional },
          defaults: new { id = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "MapChildren",
          routeTemplate: "api/map/{mapId}/{controller}/{id}",
          constraints: new { mapId = guidPattern, id = guidOptional },
          defaults: new { id = RouteParameter.Optional }
      );
      // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
      // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
      // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
      //config.EnableQuerySupport();
      config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
      config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

    }
  }
}