using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var data = WeatherRecordService.GetAllWeatherRecords();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        // GET: api/weather/{id}
        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetById(int id)
        {
            var data = WeatherRecordService.GetWeatherRecordById(id);
            if (data == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Weather record not found");
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        // POST: api/weather/create
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(WeatherRecordDTO dto)
        {
            var result = WeatherRecordService.CreateWeatherRecord(dto);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to create weather record");
        }

        // PUT: api/weather/update
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(WeatherRecordDTO dto)
        {
            var result = WeatherRecordService.UpdateWeatherRecord(dto);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.NotFound, "Weather record not found");
        }

        // DELETE: api/weather/delete/{id}
        [HttpDelete]
        [Route("delete/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            var result = WeatherRecordService.DeleteWeatherRecord(id);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.NotFound, "Weather record not found");
        }

        // GET: api/weather/location/{locationId}
        [HttpGet]
        [Route("location/{locationId:int}")]
        public HttpResponseMessage GetByLocation(int locationId)
        {
            var data = WeatherRecordService.GetWeatherRecordsByLocation(locationId);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        // GET: api/weather/daterange?start=yyyy-MM-dd&end=yyyy-MM-dd
        [HttpGet]
        [Route("daterange")]
        public HttpResponseMessage GetByDateRange([FromUri] DateTime start, [FromUri] DateTime end)
        {
            var data = WeatherRecordService.GetWeatherRecordsByDateRange(start, end);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        // GET: api/weather/latest
        [HttpGet]
        [Route("latest")]
        public HttpResponseMessage GetLatest()
        {
            var data = WeatherRecordService.GetLatestWeatherRecords();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}
