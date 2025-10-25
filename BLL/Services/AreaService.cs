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
    public class AreaService:IDisposable
    {
        private readonly IRepoFacade facade;
        private readonly IMapper mapper;

        public AreaService(IMapper mapper)
        {
            facade = DataAccessFactory.Instance.CreateFacade();
            this.mapper = mapper;
        }

        public async Task<AreaDTO> CreateAsync(AreaCreateDTO dto, string createdBy = null)
        {
            var city = await facade.GetCityWithStateAndCountryAsync(dto.CityId);
            if (city == null)
            {
                throw new Exception("City not found");
            }

            var area = mapper.Map<Area>(dto);
            area.CreatedBy = createdBy;

            area = await facade.CreateAreaAsync(area);
            await facade.SaveChangesAsync();

            var result = mapper.Map<AreaDTO>(area);
            result.CityName = city.Name;
            result.StateName = city.State?.Name;
            result.CountryName = city.State?.Country?.Name;

            return result;
        }

        public async Task<AreaDTO> UpdateAsync(AreaUpdateDTO dto, string updatedBy = null)
        {
            var existing = await facade.GetAreaByIdAsync(dto.Id);
            if (existing == null)
            {
                throw new Exception("Area not found");
            }

            mapper.Map(dto, existing);
            existing.UpdatedBy = updatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await facade.UpdateAreaAsync(existing);
            await facade.SaveChangesAsync();

            var areaWithRelations = await facade.GetAreaWithAllRelationsAsync(updated.Id);
            return mapper.Map<AreaDTO>(areaWithRelations);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await facade.DeleteAreaAsync(id);
            if (!result)
            {
                throw new Exception("Area not found");
            }

            await facade.SaveChangesAsync();
            return true;
        }

        public async Task<AreaDTO> GetByIdAsync(int id)
        {
            var area = await facade.GetAreaWithAllRelationsAsync(id);
            if (area == null)
            {
                throw new Exception("Area not found");
            }

            return mapper.Map<AreaDTO>(area);
        }

        public async Task<List<AreaDTO>> GetAllAsync()
        {
            var areas = await facade.GetAllAreasAsync();
            return mapper.Map<List<AreaDTO>>(areas);
        }

        public async Task<List<AreaDTO>> GetByCityIdAsync(int cityId)
        {
            var areas = await facade.GetAreasByCityAsync(cityId);
            var city = await facade.GetCityWithStateAndCountryAsync(cityId);

            var dtos = mapper.Map<List<AreaDTO>>(areas);
            if (city != null)
            {
                dtos.ForEach(d =>
                {
                    d.CityName = city.Name;
                    d.StateName = city.State?.Name;
                    d.CountryName = city.State?.Country?.Name;
                });
            }

            return dtos;
        }

        public async Task<List<AreaDTO>> GetByPostalCodeAsync(string postalCode)
        {
            var areas = await facade.GetAreasByPostalCodeAsync(postalCode);
            return mapper.Map<List<AreaDTO>>(areas);
        }

        public async Task<List<AreaDTO>> GetByAreaTypeAsync(string areaType)
        {
            var areas = await facade.GetAreasByTypeAsync(areaType);
            return mapper.Map<List<AreaDTO>>(areas);
        }

        public async Task<List<AreaDTO>> SearchAsync(string searchTerm)
        {
            var areas = await facade.SearchAreasAsync(searchTerm);
            return mapper.Map<List<AreaDTO>>(areas);
        }

        public void Dispose()
        {
            facade?.Dispose();
        }
    }
}
