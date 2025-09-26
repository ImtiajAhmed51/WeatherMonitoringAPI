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
        [Route("nearby/weather")]
        public HttpResponseMessage GetNearbyWeather([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm)
        {
            try
            {
                var data = LocationService.GetNearbyLocationsWeatherRecord(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("nearby/alert")]
        public HttpResponseMessage GetNearbyAlerts([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm)
        {
            try
            {
                var data = LocationService.GetNearbyLocationsAlert(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("nearby/weatheralert")]
        public HttpResponseMessage GetNearbyWeatherAndAlerts([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm)
        {
            try
            {
                var data = LocationService.GetNearbyLocationsWeatherRecordAndAlert(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // --- Nearest ---
        [HttpGet]
        [Route("nearest")]
        public HttpResponseMessage GetNearest([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm)
        {
            try
            {
                var data = LocationService.GetNearest(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("nearest/weather")]
        public HttpResponseMessage GetNearestWeather([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm)
        {
            try
            {
                var data = LocationService.GetNearestWeatherRecords(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("nearest/alert")]
        public HttpResponseMessage GetNearestAlerts([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm)
        {
            try
            {
                var data = LocationService.GetNearestAlerts(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("nearest/weatheralert")]
        public HttpResponseMessage GetNearestWeatherAndAlerts([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm)
        {
            try
            {
                var data = LocationService.GetNearestWeatherRecordsAlerts(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }





        [HttpGet]
        [Route("{id:int}/weather")]
        public HttpResponseMessage GetWeather(int id)
        {
            try
            {
                var data = LocationService.GetLocationWithWeather(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Location not found");
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}/alert")]
        public HttpResponseMessage GetAlerts(int id)
        {
            try
            {
                var data = LocationService.GetLocationWithAlerts(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Location not found");
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}/weatheralert")]
        public HttpResponseMessage GetWeatherAndAlerts(int id)
        {
            try
            {
                var data = LocationService.GetLocationWithWeatherAndAlerts(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Location not found");
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
