using BLL.DTOs;
using BLL.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Weather_Monitoring_API.Controllers
{
    [RoutePrefix("api/cities")]
    public class CitiesController : ApiController
    {
        private readonly CityService service;

        public CitiesController(CityService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            try
            {
                var cities = await service.GetAllAsync();
                return Request.CreateResponse(HttpStatusCode.OK, cities);
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
                var city = await service.GetByIdAsync(id);
                if (city == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = "City not found" });
                return Request.CreateResponse(HttpStatusCode.OK, city);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("state/{stateId:int}")]
        public async Task<HttpResponseMessage> GetByState(int stateId)
        {
            try
            {
                var cities = await service.GetByStateIdAsync(stateId);
                return Request.CreateResponse(HttpStatusCode.OK, cities);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}/areas")]
        public async Task<HttpResponseMessage> GetWithAreas(int id)
        {
            try
            {
                var city = await service.GetWithAreasAsync(id);
                if (city == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = "City not found" });
                return Request.CreateResponse(HttpStatusCode.OK, city);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("capitals")]
        public async Task<HttpResponseMessage> GetCapitals()
        {
            try
            {
                var cities = await service.GetCapitalCitiesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, cities);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("postal/{postalCode}")]
        public async Task<HttpResponseMessage> GetByPostalCode(string postalCode)
        {
            try
            {
                var cities = await service.GetByPostalCodeAsync(postalCode);
                return Request.CreateResponse(HttpStatusCode.OK, cities);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("nearby")]
        public async Task<HttpResponseMessage> GetNearby([FromUri] decimal latitude, [FromUri] decimal longitude, [FromUri] decimal radiusKm = 50)
        {
            try
            {
                if (radiusKm <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = "Radius must be greater than 0" });

                var cities = await service.GetNearbyCitiesAsync(latitude, longitude, radiusKm);
                return Request.CreateResponse(HttpStatusCode.OK, cities);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("search")]
        public async Task<HttpResponseMessage> Search([FromUri] string term)
        {
            try
            {
                var cities = await service.SearchAsync(term);
                return Request.CreateResponse(HttpStatusCode.OK, cities);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Create([FromBody] CityCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                var city = await service.CreateAsync(dto, User?.Identity?.Name);
                return Request.CreateResponse(HttpStatusCode.Created, city);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<HttpResponseMessage> Update(int id, [FromBody] CityUpdateDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = "ID mismatch" });

                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                var city = await service.UpdateAsync(dto, User?.Identity?.Name);
                return Request.CreateResponse(HttpStatusCode.OK, city);
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
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "City deleted successfully" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.Message });
            }
        }
    }
}
