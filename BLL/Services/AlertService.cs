using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AlertService
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Alert, AlertDTO>().ReverseMap();
                cfg.CreateMap<Alert, AlertWithLocationDTO>().ReverseMap();
            
                cfg.CreateMap<Location, LocationDTO>();

            });
            return new Mapper(config);
        }

        public static List<AlertDTO> GetAllAlerts()
        {
            var alerts = DataAccessFactory.AlertData().Get();
            return GetMapper().Map<List<AlertDTO>>(alerts);
        }

        public static AlertDTO GetAlertById(int id)
        {
            var alert = DataAccessFactory.AlertData().Get(id);
            return GetMapper().Map<AlertDTO>(alert);
        }

        public static bool CreateAlert(AlertDTO dto)
        {
            var entity = GetMapper().Map<Alert>(dto);
            return DataAccessFactory.AlertData().Create(entity);
        }

        public static AlertWithLocationDTO GetAlertWithLocationById(int id)
        {
            var alert = DataAccessFactory.AlertDataFeature().GetAlertLocation(id);
            if (alert == null) return null;

            return GetMapper().Map<AlertWithLocationDTO>(alert);
        }

        public static List<AlertWithLocationDTO> GetAlertsWithLocation()
        {
            var alert = DataAccessFactory.AlertDataFeature().GetAlertsLocation();
            if (alert == null) return null;

            return GetMapper().Map<List<AlertWithLocationDTO>>(alert);
        }

        public static bool UpdateAlert(AlertDTO dto)
        {
            var entity = GetMapper().Map<Alert>(dto);
            return DataAccessFactory.AlertData().Update(entity);
        }

        public static bool DeleteAlert(int id)
        {
            return DataAccessFactory.AlertData().Delete(id);
        }
        public static List<AlertDTO> GetActiveAlerts()
        {
            var alerts = DataAccessFactory.AlertData().Get()
                .Where(a => a.IsActive && (a.ExpiresAt == null || a.ExpiresAt > DateTime.UtcNow))
                .ToList();

            return GetMapper().Map<List<AlertDTO>>(alerts);
        }


        public static List<AlertDTO> GetAlertsByLocation(int locationId, bool onlyActive = true)
        {
            var alerts = DataAccessFactory.AlertDataFeature().GetByLocation(locationId, onlyActive);
            return GetMapper().Map<List<AlertDTO>>(alerts);
        }


        public static List<AlertDTO> GetAlertsExpiringSoon(double hours = 6)
        {
            var alerts = DataAccessFactory.AlertDataFeature().GetExpiringSoon(hours);
            return GetMapper().Map<List<AlertDTO>>(alerts);
        }

        public static int DeactivateAllAlerts()
        {
            return DataAccessFactory.AlertDataFeature().DeactivateAll();
        }
    }
}
