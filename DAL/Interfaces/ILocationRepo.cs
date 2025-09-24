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
        List<Location> GetByCountry(string country);
        List<Location> GetByName(string name);
        bool Exists(string name, string country);
        List<Location> GetWithActiveAlerts();
        List<Location> GetNearby(decimal latitude, decimal longitude, double radiusKm);
        Location GetNearest(decimal latitude, decimal longitude, double radiusKm);
    }
}
