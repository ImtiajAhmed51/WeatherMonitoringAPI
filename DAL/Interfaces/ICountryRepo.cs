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
        Task<Country> GetByCountryCode(string countryCode);
        Task<List<Country>> GetByName(string name);
        Task<Country> GetWithStates(int countryId);
        Task<List<Country>> GetAllWithStates();
        Task<bool> CountryCodeExists(string countryCode, int? excludeId = null);
    }
}
