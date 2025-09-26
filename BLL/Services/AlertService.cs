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
            return DataAccessFactory.AlertData().Create(entity);
        }

        public static AlertWithLocationDTO GetAlertWithLocationById(int id)
        {
            var alert = DataAccessFactory.AlertDataFeature().GetAlertLocation(id);
            return alert !=null? mapper.Map<AlertWithLocationDTO>(alert):null;
        }

        public static List<AlertWithLocationDTO> GetAlertsWithLocation()
        {
            var alert = DataAccessFactory.AlertDataFeature().GetAlertsLocation();
            return alert != null?mapper.Map<List<AlertWithLocationDTO>>(alert):null;
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
        public static List<AlertDTO> GetActiveAlerts()
        {
            var alerts = DataAccessFactory.AlertData().Get()
                .Where(a => a.IsActive && (a.ExpiresAt == null || a.ExpiresAt > DateTime.UtcNow))
                .ToList();

            return mapper.Map<List<AlertDTO>>(alerts);
        }


        public static List<AlertDTO> GetAlertsByLocation(int locationId, bool onlyActive = true)
        {
            var alerts = DataAccessFactory.AlertDataFeature().GetByLocation(locationId, onlyActive);
            return mapper.Map<List<AlertDTO>>(alerts);
        }


        public static List<AlertDTO> GetAlertsExpiringSoon(double hours = 6)
        {
            var alerts = DataAccessFactory.AlertDataFeature().GetExpiringSoon(hours);
            return mapper.Map<List<AlertDTO>>(alerts);
        }

        public static int DeactivateAllAlerts()
        {
            return DataAccessFactory.AlertDataFeature().DeactivateAll();
        }
    }
}
