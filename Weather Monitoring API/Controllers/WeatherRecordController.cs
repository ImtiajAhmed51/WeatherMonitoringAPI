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
                var data = WeatherRecordService.GetWeatherRecordById(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Weather record not found");
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(WeatherRecordDTO dto)
        {
            try
            {
                var result = WeatherRecordService.CreateWeatherRecord(dto);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to create weather record");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(WeatherRecordDTO dto)
        {
            try
            {
                var result = WeatherRecordService.UpdateWeatherRecord(dto);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                return Request.CreateResponse(HttpStatusCode.NotFound, "Weather record not found");
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
                var result = WeatherRecordService.DeleteWeatherRecord(id);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                return Request.CreateResponse(HttpStatusCode.NotFound, "Weather record not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("location/{locationId:int}")]
        public HttpResponseMessage GetByLocation(int locationId)
        {
            try
            {
                var data = WeatherRecordService.GetWeatherRecordsByLocation(locationId);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("daterange")]
        public HttpResponseMessage GetByDateRange([FromUri] DateTime start, [FromUri] DateTime end)
        {
            try
            {
                var data = WeatherRecordService.GetWeatherRecordsByDateRange(start, end);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("latest")]
        public HttpResponseMessage GetLatest()
        {
            try
            {
                var data = WeatherRecordService.GetLatestWeatherRecords();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("temperature")]
        public HttpResponseMessage GetByTemperature([FromUri] decimal min, [FromUri] decimal max)
        {
            try
            {
                var data = WeatherRecordService.GetWeatherRecordsByTemperature(min, max);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("humidity")]
        public HttpResponseMessage GetByHumidity([FromUri] decimal min, [FromUri] decimal max)
        {
            try
            {
                var data = WeatherRecordService.GetWeatherRecordsByHumidity(min, max);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
