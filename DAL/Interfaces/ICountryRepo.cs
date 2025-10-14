using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICountryRepo
    {
        Task<Country> GetByCountryCodeAsync(string countryCode);
        Task<List<Country>> GetByNameAsync(string name);
        Task<Country> GetWithStatesAsync(int countryId);
        Task<List<Country>> GetAllWithStatesAsync();
        Task<bool> CountryCodeExistsAsync(string countryCode, int? excludeId = null);
    }
}
