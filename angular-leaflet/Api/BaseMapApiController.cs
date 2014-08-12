namespace angular_leaflet.Api
{
  using System.Web.Http;
  using Data = DataModel;

  public class BaseMapApiController : ApiController
  {
    protected Data.MapContext GetDb()
    {
      return new Data.MapContext("MapContext");
    }
  }
}
