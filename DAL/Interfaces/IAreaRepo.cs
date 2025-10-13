using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAreaRepo
    {
        Task<List<Area>> GetByCityIdAsync(int cityId);
        Task<Area> GetWithCityAsync(int areaId);
        Task<List<Area>> GetByPostalCodeAsync(string postalCode);
        Task<List<Area>> GetByAreaTypeAsync(string areaType);
        Task<List<Area>> SearchByNameAsync(string searchTerm);
        Task<List<Area>> GetByCityIdWithWeatherAsync(int cityId);
        Task<Area> GetWithAllRelationsAsync(int areaId);
    }
}
