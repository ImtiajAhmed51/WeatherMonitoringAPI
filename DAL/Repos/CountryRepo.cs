using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class CountryRepo : IAsyncRepo<Country, int, Country>, ICountryRepo
    {
        private readonly WeatherContext context;

        public CountryRepo()
        {
            context = new WeatherContext();
        }

        public CountryRepo(WeatherContext context)
        {
            this.context = context;
        }

        public async Task<Country> CreateAsync(Country entity)
        {
            try
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.IsActive = true;
                entity.IsDeleted = false;

                context.Countries.Add(entity);
                await context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating country: " + ex.Message, ex);
            }
        }

        public async Task<Country> UpdateAsync(Country entity)
        {
            try
            {
                var existingCountry = await context.Countries.FindAsync(entity.Id);

                if (existingCountry == null)
                    throw new Exception("Country not found");

                existingCountry.Name = entity.Name;
                existingCountry.CountryCode = entity.CountryCode;
                existingCountry.Capital = entity.Capital;
                existingCountry.Currency = entity.Currency;
                existingCountry.PhoneCode = entity.PhoneCode;
                existingCountry.Population = entity.Population;
                existingCountry.Area = entity.Area;
                existingCountry.TimeZone = entity.TimeZone;
                existingCountry.IsActive = entity.IsActive;
                existingCountry.UpdatedAt = DateTime.UtcNow;
                existingCountry.UpdatedBy = entity.UpdatedBy;

                context.Entry(existingCountry).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return existingCountry;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating country: " + ex.Message, ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var country = await context.Countries.FindAsync(id);

                if (country == null)
                    return false;

                country.IsDeleted = true;
                country.UpdatedAt = DateTime.UtcNow;

                context.Entry(country).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting country: " + ex.Message, ex);
            }
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            try
            {
                return await context.Countries
                    .Where(c => !c.IsDeleted && c.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting country by id: " + ex.Message, ex);
            }
        }

        public async Task<List<Country>> GetAllAsync()
        {
            try
            {
                return await context.Countries
                    .Where(c => !c.IsDeleted)
                    .OrderBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all countries: " + ex.Message, ex);
            }
        }

        public async Task<Country> GetByCountryCodeAsync(string countryCode)
        {
            try
            {
                return await context.Countries
                    .Where(c => !c.IsDeleted && c.CountryCode == countryCode)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting country by code: " + ex.Message, ex);
            }
        }

        public async Task<List<Country>> GetByNameAsync(string name)
        {
            try
            {
                return await context.Countries
                    .Where(c => !c.IsDeleted && c.Name.Contains(name))
                    .OrderBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting countries by name: " + ex.Message, ex);
            }
        }

        public async Task<Country> GetWithStatesAsync(int countryId)
        {
            try
            {
                return await context.Countries
                    .Include(c => c.States)
                    .Where(c => !c.IsDeleted && c.Id == countryId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting country with states: " + ex.Message, ex);
            }
        }

        public async Task<List<Country>> GetAllWithStatesAsync()
        {
            try
            {
                return await context.Countries
                    .Include(c => c.States)
                    .Where(c => !c.IsDeleted)
                    .OrderBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all countries with states: " + ex.Message, ex);
            }
        }

        public async Task<bool> CountryCodeExistsAsync(string countryCode, int? excludeId = null)
        {
            try
            {
                var query = context.Countries
                    .Where(c => !c.IsDeleted && c.CountryCode == countryCode);

                if (excludeId.HasValue)
                {
                    query = query.Where(c => c.Id != excludeId.Value);
                }

                return await query.AnyAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking country code: " + ex.Message, ex);
            }
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
