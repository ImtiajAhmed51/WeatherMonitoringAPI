using BLL.DTOs;
using BLL.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Weather_Monitoring_API.Controllers
{
    [RoutePrefix("api/alert")]
    public class AlertController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = AlertService.GetAllAlerts();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("all/location")]
        public HttpResponseMessage GetAllWithLocation()
        {
            try
            {
                var data = AlertService.GetAlertsWithLocation();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("active")]
        public HttpResponseMessage GetActive()
        {
            try
            {
                var data = AlertService.GetActiveAlerts();
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
                var data = AlertService.GetAlertById(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Alert not found");
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id:int}/location")]
        public HttpResponseMessage GetByIdWithLocation(int id)
        {
            try
            {
                var data = AlertService.GetAlertWithLocationById(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Alert not found");
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [HttpGet]
        [Route("location/{locationId:int}")]
        public HttpResponseMessage GetByLocation(int locationId, [FromUri] bool onlyActive = true)
        {
            try
            {
                var data = AlertService.GetAlertsByLocation(locationId, onlyActive);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("expiringsoon")]
        public HttpResponseMessage GetExpiringSoon([FromUri] double hours = 6)
        {
            try
            {
                var data = AlertService.GetAlertsExpiringSoon(hours);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(AlertDTO dto)
        {
            try
            {
                var result = AlertService.CreateAlert(dto);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, "Alert created successfully");
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to create alert");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(AlertDTO dto)
        {
            try
            {
                var result = AlertService.UpdateAlert(dto);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, "Alert updated successfully");
                return Request.CreateResponse(HttpStatusCode.NotFound, "Alert not found");
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
                var result = AlertService.DeleteAlert(id);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, "Alert deleted successfully");
                return Request.CreateResponse(HttpStatusCode.NotFound, "Alert not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("deactivateall")]
        public HttpResponseMessage DeactivateAll()
        {
            try
            {
                var count = AlertService.DeactivateAllAlerts();
                return Request.CreateResponse(HttpStatusCode.OK, $"{count} alert(s) deactivated successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
