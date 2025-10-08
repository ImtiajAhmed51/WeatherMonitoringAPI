using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ILocationRepo
    {
        Location GetWithAlerts(int id);
        Location GetWithWeatherRecords(int id);
        Location GetWithAllData(int id);
        List<Location> GetAllWithAlerts();
        List<Location> GetAllWithWeatherRecords();
        List<Location> SearchByName(string name);
        List<Location> SearchByCountry(string country);
        Location GetByNameAndCountry(string name, string country);
        List<Location> GetByCoordinates(decimal latitude, decimal longitude);
        int GetLocationCount();
        bool Exists(int id);
        bool LocationNameExists(string name, string country);
 
    }
}
