using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICityRepo
    {
        Task<List<City>> GetByStateIdAsync(int stateId);
        Task<City> GetWithAreasAsync(int cityId);
        Task<City> GetWithStateAndCountryAsync(int cityId);
        Task<List<City>> GetByPostalCodeAsync(string postalCode);
        Task<List<City>> GetCapitalCitiesAsync();
        Task<List<City>> SearchByNameAsync(string searchTerm);
        Task<List<City>> GetByStateIdWithAreasAsync(int stateId);
        Task<List<City>> GetNearbyCitiesAsync(decimal latitude, decimal longitude, decimal radiusKm);
    }
}
