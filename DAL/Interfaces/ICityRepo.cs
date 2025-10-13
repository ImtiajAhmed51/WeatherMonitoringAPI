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
        Task<List<City>> GetByStateId(int stateId);
        Task<City> GetWithAreas(int cityId);
        Task<City> GetWithStateAndCountry(int cityId);
        Task<List<City>> GetByPostalCode(string postalCode);
        Task<List<City>> GetCapitalCities();
        Task<List<City>> SearchByName(string searchTerm);
        Task<List<City>> GetByStateIdWithAreas(int stateId);
        Task<List<City>> GetNearbyCities(decimal latitude, decimal longitude, decimal radiusKm);
    }
}
