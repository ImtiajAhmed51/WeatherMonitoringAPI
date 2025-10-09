using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Weather_Monitoring_API.Controllers
{
    [RoutePrefix("api/weather")]
    public class WeatherRecordController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = WeatherRecordService.GetAllWeatherRecords();
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
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid weather record ID" });

                var data = WeatherRecordService.GetWeatherRecordById(id);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "Weather record not found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(WeatherRecordDTO dto)
        {
            try
            {
                if (dto == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid weather record data" });

                if (!WeatherRecordService.IsWeatherRecordValid(dto))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Weather record validation failed" });

                var result = WeatherRecordService.CreateWeatherRecord(dto);
                return result
                    ? Request.CreateResponse(HttpStatusCode.Created, new { success = true, message = "Weather record created successfully" })
                    : Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Failed to create weather record. Record may already exist." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(WeatherRecordDTO dto)
        {
            try
            {
                if (dto == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid weather record data" });

                if (!WeatherRecordService.IsWeatherRecordValid(dto))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Weather record validation failed" });

                var result = WeatherRecordService.UpdateWeatherRecord(dto);
                return result
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Weather record updated successfully" })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "Weather record not found" });
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
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid weather record ID" });

                var result = WeatherRecordService.DeleteWeatherRecord(id);
                return result
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Weather record deleted successfully" })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "Weather record not found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("all/locations")]
        public HttpResponseMessage GetAllWithLocations()
        {
            try
            {
                var data = WeatherRecordService.GetAllWeatherRecordsWithLocations();
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}/location")]
        public HttpResponseMessage GetWithLocation(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid weather record ID" });

                var data = WeatherRecordService.GetWeatherRecordWithLocation(id);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "Weather record not found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("location/{locationId:int}")]
        public HttpResponseMessage GetByLocation(int locationId)
        {
            try
            {
                if (locationId <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                var data = WeatherRecordService.GetWeatherRecordsByLocation(locationId);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("location/{locationId:int}/details")]
        public HttpResponseMessage GetByLocationWithDetails(int locationId)
        {
            try
            {
                if (locationId <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                var data = WeatherRecordService.GetWeatherRecordsByLocationWithDetails(locationId);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("location/{locationId:int}/latest")]
        public HttpResponseMessage GetLatestByLocation(int locationId)
        {
            try
            {
                if (locationId <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                var data = WeatherRecordService.GetLatestWeatherRecordByLocation(locationId);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "No weather records found for this location" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("location/{locationId:int}/latest/details")]
        public HttpResponseMessage GetLatestByLocationWithDetails(int locationId)
        {
            try
            {
                if (locationId <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                var data = WeatherRecordService.GetLatestWeatherRecordByLocationWithDetails(locationId);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "No weather records found for this location" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("location/{locationId:int}/stats")]
        public HttpResponseMessage GetStatsByLocation(int locationId)
        {
            try
            {
                if (locationId <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                var data = WeatherRecordService.GetWeatherStatsByLocation(locationId);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "No weather records found for this location" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("daterange")]
        public HttpResponseMessage GetByDateRange([FromUri] DateTime start, [FromUri] DateTime end)
        {
            try
            {
                if (start > end)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Start date must be before end date" });

                var data = WeatherRecordService.GetWeatherRecordsByDateRange(start, end);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("daterange/locations")]
        public HttpResponseMessage GetByDateRangeWithLocations([FromUri] DateTime start, [FromUri] DateTime end)
        {
            try
            {
                if (start > end)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Start date must be before end date" });

                var data = WeatherRecordService.GetWeatherRecordsByDateRangeWithLocations(start, end);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("location/{locationId:int}/daterange")]
        public HttpResponseMessage GetByLocationAndDateRange(int locationId, [FromUri] DateTime start, [FromUri] DateTime end)
        {
            try
            {
                if (locationId <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                if (start > end)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Start date must be before end date" });

                var data = WeatherRecordService.GetWeatherRecordsByLocationAndDateRange(locationId, start, end);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("location/{locationId:int}/daterange/stats")]
        public HttpResponseMessage GetStatsByLocationAndDateRange(int locationId, [FromUri] DateTime start, [FromUri] DateTime end)
        {
            try
            {
                if (locationId <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                if (start > end)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Start date must be before end date" });

                var data = WeatherRecordService.GetWeatherStatsByLocationAndDateRange(locationId, start, end);
                return data != null
                    ? Request.CreateResponse(HttpStatusCode.OK, new { success = true, data })
                    : Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "No weather records found for this location and date range" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("after")]
        public HttpResponseMessage GetAfterDate([FromUri] DateTime date)
        {
            try
            {
                var data = WeatherRecordService.GetWeatherRecordsAfter(date);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("before")]
        public HttpResponseMessage GetBeforeDate([FromUri] DateTime date)
        {
            try
            {
                var data = WeatherRecordService.GetWeatherRecordsBefore(date);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("recent")]
        public HttpResponseMessage GetRecent([FromUri] int count = 10)
        {
            try
            {
                if (count <= 0 || count > 100)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Count must be between 1 and 100" });

                var data = WeatherRecordService.GetRecentWeatherRecords(count);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("recent/locations")]
        public HttpResponseMessage GetRecentWithLocations([FromUri] int count = 10)
        {
            try
            {
                if (count <= 0 || count > 100)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Count must be between 1 and 100" });

                var data = WeatherRecordService.GetRecentWeatherRecordsWithLocations(count);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("location/{locationId:int}/recent")]
        public HttpResponseMessage GetRecentByLocation(int locationId, [FromUri] int count = 10)
        {
            try
            {
                if (locationId <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                if (count <= 0 || count > 100)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Count must be between 1 and 100" });

                var data = WeatherRecordService.GetRecentWeatherRecordsByLocation(locationId, count);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("latest")]
        public HttpResponseMessage GetLatestForAllLocations()
        {
            try
            {
                var data = WeatherRecordService.GetLatestWeatherRecordsForAllLocations();
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("temperature")]
        public HttpResponseMessage GetByTemperature([FromUri] decimal min, [FromUri] decimal max)
        {
            try
            {
                if (min > max)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Minimum temperature must be less than or equal to maximum temperature" });

                var data = WeatherRecordService.GetWeatherRecordsByTemperature(min, max);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("temperature/locations")]
        public HttpResponseMessage GetByTemperatureWithLocations([FromUri] decimal min, [FromUri] decimal max)
        {
            try
            {
                if (min > max)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Minimum temperature must be less than or equal to maximum temperature" });

                var data = WeatherRecordService.GetWeatherRecordsByTemperatureWithLocations(min, max);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("humidity")]
        public HttpResponseMessage GetByHumidity([FromUri] decimal min, [FromUri] decimal max)
        {
            try
            {
                if (min < 0 || max > 100 || min > max)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid humidity range. Must be between 0-100 and min <= max" });

                var data = WeatherRecordService.GetWeatherRecordsByHumidity(min, max);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("humidity/locations")]
        public HttpResponseMessage GetByHumidityWithLocations([FromUri] decimal min, [FromUri] decimal max)
        {
            try
            {
                if (min < 0 || max > 100 || min > max)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid humidity range. Must be between 0-100 and min <= max" });

                var data = WeatherRecordService.GetWeatherRecordsByHumidityWithLocations(min, max);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("precipitation")]
        public HttpResponseMessage GetByPrecipitation([FromUri] decimal min, [FromUri] decimal max)
        {
            try
            {
                if (min < 0 || min > max)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid precipitation range" });

                var data = WeatherRecordService.GetWeatherRecordsByPrecipitation(min, max);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("windspeed")]
        public HttpResponseMessage GetByWindSpeed([FromUri] decimal min, [FromUri] decimal max)
        {
            try
            {
                if (min < 0 || min > max)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid wind speed range" });

                var data = WeatherRecordService.GetWeatherRecordsByWindSpeed(min, max);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
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
                var count = WeatherRecordService.GetTotalRecordCount();
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, count });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("location/{locationId:int}/count")]
        public HttpResponseMessage GetCountByLocation(int locationId)
        {
            try
            {
                if (locationId <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                var count = WeatherRecordService.GetRecordCountByLocation(locationId);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data = new { locationId, count } });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("stats/bylocation")]
        public HttpResponseMessage GetCountsByLocation()
        {
            try
            {
                var data = WeatherRecordService.GetRecordCountsByLocation();
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("location/{locationId:int}/dates")]
        public HttpResponseMessage GetRecordDates(int locationId)
        {
            try
            {
                if (locationId <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid location ID" });

                var firstDate = WeatherRecordService.GetFirstRecordDate(locationId);
                var lastDate = WeatherRecordService.GetLastRecordDate(locationId);

                if (firstDate == null || lastDate == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { success = false, message = "No weather records found for this location" });

                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data = new { firstDate, lastDate } });
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
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Invalid weather record ID" });

                var exists = WeatherRecordService.WeatherRecordExists(id);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, exists });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }
    }
}
