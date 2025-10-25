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
    public class CountryService : IDisposable
    {
        private readonly IRepoFacade facade;
        private readonly IMapper mapper;

        public CountryService(IMapper mapper)
        {
            facade = DataAccessFactory.Instance.CreateFacade();
            this.mapper = mapper;
        }

        public async Task<CountryDTO> CreateAsync(CountryCreateDTO dto, string createdBy = null)
        {
            if (await facade.CountryCodeExistsAsync(dto.CountryCode))
            {
                throw new Exception($"Country code '{dto.CountryCode}' already exists");
            }

     
            var country = mapper.Map<Country>(dto);
            country.CreatedBy = createdBy;

            country = await facade.CreateCountryAsync(country);
            await facade.SaveChangesAsync();

            return mapper.Map<CountryDTO>(country);
        }

        public async Task<CountryDTO> UpdateAsync(CountryUpdateDTO dto, string updatedBy = null)
        {
            var existing = await facade.GetCountryByIdAsync(dto.Id);
            if (existing == null)
            {
                throw new Exception("Country not found");
            }

            if (await facade.CountryCodeExistsAsync(dto.CountryCode, dto.Id))
            {
                throw new Exception("Country code already exists");
            }

          
            mapper.Map(dto, existing);
            existing.UpdatedBy = updatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await facade.UpdateCountryAsync(existing);
            await facade.SaveChangesAsync();

            return mapper.Map<CountryDTO>(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await facade.DeleteCountryAsync(id);
            if (!result)
            {
                throw new Exception("Country not found");
            }

            await facade.SaveChangesAsync();
            return true;
        }

        public async Task<CountryDTO> GetByIdAsync(int id)
        {
            var country = await facade.GetCountryByIdAsync(id);
            if (country == null)
            {
                throw new Exception("Country not found");
            }

            return mapper.Map<CountryDTO>(country);
        }

        public async Task<CountryDTO> GetByCodeAsync(string code)
        {
            var country = await facade.GetCountryByCodeAsync(code);
            if (country == null)
            {
                throw new Exception("Country not found");
            }

            return mapper.Map<CountryDTO>(country);
        }

        public async Task<List<CountryDTO>> GetAllAsync()
        {
            var countries = await facade.GetAllCountriesAsync();
            return mapper.Map<List<CountryDTO>>(countries);
        }

        public async Task<CountryWithStatesDTO> GetWithStatesAsync(int id)
        {
            var country = await facade.GetCountryWithStatesAsync(id);
            if (country == null)
            {
                throw new Exception("Country not found");
            }

            return mapper.Map<CountryWithStatesDTO>(country);
        }

        public async Task<List<CountryDTO>> SearchAsync(string searchTerm)
        {
            var countries = await facade.SearchCountriesAsync(searchTerm);
            return mapper.Map<List<CountryDTO>>(countries);
        }

        public void Dispose()
        {
            facade?.Dispose();
        }
    }
}
