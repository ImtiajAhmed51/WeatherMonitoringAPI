using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
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
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var data = LocationService.GetLocationById(id);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "Location not found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(LocationDTO dto)
        {
            try
            {
                if (dto == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location data" });

                if (!LocationService.IsLocationValid(dto))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Location validation failed" });

                var result = LocationService.CreateLocation(dto);
                return result
                    ? Request.CreateResponse(HttpStatusCode.Created, new { success = true, message = "Location created successfully" })
                    : Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Failed to create location. Location may already exist." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(LocationDTO dto)
        {
            try
            {
                if (dto == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location data" });

                if (!LocationService.IsLocationValid(dto))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Location validation failed" });

                var result = LocationService.UpdateLocation(dto);
                return result
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Location updated successfully" })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "Location not found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                var result = LocationService.DeleteLocation(id);
                return result
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Location deleted successfully" })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "Location not found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("all/alerts")]
        public HttpResponseMessage GetAllWithAlerts()
        {
            try
            {
                var data = LocationService.GetAllLocationsWithAlerts();
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("all/weather")]
        public HttpResponseMessage GetAllWithWeather()
        {
            try
            {
                var data = LocationService.GetAllLocationsWithWeather();
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}/weather")]
        public HttpResponseMessage GetWithWeather(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                var data = LocationService.GetLocationWithWeather(id);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "Location not found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}/alerts")]
        public HttpResponseMessage GetWithAlerts(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                var data = LocationService.GetLocationWithAlerts(id);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "Location not found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}/all")]
        public HttpResponseMessage GetWithWeatherAndAlerts(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                var data = LocationService.GetLocationWithWeatherAndAlerts(id);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "Location not found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}/weather/stats")]
        public HttpResponseMessage GetWithWeatherStats(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                var data = LocationService.GetLocationWithWeatherStats(id);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "Location not found or no weather data available" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("search")]
        public HttpResponseMessage Search([FromUri] string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Search keyword is required" });

                var data = LocationService.SearchByNameOrCountry(keyword);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("search/name")]
        public HttpResponseMessage SearchByName([FromUri] string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Name is required" });

                var data = LocationService.SearchByName(name);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("search/country")]
        public HttpResponseMessage SearchByCountry([FromUri] string country)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(country))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Country is required" });

                var data = LocationService.SearchByCountry(country);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("find")]
        public HttpResponseMessage FindByNameAndCountry([FromUri] string name, [FromUri] string country)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(country))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Name and country are required" });

                var data = LocationService.GetByNameAndCountry(name, country);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "Location not found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("coordinates")]
        public HttpResponseMessage GetByCoordinates([FromUri] decimal latitude, [FromUri] decimal longitude)
        {
            try
            {
                var data = LocationService.GetByCoordinates(latitude, longitude);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("alerts/active")]
        public HttpResponseMessage GetWithActiveAlerts()
        {
            try
            {
                var data = LocationService.GetLocationsWithActiveAlerts();
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("alerts/active/basic")]
        public HttpResponseMessage GetWithActiveAlertsBasic()
        {
            try
            {
                var data = LocationService.GetLocationsWithActiveAlertsBasic();
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearby")]
        public HttpResponseMessage GetNearby([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Radius must be greater than 0" });

                var data = LocationService.GetNearbyLocations(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearby/alerts")]
        public HttpResponseMessage GetNearbyWithAlerts([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Radius must be greater than 0" });

                var data = LocationService.GetNearbyLocationsWithAlerts(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearby/weather")]
        public HttpResponseMessage GetNearbyWithWeather([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Radius must be greater than 0" });

                var data = LocationService.GetNearbyLocationsWithWeather(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearest")]
        public HttpResponseMessage GetNearest([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Radius must be greater than 0" });

                var data = LocationService.GetNearestLocation(latitude, longitude, radiusKm);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "No location found within radius" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearest/alerts")]
        public HttpResponseMessage GetNearestWithAlerts([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Radius must be greater than 0" });

                var data = LocationService.GetNearestLocationWithAlerts(latitude, longitude, radiusKm);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "No location found within radius" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearest/weather")]
        public HttpResponseMessage GetNearestWithWeather([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Radius must be greater than 0" });

                var data = LocationService.GetNearestLocationWithWeather(latitude, longitude, radiusKm);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "No location found within radius" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearest/weather/stats")]
        public HttpResponseMessage GetNearestWithWeatherStats([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Radius must be greater than 0" });

                var data = LocationService.GetNearestLocationWithWeatherStats(latitude, longitude, radiusKm);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "No location found within radius or no weather data available" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearest/all")]
        public HttpResponseMessage GetNearestWithAll([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Radius must be greater than 0" });

                var data = LocationService.GetNearestLocationWithAll(latitude, longitude, radiusKm);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "No location found within radius" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearest/current")]
        public HttpResponseMessage GetNearestWithCurrent([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] double radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Radius must be greater than 0" });

                var data = LocationService.GetNearestWithActiveAlertsAndLatestWeather(latitude, longitude, radiusKm);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "No location found within radius" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("count")]
        public HttpResponseMessage GetCount()
        {
            try
            {
                var count = LocationService.GetTotalLocationCount();
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, count });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("stats/country")]
        public HttpResponseMessage GetCountByCountry()
        {
            try
            {
                var stats = LocationService.GetLocationCountByCountry();
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data = stats });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("exists/{id:int}")]
        public HttpResponseMessage CheckExists(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                var exists = LocationService.LocationExists(id);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, exists });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }
    }
}
