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
    internal class CityRepo : IAsyncRepo<City, int, City>, ICityRepo
    {
        private readonly WeatherContext context;

        public CityRepo()
        {
            context = new WeatherContext();
        }

        public CityRepo(WeatherContext context)
        {
            this.context = context;
        }

        public async Task<City> CreateAsync(City entity)
        {
            try
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.IsActive = true;
                entity.IsDeleted = false;

                context.Cities.Add(entity);
                await context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating city: " + ex.Message, ex);
            }
        }

        public async Task<City> UpdateAsync(City entity)
        {
            try
            {
                var existingCity = await context.Cities.FindAsync(entity.Id);

                if (existingCity == null)
                    throw new Exception("City not found");

                existingCity.Name = entity.Name;
                existingCity.Latitude = entity.Latitude;
                existingCity.Longitude = entity.Longitude;
                existingCity.PostalCode = entity.PostalCode;
                existingCity.Population = entity.Population;
                existingCity.Area = entity.Area;
                existingCity.Elevation = entity.Elevation;
                existingCity.TimeZone = entity.TimeZone;
                existingCity.CityType = entity.CityType;
                existingCity.IsCapital = entity.IsCapital;
                existingCity.StateId = entity.StateId;
                existingCity.IsActive = entity.IsActive;
                existingCity.UpdatedAt = DateTime.UtcNow;
                existingCity.UpdatedBy = entity.UpdatedBy;

                context.Entry(existingCity).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return existingCity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating city: " + ex.Message, ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var city = await context.Cities.FindAsync(id);

                if (city == null)
                    return false;

                city.IsDeleted = true;
                city.UpdatedAt = DateTime.UtcNow;

                context.Entry(city).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting city: " + ex.Message, ex);
            }
        }

        public async Task<City> GetByIdAsync(int id)
        {
            try
            {
                return await context.Cities
                    .Where(c => !c.IsDeleted && c.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting city by id: " + ex.Message, ex);
            }
        }

        public async Task<List<City>> GetAllAsync()
        {
            try
            {
                return await context.Cities
                    .Where(c => !c.IsDeleted)
                    .OrderBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all cities: " + ex.Message, ex);
            }
        }

        public async Task<List<City>> GetByStateIdAsync(int stateId)
        {
            try
            {
                return await context.Cities
                    .Where(c => !c.IsDeleted && c.StateId == stateId)
                    .OrderBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting cities by state: " + ex.Message, ex);
            }
        }

        public async Task<List<City>> GetByStateIdWithAreasAsync(int stateId)
        {
            try
            {
                return await context.Cities
                    .Include(c => c.Areas)
                    .Where(c => !c.IsDeleted && c.StateId == stateId)
                    .OrderBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting cities with areas: " + ex.Message, ex);
            }
        }

        public async Task<City> GetWithAreasAsync(int cityId)
        {
            try
            {
                return await context.Cities
                    .Include(c => c.Areas)
                    .Where(c => !c.IsDeleted && c.Id == cityId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting city with areas: " + ex.Message, ex);
            }
        }

        public async Task<City> GetWithStateAndCountryAsync(int cityId)
        {
            try
            {
                return await context.Cities
                    .Include(c => c.State)
                    .Include(c => c.State.Country)
                    .Where(c => !c.IsDeleted && c.Id == cityId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting city with state and country: " + ex.Message, ex);
            }
        }

        public async Task<List<City>> GetCapitalCitiesAsync()
        {
            try
            {
                return await context.Cities
                    .Where(c => !c.IsDeleted && c.IsCapital)
                    .OrderBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting capital cities: " + ex.Message, ex);
            }
        }

        public async Task<List<City>> GetByPostalCodeAsync(string postalCode)
        {
            try
            {
                return await context.Cities
                    .Where(c => !c.IsDeleted && c.PostalCode == postalCode)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting cities by postal code: " + ex.Message, ex);
            }
        }

        public async Task<List<City>> SearchByNameAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return new List<City>();

                return await context.Cities
                    .Where(c => !c.IsDeleted && c.Name.Contains(searchTerm))
                    .OrderBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching cities: " + ex.Message, ex);
            }
        }

        public async Task<List<City>> GetNearbyCitiesAsync(decimal latitude, decimal longitude, decimal radiusKm)
        {
            try
            {
                
                var cities = await context.Cities
                    .Where(c => !c.IsDeleted)
                    .ToListAsync();

                return cities
                    .Where(c => (Decimal)CalculateDistance(latitude, longitude, c.Latitude, c.Longitude) <= radiusKm)
                    .OrderBy(c => CalculateDistance(latitude, longitude, c.Latitude, c.Longitude))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting nearby cities: " + ex.Message, ex);
            }
        }

       
        private double CalculateDistance(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            var R = 6371; 
            var dLat = ToRadians((double)(lat2 - lat1));
            var dLon = ToRadians((double)(lon2 - lon1));

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians((double)lat1)) * Math.Cos(ToRadians((double)lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
