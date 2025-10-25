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
    public class LocationHierarchyService: IDisposable
    {
        private readonly IRepoFacade facade;
        private readonly IMapper mapper;

        public LocationHierarchyService(IMapper mapper)
        {
            facade = DataAccessFactory.Instance.CreateFacade();
            this.mapper = mapper;
        }

        public async Task<string> GetFullLocationPathAsync(int areaId)
        {
            var area = await facade.GetAreaWithAllRelationsAsync(areaId);
            if (area == null)
            {
                throw new Exception("Area not found");
            }

            return $"{area.Name}, {area.City.Name}, {area.City.State.Name}, {area.City.State.Country.Name}";
        }

        public async Task<Dictionary<string, int>> GetStatisticsAsync()
        {
            return new Dictionary<string, int>
            {
                ["TotalCountries"] = (await facade.GetAllCountriesAsync()).Count,
                ["TotalStates"] = (await facade.GetAllStatesAsync()).Count,
                ["TotalCities"] = (await facade.GetAllCitiesAsync()).Count,
                ["TotalAreas"] = (await facade.GetAllAreasAsync()).Count
            };
        }

        public async Task<string> CreateCompleteHierarchyAsync(
            CountryCreateDTO countryDto,
            StateCreateDTO stateDto,
            CityCreateDTO cityDto,
            AreaCreateDTO areaDto,
            string createdBy = null)
        {
            facade.BeginTransaction();

            try
            {
              
                var country = mapper.Map<Country>(countryDto);
                country.CreatedBy = createdBy;
                country = await facade.CreateCountryAsync(country);
                await facade.SaveChangesAsync();

                var state = mapper.Map<State>(stateDto);
                state.CountryId = country.Id;
                state.CreatedBy = createdBy;
                state = await facade.CreateStateAsync(state);
                await facade.SaveChangesAsync();

                
                var city = mapper.Map<City>(cityDto);
                city.StateId = state.Id;
                city.CreatedBy = createdBy;
                city = await facade.CreateCityAsync(city);
                await facade.SaveChangesAsync();

                
                var area = mapper.Map<Area>(areaDto);
                area.CityId = city.Id;
                area.CreatedBy = createdBy;
                area = await facade.CreateAreaAsync(area);

                await facade.CommitAsync();

                return $"{area.Name}, {city.Name}, {state.Name}, {country.Name}";
            }
            catch
            {
                facade.Rollback();
                throw;
            }
        }

        public void Dispose()
        {
            facade?.Dispose();
        }
    }
}
