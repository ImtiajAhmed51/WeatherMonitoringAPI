using BLL.DTOs;
using BLL.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Weather_Monitoring_API.Controllers
{
    [RoutePrefix("api/states")]
    public class StatesController : ApiController
    {
        private readonly StateService service;

        public StatesController(StateService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            try
            {
                var states = await service.GetAllAsync();
                return Request.CreateResponse(HttpStatusCode.OK, states);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<HttpResponseMessage> GetById(int id)
        {
            try
            {
                var state = await service.GetByIdAsync(id);
                if (state == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = "State not found" });
                return Request.CreateResponse(HttpStatusCode.OK, state);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("code/{code}")]
        public async Task<HttpResponseMessage> GetByCode(string code)
        {
            try
            {
                var state = await service.GetByCodeAsync(code);
                if (state == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = "State not found" });
                return Request.CreateResponse(HttpStatusCode.OK, state);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("country/{countryId:int}")]
        public async Task<HttpResponseMessage> GetByCountry(int countryId)
        {
            try
            {
                var states = await service.GetByCountryIdAsync(countryId);
                return Request.CreateResponse(HttpStatusCode.OK, states);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}/cities")]
        public async Task<HttpResponseMessage> GetWithCities(int id)
        {
            try
            {
                var state = await service.GetWithCitiesAsync(id);
                if (state == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = "State not found" });
                return Request.CreateResponse(HttpStatusCode.OK, state);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Create([FromBody] StateCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                var state = await service.CreateAsync(dto, User?.Identity?.Name);
                return Request.CreateResponse(HttpStatusCode.Created, state);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<HttpResponseMessage> Update(int id, [FromBody] StateUpdateDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = "ID mismatch" });

                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                var state = await service.UpdateAsync(dto, User?.Identity?.Name);
                return Request.CreateResponse(HttpStatusCode.OK, state);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                await service.DeleteAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "State deleted successfully" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }
    }
}
