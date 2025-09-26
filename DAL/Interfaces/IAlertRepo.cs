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
        List<Alert> GetByLocation(int locationId, bool onlyActive = true);

        int DeactivateAll();

        List<Alert> GetExpiringSoon(double hours = 6);
        Alert GetAlertLocation(int id);

        List<Alert> GetAlertsLocation();
    }
}
