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
        private static IMapper mapper;
        static AlertService(){
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Alert, AlertDTO>().ReverseMap();
                cfg.CreateMap<Alert, AlertWithLocationDTO>().ReverseMap();

                cfg.CreateMap<Location, LocationDTO>().ReverseMap(); ;

            });
            mapper = config.CreateMapper();
        }

        public static List<AlertDTO> GetAllAlerts()
        {
            var alerts = DataAccessFactory.AlertData().Get();
            return mapper.Map<List<AlertDTO>>(alerts);
        }

        public static AlertDTO GetAlertById(int id)
        {
            var alert = DataAccessFactory.AlertData().Get(id);
            return mapper.Map<AlertDTO>(alert);
        }

        public static bool CreateAlert(AlertDTO dto)
        {
            var entity = mapper.Map<Alert>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.IsActive = true;
            return DataAccessFactory.AlertData().Create(entity);
        }

        public static bool UpdateAlert(AlertDTO dto)
        {
            var entity = mapper.Map<Alert>(dto);
            return DataAccessFactory.AlertData().Update(entity);
        }

        public static bool DeleteAlert(int id)
        {
            return DataAccessFactory.AlertData().Delete(id);
        }

        public static AlertWithLocationDTO GetAlertWithLocationById(int id)
        {
            var alert = DataAccessFactory.AlertDataFeature().GetAlertWithLocation(id);
            return alert != null ? mapper.Map<AlertWithLocationDTO>(alert) : null;
        }

        public static List<AlertWithLocationDTO> GetAllAlertsWithLocations()
        {
            var alerts = DataAccessFactory.AlertDataFeature().GetAllWithLocations();
            return alerts != null ? mapper.Map<List<AlertWithLocationDTO>>(alerts) : null;
        }
        public static List<AlertDTO> GetAlertsByLocation(int locationId)
        {
            var alerts = DataAccessFactory.AlertDataFeature().GetByLocation(locationId);
            return mapper.Map<List<AlertDTO>>(alerts);
        }

        public static List<AlertWithLocationDTO> GetAlertsByLocationWithDetails(int locationId)
        {
            var allAlerts = DataAccessFactory.AlertDataFeature().GetAllWithLocations();
            var filteredAlerts = allAlerts.Where(a => a.LocationId == locationId).ToList();
            return mapper.Map<List<AlertWithLocationDTO>>(filteredAlerts);
        }

        public static int GetAlertCountByLocation(int locationId)
        {
            return DataAccessFactory.AlertDataFeature().GetAlertCountByLocation(locationId);
        }
        public static List<AlertDTO> GetActiveAlerts()
        {
            var alerts = DataAccessFactory.AlertDataFeature().GetActiveAlerts();
            return mapper.Map<List<AlertDTO>>(alerts);
        }

        public static List<AlertWithLocationDTO> GetActiveAlertsWithLocations()
        {
            var alerts = DataAccessFactory.AlertDataFeature().GetAllWithLocations()
                .Where(a => a.IsActive)
                .ToList();
            return mapper.Map<List<AlertWithLocationDTO>>(alerts);
        }

        public static bool ActivateAlert(int id)
        {
            var alert = DataAccessFactory.AlertData().Get(id);
            if (alert == null)
                return false;

            alert.IsActive = true;
            return DataAccessFactory.AlertData().Update(alert);
        }

        public static bool DeactivateAlert(int id)
        {
            var alert = DataAccessFactory.AlertData().Get(id);
            if (alert == null)
                return false;

            alert.IsActive = false;
            return DataAccessFactory.AlertData().Update(alert);
        }
        public static List<AlertDTO> GetExpiredAlerts()
        {
            var alerts = DataAccessFactory.AlertData().Get()
                .Where(a => a.ExpiresAt != null && a.ExpiresAt <= DateTime.UtcNow)
                .ToList();
            return mapper.Map<List<AlertDTO>>(alerts);
        }

        public static List<AlertDTO> GetAlertsByExpiration(DateTime startDate, DateTime endDate)
        {
            var alerts = DataAccessFactory.AlertData().Get()
                .Where(a => a.ExpiresAt != null &&
                           a.ExpiresAt >= startDate &&
                           a.ExpiresAt <= endDate)
                .OrderBy(a => a.ExpiresAt)
                .ToList();
            return mapper.Map<List<AlertDTO>>(alerts);
        }

        public static int CleanupExpiredAlerts()
        {
            var expiredAlerts = DataAccessFactory.AlertData().Get()
                .Where(a => a.ExpiresAt != null && a.ExpiresAt <= DateTime.UtcNow)
                .ToList();

            int count = 0;
            foreach (var alert in expiredAlerts)
            {
                alert.IsActive = false;
                if (DataAccessFactory.AlertData().Update(alert))
                    count++;
            }
            return count;
        }

        public static List<AlertDTO> GetAlertsBySeverity(string severity)
        {
            var alerts = DataAccessFactory.AlertData().Get()
                .Where(a => a.Severity != null && a.Severity.Equals(severity, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return mapper.Map<List<AlertDTO>>(alerts);
        }

        public static List<AlertDTO> GetAlertsByDateRange(DateTime startDate, DateTime endDate)
        {
            var alerts = DataAccessFactory.AlertData().Get()
                .Where(a => a.CreatedAt >= startDate && a.CreatedAt <= endDate)
                .OrderByDescending(a => a.CreatedAt)
                .ToList();
            return mapper.Map<List<AlertDTO>>(alerts);
        }

        public static List<AlertWithLocationDTO> GetRecentAlertsWithLocation(int days = 7)
        {
            var cutoffDate = DateTime.UtcNow.AddDays(-days);
            var alerts = DataAccessFactory.AlertDataFeature().GetAllWithLocations()
                .Where(a => a.CreatedAt >= cutoffDate)
                .OrderByDescending(a => a.CreatedAt)
                .ToList();
            return mapper.Map<List<AlertWithLocationDTO>>(alerts);
        }
        public static int GetTotalActiveAlertsCount()
        {
            return DataAccessFactory.AlertDataFeature().GetActiveAlerts().Count;
        }

        public static int GetTotalAlertsCount()
        {
            return DataAccessFactory.AlertData().Get().Count;
        }

        public static Dictionary<string, int> GetAlertStatsBySeverity()
        {
            var alerts = DataAccessFactory.AlertData().Get();
            return alerts
                .Where(a => a.Severity != null)
                .GroupBy(a => a.Severity)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public static Dictionary<int, int> GetAlertStatsByLocation()
        {
            var alerts = DataAccessFactory.AlertData().Get();
            return alerts
                .GroupBy(a => a.LocationId)
                .ToDictionary(g => g.Key, g => g.Count());
        }

    }
}
