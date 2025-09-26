using BLL.DTOs;
using BLL.Services;
using System;
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
            try
            {
                var data = LocationService.GetAllLocations();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var data = LocationService.GetLocationById(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Location not found");
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(LocationDTO dto)
        {
            try
            {
                var result = LocationService.CreateLocation(dto);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to create location");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(LocationDTO dto)
        {
            try
            {
                var result = LocationService.UpdateLocation(dto);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                return Request.CreateResponse(HttpStatusCode.NotFound, "Location not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = LocationService.DeleteLocation(id);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                return Request.CreateResponse(HttpStatusCode.NotFound, "Location not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [HttpGet]
        [Route("search")]
        public HttpResponseMessage Search([FromUri] string keyword)
        {
            try
            {
                var data = LocationService.SearchLocationsByNameOrCountry(keyword);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        [Route("alerts/active")]
        public HttpResponseMessage GetWithActiveAlerts()
        {
            try
            {
                var data = LocationService.GetLocationsWithActiveAlerts();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("nearby")]
        public HttpResponseMessage GetNearby([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm)
        {
            try
            {
                var data = LocationService.GetNearbyLocations(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("nearestby")]
        public HttpResponseMessage GetNearestLocationWeatherRecords([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm)
        {
            try
            {
                var data = LocationService.GetNearestLocationWeatherRecords(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
