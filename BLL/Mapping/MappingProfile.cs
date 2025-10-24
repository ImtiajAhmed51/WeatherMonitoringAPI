using AutoMapper;
using BLL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
         
            CreateMap<Country, CountryDTO>();
            CreateMap<Country, CountryWithStatesDTO>()
                .IncludeBase<Country, CountryDTO>();

            CreateMap<CountryCreateDTO, Country>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => true))
                .ForMember(d => d.IsDeleted, opt => opt.MapFrom(s => false))
                .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.States, opt => opt.Ignore());

            CreateMap<CountryUpdateDTO, Country>()
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.IsDeleted, opt => opt.Ignore())
                .ForMember(d => d.States, opt => opt.Ignore());

         
            CreateMap<State, StateDTO>()
                .ForMember(d => d.CountryName, opt => opt.MapFrom(s => s.Country != null ? s.Country.Name : null));

            CreateMap<State, StateWithCitiesDTO>()
                .IncludeBase<State, StateDTO>();

            CreateMap<StateCreateDTO, State>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => true))
                .ForMember(d => d.IsDeleted, opt => opt.MapFrom(s => false))
                .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Country, opt => opt.Ignore())
                .ForMember(d => d.Cities, opt => opt.Ignore());

            CreateMap<StateUpdateDTO, State>()
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.IsDeleted, opt => opt.Ignore())
                .ForMember(d => d.Country, opt => opt.Ignore())
                .ForMember(d => d.Cities, opt => opt.Ignore());

            
            CreateMap<City, CityDTO>()
                .ForMember(d => d.StateName, opt => opt.MapFrom(s => s.State != null ? s.State.Name : null))
                .ForMember(d => d.CountryName, opt => opt.MapFrom(s => s.State != null && s.State.Country != null ? s.State.Country.Name : null));

            CreateMap<CityCreateDTO, City>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => true))
                .ForMember(d => d.IsDeleted, opt => opt.MapFrom(s => false))
                .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.State, opt => opt.Ignore())
                .ForMember(d => d.Areas, opt => opt.Ignore())
                .ForMember(d => d.Alerts, opt => opt.Ignore())
                .ForMember(d => d.WeatherRecords, opt => opt.Ignore());

            CreateMap<CityUpdateDTO, City>()
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.IsDeleted, opt => opt.Ignore())
                .ForMember(d => d.State, opt => opt.Ignore())
                .ForMember(d => d.Areas, opt => opt.Ignore())
                .ForMember(d => d.Alerts, opt => opt.Ignore())
                .ForMember(d => d.WeatherRecords, opt => opt.Ignore());

         
            CreateMap<Area, AreaDTO>()
                .ForMember(d => d.CityName, opt => opt.MapFrom(s => s.City != null ? s.City.Name : null))
                .ForMember(d => d.StateName, opt => opt.MapFrom(s => s.City != null && s.City.State != null ? s.City.State.Name : null))
                .ForMember(d => d.CountryName, opt => opt.MapFrom(s => s.City != null && s.City.State != null && s.City.State.Country != null ? s.City.State.Country.Name : null));

            CreateMap<AreaCreateDTO, Area>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => true))
                .ForMember(d => d.IsDeleted, opt => opt.MapFrom(s => false))
                .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.City, opt => opt.Ignore())
                .ForMember(d => d.Alerts, opt => opt.Ignore())
                .ForMember(d => d.WeatherRecords, opt => opt.Ignore());

            CreateMap<AreaUpdateDTO, Area>()
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.IsDeleted, opt => opt.Ignore())
                .ForMember(d => d.City, opt => opt.Ignore())
                .ForMember(d => d.Alerts, opt => opt.Ignore())
                .ForMember(d => d.WeatherRecords, opt => opt.Ignore());
        }
    }
}
