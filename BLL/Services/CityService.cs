using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.Factory;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CityService:IDisposable
    {
        private readonly IRepoFacade facade;
        private readonly IMapper mapper;

        public CityService(IMapper mapper)
        {
            facade = DataAccessFactory.Instance.CreateFacade();
            this.mapper = mapper;
        }

        public async Task<CityDTO> CreateAsync(CityCreateDTO dto, string createdBy = null)
        {
            var state = await facade.GetStateWithCountryAsync(dto.StateId);
            if (state == null)
            {
                throw new Exception("State not found");
            }

            var city = mapper.Map<City>(dto);
            city.CreatedBy = createdBy;

            city = await facade.CreateCityAsync(city);
            await facade.SaveChangesAsync();

            var result = mapper.Map<CityDTO>(city);
            result.StateName = state.Name;
            result.CountryName = state.Country?.Name;

            return result;
        }

        public async Task<CityDTO> UpdateAsync(CityUpdateDTO dto, string updatedBy = null)
        {
            var existing = await facade.GetCityByIdAsync(dto.Id);
            if (existing == null)
            {
                throw new Exception("City not found");
            }

            mapper.Map(dto, existing);
            existing.UpdatedBy = updatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await facade.UpdateCityAsync(existing);
            await facade.SaveChangesAsync();

            var cityWithRelations = await facade.GetCityWithStateAndCountryAsync(updated.Id);
            return mapper.Map<CityDTO>(cityWithRelations);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await facade.DeleteCityAsync(id);
            if (!result)
            {
                throw new Exception("City not found");
            }

            await facade.SaveChangesAsync();
            return true;
        }

        public async Task<CityDTO> GetByIdAsync(int id)
        {
            var city = await facade.GetCityWithStateAndCountryAsync(id);
            if (city == null)
            {
                throw new Exception("City not found");
            }

            return mapper.Map<CityDTO>(city);
        }

        public async Task<List<CityDTO>> GetAllAsync()
        {
            var cities = await facade.GetAllCitiesAsync();
            return mapper.Map<List<CityDTO>>(cities);
        }

        public async Task<List<CityDTO>> GetByStateIdAsync(int stateId)
        {
            var cities = await facade.GetCitiesByStateAsync(stateId);
            var state = await facade.GetStateWithCountryAsync(stateId);

            var dtos = mapper.Map<List<CityDTO>>(cities);
            if (state != null)
            {
                dtos.ForEach(d =>
                {
                    d.StateName = state.Name;
                    d.CountryName = state.Country?.Name;
                });
            }

            return dtos;
        }

        public async Task<List<CityDTO>> GetCapitalCitiesAsync()
        {
            var cities = await facade.GetCapitalCitiesAsync();
            return mapper.Map<List<CityDTO>>(cities);
        }

        public async Task<List<CityDTO>> GetByPostalCodeAsync(string postalCode)
        {
            var cities = await facade.GetCitiesByPostalCodeAsync(postalCode);
            return mapper.Map<List<CityDTO>>(cities);
        }

        public async Task<List<CityDTO>> GetNearbyCitiesAsync(decimal latitude, decimal longitude, decimal radiusKm)
        {
            var cities = await facade.GetNearbyCitiesAsync(latitude, longitude, radiusKm);
            return mapper.Map<List<CityDTO>>(cities);
        }

        public async Task<List<CityDTO>> SearchAsync(string searchTerm)
        {
            var cities = await facade.SearchCitiesAsync(searchTerm);
            return mapper.Map<List<CityDTO>>(cities);
        }

        public void Dispose()
        {
            facade?.Dispose();
        }
    }
}
