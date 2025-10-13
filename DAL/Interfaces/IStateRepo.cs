using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IStateRepo
    {
        Task<State> GetByStateCode(string stateCode);
        Task<List<State>> GetByCountryId(int countryId);
        Task<State> GetWithCities(int stateId);
        Task<State> GetWithCountry(int stateId);
        Task<IEnumerable<State>> GetByCountryIdWithCities(int countryId);
        Task<bool> StateCodeExistsInCountry(string stateCode, int countryId, int? excludeId = null);
    }
}
