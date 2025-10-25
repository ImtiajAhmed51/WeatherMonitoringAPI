using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.Factory;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class StateService:IDisposable
    {
        private readonly IRepoFacade facade;
        private readonly IMapper mapper;

        public StateService(IMapper mapper)
        {
            facade = DataAccessFactory.Instance.CreateFacade();
            this.mapper = mapper;
        }

        public async Task<StateDTO> CreateAsync(StateCreateDTO dto, string createdBy = null)
        {

            var country = await facade.GetCountryByIdAsync(dto.CountryId);
            if (country == null)
            {
                throw new Exception("Country not found");
            }


            if (!string.IsNullOrEmpty(dto.StateCode))
            {
                if (await facade.StateCodeExistsAsync(dto.StateCode, dto.CountryId))
                {
                    throw new Exception("State code already exists in this country");
                }
            }

            var state = mapper.Map<State>(dto);
            state.CreatedBy = createdBy;

            state = await facade.CreateStateAsync(state);
            await facade.SaveChangesAsync();

            var result = mapper.Map<StateDTO>(state);
            result.CountryName = country.Name;

            return result;
        }

        public async Task<StateDTO> UpdateAsync(StateUpdateDTO dto, string updatedBy = null)
        {
            var existing = await facade.GetStateByIdAsync(dto.Id);
            if (existing == null)
            {
                throw new Exception("State not found");
            }

            if (!string.IsNullOrEmpty(dto.StateCode))
            {
                if (await facade.StateCodeExistsAsync(dto.StateCode, dto.CountryId, dto.Id))
                {
                    throw new Exception("State code already exists in this country");
                }
            }

            mapper.Map(dto, existing);
            existing.UpdatedBy = updatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await facade.UpdateStateAsync(existing);
            await facade.SaveChangesAsync();

            var stateWithCountry = await facade.GetStateWithCountryAsync(updated.Id);
            return mapper.Map<StateDTO>(stateWithCountry);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await facade.DeleteStateAsync(id);
            if (!result)
            {
                throw new Exception("State not found");
            }

            await facade.SaveChangesAsync();
            return true;
        }

        public async Task<StateDTO> GetByIdAsync(int id)
        {
            var state = await facade.GetStateWithCountryAsync(id);
            if (state == null)
            {
                throw new Exception("State not found");
            }

            return mapper.Map<StateDTO>(state);
        }

        public async Task<StateDTO> GetByCodeAsync(string code)
        {
            var state = await facade.GetStateByCodeAsync(code);
            if (state == null)
            {
                throw new Exception("State not found");
            }

            return mapper.Map<StateDTO>(state);
        }

        public async Task<List<StateDTO>> GetAllAsync()
        {
            var states = await facade.GetAllStatesAsync();
            return mapper.Map<List<StateDTO>>(states);
        }

        public async Task<List<StateDTO>> GetByCountryIdAsync(int countryId)
        {
            var states = await facade.GetStatesByCountryAsync(countryId);
            var country = await facade.GetCountryByIdAsync(countryId);

            var dtos = mapper.Map<List<StateDTO>>(states);
            if (country != null)
            {
                dtos.ForEach(d => d.CountryName = country.Name);
            }

            return dtos;
        }

        public async Task<StateWithCitiesDTO> GetWithCitiesAsync(int id)
        {
            var state = await facade.GetStateWithCitiesAsync(id);
            if (state == null)
            {
                throw new Exception("State not found");
            }

            return mapper.Map<StateWithCitiesDTO>(state);
        }

        public void Dispose()
        {
            facade?.Dispose();
        }
    }
}
