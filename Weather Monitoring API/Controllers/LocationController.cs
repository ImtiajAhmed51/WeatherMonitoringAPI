using BLL.DTOs;
using BLL.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PresentationAPI.Controllers
{
    [RoutePrefix("api/location")]
    public class LocationController : ApiController
    {

        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            var data = LocationService.GetAllLocations();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetById(int id)
        {
            var data = LocationService.GetLocationById(id);
            if (data == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Location not found");
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(LocationDTO dto)
        {
            var result = LocationService.CreateLocation(dto);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to create location");
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(LocationDTO dto)
        {
            var result = LocationService.UpdateLocation(dto);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.NotFound, "Location not found");
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            var result = LocationService.DeleteLocation(id);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.NotFound, "Location not found");
        }


        [HttpGet]
        [Route("country/{country}")]
        public HttpResponseMessage GetByCountry(string country)
        {
            var data = LocationService.GetLocationsByCountry(country);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        [HttpGet]
        [Route("search/{name}")]
        public HttpResponseMessage SearchByName(string name)
        {
            var data = LocationService.SearchLocationsByName(name);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("exists")]
        public HttpResponseMessage CheckExists([FromUri] string name, [FromUri] string country)
        {
            var exists = LocationService.CheckLocationExists(name, country);
            return Request.CreateResponse(HttpStatusCode.OK, exists);
        }


        [HttpGet]
        [Route("alerts/active")]
        public HttpResponseMessage GetWithActiveAlerts()
        {
            var data = LocationService.GetLocationsWithActiveAlerts();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        [HttpGet]
        [Route("nearby")]
        public HttpResponseMessage GetNearby([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm)
        {
            var data = LocationService.GetNearbyLocations(latitude, longitude, radiusKm);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }



        [HttpGet]
        [Route("nearestby")]
        public HttpResponseMessage GetNearestLocationWeatherRecords([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm)
        {
            var data = LocationService.GetNearestLocationWeatherRecords(latitude, longitude, radiusKm);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}
