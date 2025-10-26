using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Weather_Monitoring_API.Controllers
{
    [RoutePrefix("api/countries")]
    public class CountriesController : ApiController
    {
        private readonly CountryService service;

        public CountriesController(CountryService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            try
            {
                var countries = await service.GetAllAsync();
                return Request.CreateResponse(HttpStatusCode.OK, countries);
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
                var country = await service.GetByIdAsync(id);
                if (country == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = "Country not found" });
                return Request.CreateResponse(HttpStatusCode.OK, country);
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
                var country = await service.GetByCodeAsync(code);
                if (country == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = "Country not found" });
                return Request.CreateResponse(HttpStatusCode.OK, country);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}/states")]
        public async Task<HttpResponseMessage> GetWithStates(int id)
        {
            try
            {
                var country = await service.GetWithStatesAsync(id);
                if (country == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = "Country not found" });
                return Request.CreateResponse(HttpStatusCode.OK, country);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("search")]
        public async Task<HttpResponseMessage> Search([FromUri] string term)
        {
            try
            {
                var countries = await service.SearchAsync(term);
                return Request.CreateResponse(HttpStatusCode.OK, countries);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Create([FromBody] CountryCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                var country = await service.CreateAsync(dto, User?.Identity?.Name);
                return Request.CreateResponse(HttpStatusCode.Created, country);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<HttpResponseMessage> Update(int id, [FromBody] CountryUpdateDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = "ID mismatch" });

                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                var country = await service.UpdateAsync(dto, User?.Identity?.Name);
                return Request.CreateResponse(HttpStatusCode.OK, country);
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
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Country deleted successfully" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }
    }
}
