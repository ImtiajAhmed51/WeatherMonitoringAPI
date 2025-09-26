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

    [RoutePrefix("api/alert")]
    public class AlertController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            var data = AlertService.GetAllAlerts();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetById(int id)
        {
            var data = AlertService.GetAlertById(id);
            if (data == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Alert not found");
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(AlertDTO dto)
        {
            var result = AlertService.CreateAlert(dto);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, "Alert created successfully");
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to create alert");
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(AlertDTO dto)
        {
            var result = AlertService.UpdateAlert(dto);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, "Alert updated successfully");
            return Request.CreateResponse(HttpStatusCode.NotFound, "Alert not found");
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            var result = AlertService.DeleteAlert(id);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, "Alert deleted successfully");
            return Request.CreateResponse(HttpStatusCode.NotFound, "Alert not found");
        }


    }
}
