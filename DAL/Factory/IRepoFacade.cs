using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory
{
    public interface IRepoFacade:IDisposable
    {

        Task<Country> GetCountryByIdAsync(int id);
        Task<Country> GetCountryByCodeAsync(string code);
        Task<List<Country>> GetAllCountriesAsync();
        Task<List<Country>> SearchCountriesAsync(string searchTerm);
        Task<Country> GetCountryWithStatesAsync(int id);
        Task<List<Country>> GetAllCountriesWithStatesAsync();
        Task<Country> CreateCountryAsync(Country country);
        Task<Country> UpdateCountryAsync(Country country);
        Task<bool> DeleteCountryAsync(int id);
        Task<bool> CountryCodeExistsAsync(string code, int? excludeId = null);

        Task<State> GetStateByIdAsync(int id);
        Task<State> GetStateByCodeAsync(string code);
        Task<List<State>> GetAllStatesAsync();
        Task<List<State>> GetStatesByCountryAsync(int countryId);
        Task<State> GetStateWithCitiesAsync(int id);
        Task<State> GetStateWithCountryAsync(int id);
        Task<IEnumerable<State>> GetStatesByCountryWithCitiesAsync(int countryId);
        Task<State> CreateStateAsync(State state);
        Task<State> UpdateStateAsync(State state);
        Task<bool> DeleteStateAsync(int id);
        Task<bool> StateCodeExistsAsync(string code, int countryId, int? excludeId = null);


        Task<City> GetCityByIdAsync(int id);
        Task<List<City>> GetAllCitiesAsync();
        Task<List<City>> GetCitiesByStateAsync(int stateId);
        Task<List<City>> GetCitiesByStateWithAreasAsync(int stateId);
        Task<City> GetCityWithAreasAsync(int id);
        Task<City> GetCityWithStateAndCountryAsync(int id);
        Task<List<City>> GetCapitalCitiesAsync();
        Task<List<City>> GetCitiesByPostalCodeAsync(string postalCode);
        Task<List<City>> SearchCitiesAsync(string searchTerm);
        Task<List<City>> GetNearbyCitiesAsync(decimal latitude, decimal longitude, decimal radiusKm);
        Task<City> CreateCityAsync(City city);
        Task<City> UpdateCityAsync(City city);
        Task<bool> DeleteCityAsync(int id);


        Task<Area> GetAreaByIdAsync(int id);
        Task<List<Area>> GetAllAreasAsync();
        Task<List<Area>> GetAreasByCityAsync(int cityId);
        Task<List<Area>> GetAreasByCityWithWeatherAsync(int cityId);
        Task<List<Area>> GetAreasByPostalCodeAsync(string postalCode);
        Task<List<Area>> GetAreasByTypeAsync(string areaType);
        Task<Area> GetAreaWithCityAsync(int id);
        Task<Area> GetAreaWithAllRelationsAsync(int id);
        Task<List<Area>> SearchAreasAsync(string searchTerm);
        Task<Area> CreateAreaAsync(Area area);
        Task<Area> UpdateAreaAsync(Area area);
        Task<bool> DeleteAreaAsync(int id);


        void BeginTransaction();
        Task CommitAsync();
        void Rollback();
        Task<int> SaveChangesAsync();
    }
}
