# 🌦️ Weather Monitoring System

A comprehensive three-tier ASP.NET Web API application for monitoring weather conditions, managing locations, and tracking weather alerts across different geographical areas.

## 📋 Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
- [Getting Started](#getting-started)
- [Database Schema](#database-schema)

## 🎯 Overview

The Weather Monitoring System is a robust API-based application that allows users to:
- Track weather conditions across multiple locations
- Manage weather alerts and warnings
- Search and filter locations by various criteria
- Calculate nearest locations using geospatial algorithms
- Generate weather statistics and analytics

## 🏗️ Architecture

The application follows a **Three-Tier Architecture** pattern:

```
┌─────────────────────────────────────────┐
│   Presentation Layer (API Controllers)   │
│     - LocationController                 │
│     - AlertController                    │
│     - WeatherRecordController            │
└──────────────────┬──────────────────────┘
                   │
┌──────────────────▼──────────────────────┐
│   Business Logic Layer (Services)        │
│     - LocationService                    │
│     - AlertService                       │
│     - WeatherRecordService               │
│     - AutoMapper Configuration           │
└──────────────────┬──────────────────────┘
                   │
┌──────────────────▼──────────────────────┐
│   Data Access Layer (Repositories)       │
│     - LocationRepo                       │
│     - AlertRepo                          │
│     - WeatherRecordRepo                  │
│     - Entity Framework Context           │
└──────────────────┬──────────────────────┘
                   │
                ┌──▼──┐
                │ DB  │
                └─────┘
```

### Layer Responsibilities

#### 1. **Presentation Layer (API Controllers)**
- Handles HTTP requests and responses
- Input validation and error handling
- Routes management
- Returns standardized JSON responses

#### 2. **Business Logic Layer (Services)**
- Core business rules and logic
- Data transformation using AutoMapper
- Complex calculations (distance, statistics)
- Data validation
- DTO to Entity mapping

#### 3. **Data Access Layer (Repositories)**
- Direct database operations
- Entity Framework queries
- CRUD operations
- Database context management

## ✨ Features

### Location Management
- ✅ CRUD operations for locations
- 🔍 Search by name, country, or coordinates
- 📍 Find nearby locations within radius
- 🌍 Geospatial distance calculations (Haversine formula)
- 📊 Location statistics by country

### Weather Records
- ✅ Track temperature, humidity, precipitation, wind speed
- 📅 Filter by date ranges
- 🔢 Statistical analysis (min, max, average)
- 📈 Daily weather statistics
- 🕒 Recent and latest weather data retrieval

### Alert System
- ⚠️ Create and manage weather alerts
- 🚨 Track active alerts
- ⏰ Alert expiration management
- 📊 Alert statistics by severity and location
- 🔔 Activate/deactivate alerts

### Advanced Features
- 🎯 Nearest location with active alerts and weather
- 📊 Daily aggregated weather statistics
- 🗺️ Location-based weather and alert queries
- 🔢 Comprehensive counting and statistics

## 🛠️ Technologies Used

### Backend
- **ASP.NET Web API** - RESTful API framework
- **Entity Framework 6** - ORM for database operations
- **AutoMapper** - Object-to-object mapping
- **C# .NET Framework** - Primary programming language

### Database
- **SQL Server** - Primary database
- **Entity Framework Code First** - Database modeling

### Architecture Patterns
- **Three-Tier Architecture** - Separation of concerns
- **Repository Pattern** - Data access abstraction
- **Dependency Injection** - Factory pattern for data access
- **DTO Pattern** - Data transfer objects

## 📁 Project Structure

```
WeatherMonitoringSystem/
├── PresentationAPI/
│   └── Controllers/
│       ├── LocationController.cs
│       ├── AlertController.cs
│       └── WeatherRecordController.cs
├── BLL/ (Business Logic Layer)
│   ├── Services/
│   │   ├── LocationService.cs
│   │   ├── AlertService.cs
│   │   └── WeatherRecordService.cs
│   └── DTOs/
│       ├── LocationDTO.cs
│       ├── AlertDTO.cs
│       ├── WeatherRecordDTO.cs
│       └── ... (other DTOs)
└── DAL/ (Data Access Layer)
    ├── Models/
    │   ├── Location.cs
    │   ├── Alert.cs
    │   └── WeatherRecord.cs
    ├── Repos/
    │   ├── LocationRepo.cs
    │   ├── AlertRepo.cs
    │   └── WeatherRecordRepo.cs
    └── Interfaces/
        ├── IRepo.cs
        └── ... (other Interface)
```

## 🔌 API Endpoints

### Location Endpoints

| Method | Endpoint                              | Params                                         | Description                              |
| ------ | ------------------------------------- | ---------------------------------------------- | ---------------------------------------- |
| GET    | `/api/location/all`                   | —                                              | Get all locations                        |
| GET    | `/api/location/{id}`                  | path: `id:int`                                 | Get location by ID                       |
| POST   | `/api/location/create`                | body: `LocationDTO`                            | Create new location                      |
| PUT    | `/api/location/update`                | body: `LocationDTO`                            | Update location                          |
| DELETE | `/api/location/delete/{id}`           | path: `id:int`                                 | Delete location                          |
| GET    | `/api/location/all/alerts`            | —                                              | All locations with alerts (joined)       |
| GET    | `/api/location/all/weather`           | —                                              | All locations with weather (joined)      |
| GET    | `/api/location/{id}/weather`          | path: `id:int`                                 | One location with weather                |
| GET    | `/api/location/{id}/alerts`           | path: `id:int`                                 | One location with alerts                 |
| GET    | `/api/location/{id}/all`              | path: `id:int`                                 | One location with weather + alerts       |
| GET    | `/api/location/{id}/weather/stats`    | path: `id:int`                                 | Weather stats for a location             |
| GET    | `/api/location/search`                | query: `keyword`                               | Search by name or country                |
| GET    | `/api/location/search/name`           | query: `name`                                  | Search by name                           |
| GET    | `/api/location/search/country`        | query: `country`                               | Search by country                        |
| GET    | `/api/location/find`                  | query: `name`, `country`                       | Find by name & country (exact)           |
| GET    | `/api/location/coordinates`           | query: `latitude:decimal`, `longitude:decimal` | Find by coordinates                      |
| GET    | `/api/location/alerts/active`         | —                                              | Locations having **active** alerts       |
| GET    | `/api/location/alerts/active/basic`   | —                                              | Locations with active alerts (basic)     |
| GET    | `/api/location/nearby`                | query: `latitude, longitude, radiusKm=50`      | Nearby locations within radius           |
| GET    | `/api/location/nearby/alerts`         | query: `latitude, longitude, radiusKm=50`      | Nearby locations + alerts                |
| GET    | `/api/location/nearby/weather`        | query: `latitude, longitude, radiusKm=50`      | Nearby locations + weather               |
| GET    | `/api/location/nearest`               | query: `latitude, longitude, radiusKm=50`      | Nearest location                         |
| GET    | `/api/location/nearest/alerts`        | query: `latitude, longitude, radiusKm=50`      | Nearest location + alerts                |
| GET    | `/api/location/nearest/weather`       | query: `latitude, longitude, radiusKm=50`      | Nearest location + weather               |
| GET    | `/api/location/nearest/weather/stats` | query: `latitude, longitude, radiusKm=50`      | Nearest location + weather stats         |
| GET    | `/api/location/nearest/all`           | query: `latitude, longitude, radiusKm=50`      | Nearest location (all joins)             |
| GET    | `/api/location/nearest/current`       | query: `latitude, longitude, radiusKm=50`      | Nearest + active alerts + latest weather |
| GET    | `/api/location/count`                 | —                                              | Total location count                     |
| GET    | `/api/location/stats/country`         | —                                              | Count by country                         |
| GET    | `/api/location/exists/{id}`           | path: `id:int`                                 | Location exists?                         |


### Alert Endpoints

| Method | Endpoint                                   | Params                        | Description                        |
| ------ | ------------------------------------------ | ----------------------------- | ---------------------------------- |
| GET    | `/api/alert/all`                           | —                             | Get all alerts                     |
| GET    | `/api/alert/{id}`                          | path: `id:int`                | Get alert by ID                    |
| GET    | `/api/alert/all/location`                  | —                             | All alerts with locations (joined) |
| GET    | `/api/alert/{id}/location`                 | path: `id:int`                | Alert + location                   |
| GET    | `/api/alert/active`                        | —                             | Active alerts                      |
| GET    | `/api/alert/active/location`               | —                             | Active alerts + locations          |
| GET    | `/api/alert/location/{locationId}`         | path: `locationId:int`        | Alerts by location                 |
| GET    | `/api/alert/location/{locationId}/details` | path: `locationId:int`        | Alerts by location (details)       |
| GET    | `/api/alert/location/{locationId}/count`   | path: `locationId:int`        | Alert count for a location         |
| GET    | `/api/alert/expired`                       | —                             | Expired alerts                     |
| GET    | `/api/alert/expiration`                    | query: `startDate`, `endDate` | Alerts by expiration range         |
| GET    | `/api/alert/date-range`                    | query: `startDate`, `endDate` | Alerts by created date range       |
| GET    | `/api/alert/recentwithlocation`            | query: `days=7`               | Recent alerts + location           |
| GET    | `/api/alert/severity/{severity}`           | path: `severity`              | Alerts by severity                 |
| GET    | `/api/alert/total/count`                   | —                             | Total alert count                  |
| GET    | `/api/alert/total/activecount`             | —                             | Total **active** alert count       |
| GET    | `/api/alert/stats/severity`                | —                             | Stats grouped by severity          |
| GET    | `/api/alert/stats/location`                | —                             | Stats grouped by location          |
| POST   | `/api/alert/create`                        | body: `AlertDTO`              | Create alert                       |
| PUT    | `/api/alert/update`                        | body: `AlertDTO`              | Update alert                       |
| DELETE | `/api/alert/delete/{id}`                   | path: `id:int`                | Delete alert                       |
| PUT    | `/api/alert/{id}/activate`                 | path: `id:int`                | Activate alert                     |
| PUT    | `/api/alert/{id}/deactivate`               | path: `id:int`                | Deactivate alert                   |


### Weather Record Endpoints

| Method | Endpoint                                             | Params                                        | Description                         |
| ------ | ---------------------------------------------------- | --------------------------------------------- | ----------------------------------- |
| GET    | `/api/weather/all`                                   | —                                             | All weather records                 |
| GET    | `/api/weather/{id}`                                  | path: `id:int`                                | Weather record by ID                |
| POST   | `/api/weather/create`                                | body: `WeatherRecordDTO`                      | Create record                       |
| PUT    | `/api/weather/update`                                | body: `WeatherRecordDTO`                      | Update record                       |
| DELETE | `/api/weather/delete/{id}`                           | path: `id:int`                                | Delete record                       |
| GET    | `/api/weather/all/locations`                         | —                                             | All records with locations (joined) |
| GET    | `/api/weather/{id}/location`                         | path: `id:int`                                | One record + location               |
| GET    | `/api/weather/location/{locationId}`                 | path: `locationId:int`                        | Records by location                 |
| GET    | `/api/weather/location/{locationId}/details`         | path: `locationId:int`                        | Records by location (details)       |
| GET    | `/api/weather/location/{locationId}/latest`          | path: `locationId:int`                        | Latest record for location          |
| GET    | `/api/weather/location/{locationId}/latest/details`  | path: `locationId:int`                        | Latest record + details             |
| GET    | `/api/weather/location/{locationId}/stats`           | path: `locationId:int`                        | Stats for a location                |
| GET    | `/api/weather/daterange`                             | query: `start`, `end`                         | Records by date range               |
| GET    | `/api/weather/daterange/locations`                   | query: `start`, `end`                         | Date range + locations              |
| GET    | `/api/weather/location/{locationId}/daterange`       | path: `locationId:int`; query: `start`, `end` | Records by location & range         |
| GET    | `/api/weather/location/{locationId}/daterange/stats` | path: `locationId:int`; query: `start`, `end` | Stats by location & range           |
| GET    | `/api/weather/after`                                 | query: `date`                                 | Records after date                  |
| GET    | `/api/weather/before`                                | query: `date`                                 | Records before date                 |
| GET    | `/api/weather/recent`                                | query: `count=10 (1..100)`                    | N recent records                    |
| GET    | `/api/weather/recent/locations`                      | query: `count=10 (1..100)`                    | N recent + locations                |
| GET    | `/api/weather/location/{locationId}/recent`          | path: `locationId:int`; query: `count=10`     | N recent for location               |
| GET    | `/api/weather/latest`                                | —                                             | Latest for all locations            |
| GET    | `/api/weather/temperature`                           | query: `min:decimal`, `max:decimal`           | By temperature range                |
| GET    | `/api/weather/temperature/locations`                 | query: `min`, `max`                           | Temp range + locations              |
| GET    | `/api/weather/humidity`                              | query: `min`, `max (0..100)`                  | By humidity range                   |
| GET    | `/api/weather/humidity/locations`                    | query: `min`, `max (0..100)`                  | Humidity + locations                |
| GET    | `/api/weather/precipitation`                         | query: `min>=0`, `max`                        | By precipitation range              |
| GET    | `/api/weather/windspeed`                             | query: `min>=0`, `max`                        | By wind speed range                 |
| GET    | `/api/weather/count`                                 | —                                             | Total record count                  |
| GET    | `/api/weather/location/{locationId}/count`           | path: `locationId:int`                        | Count by location                   |
| GET    | `/api/weather/stats/bylocation`                      | —                                             | Record counts by location           |
| GET    | `/api/weather/location/{locationId}/dates`           | path: `locationId:int`                        | First/last record dates             |
| GET    | `/api/weather/exists/{id}`                           | path: `id:int`                                | Record exists?                      |


## 🚀 Getting Started

### Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.7.2 or later
- SQL Server 2016 or later
- AutoMapper (10.0.0)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/weather-monitoring-system.git
   ```

2. **Open the solution**
   - Open `WeatherMonitoringSystem.sln` in Visual Studio

3. **Update connection string**
   - Navigate to `DAL/WeatherContext.cs`
   - Update the connection string with your SQL Server details

4. **Run migrations**
   ```bash
   Update-Database
   ```

5. **Build and run**
   - Press `F5` or click Run in Visual Studio
   - API will be available at `http://localhost:[port]/api/`

## 💾 Database Schema

### Location Table
```sql
- Id (int, PK)
- Name (varchar(50))
- Latitude (decimal)
- Longitude (decimal)
- Country (varchar(50))
```

### WeatherRecord Table
```sql
- Id (int, PK)
- LocationId (int, FK)
- CreatedAt (datetime)
- Temperature (decimal)
- Humidity (decimal)
- Precipitation (decimal)
- WindDirection (varchar)
- WindSpeed (decimal)
- RecordedAt (datetime)
```

### Alert Table
```sql
- Id (int, PK)
- LocationId (int, FK)
- Condition (varchar)
- Message (varchar)
- Severity (varchar)
- IsActive (bool)
- TriggeredAt (datetime)
- UpdatedAt (datetime, nullable)
- CreatedAt (datetime)
- ExpiresAt (datetime, nullable)
```

## 📊 Key Algorithms

### Haversine Distance Formula
The system uses the Haversine formula to calculate distances between geographical coordinates:

```csharp
private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
{
    var dLat = ToRadians(lat2 - lat1);
    var dLon = ToRadians(lon2 - lon1);
    var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
    var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
    return EarthRadius * c;
}
```

## 📝 Response Format

All API responses follow a standardized format:

**Success Response:**
```json
{
  "success": true,
  "data": { ... }
}
```

**Error Response:**
```json
{
  "success": false,
  "message": "Error description"
}
```
