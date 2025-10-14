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
        Task<State> GetByStateCodeAsync(string stateCode);
        Task<List<State>> GetByCountryIdAsync(int countryId);
        Task<State> GetWithCitiesAsync(int stateId);
        Task<State> GetWithCountryAsync(int stateId);
        Task<IEnumerable<State>> GetByCountryIdWithCitiesAsync(int countryId);
        Task<bool> StateCodeExistsInCountryAsync(string stateCode, int countryId, int? excludeId = null);
    }
}
