using Entities;
using ServiceContract;
using ServiceContract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CountriesService : ICountryService
    {
        private readonly  PersonsDbContext _db;
        public CountriesService(PersonsDbContext personsDbContext)
        {
           _db = personsDbContext;
        }
        public CountryResponseDTO AddCountry(CountryAddDTO? countryAddRequest)
        {
            Country country = countryAddRequest.ToCountry();
            country.CountryId = new Guid();
            _db.Countries.Add(country);
            _db.SaveChanges();
            return country.ToCountryResponse();
        }

        public List<CountryResponseDTO> GetAllCountries()
        {
            return _db.Countries.Select(country=>country.ToCountryResponse()).ToList();
        }

        public CountryResponseDTO? GetCountryById(Guid? countryId)
        {
            if (countryId == null || countryId == Guid.Empty)
                return null;

            Country? countryResponse = _db.Countries.FirstOrDefault(country => country.CountryId == countryId);
            if (countryResponse == null)return null;
            return countryResponse.ToCountryResponse();
        }

   
    }
}
