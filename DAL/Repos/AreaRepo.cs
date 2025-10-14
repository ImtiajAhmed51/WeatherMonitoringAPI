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
    internal class AreaRepo : IAsyncRepo<Area, int, Area>, IAreaRepo
    {
        private readonly WeatherContext context;

        public AreaRepo()
        {
            context = new WeatherContext();
        }

        public AreaRepo(WeatherContext context)
        {
            this.context = context;
        }

        public async Task<Area> CreateAsync(Area entity)
        {
            try
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.IsActive = true;
                entity.IsDeleted = false;

                context.Areas.Add(entity);
                await context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating area: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var area = await context.Areas.FindAsync(id);

                if (area == null)
                    return false;

                area.IsDeleted = true;
                area.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting area: {ex.Message}", ex);
            }
        }

        public async Task<List<Area>> GetAllAsync()
        {
            try
            {
                return await context.Areas
                    .Where(a => !a.IsDeleted)
                    .OrderBy(a => a.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all areas: {ex.Message}", ex);
            }
        }

        public async Task<Area> GetByIdAsync(int id)
        {
            try
            {
                return await context.Areas
                    .Where(a => !a.IsDeleted && a.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting area by id: {ex.Message}", ex);
            }
        }

        public async Task<List<Area>> GetByCityIdAsync(int cityId)
        {
            try
            {
                return await context.Areas
                    .Where(a => !a.IsDeleted && a.CityId == cityId)
                    .OrderBy(a => a.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting areas by city id: {ex.Message}", ex);
            }
        }

        public async Task<List<Area>> GetByCityIdWithWeatherAsync(int cityId)
        {
            try
            {
                return await context.Areas
                    .Include(a => a.WeatherRecords)
                    .Include(a => a.Alerts)
                    .Where(a => !a.IsDeleted && a.CityId == cityId)
                    .OrderBy(a => a.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting areas with weather by city id: {ex.Message}", ex);
            }
        }

        public async Task<List<Area>> GetByPostalCodeAsync(string postalCode)
        {
            try
            {
                return await context.Areas
                    .Where(a => !a.IsDeleted && a.PostalCode == postalCode)
                    .OrderBy(a => a.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting areas by postal code: {ex.Message}", ex);
            }
        }

        public async Task<List<Area>> GetByAreaTypeAsync(string areaType)
        {
            try
            {
                return await context.Areas
                    .Where(a => !a.IsDeleted && a.AreaType == areaType)
                    .OrderBy(a => a.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting areas by area type: {ex.Message}", ex);
            }
        }

        public async Task<Area> GetWithCityAsync(int areaId)
        {
            try
            {
                return await context.Areas
                    .Include(a => a.City)
                    .Where(a => !a.IsDeleted && a.Id == areaId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting area with city: {ex.Message}", ex);
            }
        }

        public async Task<Area> GetWithAllRelationsAsync(int areaId)
        {
            try
            {
                return await context.Areas
                    .Include(a => a.City)
                    .Include(a => a.City.State)
                    .Include(a => a.City.State.Country)
                    .Include(a => a.WeatherRecords)
                    .Include(a => a.Alerts)
                    .Where(a => !a.IsDeleted && a.Id == areaId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting area with all relations: {ex.Message}", ex);
            }
        }

        public async Task<List<Area>> SearchByNameAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return new List<Area>();

                return await context.Areas
                    .Where(a => !a.IsDeleted && a.Name.Contains(searchTerm))
                    .OrderBy(a => a.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching areas by name: {ex.Message}", ex);
            }
        }

        public async Task<Area> UpdateAsync(Area entity)
        {
            try
            {
                var existingArea = await context.Areas.FindAsync(entity.Id);

                if (existingArea == null)
                    throw new Exception("Area not found");

                existingArea.Name = entity.Name;
                existingArea.Latitude = entity.Latitude;
                existingArea.Longitude = entity.Longitude;
                existingArea.PostalCode = entity.PostalCode;
                existingArea.Population = entity.Population;
                existingArea.AreaSize = entity.AreaSize;
                existingArea.AreaType = entity.AreaType;
                existingArea.CityId = entity.CityId;
                existingArea.IsActive = entity.IsActive;
                existingArea.UpdatedAt = DateTime.UtcNow;
                existingArea.UpdatedBy = entity.UpdatedBy;

                context.Entry(existingArea).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return existingArea;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating area: {ex.Message}", ex);
            }
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await context.Areas.CountAsync(a => !a.IsDeleted);
        }

        public async Task<int> GetActiveCountAsync()
        {
            return await context.Areas.CountAsync(a => !a.IsDeleted && a.IsActive);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await context.Areas.AnyAsync(a => !a.IsDeleted && a.Id == id);
        }

        public async Task<bool> ExistsByNameInCityAsync(string name, int cityId)
        {
            return await context.Areas.AnyAsync(a =>
                !a.IsDeleted &&
                a.Name == name &&
                a.CityId == cityId);
        }

        public async Task<List<Area>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await context.Areas
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
