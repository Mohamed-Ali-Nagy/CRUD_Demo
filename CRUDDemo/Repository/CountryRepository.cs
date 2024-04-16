using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContract;

namespace Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly PersonsDbContext _db;
        public CountryRepository(PersonsDbContext db)
        {
            _db = db;
        }
        public async Task<Country> AddCountry(Country country)
        {
            _db.Countries.Add(country);
           await _db.SaveChangesAsync();
            return country;
        }

        public async Task<List<Country>> GetAllCountries()
        {
           return await _db.Countries.ToListAsync();
        }

        public async Task<Country?> GetCountryByCountryID(Guid countryID)
        {
            return await _db.Countries.FirstOrDefaultAsync(c => countryID == c.CountryId);
        }

        public async Task<Country?> GetCountryByCountryName(string countryName)
        {
            return await _db.Countries.FirstOrDefaultAsync(c=>c.CountryName == countryName);
        }
    }
}
