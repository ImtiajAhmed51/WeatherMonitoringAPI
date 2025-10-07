using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAlertRepo
    {
        Alert GetAlertWithLocation(int id);


        List<Alert> GetAllWithLocations();

        List<Alert> GetByLocation(int locationId);


        List<Alert> GetActiveAlerts();
        int GetAlertCountByLocation(int locationId);
      
    }
}
