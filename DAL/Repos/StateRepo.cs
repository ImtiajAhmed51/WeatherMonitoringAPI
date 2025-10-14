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
    internal class StateRepo : IAsyncRepo<State, int, State>, IStateRepo
    {
        private readonly WeatherContext context;

        public StateRepo()
        {
            context = new WeatherContext();
        }

        public StateRepo(WeatherContext context)
        {
            this.context = context;
        }

        public async Task<State> CreateAsync(State entity)
        {
            try
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.IsActive = true;
                entity.IsDeleted = false;

                context.States.Add(entity);
                await context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating state: " + ex.Message, ex);
            }
        }

        public async Task<State> UpdateAsync(State entity)
        {
            try
            {
                var existingState = await context.States.FindAsync(entity.Id);

                if (existingState == null)
                    throw new Exception("State not found");

                existingState.Name = entity.Name;
                existingState.StateCode = entity.StateCode;
                existingState.Capital = entity.Capital;
                existingState.Population = entity.Population;
                existingState.Area = entity.Area;
                existingState.TimeZone = entity.TimeZone;
                existingState.CountryId = entity.CountryId;
                existingState.IsActive = entity.IsActive;
                existingState.UpdatedAt = DateTime.UtcNow;
                existingState.UpdatedBy = entity.UpdatedBy;

                context.Entry(existingState).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return existingState;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating state: " + ex.Message, ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var state = await context.States.FindAsync(id);

                if (state == null)
                    return false;

                state.IsDeleted = true;
                state.UpdatedAt = DateTime.UtcNow;

                context.Entry(state).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting state: " + ex.Message, ex);
            }
        }

        public async Task<State> GetByIdAsync(int id)
        {
            try
            {
                return await context.States
                    .Where(s => !s.IsDeleted && s.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting state by id: " + ex.Message, ex);
            }
        }

        public async Task<List<State>> GetAllAsync()
        {
            try
            {
                return await context.States
                    .Where(s => !s.IsDeleted)
                    .OrderBy(s => s.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all states: " + ex.Message, ex);
            }
        }

        public async Task<List<State>> GetByCountryIdAsync(int countryId)
        {
            try
            {
                return await context.States
                    .Where(s => !s.IsDeleted && s.CountryId == countryId)
                    .OrderBy(s => s.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting states by country: " + ex.Message, ex);
            }
        }

        public async Task<State> GetByStateCodeAsync(string stateCode)
        {
            try
            {
                return await context.States
                    .Where(s => !s.IsDeleted && s.StateCode == stateCode)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting state by code: " + ex.Message, ex);
            }
        }

        public async Task<State> GetWithCitiesAsync(int stateId)
        {
            try
            {
                return await context.States
                    .Include(s => s.Cities)
                    .Where(s => !s.IsDeleted && s.Id == stateId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting state with cities: " + ex.Message, ex);
            }
        }

        public async Task<State> GetWithCountryAsync(int stateId)
        {
            try
            {
                return await context.States
                    .Include(s => s.Country)
                    .Where(s => !s.IsDeleted && s.Id == stateId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting state with country: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<State>> GetByCountryIdWithCitiesAsync(int countryId)
        {
            try
            {
                return await context.States
                    .Include(s => s.Cities)
                    .Where(s => !s.IsDeleted && s.CountryId == countryId)
                    .OrderBy(s => s.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting states with cities: " + ex.Message, ex);
            }
        }

        public async Task<bool> StateCodeExistsInCountryAsync(string stateCode, int countryId, int? excludeId = null)
        {
            try
            {
                var query = context.States
                    .Where(s => !s.IsDeleted && s.StateCode == stateCode && s.CountryId == countryId);

                if (excludeId.HasValue)
                {
                    query = query.Where(s => s.Id != excludeId.Value);
                }

                return await query.AnyAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking state code: " + ex.Message, ex);
            }
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
