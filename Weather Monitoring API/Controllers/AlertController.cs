using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
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
                    return Request.CreateResponse(HttpStatusCode.NotFound,"Alert not found");
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpGet]
        [Route("all/location")]
        public HttpResponseMessage GetAllWithLocation()
        {
            try
            {
                var data = AlertService.GetAllAlertsWithLocations();
                return Request.CreateResponse(HttpStatusCode.OK, data ?? new List<AlertWithLocationDTO>());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("active/location")]
        public HttpResponseMessage GetActiveWithLocation()
        {
            try
            {
                var data = AlertService.GetActiveAlertsWithLocations();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("location/{locationId:int}")]
        public HttpResponseMessage GetByLocation(int locationId)
        {
            try
            {
                var data = AlertService.GetAlertsByLocation(locationId);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("location/{locationId:int}/details")]
        public HttpResponseMessage GetByLocationWithDetails(int locationId)
        {
            try
            {
                var data = AlertService.GetAlertsByLocationWithDetails(locationId);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("location/{locationId:int}/count")]
        public HttpResponseMessage GetLocationCount(int locationId)
        {
            try
            {
                var count = AlertService.GetAlertCountByLocation(locationId);
                return Request.CreateResponse(HttpStatusCode.OK, new { locationId, count });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpGet]
        [Route("expired")]
        public HttpResponseMessage GetExpired()
        {
            try
            {
                var data = AlertService.GetExpiredAlerts();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("expiration")]
        public HttpResponseMessage GetByExpiration([FromUri] DateTime startDate, [FromUri] DateTime endDate)
        {
            try
            {
                var data = AlertService.GetAlertsByExpiration(startDate, endDate);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("date-range")]
        public HttpResponseMessage GetByDateRange([FromUri] DateTime startDate, [FromUri] DateTime endDate)
        {
            try
            {
                var data = AlertService.GetAlertsByDateRange(startDate, endDate);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("recentwithlocation")]
        public HttpResponseMessage GetRecentWithLocation([FromUri] int days = 7)
        {
            try
            {
                var data = AlertService.GetRecentAlertsWithLocation(days);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("severity/{severity}")]
        public HttpResponseMessage GetBySeverity(string severity)
        {
            try
            {
                var data = AlertService.GetAlertsBySeverity(severity);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpGet]
        [Route("total/count")]
        public HttpResponseMessage GetTotalCount()
        {
            try
            {
                var count = AlertService.GetTotalAlertsCount();
                return Request.CreateResponse(HttpStatusCode.OK, new { total = count });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("total/activecount")]
        public HttpResponseMessage GetTotalActiveCount()
        {
            try
            {
                var count = AlertService.GetTotalActiveAlertsCount();
                return Request.CreateResponse(HttpStatusCode.OK, new { active = count });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("stats/severity")]
        public HttpResponseMessage GetStatsBySeverity()
        {
            try
            {
                var dict = AlertService.GetAlertStatsBySeverity();
                return Request.CreateResponse(HttpStatusCode.OK, dict);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("stats/location")]
        public HttpResponseMessage GetStatsByLocation()
        {
            try
            {
                var dict = AlertService.GetAlertStatsByLocation();
                return Request.CreateResponse(HttpStatusCode.OK, dict);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(AlertDTO dto)
        {
            try
            {
                if (dto == null) 
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Payload is required.");
                var data = AlertService.CreateAlert(dto);
                return data? Request.CreateResponse(HttpStatusCode.OK, "Alert created successfully"):Request.CreateResponse(HttpStatusCode.BadRequest,"Failed to create alert");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(AlertDTO dto)
        {
            try
            {
                if (dto == null) 
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Payload is required.");
                var data = AlertService.UpdateAlert(dto); 
                return data? Request.CreateResponse(HttpStatusCode.OK, "Alert updated successfully"):Request.CreateResponse(HttpStatusCode.NotFound, "Alert not found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var data = AlertService.DeleteAlert(id);
                return data? Request.CreateResponse(HttpStatusCode.OK,"Alert deleted successfully"):Request.CreateResponse(HttpStatusCode.NotFound,"Alert not found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpPut]
        [Route("{id:int}/activate")]
        public HttpResponseMessage Activate(int id)
        {
            try
            {
                var data = AlertService.ActivateAlert(id);
                return data?Request.CreateResponse(HttpStatusCode.OK,"Alert activated"):Request.CreateResponse(HttpStatusCode.NotFound,"Alert not found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,ex);
            }
        }

        [HttpPut]
        [Route("{id:int}/deactivate")]
        public HttpResponseMessage Deactivate(int id)
        {
            try
            {
                var data = AlertService.DeactivateAlert(id);
                return data? Request.CreateResponse(HttpStatusCode.OK,"Alert deactivated"):Request.CreateResponse(HttpStatusCode.NotFound,"Alert not found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,ex);
            }
        }
    }
}
