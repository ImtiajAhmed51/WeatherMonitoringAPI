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
        List<Location> Search(string keyword);
        List<Location> GetWithActiveAlerts();
        List<Location> GetLocationsInRange(double minLat, double maxLat, double minLon, double maxLon);


        Location GetWithOtherData(int id);
    }
}
