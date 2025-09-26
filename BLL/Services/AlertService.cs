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

        public static bool UpdateAlert(AlertDTO dto)
        {
            var entity = GetMapper().Map<Alert>(dto);
            return DataAccessFactory.AlertData().Update(entity);
        }

        public static bool DeleteAlert(int id)
        {
            return DataAccessFactory.AlertData().Delete(id);
        }
    }
}
