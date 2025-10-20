using DAL.Interfaces;
using DAL.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory
{
    public class RepoFacade : IRepoFacade
    {
        private readonly WeatherContext context;
        private readonly bool ownsContext;
        private DbContextTransaction transaction;

        private IAsyncRepo<Country, int, Country> _mainCountry;
        private IAsyncRepo<State, int, State> _mainState;
        private IAsyncRepo<City, int, City> _mainCity;
        private IAsyncRepo<Area, int, Area> _mainArea;
        private ICountryRepo countryRepo;
        private IStateRepo stateRepo;
        private ICityRepo cityRepo;
        private IAreaRepo areaRepo;

        public RepoFacade()
        {
            context = new WeatherContext();
            ownsContext = true;
        }

        public RepoFacade(WeatherContext context)
        {
            this.context = context;
            ownsContext = false;
        }

        private IAsyncRepo<Country, int, Country> MainCountry => _mainCountry ?? (_mainCountry = new CountryRepo(context));
        private IAsyncRepo<State, int, State> MainState => _mainState ?? (_mainState = new StateRepo(context));
        private IAsyncRepo<City, int, City> MainCity => _mainCity ?? (_mainCity = new CityRepo(context));
        private IAsyncRepo<Area, int, Area> MainArea => _mainArea ?? (_mainArea = new AreaRepo(context));

        private ICountryRepo CountryRepo => countryRepo ?? (countryRepo = new CountryRepo(context));
        private IStateRepo StateRepo => stateRepo ?? (stateRepo = new StateRepo(context));
        private ICityRepo CityRepo => cityRepo ?? (cityRepo = new CityRepo(context));
        private IAreaRepo AreaRepo => areaRepo ?? (areaRepo = new AreaRepo(context));

       
        public async Task<Country> GetCountryByIdAsync(int id)
        {
            return await MainCountry.GetByIdAsync(id);
        }

        public async Task<Country> GetCountryByCodeAsync(string code)
        {
            return await CountryRepo.GetByCountryCodeAsync(code);
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await MainCountry.GetAllAsync();
        }

        public async Task<List<Country>> SearchCountriesAsync(string searchTerm)
        {
            return await CountryRepo.GetByNameAsync(searchTerm);
        }

        public async Task<Country> GetCountryWithStatesAsync(int id)
        {
            return await CountryRepo.GetWithStatesAsync(id);
        }

        public async Task<List<Country>> GetAllCountriesWithStatesAsync()
        {
            return await CountryRepo.GetAllWithStatesAsync();
        }

        public async Task<Country> CreateCountryAsync(Country country)
        {
            return await MainCountry.CreateAsync(country);
        }

        public async Task<Country> UpdateCountryAsync(Country country)
        {
            return await MainCountry.UpdateAsync(country);
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            return await MainCountry.DeleteAsync(id);
        }

        public async Task<bool> CountryCodeExistsAsync(string code, int? excludeId = null)
        {
            return await CountryRepo.CountryCodeExistsAsync(code, excludeId);
        }

        
        public async Task<State> GetStateByIdAsync(int id)
        {
            return await MainState.GetByIdAsync(id);
        }

        public async Task<State> GetStateByCodeAsync(string code)
        {
            return await StateRepo.GetByStateCodeAsync(code);
        }

        public async Task<List<State>> GetAllStatesAsync()
        {
            return await MainState.GetAllAsync();
        }

        public async Task<List<State>> GetStatesByCountryAsync(int countryId)
        {
            return await StateRepo.GetByCountryIdAsync(countryId);
        }

        public async Task<State> GetStateWithCitiesAsync(int id)
        {
            return await StateRepo.GetWithCitiesAsync(id);
        }

        public async Task<State> GetStateWithCountryAsync(int id)
        {
            return await StateRepo.GetWithCountryAsync(id);
        }

        public async Task<IEnumerable<State>> GetStatesByCountryWithCitiesAsync(int countryId)
        {
            return await StateRepo.GetByCountryIdWithCitiesAsync(countryId);
        }

        public async Task<State> CreateStateAsync(State state)
        {
            return await MainState.CreateAsync(state);
        }

        public async Task<State> UpdateStateAsync(State state)
        {
            return await MainState.UpdateAsync(state);
        }

        public async Task<bool> DeleteStateAsync(int id)
        {
            return await MainState.DeleteAsync(id);
        }

        public async Task<bool> StateCodeExistsAsync(string code, int countryId, int? excludeId = null)
        {
            return await StateRepo.StateCodeExistsInCountryAsync(code, countryId, excludeId);
        }

        
        public async Task<City> GetCityByIdAsync(int id)
        {
            return await MainCity.GetByIdAsync(id);
        }

        public async Task<List<City>> GetAllCitiesAsync()
        {
            return await MainCity.GetAllAsync();
        }

        public async Task<List<City>> GetCitiesByStateAsync(int stateId)
        {
            return await CityRepo.GetByStateIdAsync(stateId);
        }

        public async Task<List<City>> GetCitiesByStateWithAreasAsync(int stateId)
        {
            return await CityRepo.GetByStateIdWithAreasAsync(stateId);
        }

        public async Task<City> GetCityWithAreasAsync(int id)
        {
            return await CityRepo.GetWithAreasAsync(id);
        }

        public async Task<City> GetCityWithStateAndCountryAsync(int id)
        {
            return await CityRepo.GetWithStateAndCountryAsync(id);
        }

        public async Task<List<City>> GetCapitalCitiesAsync()
        {
            return await CityRepo.GetCapitalCitiesAsync();
        }

        public async Task<List<City>> GetCitiesByPostalCodeAsync(string postalCode)
        {
            return await CityRepo.GetByPostalCodeAsync(postalCode);
        }

        public async Task<List<City>> SearchCitiesAsync(string searchTerm)
        {
            return await CityRepo.SearchByNameAsync(searchTerm);
        }

        public async Task<List<City>> GetNearbyCitiesAsync(decimal latitude, decimal longitude, decimal radiusKm)
        {
            return await CityRepo.GetNearbyCitiesAsync(latitude, longitude, radiusKm);
        }

        public async Task<City> CreateCityAsync(City city)
        {
            return await MainCity.CreateAsync(city);
        }

        public async Task<City> UpdateCityAsync(City city)
        {
            return await MainCity.UpdateAsync(city);
        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            return await MainCity.DeleteAsync(id);
        }

        
        public async Task<Area> GetAreaByIdAsync(int id)
        {
            return await MainArea.GetByIdAsync(id);
        }

        public async Task<List<Area>> GetAllAreasAsync()
        {
            return await MainArea.GetAllAsync();
        }

        public async Task<List<Area>> GetAreasByCityAsync(int cityId)
        {
            return await AreaRepo.GetByCityIdAsync(cityId);
        }

        public async Task<List<Area>> GetAreasByCityWithWeatherAsync(int cityId)
        {
            return await AreaRepo.GetByCityIdWithWeatherAsync(cityId);
        }

        public async Task<List<Area>> GetAreasByPostalCodeAsync(string postalCode)
        {
            return await AreaRepo.GetByPostalCodeAsync(postalCode);
        }

        public async Task<List<Area>> GetAreasByTypeAsync(string areaType)
        {
            return await AreaRepo.GetByAreaTypeAsync(areaType);
        }

        public async Task<Area> GetAreaWithCityAsync(int id)
        {
            return await AreaRepo.GetWithCityAsync(id);
        }

        public async Task<Area> GetAreaWithAllRelationsAsync(int id)
        {
            return await AreaRepo.GetWithAllRelationsAsync(id);
        }

        public async Task<List<Area>> SearchAreasAsync(string searchTerm)
        {
            return await AreaRepo.SearchByNameAsync(searchTerm);
        }

        public async Task<Area> CreateAreaAsync(Area area)
        {
            return await MainArea.CreateAsync(area);
        }

        public async Task<Area> UpdateAreaAsync(Area area)
        {
            return await MainArea.UpdateAsync(area);
        }

        public async Task<bool> DeleteAreaAsync(int id)
        {
            return await MainArea.DeleteAsync(id);
        }

        
        public void BeginTransaction()
        {
            transaction = context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            try
            {
                await context.SaveChangesAsync();
                transaction?.Commit();
            }
            catch
            {
                transaction?.Rollback();
                throw;
            }
            finally
            {
                transaction?.Dispose();
                transaction = null;
            }
        }

        public void Rollback()
        {
            transaction?.Rollback();
            transaction?.Dispose();
            transaction = null;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            transaction?.Dispose();
            if (ownsContext)
            {
                context?.Dispose();
            }
        }
    }
}
