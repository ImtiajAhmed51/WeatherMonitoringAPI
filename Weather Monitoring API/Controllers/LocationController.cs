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
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Invalid location ID" });

                var data = LocationService.GetLocationById(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { error = "Location not found" });

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(LocationDTO dto)
        {
            try
            {
                if (dto == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Invalid location data" });

                if (!LocationService.IsLocationValid(dto))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Location validation failed" });

                var result = LocationService.CreateLocation(dto);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.Created, new { success = true, message = "Location created successfully" });

                return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Failed to create location. Location may already exist." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(LocationDTO dto)
        {
            try
            {
                if (dto == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Invalid location data" });

                if (!LocationService.IsLocationValid(dto))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Location validation failed" });

                var result = LocationService.UpdateLocation(dto);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Location updated successfully" });

                return Request.CreateResponse(HttpStatusCode.NotFound, new { error = "Location not found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Invalid location ID" });

                var result = LocationService.DeleteLocation(id);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Location deleted successfully" });

                return Request.CreateResponse(HttpStatusCode.NotFound, new { error = "Location not found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }
        [HttpGet]
        [Route("all/alerts")]
        public HttpResponseMessage GetAllWithAlerts()
        {
            try
            {
                var data = LocationService.GetAllLocationsWithAlerts();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("all/weather")]
        public HttpResponseMessage GetAllWithWeather()
        {
            try
            {
                var data = LocationService.GetAllLocationsWithWeather();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}/weather")]
        public HttpResponseMessage GetWithWeather(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Invalid location ID" });

                var data = LocationService.GetLocationWithWeather(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { error = "Location not found" });

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }
        [HttpGet]
        [Route("{id:int}/alerts")]
        public HttpResponseMessage GetWithAlerts(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Invalid location ID" });

                var data = LocationService.GetLocationWithAlerts(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { error = "Location not found" });

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}/all")]
        public HttpResponseMessage GetWithWeatherAndAlerts(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Invalid location ID" });

                var data = LocationService.GetLocationWithWeatherAndAlerts(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { error = "Location not found" });

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}/weather/stats")]
        public HttpResponseMessage GetWithWeatherStats(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Invalid location ID" });

                var data = LocationService.GetLocationWithWeatherStats(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { error = "Location not found or no weather data available" });

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }
        [HttpGet]
        [Route("search")]
        public HttpResponseMessage Search([FromUri] string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Search keyword is required" });

                var data = LocationService.SearchByNameOrCountry(keyword);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("search/name")]
        public HttpResponseMessage SearchByName([FromUri] string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Name is required" });

                var data = LocationService.SearchByName(name);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("search/country")]
        public HttpResponseMessage SearchByCountry([FromUri] string country)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(country))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Country is required" });

                var data = LocationService.SearchByCountry(country);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("find")]
        public HttpResponseMessage FindByNameAndCountry([FromUri] string name, [FromUri] string country)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(country))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Name and country are required" });

                var data = LocationService.GetByNameAndCountry(name, country);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { error = "Location not found" });

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("coordinates")]
        public HttpResponseMessage GetByCoordinates([FromUri] decimal latitude, [FromUri] decimal longitude)
        {
            try
            {
                var data = LocationService.GetByCoordinates(latitude, longitude);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
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
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("alerts/active/basic")]
        public HttpResponseMessage GetWithActiveAlertsBasic()
        {
            try
            {
                var data = LocationService.GetLocationsWithActiveAlertsBasic();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearby")]
        public HttpResponseMessage GetNearby([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Radius must be greater than 0" });

                var data = LocationService.GetNearbyLocations(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearby/alerts")]
        public HttpResponseMessage GetNearbyWithAlerts([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Radius must be greater than 0" });

                var data = LocationService.GetNearbyLocationsWithAlerts(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearby/weather")]
        public HttpResponseMessage GetNearbyWithWeather([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Radius must be greater than 0" });

                var data = LocationService.GetNearbyLocationsWithWeather(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearest")]
        public HttpResponseMessage GetNearest([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Radius must be greater than 0" });

                var data = LocationService.GetNearestLocation(latitude, longitude, radiusKm);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { error = "No location found within radius" });

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearest/alerts")]
        public HttpResponseMessage GetNearestWithAlerts([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Radius must be greater than 0" });

                var data = LocationService.GetNearestLocationWithAlerts(latitude, longitude, radiusKm);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { error = "No location found within radius" });

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearest/weather")]
        public HttpResponseMessage GetNearestWithWeather([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Radius must be greater than 0" });

                var data = LocationService.GetNearestLocationWithWeather(latitude, longitude, radiusKm);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { error = "No location found within radius" });

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearest/weather/stats")]
        public HttpResponseMessage GetNearestWithWeatherStats([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Radius must be greater than 0" });

                var data = LocationService.GetNearestLocationWithWeatherStats(latitude, longitude, radiusKm);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { error = "No location found within radius or no weather data available" });

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearest/all")]
        public HttpResponseMessage GetNearestWithAll([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Radius must be greater than 0" });

                var data = LocationService.GetNearestLocationWithAll(latitude, longitude, radiusKm);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { error = "No location found within radius" });

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearest/current")]
        public HttpResponseMessage GetNearestWithCurrent([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Radius must be greater than 0" });

                var data = LocationService.GetNearestWithActiveAlertsAndLatestWeather(latitude, longitude, radiusKm);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { error = "No location found within radius" });

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("count")]
        public HttpResponseMessage GetCount()
        {
            try
            {
                var count = LocationService.GetTotalLocationCount();
                return Request.CreateResponse(HttpStatusCode.OK, new { count });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("stats/country")]
        public HttpResponseMessage GetCountByCountry()
        {
            try
            {
                var stats = LocationService.GetLocationCountByCountry();
                return Request.CreateResponse(HttpStatusCode.OK, stats);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("exists/{id:int}")]
        public HttpResponseMessage CheckExists(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Invalid location ID" });

                var exists = LocationService.LocationExists(id);
                return Request.CreateResponse(HttpStatusCode.OK, new { exists });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = ex.Message });
            }
        }
    }
}
